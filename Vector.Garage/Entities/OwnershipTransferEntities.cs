using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector.Garage.Entities
{
    public class OwnerShipTransferEntity
    { 
        public Int64 FromClientId  { get; set; }
        public Int64 ToClientId { get; set; }
        public Int64 PropertyId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
        public string BillingEmail { get; set; }
        public string BillingPhone { get; set; }
        public string BillingMobile { get; set; }
        public DateTime? PropertyTransferDate { get; set; } 
        public string Comments { get; set; }
        public Int64? TaskId { get; set; }
        public int? IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }

    public class OwnershipTransferLogEmail
    {
        public string Action { get; set; }
        public Int64 OwnerShipTransferId { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string CCEmail { get; set; }
        public string BCCEmail { get; set; }
        public string Body { get; set; }
        public string BodyHtml { get; set; }
        public string EmlFilePath { get; set; }
        public string EmlFileName { get; set; }
        public string EmlFileUniqueId { get; set; }
        public string Comments { get; set; }
        public Int64 UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }

    public class OTClientApprovalRequest {
        public string Action { get; set; }
        public string OwnerShipTransferId { get; set; }
        public string NewClientId { get; set; }
        public string NewClientName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string Comments { get; set; }
        public string TaskId { get; set; }
        public string UserId { get; set; }
        public string IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
        public string ClientApproval { get; set; }

    }
}
