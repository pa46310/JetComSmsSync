using JetComSmsSync.Modules.Tekmetric.Responses;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JetComSmsSync.Modules.Tekmetric.Models
{
    public class RepairOrderComparer : IEqualityComparer<ContentRepairOrder>
    {
        public bool Equals([AllowNull] ContentRepairOrder x, [AllowNull] ContentRepairOrder y)
        {
            if (x is null && y is null) return true;

            if (x is null || y is null) return false;

            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] ContentRepairOrder obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
