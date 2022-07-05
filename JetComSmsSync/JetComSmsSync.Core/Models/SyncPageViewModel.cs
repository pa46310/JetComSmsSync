using JetComSmsSync.Core.Mvvm;
using JetComSmsSync.Services.Interfaces;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using Serilog;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JetComSmsSync.Core.Models
{
    public abstract class SyncPageViewModel<T> : ViewModelBase, INavigationAware where T : ISelectable
    {
        private System.Timers.Timer _autoSendTimer;
        private CancellationTokenSource _cts;
        public DateTime? PreviousDate { get; set; }
        protected virtual ILogger Log { get; } = Serilog.Log.ForContext<SyncPageViewModel<T>>();
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

        private T[] _accounts;
        public T[] Accounts
        {
            get { return _accounts; }
            set { SetProperty(ref _accounts, value); }
        }

        private DateTime _startDate = DateTime.UtcNow.Date.AddDays(-1);
        public virtual DateTime StartDate
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

        public SyncPageViewModel()
        {
            SelectedRecurrence = Items.FirstOrDefault();
        }

        private DelegateCommand _refreshCommand;
        public DelegateCommand RefreshCommand =>
            _refreshCommand ?? (_refreshCommand = new DelegateCommand(ExecuteRefreshCommand, () => !IsBusy));

        private async void ExecuteRefreshCommand()
        {
            try
            {
                IsBusy = true;
                MessageService.Instance.ShowPersistentMessage("Loading accounts...");
                Accounts = await GetAccounts();
                IsBusy = false;
                MessageService.Instance.HidePersistentMessage();
            }
            catch (Exception ex)
            {
                IsBusy = false;
                MessageService.Instance.HidePersistentMessage();
                Log.LogAndShowError(ex, "Failed to refresh");
            }
        }

        protected abstract Task<T[]> GetAccounts();

        private DelegateCommand<DateTime?> _sendCommand;
        public DelegateCommand<DateTime?> SendCommand =>
            _sendCommand ?? (_sendCommand = new DelegateCommand<DateTime?>(ExecuteSendCommand, (d) => !IsBusy));

        private async void ExecuteSendCommand(DateTime? startDate)
        {
            try
            {
                IsBusy = true;
                CanCancel = true;
                using (_cts = new CancellationTokenSource())
                {
                    var selected = Accounts.Where(x => x.IsSelected).ToList();
                    if (selected.Count == 0)
                    {
                        MessageService.Instance.EnqueInformation("No accounts selected. Please select at least one account to send data");
                        return;
                    }

                    Log.Debug("Begin send with start as {0}", startDate);
                    await Task.Run(() => Send(startDate, selected, _cts.Token));
                    Log.Debug("End send with start as {0}", startDate);
                }
            }
            catch (Exception ex)
            {
                Log.LogAndShowError(ex, "Failed to send");
            }
            finally
            {
                CanCancel = false;
                IsBusy = false;
            }
        }

        protected abstract void Send(DateTime? startDate, IList<T> accounts, CancellationToken ct);

        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand =>
            _cancelCommand ?? (_cancelCommand = new DelegateCommand(ExecuteCancelCommand, () => CanCancel));

        private void ExecuteCancelCommand()
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
                    var selected = Accounts.Where(x => x.IsSelected).ToList();
                    if (selected.Count > 0)
                    {
                        Send(start, selected, _cts.Token);
                    }
                    CanCancel = false;
                }
                IsBusy = false;
                StartDate = end;
            }
            catch (Exception ex)
            {
                CanCancel = false;
                IsBusy = false;
                Log.Error(ex, "Failed to run auto send timer");
            }

            UpdateAutoSend();
        }

        #region INavigationAware
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            RefreshCommand.Execute();
        }
        public virtual bool IsNavigationTarget(NavigationContext navigationContext) => true;
        public virtual void OnNavigatedFrom(NavigationContext navigationContext) { }
        #endregion
    }
}
