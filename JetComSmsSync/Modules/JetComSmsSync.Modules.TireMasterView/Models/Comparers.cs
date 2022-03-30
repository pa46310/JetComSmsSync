using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace JetComSmsSync.Modules.TireMasterView.Models
{
    public static class Comparers
    {
        public static CustomerComparer Customer { get; } = new CustomerComparer();
        public static VehicleComparer Vehicle { get; } = new VehicleComparer();
        public static RepairOrderComparer RepairOrder { get; } = new RepairOrderComparer();
        public static LineItemComparer LineItem { get; } = new LineItemComparer();
    }

    public class VehicleComparer : IEqualityComparer<ItemModel>
    {
        public bool Equals([AllowNull] ItemModel x, [AllowNull] ItemModel y)
        {
            return Equals(x.VehicleId, y.VehicleId) && x.LocationId == y.LocationId;
        }

        public int GetHashCode([DisallowNull] ItemModel item)
        {
            return item.VehicleId.GetHashCode();
        }
    }

    public class CustomerComparer : IEqualityComparer<ItemModel>
    {
        public bool Equals([AllowNull] ItemModel x, [AllowNull] ItemModel y)
        {
            return string.Equals(x.CustomerId, y.CustomerId);
        }

        public int GetHashCode([DisallowNull] ItemModel item)
        {
            return item.CustomerId.GetHashCode();
        }
    }

    public class RepairOrderComparer : IEqualityComparer<ItemModel>
    {
        public bool Equals([AllowNull] ItemModel x, [AllowNull] ItemModel y)
        {
            return Equals(x.OrderId, y.OrderId) && x.LocationId == y.LocationId;
        }

        public int GetHashCode([DisallowNull] ItemModel item)
        {
            return item.OrderId.GetHashCode();
        }
    }

    public class LineItemComparer : IEqualityComparer<ItemModel>
    {
        public bool Equals([AllowNull] ItemModel x, [AllowNull] ItemModel y)
        {
            return Equals(x.OrderId, y.OrderId) && x.LocationId == y.LocationId && string.Equals(x.ItemDescription, y.ItemDescription);
        }

        public int GetHashCode([DisallowNull] ItemModel item)
        {
            return item.ItemDescription.GetHashCode();
        }
    }
}
