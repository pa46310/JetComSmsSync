using HtmlAgilityPack;
using JetComSmsSync.Core.Extensions;
using JetComSmsSync.Core.Models;
using JetComSmsSync.Core.Utils;
using JetComSmsSync.Modules.Protractor.Models;
using RestSharp;
using Serilog;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace JetComSmsSync.Modules.Protractor.ViewModels
{
    public class ProtractorSyncPageViewModel : SyncPageViewModel<AccountModel>
    {
        private readonly DatabaseClient _database;

        protected override ILogger Log { get; } = Serilog.Log.ForContext<ProtractorSyncPageViewModel>();
        private IRestClient Client { get; }
        public ProtractorSyncPageViewModel(DatabaseClient database)
        {
            _database = database;
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
            var current = 0;
            var total = accounts.Count;
            foreach (var account in accounts)
            {
                current++;
                ct.ThrowIfCancellationRequested();

                var prefix = $"[{current}/{total}]";

                Message = $"{prefix} Getting items for compare";

                var contactsForCompare = _database.GetContactForCompare(account.BigId);
                using var ctx1 = LogContext.PushProperty("LiveContact", contactsForCompare.Count);
                ct.ThrowIfCancellationRequested();

                var invoiceForCompare = _database.GetInvoiceForCompare(account.BigId);
                using var ctx2 = LogContext.PushProperty("LiveInvoice", invoiceForCompare.Count);
                ct.ThrowIfCancellationRequested();

                var serviceItemsForCompare = _database.GetServiceItemsForCompare(account.BigId);
                using var ctx3 = LogContext.PushProperty("LiveServiceItem", serviceItemsForCompare.Count);
                ct.ThrowIfCancellationRequested();

                var servicePackagesForCompare = _database.GetServicePackagesForCompare(account.BigId);
                using var ctx4 = LogContext.PushProperty("LiveServicePackage", servicePackagesForCompare.Count);
                ct.ThrowIfCancellationRequested();

                var appointmentsForCompare = _database.GetAppointmentsForCompare(account.BigId);
                using var ctx5 = LogContext.PushProperty("LiveAppointment", appointmentsForCompare.Count);
                ct.ThrowIfCancellationRequested();

                var dayLimit = 25;

                var start = startDate.Value;
                var next = start.AddDays(dayLimit);
                if (next > DateTime.Today)
                {
                    next = DateTime.Today;
                }
                do
                {
                    var prefix2 = $"[{start.ToString("MM/dd/yyyy")}-{next.ToString("MM/dd/yyyy")}]";
                    ct.ThrowIfCancellationRequested();

                    Message = $"{prefix} {prefix2} Getting local data";
                    var local = GetXmlData(account, start, next);

                    if (local is null) { break; }

                    var inserted = 0;
                    Message = $"{prefix} {prefix2} Comparing local and live data";

                    var uniqueContacts = local.Contacts.Except(contactsForCompare, Comparers.Contact).ToList();
                    contactsForCompare.AddRange(uniqueContacts);
                    using var ctx21 = LogContext.PushProperty("UniqueContact", uniqueContacts.Count);

                    var uniqueInvoices = local.Invoices.Except(invoiceForCompare, Comparers.Invoice).ToList();
                    invoiceForCompare.AddRange(uniqueInvoices);
                    using var ctx22 = LogContext.PushProperty("UniqueInvoice", uniqueInvoices.Count);

                    var uniqueServiceItems = local.ServiceItems.Except(serviceItemsForCompare, Comparers.ServiceItem).ToList();
                    serviceItemsForCompare.AddRange(uniqueServiceItems);
                    using var ctx23 = LogContext.PushProperty("UniqueServiceItem", uniqueServiceItems.Count);

                    var uniqueServicePackages = local.ServicePackages.Except(servicePackagesForCompare, Comparers.ServicePackage).ToList();
                    servicePackagesForCompare.AddRange(uniqueServicePackages);
                    using var ctx24 = LogContext.PushProperty("UniqueServicePackage", uniqueServicePackages.Count);

                    var uniqueAppointments = local.Appointments.Except(appointmentsForCompare, Comparers.Appointment).ToList();
                    appointmentsForCompare.AddRange(uniqueAppointments);
                    using var ctx25 = LogContext.PushProperty("UniqueAppointment", uniqueAppointments.Count);


                    Message = $"{prefix} {prefix2} Inserting unique data";

                    inserted = _database.InsertContacts(uniqueContacts);
                    ct.ThrowIfCancellationRequested();
                    using var ctx31 = LogContext.PushProperty("ContactInserted", inserted);

                    inserted = _database.InsertInvoices(uniqueInvoices);
                    ct.ThrowIfCancellationRequested();
                    using var ctx32 = LogContext.PushProperty("InvoiceInserted", inserted);

                    inserted = _database.InsertServiceItems(uniqueServiceItems);
                    ct.ThrowIfCancellationRequested();
                    using var ctx33 = LogContext.PushProperty("ServiceItemInserted", inserted);

                    inserted = _database.InsertServicePackages(uniqueServicePackages);
                    ct.ThrowIfCancellationRequested();
                    using var ctx34 = LogContext.PushProperty("ServicePackageInserted", inserted);

                    inserted = _database.InsertAppointments(uniqueAppointments);
                    ct.ThrowIfCancellationRequested();
                    using var ctx35 = LogContext.PushProperty("AppointmentInserted", inserted);

                    Message = $"{prefix} {prefix2} Inserted";

                    start = next;
                    next = next.AddDays(dayLimit);
                    if (next > DateTime.Today)
                    {
                        next = DateTime.Today;
                    }
                } while (start < DateTime.Today);
            }
        }


        private ProtractorData GetXmlData(AccountModel account, DateTime start, DateTime end)
        {
            var request = new RestRequest("/ExternalCRM/1.0/GetCRMData.ashx")
                .AddQueryParameter("connectionID", account.ConnectionId)
                .AddQueryParameter("apiKey", account.ApiKey)
                .AddQueryParameter("startDate", start.ToString("yyyy-MM-dd"))
                .AddQueryParameter("endDate", end.ToString("yyyy-MM-dd"));

            var response = Client.Get(request);
            var xml = response.Content;

            if (!response.IsSuccessful)
            {
                Log.Error("Status: {0}, Content: {1}", response.StatusCode, response.Content);
                return null;
            }

            // save to file
            //var key = $"protractor-{account.ConnectionId}.xml";
            //File.WriteAllText(key, response.Content);
            //var xml = File.ReadAllText(key);

            var output = new ProtractorData();
            try
            {
                var document = new HtmlDocument();
                document.LoadHtml(xml);

                var errornumber = document.DocumentNode.SelectSingleNode("/crmdataset/header/errornumber")?.InnerText;
                var errorMessage = document.DocumentNode.SelectSingleNode("/crmdataset/header/errormessage")?.InnerText;

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    Log.Error("Error: {0}, Number: {1}", errorMessage, errornumber);
                    if (string.Equals("CallThrottled", errornumber))
                    {
                        var time = Regex.Match(errorMessage, @"\d\d:\d\d:\d\d");
                        var t = TimeSpan.Parse(time.Value);
                        Thread.Sleep(t);
                        return GetXmlData(account, start, end);
                    }
                    return null;
                }

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
                            Discount = item.SelectInnerText("./servicepackages[1]/item[1]/servicepackagelines[1]/item[1]/discount"),
                            GrandTotal = item.SelectInnerText("./summary/grandtotal"),
                            ID = item.SelectInnerText("./header/id"),
                            InvoiceNumber = item.SelectInnerText("./invoicenumber"),
                            InvoiceTime = item.SelectInnerText("./invoicetime"),
                            LaborTotal = item.SelectInnerText("./summary/labortotal"),
                            LastModifiedTime = item.SelectInnerText("./header/lastmodifiedtime"),
                            LocationID = item.SelectInnerText("./locationid"),
                            NetTotal = item.SelectInnerText("./summary/nettotal"),
                            PartsTotal = item.SelectInnerText("./summary/partstotal"),
                            PromisedTime = item.SelectInnerText("./promisedtime"),
                            ScheduledTime = item.SelectInnerText("./scheduledtime"),
                            ServiceAdvisor = item.SelectInnerText("./serviceadvisor"),
                            SubletTotal = item.SelectInnerText("./summary/sublettotal"),
                            Type = item.SelectInnerText("./type"),
                            WorkOrderID = item.SelectInnerText("./id"),
                            WorkOrderNumber = item.SelectInnerText("./workordernumber"),
                            ServiceItemID = item.SelectInnerText("./servicepackages[1]/item[1]/id"),
                            Technician = item.SelectInnerText("./technician")
                        };
                        output.Invoices.Add(i);

                        var servicePackageNodes = item.SelectNodes("./servicepackages/item");
                        if (servicePackageNodes != null)
                        {
                            foreach (var sub in servicePackageNodes)
                            {
                                var i2 = new ServicePackagesModel
                                {
                                    BigID = account.BigId,
                                    Chapter = sub.SelectInnerText("./chapter"),
                                    ID = sub.SelectInnerText("./header/id"),
                                    Rank = sub.SelectInnerText("./rank"),
                                    ServicePackagesID = i.ID,
                                    Title = sub.SelectInnerText("./title"),
                                };
                                output.ServicePackages.Add(i2);
                            }
                        }
                    }
                }

                var appointmentNodes = document.DocumentNode.SelectNodes("/crmdataset/appointments/item");
                if (appointmentNodes != null)
                {
                    foreach (var item in appointmentNodes)
                    {
                        var i = new AppointmentModel
                        {
                            BigID = account.BigId,
                            ContactID = item.SelectInnerText("./contactid"),
                            CreationTime = item.SelectInnerText("./header/creationtime"),
                            DeferredServicePackages = item.SelectInnerText("./deferredservicepackages"),
                            ID = item.SelectInnerText("./id"),
                            InUsage = item.SelectInnerText("./inusage"),
                            InvoiceNumber = item.SelectInnerText("./invoicenumber"),
                            InvoiceTime = item.SelectInnerText("./invoicetime"),
                            LastModifiedTime = item.SelectInnerText("./header/lastmodifiedtime"),
                            LocationID = item.SelectInnerText("./locationid"),
                            Note = item.SelectInnerText("./note"),
                            OtherChargeCode = item.SelectInnerText("./otherchargecode"),
                            PromisedTime = item.SelectInnerText("./promisedtime"),
                            PurchaseOrderNumber = item.SelectInnerText("./purchaseordernumber"),
                            ScheduledTime = item.SelectInnerText("./scheduledtime"),
                            ServiceAdvisor = item.SelectInnerText("./serviceadvisor"),
                            ServiceItemID = item.SelectInnerText("./serviceitemid"),
                            ServicePackages = item.SelectInnerText("./servicepackages"),
                            Technician = item.SelectInnerText("./technician"),
                            Type = item.SelectInnerText("./type"),
                            WorkOrderNumber = item.SelectInnerText("./workordernumber")
                        };
                        output.Appointments.Add(i);
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
