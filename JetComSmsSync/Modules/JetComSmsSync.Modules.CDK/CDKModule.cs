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
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<CdkDatabaseClient>();
            containerRegistry.RegisterForNavigation<CdkSyncPage, CdkSyncPageViewModel>();
        }
    }
}