using JetComSmsSync.Core;

using JetComSMSSync.Modules.ShopWare.ViewModels;
using JetComSMSSync.Modules.ShopWare.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace JetComSMSSync.Modules.ShopWare
{
    public class ShopWareModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            _ = containerProvider.Resolve<IRegionManager>()
                   .RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ShopWareSyncPage));

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ShopWareSyncPage, ShopWareSyncPageViewModel>();
            containerRegistry.RegisterDialog<ViewShopsPage, ViewShopsPageViewModel>();
        }
    }
}