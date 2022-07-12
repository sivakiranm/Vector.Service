using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector.Garage.Entities
{
    public class ClientPayAccountInfo
    {
        public string Action { get; set; }
        public Int64? ClientPayAccountId { get; set; }
        public Int64? ClientId { get; set; }
        public string AbaRoutingNumber { get; set; }
        public string Currency { get; set; } 
        public string PayAccountNumber { get; set; }
        public string PayAccountType { get; set; }
        public string BankName { get; set; }
        public Int64? DisbursementAccountId { get; set; }
        public string DisbursementAccountNumber { get; set; } 
        public string BankId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string PayAccountStatus { get; set; }
        public string CheckDocNumber { get; set; }
        public string CheckPrefix { get; set; }
        public Int64? LastSequenceNumber { get; set; }
        public Int64? NextSequenceNumber { get; set; }
        public string MessageToPrint { get; set; }
        public Int64? TaskId { get; set; }
        public Int64? UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }

    public class PayFileInfo
    {
        public string Action { get; set; }
        public Int64? DisbursementAccountId { get; set; }
        public string Currency { get; set; }
        public string ConsolidatedInvoiceIds { get; set; }
        public string ConsolidatedInvoiceDetailIds { get; set; }
        public Int64? TaskId { get; set; }
        public Int64? UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }

    public class SearchPayFile
    {
        public string Action { get; set; }
        public string status { get; set; } 
        public Int64? TaskId { get; set; }
        public Int64? UserId { get; set; }
        public Int64? PayFileId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }

    public class ManagePayFile
    {
        public string Action { get; set; }
        public Int64 PayFileId { get; set; }
        public string Comments { get; set; }
        public string PayFileDetailIds { get; set; }
        public Int64? TaskId { get; set; }
        public Int64? UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }

    public class EletronicTransaction
    {
        public string Action { get; set; }
        public Int64? PayFileDetailId { get; set; }
        public string Type { get; set; }
        public string Comments { get; set; }
        public Int64? TaskId { get; set; }
        public Int64? UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }
}
