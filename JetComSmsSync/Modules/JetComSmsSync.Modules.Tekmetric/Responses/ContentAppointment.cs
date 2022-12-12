using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JetComSmsSync.Modules.Tekmetric.Responses
{
    public class ContentAppointment
    {
        public int Id { get; set; }
        public int ShopId { get; set; }
        public int CustomerId { get; set; }
        public int? VehicleId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public string LeadSource { get; set; }
        public bool? Arrived { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string DeletedDate { get; set; }
        public string BigID { get; set; }
        public void UpdateParameters(string bigId)
        {
            BigID = bigId;
        }
    }
    public class AppointmentComparer : IEqualityComparer<ContentAppointment>
    {
        public bool Equals([AllowNull] ContentAppointment x, [AllowNull] ContentAppointment y)
        {
            if (x is null && y is null) return true;

            if (x is null || y is null) return false;

            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] ContentAppointment obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
