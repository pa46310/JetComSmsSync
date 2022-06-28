using JetComSmsSync.Core;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace JetComSmsSync.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        public MainPageViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        private DelegateCommand<string> _navigateCommand;
        public DelegateCommand<string> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand));

        void ExecuteNavigateCommand(string parameter)
        {
            if (string.IsNullOrEmpty(parameter)) return;

            _regionManager.RequestNavigate(RegionNames.TabRegion, parameter);
        }
    }
}
