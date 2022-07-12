using System;
using System.Collections.Generic;
using System.Text;

namespace Vector.Workbench.Entities
{
  public class TicketTask
    {
        public Int64 TicketId { get; set; }
        public Int64 TaskId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string DueDate { get; set; }
        public Int64 CreatedUserId { get; set; }
        public Int64 StatusMasterId { get; set; }
        public string TicketStatus { get; set; }
        public Int64 AssignedToUserId { get; set; }
        public Int64 TicketCategoryId { get; set; }
        public string TicketChips { get; set; }
        public Int64 TypeId { get; set; }
        public Int64 ManifestId { get; set; }
        public Int64 FlowId { get; set; }
        public Int64 FlowDetailsId { get; set; }
        public Int64? manifestDetailsId { get; set; }


    }

    public class Ticket
    {
        public Int64 TicketId { get; set; }
        public Int64 CreatedUserId { get; set; }
        public string ticketSubject { get; set; }
        public string Description { get; set; }
        public string DueDate { get; set; } 
        public Int64 AssignedToUserId { get; set; }
        public Int64 TicketStatusMasterId { get; set; }
        public string Comments { get; set; }
        public string TicketChips { get; set; }
        public List<TicketDocument> TicketDocuments { get; set; }

        public int? ClientId { get; set; }
        public int? PropertyId { get; set; }
        public int? VendorId { get; set; }
        public int? AccountId { get; set; }
        public string ClientName { get; set; }
        public string PropertyName { get; set; }
        public string VendorName { get; set; }
        public string AccountNumber { get; set; }

        public string requestorEmail { get; set; }

        public string requestorName { get; set; }


        public Int64? requesterUserId { get; set; }


        public string instruction { get; set; }
        public string ticketType { get; set; }
        public int? ticketETA { get; set; }
        public Int32? ticketPriority { get; set; }
        public Int32? ticketSubCategoryId { get; set; }
        public string ticketSubCategoryName { get; set; }

        public Int64? ticketCategoryId { get; set; }
        public string irpUniqueCode { get; set; }

        public Int64? contractLineitemId { get; set; }

        public string emailUIDL { get; set; }
        public string emailAction { get; set; }
    }
    public class TicketDocument
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
    }

    public class MyQueue
    {
        public string Action { get; set; }
        public string ticketId { get; set; }
        public string taskid { get; set; }
        public string type { get; set; }
    }

    public class MyQueueInfo
    {
        public List<Tickets> Tickets { get; set; }
    }



    public class Tickets
    {
        public Int64 TicketId { get; set; }
        public string TicketNumber { get; set; }
        public string TicketSubject { get; set; }
        public string TicketCreatedUser { get; set; }
        public string TicketCreatedUserIcon { get; set; }
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int PendingTasks { get; set; }
        public int AvailableTasks { get; set; }
        public Boolean isTicketAvailableForEdit { get; set; }
        public List<Tasks> Tasks { get; set; }
    }

    public class Tasks
    {
        public string Action { get; set; }
        public string TicketId { get; set; }
        public Int64 TaskId { get; set; }
        public string TaskNumber { get; set; }
        public string TaskAssignedUser { get; set; }
        public string TaskAssignedUserIcon { get; set; }
        public string TaskName { get; set; }
        public string TaskStatus { get; set; }
        public string TaskSubject { get; set; }
        public string TaskDescription { get; set; }
        public Int32 TaskEstimatedHours { get; set; }
        public Int32 TaskSpentHours { get; set; }
        public Int32 TaskRemainingHours { get; set; }
        public Int32 TaskRemainingMin { get; set; }
        public Int32 TaskLastVisitedHours { get; set; }
        public Int32 TaskLastVisitedMin { get; set; }
        public string ComponentName { get; set; }
        public string RoutingPath { get; set; }
        public string ColorCode { get; set; }
        public Boolean isTaskAvailableForEdit { get; set; }
        public string DueDate { get; internal set; }
        public Int64 AssignedToUserId { get; internal set; }
        public Int64 ManifestId { get; set; }
        public Int64 FlowId { get; set; }
        public Int64 FlowDetailsId { get; set; }
        public Int64 existingTaskId { get; set; }

    }
}
