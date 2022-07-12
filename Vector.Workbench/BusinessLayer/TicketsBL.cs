using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Workbench.DataLayer;
using Vector.Workbench.Entities;
using static Vector.Common.BusinessLayer.EmailTemplateEnums;
using static Vector.Common.BusinessLayer.EmailProcessManager;
using System.Net.Configuration;
using System.Data;
using System.Linq;
using Vector.Garage.Entities;

namespace Vector.Workbench.BusinessLayer
{
    public class TicketsBL : DisposeLogic
    {
        private VectorDataConnection objVectorDB;
        public TicketsBL(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }


        public VectorResponse<object> GetMessageBoxInfo(string action, Int64 userId)
        {
            using (var objTicketsDL = new TicketsDL(objVectorDB))
            {
                var result = objTicketsDL.GetMessageBoxInfo(action, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "Summary";
                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> GetTicketDetails(TicketInfo objTicketInfo, Int64 userId)
        {
            using (var objTicketsDL = new TicketsDL(objVectorDB))
            {
                var result = objTicketsDL.GetTicketDetails(objTicketInfo, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "TicketDetails";
                    result.Tables[1].TableName = "Documents";
                    result.Tables[2].TableName = "Activity";
                    result.Tables[3].TableName = "Tags";
                    result.Tables[4].TableName = "Instructions";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> ManageTicketDetails(TicketInfo objTicketInfo, Int64 userId)
        {
            using (var objTicketsDL = new TicketsDL(objVectorDB))
            {
                var result = objTicketsDL.ManageTicketDetails(objTicketInfo, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    string emailResult = SendTicketEmailToUsers(objTicketInfo.TicketId, objTicketInfo.TicketStatus, objTicketInfo.UserId);

                    if (string.IsNullOrEmpty(emailResult))
                        return new VectorResponse<object>() { ResponseData = result };
                    else
                        return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Ticket Details Updated Successfully,Failed to send an email." } };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        private string SendTicketEmailToUsers(Int64 ticketId, string ticketStatus, long userId)
        {
            string emailResult = string.Empty;
            if (ticketStatus == "Completed" || ticketStatus == "Abandonded")
            {
                using (var objTicketsDL = new TicketsDL(objVectorDB))
                {
                    var result = objTicketsDL.GetTicketEmailDetails(ticketId, Convert.ToInt64(userId));
                    if (DataValidationLayer.isDataSetNotNull(result))
                    {
                        string TicketNo = result.Tables[0].Rows[0]["TicketNo"].ToString();
                        string CreatedUserEmail = result.Tables[0].Rows[0]["CreatedUser"].ToString();
                        string TaggedEmail = result.Tables[0].Rows[0]["TaggedEmail"].ToString();
                        string ModifiedUserEmail = result.Tables[0].Rows[0]["ModifiedUserEmail"].ToString();
                        string ModifiedUser = result.Tables[0].Rows[0]["ModifiedUserName"].ToString();
                        EmailTemplate objEmailTemplate = EmailTemplateManager.GetEmailTemplate(AppDomain.CurrentDomain.BaseDirectory, EmailTemplates.Common, EmailTemplateGUIDs.TicketStatusUpdateEmail);

                        string emailBody = string.Format(objEmailTemplate.EmailBody, TicketNo, ticketStatus, DateTime.Now.ToString(), ModifiedUser, SecurityManager.GetConfigValue("VectorWebSite"));

                        string subject = string.Format(objEmailTemplate.Subject, TicketNo);

                        string logoPath = HttpContext.Current.Server.MapPath(VectorConstants.TildeSeparator.ToString());
                        logoPath += objEmailTemplate.EmailImagesList[default(int)].Path;

                        if (!string.IsNullOrEmpty(TaggedEmail))
                            CreatedUserEmail = CreatedUserEmail + "," + TaggedEmail;

                        if (!string.IsNullOrEmpty(ModifiedUserEmail))
                            CreatedUserEmail = CreatedUserEmail + "," + ModifiedUserEmail;

                        bool email = EmailManager.SendEmailWithMutipleAttachments(CreatedUserEmail, SecurityManager.GetConfigValue("FromEmail"), subject,
                                                                            emailBody, string.Empty, string.Empty, null, string.Empty, false, logoPath);
                        if (!email)
                        {
                            emailResult = "Failed";
                        }
                    }
                    else
                    {
                        emailResult = "Failed";
                    }
                }
            }
            return emailResult;
        }

        public VectorResponse<object> GetTicketEmailDetails(Int64 ticketId, Int64 userId)
        {
            using (var objTicketsDL = new TicketsDL(objVectorDB))
            {
                var result = objTicketsDL.GetTicketEmailDetails(ticketId, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    if (!DataManager.IsNullOrEmptyDataSet(result))
                    {
                        var resultInfo = (from c in result.Tables[VectorConstants.Zero].AsEnumerable()
                                          select c.Field<string>("TicketType")).ToList().FirstOrDefault();

                        SmtpSection smtpSection = SecurityManager.GetSmtpMailSection("TicketMail");
                        var emailresult = GetEmailDetailsByTicket(result.Tables[0].Rows[0]["MessageId"].ToString(), SecurityManager.GetConfigValue("EmailServer"),
                                                                smtpSection.Network.UserName,
                                                                smtpSection.Network.Password, SecurityManager.GetConfigValue("ServerProtocol"));


                        if (!string.IsNullOrEmpty(resultInfo) && string.Equals(resultInfo, "ShortPay"))
                        {
                            SmtpSection smtpSection1 = SecurityManager.GetSmtpMailSection("ShortPayMail");
                            emailresult = GetEmailDetailsByTicket(result.Tables[0].Rows[0]["MessageId"].ToString(), SecurityManager.GetConfigValue("EmailServer"),
                                                                    smtpSection1.Network.UserName,
                                                                    smtpSection1.Network.Password, SecurityManager.GetConfigValue("ServerProtocol"));
                        }





                        //emailresult.MailDetailsList.
                        if (emailresult.MailDetailsList.Count != 0)
                        {
                            return new VectorResponse<object>() { ResponseData = emailresult };
                        }
                        else
                            return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                    }
                    else
                        return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Email Details Found." } };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> SendEmailFromTickets(NegotiationSendEmail objSendEmail)
        {
            using (var objTicketsDL = new TicketsDL(objVectorDB))
            {

                EmailTemplate objEmailTemplate = EmailTemplateManager.GetEmailTemplate(AppDomain.CurrentDomain.BaseDirectory, EmailTemplates.Common, EmailTemplateGUIDs.CommonEmail);

                string emailBody = string.Format(objEmailTemplate.EmailBody, objSendEmail.Body, SecurityManager.GetConfigValue("VectorURL"), SecurityManager.GetConfigValue("VectorWebSite"));

                string subject = string.Format(objEmailTemplate.Subject, objSendEmail.Subject);

                string logoPath = HttpContext.Current.Server.MapPath(VectorConstants.TildeSeparator.ToString());
                logoPath += objEmailTemplate.EmailImagesList[default(int)].Path;

                bool emailResult = EmailManager.SendEmailWithMutipleAttachments(objSendEmail.To,
                    SecurityManager.GetConfigValue("FromEmailTicket"), subject,
                                                                    emailBody, objSendEmail.CC, objSendEmail.BCC, objSendEmail.EmailAttachments,
                                                                    objSendEmail.AttachmentsFolderPath, objSendEmail.IsTempFolder, logoPath,
                                                                    smtpSection: SecurityManager.GetSmtpMailSection("TicketMail"));

                if (emailResult)
                {
                    SmtpSection smtpSection = SecurityManager.GetSmtpMailSection("TicketMail");
                    List<string> emailUniqueIds = new List<string>() { objSendEmail.Uidl };
                    int emailresult = MarkMailAsReadFromES(SecurityManager.GetConfigValue(VectorEnums.ConfigValue.EmailServer.ToString()),
                                                            smtpSection.Network.UserName,
                                                            smtpSection.Network.Password,
                                                            SecurityManager.GetConfigValue(VectorEnums.ConfigValue.ServerProtocol.ToString()), emailUniqueIds);
                    if (emailresult == 0)
                        return new VectorResponse<object>() { ResponseData = "Email sent successfully" };
                    else
                        return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Email sent successfully,Failed to update Email as Read." } };
                }
                else
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to send an Email.Please contact Administrator." } };
            }
        }

        public VectorResponse<object> ManageUserAssignment(AssignTo action, Int64 userId)
        {
            using (var objTicketsDL = new TicketsDL(objVectorDB))
            {
                var result = objTicketsDL.ManageUserAssignment(action, userId);
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



        public VectorResponse<object> ManageCommonTask(CommonTask objCommonTask, Int64 userId)
        {
            using (var objTicketsDL = new TicketsDL(objVectorDB))
            {
                var result = objTicketsDL.ManageCommonTask(objCommonTask, userId);
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

        public VectorResponse<object> GetCommonTaskInfo(string action, Int64 taskId, Int64 userId)
        {
            using (var objTicketsDL = new TicketsDL(objVectorDB))
            {
                var result = objTicketsDL.GetCommonTaskInfo(action, taskId, userId);
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


        public VectorResponse<object> GetEmails(SearchEntity objSearchEntity, Int64 userId)
        {
            using (var objTicketsDL = new TicketsDL(objVectorDB))
            {


                string server = SecurityManager.GetConfigValue("EmailServer");

                SmtpSection smtpSection = new SmtpSection();


                string serverProtocol = SecurityManager.GetConfigValue("ServerProtocol");

                if (objSearchEntity.action.Equals("noc"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("NOCMail");
                }
                else if (objSearchEntity.action.Equals("ticket"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("TicketMail");
                }
                else if (objSearchEntity.action.Equals("shortpay"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("ShortPayMail");
                }
                else if (objSearchEntity.action.Equals("negotiation"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("NegotiaitonMail");
                }
                else if (objSearchEntity.action.Equals("missing"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("MissingMail");
                }
                else if (objSearchEntity.action.Equals("contract"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("ContractsMail");
                }
                else if (objSearchEntity.action.Equals("invoice"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("InvoiceMail");
                }


                var emailresult = EmailProcessManager.GetEmails(SecurityManager.GetConfigValue("EmailServer"),
                                                                smtpSection.Network.UserName,
                                                                smtpSection.Network.Password, SecurityManager.GetConfigValue("ServerProtocol"));


                //emailresult.MailDetailsList.
                if (emailresult.MailDetailsList.Count != 0)
                {
                    return new VectorResponse<object>() { ResponseData = emailresult };
                }
                else
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

            }
        }



        public VectorResponse<object> ManageEmailTicket(Common.BusinessLayer.EmailProcessManager.MailDetail objMailDetail, Int64 userId, bool isEmailRead = false)
        {
            using (var objTicketsDL = new TicketsDL(objVectorDB))
            {

                string server = SecurityManager.GetConfigValue("EmailServer");

                SmtpSection smtpSection = new SmtpSection();


                string serverProtocol = SecurityManager.GetConfigValue("ServerProtocol");

                if (objMailDetail.Action.Equals("noc"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("NOCMail");
                }
                else if (objMailDetail.Action.Equals("ticket"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("TicketMail");
                }
                else if (objMailDetail.Action.Equals("shortpay"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("ShortPayMail");
                }
                else if (objMailDetail.Action.Equals("negotiation"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("NegotiaitonMail");
                }
                else if (objMailDetail.Action.Equals("missing"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("MissingMail");
                }
                else if (objMailDetail.Action.Equals("contract"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("ContractsMail");
                }
                else if (objMailDetail.Action.Equals("invoice"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("InvoiceMail");
                }


                var result = objTicketsDL.ManageEmailTicket(objMailDetail, userId, isEmailRead, "", 0);



                var ticketInfo = (from c in result.Tables[0].AsEnumerable()
                                  select new
                                  {
                                      TicketPath = c.Field<string>("TicketFolderPath"),
                                      TicketId = c.Field<Int64>("TicketId")
                                  }).FirstOrDefault();


                var downloadedDocuments = EmailProcessManager.DownloadEmailDocuments(SecurityManager.GetConfigValue("EmailServer"),
                                                                smtpSection.Network.UserName,
                                                                smtpSection.Network.Password, SecurityManager.GetConfigValue("ServerProtocol"), objMailDetail.UIDL, ticketInfo.TicketPath);
                if (downloadedDocuments != null && downloadedDocuments.Count > 0)
                {
                    string documentLits = "";

                    foreach (var doc in downloadedDocuments)
                    {
                        if (string.IsNullOrEmpty(documentLits))
                            documentLits = doc;
                        else
                            documentLits = documentLits + "|" + doc;
                    }

                    objMailDetail.Action = "AddDocuments";
                    var isDocumentUploaded = objTicketsDL.ManageEmailTicket(objMailDetail, userId, isEmailRead, documentLits, ticketInfo.TicketId);
                }

                var isUpdated = EmailProcessManager.SetMailAsRead(SecurityManager.GetConfigValue("EmailServer"),
                                                          smtpSection.Network.UserName,
                                                          smtpSection.Network.Password, SecurityManager.GetConfigValue("ServerProtocol"), objMailDetail.UIDL);






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

        public VectorResponse<object> GetEmailDocuments(Common.BusinessLayer.EmailProcessManager.MailDetail objMailDetail, Int64 userId,string folderPath, string folderPathUrl)
        {
            using (var objTicketsDL = new TicketsDL(objVectorDB))
            {

                string server = SecurityManager.GetConfigValue("EmailServer");

                SmtpSection smtpSection = new SmtpSection();


                string serverProtocol = SecurityManager.GetConfigValue("ServerProtocol");

                if (objMailDetail.Action.Equals("noc"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("NOCMail");
                }
                else if (objMailDetail.Action.Equals("ticket"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("TicketMail");
                }
                else if (objMailDetail.Action.Equals("shortpay"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("ShortPayMail");
                }
                else if (objMailDetail.Action.Equals("negotiation"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("NegotiaitonMail");
                }
                else if (objMailDetail.Action.Equals("missing"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("MissingMail");
                }
                else if (objMailDetail.Action.Equals("contract"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("ContractsMail");
                }
                else if (objMailDetail.Action.Equals("invoice"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("InvoiceMail");
                }


                 
                
                var downloadedDocuments = EmailProcessManager.GetEmailDocuments(SecurityManager.GetConfigValue("EmailServer"),
                                                                smtpSection.Network.UserName,
                                                                smtpSection.Network.Password, SecurityManager.GetConfigValue("ServerProtocol"), objMailDetail, folderPath, folderPathUrl);
                 

                


                if (DataValidationLayer.isDataSetNotNull(downloadedDocuments))
                {
                    return new VectorResponse<object>() { ResponseData = downloadedDocuments };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }



            }
        }


        public bool ReadAndAddDocumentToTicket(string action, Int64 userId,Int64 ticketId,string ticketFolderPath,string udil)
        {
            using (var objTicketsDL = new TicketsDL(objVectorDB))
            {

                string server = SecurityManager.GetConfigValue("EmailServer");

                SmtpSection smtpSection = new SmtpSection();


                string serverProtocol = SecurityManager.GetConfigValue("ServerProtocol");

                if (action.Equals("noc"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("NOCMail");
                }
                else if (action.Equals("ticket"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("TicketMail");
                }
                else if (action.Equals("shortpay"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("ShortPayMail");
                }
                else if (action.Equals("negotiation"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("NegotiaitonMail");
                }
                else if (action.Equals("missing"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("MissingMail");
                }
                else if (action.Equals("contract"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("ContractsMail");
                }
                else if (action.Equals("invoice"))
                {
                    smtpSection = SecurityManager.GetSmtpMailSection("InvoiceMail");
                } 
                 
                var downloadedDocuments = EmailProcessManager.DownloadEmailDocuments(SecurityManager.GetConfigValue("EmailServer"),
                                                                smtpSection.Network.UserName,
                                                                smtpSection.Network.Password, SecurityManager.GetConfigValue("ServerProtocol"), udil, ticketFolderPath);
                if (downloadedDocuments != null && downloadedDocuments.Count > 0)
                {
                    string documentLits = "";

                    foreach (var doc in downloadedDocuments)
                    {
                        if (string.IsNullOrEmpty(documentLits))
                            documentLits = doc;
                        else
                            documentLits = documentLits + "|" + doc;
                    }

                    MailDetail objMailDetail = new MailDetail();
                    objMailDetail.Action = "AddDocuments";
                    objMailDetail.UIDL = udil;

                    var isDocumentUploaded = objTicketsDL.ManageEmailTicket(objMailDetail, userId, false, documentLits, ticketId);
                }

                var isUpdated = EmailProcessManager.SetMailAsRead(SecurityManager.GetConfigValue("EmailServer"),
                                                          smtpSection.Network.UserName,
                                                          smtpSection.Network.Password, SecurityManager.GetConfigValue("ServerProtocol"), udil);




                return isUpdated;
            }
        }


    }


}   
