using HtmlAgilityPack;

using JetComSmsSync.Core.Utils;
using JetComSmsSync.Modules.CDK.Extensions;
using JetComSmsSync.Modules.CDK.Models;

using RestSharp;
using RestSharp.Authenticators;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace JetComSmsSync.Modules.CDK
{
    public class ServiceClient
    {
        public IRestClient Client { get; }

        private readonly string _dealerId;

        public ServiceClient(AccountModel account)
        {
            var client = RestClientUtils.CreateRestClient(account.BaseUrl);
            client.Authenticator = new HttpBasicAuthenticator(account.Username, account.Password);
            Client = client;
            _dealerId = account.DealerId;
        }

        public string SelectSingleNodeText(HtmlNode node, string name)
        {
            var xPath = $"./{name} | ./{name.ToLower()}";
            var result = node.SelectSingleNode(xPath);

            if (result is null) return "";

            return result.InnerText;
        }

        #region Customers
        public CustomerModel[] GetCustomerBulk()
        {
            var request = new RestRequest("/pip-extract/customer/extract");
            request.AddQueryParameter("queryId", "CUST_Bulk");
            request.AddQueryParameter("dealerId", _dealerId);
            request.AddQueryParameter("timeoutSeconds", "300");

            var response = Client.Post(request);
            return ParseCustomers(response);
        }
        public CustomerModel[] GetCustomerBulkUsingFile()
        {
            var response = new RestResponse
            {
                Content = File.ReadAllText("customers-bulk.xml")
            };
            return ParseCustomers(response);
        }
        private CustomerModel[] ParseCustomers(IRestResponse response)
        {
            var document = new HtmlDocument();
            document.LoadHtml(response.Content);

            var customers = document.DocumentNode.SelectNodes("//customer|//Customer");
            if (customers is null || customers.Count == 0) return new CustomerModel[0];

            var output = new List<CustomerModel>(customers.Count);
            foreach (var node in customers)
            {
                var customer = new CustomerModel
                {
                    Address = SelectSingleNodeText(node, "Address"),
                    AddressSecondLine = SelectSingleNodeText(node, "AddressSecondLine"),
                    BusinessPhone = SelectSingleNodeText(node, "BusinessPhone"),
                    BirthDate = SelectSingleNodeText(node, "BirthDate"),
                    BlockEMail = SelectSingleNodeText(node, "BlockEMail"),
                    BlockMail = SelectSingleNodeText(node, "BlockMail"),
                    BlockPhone = SelectSingleNodeText(node, "BlockPhone"),
                    BusinessPhoneExt = SelectSingleNodeText(node, "BusinessPhoneExt"),
                    Cellular = SelectSingleNodeText(node, "Cellular"),
                    City = SelectSingleNodeText(node, "City"),
                    Comment = SelectSingleNodeText(node, "Comment"),
                    CommentDate = SelectSingleNodeText(node, "CommentDate"),
                    ContactMethod = SelectSingleNodeText(node, "ContactMethod"),
                    Country = SelectSingleNodeText(node, "Country"),
                    County = SelectSingleNodeText(node, "County"),
                    CreditLimit = SelectSingleNodeText(node, "CreditLimit"),
                    CurrentDue = SelectSingleNodeText(node, "CurrentDue"),
                    CustNo = SelectSingleNodeText(node, "CustNo"),
                    DateAdded = SelectSingleNodeText(node, "DateAdded"),
                    DeleteDataDate = SelectSingleNodeText(node, "DeleteDataDate"),
                    DeleteDataTime = SelectSingleNodeText(node, "DeleteDataTime"),
                    DriverLicenseExpDate = SelectSingleNodeText(node, "DriverLicenseExpDate"),
                    DriverLicenseStOrProv = SelectSingleNodeText(node, "DriverLicenseStOrProv"),
                    Email = SelectSingleNodeText(node, "Email"),
                    Email2 = SelectSingleNodeText(node, "Email2"),
                    Email3 = SelectSingleNodeText(node, "Email3"),
                    EmailDesc = SelectSingleNodeText(node, "EmailDesc"),
                    EmailDesc2 = SelectSingleNodeText(node, "EmailDesc2"),
                    EmailDesc3 = SelectSingleNodeText(node, "EmailDesc3"),
                    Employer = SelectSingleNodeText(node, "Employer"),
                    ErrorLevel = SelectSingleNodeText(node, "ErrorLevel"),
                    ErrorMessage = SelectSingleNodeText(node, "ErrorMessage"),
                    FirstName = SelectSingleNodeText(node, "FirstName"),
                    Gender = SelectSingleNodeText(node, "Gender"),
                    HomeFax = SelectSingleNodeText(node, "HomeFax"),
                    HomePhone = SelectSingleNodeText(node, "HomePhone"),
                    HostItemID = SelectSingleNodeText(node, "HostItemID"),
                    InsAgency = SelectSingleNodeText(node, "InsAgency"),
                    InsAgent = SelectSingleNodeText(node, "InsAgent"),
                    InsAgentAddress1 = SelectSingleNodeText(node, "InsAgentAddress1"),
                    InsAgentAddress2 = SelectSingleNodeText(node, "InsAgentAddress2"),
                    InsAgentCity = SelectSingleNodeText(node, "InsAgentCity"),
                    InsAgentPhone = SelectSingleNodeText(node, "InsAgentPhone"),
                    InsAgentState = SelectSingleNodeText(node, "InsAgentState"),
                    InsAgentZipOrPostalCode = SelectSingleNodeText(node, "InsAgentZipOrPostalCode"),
                    InsCompany = SelectSingleNodeText(node, "InsCompany"),
                    InsCompanyAddress1 = SelectSingleNodeText(node, "InsCompanyAddress1"),
                    InsCompanyAddress2 = SelectSingleNodeText(node, "InsCompanyAddress2"),
                    InsCompanyCity = SelectSingleNodeText(node, "InsCompanyCity"),
                    InsCompanyPhone = SelectSingleNodeText(node, "InsCompanyPhone"),
                    InsCompanyState = SelectSingleNodeText(node, "InsCompanyState"),
                    InsCompanyZipOrPostalCode = SelectSingleNodeText(node, "InsCompanyZipOrPostalCode"),
                    InsPolicyExpDate = SelectSingleNodeText(node, "InsPolicyExpDate"),
                    InsPolicyNo = SelectSingleNodeText(node, "InsPolicyNo"),
                    InsVerifiedBy = SelectSingleNodeText(node, "InsVerifiedBy"),
                    InsVerifiedDate = SelectSingleNodeText(node, "InsVerifiedDate"),
                    Language = SelectSingleNodeText(node, "Language"),
                    LastName = SelectSingleNodeText(node, "LastName"),
                    LastUpdated = SelectSingleNodeText(node, "LastUpdated"),
                    Mailability = SelectSingleNodeText(node, "Mailability"),
                    MiddleName = SelectSingleNodeText(node, "MiddleName"),
                    Name1 = SelectSingleNodeText(node, "Name1"),
                    Name2Company = SelectSingleNodeText(node, "Name2Company"),
                    Name2First = SelectSingleNodeText(node, "Name2First"),
                    Name2Last = SelectSingleNodeText(node, "Name2Last"),
                    Name2Middle = SelectSingleNodeText(node, "Name2Middle"),
                    Name2Suffix = SelectSingleNodeText(node, "Name2Suffix"),
                    Name2Title = SelectSingleNodeText(node, "Name2Title"),
                    NameBalances1 = SelectSingleNodeText(node, "NameBalances1"),
                    NameCode = SelectSingleNodeText(node, "NameCode"),
                    NameCompany = SelectSingleNodeText(node, "NameCompany"),
                    NameSuffix = SelectSingleNodeText(node, "NameSuffix"),
                    NameTitle = SelectSingleNodeText(node, "NameTitle"),
                    OptOutDate = SelectSingleNodeText(node, "OptOutDate"),
                    OptOutTime = SelectSingleNodeText(node, "OptOutTime"),
                    Over120Due = SelectSingleNodeText(node, "Over120Due"),
                    Over30Due = SelectSingleNodeText(node, "Over30Due"),
                    Over60Due = SelectSingleNodeText(node, "Over60Due"),
                    Over90Due = SelectSingleNodeText(node, "Over90Due"),
                    Pager = SelectSingleNodeText(node, "Pager"),
                    PartsCounterCode = SelectSingleNodeText(node, "PartsCounterCode"),
                    PartsFlag = SelectSingleNodeText(node, "PartsFlag"),
                    PartsType = SelectSingleNodeText(node, "PartsType"),
                    PreferredContactDay = SelectSingleNodeText(node, "PreferredContactDay"),
                    PreferredContactMethod = SelectSingleNodeText(node, "PreferredContactMethod"),
                    PreferredLanguage = SelectSingleNodeText(node, "PreferredLanguage"),
                    PreferredContactTime = SelectSingleNodeText(node, "PreferredContactTime"),
                    SaleType = SelectSingleNodeText(node, "SaleType"),
                    SecondaryHomePhone = SelectSingleNodeText(node, "SecondaryHomePhone"),
                    ServiceCustomer = SelectSingleNodeText(node, "ServiceCustomer"),
                    SpecInstructions = SelectSingleNodeText(node, "SpecInstructions"),
                    SpecInstructions2 = SelectSingleNodeText(node, "SpecInstructions2"),
                    SpecInstructions3 = SelectSingleNodeText(node, "SpecInstructions3"),
                    SpecInstructions4 = SelectSingleNodeText(node, "SpecInstructions4"),
                    SpecInstructions5 = SelectSingleNodeText(node, "SpecInstructions5"),
                    State = SelectSingleNodeText(node, "State"),
                    TaxCode = SelectSingleNodeText(node, "TaxCode"),
                    Telephone = SelectSingleNodeText(node, "Telephone"),
                    TextMessageCarrier = SelectSingleNodeText(node, "TextMessageCarrier"),
                    TextMessagePhone = SelectSingleNodeText(node, "TextMessagePhone"),
                    Title1 = SelectSingleNodeText(node, "Title1"),
                    ZipOrPostalCode = SelectSingleNodeText(node, "ZipOrPostalCode"),
                };
                output.Add(customer);
            }
            return output.ToArray();
        }
        public CustomerModel[] GetCustomerDelta(DateTime date)
        {
            var request = new RestRequest("/pip-extract/customer/extract");
            request.AddQueryParameter("queryId", "CUST_Delta");
            request.AddQueryParameter("dealerId", _dealerId);
            request.AddQueryParameter("deltaDate", date.ToString("MM/dd/yyyy"));

            var response = Client.Post(request);
            return ParseCustomers(response);
        }
        #endregion

        #region Help Employees
        public HelpEmployeeModel[] GetHelpEmployeeBulk()
        {
            var request = new RestRequest("/pip-extract/help-employee/extract");
            request.AddQueryParameter("queryId", "HEMPL_Bulk_Service");
            request.AddQueryParameter("dealerId", _dealerId);

            var response = Client.Post(request);
            return ParseHelpEmployee(response);
        }

        public HelpEmployeeModel[] GetHelpEmployeeDelta(DateTime date)
        {
            var request = new RestRequest("/pip-extract/help-employee/extract");
            request.AddQueryParameter("queryId", "HEMPL_Delta_Service");
            request.AddQueryParameter("dealerId", _dealerId);
            request.AddQueryParameter("deltaDate", date.ToString("MM/dd/yyyy"));

            var response = Client.Post(request);
            return ParseHelpEmployee(response);
        }

        private HelpEmployeeModel[] ParseHelpEmployee(IRestResponse response)
        {
            var document = new HtmlDocument();
            document.LoadHtml(response.Content);
            var nodes = document.DocumentNode.SelectNodes("//helpemployee|//HelpEmployee");

            if (nodes is null || nodes.Count < 2) return new HelpEmployeeModel[0];

            var output = new List<HelpEmployeeModel>(nodes.Count - 1);
            foreach (var node in nodes.Skip(1))
            {
                var id = SelectSingleNodeText(node, "Id");
                var name = SelectSingleNodeText(node, "Name");
                var item = new HelpEmployeeModel
                {
                    Id = id,
                    Name = name,
                };
                output.Add(item);
            }
            return output.ToArray();
        }
        #endregion

        #region Repair Orders

        public void LoadRepairOrderUsingFile()
        {
            var data = File.ReadAllText("xmls\\service-repair-order-history.xml");
            var document = new HtmlDocument();
            document.LoadHtml(data);
            var nodes = document.DocumentNode.SelectNodes("//service-repair-order-history");
            if (nodes is null || nodes.Count == 0) return;

            try
            {
                var serializer = new XmlSerializer(typeof(ServiceRODetailHistory));
                using (var reader = File.OpenRead("xmls\\service-repair-order-history.xml"))
                {
                    var history = (ServiceRODetailHistory)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to load repair orders");
            }
        }

        public SRODModel[] GetServiceRepairOrderHistory(DateTime start, DateTime end)
        {
            var request = new RestRequest("/pip-extract/service-ro-history/extract");
            request.AddQueryParameter("queryId", "SROD_History_DateRange");
            request.AddQueryParameter("dealerId", _dealerId);
            request.AddQueryParameter("ReportType", "CLOSED");
            request.AddQueryParameter("qparamStartDate", start.ToString("MM/dd/yyyy"));
            request.AddQueryParameter("qparamEndDate", end.ToString("MM/dd/yyyy"));
            request.AddQueryParameter("timeoutSeconds", "600");

            var response = Client.Post(request);
            var serializer = new XmlSerializer(typeof(ServiceRODetailHistory));
            using (var reader = new StringReader(response.Content))
            {
                var data = (ServiceRODetailHistory)serializer.Deserialize(reader);
                var map = data.Servicerepairorderhistory.Select(x => x.ToSROD()).ToArray();
                return map;
            }
        }

        public SRODModel[] GetServiceRepairOrderClosed(DateTime start, DateTime end)
        {
            return null;
        } 
        #endregion

    }
}
