using JetComSmsSync.Services.Interfaces;
using Prism.Mvvm;

namespace JetComSmsSync.Modules.TireMasterView.Models
{
    public class AccountModel : BindableBase, ISelectable
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }
        public string Server { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
