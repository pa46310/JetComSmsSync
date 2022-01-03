using Prism.Mvvm;

namespace JetComSmsSync.Modules.Tekmetric.Models
{
    public class AccountModel : BindableBase
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }
        public string BigID { get; set; }
        public long ShopID { get; set; }
        public string AccountFullName { get; set; }
        public string Environment { get; set; }
        public string AccessToken { get; set; }
    }
}
