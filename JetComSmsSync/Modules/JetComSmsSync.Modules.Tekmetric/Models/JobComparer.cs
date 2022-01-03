using JetComSmsSync.Modules.Tekmetric.Responses;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JetComSmsSync.Modules.Tekmetric.Models
{
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
