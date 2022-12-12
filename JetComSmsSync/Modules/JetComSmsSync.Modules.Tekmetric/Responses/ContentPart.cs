using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JetComSmsSync.Modules.Tekmetric.Responses
{
    public class ContentPart
    {
        public int Id { get; set; }
        public double Quantity { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public int Retail { get; set; }
        public string Model { get; set; }
        public string Width { get; set; }
        public string Ratio { get; set; }
        public string Diameter { get; set; }
        public string ConstructionType { get; set; }
        public string LoadIndex { get; set; }
        public string SpeedRating { get; set; }
        //public PartType PartType { get; set; }
        //public string PartStatus { get; set; }
        //public List<object> DotNumbers { get; set; }
        public int JobId { get; set; }
        public string BigID { get; set; }
    }
    public class PartComparer : IEqualityComparer<ContentPart>
    {
        public bool Equals([AllowNull] ContentPart x, [AllowNull] ContentPart y)
        {
            if (x is null && y is null) return true;

            if (x is null || y is null) return false;

            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] ContentPart obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
