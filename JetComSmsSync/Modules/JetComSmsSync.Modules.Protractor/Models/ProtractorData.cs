using System.Collections.Generic;

namespace JetComSmsSync.Modules.Protractor.Models
{
    public class ProtractorData
    {
        public List<ContactModel> Contacts { get; set; } = new List<ContactModel>();
        public List<InvoiceModel> Invoices { get; set; } = new List<InvoiceModel>();
        public List<ServiceItemModel> ServiceItems { get; set; } = new List<ServiceItemModel>();
        public List<ServicePackagesModel> ServicePackages { get; set; } = new List<ServicePackagesModel>();
        public List<AppointmentModel> Appointments { get; set; } = new List<AppointmentModel>();
    }
}
