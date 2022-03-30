using System.Collections.Generic;

namespace JetComSmsSync.Core.Models
{
    public partial class RecurrenceModel
    {
        public class PaginatedData<T>
        {
            public IEnumerable<T> Data { get; set; }
            public int Total { get; set; }
            public int Limit { get; set; }
            public int Offset { get; set; }
            public int NextOffset { get; set; }
        }
    }
}
