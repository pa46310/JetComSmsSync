using JetComSmsSync.Core.Utils;
using JetComSmsSync.Modules.Shop4D.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;

namespace JetComSmsSync.Modules.Shop4D
{
    public class Shop4DClient
    {
        public IRestClient Client { get; }

        private string _authKey;
        private readonly AccountModel _account;

        public Shop4DClient(AccountModel account)
        {
            var client = RestClientUtils.CreateRestClient("https://shop4d.com/common/api/");
            Client = client;
            _account = account;
        }

        public void Login()
        {
            var request = new RestRequest("/authorize/");
            request.AddParameter("companyid", _account.CompanyId);
            request.AddParameter("username", _account.Username);
            request.AddParameter("password", _account.Password);

            var response = Client.Post(request);
            var data = JsonConvert.DeserializeObject<AuthenticationTokenResponse>(response.Content);
            _authKey = RestSharp.Extensions.StringExtensions.UrlEncode(data.AuthenticationKey);
        }

        public RepairOrderInfo[] GetRepairOrders(DateTime start, DateTime end)
        {
            if (string.IsNullOrEmpty(_authKey))
            {
                Login();
            }

            var request = new RestRequest("/rodata/");
            request.AddParameter("auth", _authKey);
            request.AddParameter("rangeStart", start.ToString("yyyy-MM-dd"));
            request.AddParameter("rangeEnd", end.ToString("yyyy-MM-dd"));

            var response = Client.Post(request);
            var data = JsonConvert.DeserializeObject<RepairOrderResponse>(response.Content);
            if (data.IsUnAuthorized)
            {
                Login();
                response = Client.Post(request);
                data = JsonConvert.DeserializeObject<RepairOrderResponse>(response.Content);
            }
            data.UpdateList(_account.BigID);
            return data.Success.Select(x => x.RepairOrderInfo).ToArray();
        }
    }
}
