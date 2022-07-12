using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector.Garage.Entities
{
    public class ExceptionsSearch
    {
        public string Action { get; set; }
        public string BatchId { get; set; }
        public string ExpType { get; set; }
        public string ExpPriority { get; set; }
        public string ExpStatus { get; set; }
        public Int64? ClientID { get; set; }
        public Int64? PropertyId { get; set; }
        public Int64? VendorId { get; set; }
        public Int64? InvoiceId { get; set; }
        public Int64? AccountId{ get; set; }
        public string AssigneTo { get; set; }
        public string RaisedFromDate { get; set; }
        public string RaisedToDate { get; set; }
        public string ClosedFromDate { get; set; }
        public string ClosedToDate { get; set; }
        public bool IsIncludeDuplicateBill { get; set; }
        public string InvoiceDateFrom { get; set; }
        public string InvoiceDateTo { get; set; }
        public string SalesPerson { get; set; }
        public bool UnassignedExceptions { get; set; }
        public string AccountExecutive { get; set; }
        public string BillingAnalyst  { get; set; }
        public string ExceptionDescription { get; set; }
        public string Aging { get; set; }
        public string ExceptionStatusName { get; set; }

    }
    public class CreateException
    {
        public Int64? BatchDetailId { get; set; }
        public Int64? InvoiceId { get; set; }
        public string InvoiceNbr { get; set; }
        public string InvoiceDate { get; set; }
        public string DocId { get; set; }
        public Int64? AccountId { get; set; }
        public string AcctNbr { get; set; }
        public Int64? PropertyId { get; set; }
        public string ProPertyName { get; set; }
        public Int64? VendorId { get; set; }
        public string VendorName { get; set; }
        public Int64? ClientId { get; set; }
        public string ClientName { get; set; }
        public string ExceptionType { get; set; }
        public string ExceptionDesc { get; set; }
        public string ExceptionCategory { get; set; }
        public string BillingAnalyst { get; set; }
        public string Comments { get; set; }
        public bool? ImpComment { get; set; }
        public DateTime? DueDate { get; set; }
        public Int64? LoginId { get; set; }
        public Int64? TaskId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
        public int Result { get; set; }
        public string ReOpenResult { get; set; }
        public Int64? ExceptionId { get; set; }
    }


    public class ExceptionResult
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string errorlog { get; set; }
        public string emails { get; set; }
    }

    public class ExceptionTicketEntity
    {
        public Int64? ticketId { get; set; }
        public Int64?  taskid { get; set; }
        public string type { get; set; }
        public string action { get; set; }
        public string subject { get; set; }
        public string description { get; set; }
        public Int64? manifestId { get; set; }
        public Int64? clientId { get; set; }
        public Int64? propertyId { get; set; }
        public Int64? accountId { get; set; }
        public Int64? vendorId { get; set; }
        public Int64? batchDetailid { get; set; }
        public Int64? invoiceId { get; set; }       
        
        public Int64? exceptionId { get; set; }
        public string comments { get; set; }
        public string accountNumber { get; set; }
        public string propertyName { get; set; }
        public string clientName { get; set; }
        public string vendorName { get; set; }

    }
}
