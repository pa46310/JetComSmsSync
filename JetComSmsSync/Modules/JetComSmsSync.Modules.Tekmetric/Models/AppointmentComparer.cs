using JetComSmsSync.Modules.Tekmetric.Responses;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JetComSmsSync.Modules.Tekmetric.Models
{
    public class AppointmentComparer : IEqualityComparer<ContentAppointment>
    {
        public bool Equals([AllowNull] ContentAppointment x, [AllowNull] ContentAppointment y)
        {
            if (x is null && y is null) return true;

            if (x is null || y is null) return false;

            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] ContentAppointment obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
