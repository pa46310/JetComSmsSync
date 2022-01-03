using JetComSmsSync.Modules.Tekmetric.Responses;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JetComSmsSync.Modules.Tekmetric.Models
{
    public class CustomerComparer : IEqualityComparer<ContentCustomer>
    {
        public bool Equals([AllowNull] ContentCustomer x, [AllowNull] ContentCustomer y)
        {
            if (x is null && y is null) return true;

            if (x is null || y is null) return false;

            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] ContentCustomer obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
