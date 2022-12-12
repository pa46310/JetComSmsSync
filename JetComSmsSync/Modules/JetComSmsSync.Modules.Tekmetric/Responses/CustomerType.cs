using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JetComSmsSync.Modules.Tekmetric.Responses
{
    public class CustomerType
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long CustomerId { get; set; }
        public string BigID { get; set; }
    }
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
