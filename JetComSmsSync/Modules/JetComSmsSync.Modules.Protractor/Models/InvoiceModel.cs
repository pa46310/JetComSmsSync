namespace JetComSmsSync.Modules.Protractor.Models
{
    public class InvoiceModel
    {
        public string ID { get; set; }
        public string CreationTime { get; set; }
        public string LastModifiedTime { get; set; }
        public string Type { get; set; }
        public string ScheduledTime { get; set; }
        public string PromisedTime { get; set; }
        public string InvoiceTime { get; set; }
        public string WorkOrderNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public string ContactID { get; set; }
        public string ServiceAdvisor { get; set; }
        public string PartsTotal { get; set; }
        public string LaborTotal { get; set; }
        public string SubletTotal { get; set; }
        public string NetTotal { get; set; }
        public string WorkOrderID { get; set; }
        public string GrandTotal { get; set; }
        public string LocationID { get; set; }
        public string Discount { get; set; }
        public string BigID { get; set; }
    }
}
