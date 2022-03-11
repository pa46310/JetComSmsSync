using Newtonsoft.Json;

using Prism.Mvvm;


namespace JetComSmsSync.Modules.CDK.Models
{
    public class AccountModel : BindableBase
    {
        private bool _isSelected;
        [JsonIgnore]
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }

        public string BigID { get; set; }
        public string DealerId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string BaseUrl { get; set; }
    }
}
