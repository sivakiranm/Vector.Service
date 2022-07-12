using System;
using System.Collections.Generic;
using System.Text;

namespace Vector.Workbench.Entities
{

    public class TicketsInformation
    {
        public List<TicketInfo> data { get; set; }
    }

    public class TicketInfo
    {
        public string Action { get; set; }
        public Int64 TicketId { get; set; }
        public string TicketSubject { get; set; }
        public string Description { get; set; }

        public DateTime? DueDate { get; set; }
        public string assignedUserName { get; set; }
        public string assignedUserIcon { get; set; }
        public Int64? assignedUserId { get; set; }

        public Int64 UserId { get; set; }
        public string comments { get; set; }
        public string TicketStatus { get; set; }
        public int? TicketStatusId { get; set; }
        public string Tags { get; set; }
        public string ticketDocuments { get; set; }
        public int? ticketCategoryId { get; set; }
        public string ticketCategoryName { get; set; }
        public int? ClientId { get; set; }
        public int? PropertyId { get; set; }
        public int? VendorId { get; set; }
        public int? AccountId { get; set; }
        public string ClientName { get; set; }
        public string PropertyName { get; set; }
        public string VendorName { get; set; }
        public string AccountNumber { get; set; }
        public string RequestorName { get; set; }
        public string RequestorEmail { get; set; }
        public string MessageId { get; set; }

        public Int32? ticketSubCategoryId { get; set; }

    }

    public class TicketRequestorEmail
    {
        public string TicketId { get; set; }
        public string TicketNumber { get; set; }
        public string RequestorName { get; set; }
        public string RequestorEmail { get; set; }
        public string MessageId { get; set; }
        public string Subject { get; set; }
    }

    public class TicketTags
    {
        public Int64? TagId { get; set; }
        public string TagType { get; set; }
        public Int64 TagValueId { get; set; }
        public string TagValue { get; set; }
    }

    public class TicketDocuments
    {
        public Int64? DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
    }



}
