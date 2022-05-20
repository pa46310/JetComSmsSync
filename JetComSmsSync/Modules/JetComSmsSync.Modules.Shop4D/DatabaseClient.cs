using Dapper;
using JetComSmsSync.Modules.Shop4D.Models;

using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace JetComSmsSync.Modules.Shop4D
{
    public class DatabaseClient
    {
        private readonly string _autoRepairConnectionString;
        private readonly string _reportsConnectionString;

        public DatabaseClient(IConfiguration configuration)
        {
            _reportsConnectionString = configuration.GetConnectionString("V2Reports");
            _autoRepairConnectionString = configuration.GetConnectionString("AutoRepairSMS");
        }
        public AccountModel[] GetAccounts()
        {
            using var connection = new SqlConnection(_reportsConnectionString);
            var output = connection.Query<AccountModel>("Select AccountFullName,companyid,username,password,bigid from Shop4DAccountdetail");
            return output.ToArray();
        }

        public List<Contact> GetContactForCompare(string bigId)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            return connection.Query<Contact>("SELECT ContactData,CustomerId FROM [dbo].[Shop4D_Contact] WHERE BigID=@BigID", new { BigID = bigId }).AsList();

        }

        public List<Customer> GetCustomerForCompare(string bigId)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            return connection.Query<Customer>("SELECT CustomerId FROM [dbo].[Shop4D_Customer] WHERE BigID=@BigID", new { BigID = bigId }).AsList();

        }

        public List<Labor> GetLaborForCompare(string bigId)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            return connection.Query<Labor>("SELECT RoNumber,Description FROM [dbo].[Shop4D_Labor] WHERE BigID=@BigID", new { BigID = bigId }).AsList();

        }
        public List<Part> GetPartForCompare(string bigId)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            return connection.Query<Part>("SELECT RoNumber,Description FROM [dbo].[Shop4D_Labor] WHERE BigID=@BigID", new { BigID = bigId }).AsList();

        }
        public List<RepairOrderInfo> GetRepairOrderForCompare(string bigId)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            return connection.Query<RepairOrderInfo>("SELECT RoNumber FROM [dbo].[Shop4D_RepairOrder] WHERE BigID=@BigID", new { BigID = bigId }).AsList();

        }
        public List<Vehicle> GetVehicleForCompare(string bigId)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            return connection.Query<Vehicle>("SELECT VehicleId,CustomerId FROM [dbo].[Shop4D_Vehicle] WHERE BigID=@BigID", new { BigID = bigId }).AsList();

        }

        public int InsertContact(IEnumerable<Contact> items)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(@"INSERT INTO [dbo].[Shop4D_Contact]
           ([customerid]
           ,[contactdata]
           ,[contacttype]
           ,[other]
           ,[bigid])
     VALUES
           (@CustomerId
           ,@ContactData
           ,@ContactType
           ,@Other
           ,@BigID)", items);
            return output;
        }
        public int InsertCustomers(IEnumerable<Customer> items)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(@"INSERT INTO [dbo].[Shop4D_Customer]
           ([customerid]
           ,[customerfirstname]
           ,[customerlastname]
           ,[customerbusinessname]
           ,[address]
           ,[city]
           ,[state]
           ,[zip]
           ,[bigid]
           ,[isdone])
     VALUES
           (@CustomerId
           ,@CustomerFirstName
           ,@CustomerLastName
           ,@CustomerBusinessName
           ,@Address
           ,@City
           ,@State
           ,@Zip
           ,@BigID
           ,0)", items);
            return output;
        }
        public int InsertLabor(IEnumerable<Labor> items)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(@"INSERT INTO [dbo].[Shop4D_Labor]
           ([ronumber]
           ,[description]
           ,[hours]
           ,[price]
           ,[approved]
           ,[technician]
           ,[bigid])
     VALUES
           (@RoNumber
           ,@Description
           ,@Hours
           ,@Price
           ,@Approved
           ,@Technician
           ,@BigID)", items);
            return output;
        }
        public int InsertParts(IEnumerable<Part> items)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(@"INSERT INTO [dbo].[Shop4D_part]
           ([ronumber]
           ,[description]
           ,[price]
           ,[quantity]
           ,[bigid])
     VALUES
           (@RoNumber
           ,@Description
           ,@Price
           ,@Cost
           ,@BigID)", items);
            return output;
        }
        public int InsertRepairOrder(IEnumerable<RepairOrderInfo> repairOrders)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(@"INSERT INTO [dbo].[Shop4D_RepairOrder]
           ([customerid]
           ,[vehicleid]
           ,[ronumber]
           ,[servicewriter]
           ,[rodate]
           ,[paydate]
           ,[source]
           ,[partsonly]
           ,[parts]
           ,[labor]
           ,[discountparts]
           ,[discountlabor]
           ,[bigid]
           ,[isdone])
     VALUES
           (@CustomerId
           ,@VehicleId
           ,@RoNumber
           ,@ServiceWriter
           ,@RoDate
           ,@PayDate
           ,@Source
           ,@PartsOnly
           ,@Parts
           ,@Labor
           ,@DiscountParts
           ,@DiscountLabor
           ,@BigID
           ,0)", repairOrders);

            return output;
        }
        public int InsertVehicles(IEnumerable<Vehicle> items)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(@"INSERT INTO [dbo].[Shop4D_Vehicle]
           ([customerid]
           ,[vehicleid]
           ,[vin]
           ,[year]
           ,[make]
           ,[model]
           ,[license]
           ,[mileage]
           ,[bigid]
           ,[isdone])
     VALUES
           (@CustomerId
           ,@VehicleId
           ,@Vin
           ,@Year
           ,@Make
           ,@Model
           ,@License
           ,@Mileage
           ,@BigID
           ,0)", items);
            return output;
        }
    }
}
