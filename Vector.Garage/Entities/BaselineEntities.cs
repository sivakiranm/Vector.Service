using System;
using System.Collections.Generic;

namespace Vector.Garage.Entities
{
    public class BaselineSupportFilesSearch
    {

        public string Action { get; set; }
        public Int64 BaselineId { get; set; }
        public Int64 BaselineInfoId { get; set; }
        public Int64 TaskId { get; set; }
    }
    public class BaseLineSupportFiles
    {
        public string Action { get; set; }
        public Int64 BaseLineId { get; set; }
        public Int64 BaseLineInfoId { get; set; }
        public Int64 TaskId { get; set; }
        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
        public List<BaseLineSupportingFilesDocuments> BaseLineSupportingFilesDocuments { get; set; }
    }

    public class BaseLineFileModel
    {
        public string BaseLineName { get; set; }
        //public List<IFormFile> FormFiles { get; set; }
    }

    public class BaseLineDocuments
    {

        public Int64 BaseLineId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public string Version { get; set; }
        public string ChooseDocumentType { get; set; }
        public string Status { get; set; }

    }

    public class BaseLineInfo
    {
        public string Action { get; set; }
        public Int64? BaseLineId { get; set; }
        public Int64? BaseLineInfoId { get; set; }
        public Int64? VendorId { get; set; }
        public Int64? PropertyId { get; set; }
        public Int64? TaskId { get; set; }
        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }
    public class BaselineInfoSearch
    {
        public string Action { get; set; }
        public Int64? BaseLineId { get; set; }
        public Int64? BaseLineInfoId { get; set; }
        public Int64? TaskId { get; set; }
    }

    public class LineitemInfo
    {
        public string Action { get; set; }
        public string State { get; set; }
        public Int64? EntityId { get; set; }
        public Int64? EntityLineitemId { get; set; }
        public string Comments { get; set; }
        public Int64? TaskId { get; set; }
    }

    public class MapCatalogSearch
    {

        public string Action { get; set; }
        public Int64? BaselineId { get; set; }
        public Int64? BaselineMapCatalogLineItemInfoId { get; set; }
        public Int64? TaskId { get; set; }
        public Int64? BaselineCatalogLineItemId { get; set; }
        public Int64? NegotiationLineItemsId { get; set; }

    }

    public class MapCatalog
    {

        public string Action { get; set; }
        public Int64? BaseLineId { get; set; }
        public Int64? BaselineMapCatalogLineItemInfoId { get; set; }
        public Int64? TaskId { get; set; }
        public Int64? VendorContactId { get; set; }
        public string LineItemsXml { get; set; }
        public string Comments { get; set; }
        public Int64? UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }

        public bool IsAdditionalChargesAdded { get; set; }
        public bool IsNegotiationRequired { get; set; }
        public string ContractIds { get; set; }
    }

    public class ApproveBaseLineInfoSearch
    {

        public string Action { get; set; }
        public Int64? BaselineId { get; set; }
        public Int64? ApproveBaselineInfoId { get; set; }
        public Int64? TaskId { get; set; }
    }
    public class ApproveBaseLineInfo
    {
        public string Action { get; set; }
        public Int64? BaseLineId { get; set; }
        public Int64? ApproveBaselineInfoId { get; set; }
        public Int64? TaskId { get; set; }
        public string SalesPersons { get; set; }
        public string Negotiator { get; set; }
        public decimal RsSavingsSharePercent { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal BaseLineAmount { get; set; }
        public string AccountType { get; set; }
        public string BillingCycle { get; set; }
        public Int64? VendorInvoiceDay { get; set; }
        public string ModeOfReceipt { get; set; }
        public string BilledWhen { get; set; }
        public List<ApproveBaseLineDocument> ApproveBaseLineDocuments { get; set; }
        public string BaseLineItemsXml { get; set; }
        public string BaseLineItemsXmlExtra { get; set; }
        public string BaseLineItemsXmlArchived { get; set; }
        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
        public string Condition { get; set; }
        public Int64? ContractId { get; set; }
    }
    public class ApproveBaseLineDocument
    {
        public Int64? ApproveBaselineInfoDocumentId { get; set; }
        public Int64? ApproveBaselineInfoId { get; set; }
        public Int64? BaselineDocumentId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public string Type { get; set; }
    }

    public class BaseLineMainInfo
    {
        public string Action { get; set; }
        public Int64? BaselineId { get; set; }
        public Int64? BaselineMainInfoId { get; set; }
        public Int64? TaskId { get; set; }
        public string SalesPersons { get; set; }
        public string Negotiator { get; set; }
        public decimal RsSavingsSharePercentage { get; set; }
        public decimal DiscountPercentage { get; set; }
        public string AccountType { get; set; }
        public string BillingCycle { get; set; }
        public int VendorInvoiceDay { get; set; }
        public string ModeOfReceipt { get; set; }
        public string BilledWhen { get; set; }
        public BaseLineMainInfoDocument[] Documents { get; set; }
        public string Comments { get; set; }
        public Int64? UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string ProcessStatus { get; set; }
        public string SaveOrComplete { get; set; }
    }

    public class BaseLineMainInfoDocument
    {
        public Int64? BaselineDocumentId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public string Type { get; set; }
    }
    public class BaselineSupportingFilesInfoSearch
    {
        public string Action { get; set; }
        public Int64 BaseLineId { get; set; }
        public Int64 TaskId { get; set; }
    }

    public class BaseLineSupportingFilesDocuments
    {
        public Int64 BaselineSupportingFilesInfoId { get; set; }
        public Int64 BaselineSupportingFileId { get; set; }
        public Int64 BaselineSupportingFileDetailInfoId { get; set; }
        public Int64 BaselineId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
    }

    public class SearchBaseline
    {
        public string Action { get; set; }
        public string AccountNumber { get; set; }
        public string AccountManager { get; set; }
        public string ClientName { get; set; }
        public string BaselineStatus { get; set; }
        public string VendorCorporateName { get; set; }
        public string ContractNo { get; set; }
        public string PropertyName { get; set; }
        public string VendorName { get; set; }
        public string PropertyCity { get; set; }
        public string PropertyState { get; set; }
        public Int64? BaselineId { get; set; }

    }

    public class Baseline
    {
        public string Action { get; set; }
        public string BaseLineId { get; set; }
        public string PropertyId { get; set; }
        public string VendorId { get; set; }
        public string BaseLineInfoId { get; set; }
        public string PropertyName { get; set; }
        public string VendorName { get; set; }
        public string salesPersonIds { get; set; }
        public string SalesPersons { get; set; }
        public string Negotiators { get; set; }
        public string NegotiatorIds { get; set; }
        public string BaselineNo { get; set; }
        public decimal? VectorSavingShare { get; set; }
        public decimal? Discount { get; set; }
        public string BillingCycle { get; set; }
        public int VendorInvoiceDay { get; set; }
        public string ModeOfReciept { get; set; }
        public string AccountType { get; set; }
        public string BilledWhen { get; set; }
        public Int64? VendorContactId { get; set; }
        public string LineitemXml { get; set; }
        public string LineitemXmlArchive { get; set; }
        public string LineitemXmlExtras { get; set; }
        public string TempFolderName { get; set; }
        public string DocumentXml { get; set; }
        public List<Documents> Documents { get; set; }
        public Int64? TaskId { get; set; }
        public string Comments { get; set; } 
        public Boolean IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }

    }
 
    public class Documents
    {
        public string fileName { get; set; }
        public string tempFolderName { get; set; }

    }

}
