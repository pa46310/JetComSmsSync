namespace JetComSmsSync.Modules.Tekmetric.Responses
{
    public class ContentLabor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rate { get; set; }
        public double Hours { get; set; }
        public bool Complete { get; set; }
        public int JobId { get; set; }
        public string BigID { get; set; }
    }
}
