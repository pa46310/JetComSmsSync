using Prism.Mvvm;

namespace JetComSmsSync.Modules.Shop4D.Models
{
    public class AccountModel : BindableBase
    {
        public string AccountFullName { get; set; }
        public string CompanyId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string BigID { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }
    }
}
