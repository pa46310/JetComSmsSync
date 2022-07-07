using JetComSmsSync.Core;
using JetComSmsSync.Modules.loc8nearme.Models;

using MaterialDesignThemes.Wpf;

using Prism.Commands;
using Prism.Mvvm;

namespace JetComSmsSync.Modules.loc8nearme.ViewModels
{
    public class CreateUserPageViewModel : BindableBase
    {
        private readonly AccountModel _account;
        private readonly DatabaseClient _database;

        private string _id;
        public string ID
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private int? _accountId;
        public int? AccountID
        {
            get { return _accountId; }
            set { SetProperty(ref _accountId, value); }
        }

        private string _url;
        public string Url
        {
            get { return _url; }
            set { SetProperty(ref _url, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (SetProperty(ref _isBusy, value))
                {
                    AddCommand.RaiseCanExecuteChanged();
                    UpdateCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private bool _bulkChanges;
        public bool BulkChanges
        {
            get { return _bulkChanges; }
            set { SetProperty(ref _bulkChanges, value); }
        }

        public CreateUserPageViewModel(DatabaseClient database)
        {
            _database = database;
        }

        public CreateUserPageViewModel(DatabaseClient database, AccountModel account)
            : this(database)
        {
            _account = account;
            if (_account != null)
            {
                ID = _account.Id;
                AccountID = _account.AccountId;
                Url = _account.Url;
            }
        }

        private DelegateCommand _addCommand;
        public DelegateCommand AddCommand =>
            _addCommand ?? (_addCommand = new DelegateCommand(ExecuteAddCommand, () => !IsBusy));

        async void ExecuteAddCommand()
        {
            try
            {
                IsBusy = true;
                var account = new AccountModel
                {
                    AccountId = AccountID ?? 0,
                    Url = Url,
                };
                var result = await _database.InsertAccountAsync(account);
                if (!string.IsNullOrEmpty(result) && !BulkChanges)
                {
                    DialogHost.Close(DialogIdentifiers.RootDialog, true);
                }
                IsBusy = false;
            }
            catch (System.Exception ex)
            {
                IsBusy = false;
                MessageService.Instance.ShowError(ex, "Failed to add");
            }
        }

        private DelegateCommand _updateCommand;
        public DelegateCommand UpdateCommand =>
            _updateCommand ?? (_updateCommand = new DelegateCommand(ExecuteUpdateCommand, () => !IsBusy));

        async void ExecuteUpdateCommand()
        {
            try
            {
                IsBusy = true;
                var account = new AccountModel
                {
                    AccountId = AccountID ?? 0,
                    Url = Url,
                    Id = ID,
                };
                var result = await _database.UpdateAccountAsync(account);
                if (result > 0 && !BulkChanges)
                {
                    DialogHost.Close(DialogIdentifiers.RootDialog, true);
                }
                IsBusy = false;
            }
            catch (System.Exception ex)
            {
                IsBusy = false;
                MessageService.Instance.ShowError(ex, "Failed to update");
            }
        }

        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand =>
            _cancelCommand ?? (_cancelCommand = new DelegateCommand(ExecuteCancelCommand, () => !IsBusy));

        void ExecuteCancelCommand()
        {
            try
            {
                DialogHost.Close(DialogIdentifiers.RootDialog);
            }
            catch { }
        }
    }
}
