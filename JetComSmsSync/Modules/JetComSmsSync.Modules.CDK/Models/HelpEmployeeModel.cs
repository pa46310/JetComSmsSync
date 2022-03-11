using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JetComSmsSync.Modules.CDK.Models
{
    public class HelpEmployeeModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class HelpEmployeeModelComparer : IEqualityComparer<HelpEmployeeModel>
    {
        public bool Equals([AllowNull] HelpEmployeeModel x, [AllowNull] HelpEmployeeModel y)
        {
            return x.Id.Equals(y.Id);
        }

        public int GetHashCode([DisallowNull] HelpEmployeeModel item)
        {
            return item.Id.GetHashCode();
        }
    }

    public static class CdkComparers
    {
        public static HelpEmployeeModelComparer HelpEmployeeComparer { get; } = new HelpEmployeeModelComparer();
        public static CustomerModelComparer CustomerComparer { get; } = new CustomerModelComparer();
        public static SRODModelComparer SRODComparer { get; } = new SRODModelComparer();
    }
}
