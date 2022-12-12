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
        public static RestClient CreateRestClient(string url)
        {
            var output = new RestClient(url)
                .UseNewtonsoftJson();

            return output;
        }
    }
}
