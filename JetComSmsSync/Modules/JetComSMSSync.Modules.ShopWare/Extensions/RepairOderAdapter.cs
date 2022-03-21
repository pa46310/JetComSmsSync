using JetComSMSSync.Modules.ShopWare.Models;
using JetComSMSSync.Modules.ShopWare.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JetComSMSSync.Modules.ShopWare.Adapters
{
    public static class RepairOrderExtensions
    {
        public static RepairOrderModel ToRepairOrder(this RepairOrderResponse response, string bigId)
        {
            return new RepairOrderModel
            {
                Advisor_Id = response.AdvisorId,
                BigID = bigId,
                Closed_At = response.ClosedAt,
                Created_At = response.CreatedAt,
                Customer_Id = response.CustomerId,
                Customer_Source = response.CustomerSource,
                Due_In_At = response.DueInAt,
                Due_Out_At = response.DueOutAt,
                Hazmat_Tax_Rate = response.HazmatTaxRate,
                Id = response.Id,
                Integrator_Tags = string.Join(",", response.IntegratorTags.Select(x => x.Id)),
                Labor_Discount_Cents = response.LaborDiscountCents,
                Labor_Discount_Percentage = response.LaborDiscountPercentage,
                Labor_Tax_Rate = response.LaborTaxRate,
                Number = response.Number,
                Odometer = response.Odometer,
                Part_Discount_Cents = response.PartDiscountCents,
                Part_Discount_Percentage = response.PartDiscountPercentage,
                Part_Tax_Rate = response.PartTaxRate,
                Picked_Up_At = response.PickedUpAt,
                Preferred_Contact_Type = response.PreferredContactType,
                Shop_Id = response.ShopId,
                Started_At = response.StartedAt,
                State = response.State,
                Status_Id = response.StatusId,
                Sublet_Tax_Rate = response.SubletTaxRate,
                Supply_Fee_Cents = response.SupplyFeeCents,
                Taxable = response.Taxable,
                Technician_Id = response.TechnicianId,
                Updated_At = response.UpdatedAt,
                Vehicle_Id = response.VehicleId,
            };
        }

        public static IEnumerable<ServiceModel> ToServices(this RepairOrderResponse response, string bigId)
        {
            return response.Services.Select(x => new ServiceModel
            {
                BigID = bigId,
                Canned_Job_Id = x.CannedJobId,
                Category_Id = x.CategoryId,
                Comment = x.Comment,
                Completed = x.Completed,
                Completed_At = x.CompletedAt,
                Created_At = x.CreatedAt,
                Id = x.Id,
                Labor_Rate_Cents = x.LaborRateCents,
                Last_Completed_At = x.LastCompletedAt,
                ServicInvoiceUniqueID = response.Id,
                Title = x.Title,
                Updated_At = x.UpdatedAt,
            });
        }

        public static IEnumerable<ServiceLaborModel> ToLabors(this RepairOrderResponse response, string bigId)
        {
            return response.Services
                .Where(x => x.Labors != null)
                .SelectMany(x => x.Labors.Select(y => new ServiceLaborModel
                {
                    Updated_At = y.UpdatedAt,
                    BigID = bigId,
                    Created_At = y.CreatedAt,
                    Hours = y.Hours,
                    Id = y.Id,
                    Name = y.Name,
                    ServiceID = x.Id,
                    Taxable = y.Taxable,
                    Technician_Id = y.TechnicianId,
                }));
        }

        public static IEnumerable<ServicePartModel> ToParts(this RepairOrderResponse response, string bigId)
        {
            return response.Services
                .Where(x => x.Parts != null)
                .SelectMany(x => x.Parts.Select(y => new ServicePartModel
                {
                    Taxable = y.Taxable,
                    Id = y.Id,
                    BigID = bigId,
                    Brand = y.Brand,
                    Cost_Cents = y.CostCents,
                    Created_At = y.CreatedAt,
                    Description = y.Description,
                    Number = y.Number,
                    Part_Inventory_Id =y.PartInventoryId,
                    Quantity = y.Quantity,
                    Quoted_Price_Cents = y.QuotedPriceCents,
                    ServiceID = x.Id,
                    Updated_At = y.UpdatedAt,
                }));
        }

        public static IEnumerable<ServiceHazmatModel> ToHazmats(this RepairOrderResponse response, string bigId)
        {
            return response.Services
                .Where(x => x.Hazmats != null)
                .SelectMany(x => x.Hazmats.Select(y => new ServiceHazmatModel
                {
                    Taxable = y.Taxable,
                    Id = y.Id,
                    BigID = bigId,
                    Created_At = y.CreatedAt,
                    Quantity = y.Quantity,
                    ServiceID = x.Id,
                    Updated_At = y.UpdatedAt,
                    Fee_Cents = y.FeeCents,
                    Name = y.Name,
                }));
        }

        public static IEnumerable<ServiceSubletModel> ToSublets(this RepairOrderResponse response, string bigId)
        {
            return response.Services
                .Where(x => x.Sublets != null)
                .SelectMany(x => x.Sublets.Select(y => new ServiceSubletModel
                {
                    Taxable = y.Taxable,
                    Id = y.Id,
                    BigID = bigId,
                    Created_At = y.CreatedAt,
                    ServiceID = x.Id,
                    Updated_At = y.UpdatedAt,
                    Name = y.Name,
                    Cost_Cents = y.CostCents,
                    Description = y.Description,
                    Invoice_Date = y.InvoiceDate,
                    Invoice_Number = y.InvoiceNumber,
                    Price_Cents = y.PriceCents,
                    Provider = y.Provider,
                    Vendor_Id = y.VendorId,
                }));
        }

        public static IEnumerable<ServiceInspectionModel> ToInspections(this RepairOrderResponse response, string bigId)
        {
            return response.Services
                .Where(x => x.Inspections != null)
                .SelectMany(x => x.Inspections.Select(y => new ServiceInspectionModel
                {
                    Id = y.Id,
                    BigID = bigId,
                    Created_At = y.CreatedAt,
                    ServiceID = x.Id,
                    Updated_At = y.UpdatedAt,
                    Name = y.Name,
                    Detail = y.Detail,
                    State = y.State,
                }));
        }
    }
}
