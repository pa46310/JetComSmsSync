using JetComSmsSync.Core;
using JetComSmsSync.Core.Models;
using JetComSmsSync.Modules.Tekmetric.Models;
using JetComSmsSync.Modules.Tekmetric.Responses;
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

namespace JetComSmsSync.Modules.Tekmetric.ViewModels
{
    public class TekmetricSyncPageViewModel : BindableBase
    {
        private System.Timers.Timer _autoSendTimer;
        private CancellationTokenSource _cts;
        public DateTime? PreviousDate { get; set; }
        private ILogger Log { get; } = Serilog.Log.ForContext<TekmetricSyncPageViewModel>();
        private DatabaseClient Database { get; }
        private string ApplicationName = "Tekmetric";

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

        public TekmetricSyncPageViewModel(DatabaseClient database, ICacheService cache)
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
                Accounts = items.OrderBy(x => x.ShopID).ToArray();
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

            foreach (var account in selected)
            {
                current++;
                using var context1 = LogContext.PushProperty("BigID", account.BigID);
                using var context2 = LogContext.PushProperty("ShopID", account.ShopID);
                _cts.Token.ThrowIfCancellationRequested();
                var client = new ServiceClient(account);

                var processing = 0;
                var totalProcessing = 5;

                var message = $"({current}/{total}) [{processing++}/{totalProcessing}] Sending Customers.";
                SendCustomers(account, client, startDate, message);

                message = $"({current}/{total}) [{processing++}/{totalProcessing}] Sending vehicles.";
                SendVehicles(account, client, startDate, message);

                message = $"({current}/{total}) [{processing++}/{totalProcessing}] Sending appointments.";
                SendAppointments(account, client, startDate, message);

                message = $"({current}/{total}) [{processing++}/{totalProcessing}] Sending repair orders.";
                SendRepairOrders(account, client, startDate, message);

                message = $"({current}/{total}) [{processing++}/{totalProcessing}] Sending jobs.";
                SendJobs(account, client, startDate, message);
            }
        }

