using JetComSmsSync.Core.Utils;
using JetComSMSSync.Modules.ShopWare.Models;
using JetComSMSSync.Modules.ShopWare.Responses;
using RestSharp;
using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace JetComSMSSync.Modules.ShopWare
{
    public class ServiceClient
    {
        private readonly AccountModel _account;
        private readonly string _limit = "100";
        public IRestClient Client { get; }

        public ServiceClient(AccountModel account)
        {
            _account = account;

            var url = $"https://api.shop-ware.com";
            Client = RestClientUtils.CreateRestClient(url);
            Client.Authenticator = new Authenticator(account);
        }

        public void GetShops()
        {
            var request = new RestRequest($"/api/v1/tenants/{_account.TenantID}/shops");
            var response = Client.Get(request);
        }

        public IEnumerable<PageableResponse<CustomerModel>> GetCustomers(int page = 1)
        {
            do
            {
                var request = new RestRequest($"/api/v1/tenants/{_account.TenantID}/customers");
                request.AddQueryParameter("per_page", _limit);
                if (page > 1)
                {
                    request.AddQueryParameter("page", page.ToString());
                }

                var response = Client.Get<PageableResponse<CustomerModel>>(request);
                if (!response.IsSuccessful)
                {
                    yield break;
                }
                Log.Debug("{0}/{1} page parsed. Total Count: {2}", response.Data.Current_Page, response.Data.Total_Pages, response.Data.Total_Count);
                foreach (var item in response.Data.Results)
                {
                    item.BigID = _account.BigID;
                    item.ShopId = _account.ShopID;
                }
                yield return response.Data;

                if (response.Data.Current_Page >= response.Data.Total_Pages)
                {
                    break;
                }

                page++;

            } while (true);
        }
        public IEnumerable<PageableResponse<PastRecomendationModel>> GetPastRecomendations(int page = 1)
        {
            do
            {
                var request = new RestRequest($"/api/v1/tenants/{_account.TenantID}/past_recommendations");
                request.AddQueryParameter("per_page", _limit);
                if (page > 1)
                {
                    request.AddQueryParameter("page", page.ToString());
                }

                var response = Client.Get<PageableResponse<PastRecomendationModel>>(request);
                if (!response.IsSuccessful)
                {
                    yield break;
                }
                Log.Debug("{0}/{1} page parsed. Total Count: {2}", response.Data.Current_Page, response.Data.Total_Pages, response.Data.Total_Count);
                foreach (var item in response.Data.Results)
                {
                    item.BigID = _account.BigID;
                }
                yield return response.Data;

                if (response.Data.Current_Page >= response.Data.Total_Pages)
                {
                    break;
                }

                page++;

            } while (true);
        }
        public IEnumerable<PageableResponse<PaymentModel>> GetPayments(int page = 1)
        {
            do
            {
                var request = new RestRequest($"/api/v1/tenants/{_account.TenantID}/payments");
                request.AddQueryParameter("per_page", _limit);
                if (page > 1)
                {
                    request.AddQueryParameter("page", page.ToString());
                }

                var response = Client.Get<PageableResponse<PaymentModel>>(request);
                if (!response.IsSuccessful)
                {
                    yield break;
                }
                Log.Debug("{0}/{1} page parsed. Total Count: {2}", response.Data.Current_Page, response.Data.Total_Pages, response.Data.Total_Count);
                foreach (var item in response.Data.Results)
                {
                    item.BigID = _account.BigID;
                    item.InvoiceUniqueID = item.Repair_Order_Id;
                }
                yield return response.Data;

                if (response.Data.Current_Page >= response.Data.Total_Pages)
                {
                    break;
                }

                page++;

            } while (true);
        }

        public IEnumerable<PageableResponse<RepairOrderResponse>> GetRepairOrders(int page = 1)
        {
            Log.Debug("Getting repair orders");
            do
            {

                var request = new RestRequest($"/api/v1/tenants/{_account.TenantID}/repair_orders")
                    .AddQueryParameter("shop_id", _account.ShopID)
                    .AddQueryParameter("per_page", _limit);
                if (page > 1)
                {
                    request.AddQueryParameter("page", page.ToString());
                }

                var response = Client.Get<PageableResponse<RepairOrderResponse>>(request);
                Log.Debug("{0}/{1} page parsed. Total Count: {2}", response.Data.Current_Page, response.Data.Total_Pages, response.Data.Total_Count);

                if (!response.IsSuccessful)
                {
                    yield break;
                }

                yield return response.Data;

            } while (true);
        }
        public IEnumerable<PageableResponse<VehicleModel>> GetVehicles(int page = 1)
        {
            do
            {
                var request = new RestRequest($"/api/v1/tenants/{_account.TenantID}/vehicles");
                request.AddQueryParameter("per_page", _limit);
                if (page > 1)
                {
                    request.AddQueryParameter("page", page.ToString());
                }

                var response = Client.Get<PageableResponse<VehicleModel>>(request);
                if (!response.IsSuccessful)
                {
                    yield break;
                }
                Log.Debug("{0}/{1} page parsed. Total Count: {2}", response.Data.Current_Page, response.Data.Total_Pages, response.Data.Total_Count);
                foreach (var item in response.Data.Results)
                {
                    item.BigID = _account.BigID;
                    item.CustomerId = item.Customer_Ids.FirstOrDefault();
                }
                yield return response.Data;

                if (response.Data.Current_Page >= response.Data.Total_Pages)
                {
                    break;
                }

                page++;

            } while (true);
        }
    }
}

