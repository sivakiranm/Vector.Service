
namespace Vector.Garage.Entities
{
    using System;
    using System.Collections.Generic;

    public class ClientInfo
    {
        public Int64? ClientInfoId { get; set; }
        public Int64? ClientId { get; set; }
        public string Action { get; set; }
        public string Path { get; set; }
        public string ClientNo { get; set; }
        public string ClientName { get; set; }
        public string ClientLegalName { get; set; }
        public string ContractVersion { get; set; }
        public string VectorRevenueStream { get; set; }
        public string BasicOfManagementFee { get; set; }
        public decimal? ManagementFee { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public string ClientStatus { get; set; }
        public string InActivationReason { get; set; }
        public string ClientRegions { get; set; }
        public string Vertical { get; set; }
        public string Currency { get; set; }
        public string Comments { get; set; }
        public bool? Status { get; set; }
        public Int64? TaskId { get; set; }
        public Int64? ManifestDetailsId { get; set; }
        public decimal VectorSavingShare { get; set; }
        public Int64 UserId { get; set; }
        public string SaveOrComplete { get; set; }
    }

    public class ClientInvoicePreferences
    {
        public string Action { get; set; }
        public Int64? ClientId { get; set; }
        public Int64? ClientInvoicingPreferencesId { get; set; }
        public Int64? TaskId { get; set; }
        public string SummeryRecipient { get; set; }

        public string SummaryInvoiceDeliveryMode { get; set; }
        public string SummaryInvoiceDeliveryFrequency { get; set; }
        public int? SummaryInvoiceDayOfTheMonth { get; set; }
        public bool? SummaryInvoiceIsLastDay { get; set; }
        public string SummaryInvoiceDayOfTheWeek { get; set; }
        public string SummaryInvoiceModeOfReceipt { get; set; }
        public string SummaryInvoiceSendTo { get; set; }
        public string SummaryInvoiceDeliveryInstructions { get; set; }

        public string VendorRecipient { get; set; }
        public string VendorInvoiceDeliveryMode { get; set; }
        public string VendorInvoiceDeliveryFrequency { get; set; }
        public int? VendorInvoiceDayOfTheMonth { get; set; }
        public bool VendorInvoiceIsLastDay { get; set; }
        public string VendorInvoiceDayOfTheWeek { get; set; }
        public string VendorInvoiceModeOfReceipt { get; set; }
        public string VendorInvoiceSendTo { get; set; }
        public string VendorInvoiceDeliveryInstructions { get; set; }
        public string RSRecipient { get; set; }
        public string RsInvoiceDeliveryMode { get; set; }
        public string RsInvoiceDeliveryFrequency { get; set; }
        public int? RsInvoiceDayOfTheMonth { get; set; }
        public bool? RsInvoiceIsLastDay { get; set; }
        public string RsInvoiceDayOfTheWeek { get; set; }
        public string RsInvoiceModeOfReceipt { get; set; }
        public string RsInvoiceSendTo { get; set; }
        public string RsInvoiceDeliveryInstructions { get; set; }
        public string Comments { get; set; }
        public Int64? UserId { get; set; }
        public bool? IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }

    public class ClientInvoicePreferenceSearch
    {
        public string Action { get; set; }
        public Int64 ClientId { get; set; }
        public Int64 ClientInvoicingPreferencesId { get; set; }
        public Int64 TaskId { get; set; }
    }

    public class ClientAddressInfo
    {
        public string Action { get; set; }
        public Int64 ClientId { get; set; }
        public Int64 ClientAddressInfoId { get; set; }
        public Int64 TaskId { get; set; }
        public string MailIngAddress1 { get; set; }
        public string MailIngAddress2 { get; set; }
        public string MailingCity { get; set; }
        public string MailingState { get; set; }
        public string MailingZip { get; set; }
        public string MailingCountry { get; set; }
        public string MailingPhone { get; set; }
        public string MailingPhoneExtension { get; set; }
        public string MailingMobile { get; set; }
        public string MailingEmail { get; set; }
        public string MialingFax { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
        public string BillingCountry { get; set; }
        public string BillingPhone { get; set; }
        public string BillingPhoneExtension { get; set; }
        public string BillingMobile { get; set; }
        public string BillingEmail { get; set; }
        public string BillingFax { get; set; }
        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }

    public class ClientAddressInfoSearch
    {
        public string Action { get; set; }
        public Int64 ClientId { get; set; }
        public Int64 ClientAddressInfoId { get; set; }
        public Int64 TaskId { get; set; }
    }

