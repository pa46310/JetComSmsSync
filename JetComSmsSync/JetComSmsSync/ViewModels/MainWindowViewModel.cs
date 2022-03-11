using Prism.Mvvm;

namespace JetComSmsSync.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "JetCom SMS Sync";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}
