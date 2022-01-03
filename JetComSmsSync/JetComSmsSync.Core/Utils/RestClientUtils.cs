using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace JetComSmsSync.Core.Utils
{
    public static class RestClientUtils
    {
        public static IRestClient CreateRestClient(string url)
        {
            var output = new RestClient(url)
            {
                CookieContainer = new System.Net.CookieContainer(),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
            };
            output.UseNewtonsoftJson();
            return output;
        }
    }
}
