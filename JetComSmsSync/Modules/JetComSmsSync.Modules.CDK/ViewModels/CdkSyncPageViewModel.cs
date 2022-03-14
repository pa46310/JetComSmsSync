using JetComSmsSync.Core;
using JetComSmsSync.Core.Models;
using JetComSmsSync.Modules.CDK.Models;
using JetComSmsSync.Services.Interfaces;

using Prism.Commands;
using Prism.Mvvm;

using Serilog;
using Serilog.Context;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace JetComSmsSync.Modules.CDK.ViewModels
{
    public class CdkSyncPageViewModel : BindableBase
    {
        private System.Timers.Timer _autoSendTimer;
        private CancellationTokenSource _cts;
        public DateTime? PreviousDate { get; set; }
        private ILogger Log { get; } = Serilog.Log.ForContext<CdkSyncPageViewModel>();
        private CdkDatabaseClient Database { get; }
        public RecurrenceModel[] Items { get; } = RecurrenceModel.Default;

        private RecurrenceModel _selectedRecurrence;
        public RecurrenceModel SelectedRecurrence
        {
            get { return _selectedRecurrence; }
            set { SetProperty(ref _selectedRecurrence, value); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                if (SetProperty(ref _message, value))
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        Log.Debug(value);
                    }
                }
            }
        }

        private bool _canCancel;
        public bool CanCancel
        {
            get { return _canCancel; }
            set
            {
                if (SetProperty(ref _canCancel, value))
                {
                    CancelCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (SetProperty(ref _isBusy, value))
                {
                    Message = string.Empty;
                    RefreshCommand.RaiseCanExecuteChanged();
                    SendCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private bool _isAutoSend;
        public bool IsAutoSend
        {
            get { return _isAutoSend; }
            set
            {
                if (SetProperty(ref _isAutoSend, value))
                {
                    UpdateAutoSend();
                }
            }
        }

        private DateTime _selectedTime;
        public DateTime SelectedTime
        {
            get { return _selectedTime; }
            set { SetProperty(ref _selectedTime, value); }
        }

        private AccountModel[] _accounts;
        public AccountModel[] Accounts
        {
            get { return _accounts; }
            set { SetProperty(ref _accounts, value); }
        }

        private DateTime _startDate = DateTime.UtcNow.Date.AddDays(-1);
        public DateTime StartDate
        {
            get { return _startDate; }
            set { SetProperty(ref _startDate, value); }
        }

        public string StartDateText => StartDate.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");

        private bool _isAllSelected = true;
        public bool IsAllSelected
        {
            get { return _isAllSelected; }
            set
            {
                if (SetProperty(ref _isAllSelected, value))
                {
                    if (Accounts is null || Accounts.Length == 0) return;

                    foreach (var account in Accounts)
                    {
                        account.IsSelected = value;
                    }
                }
            }
        }

        public CdkSyncPageViewModel(CdkDatabaseClient database, ICacheService cache)
        {
            Database = database;
            _cache = cache;

            SelectedRecurrence = Items.FirstOrDefault();
            RefreshCommand.Execute();
        }

        private DelegateCommand _refreshCommand;
        public DelegateCommand RefreshCommand =>
            _refreshCommand ?? (_refreshCommand = new DelegateCommand(ExecuteRefreshCommand, () => !IsBusy));

        async void ExecuteRefreshCommand()
        {
            try
            {
                IsBusy = true;
                Message = "Loading accounts...";
                var items = await Task.Run(Database.GetAccounts);
                foreach (var item in items)
                {
                    item.IsSelected = IsAllSelected;
                }
                Accounts = items.OrderBy(x => x.DealerId).ToArray();
                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                Log.LogAndShowError(ex, "Failed to refresh");
            }
        }

        private DelegateCommand<DateTime?> _sendCommand;
        public DelegateCommand<DateTime?> SendCommand =>
            _sendCommand ?? (_sendCommand = new DelegateCommand<DateTime?>(ExecuteSendCommand, (d) => !IsBusy));

        async void ExecuteSendCommand(DateTime? startDate)
        {
            try
            {
                IsBusy = true;
                CanCancel = true;
                using (_cts = new CancellationTokenSource())
                {
                    await Task.Run(() => SendSync(startDate), _cts.Token);
                }
                CanCancel = false;
                IsBusy = false;
            }
            catch (Exception ex)
            {
                CanCancel = false;
                IsBusy = false;
                Log.LogAndShowError(ex, "Failed to send");
            }
        }

        private void SendSync(DateTime? startDate)
        {
            var selected = Accounts.Where(x => x.IsSelected).ToList();
            if (selected.Count == 0) return;

            var current = 0;
            var total = selected.Count;
            if (startDate.HasValue)
            {
                Log.Debug("Sending delta data");
            }
            else
            {
                Log.Debug("Sending bulk data");
            }

            foreach (var account in selected)
            {
                current++;
                using var context1 = LogContext.PushProperty("DealerId", account.DealerId);
                using var context2 = LogContext.PushProperty("BaseUrl", account.BaseUrl);
                _cts.Token.ThrowIfCancellationRequested();
                var client = new CdkClient(account);

                var processing = 1;
                var totalProcessing = 3;

                var message = $"({current}/{total}) [{processing++}/{totalProcessing}] Sending help employees.";
                SendHelpEmployees(account, client, startDate, message);

                message = $"({current}/{total}) [{processing++}/{totalProcessing}] Sending customers.";
                SendCustomers(account, client, startDate, message);

                message = $"({current}/{total}) [{processing++}/{totalProcessing}] Sending repair orders.";
                SendRepairOrderHistory(account, client, startDate, message);
            }
        }

        private void SendRepairOrderHistory(AccountModel account, CdkClient client, DateTime? startDate, string message)
        {
            try
            {
                _cts.Token.ThrowIfCancellationRequested();

                Message = $"{message} Loading live data";
                var live = Database.GetSRODForCompare(account.BigID);
                _cts.Token.ThrowIfCancellationRequested();

                Message = $"{message} Loading local data";
                var isBulk = true;
                var start = startDate.HasValue ? startDate.Value : DateTime.Now.AddYears(-1);
                var end = DateTime.Now;
                var local = isBulk ? client.GetServiceRepairOrderHistory(start, end) : client.GetServiceRepairOrderClosed(start, end);
                _cts.Token.ThrowIfCancellationRequested();

                Message = $"{message} Comparing local and live data";
                var unique = local.Except(live, CdkComparers.SRODComparer).ToArray();
                _cts.Token.ThrowIfCancellationRequested();

                Message = $"{message} Sending unique data";
                Log.Debug("Local: {0}, Live: {1}, Unique: {2}", local.Length, live.Length, unique.Length);
                var inserted = Database.InsertServiceRepairOrders(unique, account.BigID);
                Log.Debug("{0} items inserted", inserted);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to send jobs");
            }
        }

        private void SendCustomers(AccountModel account, CdkClient client, DateTime? startDate, string message)
        {
            try
            {
                _cts.Token.ThrowIfCancellationRequested();

                Message = $"{message} Loading live data";
                var live = Database.GetCustomersForCompare(account.BigID);
                _cts.Token.ThrowIfCancellationRequested();

                Message = $"{message} Loading local data";
                var local = startDate.HasValue ? client.GetCustomerDelta(startDate.Value) : client.GetCustomerBulk();
                _cts.Token.ThrowIfCancellationRequested();

                Message = $"{message} Comparing local and live data";
                var unique = local.Except(live, CdkComparers.CustomerComparer).ToArray();
                _cts.Token.ThrowIfCancellationRequested();

                Message = $"{message} Sending unique data";
                Log.Debug("Local: {0}, Live: {1}, Unique: {2}", local.Length, live.Length, unique.Length);
                var inserted = Database.InsertCustomers(unique, account.BigID);
                Log.Debug("{0} items inserted", inserted);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to send jobs");
            }
        }

        private void SendHelpEmployees(AccountModel account, CdkClient client, DateTime? startDate, string message)
        {
            try
            {
                _cts.Token.ThrowIfCancellationRequested();

                Message = $"{message} Loading live data";
                var live = Database.GetHelpEmployeeForCompare(account.BigID);
                _cts.Token.ThrowIfCancellationRequested();

                Message = $"{message} Loading local data";
                var local = startDate.HasValue ? client.GetHelpEmployeeDelta(startDate.Value) : client.GetHelpEmployeeBulk();
                _cts.Token.ThrowIfCancellationRequested();

                Message = $"{message} Comparing local and live data";
                var unique = local.Except(live, CdkComparers.HelpEmployeeComparer).ToArray();
                _cts.Token.ThrowIfCancellationRequested();

                Message = $"{message} Sending unique data";
                Log.Debug("Local: {0}, Live: {1}, Unique: {2}", local.Length, live.Length, unique.Length);
                var inserted = Database.InsertHelpEmployees(unique, account.BigID);
                Log.Debug("{0} items inserted", inserted);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to send jobs");
            }
        }

        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand =>
            _cancelCommand ?? (_cancelCommand = new DelegateCommand(ExecuteCancelCommand, () => CanCancel));

        void ExecuteCancelCommand()
        {
            try
            {
                CanCancel = false;
                _cts.Cancel();
                IsAutoSend = false;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to cancel");
            }
        }

        private void UpdateAutoSend()
        {
            try
            {
                if (IsAutoSend)
                {
                    if (SelectedRecurrence is null)
                    {
                        IsAutoSend = false;
                        return;
                    }

                    var interval = SelectedRecurrence.Millisecond;
                    var breathingTime = 2_000;
                    if (interval < breathingTime)
                    {
                        interval = breathingTime;
                    }

                    _autoSendTimer = new System.Timers.Timer(interval)
                    {
                        AutoReset = false,
                    };
                    _autoSendTimer.Elapsed += AutoSend_TimerElapsed;
                    Log.Debug("Next send will occur at {0}", DateTime.Now.AddMilliseconds(interval));
                    _autoSendTimer.Start();
                }
                else
                {
                    _autoSendTimer?.Stop();
                    _autoSendTimer?.Dispose();
                    _autoSendTimer = null;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to update auto send");
            }
        }

        private void AutoSend_TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                // execute send now
                var start = StartDate;
                var end = DateTime.UtcNow;
                IsBusy = true;
                using (_cts = new CancellationTokenSource())
                {
                    CanCancel = true;
                    SendSync(start);
                    CanCancel = false;
                }
                IsBusy = false;
                StartDate = end;
                UpdateAutoSend();
            }
            catch (Exception ex)
            {
                CanCancel = false;
                IsBusy = false;
                Log.Error(ex, "Failed to run auto send timer");
            }
        }

        private DelegateCommand _clearCacheCommand;
        private readonly ICacheService _cache;

        public DelegateCommand ClearCacheCommand =>
            _clearCacheCommand ?? (_clearCacheCommand = new DelegateCommand(ExecuteClearCacheCommand));

        void ExecuteClearCacheCommand()
        {
            try
            {
                var result = MessageBox.Show($"Do you want to clear the cache?\r\n {_cache.GetCacheSizeStr()} will be freed.", "Clear cache", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _cache.ClearCache();
                }
            }
            catch (Exception ex)
            {
                Log.LogAndShowError(ex, "Failed to clear cache");
            }
        }
    }
}
