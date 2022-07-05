using JetComSmsSync.Core.Utils;
using JetComSmsSync.Modules.loc8nearme.Models.Requests;
using JetComSmsSync.Modules.loc8nearme.Models.Responses;

using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

using RestSharp;

using Serilog;
using Serilog.Context;

using System.Collections.Generic;
using System.Linq;

namespace JetComSmsSync.Modules.loc8nearme
{
    public class ServiceClient
    {
        public IRestClient Client { get; set; }
        public ServiceClient(IConfiguration configuration)
        {
            var url = configuration.GetValue("Loc8NearMe:Url", "https://www.loc8nearme.com");
            Client = RestClientUtils.CreateRestClient(url);
            var accept = Client.DefaultParameters.FirstOrDefault(x => x.Name == "Accept");
            if (accept != null)
            {
                accept.Value = "application/json";
            }
        }

        public CommentResponse[] GetComments(MoreCommentPayload payload)
        {
            var request = new RestRequest("/request_controller.php")
                .AddQueryParameter("q", "moreComments")
                .AddJsonBody(payload);

            var response = Client.Post(request);
            try
            {
                if (string.IsNullOrEmpty(response.Content) || response.Content == "0")
                {
                    return new CommentResponse[0];
                }
                else
                {
                    var data = JsonConvert.DeserializeObject<CommentResponse[]>(response.Content);
                    return data;
                }
            }
            catch (System.Exception ex)
            {
                Log.Error(ex, "Failed to deserialize data");
                return null;
            }
        }

        public IEnumerable<CommentResponse[]> GetAllComments(string businessId)
        {
            using var context1 = LogContext.PushProperty("BusinessId", businessId);
            Log.Debug("Getting all comments", businessId);
            var offset = 0;
            while (true)
            {
                using var context2 = LogContext.PushProperty("Offset", offset);
                var request = new MoreCommentPayload
                {
                    BusinessId = businessId,
                    Offset = offset,
                };

                var data = GetComments(request);
                if (data is null || data.Length == 0)
                {
                    Log.Debug("Final page reached. Total: {0}", offset);
                    yield break;
                }

                offset += data.Length;
                yield return data;

                if (data.Length < 10)
                {
                    Log.Debug("Final page reached. Total: {0}", offset);
                    yield break;
                }

                Log.Debug("Got {0} items", offset);
            }
        }
    }
}
