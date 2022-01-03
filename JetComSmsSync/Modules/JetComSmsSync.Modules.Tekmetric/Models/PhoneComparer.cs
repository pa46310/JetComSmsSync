using JetComSmsSync.Modules.Tekmetric.Responses;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JetComSmsSync.Modules.Tekmetric.Models
{
    public class PhoneComparer : IEqualityComparer<Phone>
    {
        public bool Equals([AllowNull] Phone x, [AllowNull] Phone y)
        {
            if (x is null && y is null) return true;

            if (x is null || y is null) return false;

            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] Phone obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
