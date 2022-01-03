using JetComSmsSync.Modules.Tekmetric.Responses;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JetComSmsSync.Modules.Tekmetric.Models
{
    public class VehicleComparer : IEqualityComparer<ContentVehicle>
    {
        public bool Equals([AllowNull] ContentVehicle x, [AllowNull] ContentVehicle y)
        {
            if (x is null && y is null) return true;

            if (x is null || y is null) return false;

            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] ContentVehicle obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
