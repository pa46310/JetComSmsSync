using JetComSmsSync.Core;
using JetComSmsSync.Services;
using JetComSmsSync.Services.Interfaces;
using JetComSmsSync.Views;
using Microsoft.Extensions.Configuration;
using Prism.Ioc;
using Prism.Modularity;

using Serilog;
using System;
using System.IO;
using System.Windows;

namespace JetComSmsSync
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public const string JetComSeqApiKey = "g10Imh0Coi9bjXLvF9XA";
        public IConfiguration Configuration { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var seqUrl = Configuration["LogServerUrl"];

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
#if DEBUG
                .Enrich.WithProperty("Environment", "Development")
#else
                .Enrich.WithProperty("Environment", "Production")
#endif
                .Enrich.WithProperty("Application", nameof(JetComSMSSync))
                .WriteTo.Seq(seqUrl, apiKey: JetComSeqApiKey)
                .CreateLogger();

            Dispatcher.UnhandledException += Dispatcher_UnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            base.OnStartup(e);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Fatal("Unknown error: {0}", e.ExceptionObject);
        }

        private void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Log.Fatal(e.Exception, "Unknown error");
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(Configuration);
            containerRegistry.RegisterInstance<IMessageService>(MessageService.Instance);
            containerRegistry.RegisterSingleton<ICacheService, CacheService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<Modules.CDK.CDKModule>();
            moduleCatalog.AddModule<Modules.Tekmetric.TekmetricModule>();
            moduleCatalog.AddModule<Modules.Shop4D.Shop4DModule>();
            moduleCatalog.AddModule<Modules.TireMasterView.TireMasterViewModule>();
            moduleCatalog.AddModule<Modules.Protractor.ProtractorModule>();
            moduleCatalog.AddModule<JetComSMSSync.Modules.ShopWare.ShopWareModule>();
        }
    }
}
