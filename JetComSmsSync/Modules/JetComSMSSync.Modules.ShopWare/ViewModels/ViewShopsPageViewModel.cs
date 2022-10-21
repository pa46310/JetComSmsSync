using JetComSmsSync.Core;

using MaterialDesignThemes.Wpf;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

using System;
using System.Collections.Generic;
using System.Linq;

namespace JetComSMSSync.Modules.ShopWare.ViewModels
{
    public class ViewShopsPageViewModel : BindableBase, IDialogAware
    {
        private IEnumerable<Responses.ShopResponse> _items;
        public IEnumerable<Responses.ShopResponse> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        public ViewShopsPageViewModel()
        {

        }

        private DelegateCommand _closeCommand;

        public event Action<IDialogResult> RequestClose;

        public DelegateCommand CloseCommand =>
            _closeCommand ?? (_closeCommand = new DelegateCommand(ExecuteCloseCommand));

        public string Title { get; } = "Shop List";

        void ExecuteCloseCommand()
        {
            //DialogHost.Close(DialogIdentifiers.CloseOnClickAwayRootDialog);
            RequestClose?.Invoke(null);
        }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Items = parameters.GetValue<IEnumerable<Responses.ShopResponse>>("Items");
        }
    }
}
