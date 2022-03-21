using Dapper;
using JetComSMSSync.Modules.ShopWare.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetComSMSSync.Modules.ShopWare
{
    public class DatabaseClient
    {
        private const string _autoRepairConnectionString = "Data Source=192.168.75.2;User ID=rweb;Password=3277r;Initial Catalog=AutoRepairSMS;Connect Timeout=20000;";
        private const string _reportsConnectionString = "Data Source=192.168.75.1;User ID=rweb;Password=3277r;Initial Catalog=V2Reports;Connect Timeout=20000;";

        public AccountModel[] GetAccounts()
        {
            using var connection = new SqlConnection(_reportsConnectionString);
            var output = connection.Query<AccountModel>("SELECT A.BigID,AW.PartnerID,AW.SecretKey,AW.TenantID,AW.ShopId FROM AccountWiseApplication AW JOIN Accounts A ON AW.AccountID=A.AccountID WHERE ApplicationName='ShopWare' ORDER BY AW.ShopId");
            return output.ToArray();
        }

        public List<CustomerModel> GetCustomersForCompare(string bigId, string shopId)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Query<CustomerModel>("SELECT Id FROM [ShopWare_Customer] WHERE BigID=@BigID AND shopid=@ShopId", new { BigID = bigId, ShopId = shopId });
            return output.ToList();
        }
        public int InsertCustomers(IEnumerable<CustomerModel> items)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(@"INSERT INTO [dbo].[ShopWare_Customer]
           ([id]
           ,[first_name]
           ,[last_name]
           ,[phone]
           ,[address]
           ,[city]
           ,[state]
           ,[zip]
           ,[marketing_ok]
           ,[email]
           ,[shopid]
           ,[created_at]
           ,[updated_at]
           ,[BigID]
           ,[IsDone])
     VALUES
           (@Id
           , @First_Name
           , @Last_Name
           , @Phone
           , @Address
           , @City
           , @State
           , @Zip
           , @Marketing_Ok
           , @Email
           , @ShopId
           , @Created_At
           , @Updated_At
           , @BigID
           , 0)", items);
            return output;
        }

        public List<PastRecomendationModel> GetPastRecommendationsForCompare(string bigId)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Query<PastRecomendationModel>("SELECT Id, Vehicle_Id FROM [ShopWare_PastRecomendation] WHERE BigID=@BigID", new { BigID = bigId });
            return output.ToList();
        }
        public int InsertPastRecomendations(IEnumerable<PastRecomendationModel> items)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(@"INSERT INTO [dbo].[ShopWare_PastRecomendation]
           ([id]
           ,[description]
           ,[approved]
           ,[approver_id]
           ,[approval_type]
           ,[imported]
           ,[vehicle_id]
           ,[done]
           ,[approval_at]
           ,[created_at]
           ,[updated_at]
           ,[BigID]
           ,[IsDone])
     VALUES
           (@Id
           ,@Description
           ,@Approved
           ,@Approver_Id
           ,@Approval_Type
           ,@Imported
           ,@Vehicle_Id
           ,@Done
           ,@Approval_At
           ,@Created_At
           ,@Updated_At
           ,@BigID
           ,0)", items);
            return output;
        }

        public List<PaymentModel> GetPaymentsForCompare(string bigId)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Query<PaymentModel>("SELECT Id, InvoiceUniqueID FROM [ShopWare_Payments] WHERE BigID=@BigID", new { BigID = bigId });
            return output.ToList();
        }
        public int InsertPayments(IEnumerable<PaymentModel> items)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(@"INSERT INTO [dbo].[ShopWare_Payments]
           ([InvoiceUniqueID]
           ,[id]
           ,[created_at]
           ,[updated_at]
           ,[payment_type]
           ,[notes]
           ,[amount_cents]
           ,[BigID])
     VALUES
           (@InvoiceUniqueID
           ,@Id
           ,@created_at
           ,@updated_at
           ,@payment_type
           ,@notes
           ,@amount_cents
           ,@BigID)", items);
            return output;
        }

        public List<RepairOrderModel> GetRepairOrdersForCompare(string bigId)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Query<RepairOrderModel>("SELECT Id, Vehicle_Id FROM [ShopWare_RepairOrder] WHERE BigID=@BigID", new { BigID = bigId });
            return output.ToList();
        }
        public int InsertRepairOrders(IEnumerable<RepairOrderModel> items)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(@"INSERT INTO [dbo].[ShopWare_RepairOrder]
           ([id]
           ,[number]
           ,[odometer]
           ,[state]
           ,[customer_id]
           ,[technician_id]
           ,[advisor_id]
           ,[vehicle_id]
           ,[preferred_contact_type]
           ,[shop_id]
           ,[started_at]
           ,[closed_at]
           ,[picked_up_at]
           ,[due_in_at]
           ,[due_out_at]
           ,[part_tax_rate]
           ,[labor_tax_rate]
           ,[hazmat_tax_rate]
           ,[sublet_tax_rate]
           ,[BigID]
           ,[created_at]
           ,[updated_at]
           ,[IsDone]
           ,[status_id]
           ,[part_discount_cents]
           ,[labor_discount_cents]
           ,[taxable]
           ,[integrator_tags]
           ,[customer_source]
           ,[supply_fee_cents]
           ,[part_discount_percentage]
           ,[labor_discount_percentage])
     VALUES
           (@id
           ,@number
           ,@odometer
           ,@state
           ,@customer_id
           ,@technician_id
           ,@advisor_id
           ,@vehicle_id
           ,@preferred_contact_type
           ,@shop_id
           ,@started_at
           ,@closed_at
           ,@picked_up_at
           ,@due_in_at
           ,@due_out_at
           ,@part_tax_rate
           ,@labor_tax_rate
           ,@hazmat_tax_rate
           ,@sublet_tax_rate
           ,@BigID
           ,@created_at
           ,@updated_at
           ,0
           ,@status_id
           ,@part_discount_cents
           ,@labor_discount_cents
           ,@taxable
           ,@integrator_tags
           ,@customer_source
           ,@supply_fee_cents
           ,@part_discount_percentage
           ,@labor_discount_percentage)", items);
            return output;
        }

        public List<ServiceHazmatModel> GetServiceHazmatsForCompare(string bigId)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Query<ServiceHazmatModel>("SELECT Id, ServiceID FROM [ShopWare_ServiceHazmats] WHERE BigID=@BigID", new { BigID = bigId });
            return output.ToList();
        }
        public int InsertServiceHazmats(IEnumerable<ServiceHazmatModel> items)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(@"INSERT INTO [dbo].[ShopWare_ServiceHazmats]
           ([ServiceID]
           ,[id]
           ,[created_at]
           ,[updated_at]
           ,[name]
           ,[fee_cents]
           ,[taxable]
           ,[quantity]
           ,[BigID])
     VALUES
           (@ServiceID
           ,@id
           ,@created_at
           ,@updated_at
           ,@name
           ,@fee_cents
           ,@taxable
           ,@quantity
           ,@BigID)", items);
            return output;
        }

        public List<ServiceInspectionModel> GetServiceInspectionsForCompare(string bigId)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Query<ServiceInspectionModel>("SELECT Id, ServiceID FROM [ShopWare_ServiceInspections] WHERE BigID=@BigID", new { BigID = bigId });
            return output.ToList();
        }
        public int InsertServiceInspections(IEnumerable<ServiceInspectionModel> items)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(@"INSERT INTO [dbo].[ShopWare_ServiceInspections]
           ([ServiceID]
           ,[id]
           ,[created_at]
           ,[updated_at]
           ,[name]
           ,[state]
           ,[detail]
           ,[BigID])
     VALUES
           (@ServiceID
           ,@id
           ,@created_at
           ,@updated_at
           ,@name
           ,@state
           ,@detail
           ,@BigID)", items);
            return output;
        }

        public List<ServiceLaborModel> GetServiceLaborsForCompare(string bigId)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Query<ServiceLaborModel>("SELECT Id, ServiceID FROM [ShopWare_ServiceLabors] WHERE BigID=@BigID", new { BigID = bigId });
            return output.ToList();
        }
        public int InsertServiceLabors(IEnumerable<ServiceLaborModel> items)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(@"INSERT INTO [dbo].[ShopWare_ServiceLabors]
           ([ServiceID]
           ,[id]
           ,[created_at]
           ,[updated_at]
           ,[name]
           ,[technician_id]
           ,[taxable]
           ,[hours]
           ,[BigID])
     VALUES
           (@ServiceID
           ,@id
           ,@created_at
           ,@updated_at
           ,@name
           ,@technician_id
           ,@taxable
           ,@hours
           ,@BigID)", items);
            return output;
        }

        public List<ServicePartModel> GetServicePartForCompare(string bigId)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Query<ServicePartModel>("SELECT Id, ServiceID FROM [Shopware_ServiceParts] WHERE BigID=@BigID", new { BigID = bigId });
            return output.ToList();
        }
        public int InsertServiceParts(IEnumerable<ServicePartModel> items)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(@"INSERT INTO [dbo].[Shopware_ServiceParts]
           ([ServiceID]
           ,[id]
           ,[created_at]
           ,[updated_at]
           ,[brand]
           ,[description]
           ,[number]
           ,[quoted_price_cents]
           ,[cost_cents]
           ,[part_inventory_id]
           ,[taxable]
           ,[quantity]
           ,[BigID])
     VALUES
           (@ServiceID
           ,@id
           ,@created_at
           ,@updated_at
           ,@brand
           ,@description
           ,@number
           ,@quoted_price_cents
           ,@cost_cents
           ,@part_inventory_id
           ,@taxable
           ,@quantity
           ,@BigID)", items);
            return output;
        }

        public List<ServiceModel> GetServiceForCompare(string bigId)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Query<ServiceModel>("SELECT Id FROM [Shopware_Services] WHERE BigID=@BigID", new { BigID = bigId });
            return output.ToList();
        }
        public int InsertServices(IEnumerable<ServiceModel> items)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(@"INSERT INTO [dbo].[ShopWare_Services]
           ([ServicInvoiceUniqueID]
           ,[id]
           ,[created_at]
           ,[updated_at]
           ,[title]
           ,[completed]
           ,[category_id]
           ,[canned_job_id]
           ,[comment]
           ,[labor_rate_cents]
           ,[completed_at]
           ,[last_completed_at]
           ,[BigID])
     VALUES
           (@ServicInvoiceUniqueID
           ,@id
           ,@created_at
           ,@updated_at
           ,@title
           ,@completed
           ,@category_id
           ,@canned_job_id
           ,@comment
           ,@labor_rate_cents
           ,@completed_at
           ,@last_completed_at
           ,@BigID)", items);
            return output;
        }

        public List<ServiceSubletModel> GetServiceSubletForCompare(string bigId)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Query<ServiceSubletModel>("SELECT Id, ServiceID FROM [ShopWare_ServiceSublets] WHERE BigID=@BigID", new { BigID = bigId });
            return output.ToList();
        }
        public int InsertServiceSublets(IEnumerable<ServiceSubletModel> items)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(@"INSERT INTO [dbo].[ShopWare_ServiceSublets]
           ([ServiceID]
           ,[id]
           ,[created_at]
           ,[updated_at]
           ,[name]
           ,[price_cents]
           ,[cost_cents]
           ,[provider]
           ,[invoice_number]
           ,[description]
           ,[taxable]
           ,[vendor_id]
           ,[invoice_date]
           ,[BigID])
     VALUES
           (@ServiceID
           ,@id
           ,@created_at
           ,@updated_at
           ,@name
           ,@price_cents
           ,@cost_cents
           ,@provider
           ,@invoice_number
           ,@description
           ,@taxable
           ,@vendor_id
           ,@invoice_date
           ,@BigID)", items);
            return output;
        }

        public List<VehicleModel> GetVehicleForCompare(string bigId)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Query<VehicleModel>("SELECT Id, CustomerId FROM [ShopWare_vehicle] WHERE BigID=@BigID", new { BigID = bigId });
            return output.ToList();
        }
        public int InsertVehicle(IEnumerable<VehicleModel> items)
        {
            using var connection = new SqlConnection(_autoRepairConnectionString);
            var output = connection.Execute(@"INSERT INTO [dbo].[ShopWare_vehicle]
           ([id]
           ,[vin]
           ,[year]
           ,[make]
           ,[model]
           ,[engine]
           ,[customerId]
           ,[created_at]
           ,[updated_at]
           ,[BigID]
           ,[IsDone])
     VALUES
           (@id
           ,@vin
           ,@year
           ,@make
           ,@model
           ,@engine
           ,@customerId
           ,@created_at
           ,@updated_at
           ,@BigID
           ,0)", items);
            return output;
        }
    }
}
