using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JetComSmsSync.Modules.Tekmetric.Responses
{
    public class Address
    {
        public long Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string StreetAddress { get; set; }
        public string FullAddress { get; set; }
        public long CustomerId { get; set; }
        public string BigID { get; set; }
    }
    public class AddressComparer : IEqualityComparer<Address>
    {
        public bool Equals([AllowNull] Address x, [AllowNull] Address y)
        {
            if (x is null && y is null) return true;

            if (x is null || y is null) return false;

            return x.Id == y.Id &&
                string.Equals(x.Address1, y.Address1) &&
                string.Equals(x.Address2, y.Address2) &&
                string.Equals(x.City, y.City) &&
                string.Equals(x.State, y.State) &&
                string.Equals(x.Zip, y.Zip);
        }

        public int GetHashCode([DisallowNull] Address obj)
        {
            return obj.Id.GetHashCode();
        }
    }


}
