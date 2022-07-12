using System;
using System.Collections.Generic;
using System.Text;

namespace Vector.Garage.Entities
{
    public class Negotiations
    {
        public string Action { get; set; }
        public Int64 NegotiationId { get; set; }
        public Int64 NegotiationInfoId { get; set; }
        public Int64 TaskId { get; set; }
        public string NegotiationNo { get; set; }
        public string NegotiationType { get; set; }
        public Int64 ClientId { get; set; }
        public string Property { get; set; }
        public string Vendor { get; set; }
        public string BaseLineItems { get; set; }
        public string Condition { get; set; }
        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string ProcessStatus { get; set; }
        public string SaveOrComplete { get; set; }
    }

    public class NegotiationSearch
    {
        public string Action { get; set; }
        public Int64 NegotiationId { get; set; }
        public Int64 NegotiationIdInfoId { get; set; }
        public string PropertyId { get; set; }
        public string VendorId { get; set; }
        public Int64 TaskId { get; set; }
        public string Client { get; set; }
        public Int64 ClientId { get; set; }
        public string PropertyName { get; set; }
        public string HaulerName { get; set; }
        public string AccountManager { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string CreatedDate { get; set; }
        public string EffectiveDate { get; set; }
        public string EndDate { get; set; }
        public string Negotiator { get; set; }
        public string NegotiationStatus { get; set; }
        public string PropertyMode { get; set; }
        public string NegotiationNo { get; set; }
        public string RemainderDate { get; set; }
        public Int64 UserId { get; set; }
        public Int64 NegotiationBidSheetInfoId { get; set; }
        public Int64 NegotiationLineItemsId { get; set; }
        public string ReportBy { get; set; }
        public string ContarctEffectiveFromDate { get; set; }
        public string ContarctEffectiveToDate { get; set; }
        public string ContarctEndFromDate { get; set; }
        public string ContarctEndToDate { get; set; }
        public string NegotiationBeginDate { get; set; }
        public string NegotiationEndDate { get; set; }
        public Int64 AccountExecutiveId { get; set; }


    }

    public class NegotiationsBidSheetInfo
    {
        public string Action { get; set; }
        public Int64 NegotiationId { get; set; }
        public Int64 NegotiationBidSheetInfoId { get; set; }
        public string PropertyId { get; set; }
        public int TaskId { get; set; }
    }

    public class NegotiationsBidValues
    {
        public string VendorId { get; set; }
        public string NegotiationId { get; set; }
        public string BidValuesXml { get; set; }
        public string UserId { get; set; }
        public string Comments { get; set; }
    }

    public class DraftNegotiations
    {
        public string Action { get; set; }
        public Int64 NegotiationId { get; set; }
        public Int64 DraftNegotiationInfoId { get; set; }
        public Int64 TaskId { get; set; }
        public Int64 ClientId { get; set; }
        public string NegotiationNo { get; set; }
        public string NegotiationType { get; set; }
        public string NegotiationStatus { get; set; }
        public string Property { get; set; }
        public string Vendor { get; set; }
        public string BaseLineItems { get; set; }
        public decimal ApprovedAnnualIncrease { get; set; }
        public decimal? ManagementFee { get; set; }
        public string ExistingContractExpiryDate { get; set; }
        public string Negotiator { get; set; }
        public string RevisitReminderDate { get; set; }
        public string nonRenewalRemainderDate { get; set; }
        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string ProcessStatus { get; set; }
        public string SaveOrComplete { get; set; }
        public List<RequestbidVendorDetails> RequestbidVendorDetails { get; set; }
        public string ProcessInvoicePreriorToNegotiation { get; set; }
        public string BillProcessing { get; set; }
        public string ProcessingStartDate { get; set; }

        public string DocumentXml { get; set; }

        public string TempFolder { get; set; }

        public string Reason { get; set; }

        public string createDraftNegotaition { get; set; }


        public string IsSavings { get; set; } 
        public string VendorPaymentTerm { get; set; }
        
    }

    public class RequestbidVendorDetails
    {
        public string VendorName { get; set; }
        public Int64 VendorId { get; set; }
        public string VendorEmail { get; set; }
        public string lineItems { get; set; }
    }

