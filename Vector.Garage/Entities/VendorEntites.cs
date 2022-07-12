using System;
using System.Collections.Generic;
using System.Text;

namespace Vector.Garage.Entities
{
    public class VendorInfo
    {
        public string Action { get; set; }
        public Int64 VendorId { get; set; }
        public Int64 VendorInfoId { get; set; }
        public Int64 VendorCorporateId { get; set; }
        public Int64 TaskId { get; set; }
        public string VendorName { get; set; }
        public string VendorStatus { get; set; }
        public string BillingAnalyst { get; set; }
        public string Negotiator { get; set; }
        public string invoiceRecipients { get; set; }
        public string ModeOfInvoiceReciept { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ContractName { get; set; }
        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
        public List<VendorDocuments> VendorDocuments { get; set; }

    }

    public class VendorInfoSearch
    {
        public string Action { get; set; }
        public Int64 VendorId { get; set; }
        public Int64 VendorInfoId { get; set; }
        public Int64 TaskId { get; set; }
        public string VendorName { get; set; }
    }
    public class VendorCorporateContactInfoSearch
    {

        public string Action { get; set; }
        public Int64 VendorCorporateId { get; set; }
        public Int64 VendorCorporateContactInfoId { get; set; }
        public Int64 TaskId { get; set; }
    }

    public class VendorCorporateContactInfo
    {

        public string Action { get; set; }
        public Int64 VendorCorporateId { get; set; }
        public Int64 TaskId { get; set; }
        public string ContactsXml { get; set; }
        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }

    public class VendorCorporateInfo
    {

        public string Action { get; set; }
        public Int64 VendorCorporateId { get; set; }
        public Int64 VendorCorporateInfoId { get; set; }
        public Int64 TaskId { get; set; }
        public string VendorCorporateName { get; set; }
        public string ModeOfInvoiceReceipt { get; set; }
        public string PaymentType { get; set; }
        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }
    public class VendorCorporateAddressInfo
    {
        public string Action { get; set; }
        public Int64 VendorCorporateId { get; set; }
        public Int64 VendorCorporateAddressInfoId { get; set; }
        public Int64 TaskId { get; set; }
        public string PhysicalAddress1 { get; set; }
        public string PhysicalAddress2 { get; set; }
        public string PhysicalCity { get; set; }
        public string PhysicalState { get; set; }
        public string PhysicalZip { get; set; }
        public string PhysicalCountry { get; set; }
        public string PhysicalPhone { get; set; }
        public string PhysicalPhoneExtension { get; set; }
        public string PhysicalEmail { get; set; }
        public string PhysicalFax { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
        public string BillingCountry { get; set; }
        public string BillingPhone { get; set; }
        public string BillingPhoneExtension { get; set; }
        public string BillingEmail { get; set; }
        public string BillingFax { get; set; }
        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }

    public class VendorContactInfoSearch
    {

        public string Action { get; set; }

        public Int64 VendorId { get; set; }
        public Int64 vendorContactInfoId { get; set; }
        public Int64 TaskId { get; set; }
    }

    public class VendorContactInfo
    {

        public string Action { get; set; }
        public Int64 VendorId { get; set; }
        public Int64 TaskId { get; set; }
        public string ContactsXml { get; set; }
        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }


    public class VendorCorporateAddressInfoSearch
    {
        public string Action { get; set; }
        public Int64 VendorCorporateId { get; set; }
        public Int64 VendorCorporateAddressInfoId { get; set; }
        public Int64 TaskId { get; set; }
    }
    public class VendorAddressInfo
    {
        public string Action { get; set; }
        public Int64 VendorId { get; set; }
        public Int64 VendorAddressInfoId { get; set; }
        public Int64 TaskId { get; set; }
        public string PhysicalAddress1 { get; set; }
        public string PhysicalAddress2 { get; set; }
        public string PhysicalCity { get; set; }
        public string PhysicalState { get; set; }
        public string PhysicalZip { get; set; }
        public string PhysicalCountry { get; set; }
        public string PhysicalPhone { get; set; }
        public string PhysicalPhoneExtension { get; set; }
        public string PhysicalEmail { get; set; }
        public string PhysicalFax { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
        public string BillingCountry { get; set; }
        public string BillingPhone { get; set; }
        public string BillingPhoneExtension { get; set; }
        public string BillingEmail { get; set; }
        public string BillingFax { get; set; }
        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }


    public class VendorAddressInfoSearch
    {
        public string Action { get; set; }
        public Int64 VendorId { get; set; }
        public Int64 VendorAddressInfoId { get; set; }
        public Int64 TaskId { get; set; }
    }
    public class VendorPaymentInfoSearch
    {
        public string Action { get; set; }
        public Int64 VendorId { get; set; }
        public Int64 VendorPaymentInfoId { get; set; }
        public Int64 TaskId { get; set; }
    }
    public class VendorPaymentInfo
    {
        public string Action { get; set; }
        public Int64 VendorId { get; set; }
        public Int64 VendorPaymentInfoId { get; set; }
        public Int64 TaskId { get; set; }
        public string PaymentType { get; set; }
        public string AchXml { get; set; }
        public string PCardXml { get; set; }
        public string CheckXml { get; set; }
        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }

    }

    public class VendorCorporatePaymentInfoSearch
    {
        public string Action { get; set; }
        public Int64 VendorCorporateId { get; set; }
        public Int64 VendorCorporatePaymentInfoId { get; set; }
        public Int64 TaskId { get; set; }
    }
    public class VendorCorporatePaymentInfo
    {
        public string Action { get; set; }
        public Int64 VendorCorporateId { get; set; }
        public Int64 VendorCorporatePaymentInfoId { get; set; }
        public Int64 TaskId { get; set; }
        public string PaymentType { get; set; }
        public string AchXml { get; set; }
        public string PCardXml { get; set; }
        public string CheckXml { get; set; }
        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }

    }
    public class VendorDocuments
    {
        public Int64 VendorInfoId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Status { get; set; }
        public string ContractName { get; set; }
    }
}
