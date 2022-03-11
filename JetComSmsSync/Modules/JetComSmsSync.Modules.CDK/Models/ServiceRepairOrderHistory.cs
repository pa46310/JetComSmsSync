using System.Collections.Generic;
using System.Xml.Serialization;
namespace JetComSmsSync.Modules.CDK.Models
{
    [XmlRoot(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class V
    {
        [XmlAttribute(AttributeName = "Idx")]
        public string Idx { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "payPaymentAmount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PayPaymentAmount
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "EmailAddress", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class EmailAddress
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totSubletCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotSubletCost
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "EmailDesc", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class EmailDesc
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "hrsTimeCardHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class HrsTimeCardHours
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "lbrFlagHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LbrFlagHours
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "lbrSoldHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LbrSoldHours
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totForcedShopCharge", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotForcedShopCharge
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totLaborDiscount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotLaborDiscount
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totLaborSalePostDed", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotLaborSalePostDed
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totPartsDiscount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotPartsDiscount
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totPartsSalePostDed", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotPartsSalePostDed
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totRoTax", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotRoTax
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totShopChargeCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotShopChargeCost
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "hrsSequenceNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class HrsSequenceNo
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "PhoneNumber", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PhoneNumber
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totOtherHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotOtherHours
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totRoSalePostDed", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotRoSalePostDed
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totTimeCardHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotTimeCardHours
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "PhoneExt", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PhoneExt
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totCoreSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotCoreSale
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totFlagHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotFlagHours
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totPartsCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotPartsCount
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "lbrOtherHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LbrOtherHours
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "linAddOnFlag", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LinAddOnFlag
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totCoreCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotCoreCost
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totMiscCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotMiscCount
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totPayType", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotPayType
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "hrsMcdPercentage", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class HrsMcdPercentage
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "lbrLineCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LbrLineCode
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totPartsCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotPartsCost
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totSupp4Tax", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotSupp4Tax
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "lbrTechNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LbrTechNo
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "hrsTechNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class HrsTechNo
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totPartsSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotPartsSale
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "linCause", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LinCause
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totMiscSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotMiscSale
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "hrsOtherHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class HrsOtherHours
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "hrsSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class HrsSale
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "lbrOpCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LbrOpCode
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "linServiceRequest", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LinServiceRequest
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "lbrOpCodeDesc", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LbrOpCodeDesc
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "lbrTimeCardHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LbrTimeCardHours
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "linDispatchCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LinDispatchCode
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totLaborSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotLaborSale
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totLocalTax", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotLocalTax
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "linComplaintCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LinComplaintCode
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "payInsuranceFlag", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PayInsuranceFlag
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totLaborCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotLaborCost
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "hrsLaborType", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class HrsLaborType
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "lbrMcdPercentage", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LbrMcdPercentage
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "hrsHourType", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class HrsHourType
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "hrsPercentage", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class HrsPercentage
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "linComebackFlag", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LinComebackFlag
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "linLineCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LinLineCode
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totLubeSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotLubeSale
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totRoSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotRoSale
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totSoldHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotSoldHours
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totSubletSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotSubletSale
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "hrsSoldHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class HrsSoldHours
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "lbrCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LbrCost
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "lbrLaborType", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LbrLaborType
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "lbrSequenceNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LbrSequenceNo
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totShopChargeSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotShopChargeSale
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totSupp2Tax", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotSupp2Tax
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totSupp3Tax", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotSupp3Tax
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "lbrSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LbrSale
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "linEstDuration", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LinEstDuration
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totMiscCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotMiscCost
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totStateTax", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotStateTax
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "hrsCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class HrsCost
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "hrsLineCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class HrsLineCode
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "hrsActualHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class HrsActualHours
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "payPaymentCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PayPaymentCode
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "linBookerNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LinBookerNo
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totLubeCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotLubeCost
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "lbrComebackFlag", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LbrComebackFlag
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totLaborCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotLaborCount
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totRoCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotRoCost
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totLubeCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotLubeCount
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "hrsFlagHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class HrsFlagHours
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "lbrActualHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LbrActualHours
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "linActualWork", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LinActualWork
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "PhoneDesc", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PhoneDesc
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totActualHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotActualHours
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totDiscount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotDiscount
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "totSubletCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class TotSubletCount
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "service-repair-order-history", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class Servicerepairorderhistory
    {
        [XmlElement(ElementName = "payPaymentAmount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PayPaymentAmount PayPaymentAmount { get; set; }

        [XmlElement(ElementName = "disSequenceNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string DisSequenceNo { get; set; }

        [XmlElement(ElementName = "EmailAddress", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public EmailAddress EmailAddress { get; set; }

        [XmlElement(ElementName = "lbrComebackSA", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string LbrComebackSA { get; set; }

        [XmlElement(ElementName = "payCPTotal", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string PayCPTotal { get; set; }

        [XmlElement(ElementName = "lbrComebackRO", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string LbrComebackRO { get; set; }

        [XmlElement(ElementName = "disLevel", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string DisLevel { get; set; }

        [XmlElement(ElementName = "totSubletCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotSubletCost TotSubletCost { get; set; }

        [XmlElement(ElementName = "prtMultivalueCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string PrtMultivalueCount { get; set; }

        [XmlElement(ElementName = "disClassOrType", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string DisClassOrType { get; set; }

        [XmlElement(ElementName = "disLopSeqNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string DisLopSeqNo { get; set; }

        [XmlElement(ElementName = "disOverrideAmount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string DisOverrideAmount { get; set; }

        [XmlElement(ElementName = "disOverridePercent", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string DisOverridePercent { get; set; }

        [XmlElement(ElementName = "EmailDesc", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public EmailDesc EmailDesc { get; set; }
        [XmlElement(ElementName = "EmailMultivalueCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string EmailMultivalueCount { get; set; }
        [XmlElement(ElementName = "hrsTimeCardHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public HrsTimeCardHours HrsTimeCardHours { get; set; }
        [XmlElement(ElementName = "lbrFlagHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LbrFlagHours LbrFlagHours { get; set; }
        [XmlElement(ElementName = "lbrSoldHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LbrSoldHours LbrSoldHours { get; set; }
        [XmlElement(ElementName = "LicenseNumber", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string LicenseNumber { get; set; }
        [XmlElement(ElementName = "ModelDesc", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string ModelDesc { get; set; }
        [XmlElement(ElementName = "StatusDesc", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string StatusDesc { get; set; }
        [XmlElement(ElementName = "totForcedShopCharge", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotForcedShopCharge TotForcedShopCharge { get; set; }
        [XmlElement(ElementName = "totLaborDiscount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotLaborDiscount TotLaborDiscount { get; set; }
        [XmlElement(ElementName = "totLaborSalePostDed", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotLaborSalePostDed TotLaborSalePostDed { get; set; }
        [XmlElement(ElementName = "totPartsDiscount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotPartsDiscount TotPartsDiscount { get; set; }
        [XmlElement(ElementName = "totPartsSalePostDed", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotPartsSalePostDed TotPartsSalePostDed { get; set; }
        [XmlElement(ElementName = "totRoTax", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotRoTax TotRoTax { get; set; }
        [XmlElement(ElementName = "totShopChargeCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotShopChargeCost TotShopChargeCost { get; set; }
        [XmlElement(ElementName = "disLineCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string DisLineCode { get; set; }
        [XmlElement(ElementName = "disOriginalDiscount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string DisOriginalDiscount { get; set; }
        [XmlElement(ElementName = "hrsSequenceNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public HrsSequenceNo HrsSequenceNo { get; set; }
        [XmlElement(ElementName = "lbrMultivalueCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string LbrMultivalueCount { get; set; }
        [XmlElement(ElementName = "PhoneNumber", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PhoneNumber PhoneNumber { get; set; }
        [XmlElement(ElementName = "totMultivalueCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string TotMultivalueCount { get; set; }
        [XmlElement(ElementName = "totOtherHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotOtherHours TotOtherHours { get; set; }
        [XmlElement(ElementName = "totRoSalePostDed", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotRoSalePostDed TotRoSalePostDed { get; set; }
        [XmlElement(ElementName = "totTimeCardHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotTimeCardHours TotTimeCardHours { get; set; }
        [XmlElement(ElementName = "warFailedPartNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string WarFailedPartNo { get; set; }
        [XmlElement(ElementName = "warLaborSequenceNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string WarLaborSequenceNo { get; set; }
        [XmlElement(ElementName = "disAppliedBy", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string DisAppliedBy { get; set; }
        [XmlElement(ElementName = "disTotalDiscount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string DisTotalDiscount { get; set; }
        [XmlElement(ElementName = "HasWarrPayFlag", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string HasWarrPayFlag { get; set; }
        [XmlElement(ElementName = "hrsMultivalueCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string HrsMultivalueCount { get; set; }
        [XmlElement(ElementName = "lbrForcedShopCharge", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string LbrForcedShopCharge { get; set; }
        [XmlElement(ElementName = "LotLocation", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string LotLocation { get; set; }
        [XmlElement(ElementName = "PhoneExt", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PhoneExt PhoneExt { get; set; }
        [XmlElement(ElementName = "PhoneMultivalueCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string PhoneMultivalueCount { get; set; }
        [XmlElement(ElementName = "totCoreSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotCoreSale TotCoreSale { get; set; }
        [XmlElement(ElementName = "totFlagHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotFlagHours TotFlagHours { get; set; }
        [XmlElement(ElementName = "totPartsCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotPartsCount TotPartsCount { get; set; }
        [XmlElement(ElementName = "warClaimType", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string WarClaimType { get; set; }
        [XmlElement(ElementName = "disLaborDiscount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string DisLaborDiscount { get; set; }
        [XmlElement(ElementName = "BlockAutoMsg", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string BlockAutoMsg { get; set; }
        [XmlElement(ElementName = "disOverrideTarget", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string DisOverrideTarget { get; set; }
        [XmlElement(ElementName = "disPartsDiscount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string DisPartsDiscount { get; set; }
        [XmlElement(ElementName = "HasCustPayFlag", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string HasCustPayFlag { get; set; }
        [XmlElement(ElementName = "lbrOtherHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LbrOtherHours LbrOtherHours { get; set; }
        [XmlElement(ElementName = "linAddOnFlag", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LinAddOnFlag LinAddOnFlag { get; set; }
        [XmlElement(ElementName = "payMultivalueCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string PayMultivalueCount { get; set; }
        [XmlElement(ElementName = "totCoreCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotCoreCost TotCoreCost { get; set; }
        [XmlElement(ElementName = "totMiscCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotMiscCount TotMiscCount { get; set; }
        [XmlElement(ElementName = "totPayType", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotPayType TotPayType { get; set; }
        [XmlElement(ElementName = "warConditionCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string WarConditionCode { get; set; }
        [XmlElement(ElementName = "Zip", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string Zip { get; set; }
        [XmlElement(ElementName = "disDesc", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string DisDesc { get; set; }
        [XmlElement(ElementName = "HasIntPayFlag", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string HasIntPayFlag { get; set; }
        [XmlElement(ElementName = "hrsMcdPercentage", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public HrsMcdPercentage HrsMcdPercentage { get; set; }
        [XmlElement(ElementName = "lbrLineCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LbrLineCode LbrLineCode { get; set; }
        [XmlElement(ElementName = "totPartsCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotPartsCost TotPartsCost { get; set; }
        [XmlElement(ElementName = "totSupp4Tax", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotSupp4Tax TotSupp4Tax { get; set; }
        [XmlElement(ElementName = "lbrTechNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LbrTechNo LbrTechNo { get; set; }
        [XmlElement(ElementName = "hrsTechNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public HrsTechNo HrsTechNo { get; set; }
        [XmlElement(ElementName = "totPartsSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotPartsSale TotPartsSale { get; set; }
        [XmlElement(ElementName = "linCause", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LinCause LinCause { get; set; }
        [XmlElement(ElementName = "totMiscSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotMiscSale TotMiscSale { get; set; }
        [XmlElement(ElementName = "Address", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string Address { get; set; }
        [XmlElement(ElementName = "Cashier", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string Cashier { get; set; }
        [XmlElement(ElementName = "ComebackFlag", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string ComebackFlag { get; set; }
        [XmlElement(ElementName = "Name2", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string Name2 { get; set; }
        [XmlElement(ElementName = "RentalFlag", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string RentalFlag { get; set; }
        [XmlElement(ElementName = "ServiceAdvisor", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string ServiceAdvisor { get; set; }
        [XmlElement(ElementName = "hrsOtherHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public HrsOtherHours HrsOtherHours { get; set; }
        [XmlElement(ElementName = "hrsSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public HrsSale HrsSale { get; set; }
        [XmlElement(ElementName = "lbrOpCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LbrOpCode LbrOpCode { get; set; }
        [XmlElement(ElementName = "linServiceRequest", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LinServiceRequest LinServiceRequest { get; set; }
        [XmlElement(ElementName = "linStatusDesc", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string LinStatusDesc { get; set; }
        [XmlElement(ElementName = "prtCompLineCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string PrtCompLineCode { get; set; }
        [XmlElement(ElementName = "BookedDate", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string BookedDate { get; set; }
        [XmlElement(ElementName = "CustNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string CustNo { get; set; }
        [XmlElement(ElementName = "ErrorLevel", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string ErrorLevel { get; set; }
        [XmlElement(ElementName = "PostedDate", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string PostedDate { get; set; }
        [XmlElement(ElementName = "VoidedDate", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string VoidedDate { get; set; }
        [XmlElement(ElementName = "lbrOpCodeDesc", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LbrOpCodeDesc LbrOpCodeDesc { get; set; }
        [XmlElement(ElementName = "lbrTimeCardHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LbrTimeCardHours LbrTimeCardHours { get; set; }
        [XmlElement(ElementName = "linDispatchCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LinDispatchCode LinDispatchCode { get; set; }
        [XmlElement(ElementName = "totLaborSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotLaborSale TotLaborSale { get; set; }
        [XmlElement(ElementName = "totLocalTax", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotLocalTax TotLocalTax { get; set; }
        [XmlElement(ElementName = "ApptDate", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string ApptDate { get; set; }
        [XmlElement(ElementName = "ApptTime", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string ApptTime { get; set; }
        [XmlElement(ElementName = "EstComplDate", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string EstComplDate { get; set; }
        [XmlElement(ElementName = "MileageLastVisit", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string MileageLastVisit { get; set; }
        [XmlElement(ElementName = "PurchaseOrderNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string PurchaseOrderNo { get; set; }
        [XmlElement(ElementName = "Remarks", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string Remarks { get; set; }
        [XmlElement(ElementName = "Year", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string Year { get; set; }
        [XmlElement(ElementName = "linCampaignCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string LinCampaignCode { get; set; }
        [XmlElement(ElementName = "linComplaintCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LinComplaintCode LinComplaintCode { get; set; }
        [XmlElement(ElementName = "payInsuranceFlag", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PayInsuranceFlag PayInsuranceFlag { get; set; }
        [XmlElement(ElementName = "totLaborCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotLaborCost TotLaborCost { get; set; }
        [XmlElement(ElementName = "warFailureCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string WarFailureCode { get; set; }
        [XmlElement(ElementName = "hrsLaborType", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public HrsLaborType HrsLaborType { get; set; }
        [XmlElement(ElementName = "Model", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string Model { get; set; }
        [XmlElement(ElementName = "Name1", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string Name1 { get; set; }
        [XmlElement(ElementName = "disDebitControlNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string DisDebitControlNo { get; set; }
        [XmlElement(ElementName = "disUserID", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string DisUserID { get; set; }
        [XmlElement(ElementName = "lbrMcdPercentage", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LbrMcdPercentage LbrMcdPercentage { get; set; }
        [XmlElement(ElementName = "linStatusCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string LinStatusCode { get; set; }
        [XmlElement(ElementName = "prtOutsideSalesmanNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string PrtOutsideSalesmanNo { get; set; }
        [XmlElement(ElementName = "hrsHourType", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public HrsHourType HrsHourType { get; set; }
        [XmlElement(ElementName = "ApptFlag", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string ApptFlag { get; set; }
        [XmlElement(ElementName = "CityStateZip", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string CityStateZip { get; set; }
        [XmlElement(ElementName = "ContactEmailAddress", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string ContactEmailAddress { get; set; }
        [XmlElement(ElementName = "ErrorMessage", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string ErrorMessage { get; set; }
        [XmlElement(ElementName = "LastServiceDate", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string LastServiceDate { get; set; }
        [XmlElement(ElementName = "Make", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string Make { get; set; }
        [XmlElement(ElementName = "MakeDesc", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string MakeDesc { get; set; }
        [XmlElement(ElementName = "MileageOut", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string MileageOut { get; set; }
        [XmlElement(ElementName = "OrigWaiterFlag", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string OrigWaiterFlag { get; set; }
        [XmlElement(ElementName = "RONumber", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string RONumber { get; set; }
        [XmlElement(ElementName = "VehID", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string VehID { get; set; }
        [XmlElement(ElementName = "WaiterFlag", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string WaiterFlag { get; set; }
        [XmlElement(ElementName = "disDebitTargetCo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string DisDebitTargetCo { get; set; }
        [XmlElement(ElementName = "hrsPercentage", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public HrsPercentage HrsPercentage { get; set; }
        [XmlElement(ElementName = "linComebackFlag", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LinComebackFlag LinComebackFlag { get; set; }
        [XmlElement(ElementName = "linLineCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LinLineCode LinLineCode { get; set; }
        [XmlElement(ElementName = "prtSpecialStatus", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string PrtSpecialStatus { get; set; }
        [XmlElement(ElementName = "totLubeSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotLubeSale TotLubeSale { get; set; }
        [XmlElement(ElementName = "totRoSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotRoSale TotRoSale { get; set; }
        [XmlElement(ElementName = "totSoldHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotSoldHours TotSoldHours { get; set; }
        [XmlElement(ElementName = "totSubletSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotSubletSale TotSubletSale { get; set; }
        [XmlElement(ElementName = "ClosedDate", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string ClosedDate { get; set; }
        [XmlElement(ElementName = "EstComplTime", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string EstComplTime { get; set; }
        [XmlElement(ElementName = "PriorityValue", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string PriorityValue { get; set; }
        [XmlElement(ElementName = "PromisedTime", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string PromisedTime { get; set; }
        [XmlElement(ElementName = "VIN", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string VIN { get; set; }
        [XmlElement(ElementName = "hrsSoldHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public HrsSoldHours HrsSoldHours { get; set; }
        [XmlElement(ElementName = "lbrCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LbrCost LbrCost { get; set; }
        [XmlElement(ElementName = "lbrLaborType", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LbrLaborType LbrLaborType { get; set; }
        [XmlElement(ElementName = "lbrSequenceNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LbrSequenceNo LbrSequenceNo { get; set; }
        [XmlElement(ElementName = "totShopChargeSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotShopChargeSale TotShopChargeSale { get; set; }
        [XmlElement(ElementName = "totSupp2Tax", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotSupp2Tax TotSupp2Tax { get; set; }
        [XmlElement(ElementName = "totSupp3Tax", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotSupp3Tax TotSupp3Tax { get; set; }
        [XmlElement(ElementName = "AddOnFlag", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string AddOnFlag { get; set; }
        [XmlElement(ElementName = "Comments", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string Comments { get; set; }
        [XmlElement(ElementName = "ContactPhoneNumber", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string ContactPhoneNumber { get; set; }
        [XmlElement(ElementName = "DeliveryDate", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string DeliveryDate { get; set; }
        [XmlElement(ElementName = "HostItemID", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string HostItemID { get; set; }
        [XmlElement(ElementName = "Mileage", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string Mileage { get; set; }
        [XmlElement(ElementName = "OrigPromisedTime", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string OrigPromisedTime { get; set; }
        [XmlElement(ElementName = "SoldByDealerFlag", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string SoldByDealerFlag { get; set; }
        [XmlElement(ElementName = "lbrSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LbrSale LbrSale { get; set; }
        [XmlElement(ElementName = "linEstDuration", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LinEstDuration LinEstDuration { get; set; }
        [XmlElement(ElementName = "totMiscCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotMiscCost TotMiscCost { get; set; }
        [XmlElement(ElementName = "totStateTax", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotStateTax TotStateTax { get; set; }
        [XmlElement(ElementName = "hrsCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public HrsCost HrsCost { get; set; }
        [XmlElement(ElementName = "hrsLineCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public HrsLineCode HrsLineCode { get; set; }
        [XmlElement(ElementName = "BookerNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string BookerNo { get; set; }
        [XmlElement(ElementName = "OpenDate", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string OpenDate { get; set; }
        [XmlElement(ElementName = "OrigPromisedDate", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string OrigPromisedDate { get; set; }
        [XmlElement(ElementName = "PromisedDate", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string PromisedDate { get; set; }
        [XmlElement(ElementName = "SpecialCustFlag", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string SpecialCustFlag { get; set; }
        [XmlElement(ElementName = "TagNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string TagNo { get; set; }
        [XmlElement(ElementName = "VehicleColor", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string VehicleColor { get; set; }
        [XmlElement(ElementName = "disDebitAccountNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string DisDebitAccountNo { get; set; }
        [XmlElement(ElementName = "hrsActualHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public HrsActualHours HrsActualHours { get; set; }
        [XmlElement(ElementName = "punMultivalueCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string PunMultivalueCount { get; set; }
        [XmlElement(ElementName = "mlsMultivalueCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string MlsMultivalueCount { get; set; }
        [XmlElement(ElementName = "payPaymentCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PayPaymentCode PayPaymentCode { get; set; }
        [XmlElement(ElementName = "dedMultivalueCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string DedMultivalueCount { get; set; }
        [XmlElement(ElementName = "lbrOperationType", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string LbrOperationType { get; set; }
        [XmlElement(ElementName = "linBookerNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LinBookerNo LinBookerNo { get; set; }
        [XmlElement(ElementName = "payPaymentsMade", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string PayPaymentsMade { get; set; }
        [XmlElement(ElementName = "StatusCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string StatusCode { get; set; }
        [XmlElement(ElementName = "disOverrideGPPercent", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string DisOverrideGPPercent { get; set; }
        [XmlElement(ElementName = "totLubeCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotLubeCost TotLubeCost { get; set; }
        [XmlElement(ElementName = "warFailedPartsCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string WarFailedPartsCount { get; set; }
        [XmlElement(ElementName = "lbrComebackTech", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string LbrComebackTech { get; set; }
        [XmlElement(ElementName = "disDiscountID", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string DisDiscountID { get; set; }
        [XmlElement(ElementName = "lbrComebackFlag", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LbrComebackFlag LbrComebackFlag { get; set; }
        [XmlElement(ElementName = "disManagerOverride", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string DisManagerOverride { get; set; }
        [XmlElement(ElementName = "linMultivalueCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string LinMultivalueCount { get; set; }
        [XmlElement(ElementName = "OpenTime", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string OpenTime { get; set; }
        [XmlElement(ElementName = "rapMultivalueCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string RapMultivalueCount { get; set; }
        [XmlElement(ElementName = "totLaborCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotLaborCount TotLaborCount { get; set; }
        [XmlElement(ElementName = "totRoCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotRoCost TotRoCost { get; set; }
        [XmlElement(ElementName = "warMultivalueCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string WarMultivalueCount { get; set; }
        [XmlElement(ElementName = "disOverrideGPAmount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string DisOverrideGPAmount { get; set; }
        [XmlElement(ElementName = "feeMultivalueCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string FeeMultivalueCount { get; set; }
        [XmlElement(ElementName = "totLubeCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotLubeCount TotLubeCount { get; set; }
        [XmlElement(ElementName = "disMultivalueCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string DisMultivalueCount { get; set; }
        [XmlElement(ElementName = "hrsFlagHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public HrsFlagHours HrsFlagHours { get; set; }
        [XmlElement(ElementName = "lbrActualHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LbrActualHours LbrActualHours { get; set; }
        [XmlElement(ElementName = "linActualWork", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LinActualWork LinActualWork { get; set; }
        [XmlElement(ElementName = "PhoneDesc", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PhoneDesc PhoneDesc { get; set; }
        [XmlElement(ElementName = "totActualHours", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotActualHours TotActualHours { get; set; }
        [XmlElement(ElementName = "totDiscount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotDiscount TotDiscount { get; set; }
        [XmlElement(ElementName = "totSubletCount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public TotSubletCount TotSubletCount { get; set; }
        [XmlElement(ElementName = "BookedTime", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string BookedTime { get; set; }
        [XmlElement(ElementName = "prtLineCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PrtLineCode PrtLineCode { get; set; }
        [XmlElement(ElementName = "prtSequenceNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PrtSequenceNo PrtSequenceNo { get; set; }
        [XmlElement(ElementName = "prtEmployeeNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PrtEmployeeNo PrtEmployeeNo { get; set; }
        [XmlElement(ElementName = "prtDesc", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PrtDesc PrtDesc { get; set; }
        [XmlElement(ElementName = "prtQtyOrdered", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PrtQtyOrdered PrtQtyOrdered { get; set; }
        [XmlElement(ElementName = "prtList", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PrtList PrtList { get; set; }
        [XmlElement(ElementName = "prtUnitServiceCharge", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PrtUnitServiceCharge PrtUnitServiceCharge { get; set; }
        [XmlElement(ElementName = "prtMcdPercentage", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PrtMcdPercentage PrtMcdPercentage { get; set; }
        [XmlElement(ElementName = "prtCoreSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PrtCoreSale PrtCoreSale { get; set; }
        [XmlElement(ElementName = "prtLaborType", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PrtLaborType PrtLaborType { get; set; }
        [XmlElement(ElementName = "prtSource", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PrtSource PrtSource { get; set; }
        [XmlElement(ElementName = "prtCoreCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PrtCoreCost PrtCoreCost { get; set; }
        [XmlElement(ElementName = "prtQtySold", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PrtQtySold PrtQtySold { get; set; }
        [XmlElement(ElementName = "prtComp", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PrtComp PrtComp { get; set; }
        [XmlElement(ElementName = "prtCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PrtCost PrtCost { get; set; }
        [XmlElement(ElementName = "prtPartNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PrtPartNo PrtPartNo { get; set; }
        [XmlElement(ElementName = "prtSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PrtSale PrtSale { get; set; }
        [XmlElement(ElementName = "prtClass", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PrtClass PrtClass { get; set; }
        [XmlElement(ElementName = "prtExtendedCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PrtExtendedCost PrtExtendedCost { get; set; }
        [XmlElement(ElementName = "prtExtendedSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PrtExtendedSale PrtExtendedSale { get; set; }
        [XmlElement(ElementName = "prtLaborSequenceNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PrtLaborSequenceNo PrtLaborSequenceNo { get; set; }
        [XmlElement(ElementName = "prtQtyOnHand", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PrtQtyOnHand PrtQtyOnHand { get; set; }
        [XmlElement(ElementName = "prtQtyFilled", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PrtQtyFilled PrtQtyFilled { get; set; }
        [XmlElement(ElementName = "prtQtyBackordered", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PrtQtyBackordered PrtQtyBackordered { get; set; }
        [XmlElement(ElementName = "punAlteredFlag", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PunAlteredFlag PunAlteredFlag { get; set; }
        [XmlElement(ElementName = "punTechNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PunTechNo PunTechNo { get; set; }
        [XmlElement(ElementName = "punWorkDate", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PunWorkDate PunWorkDate { get; set; }
        [XmlElement(ElementName = "punWorkType", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PunWorkType PunWorkType { get; set; }
        [XmlElement(ElementName = "punDuration", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PunDuration PunDuration { get; set; }
        [XmlElement(ElementName = "punTimeOn", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PunTimeOn PunTimeOn { get; set; }
        [XmlElement(ElementName = "punLineCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PunLineCode PunLineCode { get; set; }
        [XmlElement(ElementName = "punTimeOff", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PunTimeOff PunTimeOff { get; set; }
        [XmlElement(ElementName = "linStorySequenceNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LinStorySequenceNo LinStorySequenceNo { get; set; }
        [XmlElement(ElementName = "linStoryEmployeeNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LinStoryEmployeeNo LinStoryEmployeeNo { get; set; }
        [XmlElement(ElementName = "linStoryText", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public LinStoryText LinStoryText { get; set; }
        [XmlElement(ElementName = "dedMaximumAmount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public DedMaximumAmount DedMaximumAmount { get; set; }
        [XmlElement(ElementName = "feeLOPorPartSeqNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public FeeLOPorPartSeqNo FeeLOPorPartSeqNo { get; set; }
        [XmlElement(ElementName = "dedLineCodes", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public DedLineCodes DedLineCodes { get; set; }
        [XmlElement(ElementName = "dedActualAmount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public DedActualAmount DedActualAmount { get; set; }
        [XmlElement(ElementName = "feeType", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public FeeType FeeType { get; set; }
        [XmlElement(ElementName = "dedLaborAmount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public DedLaborAmount DedLaborAmount { get; set; }
        [XmlElement(ElementName = "dedPartsAmount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public DedPartsAmount DedPartsAmount { get; set; }
        [XmlElement(ElementName = "dedSequenceNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public DedSequenceNo DedSequenceNo { get; set; }
        [XmlElement(ElementName = "feeSequenceNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public FeeSequenceNo FeeSequenceNo { get; set; }
        [XmlElement(ElementName = "feeLineCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public FeeLineCode FeeLineCode { get; set; }
        [XmlElement(ElementName = "feeOpCodeDesc", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public FeeOpCodeDesc FeeOpCodeDesc { get; set; }
        [XmlElement(ElementName = "feeCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public FeeCost FeeCost { get; set; }
        [XmlElement(ElementName = "feeFeeID", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public FeeFeeID FeeFeeID { get; set; }
        [XmlElement(ElementName = "feeOpCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public FeeOpCode FeeOpCode { get; set; }
        [XmlElement(ElementName = "feeLaborType", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public FeeLaborType FeeLaborType { get; set; }
        [XmlElement(ElementName = "feeSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public FeeSale FeeSale { get; set; }
        [XmlElement(ElementName = "feeLOPorPartFlag", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public FeeLOPorPartFlag FeeLOPorPartFlag { get; set; }
        [XmlElement(ElementName = "feeMcdPercentage", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public FeeMcdPercentage FeeMcdPercentage { get; set; }
        [XmlElement(ElementName = "dedLaborType", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public DedLaborType DedLaborType { get; set; }
        [XmlElement(ElementName = "rapApptID", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public RapApptID RapApptID { get; set; }
        [XmlElement(ElementName = "warLineCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public WarLineCode WarLineCode { get; set; }
        [XmlElement(ElementName = "mlsSequenceNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public MlsSequenceNo MlsSequenceNo { get; set; }
        [XmlElement(ElementName = "mlsOpCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public MlsOpCode MlsOpCode { get; set; }
        [XmlElement(ElementName = "mlsType", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public MlsType MlsType { get; set; }
        [XmlElement(ElementName = "prtBin1", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public PrtBin1 PrtBin1 { get; set; }
        [XmlElement(ElementName = "mlsSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public MlsSale MlsSale { get; set; }
        [XmlElement(ElementName = "mlsCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public MlsCost MlsCost { get; set; }
        [XmlElement(ElementName = "warAuthorizationCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public WarAuthorizationCode WarAuthorizationCode { get; set; }
        [XmlElement(ElementName = "mlsLaborType", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public MlsLaborType MlsLaborType { get; set; }
        [XmlElement(ElementName = "mlsMcdPercentage", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public MlsMcdPercentage MlsMcdPercentage { get; set; }
        [XmlElement(ElementName = "mlsOpCodeDesc", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public MlsOpCodeDesc MlsOpCodeDesc { get; set; }
        [XmlElement(ElementName = "mlsLineCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public MlsLineCode MlsLineCode { get; set; }
    }

    [XmlRoot(ElementName = "prtLineCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PrtLineCode
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "prtSequenceNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PrtSequenceNo
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "prtEmployeeNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PrtEmployeeNo
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "prtDesc", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PrtDesc
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "prtQtyOrdered", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PrtQtyOrdered
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "prtList", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PrtList
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "prtUnitServiceCharge", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PrtUnitServiceCharge
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "prtMcdPercentage", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PrtMcdPercentage
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "prtCoreSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PrtCoreSale
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "prtLaborType", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PrtLaborType
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "prtSource", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PrtSource
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "prtCoreCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PrtCoreCost
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "prtQtySold", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PrtQtySold
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "prtComp", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PrtComp
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "prtCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PrtCost
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "prtPartNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PrtPartNo
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "prtSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PrtSale
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "prtClass", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PrtClass
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "prtExtendedCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PrtExtendedCost
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "prtExtendedSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PrtExtendedSale
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "prtLaborSequenceNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PrtLaborSequenceNo
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "prtQtyOnHand", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PrtQtyOnHand
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "prtQtyFilled", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PrtQtyFilled
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "prtQtyBackordered", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PrtQtyBackordered
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "punAlteredFlag", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PunAlteredFlag
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "punTechNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PunTechNo
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "punWorkDate", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PunWorkDate
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "punWorkType", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PunWorkType
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "punDuration", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PunDuration
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "punTimeOn", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PunTimeOn
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "punLineCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PunLineCode
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "punTimeOff", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PunTimeOff
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "linStorySequenceNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LinStorySequenceNo
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "linStoryEmployeeNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LinStoryEmployeeNo
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "linStoryText", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class LinStoryText
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "dedMaximumAmount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class DedMaximumAmount
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "feeLOPorPartSeqNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class FeeLOPorPartSeqNo
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "dedLineCodes", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class DedLineCodes
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "dedActualAmount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class DedActualAmount
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "feeType", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class FeeType
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "dedLaborAmount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class DedLaborAmount
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "dedPartsAmount", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class DedPartsAmount
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "dedSequenceNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class DedSequenceNo
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "feeSequenceNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class FeeSequenceNo
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "feeLineCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class FeeLineCode
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "feeOpCodeDesc", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class FeeOpCodeDesc
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "feeCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class FeeCost
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "feeFeeID", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class FeeFeeID
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "feeOpCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class FeeOpCode
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "feeLaborType", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class FeeLaborType
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "feeSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class FeeSale
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "feeLOPorPartFlag", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class FeeLOPorPartFlag
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "feeMcdPercentage", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class FeeMcdPercentage
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "dedLaborType", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class DedLaborType
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "rapApptID", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class RapApptID
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "warLineCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class WarLineCode
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "mlsSequenceNo", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class MlsSequenceNo
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "mlsOpCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class MlsOpCode
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "mlsType", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class MlsType
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "prtBin1", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class PrtBin1
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "mlsSale", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class MlsSale
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "mlsCost", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class MlsCost
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "warAuthorizationCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class WarAuthorizationCode
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "mlsLaborType", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class MlsLaborType
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "mlsMcdPercentage", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class MlsMcdPercentage
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "mlsOpCodeDesc", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class MlsOpCodeDesc
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "mlsLineCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class MlsLineCode
    {
        [XmlElement(ElementName = "V", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<V> V { get; set; }
    }

    [XmlRoot(ElementName = "ServiceRODetailHistory", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
    public class ServiceRODetailHistory
    {
        [XmlElement(ElementName = "service-repair-order-history", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public List<Servicerepairorderhistory> Servicerepairorderhistory { get; set; }
        [XmlElement(ElementName = "ErrorCode", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string ErrorCode { get; set; }
        [XmlElement(ElementName = "ErrorMessage", Namespace = "http://www.dmotorworks.com/service-repair-order-history")]
        public string ErrorMessage { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

}