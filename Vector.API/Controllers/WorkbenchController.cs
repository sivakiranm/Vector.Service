using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http; 
using System.Web.Http;
using System.Web.Http.Cors;
using Vector.API.Handlers;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Garage.BusinessLayer;
using Vector.Garage.Entities;
using Vector.UserManagement.ClientInfo;
using Vector.Workbench.BusinessLayer;
using Vector.Workbench.Entities;

namespace Vector.API.Controllers
{
    [RoutePrefix("api/workbench")]
    [VectorActionAutorizationFilter]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WorkbenchController : ApiController
    {


        #region"                METHOD TO RETURN THE TASKS BACKLOG DATA IN THE FORM OF JSON OBJECT                "

        [HttpGet]
        [Route("GetTasksBacklog")]

        public IHttpActionResult GetTasksBacklog(string userId, string userName)
        {

            TaskBacklogArchivedInformation taskbacklogarchivedinformation = null;
            TaskbacklogArchivedBL taskbacklogbl = new TaskbacklogArchivedBL();
            taskbacklogarchivedinformation = taskbacklogbl.GetTasksBacklog(userId, userName);

            var model = new
            {
                response = "Success",
                responseMessage = "Login Success, Kindly Secure Token",
                responseCode = "" + "200" + "",
                responseData = taskbacklogarchivedinformation,
            };
            return Ok(model);

        }

        #endregion

        #region"                METHOD TO RETURN THE TASKS ARCHIVED DATA IN THE FORM OF JSON OBJECT                "

        [HttpGet]
        [Route("GetTasksArchived")]

        public IHttpActionResult GetTasksArchived(string userId, string userName)
        {

            TaskBacklogArchivedInformation taskbacklogarchivedinformation = null;
            TaskbacklogArchivedBL taskbacklogbl = new TaskbacklogArchivedBL();
            taskbacklogarchivedinformation = taskbacklogbl.GetTasksArchived(userId, userName);

            var model = new
            {
                response = "Success",
                responseMessage = "Login Success, Kindly Secure Token",
                responseCode = "" + "200" + "",
                responseData = taskbacklogarchivedinformation,
            };
            return Ok(model);

        }

        #endregion


        #region"                METHOD TO RETURN THE INBOX  DATA IN THE FORM OF JSON OBJECT                "

        [HttpGet]
        [Route("GetInboxInfo")]

        public IHttpActionResult GetInboxInfo(Int64 userid)
        {
            using (var objTicketsBL = new TicketsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objTicketsBL.GetMessageBoxInfo("Inbox", VectorAPIContext.Current.UserId));
            }

        }

        #endregion

        #region"                METHOD TO RETURN THE OUTBOX DATA IN THE FORM OF JSON OBJECT                "

        [HttpGet]
        [Route("GetOutboxInfo")]

