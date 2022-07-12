using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vector.Common.Entities;

namespace Vector.Garage.Entities
{
    public class ContractSearch
    {
        public string Action { get; set; }
        public string NegotiationId { get; set; }
        public string PropertyId { get; set; }
        public string VendorId { get; set; }
        public string ClientId { get; set; }
        public string ContractId { get; set; }
        public string ContractInfoId { get; set; }
        public string TaskId { get; set; }
        public string Negotiation { get; set; }
        public string Property { get; set; }
        public string Vendor { get; set; }
        public string Client { get; set; }
        public string ContractNo { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string Address { get; set; }

    }

    public class ContractData
    {
        public string Action { get; set; }
        public Int32 ContractId { get; set; }
        public Int32 ContractInfoId { get; set; }
        public Int32 TaskId { get; set; }
    }

    public class ApproveOrDeclineAnnualIncrease
    {
        public string Action { get; set; }
        public Int32 ContractId { get; set; }
        public Int32 ContractApproveAnnualIncrementInfoId { get; set; }
        public Int32 TaskId { get; set; }
        public string LineItems { get; set; }
        public string Comments { get; set; }
        public Int32 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }

    public class Contract
    {
        public string Action { get; set; }
        public Int32 ContractId { get; set; }
        public Int32 ContractInfoId { get; set; }
        public Int32 TaskId { get; set; }
        public Int32 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
        public string ContractNo { get; set; }
        public Int32 ClientId { get; set; }
        public string Client { get; set; }
        public Int32 PropertyId { get; set; }
        public string Property { get; set; }
        public Int32 VendorId { get; set; }
        public string Vendor { get; set; }
        public Int32 NegotiationId { get; set; }
        public string Negotiation { get; set; }
        public string ContractStatus { get; set; }
        public object Contacts { get; set; }

        public string ContractBeginDate { get; set; }
        public string ContractEndDate { get; set; }
        public string ContractTerm { get; set; }
        public string RenewalDate { get; set; }
        public string RenewalType { get; set; }
        public ContractApproval ContractVendorApproval { get; set; }
        public ContractApproval ContractClientApproval { get; set; }
        public ContractApproval ContractVectorApproval { get; set; }
        public string IsVendorApproved { get; set; }
        public string IsClientApproved { get; set; }
        public string IsVectorApproved { get; set; }
        public bool IsVendorTransmitted { get; set; }
        public bool IsClientTransmitted { get; set; }
        public bool IsVectorTransmitted { get; set; }
        public string ContractVersion { get; set; }
        public string RevenueStream { get; set; }
        public string BasisOfManagementFee { get; set; }
        public string ManagementFee { get; set; }
        public string DiscountPer { get; set; }
        public string ClientContractBeginDate { get; set; }
        public string ClientContractEndDate { get; set; }
        public string ClientContractTerm { get; set; }
        public string ClientPaymentTerm { get; set; }
        public string ClientContractRenewalTeam { get; set; }
        public string ApprovedAnnualIncrease { get; set; }
        public string ChangeNeedingClientApproval { get; set; }
        public string SalesPerson { get; set; }
        public string AccountExecutive { get; set; }
        public string ReceiveVendorInvoices { get; set; }
        public string ClientApprovalChanges { get; set; }
        public string RSSavingSharePer { get; set; }
        public string NegotiationBeginDate { get; set; }
        public string NegotiationEndDate { get; set; }
        public string Negotiator { get; set; }
        public object LineItemsDetails { get; set; }
        public string LineItemDetailsXml { get; set; }
        public string Comments { get; set; }
        public object Activity { get; set; }

        public object Documents { get; set; }
        public object Contracts { get; set; }
        public object Accounts { get; set; }
        public string TempFolderName { get; set; }

        public List<Files> FilesAttached { get; set; }

        public object AnnualIncreaseLineItems { get; set; }

        public string IsSavings { get; set; }
        public string VendorPaymentTerm { get; set; }

    }

    public class ContractApproval
    {
        public string Action { get; set; }
        public Int32 ContractId { get; set; }
        public string ContractNo { get; set; }
        public string ContractDocument { get; set; }
        public List<Files> Documents { get; set; }

        public string TempFolderName { get; set; }
        public string VendorApproved { get; set; }
        public string ClientApproved { get; set; }
        public string VectorApproved { get; set; }
        public string Comments { get; set; }
        public string LoggedInUserName { get; set; }
        public string LoggedInUserPhone { get; set; }
        public string LoggedInUserEmail { get; set; }
        public Int32 TaskId { get; set; }
        public Int32 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }


    public class ContractLineItems
    {
        public Int32 ContractId { get; set; }
        public Int32 ContractLineItemId { get; set; }
        public string LineItemsXml { get; set; }
        public string Comments { get; set; }
        public Int32 UserId { get; set; }
    }

    public class UpcomingContractExpiry {

        public string Action { get; set; }
        public string ReportBy { get; set; }
        public string ClientName { get; set; }
        public int ClientId { get; set; }
        public string PropertyName { get; set; }
        public int PropertyId { get; set; }
        public string VendorName { get; set; }
        public int VendorId { get; set; }
        public string ContractNo { get; set; }
        public int ContractId { get; set; }
        public int Negotiator { get; set; }
        public string NegotiationNo { get; set; }
        public string AccountNUmber { get; set; }
        public int AccountId { get; set; }
        public DateTime ContractEffectiveDateFrom { get; set; }
        public DateTime ContractEffectiveDateTo { get; set; }
        public int UserId { get; set; }
    }
}
