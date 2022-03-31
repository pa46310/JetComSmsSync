using JetComSmsSync.Services.Interfaces;
using MaterialDesignThemes.Wpf;

namespace JetComSmsSync.Core
{
    public class MessageService : IMessageService
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
    }
}
