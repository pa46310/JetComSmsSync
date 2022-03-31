using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JetComSmsSync.Modules.Protractor.Models
{
    

    [XmlRoot(ElementName = "CRMDataSet")]
    public class CRMDataSet
    {
        [XmlElement(ElementName = "Header")]
        public CRMDataSetHeader Header;

        [XmlElement(ElementName = "Contacts")]
        public Contacts Contacts;

        [XmlElement(ElementName = "ServiceItems")]
        public ServiceItems ServiceItems;

        [XmlElement(ElementName = "Invoices")]
        public Invoices Invoices;

        [XmlElement(ElementName = "Appointments")]
        public Appointments Appointments;
    }

    [XmlRoot(ElementName = "Header")]
    public class CRMDataSetHeader
    {

        [XmlElement(ElementName = "ErrorNumber")]
        public string ErrorNumber;

        [XmlElement(ElementName = "ErrorMessage")]
        public string ErrorMessage;
    }

    [XmlRoot(ElementName = "Contacts")]
    public class Contacts
    {

        [XmlElement(ElementName = "Item")]
        public List<ContactItem> Item;
    }

    [XmlRoot(ElementName = "Header")]
    public class Header
    {

        [XmlElement(ElementName = "ID")]
        public string ID;

        [XmlElement(ElementName = "CreationTime")]
        public DateTime CreationTime;

        [XmlElement(ElementName = "DeletionTime")]
        public DateTime DeletionTime;

        [XmlElement(ElementName = "LastModifiedTime")]
        public DateTime LastModifiedTime;

        [XmlElement(ElementName = "LastModifiedBy")]
        public string LastModifiedBy;
    }

    [XmlRoot(ElementName = "Name")]
    public class Name
    {

        [XmlElement(ElementName = "Title")]
        public string Title;

        [XmlElement(ElementName = "Prefix")]
        public object Prefix;

        [XmlElement(ElementName = "FirstName")]
        public string FirstName;

        [XmlElement(ElementName = "MiddleName")]
        public object MiddleName;

        [XmlElement(ElementName = "LastName")]
        public string LastName;

        [XmlElement(ElementName = "Suffix")]
        public object Suffix;
    }

    [XmlRoot(ElementName = "Address")]
    public class Address
    {

        [XmlElement(ElementName = "Title")]
        public string Title;

        [XmlElement(ElementName = "Street")]
        public string Street;

        [XmlElement(ElementName = "City")]
        public string City;

        [XmlElement(ElementName = "Province")]
        public string Province;

        [XmlElement(ElementName = "PostalCode")]
        public int PostalCode;

        [XmlElement(ElementName = "Country")]
        public string Country;
    }

    [XmlRoot(ElementName = "Item")]
    public class ContactItem
    {

        [XmlElement(ElementName = "Address")]
        public Address Address;

        [XmlElement(ElementName = "Company")]
        public string Company;

        [XmlElement(ElementName = "Phone1Title")]
        public string Phone1Title;

        [XmlElement(ElementName = "Phone1")]
        public string Phone1;

        [XmlElement(ElementName = "Phone2Title")]
        public object Phone2Title;

        [XmlElement(ElementName = "Phone2")]
        public object Phone2;

        [XmlElement(ElementName = "EmailTitle")]
        public string EmailTitle;

        [XmlElement(ElementName = "Email")]
        public string Email;

        [XmlElement(ElementName = "AdditionalURIs")]
        public AdditionalURIs AdditionalURIs;

        [XmlElement(ElementName = "AdditionalPhones")]
        public AdditionalPhones AdditionalPhones;

        [XmlElement(ElementName = "MarketingSource")]
        public string MarketingSource;

        [XmlElement(ElementName = "Note")]
        public object Note;

        [XmlElement(ElementName = "NoMessaging")]
        public bool NoMessaging;

        [XmlElement(ElementName = "NoEmail")]
        public bool NoEmail;

        [XmlElement(ElementName = "NoPostCard")]
        public bool NoPostCard;
    }

    [XmlRoot(ElementName = "AdditionalURIs")]
    public class AdditionalURIs
    {

        [XmlElement(ElementName = "Item")]
        public ContactItem Item;
    }

    [XmlRoot(ElementName = "AdditionalPhones")]
    public class AdditionalPhones
    {

        [XmlElement(ElementName = "Item")]
        public ContactItem Item;
    }

    [XmlRoot(ElementName = "ServiceItems")]
    public class ServiceItems
    {

        [XmlElement(ElementName = "Item")]
        public List<ServiceItem> Item;
    }

    [XmlRoot(ElementName = "Item")]
    public class ServiceItem
    {

        [XmlElement(ElementName = "Header")]
        public Header Header;

        [XmlElement(ElementName = "ID")]
        public string ID;

        [XmlElement(ElementName = "Type")]
        public string Type;

        [XmlElement(ElementName = "Lookup")]
        public string Lookup;

        [XmlElement(ElementName = "Description")]
        public string Description;

        [XmlElement(ElementName = "Usage")]
        public int Usage;

        [XmlElement(ElementName = "ProductionDate")]
        public DateTime ProductionDate;

        [XmlElement(ElementName = "Note")]
        public object Note;

        [XmlElement(ElementName = "NoEmail")]
        public bool NoEmail;

        [XmlElement(ElementName = "NoPostCard")]
        public bool NoPostCard;

        [XmlElement(ElementName = "OwnerID")]
        public string OwnerID;

        [XmlElement(ElementName = "PlateRegistration")]
        public string PlateRegistration;

        [XmlElement(ElementName = "VIN")]
        public string VIN;

        [XmlElement(ElementName = "Unit")]
        public object Unit;

        [XmlElement(ElementName = "Color")]
        public object Color;

        [XmlElement(ElementName = "Year")]
        public int Year;

        [XmlElement(ElementName = "Make")]
        public string Make;

        [XmlElement(ElementName = "Model")]
        public string Model;

        [XmlElement(ElementName = "Submodel")]
        public string Submodel;

        [XmlElement(ElementName = "Engine")]
        public string Engine;
    }

    [XmlRoot(ElementName = "Invoices")]
    public class Invoices
    {

        [XmlElement(ElementName = "Item")]
        public List<InvoiceItem> Item;
    }

    [XmlRoot(ElementName = "Item")]
    public class InvoiceItem
    {

        [XmlElement(ElementName = "Header")]
        public Header Header;

        [XmlElement(ElementName = "ID")]
        public string ID;

        [XmlElement(ElementName = "Rank")]
        public int Rank;

        [XmlElement(ElementName = "Type")]
        public string Type;

        [XmlElement(ElementName = "Description")]
        public string Description;

        [XmlElement(ElementName = "Quantity")]
        public int Quantity;

        [XmlElement(ElementName = "Unit")]
        public string Unit;

        [XmlElement(ElementName = "Price")]
        public double Price;

        [XmlElement(ElementName = "PriceUnit")]
        public string PriceUnit;

        [XmlElement(ElementName = "Total")]
        public double Total;

        [XmlElement(ElementName = "Discount")]
        public double Discount;

        [XmlElement(ElementName = "ExtendedTotal")]
        public double ExtendedTotal;

        [XmlElement(ElementName = "TotalCost")]
        public double TotalCost;

        [XmlElement(ElementName = "OtherChargeCode")]
        public string OtherChargeCode;

        [XmlElement(ElementName = "Technician")]
        public string Technician;

        [XmlElement(ElementName = "ServiceAdvisor")]
        public string ServiceAdvisor;

        [XmlElement(ElementName = "LineCode")]
        public object LineCode;

        [XmlElement(ElementName = "PartNumber")]
        public string PartNumber;

        [XmlElement(ElementName = "Manufacturer")]
        public object Manufacturer;

        [XmlElement(ElementName = "UnitCoreValue")]
        public double UnitCoreValue;

        [XmlElement(ElementName = "TotalCoreValue")]
        public double TotalCoreValue;

        [XmlElement(ElementName = "CoreStatus")]
        public string CoreStatus;

        [XmlElement(ElementName = "Chapter")]
        public string Chapter;

        [XmlElement(ElementName = "Code")]
        public string Code;

        [XmlElement(ElementName = "Title")]
        public string Title;

        [XmlElement(ElementName = "ServicePackageLines")]
        public ServicePackageLines ServicePackageLines;

        [XmlElement(ElementName = "ServiceCategory")]
        public ServiceCategory ServiceCategory;

        [XmlElement(ElementName = "WorkOrderID")]
        public string WorkOrderID;

        [XmlElement(ElementName = "Name")]
        public string Name;

        [XmlElement(ElementName = "Value")]
        public DateTime Value;

        [XmlElement(ElementName = "PaymentMethod")]
        public string PaymentMethod;

        [XmlElement(ElementName = "Amount")]
        public double Amount;

        [XmlElement(ElementName = "ScheduledTime")]
        public DateTime ScheduledTime;

        [XmlElement(ElementName = "PromisedTime")]
        public DateTime PromisedTime;

        [XmlElement(ElementName = "InvoiceTime")]
        public DateTime InvoiceTime;

        [XmlElement(ElementName = "WorkOrderNumber")]
        public int WorkOrderNumber;

        [XmlElement(ElementName = "InvoiceNumber")]
        public int InvoiceNumber;

        [XmlElement(ElementName = "PurchaseOrderNumber")]
        public object PurchaseOrderNumber;

        [XmlElement(ElementName = "ContactID")]
        public string ContactID;

        [XmlElement(ElementName = "ServiceItemID")]
        public string ServiceItemID;

        [XmlElement(ElementName = "InUsage")]
        public int InUsage;

        [XmlElement(ElementName = "OutUsage")]
        public int OutUsage;

        [XmlElement(ElementName = "Note")]
        public object Note;

        [XmlElement(ElementName = "ServicePackages")]
        public ServicePackages ServicePackages;

        [XmlElement(ElementName = "DeferredServicePackages")]
        public object DeferredServicePackages;

        [XmlElement(ElementName = "Summary")]
        public Summary Summary;

        [XmlElement(ElementName = "Payments")]
        public Payments Payments;

        [XmlElement(ElementName = "LocationID")]
        public string LocationID;
    }

    [XmlRoot(ElementName = "ServicePackageLines")]
    public class ServicePackageLines
    {

        [XmlElement(ElementName = "Item")]
        public InvoiceItem Item;
    }

    [XmlRoot(ElementName = "ServiceCategory")]
    public class ServiceCategory
    {

        [XmlElement(ElementName = "ID")]
        public string ID;

        [XmlElement(ElementName = "Rank")]
        public int Rank;

        [XmlElement(ElementName = "Name")]
        public string Name;
    }

    [XmlRoot(ElementName = "ServicePackages")]
    public class ServicePackages
    {

        [XmlElement(ElementName = "Item")]
        public InvoiceItem Item;
    }

    [XmlRoot(ElementName = "OtherCharges")]
    public class OtherCharges
    {

        [XmlElement(ElementName = "Item")]
        public InvoiceItem Item;
    }

    [XmlRoot(ElementName = "Summary")]
    public class Summary
    {

        [XmlElement(ElementName = "PartsTotal")]
        public double PartsTotal;

        [XmlElement(ElementName = "LaborTotal")]
        public double LaborTotal;

        [XmlElement(ElementName = "SubletTotal")]
        public double SubletTotal;

        [XmlElement(ElementName = "NetTotal")]
        public double NetTotal;

        [XmlElement(ElementName = "OtherCharges")]
        public OtherCharges OtherCharges;

        [XmlElement(ElementName = "GrandTotal")]
        public double GrandTotal;
    }

    [XmlRoot(ElementName = "Payments")]
    public class Payments
    {

        [XmlElement(ElementName = "Item")]
        public InvoiceItem Item;
    }

    [XmlRoot(ElementName = "Appointments")]
    public class Appointments
    {
        [XmlElement(ElementName = "Item")]
        public List<AppointmentItem> Item;
    }

    [XmlRoot(ElementName = "Item")]
    public class AppointmentItem
    {

        [XmlElement(ElementName = "Header")]
        public Header Header;

        [XmlElement(ElementName = "ID")]
        public string ID;

        [XmlElement(ElementName = "Type")]
        public string Type;

        [XmlElement(ElementName = "ScheduledTime")]
        public DateTime ScheduledTime;

        [XmlElement(ElementName = "PromisedTime")]
        public DateTime PromisedTime;

        [XmlElement(ElementName = "InvoiceTime")]
        public DateTime InvoiceTime;

        [XmlElement(ElementName = "WorkOrderNumber")]
        public int WorkOrderNumber;

        [XmlElement(ElementName = "InvoiceNumber")]
        public int InvoiceNumber;

        [XmlElement(ElementName = "PurchaseOrderNumber")]
        public object PurchaseOrderNumber;

        [XmlElement(ElementName = "ContactID")]
        public string ContactID;

        [XmlElement(ElementName = "ServiceItemID")]
        public string ServiceItemID;

        [XmlElement(ElementName = "Technician")]
        public object Technician;

        [XmlElement(ElementName = "ServiceAdvisor")]
        public object ServiceAdvisor;

        [XmlElement(ElementName = "InUsage")]
        public int InUsage;

        [XmlElement(ElementName = "OutUsage")]
        public int OutUsage;

        [XmlElement(ElementName = "Note")]
        public string Note;

        [XmlElement(ElementName = "ServicePackages")]
        public object ServicePackages;

        [XmlElement(ElementName = "DeferredServicePackages")]
        public object DeferredServicePackages;

        [XmlElement(ElementName = "Payments")]
        public object Payments;

        [XmlElement(ElementName = "OtherChargeCode")]
        public object OtherChargeCode;

        [XmlElement(ElementName = "LocationID")]
        public string LocationID;
    }
}
