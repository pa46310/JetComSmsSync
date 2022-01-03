using JetComSmsSync.Modules.Tekmetric.Responses;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JetComSmsSync.Modules.Tekmetric.Models
{
    public class AddressComparer : IEqualityComparer<Address>
    {
        public bool Equals([AllowNull] Address x, [AllowNull] Address y)
        {
            if (x is null && y is null) return true;

            if (x is null || y is null) return false;

            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] Address obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
