using HtmlAgilityPack;
using JetComSmsSync.Core.Extensions;
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

                //using var service = new ProtractorServiceSoapClient(ProtractorServiceSoapClient.EndpointConfiguration.ProtractorServiceSoap12);
                //var table = service.GetTableForUpdateWithBigId(account.BigId);

                var data = GetXmlData(account, DateTime.Today.AddDays(-1), DateTime.Today);
            }
        }


        private ProtractorData GetXmlData(AccountModel account, DateTime start, DateTime end)
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

            var output = new ProtractorData();
            try
            {
                var document = new HtmlDocument();
                document.LoadHtml(xml);

                var errornumber = document.DocumentNode.SelectSingleNode("/crmdataset/header/errornumber")?.InnerText;
                var errorMessage = document.DocumentNode.SelectSingleNode("/crmdataset/header/errormessage")?.InnerText;

                // check for error message
                var contactNodes = document.DocumentNode.SelectNodes("/crmdataset/contacts/item");
                if (contactNodes != null)
                {
                    foreach (var item in contactNodes)
                    {
                        var i = new ContactModel
                        {
                            AddressTitle = item.SelectInnerText("./address/title"),
                            BigID = account.BigId,
                            City = item.SelectInnerText("./address/city"),
                            Company = item.SelectInnerText("./company"),
                            Country = item.SelectInnerText("./address/country"),
                            CreationTime = item.SelectInnerText("./header/creationtime"),
                            Email = item.SelectInnerText("./email"),
                            FileAs = item.SelectInnerText("./fileas"),
                            FirstName = item.SelectInnerText("./name/firstname"),
                            ID = item.SelectInnerText("./header/id"),
                            LastModifiedTime = item.SelectInnerText("./header/lastmodifiedtime"),
                            LastName = item.SelectInnerText("./name/lastname"),
                            MiddleName = item.SelectInnerText("./name/middlename"),
                            Phone1 = item.SelectInnerText("./phone1"),
                            Phone2 = item.SelectInnerText("./phone2"),
                            PostalCode = item.SelectInnerText("./address/postalcode"),
                            Prefix = item.SelectInnerText("./name/prefix"),
                            Province = item.SelectInnerText("./address/province"),
                            Street = item.SelectInnerText("./address/street"),
                            Suffix = item.SelectInnerText("./name/suffix"),
                            Title = item.SelectInnerText("./name/title"),
                        };
                        output.Contacts.Add(i);
                    }
                }

                var serviceItemNodes = document.DocumentNode.SelectNodes("/crmdataset/serviceitems/item");
                if (serviceItemNodes != null)
                {
                    foreach (var item in serviceItemNodes)
                    {
                        var i = new ServiceItemModel
                        {
                            BigID = account.BigId,
                            CreationTime = item.SelectInnerText("./header/creationtime"),
                            Description = item.SelectInnerText("./description"),
                            Engine = item.SelectInnerText("./engine"),
                            ID = item.SelectInnerText("./header/id"),
                            LastModifiedTime = item.SelectInnerText("./header/lastmodifiedtime"),
                            Lookup = item.SelectInnerText("./lookup"),
                            Make = item.SelectInnerText("./make"),
                            Model = item.SelectInnerText("./model"),
                            OwnerID = item.SelectInnerText("./ownerid"),
                            PlateRegistration = item.SelectInnerText("./plateregistration"),
                            ProductionDate = item.SelectInnerText("./productiondate"),
                            SubModel = item.SelectInnerText("./submodel"),
                            Type = item.SelectInnerText("./type"),
                            Usage = item.SelectInnerText("./usage"),
                            VIN = item.SelectInnerText("./vin"),
                            Year = item.SelectInnerText("./year"),
                        };
                        output.ServiceItems.Add(i);
                    }
                }

                var invoiceNodes = document.DocumentNode.SelectNodes("/crmdataset/invoices/item");
                if (invoiceNodes != null)
                {
                    foreach (var item in invoiceNodes)
                    {
                        var i = new InvoiceModel
                        {
                            BigID = account.BigId,
                            ContactID = item.SelectInnerText("./contactid"),
                            CreationTime = item.SelectInnerText("./header/creationtime"),
                            Discount = item.SelectInnerText("./discount")
                        };
                        output.Invoices.Add(i);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to get xml data");
            }

            return output;
        }
    }
}
