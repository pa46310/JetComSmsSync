using System.Collections.Generic;

namespace JetComSmsSync.Modules.Tekmetric.Responses
{
    public class ContentRepairOrder
    {
        public int Id { get; set; }
        public int RepairOrderNumber { get; set; }
        public int ShopId { get; set; }
        public RepairOrderStatus RepairOrderStatus { get; set; }
        public RepairOrderLabel RepairOrderLabel { get; set; }
        public int? AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public int? TechnicianId { get; set; }
        public int? ServiceWriterId { get; set; }
        public int VehicleId { get; set; }
        public string MilesIn { get; set; }
        public string MilesOut { get; set; }
        public string Keytag { get; set; }
        public string CompletedDate { get; set; }
        public string PostedDate { get; set; }
        public int LaborSales { get; set; }
        public int PartsSales { get; set; }
        public int SubletSales { get; set; }
        public int DiscountTotal { get; set; }
        public int FeeTotal { get; set; }
        public int Taxes { get; set; }
        public int AmountPaid { get; set; }
        public int TotalSales { get; set; }
        public List<ContentJob> Jobs { get; set; }
        public List<object> Sublets { get; set; }
        //public List<Fee> Fees { get; set; }
        //public List<object> Discounts { get; set; }
        //public List<CustomerConcern> CustomerConcerns { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string DeletedDate { get; set; }
        public string EstimateShareDate { get; set; }
        public string InspectionShareDate { get; set; }
        public string CustomerTimeOut { get; set; }
        public string EstimateUrl { get; set; }
        public string InspectionUrl { get; set; }
        public string InvoiceUrl { get; set; }
        public string LeadSource { get; set; }
        public string BigID { get; set; }

        public void UpdateParameters(string bigId)
        {
            BigID = bigId;
        }
    }



}
