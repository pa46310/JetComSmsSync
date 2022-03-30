using JetComSmsSync.Core.Models;
using JetComSmsSync.Core.Utils;
using JetComSmsSync.Modules.Shop4D.Models;
using JetComSmsSync.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Serilog;
using Serilog.Context;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace JetComSmsSync.Modules.Shop4D.ViewModels
{
    public class Shop4DSyncPageViewModel : BindableBase
    {
        private System.Timers.Timer _autoSendTimer;
        private CancellationTokenSource _cts;
        public DateTime? PreviousDate { get; set; }
        private ILogger Log { get; } = Serilog.Log.ForContext<Shop4DSyncPageViewModel>();
        private Shop4DDatabaseClient Database { get; }
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

        private bool _isAllSelected;
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

        public Shop4DSyncPageViewModel(Shop4DDatabaseClient database, ICacheService cache)
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
                Accounts = items.OrderBy(x => x.CompanyId).ToArray();
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
            var start = DateTime.MinValue;
            var end = DateTime.Now;
            if (startDate.HasValue)
            {
                Log.Debug("Sending delta data");
                start = startDate.Value;
            }
            else
            {
                Log.Debug("Sending bulk data");
            }


            foreach (var account in selected)
            {
                current++;
                using var context1 = LogContext.PushProperty("CompanyId", account.CompanyId);
                using var context2 = LogContext.PushProperty("FullName", account.AccountFullName);
                _cts.Token.ThrowIfCancellationRequested();
                var client = new Shop4DClient(account);

                var accountPrefix = $"[{current}/{total}] ";
                Message = accountPrefix + "Getting data for compare";
                var uniqueContact = Database.GetContactForCompare(account.BigID);
                var uniqueCustomer = Database.GetCustomerForCompare(account.BigID);
                var uniqueLabors = Database.GetLaborForCompare(account.BigID);
                var uniqueParts = Database.GetPartForCompare(account.BigID);
                var uniqueRepairOrders = Database.GetRepairOrderForCompare(account.BigID);
                var uniqueVehicles = Database.GetVehicleForCompare(account.BigID);

                foreach (var range in DateUtils.GetRangeByMonth(start, end, 1))
                {
                    var prefix = $"{accountPrefix}[{range.Item1.ToShortDateString()}-{range.Item1.ToShortDateString()}] ";

                    var inserted = 0;
                    Message = prefix + "Getting repair orders";
                    Log.Debug("Date range. Start: {0} End: {1}", range.Item1, range.Item2);
                    var repairOrders = client.GetRepairOrders(range.Item1, range.Item2);

                    try
                    {
                        Message = prefix + "Inserting contacts";
                        var contacts = repairOrders.SelectMany(x => x.Customer.Contact).Except(uniqueContact, Shop4DComparers.Contact).ToList();
                        uniqueContact.AddRange(contacts);
                        inserted = Database.InsertContact(contacts);
                        Log.Debug("{0} inserted", inserted);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Failed to send contacts");
                    }

                    try
                    {
                        Message = prefix + "Inserting customers";
                        var customers = repairOrders.Select(x => x.Customer).Where(x => !string.IsNullOrEmpty(x.CustomerId)).Except(uniqueCustomer, Shop4DComparers.Customer).ToList();
                        uniqueCustomer.AddRange(customers);
                        inserted = Database.InsertCustomers(customers);
                        Log.Debug("{0} inserted", inserted);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Failed to send customers");
                    }

                    try
                    {
                        Message = prefix + "Inserting labors";
                        var labors = repairOrders.SelectMany(x => x.LineItemDetail.SelectMany(x => x.Labor)).Except(uniqueLabors, Shop4DComparers.Labor).ToList();
                        uniqueLabors.AddRange(labors);
                        inserted = Database.InsertLabor(labors);
                        Log.Debug("{0} inserted", inserted);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Failed to send labors");
                    }

                    try
                    {
                        Message = prefix + "Inserting parts";
                        var parts = repairOrders.SelectMany(x => x.LineItemDetail.SelectMany(x => x.Parts)).Except(uniqueParts, Shop4DComparers.Part).ToList();
                        uniqueParts.AddRange(parts);
                        inserted = Database.InsertParts(parts);
                        Log.Debug("{0} inserted", inserted);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Failed to send parts");
                    }

                    try
                    {
                        Message = prefix + "Inserting repair orders";
                        var ros = repairOrders.Except(uniqueRepairOrders, Shop4DComparers.RepairOrder).ToList();
                        uniqueRepairOrders.AddRange(ros);
                        inserted = Database.InsertRepairOrder(ros);
                        Log.Debug("{0} inserted", inserted);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Failed to send repair orders");
                    }

                    try
                    {
                        Message = prefix + "Inserting vehicles";
                        var vehicles = repairOrders.Select(x => x.Vehicle).Where(x => !string.IsNullOrEmpty(x.VehicleId)).Except(uniqueVehicles, Shop4DComparers.Vehicle).ToList();
                        uniqueVehicles.AddRange(vehicles);
                        inserted = Database.InsertVehicles(vehicles);
                        Log.Debug("{0} inserted", inserted);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Failed to send vehicles");
                    }
                }
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
