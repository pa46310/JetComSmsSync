using JetComSmsSync.Services;
using JetComSmsSync.Services.Interfaces;
using JetComSmsSync.Views;

using Prism.Ioc;
using Prism.Modularity;

using Serilog;

using System.Windows;

namespace JetComSmsSync
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static string SeqUrl => "https://logs.valueaddedonline.com";

        public const string JetComSeqApiKey = "g10Imh0Coi9bjXLvF9XA";
        protected override void OnStartup(StartupEventArgs e)
        {
            Core.JetComLog.Error("test", "test", "", false);
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
#if DEBUG
                .Enrich.WithProperty("Environment", "Development")
#else
                .Enrich.WithProperty("Environment", "Production")
#endif
                .WriteTo.Seq(SeqUrl, apiKey: JetComSeqApiKey)
                .CreateLogger();
            base.OnStartup(e);
        }
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
            containerRegistry.RegisterSingleton<ICacheService, CacheService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
#if CDK

            moduleCatalog.AddModule<Modules.CDK.CDKModule>();
#elif Tekmetric
            moduleCatalog.AddModule<Modules.Tekmetric.TekmetricModule>();
#endif
        }
    }
}
