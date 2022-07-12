using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector.Garage.Entities
{
    public class ConsolidatedInvoiceInfo
    {
        public string Action { get; set; }
        public Int64? ClientId { get; set; }
        public string ConsolidateBy { get; set; }
        public DateTime? ConsolidatedDate { get; set; }
        public DateTime? FundDate { get; set; }
        public string Currency { get; set; }
        public Int64? TaskId { get; set; }
        public Int64? UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }

    public class ConsolidatedInvoiceSearch
    {
        public string Action { get; set; }
        public string clientName { get; set; }
        public string propertyName { get; set; }
        public Int64? consolidatedInvoiceId { get; set; }
        public string consolidatedInvoiceName { get; set; }
        public string consolidatedInvoiceRage { get; set; }
        public string status { get; set; }
        public Int64? TaskId { get; set; }
        public Int64? UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }

        public string vectorInvoiceNo { get; set; }
        public string invoiceNumber { get; set; }
    }

    public class FundApproveCI
    {
        public string Action { get; set; }
        public Int64? ConsolidatedInvoiceId { get; set; }
        public Int64? InvoiceId { get; set; }
        public string Comments { get; set; }
        public Int64? TaskId { get; set; }
        public Int64? UserId { get; set; }
    }

    public class RejectCITransactions
    {
        public string Action { get; set; }
        public Int64? ConsolidatedInvoiceId { get; set; }
        public string Transactions { get; set; } 
        public Int64? TaskId { get; set; }
        public Int64? UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }

    public class PartiallyFundCITransactions
    {
        public string Action { get; set; }
        public Int64? ConsolidatedInvoiceId { get; set; }
        public string Transactions { get; set; }
        public Int64? TaskId { get; set; }
        public Int64? UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
        public string comments { get; set; }
    }
} 
