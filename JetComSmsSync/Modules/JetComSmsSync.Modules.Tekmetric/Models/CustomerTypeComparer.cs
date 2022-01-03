using JetComSmsSync.Modules.Tekmetric.Responses;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JetComSmsSync.Modules.Tekmetric.Models
{
    public class CustomerTypeComparer : IEqualityComparer<CustomerType>
    {
        public bool Equals([AllowNull] CustomerType x, [AllowNull] CustomerType y)
        {
            if (x is null && y is null) return true;

            if (x is null || y is null) return false;

            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] CustomerType obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
