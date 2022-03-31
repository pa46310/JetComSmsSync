using JetComSmsSync.Core.Models;
using JetComSmsSync.Core.Utils;
using JetComSmsSync.Modules.Protractor.Models;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace JetComSmsSync.Modules.Protractor.ViewModels
{
    public class ProtractorSyncPageViewModel : SyncPageViewModel<AccountModel>
    {
        protected override ILogger Log { get; } = Serilog.Log.ForContext<ProtractorSyncPageViewModel>();
        private IRestClient Client { get; }
        public ProtractorSyncPageViewModel()
        {
            RefreshCommand.Execute();
            Client = RestClientUtils.CreateRestClient("https://integration.protractor.com");
        }

        protected override async Task<AccountModel[]> GetAccounts()
        {
            using var service = new ProtractorServiceSoapClient(ProtractorServiceSoapClient.EndpointConfiguration.ProtractorServiceSoap12);

            var output = await service.GetAccountIDDetailAsync();

            var details = output.Any1.GetElementsByTagName("AccountIDDetail");
            var outputList = new List<AccountModel>(details.Count);
            foreach (XmlNode detail in details)
            {
                var bigId = detail.ChildNodes.Item(0).InnerText;
                var connectionId = detail.ChildNodes.Item(1).InnerText;
                var apiKey = detail.ChildNodes.Item(2).InnerText;

                outputList.Add(new AccountModel
                {
                    ApiKey = apiKey,
                    BigId = bigId,
                    ConnectionId = connectionId,
                    IsSelected = false,
                });
            }

            return outputList.ToArray();
        }

        protected override void Send(DateTime? startDate, IList<AccountModel> accounts, CancellationToken ct)
        {
            foreach (var account in accounts)
            {
                var data = GetXmlData(account, DateTime.Today.AddDays(-1), DateTime.Today);
            }
        }


        private CRMDataSet GetXmlData(AccountModel account, DateTime start, DateTime end)
        {
            var request = new RestRequest("/ExternalCRM/1.0/GetCRMData.ashx")
                .AddQueryParameter("connectionID", account.ConnectionId)
                .AddQueryParameter("apiKey", account.ApiKey)
                .AddQueryParameter("startDate", start.ToString("yyyy-MM-dd"))
                .AddQueryParameter("endDate", end.ToString("yyyy-MM-dd"));

            //var response = Client.Get(request);

            // save to file
            var key = $"protractor-{account.ConnectionId}.xml";
            //File.WriteAllText(key, response.Content);
            var xml = File.ReadAllText(key);

            var serializer = new XmlSerializer(typeof(CRMDataSet));
            using (StringReader reader = new StringReader(xml))
            {
                var test = (CRMDataSet)serializer.Deserialize(reader);
                return test;
            }
        }
    }
}
