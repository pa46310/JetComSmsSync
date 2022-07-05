using JetComSmsSync.Core;
using JetComSmsSync.Services;
using JetComSmsSync.Services.Interfaces;
using JetComSmsSync.ViewModels;
using JetComSmsSync.Views;

using Microsoft.Extensions.Configuration;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

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
                .Enrich.WithProperty("Application", nameof(JetComSmsSync))
                .Enrich.WithProperty("StartingPage", Configuration["StartingPage"])
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
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
#if CDKSYNC || DEBUG || RELEASE
            moduleCatalog.AddModule<Modules.CDK.CDKModule>();
#endif

#if TEKMETRICSYNC || DEBUG || RELEASE
            moduleCatalog.AddModule<Modules.Tekmetric.TekmetricModule>();
#endif

#if SHOP4DSYNC || DEBUG || RELEASE
            moduleCatalog.AddModule<Modules.Shop4D.Shop4DModule>();
#endif

#if TIREMASTERSYNC || DEBUG || RELEASE
            moduleCatalog.AddModule<Modules.TireMasterView.TireMasterViewModule>();
#endif

#if PROTRACTORSYNC || DEBUG || RELEASE
            moduleCatalog.AddModule<Modules.Protractor.ProtractorModule>();
#endif

#if SHOPWARESYNC || DEBUG || RELEASE
            moduleCatalog.AddModule<JetComSMSSync.Modules.ShopWare.ShopWareModule>();
#endif

#if LOC8NEARMESYNC || DEBUG || RELEASE
            moduleCatalog.AddModule<Modules.loc8nearme.loc8nearmeModule>();
#endif
        }
    }
}
