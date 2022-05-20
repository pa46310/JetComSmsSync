using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JetComSmsSync.Modules.Protractor.Models
{
    public static class Comparers
    {
        public static ContactComparer Contact { get; } = new ContactComparer();
        public static InvoiceComparer Invoice { get; } = new InvoiceComparer();
        public static ServiceItemComparer ServiceItem { get; } = new ServiceItemComparer();
        public static ServicePackageComparer ServicePackage { get; } = new ServicePackageComparer();
        public static AppointmentComparer Appointment { get; } = new AppointmentComparer();
    }

    public class AppointmentComparer : IEqualityComparer<AppointmentModel>
    {
        public bool Equals([AllowNull] AppointmentModel x, [AllowNull] AppointmentModel y)
        {
            return Equals(x.ID, y.ID);
        }

        public int GetHashCode([DisallowNull] AppointmentModel item)
        {
            return item.ID.GetHashCode();
        }
    }

    public class ServicePackageComparer : IEqualityComparer<ServicePackagesModel>
    {
        public bool Equals([AllowNull] ServicePackagesModel x, [AllowNull] ServicePackagesModel y)
        {
            return Equals(x.ID, y.ID) && Equals(x.ServicePackagesID, y.ServicePackagesID);
        }

        public int GetHashCode([DisallowNull] ServicePackagesModel item)
        {
            return item.ID.GetHashCode();
        }
    }

    public class ServiceItemComparer : IEqualityComparer<ServiceItemModel>
    {
        public bool Equals([AllowNull] ServiceItemModel x, [AllowNull] ServiceItemModel y)
        {
            return Equals(x.ID, y.ID);
        }

        public int GetHashCode([DisallowNull] ServiceItemModel item)
        {
            return item.ID.GetHashCode();
        }
    }

    public class InvoiceComparer : IEqualityComparer<InvoiceModel>
    {
        public bool Equals([AllowNull] InvoiceModel x, [AllowNull] InvoiceModel y)
        {
            return Equals(x.ID, y.ID) && Equals(x.ServiceItemID, y.ServiceItemID);
        }

        public int GetHashCode([DisallowNull] InvoiceModel item)
        {
            return item.ID.GetHashCode();
        }
    }

    public class ContactComparer : IEqualityComparer<ContactModel>
    {
        public bool Equals([AllowNull] ContactModel x, [AllowNull] ContactModel y)
        {
            return Equals(x.ID, y.ID);
        }

        public int GetHashCode([DisallowNull] ContactModel item)
        {
            return item.ID.GetHashCode();
        }
    }
}
