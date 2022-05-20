using JetComSmsSync.Core.Utils;
using JetComSmsSync.Modules.Tekmetric.Models;
using JetComSmsSync.Modules.Tekmetric.Responses;

using RestSharp;
using RestSharp.Authenticators;

using System;
using System.Collections.Generic;

namespace JetComSmsSync.Modules.Tekmetric
{
    public class ServiceClient
    {
        private readonly AccountModel _account;

        public IRestClient Client { get; }
        public ServiceClient(AccountModel account)
        {
            _account = account;
            Client = RestClientUtils.CreateRestClient($"https://{account.Environment}.tekmetric.com"); //cba,shop,sandbox
            Client.Authenticator = new JwtAuthenticator(account.AccessToken);
        }

        public IEnumerable<PageResponse<ContentCustomer>> GetCustomers(DateTime? startDate)
        {
            int page = 0;
            do
            {
                var request = new RestRequest("/api/v1/customers");
                request.AddQueryParameter("shop", _account.ShopID.ToString());
                if (startDate.HasValue)
                {
                    // has previous data
                    request.AddQueryParameter("updatedDateStart", startDate?.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"));
                }
                request.AddQueryParameter("sortDirection", "ASC");
                request.AddQueryParameter("size", "500");
                if (page > 0)
                {
                    request.AddQueryParameter("page", page.ToString());
                }

                var response = Client.Get<PageResponse<ContentCustomer>>(request);
                if (response.Data.Content != null && response.Data.Content.Count > 0)
                {
                    foreach (var content in response.Data.Content)
                    {
                        content.UpdateParameters(_account.BigID);
                    }
                }

                yield return response.Data;

                if (response.Data.Last) break;

                page++;
            } while (true);
        }

        public IEnumerable<PageResponse<ContentVehicle>> GetVehicles(DateTime? startDate)
        {
            int page = 0;
            do
            {
                var request = new RestRequest("/api/v1/vehicles");
                request.AddQueryParameter("shop", _account.ShopID.ToString());
                if (startDate.HasValue)
                {
                    // has previous data
                    request.AddQueryParameter("updatedDateStart", startDate?.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"));
                }
                request.AddQueryParameter("sortDirection", "ASC");
                request.AddQueryParameter("size", "500");
                if (page > 0)
                {
                    request.AddQueryParameter("page", page.ToString());
                }

                var response = Client.Get<PageResponse<ContentVehicle>>(request);
                if (response.Data.Content != null && response.Data.Content.Count > 0)
                {
                    foreach (var content in response.Data.Content)
                    {
                        content.UpdateParameters(_account.BigID);
                    }
                }

                yield return response.Data;

                if (response.Data.Last) break;

                page++;
            } while (true);
        }

        public IEnumerable<PageResponse<ContentRepairOrder>> GetRepairOrders(DateTime? startDate)
        {
            int page = 0;
            do
            {
                var request = new RestRequest("/api/v1/repair-orders");
                request.AddQueryParameter("shop", _account.ShopID.ToString());
                if (startDate.HasValue)
                {
                    // has previous data
                    request.AddQueryParameter("postedDateStart", startDate?.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"));
                }
                request.AddQueryParameter("sortDirection", "ASC");
                request.AddQueryParameter("size", "500");
                if (page > 0)
                {
                    request.AddQueryParameter("page", page.ToString());
                }

                var response = Client.Get<PageResponse<ContentRepairOrder>>(request);
                if (response.Data.Content != null && response.Data.Content.Count > 0)
                {
                    foreach (var content in response.Data.Content)
                    {
                        content.UpdateParameters(_account.BigID);
                    }
                }

                yield return response.Data;

                if (response.Data.Last) break;

                page++;
            } while (true);
        }

        public IEnumerable<PageResponse<ContentJob>> GetJobs(DateTime? startDate)
        {
            int page = 0;
            do
            {
                var request = new RestRequest("/api/v1/jobs");
                request.AddQueryParameter("shop", _account.ShopID.ToString());
                if (startDate.HasValue)
                {
                    // has previous data
                    request.AddQueryParameter("updatedDateStart", startDate?.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"));
                }
                request.AddQueryParameter("sortDirection", "ASC");
                request.AddQueryParameter("size", "500");
                if (page > 0)
                {
                    request.AddQueryParameter("page", page.ToString());
                }

                var response = Client.Get<PageResponse<ContentJob>>(request);
                if (response.Data.Content != null && response.Data.Content.Count > 0)
                {
                    foreach (var content in response.Data.Content)
                    {
                        content.UpdateParameters(_account.BigID);
                    }
                }

                yield return response.Data;

                if (response.Data.Last) break;

                page++;
            } while (true);
        }

        public IEnumerable<PageResponse<ContentAppointment>> GetAppointments(DateTime? startDate)
        {
            int page = 0;
            do
            {
                var request = new RestRequest("/api/v1/appointments");
                request.AddQueryParameter("shop", _account.ShopID.ToString());
                if (startDate.HasValue)
                {
                    // has previous data
                    request.AddQueryParameter("updatedDateStart", startDate?.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"));
                }
                request.AddQueryParameter("sortDirection", "ASC");
                request.AddQueryParameter("size", "500");
                if (page > 0)
                {
                    request.AddQueryParameter("page", page.ToString());
                }

                var response = Client.Get<PageResponse<ContentAppointment>>(request);
                if (response.Data.Content != null && response.Data.Content.Count > 0)
                {
                    foreach (var content in response.Data.Content)
                    {
                        content.UpdateParameters(_account.BigID);
                    }
                }

                yield return response.Data;

                if (response.Data.Last) break;

                page++;
            } while (true);
        }
    }
}
