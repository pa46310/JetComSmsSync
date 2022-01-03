using JetComSmsSync.Services.Interfaces;

namespace JetComSmsSync.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
