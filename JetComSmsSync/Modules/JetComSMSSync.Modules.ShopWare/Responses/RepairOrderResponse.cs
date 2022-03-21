using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JetComSMSSync.Modules.ShopWare.Responses
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Labor
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("technician_id")]
        public string TechnicianId { get; set; }

        [JsonProperty("taxable")]
        public bool Taxable { get; set; }

        [JsonProperty("hours")]
        public string Hours { get; set; }
    }

    public class Part
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("quoted_price_cents")]
        public string QuotedPriceCents { get; set; }

        [JsonProperty("cost_cents")]
        public string CostCents { get; set; }

        [JsonProperty("part_inventory_id")]
        public string PartInventoryId { get; set; }

        [JsonProperty("taxable")]
        public bool Taxable { get; set; }

        [JsonProperty("quantity")]
        public string Quantity { get; set; }
    }

    public class Hazmat
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("fee_cents")]
        public string FeeCents { get; set; }

        [JsonProperty("taxable")]
        public bool Taxable { get; set; }

        [JsonProperty("quantity")]
        public string Quantity { get; set; }
    }

    public class Sublet
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price_cents")]
        public string PriceCents { get; set; }

        [JsonProperty("cost_cents")]
        public string CostCents { get; set; }

        [JsonProperty("provider")]
        public string Provider { get; set; }

        [JsonProperty("invoice_number")]
        public string InvoiceNumber { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("taxable")]
        public bool Taxable { get; set; }

        [JsonProperty("vendor_id")]
        public string VendorId { get; set; }

        [JsonProperty("invoice_date")]
        public string InvoiceDate { get; set; }
    }

    public class Inspection
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }
    }

    public class Service
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("category_id")]
        public string CategoryId { get; set; }

        [JsonProperty("canned_job_id")]
        public string CannedJobId { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("labor_rate_cents")]
        public string LaborRateCents { get; set; }

        [JsonProperty("completed_at")]
        public string CompletedAt { get; set; }

        [JsonProperty("last_completed_at")]
        public string LastCompletedAt { get; set; }

        [JsonProperty("labors")]
        public List<Labor> Labors { get; set; }

        [JsonProperty("parts")]
        public List<Part> Parts { get; set; }

        [JsonProperty("hazmats")]
        public List<Hazmat> Hazmats { get; set; }

        [JsonProperty("sublets")]
        public List<Sublet> Sublets { get; set; }

        [JsonProperty("inspections")]
        public List<Inspection> Inspections { get; set; }
    }

    public class PaymentTypeDetails
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Payment
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("payment_type")]
        public string PaymentType { get; set; }

        [JsonProperty("payment_type_details")]
        public PaymentTypeDetails PaymentTypeDetails { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("amount_cents")]
        public string AmountCents { get; set; }
    }

    public class stringegratorTag
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("taggable_type")]
        public string TaggableType { get; set; }

        [JsonProperty("taggable_id")]
        public string TaggableId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Label
    {
        [JsonProperty("text")]
        public object Text { get; set; }

        [JsonProperty("color_code")]
        public object ColorCode { get; set; }
    }

    public class RepairOrderResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("odometer")]
        public string Odometer { get; set; }

        [JsonProperty("odometer_out")]
        public string OdometerOut { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("technician_id")]
        public string TechnicianId { get; set; }

        [JsonProperty("advisor_id")]
        public string AdvisorId { get; set; }

        [JsonProperty("vehicle_id")]
        public string VehicleId { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }

        [JsonProperty("preferred_contact_type")]
        public string PreferredContactType { get; set; }

        [JsonProperty("part_discount_cents")]
        public string PartDiscountCents { get; set; }

        [JsonProperty("labor_discount_cents")]
        public string LaborDiscountCents { get; set; }

        [JsonProperty("shop_id")]
        public string ShopId { get; set; }

        [JsonProperty("status_id")]
        public string StatusId { get; set; }

        [JsonProperty("taxable")]
        public bool Taxable { get; set; }

        [JsonProperty("customer_source")]
        public string CustomerSource { get; set; }

        [JsonProperty("supply_fee_cents")]
        public string SupplyFeeCents { get; set; }

        [JsonProperty("part_discount_percentage")]
        public string PartDiscountPercentage { get; set; }

        [JsonProperty("labor_discount_percentage")]
        public string LaborDiscountPercentage { get; set; }

        [JsonProperty("started_at")]
        public string StartedAt { get; set; }

        [JsonProperty("closed_at")]
        public string ClosedAt { get; set; }

        [JsonProperty("picked_up_at")]
        public string PickedUpAt { get; set; }

        [JsonProperty("due_in_at")]
        public string DueInAt { get; set; }

        [JsonProperty("due_out_at")]
        public string DueOutAt { get; set; }

        [JsonProperty("part_tax_rate")]
        public string PartTaxRate { get; set; }

        [JsonProperty("labor_tax_rate")]
        public string LaborTaxRate { get; set; }

        [JsonProperty("hazmat_tax_rate")]
        public string HazmatTaxRate { get; set; }

        [JsonProperty("sublet_tax_rate")]
        public string SubletTaxRate { get; set; }

        [JsonProperty("services")]
        public List<Service> Services { get; set; }

        [JsonProperty("payments")]
        public List<Payment> Payments { get; set; }

        [JsonProperty("integrator_tags")]
        public List<stringegratorTag> IntegratorTags { get; set; }

        [JsonProperty("label")]
        public Label Label { get; set; }
    }




}
