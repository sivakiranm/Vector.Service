using System;
using System.Collections.Generic;
using System.Text;

namespace Vector.Garage.Entities
{
    public class PropertyInfo
    {
        public string Action { get; set; }
        public Int64 PropertyId { get; set; }
        public Int64 PropertyInfoId { get; set; }
        public Int64 TaskId { get; set; }
        public Int64 ClientId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyNo { get; set; }
        public string PropertyLegalName { get; set; }
        public string PropertyLedgerNo { get; set; }

        public Int64 RegionId { get; set; }
        public string Region { get; set; }
        public string PropertyStatus { get; set; }
        public string OwnershipTransfer { get; set; }
        public string TransferType { get; set; }
        public string TransferFromTo { get; set; }
        public Int64 TransferFromToClientId { get; set; }
        public DateTime? TransferDate { get; set; }
        public string MarketType { get; set; }
        public string DocumentType { get; set; }
        public string Status { get; set; }
        public string Progress { get; set; }
        public string PropertyType { get; set; }
        public string PropertyDocumentXml { get; set; }

        public List<ClientDocuments> PropertyDocuments { get; set; }

        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }

        public string tempFolderName { get; set; }
    }
    public class PropertyInfoSearch
    {
        public string Action { get; set; }
        public Int64 PropertyId { get; set; }
        public Int64 PropertyInfoId { get; set; }
        public Int64 TaskId { get; set; }
    }
    public class PropertyAddressInfo
    {
        public string Action { get; set; }
        public Int64 PropertyId { get; set; }
        public Int64 ProeprtyAddressInfoId { get; set; }
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

    public class PropertyAddressInfoSearch
    {
        public string Action { get; set; }
        public Int64 PropertyId { get; set; }
        public Int64 PropertyAddressInfoId { get; set; }
        public Int64 TaskId { get; set; }
    }

    public class PropertyInvoicePreferenceSearch
    {
        public string Action { get; set; }
        public Int64 PropertyId { get; set; }
        public Int64 PropertyInvoicingPreferencesId { get; set; }
        public Int64 TaskId { get; set; }
    }

    public class PropertyInvoicePreferences
    {
        public string Action { get; set; }
        public Int64? PropertyId { get; set; }
        public Int64? PropertyInvoicingPreferencesId { get; set; }
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
        public string RsInvoiceDeliveryMode { get; set; }
        public string RSRecipient { get; set; }
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

    public class PropertyContactInfoSearch
    {

        public string Action { get; set; }
        public Int64 PropertyId { get; set; }
        public Int64 PropertyContactInfoId { get; set; }
        public Int64 TaskId { get; set; }
    }

    public class PropertyContactInfo
    {

        public string Action { get; set; }
        public Int64 PropertyId { get; set; }
        public Int64 TaskId { get; set; }
        public string ContactsXml { get; set; }
        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }

    public class PropertyMiscInfo {
        public string Action { get; set; }
        public Int64 PropertyId { get; set; }
        public Int64 PropertyMiscellaneousInfoId { get; set; }
        public Int64 TaskId { get; set; }
        public Int64 RsSavingSharePercentage { get; set; }
        public Int64 BaselineVendorId { get; set; }
        public string Attributes { get; set; }
        public string AttributeTotal { get; set; }
        public string Occupied { get; set; }
        public string ExistingVendorContract { get; set; }
        public string ExistingVendorContractExpDate { get; set; }
        public Int64 Addendum { get; set; }
        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
        public string SalesPersons { get; set; }

        public string AccountExecutives { get; set; }

    }

    public class PropertyMiscInfoSearch
    {

        public string Action { get; set; }
        public Int64? PropertyId { get; set; }
        public Int64? PropertyMiscellaneousInfoId { get; set; }
        public Int64? TaskId { get; set; }
    }

    public class PropertyContractInfoSearch
    {

        public string Action { get; set; }
        public Int64 PropertyId { get; set; }
        public Int64 PropertyContractInfoId { get; set; }
        public Int64 TaskId { get; set; }
    }
    public class PropertyContractInfo
    {
        public string Action { get; set; }
        public Int64 PropertyId { get; set; }
        public Int64 PropertyContractInfoId { get; set; }
        public Int64 TaskId { get; set; }
        public string ContractVersion { get; set; }
        public string VectorRevenueStream { get; set; }
        public string BasisOfMgmtFee { get; set; }
        public decimal ManagementFee { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal RsSharingPercentage { get; set; }
        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public string IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }

    }

    public class PropertyBaselineinvoices
    {

        public string Action { get; set; }
        public Int64 PropertyId { get; set; } 
        public Int64 TaskId { get; set; }
    }

    public class BaselineInvoiceInfo
    {

        public string Action { get; set; }
        public Int64 PropertyId { get; set; }
        public Int64 TaskId { get; set; }

        public decimal baseLineAmount { get; set; }
        public Int32 baselineId { get; set; }
        public Int32 baselineVendorId { get; set; }
        public string baselineVendorName { get; set; }
        public DateTime existingContractExpiryDate { get; set; }
        public Int32 nonRenualWindowMin { get; set; }
        public Int32 nonRenualWindowMix { get; set; }
        public Int64 propertyBaseLineInvoicesId { get; set; }
        public string propertyName { get; set; }
        public string renualType { get; set; }
        public string serviceType { get; set; }
        public Int64? propertyId { get; set; }
        public string documents { get; set; }
    }

    public class PropertyBaselineDocuments
    {

        public string Action { get; set; }
        public string documents { get; set; }
        public Int64? propertyId { get; set; }
        public Int64? taskId { get; set; }
        public string IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }


    public class PropertySearch
    {
        public string Action { get; set; }
        public Int64 PropertyId { get; set; }
        public Int64 PropertyInfoId { get; set; }
        public Int64 TaskId { get; set; }
        public Int64 ClientId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyNo { get; set; }
        public string PropertyLegalName { get; set; }
        public string PropertyLedgerNo { get; set; }

        public Int64 RegionId { get; set; }
        public string Region { get; set; }
        public string PropertyStatus { get; set; }
        public string OwnershipTransfer { get; set; }
        public string TransferType { get; set; }
        public string TransferFromTo { get; set; }
        public Int64 TransferFromToClientId { get; set; }
        public string TransferDate { get; set; }
        public string MarketType { get; set; }
        public string DocumentType { get; set; }
        public string Status { get; set; }
        public string Progress { get; set; }
        public string PropertyType { get; set; }
        public List<ClientDocuments> PropertyDocuments { get; set; }
        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }

        public string city { get; set; }
        public string state { get; set; }
        public string zip{ get; set; }
        public string Address { get; set; }
        public string VendorName { get; set; }
        public string AccountNo { get; set; }

    }
}
