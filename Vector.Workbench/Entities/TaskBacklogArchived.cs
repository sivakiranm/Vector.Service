using System;
using System.Collections.Generic;
using System.Text;

namespace Vector.Workbench.Entities
{

    public class TaskBacklogArchivedInformation
    {
        public List<TaskBacklogArchived> data { get; set; }
    }

    public class TaskBacklogArchived
    {

        public string ticketId { get; set; }
        public string ticketNumber { get; set; }
        public string ticketTitle { get; set; }
        public string ticketDescription { get; set; }
        public string ticketCreatedBy { get; set; }
        public string ticketAssignedTo { get; set; }
        public string ticketCreatedDate { get; set; }
        public string ticketDueDate { get; set; }
        public bool isTicketPinned { get; set; }
        public Int64 messageCount { get; set; }
        public string ticketStatus { get; set; }
        public string profileImage { get; set; }
        public List<TaskBacklogArchivedticketTasks> ticketTasks { get; set; }

    }

    public class TaskBacklogArchivedticketTasks
    {
        public string taskId { get; set; }
        public string taskNumber { get; set; }
        public string ticketId { get; set; }
        public string taskCreatedBy { get; set; }
        public string taskCreatedUserAvatar { get; set; }
        public string taskAssignedTo { get; set; }
        public string taskCreatedDate { get; set; }
        public bool isTicketPinned { get; set; }
        public string taskStatus { get; set; }
        public string taskResolvedBy { get; set; }
        public string taskAssignedUserAvatar { get; set; }
        public string taskTitle { get; set; }
        public string taskDescription { get; set; }
        public string taskDueDate { get; set; }
    }

    public class TaskbacklogSchedulerInfo
    {
        public List<TaskbacklogScheduler> data { get; set; }
    }

    public class TaskbacklogScheduler
    {

        public string Status { get; set; }
        public string createdUser { get; set; }
        public string createdUserAvatar { get; set; }
        public Int64 id { get; set; }
        public string start { get; set; }
        public string taskDueDate { get; set; }
        public Int64 taskId { get; set; }
        public string taskNumber { get; set; }
        public string taskTitle { get; set; }
        public string title { get; set; }

    }

    public class TaskInfo
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignTo { get; set; }
        public string DueOn { get; set; }
        public string userId { get; set; }
        public string userName { get; set; }
    }

    public class ActivityLogDetailsInformation
    {
        public List<ActivityLogDetailsInformationList> ActivityLogDetailsInfo { get; set; }
    }

    public class ActivityLogDetails
    {
        public string TicketId { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public string Category { get; set; }
        public string DueDate { get; set; }
        public string Status { get; set; }
        public string TaskType { get; set; }
        public string Notes { get; set; }
        public string Tag { get; set; }
        public string EmailID { get; set; }
        public string createdUserAvatar { get; set; }
        public string userId { get; set; }
        public string userName { get; set; }
    }

    public class ActivityLogDetailsInformationList
    {
        public string AssignedTo { get; set; }
        public string Tag { get; set; }
        public string EmailID { get; set; }
        public string createdUserAvatar { get; set; }
    }

    public class RescheduleTask
    {
        public string ticketId { get; set; }
        public string taskId { get; set; }
        public string Title { get; set; }
        public string DueDate { get; set; }
        public string userId { get; set; }
        public string userName { get; set; }
    }

}
