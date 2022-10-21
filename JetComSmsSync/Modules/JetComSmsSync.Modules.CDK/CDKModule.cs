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
        public void OnInitialized(IContainerProvider containerProvider)
        {
            _ = containerProvider.Resolve<IRegionManager>()
                   .RegisterViewWithRegion(RegionNames.ContentRegion, typeof(CdkSyncPage));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<DatabaseClient>();
            containerRegistry.RegisterForNavigation<CdkSyncPage, CdkSyncPageViewModel>();
        }
    }
}