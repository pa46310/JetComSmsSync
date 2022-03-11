using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JetComSmsSync.Modules.CDK.Models
{
    public class SRODModelComparer : IEqualityComparer<SRODModel>
    {
        public bool Equals([AllowNull] SRODModel x, [AllowNull] SRODModel y)
        {
            return x.RONumber.Equals(y.RONumber);
        }

        public int GetHashCode([DisallowNull] SRODModel item)
        {
            return item.RONumber.GetHashCode();
        }
    }
}
