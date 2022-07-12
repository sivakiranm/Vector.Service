using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector.Garage.Entities
{

    public class AccountInfoSearch
    {

        public string Action { get; set; }
        public Int64? AccountId { get; set; }
        public Int64? AccountInfoId { get; set; }
        public Int64? TaskId { get; set; }
    }
    public class AccountInfo
    {
        public string Action { get; set; }
        public Int64 AccountId { get; set; }
        public Int64 AccountInfoId { get; set; }
        public Int64 PropertyId { get; set; }
        public Int64 ContractId { get; set; }
        public Int64 VendorId { get; set; }
        public string VendorName { get; set; }
        public string AccountMode { get; set; }
        public string AccountNumber { get; set; }
        public string ContractVersion { get; set; }
        public string AccountStatus { get; set; }
        public string PaymentTerm { get; set; }
        public DateTime? AccountInActivationDate { get; set; }
        public string AccountType { get; set; }
        public string BillingCycle { get; set; }
        public string HaulerPaymentTerm { get; set; }
        public string VectorPaymentTerm { get; set; }
        public string VendorOriginalInvoice { get; set; }
        public string VendorInvoiceId { get; set; }
        public string UniqueVendorCode { get; set; }
        public string RegisteredOnline { get; set; }
        public int VendorInvoiceDay { get; set; }
        public string ModeOfReceipt { get; set; }
        public string BilledWhen { get; set; }
        public decimal ManagementFee { get; set; }
        public string Savings { get; set; }
        public bool IsSeasonal { get; set; }
        public DateTime? SeasonalFrom { get; set; }
        public DateTime? SeasonalTo { get; set; }
        public string LineItemsXml { get; set; }
        public string LineItemsXmlExtras { get; set; }
        public string LineItemsXmlArchived { get; set; }
        public string ExemptedItems { get; set; }
        public Int64 TaskId { get; set; }
        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; } 

        public string taskCloseType { get; set; }
    }


    public class AccountRemitInfoSearch
    {
        public string Action { get; set; }
        public Int64? AccountId { get; set; }
        public Int64? AccountRemitInfoId { get; set; }
        public Int64? TaskId { get; set; }
    }

    public class AccountRemitInfo
    {
        public string Action { get; set; }
        public Int64? AccountId { get; set; }
        public Int64? AccountRemitInfoId { get; set; }
        public Int64? VendorPaymentACHDetailId { get; set; }
        public Int64? VendorPaymentPCardDetailId { get; set; }
        public Int64? VendorPaymentCheckDetailId { get; set; }
        public string PaymentType { get; set; }
        public Int64? VendorPaymentDetailId { get; set; }
        public Int64 TaskId { get; set; }
        public string Comments { get; set; }
        public string Currency { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }


    public class SearchAccounts
    {
        public string Action { get; set; }
        public string AccountNumber { get; set; }
        public string AccountManager { get; set; }
        public string ClientName { get; set; }
        public string AccountStatus { get; set; }
        public string VendorCorporateName { get; set; }
        public string ContractNo { get; set; }
        public string PropertyName { get; set; }
        public string VendorName { get; set; }

    }

    public class AccountLineItemInfo
    {
        public string action { get; set; }
        public Int64? accountId { get; set; }
        public Int64? accountLineItemId { get; set; }

        public Int64? contractLineitemId { get; set; }
        public string type { get; set; }
    }




    public class UpdateAccountLineitemInfo
    {
        public string action { get; set; }
        public Int64? accountId { get; set; }
        public Int64? accountLineitemId { get; set; }

        public Int64? serviceCatalogueId { set; get; }
        public string comments { set; get; }

        public string equipmentTypeName { set; get; }
        public decimal? equipmentSize { set; get; }
        public string uom { set; get; }
        public string serviceCategory { set; get; }
        public string wasteType { set; get; }



        public BaselineLineitem baselineLineitem { set; get; }

        public ContractLineitem contractLineitem { set; get; }
        public NegotiationLineItems negotiationLineitem { set; get; }

        public string saveOrComplete { get; set; }

        public Int64? ContractId { get; set; }
    }

    public class BaselineLineitem
    {
            public Int64? baselineCatalogLineItemId { get; set; }
        public Int64? serviceCatalogueId { get; set; }
        public string vendorDescription { get; set; }
        public string serviceBehaviour { get; set; }
        public decimal? serviceQuantity { get; set; }
        public string serviceQuantityType { get; set; }
        public decimal? minimumUnits { get; set; }
        public decimal? maximumUnits { get; set; }
        public decimal? perYardRate { get; set; }
        public decimal? totalYardRate { get; set; }
        public bool isSurchargable { get; set; }
        public bool isTaxable { get; set; }
        public string serviceFrequency { get; set; }
        public int? noOfTimes { get; set; }
        public string servicedOn { get; set; }
        public decimal? perUnitCost { get; set; }
        public decimal? totalCost { get; set; }
        public bool monthlyRecurringCharge { get; set; }
        public decimal? sixMonthsAverage { get; set; }
        public decimal? size { get; set; }
        public string lineItemType { get; set; }
        public decimal? capPercent { get; set; }
        public decimal? offeredCapPercent { get; set; }


    }

    public class ContractLineitem
    {
        public Int64? contractLineitemId { get; set; }
        public Int64? serviceCatalogueId { get; set; }
        public string vendorDescription { get; set; }
        public string serviceBehaviour { get; set; }
        public decimal? serviceQuantity { get; set; }
        public string serviceQuantityType { get; set; }
        public decimal? minimumUnits { get; set; }
        public decimal? maximumUnits { get; set; }
        public decimal? perYardRate { get; set; }
        public decimal? totalYardRate { get; set; }
        public bool isSurchargable { get; set; }
        public bool isTaxable { get; set; }
        public string serviceFrequency { get; set; }
        public int? noOfTimes { get; set; }
        public string servicedOn { get; set; }
        public decimal? perUnitCost { get; set; }
        public decimal? totalCost { get; set; }
        public bool monthlyRecurringCharge { get; set; }
        public decimal? sixMonthsAverage { get; set; }
        public decimal? size { get; set; }
        public string lineItemType { get; set; }

        public decimal? capPercent { get; set; }
        public decimal? offeredCapPercent { get; set; }
        public bool apiApplicable { get; set; }

    }


    public class NegotiationLineItems
    {
        public Int64? negotiationLineitemId { get; set; }
        public Int64? serviceCatalogueId { get; set; }
        public string vendorDescription { get; set; }
        public string serviceBehaviour { get; set; }
        public decimal? serviceQuantity { get; set; }
        public string serviceQuantityType { get; set; }
        public decimal? minimumUnits { get; set; }
        public decimal? maximumUnits { get; set; }
        public decimal? perYardRate { get; set; }
        public decimal? totalYardRate { get; set; }
        public bool isSurchargable { get; set; }
        public bool isTaxable { get; set; }
        public string serviceFrequency { get; set; }
        public int? noOfTimes { get; set; }
        public string servicedOn { get; set; }
        public decimal? perUnitCost { get; set; }
        public decimal? totalCost { get; set; }
        public bool monthlyRecurringCharge { get; set; }
        public decimal? sixMonthsAverage { get; set; }
        public decimal? size { get; set; }
        public string lineItemType { get; set; }

        public decimal? capPercent { get; set; }
        public decimal? offeredCapPercent { get; set; }

        public bool isNegotiated { get; set; }

    }

    public class AccountComments
    { 
        public Int64 accountId { get; set; }
        public DateTime? CommentsDate { get; set; }
    }


    public class ArchiveAccountVericationRecord
    {
        public Int64 accountVerificationId { get; set; }
        public string Comments { get; set; }
    }

}
