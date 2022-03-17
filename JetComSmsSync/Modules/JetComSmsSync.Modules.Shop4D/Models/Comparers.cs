using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JetComSmsSync.Modules.Shop4D.Models
{
    public static class Shop4DComparers
    {
        public static ContactComparer Contact { get; } = new ContactComparer();
        public static CustomerComparer Customer { get; } = new CustomerComparer();
        public static LaborComparer Labor { get; } = new LaborComparer();
        public static PartComparer Part { get; } = new PartComparer();
        public static RepairOrderComparer RepairOrder { get; } = new RepairOrderComparer();
        public static VehicleComparer Vehicle { get; } = new VehicleComparer();
    }

    public class ContactComparer : IEqualityComparer<Contact>
    {
        public bool Equals([AllowNull] Contact x, [AllowNull] Contact y)
        {
            return x.ContactData.Equals(y.ContactData) && x.CustomerId.Equals(y.CustomerId);
        }

        public int GetHashCode([DisallowNull] Contact item)
        {
            return item.CustomerId.GetHashCode();
        }
    }

    public class CustomerComparer : IEqualityComparer<Customer>
    {
        public bool Equals([AllowNull] Customer x, [AllowNull] Customer y)
        {
            return x.CustomerId.Equals(y.CustomerId);
        }

        public int GetHashCode([DisallowNull] Customer item)
        {
            return item.CustomerId.GetHashCode();
        }
    }

    public class LaborComparer : IEqualityComparer<Labor>
    {
        public bool Equals([AllowNull] Labor x, [AllowNull] Labor y)
        {
            return x.Description.Equals(y.Description) && x.RoNumber.Equals(y.RoNumber);
        }

        public int GetHashCode([DisallowNull] Labor item)
        {
            return item.RoNumber.GetHashCode();
        }
    }

    public class PartComparer : IEqualityComparer<Part>
    {
        public bool Equals([AllowNull] Part x, [AllowNull] Part y)
        {
            return x.RoNumber.Equals(y.RoNumber) && x.Description.Equals(y.Description);
        }

        public int GetHashCode([DisallowNull] Part item)
        {
            return item.RoNumber.GetHashCode();
        }
    }
    public class RepairOrderComparer : IEqualityComparer<RepairOrderInfo>
    {
        public bool Equals([AllowNull] RepairOrderInfo x, [AllowNull] RepairOrderInfo y)
        {
            return x.RoNumber.Equals(y.RoNumber);
        }

        public int GetHashCode([DisallowNull] RepairOrderInfo item)
        {
            return item.RoNumber.GetHashCode();
        }
    }
    public class VehicleComparer : IEqualityComparer<Vehicle>
    {
        public bool Equals([AllowNull] Vehicle x, [AllowNull] Vehicle y)
        {
            return x.VehicleId.Equals(y.VehicleId) && x.CustomerId.Equals(y.CustomerId);
        }

        public int GetHashCode([DisallowNull] Vehicle item)
        {
            return item.VehicleId.GetHashCode();
        }
    }
}
