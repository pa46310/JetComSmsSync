using System;

namespace JetComSmsSync.Modules.TireMasterView.Models
{
    public class ItemModel
    {
        public int LocationId { get; set; }
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Vin { get; set; }
        public string Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Engine { get; set; }
        public DateTime? DateOfService { get; set; }
        public int? Mileage { get; set; }
        public float ItemPrice { get; set; }
        public float ItemDiscount { get; set; }
        public int? VehicleId { get; set; }
        public int LineNumber { get; set; }
        public string ItemDescription { get; set; }
        public float Quantity { get; set; }
        public string LicenseState { get; set; }
        public string LicensePlate { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string ST { get; set; }
        public string Zip { get; set; }

        public string BigId { get; set; }

    }
}
