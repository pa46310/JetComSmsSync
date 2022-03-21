using JetComSMSSync.Modules.ShopWare.Responses;

namespace JetComSMSSync.Modules.ShopWare.Models
{
    public class RepairOrderModel
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public string Odometer { get; set; }
        public string State { get; set; }
        public string Customer_Id { get; set; }
        public string Technician_Id { get; set; }
        public string Advisor_Id { get; set; }
        public string Vehicle_Id { get; set; }
        public string Preferred_Contact_Type { get; set; }
        public string Shop_Id { get; set; }
        public string Started_At { get; set; }
        public string Closed_At { get; set; }
        public string Picked_Up_At { get; set; }
        public string Due_In_At { get; set; }
        public string Due_Out_At { get; set; }
        public string Part_Tax_Rate { get; set; }
        public string Labor_Tax_Rate { get; set; }
        public string Hazmat_Tax_Rate { get; set; }
        public string Sublet_Tax_Rate { get; set; }
        public string BigID { get; set; }
        public string Created_At { get; set; }
        public string Updated_At { get; set; }
        public string Status_Id { get; set; }
        public string Part_Discount_Cents { get; set; }
        public string Labor_Discount_Cents { get; set; }
        public bool Taxable { get; set; }
        public string Integrator_Tags { get; set; }
        public string Customer_Source { get; set; }
        public string Supply_Fee_Cents { get; set; }
        public string Part_Discount_Percentage { get; set; }
        public string Labor_Discount_Percentage { get; set; }
    }
}
