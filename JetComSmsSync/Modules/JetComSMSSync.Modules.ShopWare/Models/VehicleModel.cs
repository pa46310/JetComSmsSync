using System.Collections.Generic;

namespace JetComSMSSync.Modules.ShopWare.Models
{
    public class VehicleModel
    {
        public string Id { get; set; }
        public string Vin { get; set; }
        public string Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Engine { get; set; }
        public string CustomerId { get; set; }
        public string Created_At { get; set; }
        public string Updated_At { get; set; }
        public string BigID { get; set; }

        public List<string> Customer_Ids { get; set; }
    }
}