        private void SendJobs(AccountModel account, ServiceClient client, DateTime? startDate, string message)
        {
            try
            {
                Message = $"{message} [1/3]Loading existing jobs...";
                _cts.Token.ThrowIfCancellationRequested();
                var existingJobs = Database.GetJobForCompare(account.BigID);

                Message = $"{message} [2/3]Loading existing parts...";
                _cts.Token.ThrowIfCancellationRequested();
                var existingParts = Database.GetPartForCompare(account.BigID);

                Message = $"{message} [3/3]Loading existing labors...";
                _cts.Token.ThrowIfCancellationRequested();
                var existingLabors = Database.GetLaborForCompare(account.BigID);

                _cts.Token.ThrowIfCancellationRequested();
                var jobComparer = new JobComparer();
                var partComparer = new PartComparer();
                var laborComparer = new LaborComparer();

                foreach (var job in client.GetJobs(startDate))
                {
                    Message = $"{message} Updating({job.Number + 1}/{job.TotalPages})...";
                    _cts.Token.ThrowIfCancellationRequested();
                    // Jobs
                    var uniqueJobs = job.Content.Except(existingJobs, jobComparer).ToList();
                    var result = Database.InsertJobs(uniqueJobs);
                    Log.Debug("Jobs. Unique: {0}, Inserted: {1}", uniqueJobs.Count, result);
                    JetComLog.Error($"{result} jobs inserted", ApplicationName, account.BigID, IsAutoSend);

                    // Parts
                    var uniqueParts = job.Content.SelectMany(x => x.Parts).Except(existingParts, partComparer).ToList();
                    result = Database.InsertParts(uniqueParts);
                    Log.Debug("Parts. Unique: {0}, Inserted: {1}", uniqueParts.Count, result);
                    JetComLog.Error($"{result} parts inserted", ApplicationName, account.BigID, IsAutoSend);

                    // Labors
                    var uniqueLabors = job.Content.SelectMany(x => x.Labor).Except(existingLabors, laborComparer).ToList();
                    result = Database.InsertLabors(uniqueLabors);
                    JetComLog.Error($"{result} labors inserted", ApplicationName, account.BigID, IsAutoSend);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to send jobs");
            }
        }

        private void SendRepairOrders(AccountModel account, ServiceClient client, DateTime? startDate, string message)
        {
            try
            {
                Message = $"{message} [1/1]Loading existings repair orders...";
                _cts.Token.ThrowIfCancellationRequested();
                var existingRepairOrders = Database.GetRepairOrderForCompare(account.BigID);

                _cts.Token.ThrowIfCancellationRequested();
                var comparer = new RepairOrderComparer();

                foreach (var repairOrder in client.GetRepairOrders(startDate))
                {
                    _cts.Token.ThrowIfCancellationRequested();
                    Message = $"{message} Updating({repairOrder.Number + 1}/{repairOrder.TotalPages})...";
                    // Repair Orders
                    var uniqueRepairOrders = repairOrder.Content.Except(existingRepairOrders, comparer).ToList();
                    var result = Database.InsertRepairOrder(uniqueRepairOrders);
                    Log.Debug("Repair orders. Unique: {0}, Inserted: {1}", uniqueRepairOrders.Count, result);
                    JetComLog.Error($"{result} repair orders inserted", ApplicationName, account.BigID, IsAutoSend);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to send repair orders");
            }
        }

        private void SendAppointments(AccountModel account, ServiceClient client, DateTime? startDate, string message)
        {
            try
            {
                Message = $"{message} [1/1]Loading existing appointments...";
                _cts.Token.ThrowIfCancellationRequested();
                var existingAppointments = Database.GetAppointmentForCompare(account.BigID);

                _cts.Token.ThrowIfCancellationRequested();
                var comparer = new AppointmentComparer();
                foreach (var appointment in client.GetAppointments(startDate))
                {
                    _cts.Token.ThrowIfCancellationRequested();
                    // Appointments
                    Message = $"{message} Updating({appointment.Number + 1}/{appointment.TotalPages})...";
                    var uniqueAppointment = appointment.Content.Except(existingAppointments, comparer).ToList();
                    var result = Database.InsertAppointments(uniqueAppointment);
                    Log.Debug("Appointments. Unique: {0}, Inserted: {1}", uniqueAppointment.Count, result);
                    JetComLog.Error($"{result} appointments inserted", ApplicationName, account.BigID, IsAutoSend);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to send appointments");
            }
        }

        private void SendVehicles(AccountModel account, ServiceClient client, DateTime? startDate, string message)
        {
            try
            {
                Message = $"{message} [1/1]Loading existing vehicles...";
                _cts.Token.ThrowIfCancellationRequested();
                var existingVehicles = Database.GetVehicleForCompare(account.BigID);

                _cts.Token.ThrowIfCancellationRequested();
                var comparer = new VehicleComparer();
                foreach (var vehicle in client.GetVehicles(startDate))
                {
                    _cts.Token.ThrowIfCancellationRequested();
                    // Vehicles
                    Message = $"{message} Updating({vehicle.Number + 1}/{vehicle.TotalPages})...";
                    var uniqueVehicles = vehicle.Content.Except(existingVehicles, comparer).ToList();
                    var result = Database.InsertVehicles(uniqueVehicles);
                    Log.Debug("Vehicles. Unique: {0}, Inserted: {1}", uniqueVehicles.Count, result);
                    JetComLog.Error($"{result} vehicle inserted", ApplicationName, account.BigID, IsAutoSend);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to send vehicles");
            }
        }

        private void SendCustomers(AccountModel account, ServiceClient client, DateTime? startDate, string message)
        {
            try
            {
                Message = $"{message} [1/4]Loading existing customers...";
                _cts.Token.ThrowIfCancellationRequested();
                var existingCustomers = Database.GetCustomerForCompare(account.BigID);

                Message = $"{message} [2/4]Loading existing phone numbers...";
                _cts.Token.ThrowIfCancellationRequested();
                var existingPhones = Database.GetPhoneNumberForCompare(account.BigID);

                Message = $"{message} [3/4]Loading existing customer types...";
                _cts.Token.ThrowIfCancellationRequested();
                var existingCustomerTypes = Database.GetCustomerTypeForCompare(account.BigID);

                Message = $"{message} [4/4]Loading existing address...";
                _cts.Token.ThrowIfCancellationRequested();
                var existingAddresses = Database.GetAddressForCompare(account.BigID);

                var result = 0;
                _cts.Token.ThrowIfCancellationRequested();

                var customerComparer = new CustomerComparer();
                var phoneComparer = new PhoneComparer();
                var customerTypeComparer = new CustomerTypeComparer();
                var addressComparer = new AddressComparer();

                foreach (var customer in client.GetCustomers(startDate))
                {
                    _cts.Token.ThrowIfCancellationRequested();
                    Message = $"{message} Updating({customer.Number + 1}/{customer.TotalPages})...";
                    // Customers
                    var uniqueCustomers = customer.Content.Except(existingCustomers, customerComparer).ToList();
                    result = Database.InsertCustomer(uniqueCustomers);
                    Log.Debug("Customers. Unique: {0}, Inserted: {1}", uniqueCustomers.Count, result);
                    JetComLog.Error($"{result} customers inserted", ApplicationName, account.BigID, IsAutoSend);
                    // Phone Numbers
                    var uniquePhones = customer.Content.SelectMany(x => x.Phone).Except(existingPhones, phoneComparer).ToList();
                    result = Database.InsertPhoneNumber(uniquePhones);
                    Log.Debug("Phones. Unique: {0}, Inserted: {1}", uniquePhones.Count, result);
                    JetComLog.Error($"{result} phones inserted", ApplicationName, account.BigID, IsAutoSend);
                    // Customer Types
                    var uniqueCustomerTypes = customer.Content.Select(x => x.CustomerType)
                        .Except(existingCustomerTypes, customerTypeComparer).ToList();
                    result = Database.InsertCustomerTypes(uniqueCustomerTypes);
                    Log.Debug("Customer Types. Unique: {0}, Inserted: {1}", uniqueCustomerTypes.Count, result);
                    JetComLog.Error($"{result} customer types inserted", ApplicationName, account.BigID, IsAutoSend);
                    // Addresses
                    var uniqueAddress = customer.Content.Select(x => x.Address).Except(existingAddresses, addressComparer).ToList();
                    result = Database.InsertAddress(uniqueAddress);
                    Log.Debug("Addresses. Unique: {0}, Inserted: {1}", uniqueAddress.Count, result);
                    JetComLog.Error($"{result} addresses inserted", ApplicationName, account.BigID, IsAutoSend);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to send customers");
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
                var last = PreviousDate ?? StartDate;
                PreviousDate = DateTime.UtcNow;
                IsBusy = true;
                using (_cts = new CancellationTokenSource())
                {
                    CanCancel = true;
                    SendSync(last);
                    CanCancel = false;
                }
                IsBusy = false;
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
