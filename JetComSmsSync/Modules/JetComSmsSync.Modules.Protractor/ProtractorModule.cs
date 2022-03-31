using JetComSmsSync.Modules.Protractor.ViewModels;
using JetComSmsSync.Modules.Protractor.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace JetComSmsSync.Modules.Protractor
{
    public class ProtractorModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ProtractorSyncPage, ProtractorSyncPageViewModel>();
        }
    }
}