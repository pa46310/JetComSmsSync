using JetComSmsSync.Modules.TireMasterView.Models;

namespace JetComSmsSync.Modules.TireMasterView
{
    public class ServiceClient
    {
        private AccountModel _account;

        public ServiceClient(AccountModel account)
        {
            _account = account;
        }
    }
}
