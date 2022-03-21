namespace JetComSMSSync.Modules.ShopWare.Models
{
    public class CustomerModel
    {
        public string Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public bool Marketing_Ok { get; set; }
        public string Email { get; set; }
        public string ShopId { get; set; }
        public string Created_At { get; set; }
        public string Updated_At { get; set; }
        public string BigID { get; set; }

        public string[] Shop_Ids { get; set; }
    }
}
