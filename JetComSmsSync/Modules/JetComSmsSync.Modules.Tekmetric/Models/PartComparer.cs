using JetComSmsSync.Modules.Tekmetric.Responses;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JetComSmsSync.Modules.Tekmetric.Models
{
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
