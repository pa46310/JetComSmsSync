using JetComSmsSync.Core;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace JetComSmsSync.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        private string _title = "API SMS Sync";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

#if DEBUG
        private BaseTheme _theme = BaseTheme.Dark;
#else
        private BaseTheme _theme = BaseTheme.Light;
#endif
        public BaseTheme Theme
        {
            get { return _theme; }
            set { SetProperty(ref _theme, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        private DelegateCommand<string> _navigateCommand;
        public DelegateCommand<string> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand));

        void ExecuteNavigateCommand(string parameter)
        {
            if (string.IsNullOrEmpty(parameter)) return;

            _regionManager.RequestNavigate(RegionNames.ContentRegion, parameter);
        }

        private DelegateCommand _toggleModeCommand;
        public DelegateCommand ToggleModeCommand =>
            _toggleModeCommand ?? (_toggleModeCommand = new DelegateCommand(ExecuteToggleModeCommand));

        void ExecuteToggleModeCommand()
        {
            if (Theme == BaseTheme.Dark)
            {
                Theme = BaseTheme.Light;
            }
            else
            {
                Theme = BaseTheme.Dark;
            }
        }
    }
}
