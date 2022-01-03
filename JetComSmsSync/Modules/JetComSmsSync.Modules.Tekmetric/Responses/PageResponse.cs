using System.Collections.Generic;

namespace JetComSmsSync.Modules.Tekmetric.Responses
{
    public class PageResponse<T>
    {
        public List<T> Content { get; set; }
        public Pageable Pageable { get; set; }
        public bool Last { get; set; }
        public int TotalPages { get; set; }
        public int TotalElements { get; set; }
        public Sort Sort { get; set; }
        public int NumberOfElements { get; set; }
        public bool First { get; set; }
        public int Size { get; set; }
        public int Number { get; set; }
        public bool Empty { get; set; }
    }
}
