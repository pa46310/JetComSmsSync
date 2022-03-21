using JetComSmsSync.Services.Interfaces;
using Prism.Mvvm;

namespace JetComSMSSync.Modules.ShopWare.Models
{
    public class AccountModel : BindableBase, ISelectable
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }

        public string BigID { get; set; }
        public string PartnerID { get; set; }
        public string SecretKey { get; set; }
        public string TenantID { get; set; }
        public string ShopID { get; set; }
    }
}