    public class ClientContactInfoSearch
    {

        public string Action { get; set; }
        public Int64 ClientId { get; set; }
        public Int64 ClientContactInfoId { get; set; }
        public Int64 TaskId { get; set; }
    }

    public class ClientContactRole
    {

        public string ContactType { get; set; }
        public string ContactRoleMasterName { get; set; }
        public Int64 UserId { get; set; }
    }

    public class ClientAgreementInfoSearch
    {
        public string Action { get; set; }
        public Int64 ClientId { get; set; }
        public Int64 ClientAgreementInfoId { get; set; }
        public Int64 TaskId { get; set; }
    }

    public class ClientInfoSearch
    {
        public string Action { get; set; }
        public Int64 ClientId { get; set; }
        public Int64? ClientInfoId { get; set; }
        public Int64 TaskId { get; set; }
    }

    public class Region
    {

        public string RegionName { get; set; }
        public string RegionAddedPage { get; set; }
        public Int64 UserId { get; set; }
    }
    public class Vertical
    {

        public string VerticalName { get; set; }
        public string VerticalAddedPage { get; set; }
        public Int64 UserId { get; set; }
    }
    public class ClientAgreementInfo
    {
        public string Action { get; set; }
        public Int64 ClientId { get; set; }
        public Int64 ClientAgreementInfoId { get; set; }
        public Int64 TaskId { get; set; }
        public Int64 TermMonths { get; set; }
        public string PaymentTerms { get; set; }
        public string RenewalTerms { get; set; }
        public string RenewalType { get; set; }
        public string AgreementVenue { get; set; }
        public string ChangesNeededClientApproval { get; set; }
        public string CanVectorExecuteVendorContracts { get; set; }
        public string RetroActiveAuditAllowed { get; set; }
        public int TermToNegotiateAndProcessInvoices { get; set; }
        public decimal OutClauseForVectorClientAgreement { get; set; }
        public string NameOnVendorContract { get; set; }
        public string NameOnVendorInvoice { get; set; }
        public string NameOnRSInvoice { get; set; }
        public string LOAEditsAllowed { get; set; }
        public string PartialPortfolioAgreement { get; set; }
        public string ProcessInvoicesPriorToNegotiation { get; set; }
        public string ManageBill { get; set; }
        public DateTime? StartDate { get; set; }

        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }

    public class ClientContactInfo
    {

        public string Action { get; set; }
        public Int64 ClientId { get; set; }
        public Int64 TaskId { get; set; }
        public string ContactsXml { get; set; }
        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }

    public class ClientContractInfo
    {
        public string Action { get; set; }
        public Int64 ClientId { get; set; }
        public Int64 ClientContractInfoId { get; set; }
        public Int64 TaskId { get; set; }
        public DateTime? ClientContractBeginDate { get; set; }
        public DateTime? ClientContractEndDate { get; set; }
        public DateTime? DateOfSigning { get; set; }
        public string PaymentTerms { get; set; }
        public string RenewalTerms { get; set; }
        public int TermMonths { get; set; }

        public string SalesPersons { get; set; }
        public string Negotiations { get; set; }

        public string AccountExecutives { get; set; }
        public decimal RsSavingsSharePercent { get; set; }
        public decimal ApprovedAnnualIncrease { get; set; }
        public string Status { get; set; }
        public Int64 TicketId { get; set; }
        public Int64 ManifestId { get; set; }
        public Int64 ManifestDetailsId { get; set; }
        public string CreatedDate { get; set; }
        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
        public List<ClientDocuments> ClientDocuments { get; set; }
    }

    public class FileModel
    {
        public string TypeName { get; set; }
        public string Type { get; set; }
        // public IFormFile FormFile { get; set; }
        // public List<IFormFile> FormFiles { get; set; }
    }


    public class ClientDocuments
    {
        public Int64 ClientDocumentId { get; set; }
        public Int64 ClientContractInfoDocumentsID { get; set; }
        public Int64 ClientId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public string Version { get; set; }
        public string ChooseDocumentType { get; set; }
        public string Status { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedUserName { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedUserName { get; set; }
    }

    public class DealPackage
    {
        public string Action { get; set; }
        public string ClientName { get; set; }
        public string ClientNbr { get; set; }
        public int ClientId { get; set; }
        public string Comments { get; set; }
        public string FolderName { get; set; }
        public string FileName { get; set; }
        public bool IsTempFolder { get; set; } 
        public Int64 UserId { get; set; }
        public Int64 TaskId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }
}
