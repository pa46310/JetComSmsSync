using JetComSmsSync.Core;
using JetComSmsSync.Modules.Tekmetric.ViewModels;
using JetComSmsSync.Modules.Tekmetric.Views;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace JetComSmsSync.Modules.Tekmetric
{
    public class TekmetricModule : IModule
    {
        private IRegionManager _regionManager;

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager = containerProvider.Resolve<IRegionManager>();

            _regionManager.RegisterViewWithRegion(RegionNames.TabRegion, typeof(TekmetricSyncPage));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<DatabaseClient>();
            containerRegistry.RegisterForNavigation<TekmetricSyncPage, TekmetricSyncPageViewModel>();
        }
    }
}