    public class NegotiationBidSheetInfo
    {
        public string Action { get; set; }
        public Int64 NegotiationId { get; set; }
        public Int64 NegotiationBidSheetInfoId { get; set; }
        public Int64 TaskId { get; set; }
        public string NegotiationStatus { get; set; }
        public string NegotiationLineItems { get; set; }

        public string NegotiationLineExtra { get; set; }

        public string NegotiationLineArchived { get; set; }

        public decimal ApprovedAnnualIncrease { get; set; }
        public decimal ManagementFee { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal SavingSharePercentage { get; set; }
        public string ExistingContractExpiryDate { get; set; }
        public DateTime? ContractBeginDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string Negotiator { get; set; }
        public string MonthTerm { get; set; }
        public string ChangesNeedClientApproval { get; set; }
        public string ClientApprovedChanges { get; set; }
        public string Reasons { get; set; }
        public string RevisitReminderDate { get; set; }
        public string nonRenewalRemainderDate { get; set; }
        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }

        public int NegotiatorId { get; set; }

        public string IsSavings { get; set; }
        public string VendorPaymentTerm { get; set; }


    }
    public class NegotiationLineItem
    {
        public Int64 baselineCatalogLineItemInfoDetailId { get; set; }
        public Int64 baselineId { get; set; }
        public string chargeType { get; set; }
        public string lowestBidPrice { get; set; }
        public Int64 negotiationId { get; set; }
        public Int64 negotiationLineItemsId { get; set; }
        public string noOfTimes { get; set; }
        public string perUnitCost { get; set; }
        public Int64 propertyId { get; set; }
        public string serviceBehaviour { get; set; }
        public string serviceFrequency { get; set; }
        public string serviceName { get; set; }
        public string serviceQuantity { get; set; }
        public string sixMonthsAverage { get; set; }
        public string size { get; set; }
        public string total { get; set; }
        public Int64 vendorId { get; set; }
        public string vendorName { get; set; }
        public string negotiationLineItemsGUID { get; set; }
    }

    public class NegotiationLineItemData
    {
        public Int64 NegotiationId { get; set; }
        public Int64 NegotiationLineItemsId { get; set; }
        public Int64 VendorId { get; set; }
        public Int64 PropertyId { get; set; }
        public Int64 BidValue { get; set; }
        public Int64 ServiceQuantity { get; set; }
        public string ServiceFrequency { get; set; }
        public string NoOfTimes { get; set; }
        public Int64 Total { get; set; }
        public Int64 PerUnitCost { get; set; }
        public Int64 Savings { get; set; }
        public Int64 UserId { get; set; }
        public string Comments { get; set; }
    }

    public class BaselineNegotiationLineItems
    {
        public Int64 NegotiationId { get; set; }
        public Int64 BaselineCatalogLineItemId { get; set; }
        public string LineItemsXml { get; set; }
        public Int64 UserId { get; set; }
        public string Comments { get; set; }
        public Int64 NegotiationLineItemId { get; set; }
    }


    public class NegotiationDocuments
    {
        public string Action { get; set; }
        public Int64 NegotiationId { get; set; }
        public string NegotiationNo { get; set; }
        public Int64 taskId { get; set; }
        public string documentXml { get; set; }
        public DocumentDetails[] documentInfo { get; set; }
    }

    public class DocumentDetails
    {
        public string DocumentName { get; set; }
        public string TempFolderName { get; set; }
        public string Type { get; set; }
    }


    public class NegotiationLineitemUpdate
    {
        public Int64 negotiationId { get; set; }
        public Int64 negotiaitonLineitemId { get; set; }
        public string lineItemsXml { get; set; } 
        public string comments { get; set; } 
    }


    public class BidServiceInfo
    {
        public string Action { get; set; }
        public string Lineitems { get; set; }
        public Int64 NegotiationId { get; set; }
        public Int64 VendorId { get; set; }
    }


    public class CloneLineitems
    {
        public string Action { get; set; }
        public Int64 VendorId { get; set; }
        public Int64 NegotiationId { get; set; }
    }
}

