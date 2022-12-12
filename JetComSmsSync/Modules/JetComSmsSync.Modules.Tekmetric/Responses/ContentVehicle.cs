using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JetComSmsSync.Modules.Tekmetric.Responses
{
    public class ContentVehicle
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string SubModel { get; set; }
        public string Engine { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public string State { get; set; }
        public string Vin { get; set; }
        public string DriveType { get; set; }
        public string Transmission { get; set; }
        public string BodyType { get; set; }
        public string Notes { get; set; }
        public string UnitNumber { get; set; }
        public string CreatedDate { get; set; }
        public System.DateTime? UpdatedDate { get; set; }
        public string ProductionDate { get; set; }
        public string DeletedDate { get; set; }
        public string BigID { get; set; }


        public void UpdateParameters(string bigId)
        {
            BigID = bigId;
        }
    }
    public class VehicleComparer : IEqualityComparer<ContentVehicle>
    {
        public bool Equals([AllowNull] ContentVehicle x, [AllowNull] ContentVehicle y)
        {
            if (x is null && y is null) return true;

            if (x is null || y is null) return false;

            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] ContentVehicle obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
