using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vector.Common.Entities;

namespace Vector.Garage.Entities
{
    public class BatchInfoSearch
    {
        public string Action { get; set; }
        public Int64? BatchId { get; set; }
        public Int64? TaskId { get; set; }
    }

    public class BatchInfo
    {
        public string Action { get; set; }
        public Int64? BatchId { get; set; }
        public string BatchType { get; set; }
        public string BatchCategory { get; set; }
        public Int64 TaskId { get; set; }
        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }

    public class InvoiceHeaderInfoSearch
    {
        public string Action { get; set; }
        public Int64? BatchId { get; set; }
        public DateTime? BatchDate { get; set; }
        public string ImageStatus { get; set; }
        public Int64? TaskId { get; set; }
    } 
    public class BatchUploadDocumentsInfo
    {
        public string Action { get; set; }
        public Int64? BatchId { get; set; }
        public Int64 TaskId { get; set; }
        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
        public List<BatchUploadDocument> BatchUploadDocuments { get; set; }
    }
    public class BatchUploadDocument
    {
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
    } 
    public class InvoiceHeaderInfo
    {
        public string Action { get; set; }
        public Int64? InvoiceId { get; set; }
        public Int64? ExceptionId { get; set; }
        public Int64? ExceptionStatusId { get; set; }
        public Int64? ExceptionStatusDescId { get; set; } 
        public Int64? BatchdetailId { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public Int64? VendorId { get; set; }
        public string VendorName { get; set; }
        public Int64? AccountId { get; set; }
        public string AccountNumber { get; set; }
        public string ClientName { get; set; }
        public string PropertyName { get; set; }
        public DateTime? ContractBeginDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string ContractStatus { get; set; }
        public string BillingCycle { get; set; }
        public string ContractDocumentName { get; set; }
        public string ContractDocumentPath { get; set; }
        public decimal PreviousBalance { get; set; }
        public string InvoiceNo { get; set; }
        public decimal PaymentsRecieved { get; set; }
        public string InvoiceStatus { get; set; }
        public string InvoiceStatusValue { get; set; }
        public decimal UnPaidBalance { get; set; }
        public DateTime? ServiceStartDate { get; set; }
        public DateTime? ServiceEndDate { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal CurrentCharges { get; set; }
        public decimal CurrentDue { get; set; }
        public decimal ThirtySixtyDaysDue { get; set; }
        public decimal SixtyNintyDaysDue { get; set; }
        public decimal NintyPlusDue { get; set; }
        public decimal BillAmount { get; set; }
        public decimal ErrorsCaughtAndCorrected { get; set; }
        public string InvoiceLineitemInfo { get; set; }
        public bool? isAmendmentInvoice { get; set; }
        public bool? isPassthrough { get; set; }
        public string AdditionalLineItemInfo { get; set; }
        public Int64? TaskId { get; set; }
        public string Comments { get; set; }
        public Int64? UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }

    }


    public class InvoiceLineItemInfoSearch
    {
        public string Action { get; set; }
        public string BatchNo { get; set; }
        public string AccountNumber { get; set; }
        public string ClientName { get; set; }
        public string ProperyName { get; set; }
        public string InvoiceNumber { get; set; }
        public string ContractId { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string InvoiceStatus { get; set; }
        public string InvoiceId { get; set; }
        public Int64? TaskId { get; set; }
        public Int64? BatchDetailsId { get; set; }
    }

    public class InvoiceLineItemInfo
    { 
        public Int64? InvoiceId { get; set; }
        public Int64? InvoiceLineitemId { get; set; }
        public decimal VectorApproved { get; set; }
        public decimal VendorBilled { get; set; }
        public decimal Quantity { get; set; }  
    }

    public class InvoiceInfo
    {
        public string Action { get; set; }
        public Int64? InvoiceId { get; set; }
        public string InvoiceHeader { get; set; }
        public string InvoiceLineitems { get; set; }
        public Int64? TaskId { get; set; }
        public string Comments { get; set; }
        public Int64? UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }

    public class SearchInfo
    {
        public string Action { get; set; }
        public Int64? InvoiceId { get; set; }
        public Int64? ExceptionId { get; set; }
        public Int64? TaskId { get; set; }
        public Int64? UserId { get; set; }
        public bool IsFromTask { get; set; } 
    }


    public class InvoiceEntity
    {
        public string Action { get; set; }
        public string BatchNo { get; set; }
        public string InvoiceStatus { get; set; }
        public string ClientName { get; set; }
        public string PropertyName { get; set; }
        public string VendorName { get; set; }
        public string VendorInvoiceNo { get; set; }
        public string VectorInvoiceNo { get; set; }
        public Int64? UserId { get; set; }
        public string AccountNumber { get; set; }
        public string ContractNo { get; set; }
        public DateTime? AuditedDateFrom { get; set; }
        public DateTime? AuditedDateTo { get; set; }
    }

