using JetComSmsSync.Core;
using JetComSmsSync.Modules.CDK.ViewModels;
using JetComSmsSync.Modules.CDK.Views;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace JetComSmsSync.Modules.CDK
{
    public class CDKModule : IModule
    {
        private IRegionManager _regionManager;

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager = containerProvider.Resolve<IRegionManager>();

            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(CdkSyncPage));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<CdkDatabaseClient>();
            containerRegistry.RegisterForNavigation<CdkSyncPage, CdkSyncPageViewModel>();
        }
    }
}