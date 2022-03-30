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

        public string BigId { get; set; }
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string AccountId { get; set; }
    }
}