    public class SearchInvoice
    {
        public string Action { get; set; }
        public Int64? BatchId { get; set; }
        public string BatchNo { get; set; }
        public string DispatchStatus { get; set; }
        public Int64? ClientId { get; set; }
        public string ClientName { get; set; }
        public Int64? PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string VendorInvoiceNo { get; set; }
        public Int64? InvoiceId { get; set; }
        public string VectorInvoiceNo { get; set; }
        public string VendorAccount { get; set; }
        public Int64? VendorAccountId { get; set; }
        public Int64? ContractId { get; set; }
        public string ContractNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? InvoiceDueDate { get; set; }
        public DateTime? InvoiceFromDate { get; set; }
        public DateTime? InvoiceToDate { get; set; }
    }

    public class DispatchInvoice
    {
        public string Action { get; set; }
        public Int64? InvoiceId { get; set; }
        public Boolean isVendorInvoice { get; set; }
        public string VendorInvoiceNo { get; set; }
        public Boolean isVectorInvoice { get; set; }
        public string VectorInvoiceNo { get; set; }
        public Boolean isSummaryInvoice { get; set; }
        public string SummaryInvoice { get; set; }
        public string toEmails { get; set; }
        public string ccEmails { get; set; }
        public string bccEmails { get; set; }
        public string subject { get; set; }
        public string bodyHtml { get; set; }
        public Int64? taskid { get; set; }
        public string isFromTask { get; set; }
        public Int64? UserId { get; set; }
        public string SaveOrComplete { get; set; }
    }

    public class VendorPastDue
    {
        public string Action { get; set; }
        public Int64? InvoiceId { get; set; }
        public Int64? VendorPastDueInfoId { get; set; }
        public string PendingDueDays { get; set; }
        public string PendingCredits { get; set; }
        public string CauseOfPastDue { get; set; }
        public string Comments { get; set; }
        public Int64? Taskid { get; set; }
        public string isFromTask { get; set; }
        public Int64? UserId { get; set; }
        public string SaveOrComplete { get; set; }
    }

    public class VendorPendingCredits
    {
        public string Action { get; set; }
        public Int64? InvoiceId { get; set; }
        public Int64? VendorPendingCreditsInfoId { get; set; }
        public string CreditsReceived { get; set; }
        public string Comments { get; set; }
        public Int64? Taskid { get; set; }
        public string isFromTask { get; set; }
        public Int64? UserId { get; set; }
        public string SaveOrComplete { get; set; }
    }
    public class VendorEmailData
    {
        public string Action { get; set; }
        public Int64? InvoiceId { get; set; }        
        public string EmailSentTo { get; set; }
        public List<Files> Documents { get; set; }
        public Int64? Taskid { get; set; }
        public Int64? UserId { get; set; }
    }


    public class LineItems
    {
        public string Action { get; set; }
        public Int64? InvoiceId { get; set; }
        public string accountLineItemsXml { get; set; } 
    }

    public class BillGapComments
    {
        public string Action { get; set; }
        public Int64 AccountId { get; set; }
        public string Period { get; set; }

        public string Field { get; set; }
        public string Comments { get; set; }
    }

    public class Actions
    {
        public string Action { get; set; }
        public Int64? ActionId { get; set; }
        public string ActionName { get; set; }
        public string ActionDescription { get; set; }
        public DateTime?  DueDate { get; set; }
        public string ActionStatus { get; set; }
        public List<documents> DocumentXml { get; set; }  

        public string TempFolderPath { get; set; }

        public Int64? AssignToUserId { get; set; }
    }

    public class documents
    {
        public string DocumentName { get; set; }
        public string DocumentType { get; set; } 
    }



    public class MissingInvoice
    {
        public string Action { get; set; }
        public string AccountId { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? LastInvoiceDate { get; set; }
        public string Comments { get; set; }
        public string DocumentXml { get; set; }
        public string TempFolderName { get; set; }
        public List<InvoiceDocument> Documents { get; set; }
        public string IRUniqueCode { get; set; }
        public string ContractId { get; set; }
    }


    public class InvoiceDocument
    {
        public string fileName { get; set; }
        public string filePath { get; set; }
    }



    public class DocumentUpload
    {
        public string fileName { get; set; }
        public string filePath { get; set; }
        public string uniqueFileName { get; set; }
    }


    public class Placeholder
    {
        public string Action { get; set; }
        public Int64 AccountId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? LastInvoiceDate { get; set; }
        public string Comments { get; set; }
        public DateTime ServiceStartDate { get; set; }
        public DateTime ServiceEndDate { get; set; }
        public string InvoiceNumber { get; set; }
        public Int64? StagingInvoiceId { get; set; }
        public string IRUniqueCode { get; set; }
    }


    public class InvoiceLineitemInfo
    {
        public string Action { get; set; }
    
        public Int64 InvoiceId { get; set; }

        public Int64 InvoiceLineitemId { get; set; }
    }

    public class IRPDocuments
    {
        public string Action { get; set; }         
        public string Comments { get; set; }
        public string DocumentXml { get; set; }
        public string TempFolderName { get; set; }
        public List<DocumentUpload> Documents { get; set; }
        public string IRUniqueCode { get; set; }
    }


}
