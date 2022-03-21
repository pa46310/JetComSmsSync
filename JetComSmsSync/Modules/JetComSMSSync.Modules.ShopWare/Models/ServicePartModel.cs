namespace JetComSMSSync.Modules.ShopWare.Models
{
    public class ServicePartModel
    {
        public string ServiceID { get; set; }
        public string Id { get; set; }
        public string Created_At { get; set; }
        public string Updated_At { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public string Quoted_Price_Cents { get; set; }
        public string Cost_Cents { get; set; }
        public string Part_Inventory_Id { get; set; }
        public bool Taxable { get; set; }
        public string Quantity { get; set; }
        public string BigID { get; set; }
    }
}
