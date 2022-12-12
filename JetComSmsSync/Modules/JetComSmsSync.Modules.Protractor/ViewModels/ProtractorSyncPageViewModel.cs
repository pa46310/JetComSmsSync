using HtmlAgilityPack;

using JetComSmsSync.Core.Extensions;
using JetComSmsSync.Core.Models;
using JetComSmsSync.Core.Utils;
using JetComSmsSync.Modules.Protractor.Models;

using Microsoft.Extensions.Configuration;

using RestSharp;

using Serilog;
using Serilog.Context;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace JetComSmsSync.Modules.Protractor.ViewModels
{
    public class ProtractorSyncPageViewModel : SyncPageViewModel<AccountModel>
    {
        private readonly DatabaseClient _database;
        private readonly IConfiguration _configuration;

        protected override ILogger Log { get; } = Serilog.Log.ForContext<ProtractorSyncPageViewModel>();
        private RestClient Client { get; }

        private int _dayChunkSize;
        public int DayChunkSize
        {
            get { return _dayChunkSize; }
            set { SetProperty(ref _dayChunkSize, value); }
        }

        public ProtractorSyncPageViewModel(DatabaseClient database, IConfiguration configuration)
        {
            _database = database;
            _configuration = configuration;
            RefreshCommand.Execute();
            Client = RestClientUtils.CreateRestClient(configuration.GetSection("Protractor")["URL"]);
            var dayLimitStr = configuration.GetSection("Protractor")["DayLimit"];
            if (!int.TryParse(dayLimitStr, out _dayChunkSize))
            {
                _dayChunkSize = 25;
            }
        }

        protected override async Task<AccountModel[]> GetAccounts()
        {
            var outputList = await Task.Run(_database.GetAccounts);
            return outputList.ToArray();
        }

        protected override void Send(DateTime? startDate, IList<AccountModel> accounts, CancellationToken ct)
        {
            var start = startDate.Value;
            var maxRetry = 3;
            var next = start.AddDays(DayChunkSize);
            if (next > DateTime.Today)
            {
                next = DateTime.Today;
            }
            do
            {
                var retry = 0;
                var retryAccounts = new List<AccountModel>();
                var tryAccounts = new List<AccountModel>(accounts);
                do
                {
                    var current = 0;
                    foreach (var account in tryAccounts)
                    {
                        var total = tryAccounts.Count;
                        using var ctx0 = LogContext.PushProperty("BigID", account.BigId);
                        try
                        {
                            current++;
                            ct.ThrowIfCancellationRequested();

                            SendData(account, start, next, current, total, ct);
                        }
                        catch (OperationCanceledException)
                        {
                            throw;
                        }
                        catch (Exception ex)
                        {
                            if (retry < maxRetry)
                            {
                                Log.Warning(ex, "Failed to send data for account:{0}. Retrying({1}) again...", account.BigId, retry);
                                retryAccounts.Add(account);
                            }
                            else
                            {
                                Log.Error(ex, "Failed to send data for account:{0}", account.BigId);
                            }
                        }
                    }
                    retry++;
                    tryAccounts.Clear();
                    if (retry < maxRetry && retryAccounts.Count > 0)
                    {
                        tryAccounts.AddRange(retryAccounts);
                        retryAccounts.Clear();
                    }
                } while (tryAccounts.Count > 0);

                start = next;
                next = next.AddDays(DayChunkSize);
                if (next > DateTime.Today)
                {
                    next = DateTime.Today;
                }
            } while (start < DateTime.Today);
        }

        private void SendData(AccountModel account, DateTime start, DateTime next, int current, int total, CancellationToken ct)
        {
            if (start.Date == next.Date)
            {
                start = next.Date.AddDays(-1);
            }


            var prefix = $"[{start:MM/dd/yyyy}-{next:MM/dd/yyyy}] [{current}/{total}]";
            ct.ThrowIfCancellationRequested();

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

            Message = $"{prefix} Getting local data";
            var local = GetXmlData(account, start, next, ct);

            if (local is null) { return; }

            var inserted = 0;
            Message = $"{prefix} Comparing local and live data";

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


            Message = $"{prefix} Inserting unique data";

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

            Message = $"{prefix} Inserted";
        }

        private ProtractorData GetXmlData(AccountModel account, DateTime start, DateTime end, CancellationToken ct)
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

            var output = new ProtractorData();
            var document = new XmlDocument();
            var indexOf = xml.IndexOf("<crmdataset", StringComparison.OrdinalIgnoreCase);
            var xml1 = xml.Remove(0, indexOf);
            document.LoadXml(xml1);

            //var document = new HtmlDocument();
            //document.LoadHtml(xml);

            var errornumber = document.SelectSingleNode("/CRMDataSet/Header/ErrorNumber")?.InnerText;
            var errorMessage = document.SelectSingleNode("/CRMDataSet/Header/ErrorMessage")?.InnerText;

            if (!string.IsNullOrEmpty(errorMessage))
            {
                if (string.Equals(errornumber, "InvalidDateRange") || string.Equals(errornumber, "InvalidConnectionID") || string.Equals(errornumber, "InvalidAPIKey"))
                {
                    Log.Warning("Error: {0}, Number: {1}, Skipping...", errorMessage, errornumber);
                    return null;
                }
                else
                {
                    Log.Error("Error: {0}, Number: {1}", errorMessage, errornumber);
                    throw new Exception(response.Content);
                }
            }

            try
            {
                // check for error message
                var contactNodes = document.SelectNodes("/CRMDataSet/Contacts/Item");
                if (contactNodes != null)
                {
                    foreach (XmlNode item in contactNodes)
                    {
                        var i = new ContactModel
                        {
                            AddressTitle = item.SelectInnerText("./Address/Title"),
                            BigID = account.BigId,
                            City = item.SelectInnerText("./Address/City"),
                            Company = item.SelectInnerText("./Company"),
                            Country = item.SelectInnerText("./Address/Country"),
                            CreationTime = item.SelectInnerText("./Header/CreationTime"),
                            Email = item.SelectInnerText("./Email"),
                            FileAs = item.SelectInnerText("./FileAs"),
                            FirstName = item.SelectInnerText("./Name/FirstName"),
                            ID = item.SelectInnerText("./Header/ID"),
                            LastModifiedTime = item.SelectInnerText("./Header/LastModifiedTime"),
                            LastName = item.SelectInnerText("./Name/LastName"),
                            MiddleName = item.SelectInnerText("./Name/MiddleName"),
                            Phone1 = item.SelectInnerText("./Phone1"),
                            Phone2 = item.SelectInnerText("./Phone2"),
                            PostalCode = item.SelectInnerText("./Address/PostalCode"),
                            Prefix = item.SelectInnerText("./Name/Prefix"),
                            Province = item.SelectInnerText("./Address/Province"),
                            Street = item.SelectInnerText("./Address/Street"),
                            Suffix = item.SelectInnerText("./Name/Suffix"),
                            Title = item.SelectInnerText("./Name/Title"),
                        };
                        output.Contacts.Add(i);
                    }
                }

                var serviceItemNodes = document.SelectNodes("/CRMDataSet/ServiceItems/Item");
                if (serviceItemNodes != null)
                {
                    foreach (XmlNode item in serviceItemNodes)
                    {
                        var i = new ServiceItemModel
                        {
                            BigID = account.BigId,
                            CreationTime = item.SelectInnerText("./Header/CreationTime"),
                            Description = item.SelectInnerText("./Description"),
                            Engine = item.SelectInnerText("./Engine"),
                            ID = item.SelectInnerText("./Header/ID"),
                            LastModifiedTime = item.SelectInnerText("./Header/LastModifiedTime"),
                            Lookup = item.SelectInnerText("./Lookup"),
                            Make = item.SelectInnerText("./Make"),
                            Model = item.SelectInnerText("./Model"),
                            OwnerID = item.SelectInnerText("./OwnerID"),
                            PlateRegistration = item.SelectInnerText("./PlateRegistration"),
                            ProductionDate = item.SelectInnerText("./ProductionDate"),
                            SubModel = item.SelectInnerText("./Submodel"),
                            Type = item.SelectInnerText("./Type"),
                            Usage = item.SelectInnerText("./Usage"),
                            VIN = item.SelectInnerText("./VIN"),
                            Year = item.SelectInnerText("./Year"),
                        };
                        output.ServiceItems.Add(i);
                    }
                }

                var invoiceNodes = document.SelectNodes("/CRMDataSet/Invoices/Item");
                if (invoiceNodes != null)
                {
                    foreach (XmlNode item in invoiceNodes)
                    {
                        //var a = item.ChildNodes[16].ChildNodes[1].ChildNodes[7].ChildNodes[0].ChildNodes[10].Name;
                        //var b = item.SelectSingleNode("./ServicePackages/Item[2]/ID");
                        var i = new InvoiceModel
                        {
                            BigID = account.BigId,
                            ContactID = item.SelectInnerText("./ContactID"),
                            CreationTime = item.SelectInnerText("./Header/CreationTime"),
                            Discount = item.SelectInnerText("./ServicePackages/Item[2]/ServicePackageLines/Item[1]/Discount"),
                            GrandTotal = item.SelectInnerText("./Summary/GrandTotal"),
                            ID = item.SelectInnerText("./Header/ID"),
                            InvoiceNumber = item.SelectInnerText("./InvoiceNumber"),
                            InvoiceTime = item.SelectInnerText("./InvoiceTime"),
                            LaborTotal = item.SelectInnerText("./Summary/LaborTotal"),
                            LastModifiedTime = item.SelectInnerText("./Header/LastModifiedTime"),
                            LocationID = item.SelectInnerText("./LocationID"),
                            NetTotal = item.SelectInnerText("./Summary/NetTotal"),
                            PartsTotal = item.SelectInnerText("./Summary/PartsTotal"),
                            PromisedTime = item.SelectInnerText("./PromisedTime"),
                            ScheduledTime = item.SelectInnerText("./ScheduledTime"),
                            ServiceAdvisor = item.SelectInnerText("./ServiceAdvisor"),
                            SubletTotal = item.SelectInnerText("./Summary/SubletTotal"),
                            Type = item.SelectInnerText("./Type"),
                            WorkOrderID = item.SelectInnerText("./ID"),
                            WorkOrderNumber = item.SelectInnerText("./WorkOrderNumber"),
                            ServiceItemID = item.SelectInnerText("./ServiceItemID"),
                            Technician = item.SelectInnerText("./Technician"),
                        };
                        output.Invoices.Add(i);

                        var servicePackageNodes = item.SelectNodes("./ServicePackages/Item");
                        if (servicePackageNodes != null)
                        {
                            foreach (XmlNode sub in servicePackageNodes)
                            {
                                var i2 = new ServicePackagesModel
                                {
                                    BigID = account.BigId,
                                    Chapter = sub.SelectInnerText("./Chapter"),
                                    ID = sub.SelectInnerText("./Header/ID"),
                                    Rank = sub.SelectInnerText("./Rank"),
                                    ServicePackagesID = i.ID,
                                    Title = sub.SelectInnerText("./Title"),
                                };
                                output.ServicePackages.Add(i2);
                            }
                        }
                    }
                }

                var appointmentNodes = document.SelectNodes("/CRMDataSet/Appointments/Item");
                if (appointmentNodes != null)
                {
                    foreach (XmlNode item in appointmentNodes)
                    {
                        var i = new AppointmentModel
                        {
                            BigID = account.BigId,
                            ContactID = item.SelectInnerText("./ContactID"),
                            CreationTime = item.SelectInnerText("./Header/CreationTime"),
                            DeferredServicePackages = item.SelectInnerText("./DeferredServicePackages"),
                            ID = item.SelectInnerText("./ID"),
                            InUsage = item.SelectInnerText("./InUsage"),
                            InvoiceNumber = item.SelectInnerText("./InvoiceNumber"),
                            InvoiceTime = item.SelectInnerText("./InvoiceTime"),
                            LastModifiedTime = item.SelectInnerText("./Header/LastModifiedTime"),
                            LocationID = item.SelectInnerText("./LocationID"),
                            Note = item.SelectInnerText("./Note"),
                            OtherChargeCode = item.SelectInnerText("./OtherChargeCode"),
                            PromisedTime = item.SelectInnerText("./PromisedTime"),
                            PurchaseOrderNumber = item.SelectInnerText("./PurchaseOrderNumber"),
                            ScheduledTime = item.SelectInnerText("./ScheduledTime"),
                            ServiceAdvisor = item.SelectInnerText("./ServiceAdvisor"),
                            ServiceItemID = item.SelectInnerText("./ServiceItemID"),
                            ServicePackages = item.SelectInnerText("./ServicePackages"),
                            Technician = item.SelectInnerText("./Technician"),
                            Type = item.SelectInnerText("./Type"),
                            WorkOrderNumber = item.SelectInnerText("./WorkOrderNumber")
                        };
                        output.Appointments.Add(i);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Warning(ex, "Failed to get xml data. Skipping...");
                return null;
            }

            return output;
        }
    }
}