        public IHttpActionResult GetOutboxInfo(Int64 userid)
        {

            using (var objTicketsBL = new TicketsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objTicketsBL.GetMessageBoxInfo("Outbox", userid));
            }

        }

        #endregion

        #region"                METHOD TO RETURN THE TASKS TO DATA TO SHOW IN SCHEDULER IN THE FORM OF JSON OBJECT                "

        [HttpGet]
        [Route("GetTaskbacklogInfotoShowInScheduler")]

        public IHttpActionResult GetTaskbacklogInfotoShowInScheduler(string userId, string userName)
        {

            TaskbacklogSchedulerInfo taskbacklogschedulerinfo = null;
            TaskbacklogArchivedBL taskbacklogbl = new TaskbacklogArchivedBL();
            taskbacklogschedulerinfo = taskbacklogbl.GetTaskbacklogInfotoShowInScheduler(userId, userName);

            var model = new
            {
                response = "Success",
                responseMessage = "Login Success, Kindly Secure Token",
                responseCode = "" + "200" + "",
                responseData = taskbacklogschedulerinfo,
            };
            return Ok(model);

        }

        #endregion

        #region"                METHOD TO ADD OR UPDATE TASK INFORMATION                "

        [HttpPost]
        [Route("AddUpdateTask")]

        public IHttpActionResult AddUpdateTask(TaskInfo taskinfo)
        {
            //TODO : Implement Using in all methods
            ResultMessage resultmessage = null;
            TaskbacklogArchivedBL taskbacklogbl = new TaskbacklogArchivedBL();
            resultmessage = taskbacklogbl.AddUpdateTask(taskinfo);

            if (resultmessage.Result)
            {
                var model = new
                {
                    response = "Success",
                    responseMessage = resultmessage.Message,
                    responseCode = "" + "200" + "",
                    responseData = "[]",
                };
                return Ok(model);
            }
            else
            {
                var model = new
                {
                    response = "Failure",
                    responseMessage = resultmessage.Message,
                    responseCode = "" + "404" + "",
                    responseData = "[]",
                };
                return Ok(model);
            }

        }

        #endregion

        #region"                METHOD TO RESCHEDULE TASK FROM SCHEDULER INFORMATION                "

        [HttpPost]
        [Route("RescheduleTask")]

        public IHttpActionResult RescheduleTask(RescheduleTask rescheduletask)
        {
            //TODO : Implement Using in all methods
            ResultMessage resultmessage = null;
            TaskbacklogArchivedBL taskbacklogbl = new TaskbacklogArchivedBL();
            resultmessage = taskbacklogbl.RescheduleTask(rescheduletask);

            if (resultmessage.Result)
            {
                var model = new
                {
                    response = "Success",
                    responseMessage = resultmessage.Message,
                    responseCode = "" + "200" + "",
                    responseData = "[]",
                };
                return Ok(model);
            }
            else
            {
                var model = new
                {
                    response = "Failure",
                    responseMessage = resultmessage.Message,
                    responseCode = "" + "404" + "",
                    responseData = "[]",
                };
                return Ok(model);
            }

        }

        #endregion

        #region"                METHOD TO ADD ACTIVITY LOG DETAILS INFORMATION                "

        [HttpPost]
        [Route("AddActivityLogDetails")]

        public IHttpActionResult AddActivityLogDetails(ActivityLogDetails activitylogdetails)
        {
            //TODO : Implement Using in all methods
            ResultMessage resultmessage = null;
            TaskbacklogArchivedBL taskbacklogbl = new TaskbacklogArchivedBL();
            resultmessage = taskbacklogbl.AddActivityLogDetails(activitylogdetails);

            if (resultmessage.Result)
            {
                var model = new
                {
                    response = "Success",
                    responseMessage = resultmessage.Message,
                    responseCode = "" + "200" + "",
                    responseData = "[]",
                };
                return Ok(model);
            }
            else
            {
                var model = new
                {
                    response = "Failure",
                    responseMessage = resultmessage.Message,
                    responseCode = "" + "404" + "",
                    responseData = "[]",
                };
                return Ok(model);
            }

        }

        #endregion



        #region"                METHOD TO RETURN THE TICKET ACTIVITY LOG DETAILS DATA IN THE FORM OF JSON OBJECT                "

        [HttpGet]
        [Route("GetTicketActivityLogDetails")]

        public IHttpActionResult GetTicketActivityLogDetails(string userId, string userName, string ticketId)
        {

            ActivityLogDetailsInformation activitylogdetailsinformation = null;
            TaskbacklogArchivedBL taskbacklogbl = new TaskbacklogArchivedBL();
            activitylogdetailsinformation = taskbacklogbl.GetTicketActivityLogDetails(userId, userName, ticketId);

            var model = new
            {
                response = "Success",
                responseMessage = "Login Success, Kindly Secure Token",
                responseCode = "" + "200" + "",
                responseData = activitylogdetailsinformation,
            };
            return Ok(model);

        }

        [HttpGet]
        [Route("GetBackLogTickets")]

        public IHttpActionResult GetBackLogTickets(Int64 userId)
        {

            using (var objThreeSixtyDegreesBL = new ThreeSixtyDegreesBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objThreeSixtyDegreesBL.GetBackLogTickets("TaskBackLog", 0, 0, null, "", VectorAPIContext.Current.UserId));
            }

        }

        [HttpGet]
        [Route("GetArchivedTickets")]

        public IHttpActionResult GetArchivedTickets(Int64 userId)
        {

            using (var objThreeSixtyDegreesBL = new ThreeSixtyDegreesBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objThreeSixtyDegreesBL.GetBackLogTickets("ArchivedTickets", 0, 0, null, "", VectorAPIContext.Current.UserId));
            }

        }

        [HttpGet]
        [Route("GetTickets")]

        public IHttpActionResult GetTickets(Int64 userId)
        {

            using (var objThreeSixtyDegreesBL = new ThreeSixtyDegreesBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objThreeSixtyDegreesBL.GetBackLogTickets("Tickets", 0, 0, null, "", VectorAPIContext.Current.UserId));
            }

        }

        [HttpGet]
        [Route("GetTicketsRelatedTasks")]

        public IHttpActionResult GetTicketsRelatedTasks(Int64 userId, Int64 ticketId)
        {

            using (var objThreeSixtyDegreesBL = new ThreeSixtyDegreesBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objThreeSixtyDegreesBL.GetBackLogTickets("TasksByTicketId", 0, ticketId, null, "", VectorAPIContext.Current.UserId));
            }

        }

        [HttpGet]
        [Route("GetAllTasks")]

        public IHttpActionResult GetAllTasks(Int64 userId, Int64 ticketId)
        {

            using (var objThreeSixtyDegreesBL = new ThreeSixtyDegreesBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objThreeSixtyDegreesBL.GetBackLogTickets("AllTasks", 0, ticketId, null, "", VectorAPIContext.Current.UserId));
            }

        }

        [HttpPost]
        [Route("GetTicketDetails")]
        public IHttpActionResult GetTicketAcitivityLog(TicketInfo objTicketInfo)
        {

            using (var objTicketBl = new TicketsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objTicketBl.GetTicketDetails(objTicketInfo, VectorAPIContext.Current.UserId));
            }

        }

        [HttpPost]
        [Route("ManageTicketDetails")]
        public IHttpActionResult ManageTicketDetails(TicketInfo objTicketInfo)
        {

            using (var objTicketBl = new TicketsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objTicketBl.ManageTicketDetails(objTicketInfo, VectorAPIContext.Current.UserId));
            }

        }


        [HttpGet]
        [Route("GetBackLogTreeTickets")]

        public IHttpActionResult GetBackLogTreeTickets(Int64 userId, string action, Int64? reportingUserId, string categoryType, string categoryId, string assignedUsers)
        {

            using (var objThreeSixtyDegreesBL = new ThreeSixtyDegreesBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objThreeSixtyDegreesBL.GetAdvancedBackLogTicketsSearch(action, 0, 0, null, "", VectorAPIContext.Current.UserId, reportingUserId, categoryType, categoryId, assignedUsers));
            }

        }

        //[HttpGet]
        //[Route("GetBacklogInfo")]
        //public IHttpActionResult GetBacklogInfo()
        //{

        //    using (var objThreeSixtyDegreesBL = new ThreeSixtyDegreesBL(new VectorDataConnection()))
        //    {
        //        return VectorResponseHandler.GetVectorResponse(objThreeSixtyDegreesBL.GetTicketAndTaskInfo("GetBacklogInfo", 0, 0, null, "", VectorAPIContext.Current.UserId));
        //    }

        //} 

        [HttpGet]
        [Route("GetArchivedTreeTickets")]

        public IHttpActionResult GetArchivedTreeTickets(Int64 userId, string action)
        {

            using (var objThreeSixtyDegreesBL = new ThreeSixtyDegreesBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objThreeSixtyDegreesBL.GetAdvancedArchivedTicketsSearch(action, 0, 0, null, "", VectorAPIContext.Current.UserId));
            }

        }

        [HttpGet]
        [Route("GetTicketsRelatedTreeTasks")]
        public IHttpActionResult GetTicketsRelatedTreeTasks(Int64 userId, Int64 ticketId)
        {

            using (var objThreeSixtyDegreesBL = new ThreeSixtyDegreesBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objThreeSixtyDegreesBL.GetBackLogTickets("TreeGridTasksByTicketId", 0, ticketId, null, "", VectorAPIContext.Current.UserId));
            }

        }


        [HttpGet]
        [Route("GetTicketsRelatedArchivedTreeTasks")]
        public IHttpActionResult GetTicketsRelatedArchivedTreeTasks(Int64 userId, Int64 ticketId)
        {

            using (var objThreeSixtyDegreesBL = new ThreeSixtyDegreesBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objThreeSixtyDegreesBL.GetBackLogTickets("ArchivedTreeGridTasksByTicketId", 0, ticketId, null, "", VectorAPIContext.Current.UserId));
            }

        }
        #endregion
        #region GetTakStatusMaster

        [HttpGet]
        [Route("GetTaskStatusMaster")]
        public IHttpActionResult GetTakStatusMaster()
        {
            using (var workBenchBL = new WorkBenchBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(workBenchBL.GetTakStatusMaster());

            }
        }

        #endregion
        #region GetTakStatusMaster

        [HttpGet]
        [Route("GetTaskTypes")]
        public IHttpActionResult GetTaskTypes()
        {
            using (var workBenchBL = new WorkBenchBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(workBenchBL.GetTaskTypes());

            }
        }

        #endregion
        #region Get Users

        [HttpGet]
        [Route("GetUsersSearch")]
        public IHttpActionResult GetUsersSearch(string userName)
        {
            using (var workBenchBL = new WorkBenchBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(workBenchBL.GetUsersSearch(userName));

            }
        }

        #endregion

        #region Get Users

        [HttpPost]
        [Route("AddTicketTask")]
        public IHttpActionResult AddTicketTask([FromBody] TicketTask ticketTask)
        {
            using (var workBenchBL = new WorkBenchBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(workBenchBL.AddTicketTask(ticketTask));

            }
        }

        [HttpPost]
        [Route("ManageTicket")]
        public IHttpActionResult ManageTicket([FromBody] Ticket objTicket)
        {
            using (var workBenchBL = new WorkBenchBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(workBenchBL.ManageTicket(objTicket, VectorAPIContext.Current.UserId));

            }
        }
        #endregion
        #region Vector Auto Task Creation Service

        [HttpPost]
        [Route("VectorAutoTaskCreationService")]
        public IHttpActionResult VectorAutoTaskCreationService(int taskId, bool isLogicYesNo)
        {
            using (var workBenchBL = new WorkBenchBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(workBenchBL.VectorAutoTaskCreationService(taskId, isLogicYesNo));

            }
        }
        #endregion


        [HttpGet]
        [Route("GetManifest")]
        public IHttpActionResult GetManifest(string manifestName)
        {
            using (var workBenchBL = new WorkBenchBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(workBenchBL.GetManifest(manifestName));

            }
        }




        [HttpGet]
        [Route("GetFlows")]
        public IHttpActionResult GetFlows(Int64 manifestId)
        {
            using (var workBenchBL = new WorkBenchBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(workBenchBL.GetFlows(manifestId));

            }
        }



        [HttpGet]
        [Route("GetTasks")]
        public IHttpActionResult GetTasks(Int64 flowId)
        {
            using (var workBenchBL = new WorkBenchBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(workBenchBL.GetTasks(flowId));

            }
        }

        [HttpGet]
        [Route("GetPreviousTasks")]
        public IHttpActionResult GetPreviousTasks(Int64 taskId)
        {
            using (var workBenchBL = new WorkBenchBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(workBenchBL.GetPreviousTasks(taskId, VectorAPIContext.Current.UserId));
            }
        }

        [HttpGet]
        [Route("GetPreviousTaskInfo")]
        public IHttpActionResult GetPreviousTaskInfo(string type, Int64 taskId)
        {
            using (var workBenchBL = new WorkBenchBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(workBenchBL.GetPreviousTaskInfo(type, taskId));
            }
        }

        [HttpGet]
        [Route("GetKeyStatsInfo")]
        public IHttpActionResult GetKeyStatsInfo(Int64 Userid)
        {
            using (var workBenchBL = new WorkBenchBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(workBenchBL.GetKeyStatsInfo(Userid));
            }
        }

        //[HttpGet]
        //[Route("GetTicketInfo")]
        //public IHttpActionResult GetTicketInfo(TicketInfo objTicketInfo)
        //{
        //    using (var workBenchBL = new WorkBenchBL(new VectorDataConnection()))
        //    {
        //        return VectorResponseHandler.GetVectorResponse(workBenchBL.GetTicketInfo(objTicketInfo));
        //    }
        //}

        /// <summary>
        /// Get All Tasks by User
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllUserTasks")]
        public IHttpActionResult GetAllUserTasks(Int64 ticketId, string type, Int64 manifestId, Int64 flowdetailId, string categoryType, string categoryId, string assignedUsers)
        {
            using (var workBenchBL = new WorkBenchBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(workBenchBL.GetAllUserTasks(ticketId, type, VectorAPIContext.Current.UserId, manifestId, flowdetailId, categoryType, categoryId, assignedUsers));
            }
        }

        /// <summary>
        /// Get All Tasks by User
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBackLogTasks")]
        public IHttpActionResult GetBackLogTasks(Int64 ticketId, string type, Int64 manifestId, Int64 flowdetailId)
        {
            using (var workBenchBL = new WorkBenchBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(workBenchBL.GetBackLogTasks(ticketId, type, VectorAPIContext.Current.UserId, manifestId, flowdetailId));
            }
        }

        /// <summary>
        /// Get All Tasks by User
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ManageMyQueue")]
        public IHttpActionResult ManageMyQueue(MyQueue objMyQueue)
        {
            using (var workBenchBL = new WorkBenchBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(workBenchBL.ManageMyQueue(objMyQueue, VectorAPIContext.Current.UserId));
            }
        }


        /// <summary>
        /// Get All Tasks by User
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMyQueue")]
        public IHttpActionResult GetMyQueue(MyQueue objMyQueue)
        {
            using (var workBenchBL = new WorkBenchBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(workBenchBL.GetMyQueue(objMyQueue, VectorAPIContext.Current.UserId));
            }
        }

        /// <summary>
        /// Get All Tasks by User
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ManageTaskInfo")]
        public IHttpActionResult ManageTaskInfo(Tasks objTask)
        {
            using (var workBenchBL = new WorkBenchBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(workBenchBL.ManageTaskInfo(objTask, VectorAPIContext.Current.UserId));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objTicketInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendRequestorEmail")]
        public IHttpActionResult SendRequestorEmail(TicketRequestorEmail objTicket)
        {

            using (var objWorkBenchBL = new WorkBenchBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objWorkBenchBL.SendRequestorEmail(objTicket, VectorAPIContext.Current.UserId));
            }

        }

        [HttpGet]
        [Route("TicketEmailDetails")]
        public IHttpActionResult GetTicketEmailDetails(Int64 ticketId)
        {
            using (var objTicketBl = new TicketsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objTicketBl.GetTicketEmailDetails(ticketId, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("SendEmailFromTicket")]
        public IHttpActionResult SendEmail(NegotiationSendEmail objSendEmail)
        {
            using (var objTicketBl = new TicketsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objTicketBl.SendEmailFromTickets(objSendEmail));
            }
        }


        [HttpGet]
        [Route("GetTicketRelatedTasks")]

        public IHttpActionResult GetTicketRelatedTasks(Int64 ticketId)
        {

            using (var objThreeSixtyDegreesBL = new ThreeSixtyDegreesBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objThreeSixtyDegreesBL.GetTasksRelatedToTicket(ticketId, VectorAPIContext.Current.UserId));
            }

        }

        [HttpPost]
        [Route("ManageUserAssignment")]
        public IHttpActionResult ManageUserAssignment(AssignTo objAssignTo)
        {
            using (var objTicketBl = new TicketsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objTicketBl.ManageUserAssignment(objAssignTo, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("ManageCommonTask")]
        public IHttpActionResult ManageCommonTask(CommonTask objCommonTask)
        {
            using (var objTicketBl = new TicketsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objTicketBl.ManageCommonTask(objCommonTask, VectorAPIContext.Current.UserId));
            }
        }

        [HttpGet]
        [Route("GetCommonTaskInfo")]

        public IHttpActionResult GetCommonTaskInfo(string action, Int64 taskId)
        {
            using (var objTicketBl = new TicketsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objTicketBl.GetCommonTaskInfo(action, taskId, VectorAPIContext.Current.UserId));
            }
        }



        [HttpPost]
        [Route("GetEmails")]
        public IHttpActionResult GetTicketEmailDetails(SearchEntity objSearchEntity)
        {
            using (var objTicketBl = new TicketsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objTicketBl.GetEmails(objSearchEntity, VectorAPIContext.Current.UserId));
            }
        }


        [HttpPost]
        [Route("ManageEmailTicket")]
        public IHttpActionResult ManageEmailTicket(Common.BusinessLayer.EmailProcessManager.MailDetail objMailDetail)
        {
            using (var objTicketBl = new TicketsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objTicketBl.ManageEmailTicket(objMailDetail, VectorAPIContext.Current.UserId));
            }
        }


        [HttpPost]
        [Route("SetEmailToReadStatus")]
        public IHttpActionResult SetEmailToReadStatus(Common.BusinessLayer.EmailProcessManager.MailDetail objMailDetail)
        {
            using (var objTicketBl = new TicketsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objTicketBl.ManageEmailTicket(objMailDetail, VectorAPIContext.Current.UserId,true));
            }
        }

        [HttpPost]
        [Route("GetEmailDocuments")]

        public IHttpActionResult GetEmailDocuments(Common.BusinessLayer.EmailProcessManager.MailDetail objMailDetail)
        {
            using (var objTicketBl = new TicketsBL(new VectorDataConnection()))
            {

                 string fileTimeStamp = DateManager.GenerateTitleWithTimestamp("Email"); 
                string FileServerPath = SecurityManager.GetConfigValue("FileServerPath") + "//Temp//" + fileTimeStamp;  
                string folderPath = SecurityManager.GetConfigValue("VectorDocumentURL") + "//Temp//" + fileTimeStamp;

                return VectorResponseHandler.GetVectorResponse(objTicketBl.GetEmailDocuments(objMailDetail,VectorAPIContext.Current.UserId, FileServerPath, folderPath));
            }
        }
    }
}
