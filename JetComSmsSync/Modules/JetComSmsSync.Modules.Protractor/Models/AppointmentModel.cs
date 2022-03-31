namespace JetComSmsSync.Modules.Protractor.Models
{
    public class AppointmentModel
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
        public string PurchaseOrderNumber { get; set; }
        public string ContactID { get; set; }
        public string ServiceItemID { get; set; }
        public string Technician { get; set; }
        public string ServiceAdvisor { get; set; }
        public string InUsage { get; set; }
        public string Note { get; set; }
        public string ServicePackages { get; set; }
        public string DeferredServicePackages { get; set; }
        public string OtherChargeCode { get; set; }
        public string LocationID { get; set; }
        public string BigID { get; set; }
    }
}
