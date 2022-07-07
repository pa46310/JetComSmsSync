using JetComSmsSync.Services.Interfaces;

using MaterialDesignThemes.Wpf;

using Prism.Commands;
using Prism.Mvvm;

using Serilog;

using System;

namespace JetComSmsSync.Core
{
    public class MessageService : BindableBase, IMessageService
    {
        private static MessageService _instance;
        public static MessageService Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = new MessageService();
                }
                return _instance;
            }
        }
        public SnackbarMessageQueue MessageQueue { get; } = new SnackbarMessageQueue();

        public void EnqueInformation(object content)
        {
            MessageQueue.Enqueue(content);
        }

        #region Persistent Snackbar
        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value); }
        }

        private object _content;
        public object Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        private string _cancelText = "CANCEL";
        public string CancelText
        {
            get { return _cancelText; }
            set { SetProperty(ref _cancelText, value); }
        }

        public Action OnCancel { get; private set; }

        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand =>
            _cancelCommand ?? (_cancelCommand = new DelegateCommand(ExecuteCancelCommand));

        void ExecuteCancelCommand()
        {
            OnCancel?.Invoke();
        }

        public void ShowPersistentMessage(object message)
        {
            Content = message;
            CancelText = null;
            IsActive = true;

            OnCancel = null;
        }

        public void ShowPersistentMessage(object message, Action OnCancel)
        {
            Content = message;
            CancelText = "CANCEL";
            IsActive = true;

            this.OnCancel = OnCancel;
        }

        public void HidePersistentMessage()
        {
            Content = null;
            IsActive = false;
            OnCancel = null;
        }
        #endregion

        public bool ConfirmMessage(string text, string caption)
        {
            var result = System.Windows.MessageBox.Show(text, caption, System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question);
            return result == System.Windows.MessageBoxResult.Yes;
        }

        public void ShowError(Exception ex, string caption)
        {
            Log.Error(ex, caption);
           _ = System.Windows.MessageBox.Show(ex.Message, caption, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }
    }
}
