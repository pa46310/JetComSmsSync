namespace JetComSMSSync.Modules.ShopWare.Models
{
    public class ServiceSubletModel
    {
        public string ServiceID { get; set; }
        public string Id { get; set; }
        public string Created_At { get; set; }
        public string Updated_At { get; set; }
        public string Name { get; set; }
        public string Price_Cents { get; set; }
        public string Cost_Cents { get; set; }
        public string Provider { get; set; }
        public string Invoice_Number { get; set; }
        public string Description { get; set; }
        public bool Taxable { get; set; }
        public string Vendor_Id { get; set; }
        public string Invoice_Date { get; set; }
        public string BigID { get; set; }
    }
}
