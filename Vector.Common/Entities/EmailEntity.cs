using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vector.Common.BusinessLayer;
using static Vector.Common.BusinessLayer.EmailTemplateEnums;

namespace Vector.Common.Entities
{
    public class EmailDetails : DisposeLogic
    {
        public string Action { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Signature { get; set; }
        public List<Files> Files { get; set; }
        public string AttachmentsFolderPath { get; set; }
        public bool IsTempFolder { get; set; }
    }

    public class Files
    {
        public string fileName { get; set; }
        public string filePath { get; set; }
        public string fileType { get; set; }
        public string fileUrl { get; set; }
        public bool isLocal { get; set; }
        public bool isFilePathUrl { get; set; }
    }

    public class SendEmail : DisposeLogic
    {
        public string To { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string AttachmentsFolderPath { get; set; }
        public bool IsTempFolder { get; set; }
        public List<Files> EmailAttachments { get; set; }

        public string FromEmail { get; set; }
    }

    public class EmailResult : DisposeLogic
    {
        public bool Result { get; set; }
        public string ResultDesc { get; set; }
    }

    public class NegotiationSendEmail : DisposeLogic
    {
        public string To { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string AttachmentsFolderPath { get; set; }
        public bool IsTempFolder { get; set; }
        public List<Files> EmailAttachments { get; set; }

        public string Uidl { get; set; }
    }

    public class SendEmailNegotiation : DisposeLogic
    {
        public string To { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string AttachmentsFolderPath { get; set; }
        public bool IsTempFolder { get; set; }
        public List<Files> EmailAttachments { get; set; }

        public string Uidl { get; set; }
        public string FromEmail { get; set; }

        public string negotiationId { get; set; }

        public string vendorId { get; set; }
    }


    public class DateEntity
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string TermMonths { get; set; }
    }



    public class AssignTo
    {
        public string Action { get; set; }
        public Int64 UserId { get; set; }
        public string SelectedIds { get; set; }
        public Int64? uniqueId { get; set; }
    }

    public class CommonTask
    {
        public string Action { get; set; }
        public string InventoryType { get; set; }
        public Int64 InventoryId { get; set; }

        public string InventoryValue { get; set; }
        public Int64? TaskId { get; set; }

        public Boolean IsFurtherAction { get; set;}
        public string Comments { get; set; }
    }
}
