using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector.IntacctManager.Entities
{
    public class IntacctException
    {
        public string CustomerKey { get; set; }
        public string Intacct { get; set; }
        public string EntityType { get; set; }
        public string ErrorDescription { get; set; }
        public string IntacctSession { get; set; }
        public string SystemIp { get; set; }
        public string ErrorType { get; set; }
        public string Xml { get; set; }
        public string UserId { get; set; }
    }

    public class PaymentReceipt
    {
        public string PaymentQBID { get; set; }
        public string Comments { get; set; }
        public string PaymentType { get; set; }
        public DateTime DateUpdated { get; set; }
        public double TotalAmount { get; set; }
        public string InvoiceDetailsXML { get; set; }
        public string Id { get; set; }
    }
}
