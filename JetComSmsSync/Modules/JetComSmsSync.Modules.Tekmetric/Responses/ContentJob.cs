using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JetComSmsSync.Modules.Tekmetric.Responses
{
    public class ContentJob
    {
        public int Id { get; set; }
        public int RepairOrderId { get; set; }
        public int VehicleId { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public bool? Authorized { get; set; }
        public string AuthorizedDate { get; set; }
        public bool Selected { get; set; }
        public int? TechnicianId { get; set; }
        public string Note { get; set; }
        public string CannedJobId { get; set; }
        public string JobCategoryName { get; set; }
        public int PartsTotal { get; set; }
        public int LaborTotal { get; set; }
        public int DiscountTotal { get; set; }
        public int FeeTotal { get; set; }
        public int Subtotal { get; set; }
        public bool Archived { get; set; }
        public string CreatedDate { get; set; }
        public string CompletedDate { get; set; }
        public string UpdatedDate { get; set; }
        public List<ContentLabor> Labor { get; set; }
        public List<ContentPart> Parts { get; set; }
        //public List<Fee> Fees { get; set; }
        //public List<object> Discounts { get; set; }
        public string LaborHours { get; set; }
        public string LoggedHours { get; set; }
        public int? Sort { get; set; }
        public string BigID { get; set; }
        public void UpdateParameters(string bigId)
        {
            BigID = bigId;
            if (Labor != null && Labor.Count > 0)
            {
                foreach (var labor in Labor)
                {
                    labor.BigID = bigId;
                    labor.JobId = Id;
                }
            }
            if (Parts != null && Parts.Count > 0)
            {
                foreach (var part in Parts)
                {
                    part.BigID = bigId;
                    part.JobId = Id;
                }
            }
        }
    }
    public class JobComparer : IEqualityComparer<ContentJob>
    {
        public bool Equals([AllowNull] ContentJob x, [AllowNull] ContentJob y)
        {
            if (x is null && y is null) return true;

            if (x is null || y is null) return false;

            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] ContentJob obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
