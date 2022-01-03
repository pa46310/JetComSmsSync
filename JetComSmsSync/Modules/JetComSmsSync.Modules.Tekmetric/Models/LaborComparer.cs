using JetComSmsSync.Modules.Tekmetric.Responses;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JetComSmsSync.Modules.Tekmetric.Models
{
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
