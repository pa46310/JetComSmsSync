namespace JetComSmsSync.Core.Models
{
    public class RecurrenceModel
    {
        public static RecurrenceModel[] Default { get; } = new[]
        {
            new RecurrenceModel("1 day", 86400_000),
            new RecurrenceModel("12 hour", 43200_000),
            new RecurrenceModel("6 hour", 21600_000),
            new RecurrenceModel("2 hour", 7200_000),
            new RecurrenceModel("1 hour", 3600_000),
            new RecurrenceModel("1 minute", 60_000),
            new RecurrenceModel("Continuos", 0),
        };
        public string DisplayName { get; set; }
        public int Millisecond { get; set; }

        public RecurrenceModel(string displayName, int millisecond)
        {
            DisplayName = displayName;
            Millisecond = millisecond;
        }
    }
}
