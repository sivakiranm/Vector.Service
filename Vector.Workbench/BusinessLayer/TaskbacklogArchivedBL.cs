using System;
using System.Collections.Generic;
using Vector.Common.Entities;
using Vector.Workbench.Entities;

namespace Vector.Workbench.BusinessLayer
{
    public class TaskbacklogArchivedBL
    {

        public TaskBacklogArchivedInformation GetTasksBacklog(string userId, string userName)
        {

            TaskBacklogArchivedInformation taskbacklogarchivedinformation = null;
            List<TaskBacklogArchived> taskbacklogarchivedinfo = null;
            TaskBacklogArchived taskbacklogarchived = null;
            TaskBacklogArchivedticketTasks taskbacklogarchivedtickettasks = null;

            try
            {
                taskbacklogarchivedinformation = new TaskBacklogArchivedInformation();
                taskbacklogarchivedinfo = new List<TaskBacklogArchived>();

                taskbacklogarchived = new TaskBacklogArchived();
                taskbacklogarchived.ticketId = "1";
                taskbacklogarchived.ticketNumber = "#0111";
                taskbacklogarchived.ticketTitle = "Invoice";
                taskbacklogarchived.ticketDescription = "Approve Invoice";
                taskbacklogarchived.ticketCreatedBy = "Donald Trump";
                taskbacklogarchived.ticketAssignedTo = "Anglena";
                taskbacklogarchived.ticketCreatedDate = "01/01/2021";
                taskbacklogarchived.ticketDueDate = "12/01/2021";
                taskbacklogarchived.isTicketPinned = true;
                taskbacklogarchived.messageCount = 5;
                taskbacklogarchived.ticketStatus = "Open";
                taskbacklogarchived.profileImage = "particJohnson.png";

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "1";
                taskbacklogarchivedtickettasks.taskNumber = "Task#1123";
                taskbacklogarchivedtickettasks.ticketId = "1";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Johnson";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "micheleLlamas.png";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 1";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Patric Johnson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "particJohnson.png";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";

                taskbacklogarchived.ticketTasks = new List<TaskBacklogArchivedticketTasks>();
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "2";
                taskbacklogarchivedtickettasks.taskNumber = "Task#2444";
                taskbacklogarchivedtickettasks.ticketId = "1";
                taskbacklogarchivedtickettasks.taskCreatedBy = "kristinGarrett";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "kristinGarrett.png";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 2";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "angieGabel.png";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 2";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedinfo.Add(taskbacklogarchived);


                taskbacklogarchived = new TaskBacklogArchived();
                taskbacklogarchived.ticketId = "2";
                taskbacklogarchived.ticketNumber = "#0666";
                taskbacklogarchived.ticketTitle = "Approve Funding Statement";
                taskbacklogarchived.ticketDescription = "Description";
                taskbacklogarchived.ticketCreatedBy = "Patrica John";
                taskbacklogarchived.ticketAssignedTo = "Anglena Matinson";
                taskbacklogarchived.ticketCreatedDate = "01/15/2021";
                taskbacklogarchived.ticketDueDate = "12/01/2021";
                taskbacklogarchived.isTicketPinned = true;
                taskbacklogarchived.messageCount = 5;
                taskbacklogarchived.ticketStatus = "Open";
                taskbacklogarchived.profileImage = "particJohnson.png";

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "1";
                taskbacklogarchivedtickettasks.taskNumber = "Task#1";
                taskbacklogarchivedtickettasks.ticketId = "2";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 3";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "kristinGarrett.png";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 3";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "micheleLlamas.png";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 3";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";

                taskbacklogarchived.ticketTasks = new List<TaskBacklogArchivedticketTasks>();
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "2";
                taskbacklogarchivedtickettasks.taskNumber = "Task#2";
                taskbacklogarchivedtickettasks.ticketId = "2";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 4";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "kristinGarrett.png";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 4";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "angieGabel.png";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 4";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedinfo.Add(taskbacklogarchived);


                taskbacklogarchived = new TaskBacklogArchived();
                taskbacklogarchived.ticketId = "3";
                taskbacklogarchived.ticketNumber = "#0442";
                taskbacklogarchived.ticketTitle = "Extend Due Date Request";
                taskbacklogarchived.ticketDescription = "Extend Due Date Request";
                taskbacklogarchived.ticketCreatedBy = "Sarah Smith";
                taskbacklogarchived.ticketAssignedTo = "Anglena Matinson";
                taskbacklogarchived.ticketCreatedDate = "01/01/2021";
                taskbacklogarchived.ticketDueDate = "12/1/2021";
                taskbacklogarchived.isTicketPinned = true;
                taskbacklogarchived.messageCount = 5;
                taskbacklogarchived.ticketStatus = "Open";
                taskbacklogarchived.profileImage = "saraSmith.png";

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "1";
                taskbacklogarchivedtickettasks.taskNumber = "Task#1";
                taskbacklogarchivedtickettasks.ticketId = "4";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 3";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "saraSmith.png";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 3";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "kristinGarrett.png";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 5";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";

                taskbacklogarchived.ticketTasks = new List<TaskBacklogArchivedticketTasks>();
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "2";
                taskbacklogarchivedtickettasks.taskNumber = "Task#2";
                taskbacklogarchivedtickettasks.ticketId = "4";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 4";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "kristinGarrett.png";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 4";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "saraSmith.png";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 6";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedinfo.Add(taskbacklogarchived);


                taskbacklogarchived = new TaskBacklogArchived();
                taskbacklogarchived.ticketId = "4";
                taskbacklogarchived.ticketNumber = "#0999";
                taskbacklogarchived.ticketTitle = "Approve Overage Charge";
                taskbacklogarchived.ticketDescription = "Approve Overage Charge";
                taskbacklogarchived.ticketCreatedBy = "Kristin Garrett";
                taskbacklogarchived.ticketAssignedTo = "Anglena Matinson";
                taskbacklogarchived.ticketCreatedDate = "01/01/2021";
                taskbacklogarchived.ticketDueDate = "01/015/2021";
                taskbacklogarchived.isTicketPinned = true;
                taskbacklogarchived.messageCount = 5;
                taskbacklogarchived.ticketStatus = "Open";
                taskbacklogarchived.profileImage = "kristinGarrett.png";

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "1";
                taskbacklogarchivedtickettasks.taskNumber = "Task#1";
                taskbacklogarchivedtickettasks.ticketId = "1";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 3";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 3";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 7";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";

                taskbacklogarchived.ticketTasks = new List<TaskBacklogArchivedticketTasks>();
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "2";
                taskbacklogarchivedtickettasks.taskNumber = "Task#2";
                taskbacklogarchivedtickettasks.ticketId = "1";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 4";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 4";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 8";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedinfo.Add(taskbacklogarchived);


                taskbacklogarchived = new TaskBacklogArchived();
                taskbacklogarchived.ticketId = "5";
                taskbacklogarchived.ticketNumber = "#0666";
                taskbacklogarchived.ticketTitle = "Approve Contract";
                taskbacklogarchived.ticketDescription = "Approve Contract";
                taskbacklogarchived.ticketCreatedBy = "Elizabeth Sakla";
                taskbacklogarchived.ticketAssignedTo = "Anglena Matinson";
                taskbacklogarchived.ticketCreatedDate = "01/01/2021";
                taskbacklogarchived.ticketDueDate = "01/14/2021";
                taskbacklogarchived.isTicketPinned = true;
                taskbacklogarchived.messageCount = 5;
                taskbacklogarchived.ticketStatus = "Open";
                taskbacklogarchived.profileImage = "elizabethSakala.png";

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "1";
                taskbacklogarchivedtickettasks.taskNumber = "Task#1";
                taskbacklogarchivedtickettasks.ticketId = "5";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 3";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 3";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 9";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";

                taskbacklogarchived.ticketTasks = new List<TaskBacklogArchivedticketTasks>();
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "2";
                taskbacklogarchivedtickettasks.taskNumber = "Task#2";
                taskbacklogarchivedtickettasks.ticketId = "5";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 4";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 4";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 10";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedinfo.Add(taskbacklogarchived);


                taskbacklogarchived = new TaskBacklogArchived();
                taskbacklogarchived.ticketId = "6";
                taskbacklogarchived.ticketNumber = "#0777";
                taskbacklogarchived.ticketTitle = "Monthly Review Meting";
                taskbacklogarchived.ticketDescription = "Monthly Review Meting";
                taskbacklogarchived.ticketCreatedBy = "Angie Gabel";
                taskbacklogarchived.ticketAssignedTo = "Anglena Matinson";
                taskbacklogarchived.ticketCreatedDate = "01/01/2021";
                taskbacklogarchived.ticketDueDate = "4/1/2021";
                taskbacklogarchived.isTicketPinned = true;
                taskbacklogarchived.messageCount = 5;
                taskbacklogarchived.ticketStatus = "Closed";
                taskbacklogarchived.profileImage = "angieGabel.png";

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "1";
                taskbacklogarchivedtickettasks.taskNumber = "Task#1";
                taskbacklogarchivedtickettasks.ticketId = "6";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 3";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 3";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 11";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";

                taskbacklogarchived.ticketTasks = new List<TaskBacklogArchivedticketTasks>();
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "2";
                taskbacklogarchivedtickettasks.taskNumber = "Task#2";
                taskbacklogarchivedtickettasks.ticketId = "6";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 4";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 4";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 12";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedinfo.Add(taskbacklogarchived);


                taskbacklogarchived = new TaskBacklogArchived();
                taskbacklogarchived.ticketId = "7";
                taskbacklogarchived.ticketNumber = "#0888";
                taskbacklogarchived.ticketTitle = "Approve Invoice";
                taskbacklogarchived.ticketDescription = "Approve Invoice";
                taskbacklogarchived.ticketCreatedBy = "Michele Llamas";
                taskbacklogarchived.ticketAssignedTo = "Anglena Matinson";
                taskbacklogarchived.ticketCreatedDate = "5/1/2021";
                taskbacklogarchived.ticketDueDate = "4/1/2021";
                taskbacklogarchived.isTicketPinned = true;
                taskbacklogarchived.messageCount = 5;
                taskbacklogarchived.ticketStatus = "Closed";
                taskbacklogarchived.profileImage = "micheleLlamas.png";

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "1";
                taskbacklogarchivedtickettasks.taskNumber = "Task#1";
                taskbacklogarchivedtickettasks.ticketId = "7";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 3";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 3";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 13";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";

                taskbacklogarchived.ticketTasks = new List<TaskBacklogArchivedticketTasks>();
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "2";
                taskbacklogarchivedtickettasks.taskNumber = "Task#2";
                taskbacklogarchivedtickettasks.ticketId = "7";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 4";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 4";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 14";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedinfo.Add(taskbacklogarchived);


                taskbacklogarchived = new TaskBacklogArchived();
                taskbacklogarchived.ticketId = "8";
                taskbacklogarchived.ticketNumber = "#01212";
                taskbacklogarchived.ticketTitle = "Approve Overage Charge";
                taskbacklogarchived.ticketDescription = "Approve Overage Charge";
                taskbacklogarchived.ticketCreatedBy = "Patrica John";
                taskbacklogarchived.ticketAssignedTo = "Anglena Matinson";
                taskbacklogarchived.ticketCreatedDate = "5/1/2021";
                taskbacklogarchived.ticketDueDate = "6/1/2021";
                taskbacklogarchived.isTicketPinned = true;
                taskbacklogarchived.messageCount = 5;
                taskbacklogarchived.ticketStatus = "Closed";
                taskbacklogarchived.profileImage = "angieGabel.png";

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "1";
                taskbacklogarchivedtickettasks.taskNumber = "Task#1";
                taskbacklogarchivedtickettasks.ticketId = "8";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 3";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 3";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 15";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";

                taskbacklogarchived.ticketTasks = new List<TaskBacklogArchivedticketTasks>();
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "2";
                taskbacklogarchivedtickettasks.taskNumber = "Task#2";
                taskbacklogarchivedtickettasks.ticketId = "8";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 4";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 4";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 16";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedinfo.Add(taskbacklogarchived);


                taskbacklogarchived = new TaskBacklogArchived();
                taskbacklogarchived.ticketId = "9";
                taskbacklogarchived.ticketNumber = "#0386";
                taskbacklogarchived.ticketTitle = "Approve Overage Charge";
                taskbacklogarchived.ticketDescription = "Approve Overage Charge";
                taskbacklogarchived.ticketCreatedBy = "Elizabeth Sakla";
                taskbacklogarchived.ticketAssignedTo = "Anglena Matinsognhln";
                taskbacklogarchived.ticketCreatedDate = "5/1/2021";
                taskbacklogarchived.ticketDueDate = "6/1/2021";
                taskbacklogarchived.isTicketPinned = true;
                taskbacklogarchived.messageCount = 5;
                taskbacklogarchived.ticketStatus = "Closed";
                taskbacklogarchived.profileImage = "angieGabel.png";

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "1";
                taskbacklogarchivedtickettasks.taskNumber = "Task#1";
                taskbacklogarchivedtickettasks.ticketId = "9";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 3";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 3";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 17";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";

                taskbacklogarchived.ticketTasks = new List<TaskBacklogArchivedticketTasks>();
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "2";
                taskbacklogarchivedtickettasks.taskNumber = "Task#2";
                taskbacklogarchivedtickettasks.ticketId = "9";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 4";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 4";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 18";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedinfo.Add(taskbacklogarchived);


                taskbacklogarchivedinformation.data = new List<TaskBacklogArchived>();
                taskbacklogarchivedinformation.data = taskbacklogarchivedinfo;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return taskbacklogarchivedinformation;

        }

        public TaskBacklogArchivedInformation GetTasksArchived(string userId, string userName)
        {

            TaskBacklogArchivedInformation taskbacklogarchivedinformation = null;
            List<TaskBacklogArchived> taskbacklogarchivedinfo = null;
            TaskBacklogArchived taskbacklogarchived = null;
            TaskBacklogArchivedticketTasks taskbacklogarchivedtickettasks = null;

            try
            {
                taskbacklogarchivedinformation = new TaskBacklogArchivedInformation();
                taskbacklogarchivedinfo = new List<TaskBacklogArchived>();

                taskbacklogarchived = new TaskBacklogArchived();
                taskbacklogarchived.ticketId = "1";
                taskbacklogarchived.ticketNumber = "#0111";
                taskbacklogarchived.ticketTitle = "Invoice";
                taskbacklogarchived.ticketDescription = "Approve Invoice";
                taskbacklogarchived.ticketCreatedBy = "Donald Trump";
                taskbacklogarchived.ticketAssignedTo = "Anglena";
                taskbacklogarchived.ticketCreatedDate = "01/01/2021";
                taskbacklogarchived.ticketDueDate = "12/01/2021";
                taskbacklogarchived.isTicketPinned = true;
                taskbacklogarchived.messageCount = 5;
                taskbacklogarchived.ticketStatus = "Open";
                taskbacklogarchived.profileImage = "particJohnson.png";

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "1";
                taskbacklogarchivedtickettasks.taskNumber = "Task#1123";
                taskbacklogarchivedtickettasks.ticketId = "1";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Johnson";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "micheleLlamas.png";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 1";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Patric Johnson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "particJohnson.png";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";

                taskbacklogarchived.ticketTasks = new List<TaskBacklogArchivedticketTasks>();
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "2";
                taskbacklogarchivedtickettasks.taskNumber = "Task#2444";
                taskbacklogarchivedtickettasks.ticketId = "1";
                taskbacklogarchivedtickettasks.taskCreatedBy = "kristinGarrett";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "kristinGarrett.png";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 2";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "angieGabel.png";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 2";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedinfo.Add(taskbacklogarchived);


                taskbacklogarchived = new TaskBacklogArchived();
                taskbacklogarchived.ticketId = "2";
                taskbacklogarchived.ticketNumber = "#0666";
                taskbacklogarchived.ticketTitle = "Approve Funding Statement";
                taskbacklogarchived.ticketDescription = "Description";
                taskbacklogarchived.ticketCreatedBy = "Patrica John";
                taskbacklogarchived.ticketAssignedTo = "Anglena Matinson";
                taskbacklogarchived.ticketCreatedDate = "01/15/2021";
                taskbacklogarchived.ticketDueDate = "12/01/2021";
                taskbacklogarchived.isTicketPinned = true;
                taskbacklogarchived.messageCount = 5;
                taskbacklogarchived.ticketStatus = "Open";
                taskbacklogarchived.profileImage = "particJohnson.png";

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "1";
                taskbacklogarchivedtickettasks.taskNumber = "Task#1";
                taskbacklogarchivedtickettasks.ticketId = "2";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 3";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "kristinGarrett.png";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 3";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "micheleLlamas.png";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 3";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";

                taskbacklogarchived.ticketTasks = new List<TaskBacklogArchivedticketTasks>();
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "2";
                taskbacklogarchivedtickettasks.taskNumber = "Task#2";
                taskbacklogarchivedtickettasks.ticketId = "2";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 4";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "kristinGarrett.png";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 4";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "angieGabel.png";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 4";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedinfo.Add(taskbacklogarchived);


                taskbacklogarchived = new TaskBacklogArchived();
                taskbacklogarchived.ticketId = "3";
                taskbacklogarchived.ticketNumber = "#0442";
                taskbacklogarchived.ticketTitle = "Extend Due Date Request";
                taskbacklogarchived.ticketDescription = "Extend Due Date Request";
                taskbacklogarchived.ticketCreatedBy = "Sarah Smith";
                taskbacklogarchived.ticketAssignedTo = "Anglena Matinson";
                taskbacklogarchived.ticketCreatedDate = "01/01/2021";
                taskbacklogarchived.ticketDueDate = "12/1/2021";
                taskbacklogarchived.isTicketPinned = true;
                taskbacklogarchived.messageCount = 5;
                taskbacklogarchived.ticketStatus = "Open";
                taskbacklogarchived.profileImage = "saraSmith.png";

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "1";
                taskbacklogarchivedtickettasks.taskNumber = "Task#1";
                taskbacklogarchivedtickettasks.ticketId = "4";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 3";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "saraSmith.png";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 3";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "kristinGarrett.png";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 5";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";

                taskbacklogarchived.ticketTasks = new List<TaskBacklogArchivedticketTasks>();
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "2";
                taskbacklogarchivedtickettasks.taskNumber = "Task#2";
                taskbacklogarchivedtickettasks.ticketId = "4";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 4";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "kristinGarrett.png";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 4";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "saraSmith.png";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 6";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedinfo.Add(taskbacklogarchived);


                taskbacklogarchived = new TaskBacklogArchived();
                taskbacklogarchived.ticketId = "4";
                taskbacklogarchived.ticketNumber = "#0999";
                taskbacklogarchived.ticketTitle = "Approve Overage Charge";
                taskbacklogarchived.ticketDescription = "Approve Overage Charge";
                taskbacklogarchived.ticketCreatedBy = "Kristin Garrett";
                taskbacklogarchived.ticketAssignedTo = "Anglena Matinson";
                taskbacklogarchived.ticketCreatedDate = "01/01/2021";
                taskbacklogarchived.ticketDueDate = "01/015/2021";
                taskbacklogarchived.isTicketPinned = true;
                taskbacklogarchived.messageCount = 5;
                taskbacklogarchived.ticketStatus = "Open";
                taskbacklogarchived.profileImage = "kristinGarrett.png";

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "1";
                taskbacklogarchivedtickettasks.taskNumber = "Task#1";
                taskbacklogarchivedtickettasks.ticketId = "1";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 3";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 3";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 7";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";

                taskbacklogarchived.ticketTasks = new List<TaskBacklogArchivedticketTasks>();
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "2";
                taskbacklogarchivedtickettasks.taskNumber = "Task#2";
                taskbacklogarchivedtickettasks.ticketId = "1";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 4";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 4";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 8";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedinfo.Add(taskbacklogarchived);


                taskbacklogarchived = new TaskBacklogArchived();
                taskbacklogarchived.ticketId = "5";
                taskbacklogarchived.ticketNumber = "#0666";
                taskbacklogarchived.ticketTitle = "Approve Contract";
                taskbacklogarchived.ticketDescription = "Approve Contract";
                taskbacklogarchived.ticketCreatedBy = "Elizabeth Sakla";
                taskbacklogarchived.ticketAssignedTo = "Anglena Matinson";
                taskbacklogarchived.ticketCreatedDate = "01/01/2021";
                taskbacklogarchived.ticketDueDate = "01/14/2021";
                taskbacklogarchived.isTicketPinned = true;
                taskbacklogarchived.messageCount = 5;
                taskbacklogarchived.ticketStatus = "Open";
                taskbacklogarchived.profileImage = "elizabethSakala.png";

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "1";
                taskbacklogarchivedtickettasks.taskNumber = "Task#1";
                taskbacklogarchivedtickettasks.ticketId = "5";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 3";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 3";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 9";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";

                taskbacklogarchived.ticketTasks = new List<TaskBacklogArchivedticketTasks>();
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "2";
                taskbacklogarchivedtickettasks.taskNumber = "Task#2";
                taskbacklogarchivedtickettasks.ticketId = "5";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 4";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 4";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 10";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedinfo.Add(taskbacklogarchived);


                taskbacklogarchived = new TaskBacklogArchived();
                taskbacklogarchived.ticketId = "6";
                taskbacklogarchived.ticketNumber = "#0777";
                taskbacklogarchived.ticketTitle = "Monthly Review Meting";
                taskbacklogarchived.ticketDescription = "Monthly Review Meting";
                taskbacklogarchived.ticketCreatedBy = "Angie Gabel";
                taskbacklogarchived.ticketAssignedTo = "Anglena Matinson";
                taskbacklogarchived.ticketCreatedDate = "01/01/2021";
                taskbacklogarchived.ticketDueDate = "4/1/2021";
                taskbacklogarchived.isTicketPinned = true;
                taskbacklogarchived.messageCount = 5;
                taskbacklogarchived.ticketStatus = "Closed";
                taskbacklogarchived.profileImage = "angieGabel.png";

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "1";
                taskbacklogarchivedtickettasks.taskNumber = "Task#1";
                taskbacklogarchivedtickettasks.ticketId = "6";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 3";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 3";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 11";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";

                taskbacklogarchived.ticketTasks = new List<TaskBacklogArchivedticketTasks>();
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "2";
                taskbacklogarchivedtickettasks.taskNumber = "Task#2";
                taskbacklogarchivedtickettasks.ticketId = "6";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 4";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 4";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 12";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedinfo.Add(taskbacklogarchived);


                taskbacklogarchived = new TaskBacklogArchived();
                taskbacklogarchived.ticketId = "7";
                taskbacklogarchived.ticketNumber = "#0888";
                taskbacklogarchived.ticketTitle = "Approve Invoice";
                taskbacklogarchived.ticketDescription = "Approve Invoice";
                taskbacklogarchived.ticketCreatedBy = "Michele Llamas";
                taskbacklogarchived.ticketAssignedTo = "Anglena Matinson";
                taskbacklogarchived.ticketCreatedDate = "5/1/2021";
                taskbacklogarchived.ticketDueDate = "4/1/2021";
                taskbacklogarchived.isTicketPinned = true;
                taskbacklogarchived.messageCount = 5;
                taskbacklogarchived.ticketStatus = "Closed";
                taskbacklogarchived.profileImage = "micheleLlamas.png";

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "1";
                taskbacklogarchivedtickettasks.taskNumber = "Task#1";
                taskbacklogarchivedtickettasks.ticketId = "7";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 3";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 3";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 13";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";

                taskbacklogarchived.ticketTasks = new List<TaskBacklogArchivedticketTasks>();
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "2";
                taskbacklogarchivedtickettasks.taskNumber = "Task#2";
                taskbacklogarchivedtickettasks.ticketId = "7";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 4";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 4";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 14";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedinfo.Add(taskbacklogarchived);


                taskbacklogarchived = new TaskBacklogArchived();
                taskbacklogarchived.ticketId = "8";
                taskbacklogarchived.ticketNumber = "#01212";
                taskbacklogarchived.ticketTitle = "Approve Overage Charge";
                taskbacklogarchived.ticketDescription = "Approve Overage Charge";
                taskbacklogarchived.ticketCreatedBy = "Patrica John";
                taskbacklogarchived.ticketAssignedTo = "Anglena Matinson";
                taskbacklogarchived.ticketCreatedDate = "5/1/2021";
                taskbacklogarchived.ticketDueDate = "6/1/2021";
                taskbacklogarchived.isTicketPinned = true;
                taskbacklogarchived.messageCount = 5;
                taskbacklogarchived.ticketStatus = "Closed";
                taskbacklogarchived.profileImage = "angieGabel.png";

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "1";
                taskbacklogarchivedtickettasks.taskNumber = "Task#1";
                taskbacklogarchivedtickettasks.ticketId = "8";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 3";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 3";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 15";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";

                taskbacklogarchived.ticketTasks = new List<TaskBacklogArchivedticketTasks>();
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "2";
                taskbacklogarchivedtickettasks.taskNumber = "Task#2";
                taskbacklogarchivedtickettasks.ticketId = "8";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 4";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 4";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 16";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedinfo.Add(taskbacklogarchived);


                taskbacklogarchived = new TaskBacklogArchived();
                taskbacklogarchived.ticketId = "9";
                taskbacklogarchived.ticketNumber = "#0386";
                taskbacklogarchived.ticketTitle = "Approve Overage Charge";
                taskbacklogarchived.ticketDescription = "Approve Overage Charge";
                taskbacklogarchived.ticketCreatedBy = "Elizabeth Sakla";
                taskbacklogarchived.ticketAssignedTo = "Anglena Matinsognhln";
                taskbacklogarchived.ticketCreatedDate = "5/1/2021";
                taskbacklogarchived.ticketDueDate = "6/1/2021";
                taskbacklogarchived.isTicketPinned = true;
                taskbacklogarchived.messageCount = 5;
                taskbacklogarchived.ticketStatus = "Closed";
                taskbacklogarchived.profileImage = "angieGabel.png";

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "1";
                taskbacklogarchivedtickettasks.taskNumber = "Task#1";
                taskbacklogarchivedtickettasks.ticketId = "9";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 3";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 3";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 17";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";

                taskbacklogarchived.ticketTasks = new List<TaskBacklogArchivedticketTasks>();
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedtickettasks = new TaskBacklogArchivedticketTasks();
                taskbacklogarchivedtickettasks.taskId = "2";
                taskbacklogarchivedtickettasks.taskNumber = "Task#2";
                taskbacklogarchivedtickettasks.ticketId = "9";
                taskbacklogarchivedtickettasks.taskCreatedBy = "Lorean 4";
                taskbacklogarchivedtickettasks.taskCreatedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskAssignedTo = "Jakson 4";
                taskbacklogarchivedtickettasks.taskCreatedDate = "01/02/2021";
                taskbacklogarchivedtickettasks.isTicketPinned = false;
                taskbacklogarchivedtickettasks.taskStatus = "Open";
                taskbacklogarchivedtickettasks.taskResolvedBy = "Matinson";
                taskbacklogarchivedtickettasks.taskAssignedUserAvatar = "";
                taskbacklogarchivedtickettasks.taskTitle = "Task Title 18";
                taskbacklogarchivedtickettasks.taskDescription = "Task Title 1";
                taskbacklogarchivedtickettasks.taskDueDate = "01/22/2021";
                taskbacklogarchived.ticketTasks.Add(taskbacklogarchivedtickettasks);

                taskbacklogarchivedinfo.Add(taskbacklogarchived);


                taskbacklogarchivedinformation.data = new List<TaskBacklogArchived>();
                taskbacklogarchivedinformation.data = taskbacklogarchivedinfo;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return taskbacklogarchivedinformation;

        }

        public TaskbacklogSchedulerInfo GetTaskbacklogInfotoShowInScheduler(string userId, string userName)
        {

            TaskbacklogSchedulerInfo taskbacklogschedulerinfo = null;
            TaskbacklogScheduler taskbacklogscheduler = null;
            List<TaskbacklogScheduler> TaskbacklogSchedulerlist = null;

            try
            {
                taskbacklogschedulerinfo = new TaskbacklogSchedulerInfo();
                TaskbacklogSchedulerlist = new List<TaskbacklogScheduler>();

                taskbacklogscheduler = new TaskbacklogScheduler();
                taskbacklogscheduler.Status = "Open";
                taskbacklogscheduler.createdUser = "Mark Savas";
                taskbacklogscheduler.createdUserAvatar = "markSavas.png";
                taskbacklogscheduler.id = 1;
                taskbacklogscheduler.start = "2021-01-09";
                taskbacklogscheduler.taskDueDate = "2021-01-09";
                taskbacklogscheduler.taskId = 1;
                taskbacklogscheduler.taskNumber = "#0421";
                taskbacklogscheduler.taskTitle = "Approve Invoice";
                taskbacklogscheduler.title = "Approve Invoice";
                TaskbacklogSchedulerlist.Add(taskbacklogscheduler);

                taskbacklogscheduler = new TaskbacklogScheduler();
                taskbacklogscheduler.Status = "Open";
                taskbacklogscheduler.createdUser = "Patrica John";
                taskbacklogscheduler.createdUserAvatar = "patricaJohn.png";
                taskbacklogscheduler.id = 2;
                taskbacklogscheduler.start = "2021-01-15";
                taskbacklogscheduler.taskDueDate = "2021-01-15";
                taskbacklogscheduler.taskId = 2;
                taskbacklogscheduler.taskNumber = "#0421";
                taskbacklogscheduler.taskTitle = "Approve Funding Statement";
                taskbacklogscheduler.title = "Approve Funding Statement";
                TaskbacklogSchedulerlist.Add(taskbacklogscheduler);

                taskbacklogscheduler = new TaskbacklogScheduler();
                taskbacklogscheduler.Status = "Open";
                taskbacklogscheduler.createdUser = "Sarah Smith";
                taskbacklogscheduler.createdUserAvatar = "saraSmith.png";
                taskbacklogscheduler.id = 1;
                taskbacklogscheduler.start = "2021-12-01";
                taskbacklogscheduler.taskDueDate = "2021-12-01";
                taskbacklogscheduler.taskId = 3;
                taskbacklogscheduler.taskNumber = "#0426";
                taskbacklogscheduler.taskTitle = "Extend Due Date Request";
                taskbacklogscheduler.title = "Extend Due Date Request";
                TaskbacklogSchedulerlist.Add(taskbacklogscheduler);

                taskbacklogscheduler = new TaskbacklogScheduler();
                taskbacklogscheduler.Status = "Open";
                taskbacklogscheduler.createdUser = "Kristin Garrett";
                taskbacklogscheduler.createdUserAvatar = "kristinGarrett.png";
                taskbacklogscheduler.id = 1;
                taskbacklogscheduler.start = "2021-01-15";
                taskbacklogscheduler.taskDueDate = "2021-01-15";
                taskbacklogscheduler.taskId = 4;
                taskbacklogscheduler.taskNumber = "#0526";
                taskbacklogscheduler.taskTitle = "Approve Overage Charge";
                taskbacklogscheduler.title = "Approve Overage Charge";
                TaskbacklogSchedulerlist.Add(taskbacklogscheduler);

                taskbacklogscheduler = new TaskbacklogScheduler();
                taskbacklogscheduler.Status = "Open";
                taskbacklogscheduler.createdUser = "Elizabeth Sakla";
                taskbacklogscheduler.createdUserAvatar = "elizabethSakla.png";
                taskbacklogscheduler.id = 1;
                taskbacklogscheduler.start = "2021-01-14";
                taskbacklogscheduler.taskDueDate = "2021-01-14";
                taskbacklogscheduler.taskId = 5;
                taskbacklogscheduler.taskNumber = "#0542";
                taskbacklogscheduler.taskTitle = "Approve Contract";
                taskbacklogscheduler.title = "Approve Contract";
                TaskbacklogSchedulerlist.Add(taskbacklogscheduler);

                taskbacklogscheduler = new TaskbacklogScheduler();
                taskbacklogscheduler.Status = "Closed";
                taskbacklogscheduler.createdUser = "Angie Gabel";
                taskbacklogscheduler.createdUserAvatar = "angieGabel.png";
                taskbacklogscheduler.id = 1;
                taskbacklogscheduler.start = "2021-04-01";
                taskbacklogscheduler.taskDueDate = "2021-04-01";
                taskbacklogscheduler.taskId = 6;
                taskbacklogscheduler.taskNumber = "#0195";
                taskbacklogscheduler.taskTitle = "Monthly Review Meting";
                taskbacklogscheduler.title = "Monthly Review Meting";
                TaskbacklogSchedulerlist.Add(taskbacklogscheduler);

                taskbacklogscheduler = new TaskbacklogScheduler();
                taskbacklogscheduler.Status = "Closed";
                taskbacklogscheduler.createdUser = "Michele Llamas";
                taskbacklogscheduler.createdUserAvatar = "markSavas.png";
                taskbacklogscheduler.id = 1;
                taskbacklogscheduler.start = "2021-05-01";
                taskbacklogscheduler.taskDueDate = "2021-05-01";
                taskbacklogscheduler.taskId = 7;
                taskbacklogscheduler.taskNumber = "#0321";
                taskbacklogscheduler.taskTitle = "Approve Invoice";
                taskbacklogscheduler.title = "Approve Invoice";
                TaskbacklogSchedulerlist.Add(taskbacklogscheduler);

                taskbacklogscheduler = new TaskbacklogScheduler();
                taskbacklogscheduler.Status = "Closed";
                taskbacklogscheduler.createdUser = "Patrica John";
                taskbacklogscheduler.createdUserAvatar = "patricaJohn.png";
                taskbacklogscheduler.id = 1;
                taskbacklogscheduler.start = "2021-06-01";
                taskbacklogscheduler.taskDueDate = "2021-06-01";
                taskbacklogscheduler.taskId = 8;
                taskbacklogscheduler.taskNumber = "#0265";
                taskbacklogscheduler.taskTitle = "Approve Invoice";
                taskbacklogscheduler.title = "Approve Invoice";
                TaskbacklogSchedulerlist.Add(taskbacklogscheduler);

                taskbacklogscheduler = new TaskbacklogScheduler();
                taskbacklogscheduler.Status = "";
                taskbacklogscheduler.createdUser = "Elizabeth Sakla";
                taskbacklogscheduler.createdUserAvatar = "markSavas.png";
                taskbacklogscheduler.id = 1;
                taskbacklogscheduler.start = "2020-0121-30";
                taskbacklogscheduler.taskDueDate = "2020-12-30";
                taskbacklogscheduler.taskId = 1;
                taskbacklogscheduler.taskNumber = "#0386";
                taskbacklogscheduler.taskTitle = "Approve Contract";
                taskbacklogscheduler.title = "Approve Contract";
                TaskbacklogSchedulerlist.Add(taskbacklogscheduler);

                taskbacklogschedulerinfo.data = TaskbacklogSchedulerlist;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return taskbacklogschedulerinfo;
        }

        public ResultMessage AddUpdateTask(TaskInfo taskinfo)
        {
            ResultMessage objResult = new ResultMessage();

            try
            {

                //calling the db save or update method, if Save or update Task then return true other wise return false
                //related to the Reschedule Task from Scheduler functioning
                //return true;
                objResult.Message = "Saved / Updated Task Successfully";
                objResult.Result = true;

            }
            catch (Exception ex)
            {
                objResult.Message = "Unable to Save / Update Task, Please try again.";
                objResult.Result = false;
            }

            return objResult;

        }


        public ResultMessage RescheduleTask(RescheduleTask rescheduletask)
        {
            ResultMessage objResult = new ResultMessage();

            try
            {

                //calling the db save or update method, if successfully updated then return true other wise return false
                //related to the Reschedule Task from Scheduler functioning
                //return true;
                objResult.Message = "Rescheduled Task Successfully";
                objResult.Result = true;

            }
            catch (Exception ex)
            {
                objResult.Message = "Unable to Reschedule Task, Please try again.";
                objResult.Result = false;
            }

            return objResult;

        }

        public ResultMessage AddActivityLogDetails(ActivityLogDetails activitylogdetails)
        {
            ResultMessage objResult = new ResultMessage();

            try
            {

                //calling the db save method, if Activity Log Details Saved then return true other wise return false
                //return true;
                objResult.Message = "Activity Log Details Saved Successfully";
                objResult.Result = true;

            }
            catch (Exception ex)
            {
                objResult.Message = "Unable to Save Activity Log Details, Please try again.";
                objResult.Result = false;
            }

            return objResult;

        }

        public TaskBacklogArchivedInformation GetTicketDetails(string userId, string userName, string ticketId)
        {
            TaskBacklogArchivedInformation taskbacklogarchivedinformation = null;
            TaskBacklogArchived taskbacklogarchived = null;
            try
            {
                taskbacklogarchivedinformation = new TaskBacklogArchivedInformation();
                taskbacklogarchivedinformation.data = new List<TaskBacklogArchived>();
                taskbacklogarchived = new TaskBacklogArchived();
                taskbacklogarchived.ticketId = "1";
                taskbacklogarchived.ticketNumber = "1058";
                taskbacklogarchived.ticketDueDate = "30/08/2019";
                taskbacklogarchivedinformation.data.Add(taskbacklogarchived);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return taskbacklogarchivedinformation;

        }

        public ActivityLogDetailsInformation GetTicketActivityLogDetails(string userId, string userName, string ticketId)
        {
            ActivityLogDetailsInformation activitylogdetailsinformation = null;
            ActivityLogDetailsInformationList activitylogdetailsinformationlist = null;

            try
            {

                activitylogdetailsinformation = new ActivityLogDetailsInformation();
                activitylogdetailsinformation.ActivityLogDetailsInfo = new List<ActivityLogDetailsInformationList>();

                activitylogdetailsinformationlist = new ActivityLogDetailsInformationList();
                activitylogdetailsinformationlist.AssignedTo = "Madison Hughes";
                activitylogdetailsinformationlist.EmailID = "jane@pn-manhattan.com";
                activitylogdetailsinformationlist.createdUserAvatar = "";
                activitylogdetailsinformationlist.Tag = "";
                activitylogdetailsinformation.ActivityLogDetailsInfo.Add(activitylogdetailsinformationlist);

                activitylogdetailsinformationlist = new ActivityLogDetailsInformationList();
                activitylogdetailsinformationlist.AssignedTo = "Joshua Williams";
                activitylogdetailsinformationlist.EmailID = "emailAddress";
                activitylogdetailsinformationlist.createdUserAvatar = "";
                activitylogdetailsinformationlist.Tag = "contact Trump in WM contract";
                activitylogdetailsinformation.ActivityLogDetailsInfo.Add(activitylogdetailsinformationlist);

                activitylogdetailsinformationlist = new ActivityLogDetailsInformationList();
                activitylogdetailsinformationlist.AssignedTo = "elizabeth Sakala k";
                activitylogdetailsinformationlist.EmailID = "elizabethSakala@wm.com";
                activitylogdetailsinformationlist.createdUserAvatar = "";
                activitylogdetailsinformationlist.Tag = "Vendor Contract Approval";
                activitylogdetailsinformation.ActivityLogDetailsInfo.Add(activitylogdetailsinformationlist);

                activitylogdetailsinformationlist = new ActivityLogDetailsInformationList();
                activitylogdetailsinformationlist.AssignedTo = "Tim Johnson";
                activitylogdetailsinformationlist.EmailID = "tim@pn-manhattan.com";
                activitylogdetailsinformationlist.createdUserAvatar = "";
                activitylogdetailsinformationlist.Tag = "";
                activitylogdetailsinformation.ActivityLogDetailsInfo.Add(activitylogdetailsinformationlist);

                activitylogdetailsinformationlist = new ActivityLogDetailsInformationList();
                activitylogdetailsinformationlist.AssignedTo = "David Stark";
                activitylogdetailsinformationlist.EmailID = "kelly@pn-manhattan.com";
                activitylogdetailsinformationlist.createdUserAvatar = "";
                activitylogdetailsinformationlist.Tag = "";
                activitylogdetailsinformation.ActivityLogDetailsInfo.Add(activitylogdetailsinformationlist);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return activitylogdetailsinformation;

        }


    }
}
