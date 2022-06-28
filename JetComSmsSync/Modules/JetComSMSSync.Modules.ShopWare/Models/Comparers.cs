using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JetComSMSSync.Modules.ShopWare.Models
{
    public static class Comparers
    {
        public static CustomerComparer Customer { get; } = new CustomerComparer();
        public static CustomerUpdateComparer CustomerUpdate { get; } = new CustomerUpdateComparer();
        public static PastRecomendationComparer PastRecomendation { get; } = new PastRecomendationComparer();
        public static PaymentComparer Payment { get; } = new PaymentComparer();
        public static RepairOrderComparer RepairOrder { get; } = new RepairOrderComparer();
        public static RepairOrderUpdateComparer RepairOrderUpdate { get; } = new RepairOrderUpdateComparer();
        public static ServiceHazmatComparer ServiceHazmat { get; } = new ServiceHazmatComparer();
        public static ServiceInspectionComparer ServiceInspection { get; } = new ServiceInspectionComparer();
        public static ServiceLaborComparer ServiceLabor { get; } = new ServiceLaborComparer();
        public static ServicePartComparer ServicePart { get; } = new ServicePartComparer();
        public static ServiceComparer Service { get; } = new ServiceComparer();
        public static ServiceSubletComparer ServiceSublet { get; } = new ServiceSubletComparer();
        public static VehicleComparer Vehicle { get; } = new VehicleComparer();
    }

    public class CustomerComparer : IEqualityComparer<CustomerModel>
    {
        public bool Equals([AllowNull] CustomerModel x, [AllowNull] CustomerModel y)
        {
            return string.Equals(x.Id, y.Id);
        }

        public int GetHashCode([DisallowNull] CustomerModel item)
        {
            return item.Id.GetHashCode();
        }
    }

    public class CustomerUpdateComparer : IEqualityComparer<CustomerModel>
    {
        public bool Equals([AllowNull] CustomerModel x, [AllowNull] CustomerModel y)
        {
            if (string.Equals(x.Id, y.Id))
            {
                if (System.DateTime.TryParse(x.Updated_At, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeUniversal, out var xDate) && System.DateTime.TryParse(y.Updated_At, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeUniversal, out var yDate))
                {
                    var equal = !xDate.Equals(yDate);
                    if (equal)
                    {

                    }
                    return equal;
                }
            }

            return false;
        }

        public int GetHashCode([DisallowNull] CustomerModel item)
        {
            return item.Id.GetHashCode();
        }
    }

    public class PastRecomendationComparer : IEqualityComparer<PastRecomendationModel>
    {
        public bool Equals([AllowNull] PastRecomendationModel x, [AllowNull] PastRecomendationModel y)
        {
            return string.Equals(x.Id, y.Id) && x.Vehicle_Id.Equals(y.Vehicle_Id);
        }

        public int GetHashCode([DisallowNull] PastRecomendationModel item)
        {
            return item.Id.GetHashCode();
        }
    }

    public class PaymentComparer : IEqualityComparer<PaymentModel>
    {
        public bool Equals([AllowNull] PaymentModel x, [AllowNull] PaymentModel y)
        {
            return string.Equals(x.Id, y.Id) && string.Equals(x.InvoiceUniqueID, y.InvoiceUniqueID);
        }

        public int GetHashCode([DisallowNull] PaymentModel item)
        {
            return item.Id.GetHashCode();
        }
    }

    public class RepairOrderComparer : IEqualityComparer<RepairOrderModel>
    {
        public bool Equals([AllowNull] RepairOrderModel x, [AllowNull] RepairOrderModel y)
        {
            return string.Equals(x.Id, y.Id) && string.Equals(x.Vehicle_Id, y.Vehicle_Id);
        }

        public int GetHashCode([DisallowNull] RepairOrderModel item)
        {
            return item.Id.GetHashCode();
        }
    }

    public class RepairOrderUpdateComparer : IEqualityComparer<RepairOrderModel>
    {
        public bool Equals([AllowNull] RepairOrderModel x, [AllowNull] RepairOrderModel y)
        {
            if (string.Equals(x.Id, y.Id))
            {
                if (System.DateTime.TryParse(x.Updated_At, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeUniversal, out var xDate) && System.DateTime.TryParse(y.Updated_At, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeUniversal, out var yDate))
                {
                    var equal = !xDate.Equals(yDate);
                    return equal;
                }
            }

            return false;
        }

        public int GetHashCode([DisallowNull] RepairOrderModel item)
        {
            return item.Id.GetHashCode();
        }
    }

    public class ServiceHazmatComparer : IEqualityComparer<ServiceHazmatModel>
    {
        public bool Equals([AllowNull] ServiceHazmatModel x, [AllowNull] ServiceHazmatModel y)
        {
            return string.Equals(x.Id, y.Id) && string.Equals(x.ServiceID, y.ServiceID);
        }

        public int GetHashCode([DisallowNull] ServiceHazmatModel item)
        {
            return item.Id.GetHashCode();
        }
    }

    public class ServiceInspectionComparer : IEqualityComparer<ServiceInspectionModel>
    {
        public bool Equals([AllowNull] ServiceInspectionModel x, [AllowNull] ServiceInspectionModel y)
        {
            return string.Equals(x.Id, y.Id) && string.Equals(x.ServiceID, y.ServiceID);
        }

        public int GetHashCode([DisallowNull] ServiceInspectionModel item)
        {
            return item.Id.GetHashCode();
        }
    }

    public class ServiceLaborComparer : IEqualityComparer<ServiceLaborModel>
    {
        public bool Equals([AllowNull] ServiceLaborModel x, [AllowNull] ServiceLaborModel y)
        {
            return string.Equals(x.Id, y.Id) && string.Equals(x.ServiceID, y.ServiceID);
        }

        public int GetHashCode([DisallowNull] ServiceLaborModel item)
        {
            return item.Id.GetHashCode();
        }
    }

    public class ServicePartComparer : IEqualityComparer<ServicePartModel>
    {
        public bool Equals([AllowNull] ServicePartModel x, [AllowNull] ServicePartModel y)
        {
            return string.Equals(x.Id, y.Id) && string.Equals(x.ServiceID, y.ServiceID);
        }

        public int GetHashCode([DisallowNull] ServicePartModel item)
        {
            return item.Id.GetHashCode();
        }
    }

    public class ServiceComparer : IEqualityComparer<ServiceModel>
    {
        public bool Equals([AllowNull] ServiceModel x, [AllowNull] ServiceModel y)
        {
            return string.Equals(x.Id, y.Id);
        }

        public int GetHashCode([DisallowNull] ServiceModel item)
        {
            return item.Id.GetHashCode();
        }
    }

    public class ServiceSubletComparer : IEqualityComparer<ServiceSubletModel>
    {
        public bool Equals([AllowNull] ServiceSubletModel x, [AllowNull] ServiceSubletModel y)
        {
            return string.Equals(x.Id, y.Id) && string.Equals(x.ServiceID, y.ServiceID);
        }

        public int GetHashCode([DisallowNull] ServiceSubletModel item)
        {
            return item.Id.GetHashCode();
        }
    }

    public class VehicleComparer : IEqualityComparer<VehicleModel>
    {
        public bool Equals([AllowNull] VehicleModel x, [AllowNull] VehicleModel y)
        {
            return string.Equals(x.Id, y.Id) && string.Equals(x.CustomerId, y.CustomerId);
        }

        public int GetHashCode([DisallowNull] VehicleModel item)
        {
            return item.Id.GetHashCode();
        }
    }
}
