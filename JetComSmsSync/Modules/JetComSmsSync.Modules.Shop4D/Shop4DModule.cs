using JetComSmsSync.Modules.Shop4D.ViewModels;
using JetComSmsSync.Modules.Shop4D.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace JetComSmsSync.Modules.Shop4D
{
    public class Shop4DModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Shop4DSyncPage, Shop4DSyncPageViewModel>();
        }
    }
}