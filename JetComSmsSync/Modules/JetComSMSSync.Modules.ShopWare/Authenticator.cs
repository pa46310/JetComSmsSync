using JetComSMSSync.Modules.ShopWare.Models;
using RestSharp;
using RestSharp.Authenticators;

public class Authenticator : IAuthenticator
{
    private readonly AccountModel _account;

    public Authenticator(AccountModel account)
    {
        _account = account;
    }

    public void Authenticate(IRestClient client, IRestRequest request)
    {
        request.AddOrUpdateHeader("X-Api-Partner-Id", _account.PartnerID);
        request.AddOrUpdateHeader("X-Api-Secret", _account.SecretKey);
    }
}

