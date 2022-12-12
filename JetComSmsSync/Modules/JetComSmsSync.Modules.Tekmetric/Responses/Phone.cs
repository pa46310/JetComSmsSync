using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JetComSmsSync.Modules.Tekmetric.Responses
{
    public class Phone
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public bool Primary { get; set; }
        public long CustomerId { get; set; }
        public string BigID { get; set; }
    }
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
