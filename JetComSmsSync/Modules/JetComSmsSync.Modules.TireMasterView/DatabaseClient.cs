using Dapper;
using JetComSmsSync.Modules.TireMasterView.Models;

using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace JetComSmsSync.Modules.TireMasterView
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
            using (var connection = new SqlConnection(_reportsConnectionString))
            {
                var output = connection.Query<AccountModel>(@"SELECT a.BigId
	,VIPID as LocationId
	,VipName as Name
	,Address
	,City
	,Phone
	,v.AccountId
FROM csi.dbo.ACE_36_VIPStores v
JOIN V2Reports.dbo.accounts a ON v.accountid = a.accountid
ORDER BY VipName");
                return output.ToArray();
            }
        }

        private SqlConnection GetConnection() => new SqlConnection(_autoRepairConnectionString);

        public List<ItemModel> GetCustomerForCompare(string bigId)
        {
            using (var connection = GetConnection())
            {
                var output = connection.Query<ItemModel>("SELECT CustomerId FROM TireMasterView_Customer WHERE BigId=@BigId", new { BigId = bigId });
                return output.ToList();
            }
        }

        public int InsertCustomer(IEnumerable<ItemModel> items)
        {
            using (var connection = GetConnection())
            {
                var output = connection.Execute(@"INSERT INTO [dbo].[TireMasterView_Customer]
		   ([BigId]
		   ,[CustomerId]
		   ,[FirstName]
		   ,[LastName]
		   ,[EmailAddress]
		   ,[Phone1]
		   ,[Phone2]
		   ,[Phone3]
		   ,[Address1]
		   ,[Address2]
		   ,[City]
		   ,[ST]
		   ,[Zip])
	 VALUES
		   (@BigId
		   ,@CustomerId
		   ,@FirstName
		   ,@LastName
		   ,@EmailAddress
		   ,@Phone1
		   ,@Phone2
		   ,@Phone3
		   ,@Address1
		   ,@Address2
		   ,@City
		   ,@ST
		   ,@Zip)", items);
                return output;
            }
        }

        public List<ItemModel> GetVehicleForCompare(string bigId)
        {
            using (var connection = GetConnection())
            {
                var output = connection.Query<ItemModel>("SELECT VehicleId,LocationId FROM TireMasterView_Vehicle WHERE BigId=@BigId", new { BigId = bigId });
                return output.ToList();
            }
        }

        public int InsertVehicles(IEnumerable<ItemModel> items)
        {
            using (var connection = GetConnection())
            {
                var output = connection.Execute(@"INSERT INTO [dbo].[TireMasterView_Vehicle]
		   ([BigId]
		   ,[CustomerId]
		   ,[Vin]
		   ,[Year]
		   ,[Make]
		   ,[Model]
		   ,[Engine]
		   ,[VehicleId]
		   ,[LicenceState]
		   ,[LicencePlate]
		   ,[LocationId])
	 VALUES
		   (@BigId
		   ,@CustomerId
		   ,@Vin
		   ,@Year
		   ,@Make
		   ,@Model
		   ,@Engine
		   ,@VehicleId
		   ,@LicenceState
		   ,@LicencePlate
		   ,@LocationId)", items);
                return output;
            }
        }

        public List<ItemModel> GetLineItemForCompare(string bigId)
        {
            using (var connection = GetConnection())
            {
                var output = connection.Query<ItemModel>("SELECT OrderId,LocationId,ItemDescription FROM TireMasterView_Items WHERE BigId=@BigId", new { BigId = bigId });
                return output.ToList();
            }
        }

        public int InsertLineItems(IEnumerable<ItemModel> items)
        {
            using (var connection = GetConnection())
            {
                var output = connection.Execute(@"INSERT INTO [dbo].[TireMasterView_Items]
		   ([BigId]
		   ,[OrderId]
		   ,[LocationId]
		   ,[ItemPrice]
		   ,[ItemDiscount]
		   ,[LineNumber]
		   ,[ItemDescription]
		   ,[Quantity])
	 VALUES
		   (@BigId
		   ,@OrderId
		   ,@LocationId
		   ,@ItemPrice
		   ,@ItemDiscount
		   ,@LineNumber
		   ,@ItemDescription
		   ,@Quantity)", items);
                return output;
            }
        }

        public List<ItemModel> GetRepairOrderForCompare(string bigId, DateTime startDate)
        {
            using (var connection = GetConnection())
            {
                var output = connection.Query<ItemModel>("SELECT OrderId,LocationId FROM TireMasterView_RepairOrder WHERE DateOfService>@StartDate AND BigId=@BigId", new { StartDate = startDate, BigId = bigId });
                return output.ToList();
            }
        }

        public int InsertRepairOrders(IEnumerable<ItemModel> items)
        {
            using (var connection = GetConnection())
            {
                var output = connection.Execute(@"INSERT INTO [dbo].[TireMasterView_RepairOrder]
		   ([BigId]
		   ,[OrderId]
		   ,[CustomerId]
		   ,[VehicleId]
		   ,[DateOfService]
		   ,[mileage]
		   ,[LocationId])
	 VALUES
		   (@BigId
		   ,@OrderId
		   ,@CustomerId
		   ,@VehicleId
		   ,@DateOfService
		   ,@mileage
		   ,@LocationId)", items);
                return output;
            }
        }
    }
}
