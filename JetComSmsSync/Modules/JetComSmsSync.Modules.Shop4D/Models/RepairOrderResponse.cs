using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JetComSmsSync.Modules.Shop4D.Models
{
    public class CustomerAddress
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }

    public class Contact
    {
        public string CustomerId { get; set; }
        public string BigID { get; set; }

        public string ContactData { get; set; }
        public string ContactType { get; set; }
        public string Other { get; set; }
    }

    public class Customer
    {
        public string BigID { get; set; }

        public string CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerBusinessName { get; set; }
        public CustomerAddress CustomerAddress { get; set; }
        public List<Contact> Contact { get; set; }

        public string Address => CustomerAddress?.Address;
        public string City => CustomerAddress?.City;
        public string State => CustomerAddress?.State;
        public string Zip => CustomerAddress?.Zip;
    }

    public class Vehicle
    {
        public string CustomerId { get; set; }
        public string BigID { get; set; }

        public string VehicleId { get; set; }
        public string Vin { get; set; }
        public string Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string License { get; set; }
        public string Mileage { get; set; }
    }

    public class Sales
    {
        public decimal? Parts { get; set; }
        public decimal? Labor { get; set; }
        public decimal? Sublet { get; set; }
    }

    public class Labor
    {
        public string RoNumber { get; set; }
        public string BigID { get; set; }

        public string Description { get; set; }
        public string Hours { get; set; }
        public string Price { get; set; }
        public decimal Cost { get; set; }
        public string Approved { get; set; }
        public int CompletedOnFutureRO { get; set; }
        public string Technician { get; set; }
    }

    public class Part
    {
        public string RoNumber { get; set; }
        public string BigID { get; set; }

        public string Description { get; set; }
        public string Price { get; set; }
        public decimal Cost { get; set; }
        public string Quantity { get; set; }
    }

    public class LineItemDetail
    {
        public List<Labor> Labor { get; set; }
        public List<Part> Parts { get; set; }
    }

    public class RepairOrderInfo
    {
        public string BigID { get; set; }

        public string RoNumber { get; set; }
        public string ServiceWriter { get; set; }
        public string RoDate { get; set; }
        public string PayDate { get; set; }
        public string Source { get; set; }
        public string PartsOnly { get; set; }
        public Customer Customer { get; set; }
        public Vehicle Vehicle { get; set; }
        public Sales Sales { get; set; }
        public JToken Discounts { get; set; }
        public List<LineItemDetail> LineItemDetail { get; set; }

        public Discount Discount
        {
            get
            {
                try
                {
                    return Discounts.ToObject<Discount>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public string CustomerId => Customer?.CustomerId;
        public string VehicleId => Vehicle?.VehicleId;
        public decimal? Parts => Sales.Parts;
        public decimal? Labor => Sales.Labor;
        public decimal? DiscountParts => Discount?.Parts?.Sum();
        public decimal? DiscountLabor => Discount?.Labor?.Sum();

    }

    public class Discount
    {
        public List<decimal?> Labor { get; set; }
        public List<decimal?> Parts { get; set; }

    }

    public class RepairOrderData
    {
        public RepairOrderInfo RepairOrderInfo { get; set; }
    }

    public class RepairOrderResponse
    {
        public List<RepairOrderData> Success { get; set; }
        public string Error { get; set; }
        public bool IsUnAuthorized => string.Equals(Error, "Unauthorized access.", StringComparison.OrdinalIgnoreCase);

        public void UpdateList(string bigId)
        {
            foreach (var item in Success)
            {
                if (item.RepairOrderInfo.Customer.Contact is null)
                {
                    item.RepairOrderInfo.Customer.Contact = new List<Contact>();
                }
                // contact
                foreach (var contact in item.RepairOrderInfo.Customer.Contact)
                {
                    contact.CustomerId = item.RepairOrderInfo.Customer.CustomerId;
                    contact.BigID = bigId;
                }
                // customers
                item.RepairOrderInfo.Customer.BigID = bigId;
                // labors
                if (item.RepairOrderInfo.LineItemDetail is null)
                {
                    item.RepairOrderInfo.LineItemDetail = new List<LineItemDetail>();
                }
                foreach (var labor in item.RepairOrderInfo.LineItemDetail.SelectMany(x => x.Labor))
                {
                    labor.RoNumber = item.RepairOrderInfo.RoNumber;
                    labor.BigID = bigId;
                }
                // parts
                foreach (var part in item.RepairOrderInfo.LineItemDetail.SelectMany(x => x.Parts))
                {
                    part.RoNumber = item.RepairOrderInfo.RoNumber;
                    part.BigID = bigId;
                }
                // ro
                item.RepairOrderInfo.BigID = bigId;
                // vehicles
                if (item.RepairOrderInfo.Vehicle is null)
                {
                    item.RepairOrderInfo.Vehicle = new Vehicle();
                }
                item.RepairOrderInfo.Vehicle.BigID = bigId;
                item.RepairOrderInfo.Vehicle.CustomerId = item.RepairOrderInfo.Customer.CustomerId;
            }
        }
    }
}
