using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Workbench.DataLayer;
using Vector.Workbench.Entities;
using static Vector.Common.BusinessLayer.EmailTemplateEnums;

namespace Vector.Workbench.BusinessLayer
{

    public class WorkBenchBL : DisposeLogic
    {
        private VectorDataConnection objVectorDB;
        public WorkBenchBL(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }
        public VectorResponse<object> GetTakStatusMaster()
        {
            using (var workBenchDL = new WorkBenchDL(objVectorDB))
            {
                var result = workBenchDL.GetTakStatusMaster();
                if (DataValidationLayer.isDataSetNotNull(result))
                {

                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }
        public VectorResponse<object> GetTaskTypes()
        {
            using (var workBenchDL = new WorkBenchDL(objVectorDB))
            {
                var result = workBenchDL.GetTaskTypes();
                if (DataValidationLayer.isDataSetNotNull(result))
                {

                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }
        public VectorResponse<object> GetUsersSearch(string userName)
        {
            using (var workBenchDL = new WorkBenchDL(objVectorDB))
            {
                var result = workBenchDL.GetUsersSearch(userName);
                if (DataValidationLayer.isDataSetNotNull(result))
                {

                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }
        public VectorResponse<object> GetManifest(string manifestName)
        {
            using (var workBenchDL = new WorkBenchDL(objVectorDB))
            {
                var result = workBenchDL.GetManifest(manifestName);
                if (DataValidationLayer.isDataSetNotNull(result))
                {

                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }
        public VectorResponse<object> GetFlows(Int64 manifestId)
        {
            using (var workBenchDL = new WorkBenchDL(objVectorDB))
            {
                var result = workBenchDL.GetFlows(manifestId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {

                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }
        public VectorResponse<object> GetTasks(Int64 flowId)
        {
            using (var workBenchDL = new WorkBenchDL(objVectorDB))
            {
                var result = workBenchDL.GetTasks(flowId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {

                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }
        public VectorResponse<object> AddTicketTask(TicketTask ticketTask)
        {
            using (var workBenchDL = new WorkBenchDL(objVectorDB))
            {
                //2021 - 03 - 21

                //var splitDueDate = ticketTask.DueDate.Split('/');
                //string finalDueDate = splitDueDate[2] + "-" + splitDueDate[0] + "-" + splitDueDate[1];
                //ticketTask.DueDate = finalDueDate;
                var result = workBenchDL.AddTicketTask(ticketTask);
                if (DataValidationLayer.isDataSetNotNull(result))
                {

                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> ManageTicket(Ticket objTicket, Int64 userId)
        {
            using (var workBenchDL = new WorkBenchDL(objVectorDB))
            {
                string tktDocs = string.Empty;

                if (objTicket.TicketDocuments != null && objTicket.TicketDocuments.Count > 0)
                {
                    XDocument ticketDocs = new XDocument(
                         new XElement("ROOT",
                         from ticketDoc in objTicket.TicketDocuments
                         select new XElement("File",
                                     new XElement("FileName", ticketDoc.FileName),
                                     new XElement("FilePath", ticketDoc.FilePath),
                                     new XElement("FileType", ticketDoc.FileType)

                         )));

                    tktDocs = ticketDocs.ToString();
                }
                else
                {

                    tktDocs = "<ROOT></ROOT>";
                }

                var result = workBenchDL.ManageTicket(objTicket, tktDocs, userId);


                if(!string.IsNullOrEmpty(objTicket.emailUIDL))
                {
                    using (var objTickets = new TicketsBL(objVectorDB))
                    {
                        var ticketInfo = (from c in result.Tables[0].AsEnumerable()
                                          select new
                                          {
                                              TicketId = c.Field<Int64>("TicketId"),
                                              TicketFolderPath = c.Field<string>("TicketFolderPath")
                                          }).FirstOrDefault();

                        bool isEmailDocumentExtracted = objTickets.ReadAndAddDocumentToTicket(objTicket.emailAction, userId, ticketInfo.TicketId, ticketInfo.TicketFolderPath, objTicket.emailUIDL);
                    }
                }
                 
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    bool emailResult = SendTicketCreationEmail(result,userId);
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }
         

        public bool SendTicketCreationEmail(DataSet dsResult,Int64 UserId)
        {
            bool emailResult = false;

            try
            {
                var resultInfo = (from c in dsResult.Tables[0].AsEnumerable()
                                  select new
                                  {
                                      TicketId = c.Field<Int64>("TicketId"),
                                      ResponseType = c.Field<string>("ResponseType"),
                                      ToEmail = c.Field<string>("ToEmail"),
                                      CCEmail = c.Field<string>("CCEmail"),
                                      TicketNumber = c.Field<string>("TicketNumber"),
                                      Subject = c.Field<string>("Subject"),
                                      MessageID = c.Field<string>("MessageID")
                                  }).ToList().FirstOrDefault();

                if (!string.IsNullOrEmpty(resultInfo.MessageID) && StringManager.IsEqual(resultInfo.ResponseType, "CreateTicket"))
                {
                    EmailTemplate objEmailTemplate = EmailTemplateManager.GetEmailTemplate(AppDomain.CurrentDomain.BaseDirectory, EmailTemplates.Common,
                                                                EmailTemplateGUIDs.TicketRequestorEmail);


                    string emailBody = string.Format(objEmailTemplate.EmailBody, resultInfo.TicketNumber, resultInfo.MessageID);

                    string subject = string.Format(objEmailTemplate.Subject, resultInfo.Subject);

                    string logoPath = HttpContext.Current.Server.MapPath(VectorConstants.TildeSeparator.ToString());
                    logoPath += objEmailTemplate.EmailImagesList[default(int)].Path;


                    emailResult = EmailManager.SendEmailWithMutipleAttachments(resultInfo.ToEmail, SecurityManager.GetConfigValue("FromEmailTicket"), subject,
                                                                        emailBody, resultInfo.CCEmail, "", null, "", false, logoPath,
                                                                        smtpSection: SecurityManager.GetSmtpMailSection("TicketMail"));
                }

                
            }
            catch(Exception ex)
            {
                VectorTextLogger.LogErrortoFile("Unable to Send Email on Ticket Creation"  +  ex.Message);
            }

            return emailResult;
        }

        public VectorResponse<object> VectorAutoTaskCreationService(int taskId, bool isLogicYesNo)
        {
            using (var workBenchDL = new WorkBenchDL(objVectorDB))
            {
                var result = workBenchDL.VectorAutoTaskCreationService(taskId, isLogicYesNo);
                if (DataValidationLayer.isDataSetNotNull(result))
                {

                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> GetPreviousTasks(Int64 taskId, Int64 userId)
        {
            using (var workBenchDL = new WorkBenchDL(objVectorDB))
            {
                var result = workBenchDL.GetPreviousTasks(taskId, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {

                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> GetPreviousTaskInfo(string type, Int64 taskId)
        {
            using (var workBenchDL = new WorkBenchDL(objVectorDB))
            {
                var result = workBenchDL.GetPreviousTaskInfo(type, taskId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {

                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> GetKeyStatsInfo(Int64 Userid)
        {
            using (var workBenchDL = new WorkBenchDL(objVectorDB))
            {
                var result = workBenchDL.GetKeyStatsInfo(Userid);
                if (DataValidationLayer.isDataSetNotNull(result))
                {

                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        //public VectorResponse<object> GetTicketInfo(TicketInfo objTicketInfo)
        //{
        //    using (var workBenchDL = new WorkBenchDL(objVectorDB))
        //    {
        //        var result = workBenchDL.GetTicketInfo(objTicketInfo);
        //        if (DataValidationLayer.isDataSetNotNull(result))
        //        {

        //            return new VectorResponse<object>() { ResponseData = result };

        //        }
        //        else
        //        {
        //            return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

        //        }
        //    }
        //}

        public VectorResponse<object> GetAllUserTasks(Int64 ticketId, string type, Int64 userId, Int64 manifestId, Int64 flowdetailId, string categoryType, string categoryId, string assignedUsers)
        {
            using (var workBenchDL = new WorkBenchDL(objVectorDB))
            {
                var result = workBenchDL.GetAllUserTasks(ticketId, type, userId, manifestId, flowdetailId, categoryType, categoryId, assignedUsers);
                if (!string.Equals(type, "MyQueue"))
                {
                    if (DataValidationLayer.isDataSetNotNull(result))
                    {

                        result.Tables[VectorConstants.Zero].TableName = "summary";
                        return new VectorResponse<object>() { ResponseData = result };
                    }
                    else
                    {
                        return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                    }
                }
                else
                {
                    return new VectorResponse<object>() { ResponseData = formatworkbenchData(result, type) };
                }

            }
        }

        public VectorResponse<object> GetBackLogTasks(Int64 ticketId, string type, Int64 userId, Int64 manifestId, Int64 flowdetailId)
        {
            using (var workBenchDL = new WorkBenchDL(objVectorDB))
            {
                var result = workBenchDL.GetBackLogTasks(ticketId, type, userId, manifestId, flowdetailId);
               
                    if (DataValidationLayer.isDataSetNotNull(result))
                    {

                        result.Tables[VectorConstants.Zero].TableName = "summary";
                        return new VectorResponse<object>() { ResponseData = result };
                    }
                    else
                    {
                        return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                    }
                 
            }
        }

        private object formatworkbenchData(DataSet dsData, string type)
        {
            MyQueueInfo objMyQueueInfo = new MyQueueInfo();
            List<Tickets> objTicketsInfo = new List<Tickets>();
            var result = (from c in dsData.Tables[0].AsEnumerable()
                          select new
                          {
                              TicketId = c.Field<Int64>("TicketId"),
                              TicketNumber = c.Field<string>("TicketNumber"),
                              TicketCreatedUser = c.Field<string>("TicketCreatedUser"),
                              TicketCreatedUserIcon = c.Field<string>("TicketCreatedUserIcon"),
                              TotalTasks = c.Field<int>("TotalTasks"),
                              CompletedTasks = c.Field<int>("CompletedTasks"),
                              PendingTasks = c.Field<int>("PendingTasks"),
                              isTicketAvailableForEdit = c.Field<Boolean>("isTicketAvailableForEdit"),
                              TicketSubject = c.Field<string>("Subject")
                          }).Distinct().ToList();

            foreach (var objTicket in result)
            {
                Tickets objTickets = new Tickets();
                objTickets.TicketId = objTicket.TicketId;
                objTickets.TicketNumber = objTicket.TicketNumber;
                objTickets.TicketCreatedUser = objTicket.TicketCreatedUser;
                objTickets.TicketCreatedUserIcon = objTicket.TicketCreatedUserIcon;
                objTickets.TotalTasks = objTicket.TotalTasks;
                objTickets.CompletedTasks = objTicket.CompletedTasks;
                objTickets.PendingTasks = objTicket.PendingTasks;
                objTickets.isTicketAvailableForEdit = objTicket.isTicketAvailableForEdit;
                objTickets.TicketSubject = objTicket.TicketSubject;

                List<Tasks> objTasksList = new List<Tasks>();

                List<Tasks> tasksList = (from c in dsData.Tables[0].AsEnumerable()
                                         where c.Field<Int64>("TicketId") == objTicket.TicketId &&
                                           c.Field<Int64>("TaskId") != 0
                                         select new Tasks
                                         {
                                             TaskId = c.Field<Int64>("TaskId"),
                                             TaskNumber = c.Field<string>("TaskNumber"),
                                             TaskAssignedUser = c.Field<string>("TaskAssignedUser"),
                                             TaskAssignedUserIcon = c.Field<string>("TaskAssignedUserIcon"),
                                             TaskName = c.Field<string>("TaskName"),
                                             TaskDescription = c.Field<string>("TaskDescription"),
                                             TaskEstimatedHours = c.Field<Int32>("TaskEstimatedHours"),
                                             TaskSpentHours = c.Field<Int32>("TaskSpentHours"),
                                             TaskRemainingHours = c.Field<Int32>("TaskRemainingHours"),
                                             TaskRemainingMin = c.Field<Int32>("TaskRemainingMin"),
                                             TaskLastVisitedHours = c.Field<Int32>("TaskLastVisitedHours"),
                                             TaskLastVisitedMin = c.Field<Int32>("TaskLastVisitedMin"),
                                             ComponentName = c.Field<string>("ComponentName"),
                                             RoutingPath = c.Field<string>("RoutingPath"),
                                             isTaskAvailableForEdit = c.Field<Boolean>("isTaskAvailableForEdit"),
                                             TaskStatus = c.Field<string>("TaskStatus"),
                                             ColorCode = c.Field<string>("ColourCode"),
                                         }).Distinct().ToList();

                int availableTasks = (from c in tasksList
                                      where c.isTaskAvailableForEdit
                                      select c).Count();
                objTickets.AvailableTasks = availableTasks;
                objTickets.Tasks = tasksList;
                objTicketsInfo.Add(objTickets);
            }


            objMyQueueInfo.Tickets = objTicketsInfo;

            return objMyQueueInfo;
        }

        public VectorResponse<object> ManageMyQueue(MyQueue objMyQueue, Int64 userId)
        {
            using (var workBenchDL = new WorkBenchDL(objVectorDB))
            {
                var result = workBenchDL.ManageMyQueue(objMyQueue, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> GetMyQueue(MyQueue objMyQueue, Int64 userId)
        {
            using (var workBenchDL = new WorkBenchDL(objVectorDB))
            {
                var result = workBenchDL.GetMyQueue(objMyQueue, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> ManageTaskInfo(Tasks objTasks, Int64 userId)
        {
            using (var workBenchDL = new WorkBenchDL(objVectorDB))
            {
                var result = workBenchDL.ManageTaskInfo(objTasks, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> SendRequestorEmail(TicketRequestorEmail objTicket, Int64 userId)
        {
            using (var workBenchDL = new WorkBenchDL(objVectorDB))
            {
                EmailTemplate objEmailTemplate = EmailTemplateManager.GetEmailTemplate(AppDomain.CurrentDomain.BaseDirectory, EmailTemplates.Common,
                                                             EmailTemplateGUIDs.TicketRequestorEmail);
                string messageId = string.IsNullOrEmpty(objTicket.MessageId) ? GetEmailUniqueId(objTicket.TicketId) : objTicket.MessageId;
                string emailBody = string.Format(objEmailTemplate.EmailBody, objTicket.TicketNumber, messageId);

                string subject = string.Format(objEmailTemplate.Subject, objTicket.Subject);

                string logoPath = HttpContext.Current.Server.MapPath(VectorConstants.TildeSeparator.ToString());
                logoPath += objEmailTemplate.EmailImagesList[default(int)].Path;

                bool emailResult = false;

                emailResult = EmailManager.SendEmailWithMutipleAttachments(objTicket.RequestorEmail, SecurityManager.GetConfigValue("FromEmailTicket"), subject,
                                                                    emailBody, "", "", null, "", false, logoPath,
                                                                    smtpSection: SecurityManager.GetSmtpMailSection("TicketMail"));
                if (emailResult)
                {
                    var result = workBenchDL.UpdateTicketRequestorDetails(objTicket.TicketId, objTicket.RequestorName, objTicket.RequestorEmail, messageId, userId);
                    if (DataValidationLayer.isDataSetNotNull(result))
                    {
                        return new VectorResponse<object>() { ResponseData = "Email sent successfully" };
                    }
                    else
                    {
                        return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Email sent successfully,Failed to update Email details." } };
                    }

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to send an Email.Please contact Administrator." } };
                }
            }
        }
        public string GetEmailUniqueId(string value)
        {
            string MessageID = "";
            Guid objGuid = new Guid();
            objGuid = Guid.NewGuid();
            MessageID = string.Format("<{0}~{1}>", value, objGuid.ToString());

            return MessageID;
        }

    }
}
    