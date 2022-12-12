using Dapper;

using JetComSmsSync.Modules.Tekmetric.Models;
using JetComSmsSync.Modules.Tekmetric.Responses;
using JetComSmsSync.Services.Interfaces;

using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace JetComSmsSync.Modules.Tekmetric
{
    public class DatabaseClient
    {
        private readonly string _autoRepairConnectionString;
        private readonly string _reportsConnectionString;
        private readonly ICacheService _cache; 

        public DatabaseClient(IConfiguration configuration, ICacheService cache)
        {
            _reportsConnectionString = configuration.GetConnectionString("V2Reports");
            _autoRepairConnectionString = configuration.GetConnectionString("AutoRepairSMS");
            _cache = cache;
        }

        public IEnumerable<AccountModel> GetAccounts()
        {
            var insertQuery = "SELECT A.BigID,TA.Shopid,A.AccountFullName,TA.Environment,TA.AccessToken FROM TekmetricAccountdetail TA JOIN Accounts A ON TA.AccountID=A.AccountID";
            using var connection = new SqlConnection(_reportsConnectionString);
            var output = connection.Query<AccountModel>(insertQuery);
            return output;
        }

        #region Customer

        public IEnumerable<ContentCustomer> GetCustomerForCompare(string bigId)
        {
            var key = $"customers-{bigId}";
            if (_cache.TryRead(key, out ContentCustomer[] items))
            {
                return items;
            }

            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Query<ContentCustomer>("SELECT customeruniqueid as [Id], FirstName, LastName, Email, Notes, OkForMarketing, Birthday FROM [Tekmetric_Customer] WHERE bigid=@BigID", new { BigID = bigId });
            _cache.Append(key, output.Select(x => new ContentComparer { Id = x.Id }));
            return output;
        }

        public int InsertCustomer(IEnumerable<ContentCustomer> items)
        {
            if (!items.Any())
            {
                return 0;
            }

            var bigId = items.First().BigID;
            var key = $"customers-{bigId}";

            var insertQuery = @"INSERT INTO [dbo].[Tekmetric_Customer]
           ([id]
           ,[firstname]
           ,[lastname]
           ,[email]
           ,[notes]
           ,[contactfirstname]
           ,[contactlastname]
           ,[shopid]
           ,[okformarketing]
           ,[createddate]
           ,[updateddate]
           ,[birthday]
           ,[bigid]
           ,[isdone])
     VALUES
           (@Id
           ,@FirstName
           ,@LastName
           ,@Email
           ,@Notes
           ,@ContactFirstName
           ,@ContactLastName
           ,@ShopId
           ,@OkForMarketing
           ,@CreatedDate
           ,@UpdatedDate
           ,@Birthday
           ,@BigId
           ,0)";
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(insertQuery, items);

            _cache.Append(key, items.Select(x => new ContentComparer { Id = x.Id }));
            return output;
        }

        #endregion

        #region Phone Number
        public IEnumerable<Phone> GetPhoneNumberForCompare(string bigId)
        {
            var key = $"phone-{bigId}";
            if (_cache.TryRead(key, out Phone[] items))
            {
                return items;
            }
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Query<Phone>("SELECT id FROM [Tekmetric_PhoneNumber] WHERE bigid=@BigId", new { BigId = bigId });
            _cache.Append(key, output.Select(x => new ContentComparer { Id = x.Id }));
            return output;
        }

        public int InsertPhoneNumber(IEnumerable<Phone> items)
        {
            if (!items.Any())
            {
                return 0;
            }
            var insertQuery = @"INSERT INTO [dbo].[Tekmetric_PhoneNumber]
           ([id]
           ,[number]
           ,[type]
           ,[customerid]
           ,[bigid])
     VALUES
           (@Id
           ,@Number
           ,@Type
           ,@CustomerId
           ,@BigId)";
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(insertQuery, items);

            var bigId = items.First().BigID;
            var key = $"phone-{bigId}";
            _cache.Append(key, items.Select(x => new ContentComparer { Id = x.Id }));
            return output;
        }

        #endregion

        #region Address
        public IEnumerable<Address> GetAddressForCompare(string bigId)
        {
            var key = $"address-{bigId}";
            if (_cache.TryRead(key, out Address[] items))
            {
                return items;
            }
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Query<Address>("SELECT Id, Address1, Address2, City, State, Zip FROM [Tekmetric_Address] WHERE bigid=@BigId", new { BigId = bigId });
            _cache.Append(key, output.Select(x => new ContentComparer { Id = x.Id }));
            return output;
        }

        public int InsertAddress(IEnumerable<Address> items)
        {
            if (!items.Any())
            {
                return 0;
            }
            var insertQuery = @"INSERT INTO [dbo].[Tekmetric_Address]
           ([id]
           ,[address1]
           ,[address2]
           ,[city]
           ,[state]
           ,[zip]
           ,[streetaddress]
           ,[fulladdress]
           ,[customerid]
           ,[bigid])
     VALUES
           (@Id
           ,@Address1
           ,@Address2
           ,@City
           ,@State
           ,@Zip
           ,@StreetAddress
           ,@FullAddress
           ,@CustomerId
           ,@BigId)";
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(insertQuery, items);

            var bigId = items.First().BigID;
            var key = $"address-{bigId}";
            _cache.Append(key, items.Select(x => new ContentComparer { Id = x.Id }));
            return output;
        }

        #endregion

        #region Customer Type
        public IEnumerable<CustomerType> GetCustomerTypeForCompare(string bigId)
        {
            var key = $"custtype-{bigId}";
            if (_cache.TryRead(key, out CustomerType[] items))
            {
                return items;
            }
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Query<CustomerType>("SELECT id FROM [Tekmetric_CustomerType] WHERE bigid=@BigId", new { BigId = bigId });
            _cache.Append(key, items.Select(x => new ContentComparer { Id = x.Id }));
            return output;
        }

        public int InsertCustomerTypes(IEnumerable<CustomerType> items)
        {
            if (!items.Any())
            {
                return 0;
            }
            var insertQuery = @"INSERT INTO [dbo].[Tekmetric_CustomerType]
           ([id]
           ,[code]
           ,[name]
           ,[customerid]
           ,[bigid])
     VALUES
           (@Id
           ,@Code
           ,@Name
           ,@CustomerId
           ,@BigId)";
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(insertQuery, items);

            var bigId = items.First().BigID;
            var key = $"custtype-{bigId}";
            _cache.Append(key, items.Select(x => new ContentComparer { Id = x.Id }));
            return output;
        }

        #endregion

        #region Appointments
        public IEnumerable<ContentAppointment> GetAppointmentForCompare(string bigId)
        {
            var key = $"appt-{bigId}";
            if (_cache.TryRead(key, out ContentAppointment[] items))
            {
                return items;
            }
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Query<ContentAppointment>("SELECT id FROM [Tekmetric_Appointments] WHERE bigid=@BigId", new { BigId = bigId });
            _cache.Append(key, items.Select(x => new ContentComparer { Id = x.Id }));
            return output;
        }

        public int InsertAppointments(IEnumerable<ContentAppointment> items)
        {
            if (!items.Any())
            {
                return 0;
            }
            var insertQuery = @"INSERT INTO [dbo].[Tekmetric_Appointments]
           ([id]
           ,[shopid]
           ,[customerid]
           ,[vehicleid]
           ,[starttime]
           ,[endtime]
           ,[description]
           ,[title]
           ,[color]
           ,[leadsource]
           ,[arrived]
           ,[createddate]
           ,[updateddate]
           ,[bigid]
           ,[isdone])
     VALUES
           (@Id
           ,@ShopId
           ,@CustomerId
           ,@VehicleId
           ,@StartTime
           ,@EndTime
           ,@Description
           ,@Title
           ,@Color
           ,@LeadSource
           ,@Arrived
           ,@CreatedDate
           ,@UpdatedDate
           ,@BigID
           ,0)";
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(insertQuery, items);

            var bigId = items.First().BigID;
            var key = $"appt-{bigId}";
            _cache.Append(key, items.Select(x => new ContentComparer { Id = x.Id }));
            return output;
        }

        #endregion

        #region Vehicle
        public IEnumerable<ContentVehicle> GetVehicleForCompare(string bigId)
        {
            var key = $"vehicle-{bigId}";
            if (_cache.TryRead(key, out ContentVehicle[] items))
            {
                return items;
            }
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Query<ContentVehicle>("SELECT id FROM [Tekmetric_Vehicle] WHERE bigid=@BigId", new { BigId = bigId });
            _cache.Append(key, items.Select(x => new ContentComparer { Id = x.Id }));
            return output;
        }

        public int InsertVehicles(IEnumerable<ContentVehicle> items)
        {
            if (!items.Any())
            {
                return 0;
            }
            var insertQuery = @"INSERT INTO [dbo].[Tekmetric_Vehicle]
           ([id]
           ,[customerid]
           ,[year]
           ,[make]
           ,[model]
           ,[submodel]
           ,[engine]
           ,[color]
           ,[licenseplate]
           ,[state]
           ,[vin]
           ,[notes]
           ,[unitnumber]
           ,[createddate]
           ,[updateddate]
           ,[productiondate]
           ,[bigid]
           ,[isdone])
     VALUES
           (@Id
           ,@CustomerId
           ,@Year
           ,@Make
           ,@Model
           ,@SubModel
           ,@Engine
           ,@Color
           ,@LicensePlate
           ,@State
           ,@Vin
           ,@Notes
           ,@UnitNumber
           ,@CreatedDate
           ,@UpdatedDate
           ,@ProductionDate
           ,@BigID
           ,0)";
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(insertQuery, items);

            var bigId = items.First().BigID;
            var key = $"vehicle-{bigId}";
            _cache.Append(key, items.Select(x => new ContentComparer { Id = x.Id }));
            return output;
        }

        #endregion

        #region Repair Order
        public IEnumerable<ContentRepairOrder> GetRepairOrderForCompare(string bigId)
        {
            var key = $"ro-{bigId}";
            if (_cache.TryRead(key, out ContentRepairOrder[] items))
            {
                return items;
            }
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Query<ContentRepairOrder>("SELECT Id, MilesIn, MilesOut, KeyTag, CompletedDate, PostedDate, LaborSales, PartsSales, SubletSales, DiscountTototal, FeeTotal, Taxes, AmountPaid, TotalSales, CreatedDate, UpdatedDate FROM [Tekmetric_Repairorder] WHERE bigid=@BigId", new { BigId = bigId });
            _cache.Append(key, items.Select(x => new ContentComparer { Id = x.Id }));
            return output;
        }

        public int InsertRepairOrder(IEnumerable<ContentRepairOrder> items)
        {
            if (!items.Any())
            {
                return 0;
            }
            var insertQuery = @"INSERT INTO [dbo].[Tekmetric_Repairorder]
           ([id]
           ,[repairordernumber]
           ,[shopid]
           ,[customerid]
           ,[technicianid]
           ,[servicewriterid]
           ,[vehicleid]
           ,[milesin]
           ,[milesout]
           ,[keytag]
           ,[completeddate]
           ,[posteddate]
           ,[laborsales]
           ,[partssales]
           ,[subletsales]
           ,[discounttotal]
           ,[feetotal]
           ,[taxes]
           ,[amountpaid]
           ,[totalsales]
           ,[createddate]
           ,[updateddate]
           ,[estimateurl]
           ,[inspectionurl]
           ,[invoiceurl]
           ,[bigid]
           ,[isdone])
     VALUES
           (@Id
           ,@RepairOrderNumber
           ,@ShopId
           ,@CustomerId
           ,@TechnicianId
           ,@ServiceWriterId
           ,@VehicleId
           ,@MilesIn
           ,@MilesOut
           ,@Keytag
           ,@CompletedDate
           ,@PostedDate
           ,@LaborSales
           ,@PartsSales
           ,@SubletSales
           ,@DiscountTotal
           ,@FeeTotal
           ,@Taxes
           ,@AmountPaid
           ,@TotalSales
           ,@CreatedDate
           ,@UpdatedDate
           ,@EstimateUrl
           ,@InspectionUrl
           ,@InvoiceUrl
           ,@BigID
           ,0)";
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(insertQuery, items);

            var bigId = items.First().BigID;
            var key = $"ro-{bigId}";
            _cache.Append(key, items.Select(x => new ContentComparer { Id = x.Id }));
            return output;
        }

        #endregion

        #region Jobs
        public IEnumerable<ContentJob> GetJobForCompare(string bigId)
        {
            var key = $"job-{bigId}";
            if (_cache.TryRead(key, out ContentJob[] items))
            {
                return items;
            }
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Query<ContentJob>("SELECT id FROM [Tekmetric_Jobs] WHERE bigid=@BigId", new { BigId = bigId });
            _cache.Append(key, items.Select(x => new ContentComparer { Id = x.Id }));
            return output;
        }

        public int InsertJobs(IEnumerable<ContentJob> items)
        {
            if (!items.Any())
            {
                return 0;
            }
            var insertQuery = @"INSERT INTO [dbo].[Tekmetric_Jobs]
           ([id]
           ,[repairorderid]
           ,[vehicleid]
           ,[customerid]
           ,[name]
           ,[authorized]
           ,[authorizeddate]
           ,[selected]
           ,[technicianid]
           ,[note]
           ,[partstotal]
           ,[labortotal]
           ,[discounttotal]
           ,[feetotal]
           ,[subtotal]
           ,[archived]
           ,[createddate]
           ,[updateddate]
           ,[bigid])
     VALUES
           (@Id
           ,@RepairOrderId
           ,@VehicleId
           ,@CustomerId
           ,@Name
           ,@Authorized
           ,@AuthorizedDate
           ,@Selected
           ,@TechnicianId
           ,@Note
           ,@PartsTotal
           ,@LaborTotal
           ,@DiscountTotal
           ,@FeeTotal
           ,@Subtotal
           ,@Archived
           ,@CreatedDate
           ,@UpdatedDate
           ,@BigID)";
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(insertQuery, items);

            var bigId = items.First().BigID;
            var key = $"job-{bigId}";
            _cache.Append(key, items.Select(x => new ContentComparer { Id = x.Id }));
            return output;
        }

        #endregion

        #region Parts
        public IEnumerable<ContentPart> GetPartForCompare(string bigId)
        {
            var key = $"part-{bigId}";
            if (_cache.TryRead(key, out ContentPart[] items))
            {
                return items;
            }
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Query<ContentPart>("SELECT id FROM [Tekmetric_parts] WHERE bigid=@BigId", new { BigId = bigId });
            _cache.Append(key, items.Select(x => new ContentComparer { Id = x.Id }));
            return output;
        }

        public int InsertParts(IEnumerable<ContentPart> items)
        {
            if (!items.Any())
            {
                return 0;
            }
            var insertQuery = @"INSERT INTO [dbo].[Tekmetric_parts]
           ([id]
           ,[quantity]
           ,[brand]
           ,[name]
           ,[partnumber]
           ,[description]
           ,[cost]
           ,[retail]
           ,[model]
           ,[width]
           ,[ratio]
           ,[diameter]
           ,[constructiontype]
           ,[loadindex]
           ,[speedrating]
           ,[jobid]
           ,[bigid])
     VALUES
           (@Id
           ,@Quantity
           ,@Brand
           ,@Name
           ,@PartNumber
           ,@Description
           ,@Cost
           ,@Retail
           ,@Model
           ,@Width
           ,@Ratio
           ,@Diameter
           ,@ConstructionType
           ,@LoadIndex
           ,@SpeedRating
           ,@JobId
           ,@BigID)";
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(insertQuery, items);

            var bigId = items.First().BigID;
            var key = $"part-{bigId}";
            _cache.Append(key, items.Select(x => new ContentComparer { Id = x.Id }));
            return output;
        }
        #endregion

        #region Labor
        public IEnumerable<ContentLabor> GetLaborForCompare(string bigId)
        {
            var key = $"labor-{bigId}";
            if (_cache.TryRead(key, out ContentLabor[] items))
            {
                return items;
            }
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Query<ContentLabor>("SELECT id FROM [Tekmetric_labor] WHERE bigid=@BigId", new { BigId = bigId });
            _cache.Append(key, items.Select(x => new ContentComparer { Id = x.Id }));
            return output;
        }

        public int InsertLabors(IEnumerable<ContentLabor> items)
        {
            if (!items.Any())
            {
                return 0;
            }
            var insertQuery = @"INSERT INTO [dbo].[Tekmetric_labor]
           ([id]
           ,[name]
           ,[rate]
           ,[hours]
           ,[complete]
           ,[jobid]
           ,[bigid])
     VALUES
           (@Id
           ,@Name
           ,@Rate
           ,@Hours
           ,@Complete
           ,@JobId
           ,@BigID)";
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(insertQuery, items);

            var bigId = items.First().BigID;
            var key = $"labor-{bigId}";
            _cache.Append(key, items.Select(x => new ContentComparer { Id = x.Id }));
            return output;
        }
        #endregion
    }
}
