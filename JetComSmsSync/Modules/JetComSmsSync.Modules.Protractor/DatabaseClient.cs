using Dapper;

using JetComSmsSync.Modules.Protractor.Models;

using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

using Serilog;

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace JetComSmsSync.Modules.Protractor
{
    public class DatabaseClient
    {
        private readonly string _autoRepairConnectionString;

        public DatabaseClient(IConfiguration configuration)
        {
            _autoRepairConnectionString = configuration.GetConnectionString("AutoRepairSMSTransferInfo");
        }

        private SqlConnection GetConnection() => new SqlConnection(_autoRepairConnectionString);

        public List<ContactModel> GetContactForCompare(string bigId)
        {
            using var connection = GetConnection();
            var output = connection.Query<ContactModel>("SELECT Id FROM Protractor_Contacts WHERE BigId=@BigId", new { BigId = bigId });
            return output.ToList();
        }

        public int InsertContacts(IEnumerable<ContactModel> items)
        {
            using var connection = GetConnection();
            var output = 0;
            foreach (var item in items)
            {
                try
                {

                    output += connection.Execute(@"INSERT INTO [dbo].[Protractor_Contacts]
           ([ID]
           ,[CreationTime]
           ,[LastModifiedTime]
           ,[FileAs]
           ,[Title]
           ,[Prefix]
           ,[FirstName]
           ,[MiddleName]
           ,[LastName]
           ,[Suffix]
           ,[AddressTitle]
           ,[Street]
           ,[City]
           ,[Province]
           ,[PostalCode]
           ,[Country]
           ,[Company]
           ,[Phone1]
           ,[Phone2]
           ,[Email]
           ,[BigID]
           ,[IsDone])
     VALUES
           (@ID
           ,@CreationTime
           ,@LastModifiedTime
           ,@FileAs
           ,@Title
           ,@Prefix
           ,@FirstName
           ,@MiddleName
           ,@LastName
           ,@Suffix
           ,@AddressTitle
           ,@Street
           ,@City
           ,@Province
           ,@PostalCode
           ,@Country
           ,@Company
           ,@Phone1
           ,@Phone2
           ,@Email
           ,@BigID
           ,0)", item);
                }
                catch (System.Exception ex)
                {
                    Log.Error(ex, "Failed to insert contact. {0}", JsonConvert.SerializeObject(item));
                }
            }
            return output;
        }

        public List<InvoiceModel> GetInvoiceForCompare(string bigId)
        {
            using var connection = GetConnection();
            var output = connection.Query<InvoiceModel>("SELECT Id,ServiceItemID FROM Protractor_Invoices WHERE BigId=@BigId", new { BigId = bigId });
            return output.ToList();
        }

        public int InsertInvoices(IEnumerable<InvoiceModel> items)
        {
            using var connection = GetConnection();
            var output = connection.Execute(@"INSERT INTO [dbo].[Protractor_Invoices]
           ([ID]
           ,[CreationTime]
           ,[LastModifiedTime]
           ,[Type]
           ,[ScheduledTime]
           ,[PromisedTime]
           ,[InvoiceTime]
           ,[WorkOrderNumber]
           ,[InvoiceNumber]
           ,[ContactID]
           ,[ServiceItemID]
           ,[Technician]
           ,[ServiceAdvisor]
           ,[PartsTotal]
           ,[LaborTotal]
           ,[SubletTotal]
           ,[NetTotal]
           ,[WorkOrderID]
           ,[GrandTotal]
           ,[LocationID]
           ,[BigID]
           ,[Discount]
           ,[IsDone])
     VALUES
           (@ID
           ,@CreationTime
           ,@LastModifiedTime
           ,@Type
           ,@ScheduledTime
           ,@PromisedTime
           ,@InvoiceTime
           ,@WorkOrderNumber
           ,@InvoiceNumber
           ,@ContactID
           ,@ServiceItemID
           ,@Technician
           ,@ServiceAdvisor
           ,@PartsTotal
           ,@LaborTotal
           ,@SubletTotal
           ,@NetTotal
           ,@WorkOrderID
           ,@GrandTotal
           ,@LocationID
           ,@BigID
           ,@Discount
           ,0)", items);
            return output;
        }

        public List<ServicePackagesModel> GetServicePackagesForCompare(string bigId)
        {
            using var connection = GetConnection();
            var output = connection.Query<ServicePackagesModel>("SELECT Id,ServicePackagesID FROM Protractor_Invoices_ServicePackages WHERE BigId=@BigId", new { BigId = bigId });
            return output.ToList();
        }

        public int InsertServicePackages(IEnumerable<ServicePackagesModel> items)
        {
            using var connection = GetConnection();
            var output = connection.Execute(@"INSERT INTO [dbo].[Protractor_Invoices_ServicePackages]
           ([ServicePackagesID]
           ,[ID]
           ,[Chapter]
           ,[Rank]
           ,[Title]
           ,[BigID]
           ,[IsDone])
     VALUES
           (@ServicePackagesID
           ,@ID
           ,@Chapter
           ,@Rank
           ,@Title
           ,@BigID
           ,0)", items);
            return output;
        }

        public List<ServiceItemModel> GetServiceItemsForCompare(string bigId)
        {
            using var connection = GetConnection();
            var output = connection.Query<ServiceItemModel>("SELECT Id FROM Protractor_ServiceItems WHERE BigId=@BigId", new { BigId = bigId });
            return output.ToList();
        }

        public int InsertServiceItems(IEnumerable<ServiceItemModel> items)
        {
            using var connection = GetConnection();
            var output = connection.Execute(@"INSERT INTO [dbo].[Protractor_ServiceItems]
           ([ID]
           ,[CreationTime]
           ,[LastModifiedTime]
           ,[Type]
           ,[Lookup]
           ,[Description]
           ,[Usage]
           ,[ProductionDate]
           ,[OwnerID]
           ,[PlateRegistration]
           ,[VIN]
           ,[Year]
           ,[Make]
           ,[Model]
           ,[Submodel]
           ,[Engine]
           ,[BigID]
           ,[IsDone])
     VALUES
           (@ID
           ,@CreationTime
           ,@LastModifiedTime
           ,@Type
           ,@Lookup
           ,@Description
           ,@Usage
           ,@ProductionDate
           ,@OwnerID
           ,@PlateRegistration
           ,@VIN
           ,@Year
           ,@Make
           ,@Model
           ,@Submodel
           ,@Engine
           ,@BigID
           ,0)", items);
            return output;
        }

        public List<AppointmentModel> GetAppointmentsForCompare(string bigId)
        {
            using var connection = GetConnection();
            var output = connection.Query<AppointmentModel>("SELECT Id FROM Protractor_Appointments WHERE BigId=@BigId", new { BigId = bigId });
            return output.ToList();
        }

        public int InsertAppointments(IEnumerable<AppointmentModel> items)
        {
            using var connection = GetConnection();
            var output = connection.Execute(@"INSERT INTO [dbo].[Protractor_Appointments]
           ([ID]
           ,[CreationTime]
           ,[LastModifiedTime]
           ,[Type]
           ,[ScheduledTime]
           ,[PromisedTime]
           ,[InvoiceTime]
           ,[WorkOrderNumber]
           ,[InvoiceNumber]
           ,[PurchaseOrderNumber]
           ,[ContactID]
           ,[ServiceItemID]
           ,[Technician]
           ,[ServiceAdvisor]
           ,[InUsage]
           ,[Note]
           ,[ServicePackages]
           ,[DeferredServicePackages]
           ,[OtherChargeCode]
           ,[LocationID]
           ,[BigID])
     VALUES
           (@ID
           ,@CreationTime
           ,@LastModifiedTime
           ,@Type
           ,@ScheduledTime
           ,@PromisedTime
           ,@InvoiceTime
           ,@WorkOrderNumber
           ,@InvoiceNumber
           ,@PurchaseOrderNumber
           ,@ContactID
           ,@ServiceItemID
           ,@Technician
           ,@ServiceAdvisor
           ,@InUsage
           ,@Note
           ,@ServicePackages
           ,@DeferredServicePackages
           ,@OtherChargeCode
           ,@LocationID
           ,@BigID)", items);
            return output;
        }
    }
}
