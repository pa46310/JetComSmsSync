using JetComSmsSync.Modules.TireMasterView.ViewModels;
using JetComSmsSync.Modules.TireMasterView.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace JetComSmsSync.Modules.TireMasterView
{
    public class TireMasterViewModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<TireMasterViewPage, TireMasterViewPageViewModel>();
        }
    }
}