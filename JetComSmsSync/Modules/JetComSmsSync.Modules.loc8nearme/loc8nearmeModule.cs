using JetComSmsSync.Core;
using JetComSmsSync.Modules.loc8nearme.ViewModels;
using JetComSmsSync.Modules.loc8nearme.Views;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace JetComSmsSync.Modules.loc8nearme
{
    public class loc8nearmeModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            _ = containerProvider.Resolve<IRegionManager>()
                .RegisterViewWithRegion(RegionNames.ContentRegion, typeof(Loc8nearmeSyncPage));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Loc8nearmeSyncPage, Loc8nearmeSyncPageViewModel>();
        }
    }
}