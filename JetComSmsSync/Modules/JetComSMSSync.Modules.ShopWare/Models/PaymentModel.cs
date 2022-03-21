namespace JetComSMSSync.Modules.ShopWare.Models
{
    public class PaymentModel
    {
        public string InvoiceUniqueID { get; set; }
        public string Id { get; set; }
        public string Created_At { get; set; }
        public string Updated_At { get; set; }
        public string Payment_Type { get; set; }
        public string Notes { get; set; }
        public string Amount_Cents { get; set; }
        public string BigID { get; set; }

        public string Repair_Order_Id { get; set; }
    }
}
