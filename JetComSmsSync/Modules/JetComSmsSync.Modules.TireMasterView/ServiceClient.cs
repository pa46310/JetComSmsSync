using JetComSmsSync.Core.Utils;
using JetComSmsSync.Modules.TireMasterView.Models;
using RestSharp;
using System;
using static JetComSmsSync.Core.Models.RecurrenceModel;

namespace JetComSmsSync.Modules.TireMasterView
{
    public class ServiceClient
    {
        private AccountModel _account;
        public IRestClient Client { get; }

        public ServiceClient(AccountModel account)
        {
            _account = account;
            Client = RestClientUtils.CreateRestClient("http://api02.valueaddedonline.com");
        }

        public int GetItemCount(DateTime startDate)
        {
            var request = new RestRequest("/api/TireMasterView/count");
            request.AddQueryParameter("startDate", startDate.ToString("O"));
            request.AddQueryParameter("locationId", _account.LocationId.ToString());

            var response = Client.Get<int>(request);
            return response.Data;
        }

        public PaginatedData<ItemModel> GetItems(DateTime startDate, int offset = 0, int limit = 100)
        {
            var request = new RestRequest("/api/TireMasterView");
            request.AddQueryParameter("startDate", startDate.ToString("O"));
            request.AddQueryParameter("locationId", _account.LocationId.ToString());
            request.AddQueryParameter("limit", limit.ToString());
            request.AddQueryParameter("offset", offset.ToString());

            var response = Client.Get<PaginatedData<ItemModel>>(request);
            foreach (var item in response.Data.Data)
            {
                item.BigId = _account.BigId;
            }
            return response.Data;
        }
    }
}
