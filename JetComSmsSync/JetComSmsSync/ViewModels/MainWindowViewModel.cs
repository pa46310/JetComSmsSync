using JetComSmsSync.Core;

using MaterialDesignThemes.Wpf;

using Microsoft.Extensions.Configuration;

using Prism.Commands;
using Prism.Mvvm;

namespace JetComSmsSync.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

#if DEBUG
        private BaseTheme _theme = BaseTheme.Dark;
#else
        private BaseTheme _theme = BaseTheme.Light;
#endif
        public BaseTheme Theme
        {
            get { return _theme; }
            set { SetProperty(ref _theme, value); }
        }

        public SnackbarMessageQueue MessageQueue { get; } = MessageService.Instance.MessageQueue;

        public MainWindowViewModel(IConfiguration configuration)
        {
            Title = configuration["ApplicationName"];
        }

        private DelegateCommand _toggleModeCommand;
        public DelegateCommand ToggleModeCommand =>
            _toggleModeCommand ?? (_toggleModeCommand = new DelegateCommand(ExecuteToggleModeCommand));

        void ExecuteToggleModeCommand()
        {
            if (Theme == BaseTheme.Dark)
            {
                Theme = BaseTheme.Light;
            }
            else
            {
                Theme = BaseTheme.Dark;
            }
        }
    }
}
