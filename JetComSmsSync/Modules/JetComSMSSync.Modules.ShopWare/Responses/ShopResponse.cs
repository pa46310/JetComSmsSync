using System;

namespace JetComSMSSync.Modules.ShopWare.Responses
{
    public class ShopResponse
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string TimeZone { get; set; }
        public string ServiceDeskEmail { get; set; }
        public int AvgLaborCostCents { get; set; }
        public double PartTaxRate { get; set; }
        public double LaborTaxRate { get; set; }
        public double HazmatTaxRate { get; set; }
        public double SubletTaxRate { get; set; }
        public double SupplyFeeRate { get; set; }
        public string SupplyFeeName { get; set; }
        public double SupplyFeeCapCents { get; set; }
        public bool MycarfaxEnabled { get; set; }
        public DateTime LiveAt { get; set; }
        //public List<object> IntegratorTags { get; set; }
    }
}
