using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JetComSmsSync.Modules.CDK.Models
{
    public class CustomerModelComparer : IEqualityComparer<CustomerModel>
    {
        public bool Equals([AllowNull] CustomerModel x, [AllowNull] CustomerModel y)
        {
            return x.CustNo.Equals(y.CustNo);
        }

        public int GetHashCode([DisallowNull] CustomerModel item)
        {
            return item.CustNo.GetHashCode();
        }
    }
}
