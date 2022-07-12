using System;
using System.Collections.Generic;
using System.Text;

namespace Vector.Garage.Entities
{


    public class AgingEntity
    {
        public string ReportBy { get; set; }
        public string Client { get; set; }
        public string Property { get; set; }
        public string Hauler { get; set; }
        public string Negotiator { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string HaulerInvoiceDateFrom { get; set; }
        public string HaulerInvoiceDateTo { get; set; }
        public string RsInvoiceDateFrom { get; set; }
        public string RsInvoiceDateTo { get; set; }
        public string HaulerAccount { get; set; }
        public string HaulerInvoice { get; set; }
        public string RsInvoice { get; set; }
        public string AccountExecutive { get; set; }
        public string SalesPerson { get; set; }
        public string BillingAnalyst { get; set; }
        public string NegotiationStatus { get; set; }
        public string ContractId { get; set; }
        public string status { get; set; }
        public string vendorCoporateName { get; set; }

        public string PaymentType { get; set; }
    }


    public class ClientListReportEntity
    {
        public string Action { get; set; }
        public string ClientName { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string propertyName { get; set; }
        public string accountExecutive { get; set; }
        public string salesPerson { get; set; }
        public string billingType { get; set; }
        public string status { get; set; }
    }

    public class InvoiceTransactions
    {
        public string action { get; set; }
        public string transactions { get; set; }

    }

    public class SearchEntity
    {
        public string action { get; set; }
        public string clientName { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string propertyName { get; set; }
        public string propertyAddress { get; set; }
        public string accountExecutive { get; set; }
        public string salesPerson { get; set; }
        public string billingType { get; set; }
        public string status { get; set; }

        public string vendorCoporateName { get; set; }
        public string vendorName { get; set; }
        public string accountNumber { get; set; }

        public string billingAnalyst { get; set; }

        public string negotiator { get; set; }
        public string paymentType { get; set; }
        public string zip { get; set; }

        public DateTime? invoiceFromDate { get; set; }
        public DateTime? invoiceToDate { get; set; }

        public DateTime? auditInvoiceFromDate { get; set; }
        public DateTime? auditInvoiceToDate { get; set; }

        public string vendorInvoiceNumber { get; set; }
        public string vectorInvoiceNumber { get; set; }

        public Int64? clientId { get; set; }
        public Int64? propertyId { get; set; }

        public string accountStatus { get; set; }

        public string accountExecutiveId { get; set; }

        public string billingAnalystId { get; set; }

        public DateTime? fromDate { get; set; }

        public DateTime? toDate { get; set; }

        public string DispatchStatus { get; set; }
        public string vendorId { get; set; }
        public string InvoiceId { get; set; }
        public string accountId { get; set; }
        public string batchId { get; set; }

        public string InvoiceStatus { get; set; }

        public string contractId { get; set; }

        public string contractNo { get; set; }

        public string missingStatus { get; set; }

        public string serviceCategory { get; set; }
        public string propertyStatus { get; set; }
        public string clientStatus { get; set; }
        public string vendorCorporateStatus { get; set; }
        public string vendorStatus { get; set; }

        public Int64? vendorCorporateId { get; set; }

        public Int64? negotiationId { get; set; }
        public string negotiationStatus { get; set; }
        public string negotiationReasons { get; set; }
        public DateTime? contractEffectiveFromDate { get; set; }
        public DateTime? contractEffectiveToDate { get; set; }
        public DateTime? contractEndToDate { get; set; }
        public DateTime? negotiationBeginDate { get; set; }
        public DateTime? negotiationEndDate { get; set; }
        public string contractStatus { get; set; }

        public string vendorMailingState { get; set; }
        public string vendorMailingCity { get; set; }
        public string vendorMailingZip { get; set; }
        public string accountType { get; set; }
        public string accountMode { get; set; }

        public string reportBy { get; set; }
        public Boolean? IsOnOff { get; set; }

        public DateTime? contractEffectiveFromDateRangeFrom { get; set; }
        public DateTime? contractEffectiveFromDateRangeTo { get; set; }


        public DateTime? contractEffectiveToDateRangeFrom { get; set; }
        public DateTime? contractEffectiveToDateRangeTo { get; set; }


        public DateTime? negotiationStartDateRangeFrom { get; set; }
        public DateTime? negotiationStartDateRangeTo { get; set; }


        public DateTime? negotiationEndDateRangeFrom { get; set; }
        public DateTime? negotiationEndDateRangeTo { get; set; }

        public string baselineNo {get;set; }
        public string registeredOnline { get; set; }


        public DateTime? negotiationAwardedDateRangeFrom { get; set; }
        public DateTime? negotiationAwardedDateRangeTo { get; set; }

        public DateTime? vendorInvoiceFromDate { get; set; }
        public DateTime? vendorInvoiceToDate { get; set; }

        public Int64? obfId { get; set; }

        public string IsVerified { get; set; }

        public string monthName { get; set; }

        public string assignedTo { get; set; }

        public string colorCode { get; set; }

        public string ticketStatus { get; set; }
    }


    public class DownloadInvoices
    {
        public string invoiceXml { get; set; }

    }

    public class MarkAsNotMissing
    {
        public string Action { get; set; }
        public Int64? AccountId { get; set; }
        public DateTime? InvoiceDate { get; set; }

        public string fileName { get; set; }
        public string folderName { get; set; }
    }


    public class TaskSearch
    {
        public string Action { get; set; }
        public Int64? clientId { get; set; }
        public Int64? propertyId { get; set; }

        public Int64? negotiationId { get; set; }
        public string category { get; set; }
        public Int64? accountId { get; set; }
        public Int64? contractId { get; set; }
        public Int64? baselineId { get; set; }
        public Int64? vendorId { get; set; }
        public DateTime? taskCreatedFromDate { get; set; }
        public DateTime? taskCreatedToDate { get; set; }
        public DateTime? taskCompletedFromDate { get; set; }
        public DateTime? taskCompletedToDate { get; set; }
        public Int64? taskId { get; set; }
        public Int64? ticketId { get; set; }

    }




    public class PersonalizeSettings
    {
        public string Action { get; set; }
        public Int64? FeatureId { get; set; }
        public Int64? widgetId { get; set; }
        public string Type { get; set; }
        public string propertyIds { get; set; }
        public string clientIds { get; set; } 
    }


    public class WorkEntity
    {
        public string action { get; set; }
        public Int64? userId { get; set; }
        public string userName { get; set; }
        public string workStatus { get; set; }
        public string functionalArea { get; set; }
        public string issueType { get; set; }
        public string trackingNo { get; set; }
        public Int64? releaseId { get; set; }
        public string comments { get; set; }
        public string longDescription { get; set; }
        public string shortDescription { get; set; }


        public Int64? workId { get; set; }
        public string workType { get; set; }
        public Int64? mergerWorkId { get; set; }

        public string documentXml { get; set; }
        public string tempFolderName { get; set; }
        public List<workDocuments> documents { get; set; } 
    }

    public class workDocuments
    {
        public string fileName { get; set; }
        public string filePath { get; set; }
    }




}
