using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JetComSmsSync.Modules.Tekmetric.Responses
{
    public class ContentLabor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rate { get; set; }
        public double Hours { get; set; }
        public bool Complete { get; set; }
        public int JobId { get; set; }
        public string BigID { get; set; }
    }
    public class LaborComparer : IEqualityComparer<ContentLabor>
    {
        public bool Equals([AllowNull] ContentLabor x, [AllowNull] ContentLabor y)
        {
            if (x is null && y is null) return true;

            if (x is null || y is null) return false;

            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] ContentLabor obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
