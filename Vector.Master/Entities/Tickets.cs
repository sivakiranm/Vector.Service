using System;
using System.Collections.Generic;
using System.Text;

namespace Vector.Master.Entities
{
    public class Tickets
    {
        public List<Ticket> TicketsInfo { get; set; }
    }

    public class Ticket
    {
        public Int64 TicketId{ get; set; }
        public string TicketNumber { get; set; }
        public string TicketSubject { get; set; }
        public string TicketAssignedTo { get; set; }
        public DateTime DueDate { get; set; }
        public string DueDateVF { get; set; }
        public string TicketStatus { get; set; }
        public string TicketStatusCode { get; set; }
        public string TicketStatusId { get; set; }
        public string TicketCreatedBy { get; set; }
        public string TicketCreatedDate { get; set; } 
        public Tasks TicketTasks { get; set; }

    }

    public class Tasks
    {
        public List<Task> TasksInfo { get; set; }
    }

    public class Task
    {
        public Int64 TaskId { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public string TaskAssigned { get; set; }
        public string TaskDueDate { get; set; }
        public string TaskStatus { get; set; }
        public string TaskAssignedTo { get; set; }
        public string TaskCreatedBy { get; set; }
        public DateTime TaskCreatedDate { get; set; }
        public string TaskCreatedDateVF { get; set; }

    }
}
 