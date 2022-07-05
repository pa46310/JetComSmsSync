using Newtonsoft.Json;

using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace JetComSmsSync.Core.Utils
{
    public static class RestClientUtils
    {
        /// <summary>
        /// Creates a new rest client with camelCase naming strategy
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static IRestClient CreateRestClient(string url)
        {
            var output = new RestClient(url)
            {
                CookieContainer = new System.Net.CookieContainer(),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
                Timeout = int.MaxValue,
                ReadWriteTimeout = int.MaxValue,
            }
            .UseNewtonsoftJson();

            return output;
        }
    }
}
