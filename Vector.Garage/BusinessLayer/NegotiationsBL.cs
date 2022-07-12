using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Garage.DataLayer;
using Vector.Garage.Entities;
using static Vector.Common.BusinessLayer.EmailProcessManager;
using static Vector.Common.BusinessLayer.EmailTemplateEnums;

namespace Vector.Garage.BusinessLayer
{
    public class NegotiationsBL : DisposeLogic
    {
        private VectorDataConnection objVectorDB;
        public NegotiationsBL(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }

        public VectorResponse<object> VectorManageNegotiationsBL(Negotiations objNegotiations)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {

                var result = objNegotiationsDL.VectorManageNegotiationsDL(objNegotiations);
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

        public VectorResponse<object> GetNegotiationsInfoBL(NegotiationSearch objNegotiationSearch)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                var result = objNegotiationsDL.GetNegotiationsInfoDL(objNegotiationSearch);
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

        public VectorResponse<object> GetNegotiations(NegotiationSearch objNegotiationSearch)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                var result = objNegotiationsDL.GetNegotiations(objNegotiationSearch);
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

        public VectorResponse<object> VectorGetNegotiationsBidSheetInfoBL(NegotiationsBidSheetInfo objNegotiationsBidSheetInfo)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                var result = objNegotiationsDL.VectorGetNegotiationsBidSheetInfoDL(objNegotiationsBidSheetInfo);
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

        public VectorResponse<object> GetDraftNegotiationsBL(NegotiationSearch objNegotiationSearch)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                var result = objNegotiationsDL.GetDraftNegotiationsDL(objNegotiationSearch);
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

        public VectorResponse<object> GetNegotiationBaselineDetails(NegotiationSearch objNegotiationSearch)
        {

            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                var result = objNegotiationsDL.GetNegotiationBaselineDetails(objNegotiationSearch);
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

        public VectorResponse<object> VectorAddUpdateNegotiationBidValues(NegotiationsBidValues objNegotiationsBidValues)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                var result = objNegotiationsDL.VectorAddUpdateNegotiationBidValues(objNegotiationsBidValues);
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

        public VectorResponse<object> VectorManageDraftNegotiationsBL(DraftNegotiations objDraftNegotiations)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                var result = objNegotiationsDL.VectorManageDraftNegotiationsDL(objDraftNegotiations);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    if(objDraftNegotiations.SaveOrComplete.Equals("Save") || objDraftNegotiations.SaveOrComplete.Equals("SAVE"))
                    {
                        return new VectorResponse<object>() { ResponseData = result };
                    }
                    else
                    return new VectorResponse<object>() { ResponseData = SendRequestBidVendorEmail(objDraftNegotiations) };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to submit Negotiation Details." } };
                }
            }
        }

        private List<EmailResult> SendRequestBidVendorEmail(DraftNegotiations objDraftNegotiations)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                DataSet vendorDetails = objDraftNegotiations.RequestbidVendorDetails.ListToDataSet();
                List<EmailResult> list = new List<EmailResult>();

                using (EmailResult objEmailResult = new EmailResult())
                {
                    objEmailResult.Result = true;
                    objEmailResult.ResultDesc = "Negotiation details submitted successfully.";
                    list.Add(objEmailResult);
                }

                //StringBuilder objVendorSuccess = new StringBuilder();
                //StringBuilder objVendorFail = new StringBuilder();
                //DataSet emailDataSet = new DataSet();
                //DataTable emailData = new DataTable();
                //emailData.TableName = "EmailData";
                //emailData.Columns.Add(VectorEnums.Column.VendorId.ToString(), typeof(string));
                //emailData.Columns.Add(VectorEnums.Column.MessageId.ToString(), typeof(string));
                //emailData.Columns.Add(VectorEnums.Column.UIDL.ToString(), typeof(string));
                //if (!DataManager.IsNullOrEmptyDataSet(vendorDetails))
                //{
                //    foreach (DataRow row in vendorDetails.Tables[0].Rows)
                //    {
                //        EmailTemplate objEmailTemplate = EmailTemplateManager.GetEmailTemplate(AppDomain.CurrentDomain.BaseDirectory, EmailTemplates.Common, EmailTemplateGUIDs.RequestBidEmail);
                //        string messageId = GetNegotiationEmailUniqueId(objDraftNegotiations.NegotiationId.ToString());
                //        string emailBody = string.Format(objEmailTemplate.EmailBody, row["VendorName"].ToString(), SecurityManager.GetConfigValue("VectorURL"),
                //                                    SecurityManager.GetConfigValue("VectorWebSite"), messageId);

                //        string subject = string.Format(objEmailTemplate.Subject, row["VendorName"].ToString());

                //        string logoPath = HttpContext.Current.Server.MapPath(VectorConstants.TildeSeparator.ToString());
                //        logoPath += objEmailTemplate.EmailImagesList[default(int)].Path;

                //        bool emailResult = EmailManager.SendEmailWithMutipleAttachments(row["VendorEmail"].ToString(), SecurityManager.GetConfigValue("FromEmail"), subject,
                //                                                            emailBody, "", "", null, "", false, logoPath, MessageID: messageId);
                //        if (emailResult)
                //        {
                //            AddDataToEmailTable(emailData, row[VectorEnums.Column.VendorId.ToString()].ToString(), messageId);
                //            objVendorSuccess.Append(row["VendorName"].ToString() + ",");
                //        }
                //        else
                //            objVendorFail.Append(row["VendorName"].ToString() + ",");
                //    }

                //    if (!string.IsNullOrEmpty(objVendorSuccess.ToString()))
                //    {
                //        if (!DataManager.IsNullOrEmptyDataTable(emailData))
                //            emailDataSet.Tables.Add(emailData);
                //        UpdateNegotiationEmailDetails(objDraftNegotiations.NegotiationId.ToString(),
                //                                      emailDataSet,
                //                                      emailData);
                //        EmailResult objEmailResult = new EmailResult();
                //        objEmailResult.Result = true;
                //        objEmailResult.ResultDesc = "Email sent successfully to Vendor(s) " + objVendorSuccess.ToString().TrimEnd(',');
                //        list.Add(objEmailResult);
                //    }

                //    if (!string.IsNullOrEmpty(objVendorFail.ToString()))
                //    {
                //        EmailResult objEmailResultFail = new EmailResult();
                //        objEmailResultFail.Result = false;
                //        objEmailResultFail.ResultDesc = "Unable to send an email to Vendor(s) " + objVendorFail.ToString().TrimEnd(',');
                //        list.Add(objEmailResultFail);
                //    }
                //}
                return list;
            }
        }

        private static void AddDataToEmailTable(DataTable emailData, string vendorId, string messageId)
        {
            DataRow rowDate = emailData.NewRow();
            rowDate[VectorEnums.Column.VendorId.ToString()] = vendorId;
            rowDate[VectorEnums.Column.MessageId.ToString()] = messageId;
            rowDate[VectorEnums.Column.UIDL.ToString()] = "";
            emailData.Rows.Add(rowDate);
        }

        private void UpdateNegotiationEmailDetails(string negotiationId, DataSet emailDataSet, DataTable emailData)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                if (!DataManager.IsNullOrEmptyDataSet(emailDataSet))
                {
                    objNegotiationsDL.UpdateNegotiationEmailUniqueId("MessageId", negotiationId, emailDataSet);
                    //DataTable mailDetails = GetEmailParentIdByMessageId(emailData, SecurityManager.GetConfigValue("EmailServer"), SecurityManager.GetConfigValue("EmailId"),
                    //                                        SecurityManager.GetConfigValue("Password"), SecurityManager.GetConfigValue("ServerProtocol"));
                    //if(!DataManager.IsNullOrEmptyDataTable(mailDetails))
                    //{
                    //    mailDetails.TableName = "EmailData";
                    //    DataSet data = new DataSet();
                    //    data.Tables.Add(mailDetails);
                    //    objNegotiationsDL.UpdateNegotiationEmailUniqueId("EmailUniqueId", negotiationId, data);
                    //}
                }
            }
        }

        public VectorResponse<object> VectorManageNegotiationBidSheetInfoBL(NegotiationBidSheetInfo objNegotiationBidSheetInfo)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                var result = objNegotiationsDL.VectorManageNegotiationBidSheetInfoDL(objNegotiationBidSheetInfo);
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

        public VectorResponse<object> VectorAddUpdateBaselineNegotiationLineItemsBL(BaselineNegotiationLineItems objBaselineNegotiationLineItems)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                var result = objNegotiationsDL.VectorAddUpdateBaselineNegotiationLineItemsDL(objBaselineNegotiationLineItems);
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
        public VectorResponse<object> UpdateNegotiationLineItem(NegotiationLineItemData objNegotiationLineItem)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                var result = objNegotiationsDL.UpdateNegotiationLineItem(objNegotiationLineItem);
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

        public VectorResponse<object> SendRequestBidEmail(string userId, string negotiationId, string vendorId)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                var result = objNegotiationsDL.GetRequestBidEmails(userId, negotiationId, vendorId);
                List<EmailResult> list = new List<EmailResult>();
                StringBuilder objVendorSuccess = new StringBuilder();
                StringBuilder objVendorFail = new StringBuilder();
                DataSet emailDataSet = new DataSet();
                DataTable emailData = new DataTable();
                emailData.TableName = "EmailData";
                emailData.Columns.Add(VectorEnums.Column.VendorId.ToString(), typeof(string));
                emailData.Columns.Add(VectorEnums.Column.MessageId.ToString(), typeof(string));
                emailData.Columns.Add(VectorEnums.Column.UIDL.ToString(), typeof(string));
                if (!DataManager.IsNullOrEmptyDataSet(result))
                {
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        EmailTemplate objEmailTemplate = EmailTemplateManager.GetEmailTemplate(AppDomain.CurrentDomain.BaseDirectory, EmailTemplates.Common, EmailTemplateGUIDs.RequestBidEmailIndividualVendor);


                        string messageId = string.IsNullOrEmpty(row["EmailMessageId"].ToString()) ? GetNegotiationEmailUniqueId(negotiationId) : row["EmailMessageId"].ToString();
                        string emailBody = string.Format(objEmailTemplate.EmailBody, row["VendorEmailLink"].ToString(),
                                row["EmailUserName"].ToString(),
                                row["PropertyAddress"].ToString(), messageId);
                        emailBody = GenerateNegotiationVendorEmailBody(result, emailBody);

                        string subject = string.Format(objEmailTemplate.Subject, row["Subject"].ToString());

                        string logoPath = HttpContext.Current.Server.MapPath(VectorConstants.TildeSeparator.ToString());
                        logoPath += objEmailTemplate.EmailImagesList[default(int)].Path;


                        string cc = row["CCEmail"].ToString();

                        //if(string.IsNullOrEmpty(cc))
                        //{
                        //    cc = "Sourcing@vector97.com";
                        //}
                        //else if(!cc.ToUpper().Contains("SOURCING@VECTOR97.COM"))
                        //{
                        //    cc =  cc + ",Sourcing@vector97.com";
                        //}

                        bool emailResult = EmailManager.SendEmailWithMutipleAttachments(row["EmailOne"].ToString(), SecurityManager.GetConfigValue("FromEmailNegotiation"), subject,
                                                                            emailBody, cc, "", null, "", false, logoPath, MessageID: messageId,
                                                                            smtpSection: SecurityManager.GetSmtpMailSection("NegotiaitonMail"));

                        //bool emailResult = EmailManager.SendEmailWithMutipleAttachments(row["EmailOne"].ToString(), SecurityManager.GetConfigValue("FromEmail"), subject,
                        //                                                    emailBody, row["CCEmail"].ToString(), "", null, "", false, logoPath, MessageID: messageId);

                        if (emailResult)
                        {
                            if (string.IsNullOrEmpty(row["EmailMessageId"].ToString()))
                                AddDataToEmailTable(emailData, row["VendorId"].ToString(), messageId);
                            objVendorSuccess.Append(row["VendorName"].ToString() + ",");
                        }
                        else
                            objVendorFail.Append(row["VendorName"].ToString() + ",");
                    }

                    if (!string.IsNullOrEmpty(objVendorSuccess.ToString()))
                    {
                        if (!DataManager.IsNullOrEmptyDataTable(emailData))
                        {
                            emailDataSet.Tables.Add(emailData);
                            UpdateNegotiationEmailDetails(negotiationId,
                                                          emailDataSet,
                                                          emailData);
                        }
                        EmailResult objEmailResult = new EmailResult();
                        objEmailResult.Result = true;
                        objEmailResult.ResultDesc = "Email sent successfully to Vendor(s) " + objVendorSuccess.ToString().TrimEnd(',');
                        list.Add(objEmailResult);
                    }

                    if (!string.IsNullOrEmpty(objVendorFail.ToString()))
                    {
                        EmailResult objEmailResultFail = new EmailResult();
                        objEmailResultFail.Result = false;
                        objEmailResultFail.ResultDesc = "Unable to send an email to Vendor(s) " + objVendorFail.ToString().TrimEnd(',');
                        list.Add(objEmailResultFail);
                    }

                    return new VectorResponse<object>() { ResponseData = list };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> SendRequestBidEmail(SendEmailNegotiation objSendEmail, Int64 userId)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                var result = objNegotiationsDL.GetRequestBidEmails(Convert.ToString(userId), objSendEmail.negotiationId, objSendEmail.vendorId);
                List<EmailResult> list = new List<EmailResult>();
                StringBuilder objVendorSuccess = new StringBuilder();
                StringBuilder objVendorFail = new StringBuilder();
                DataSet emailDataSet = new DataSet();
                DataTable emailData = new DataTable();
                emailData.TableName = "EmailData";
                emailData.Columns.Add(VectorEnums.Column.VendorId.ToString(), typeof(string));
                emailData.Columns.Add(VectorEnums.Column.MessageId.ToString(), typeof(string));
                emailData.Columns.Add(VectorEnums.Column.UIDL.ToString(), typeof(string));
                if (!DataManager.IsNullOrEmptyDataSet(result))
                {
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        string messageId = string.IsNullOrEmpty(row["EmailMessageId"].ToString()) ? GetNegotiationEmailUniqueId(objSendEmail.negotiationId) : row["EmailMessageId"].ToString();
                        string emailBody = objSendEmail.Body;
                        string subject = objSendEmail.Subject;



                        string cc = objSendEmail.CC;

                        //if (string.IsNullOrEmpty(cc))
                        //{
                        //    cc = "Sourcing@vector97.com";
                        //}
                        //else if (!cc.ToUpper().Contains("SOURCING@VECTOR97.COM"))
                        //{
                        //    cc = cc + ",Sourcing@vector97.com";
                        //}


                        bool emailResult = EmailManager.SendEmailWithMutipleAttachments(objSendEmail.To, SecurityManager.GetConfigValue("FromEmailNegotiation"), subject,
                                                                            objSendEmail.Body, cc, objSendEmail.BCC, null, "", false, null, MessageID: messageId,
                                                                            smtpSection: SecurityManager.GetSmtpMailSection("NegotiaitonMail"));

                        //bool emailResult = EmailManager.SendEmailWithMutipleAttachments(row["EmailOne"].ToString(), urityManager.GetConfigValue("FromEmail"), subject,
                        //                                                    emailBody, row["CCEmail"].ToString(), "", null, "", false, logoPath, MessageID: messageId);

                        if (emailResult)
                        {
                            if (string.IsNullOrEmpty(row["EmailMessageId"].ToString()))
                                AddDataToEmailTable(emailData, row["VendorId"].ToString(), messageId);
                            objVendorSuccess.Append(row["VendorName"].ToString() + ",");
                        }
                        else
                            objVendorFail.Append(row["VendorName"].ToString() + ",");
                    }

                    if (!string.IsNullOrEmpty(objVendorSuccess.ToString()))
                    {
                        if (!DataManager.IsNullOrEmptyDataTable(emailData))
                        {
                            emailDataSet.Tables.Add(emailData);
                            UpdateNegotiationEmailDetails(objSendEmail.negotiationId,
                                                          emailDataSet,
                                                          emailData);
                        }
                        EmailResult objEmailResult = new EmailResult();
                        objEmailResult.Result = true;
                        objEmailResult.ResultDesc = "Email sent successfully to Vendor(s) " + objVendorSuccess.ToString().TrimEnd(',');
                        list.Add(objEmailResult);
                    }

                    if (!string.IsNullOrEmpty(objVendorFail.ToString()))
                    {
                        EmailResult objEmailResultFail = new EmailResult();
                        objEmailResultFail.Result = false;
                        objEmailResultFail.ResultDesc = "Unable to send an email to Vendor(s) " + objVendorFail.ToString().TrimEnd(',');
                        list.Add(objEmailResultFail);
                    }

                    return new VectorResponse<object>() { ResponseData = list };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> VectorManagePreBidRequest(DraftNegotiations objDraftNegotiations)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                var result = objNegotiationsDL.VectorManagePreBidRequest(objDraftNegotiations);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    bool emailResult = true;
                    EmailResult objEmailResult = new EmailResult();
                    if ((Convert.ToBoolean(result.Tables[0].Rows[0]["Result"])) && objDraftNegotiations.createDraftNegotaition == "Yes" &&
                        (objDraftNegotiations.NegotiationStatus == "Bill Processing Only" || objDraftNegotiations.NegotiationStatus == "Bill Processing - Audit"))
                    {
                        emailResult = SendPreBidRequestEmail(result.Tables[0]);
                    }
                    if (Convert.ToBoolean(result.Tables[0].Rows[0]["Result"]))
                    {
                        objEmailResult.Result = true;
                        objEmailResult.ResultDesc = emailResult ? "Negotiation Updated Successfully" : "Negotiation Updated Successfully,,Failed to send Email.";
                    }
                    else
                    {
                        objEmailResult.Result = false;
                        objEmailResult.ResultDesc = result.Tables[0].Rows[0]["Result"].ToString();
                    }


                    return new VectorResponse<object>() { ResponseData = objEmailResult };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to submit Negotiation Details." } };
                }
            }
        }
        private bool SendPreBidRequestEmail(DataTable dataTable)
        {
            bool emailResult = false;
            string negotiationNo = dataTable.Rows[0]["NegotiationNo"].ToString();
            string negotiationStatus = dataTable.Rows[0]["NegotiationStatus"].ToString();
            string propertyName = dataTable.Rows[0]["PropertyName"].ToString();
            string propertyAddress = dataTable.Rows[0]["PropertyAddress"].ToString();
            if (!string.IsNullOrEmpty(negotiationNo))
            {
                EmailTemplate objEmailTemplate = EmailTemplateManager.GetEmailTemplate(AppDomain.CurrentDomain.BaseDirectory, EmailTemplates.Negotiations, EmailTemplateGUIDs.NegotiationPreBidRequestEmail);

                string emailBody = string.Format(objEmailTemplate.EmailBody, negotiationNo, propertyName, propertyAddress, negotiationStatus);

                string subject = string.Format(objEmailTemplate.Subject, propertyName, negotiationNo);

                string logoPath = HttpContext.Current.Server.MapPath(VectorConstants.TildeSeparator.ToString());
                logoPath += objEmailTemplate.EmailImagesList[default(int)].Path;

                string cc = SecurityManager.GetConfigValue("NegotiationPreBidRequestCC");

                //if (string.IsNullOrEmpty(cc))
                //{
                //    cc = "Sourcing@vector97.com";
                //}
                //else if (!cc.ToUpper().Contains("SOURCING@VECTOR97.COM"))
                //{
                //    cc = cc + ",Sourcing@vector97.com";
                //}

                emailResult = EmailManager.SendEmailWithMutipleAttachments(SecurityManager.GetConfigValue("NegotiationPreBidRequestTo"),
                                                                            SecurityManager.GetConfigValue("FromEmailNegotiation"), subject,
                                                                                    emailBody, cc,
                                                                                    "", null, "", false, logoPath,
                                                                                    smtpSection: SecurityManager.GetSmtpMailSection("NegotiaitonMail"));
            }
            return emailResult;
        }

        public string GetNegotiationEmailUniqueId(string negotiationId)
        {
            string MessageID = "";
            Guid objGuid = new Guid();
            objGuid = Guid.NewGuid();
            MessageID = string.Format("<{0}~{1}>", negotiationId, objGuid.ToString());

            return MessageID;
        }

        public VectorResponse<object> GetNegotiatonRequestBidMails(string negotiationId)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                var result = objNegotiationsDL.GetNegotiatonRequestBidMails(negotiationId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    var emailresult = GetEmailDetailsByNegotiaion(result.Tables[0], SecurityManager.GetConfigValue("EmailServer"),
                                                            SecurityManager.GetConfigValue("EmailId"),
                                                            SecurityManager.GetConfigValue("Password"), SecurityManager.GetConfigValue("ServerProtocol"));

                    //emailresult.MailDetailsList.
                    if (emailresult.MailDetailsList.Count != 0)
                    {
                        return new VectorResponse<object>() { ResponseData = emailresult };
                    }
                    else
                        return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> GetNegotiatonSendEmail(NegotiationSendEmail objSendEmail)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {

                EmailTemplate objEmailTemplate = EmailTemplateManager.GetEmailTemplate(AppDomain.CurrentDomain.BaseDirectory, EmailTemplates.Common, EmailTemplateGUIDs.CommonEmail);

                string emailBody = string.Format(objEmailTemplate.EmailBody, objSendEmail.Body, SecurityManager.GetConfigValue("VectorURL"), SecurityManager.GetConfigValue("VectorWebSite"));

                string subject = string.Format(objEmailTemplate.Subject, objSendEmail.Subject);

                string logoPath = HttpContext.Current.Server.MapPath(VectorConstants.TildeSeparator.ToString());
                logoPath += objEmailTemplate.EmailImagesList[default(int)].Path;

                //bool emailResult = EmailManager.SendEmailWithMutipleAttachments(objSendEmail.To, SecurityManager.GetConfigValue("FromEmail"), subject,
                //                                                    emailBody, objSendEmail.CC, objSendEmail.BCC, objSendEmail.EmailAttachments,
                //                                                    objSendEmail.AttachmentsFolderPath, objSendEmail.IsTempFolder, logoPath,
                //                                                    smtpSection: SecurityManager.GetSmtpMailSection("NegotiaitonMail"));


                string cc = objSendEmail.CC;

                //if (string.IsNullOrEmpty(cc))
                //{
                //    cc = "Sourcing@vector97.com";
                //}
                //else if (!cc.ToUpper().Contains("SOURCING@VECTOR97.COM"))
                //{
                //    cc = cc + ",Sourcing@vector97.com";
                //}

                bool emailResult = EmailManager.SendEmailWithMutipleAttachments(objSendEmail.To,
                    SecurityManager.GetConfigValue("FromEmailNegotiation"), subject,
                                                                    emailBody, cc, objSendEmail.BCC, objSendEmail.EmailAttachments,
                                                                    objSendEmail.AttachmentsFolderPath, objSendEmail.IsTempFolder, logoPath,
                                                                    smtpSection: SecurityManager.GetSmtpMailSection("NegotiaitonMail"));

                if (emailResult)
                {
                    List<string> emailUniqueIds = new List<string>() { objSendEmail.Uidl };
                    int emailresult = MarkMailAsReadFromES(SecurityManager.GetConfigValue(VectorEnums.ConfigValue.EmailServer.ToString()),
                                                            SecurityManager.GetConfigValue(VectorEnums.ConfigValue.EmailId.ToString()),
                                                            SecurityManager.GetConfigValue(VectorEnums.ConfigValue.Password.ToString()),
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

        public VectorResponse<object> DownloadNegotiationEmailAttachments(string uniqueId, string tempFilesFolderName)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                var emailresult = DownloadAttachmentsAsZip(SecurityManager.GetConfigValue(VectorEnums.ConfigValue.EmailServer.ToString()),
                                                            SecurityManager.GetConfigValue(VectorEnums.ConfigValue.EmailId.ToString()),
                                                            SecurityManager.GetConfigValue(VectorEnums.ConfigValue.Password.ToString()),
                                                            SecurityManager.GetConfigValue(VectorEnums.ConfigValue.ServerProtocol.ToString()), uniqueId, tempFilesFolderName);
                string fileName = Path.GetFileName(emailresult);
                if (!string.IsNullOrEmpty(fileName))
                {
                    return new VectorResponse<object>() { ResponseData = fileName };
                }
                else
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Files not available to download." } };
            }
        }

        public VectorResponse<object> SendNegotiationRevisitEmail()
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                var result = objNegotiationsDL.GetNegotiationRevisitEmail();
                List<EmailResult> list = new List<EmailResult>();
                StringBuilder objNegotiationSuccess = new StringBuilder();
                StringBuilder objNegotiationFail = new StringBuilder();
                if (!DataManager.IsNullOrEmptyDataSet(result))
                {
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        EmailTemplate objEmailTemplate = EmailTemplateManager.GetEmailTemplate(AppDomain.CurrentDomain.BaseDirectory, EmailTemplates.Negotiations,
                                                            EmailTemplateGUIDs.NegotiationRevisitEmail);

                        string emailBody = string.Format(objEmailTemplate.EmailBody, row[VectorEnums.Column.NegotiationNo.ToString()].ToString(),
                                row["NegotiationStatus"].ToString(), row["RevisitReminderDate"].ToString(),
                                SecurityManager.GetConfigValue("VectorURL"), SecurityManager.GetConfigValue("VectorWebSite"));

                        string subject = string.Format(objEmailTemplate.Subject);

                        string logoPath = HttpContext.Current.Server.MapPath(VectorConstants.TildeSeparator.ToString());
                        logoPath += objEmailTemplate.EmailImagesList[default(int)].Path;

                        string negotiator = row["Negotiator"].ToString();

                        bool emailResult = false;


                        string cc = null;

                        //if (string.IsNullOrEmpty(cc))
                        //{
                        //    cc = "Sourcing@vector97.com";
                        //}
                        //else if (!cc.ToUpper().Contains("SOURCING@VECTOR97.COM"))
                        //{
                        //    cc = cc + ",Sourcing@vector97.com";
                        //}



                        if (!string.IsNullOrEmpty(negotiator))
                            emailResult = EmailManager.SendEmailWithMutipleAttachments(negotiator, SecurityManager.GetConfigValue("FromEmailNegotiation"), subject,
                                                                                emailBody, cc, "", null, "", false, logoPath,
                                                                                smtpSection: SecurityManager.GetSmtpMailSection("NegotiaitonMail"));
                        if (emailResult)
                        {
                            objNegotiationSuccess.Append(row["NegotiationNo"].ToString() + ",");
                        }
                        else
                            objNegotiationFail.Append(row["NegotiationNo"].ToString() + ",");
                    }
                    EmailResult objEmailResult = new EmailResult();
                    if (!string.IsNullOrEmpty(objNegotiationSuccess.ToString()))
                    {
                        objEmailResult.Result = true;
                        objEmailResult.ResultDesc = "Email sent successfully to Negotiation(s) " + objNegotiationSuccess.ToString().TrimEnd(',');
                    }

                    if (!string.IsNullOrEmpty(objNegotiationFail.ToString()))
                    {
                        objEmailResult.Result = false;
                        objEmailResult.ResultDesc = "Unable to send an email to Negotiation(s) " + objNegotiationFail.ToString().TrimEnd(',');
                    }

                    return new VectorResponse<object>() { ResponseData = objEmailResult };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> VectorServiceManager()
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                var result = objNegotiationsDL.ManageVectorServiceDetails(VectorEnums.StoredProcedures.VectorGetServiceManagerDetails.ToString());
                List<EmailResult> list = new List<EmailResult>();
                StringBuilder objVectorServiceSuccess = new StringBuilder();
                StringBuilder objVectorServiceFail = new StringBuilder();
                if (!DataManager.IsNullOrEmptyDataSet(result))
                {
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        string storedProcedure = row["StoredProcedure"].ToString();

                        var serviceResult = objNegotiationsDL.ManageVectorServiceDetails(storedProcedure);

                        if (!DataManager.IsNullOrEmptyDataSet(serviceResult))
                        {
                            bool serviceSuccess = Convert.ToBoolean(serviceResult.Tables[0].Rows[0]["Result"].ToString());

                            if (serviceSuccess)
                                objVectorServiceSuccess.Append(row["ServiceName"].ToString() + ",");
                            else
                                objVectorServiceFail.Append(row["ServiceName"].ToString() + ",");
                        }
                        else
                            objVectorServiceFail.Append(row["ServiceName"].ToString() + ",");
                    }

                    EmailResult objEmailResult = new EmailResult();
                    if (!string.IsNullOrEmpty(objVectorServiceSuccess.ToString()))
                    {
                        objEmailResult.Result = true;
                        objEmailResult.ResultDesc = "Service(s) " + objVectorServiceSuccess.ToString().TrimEnd(',') + " run successfully";
                    }

                    if (!string.IsNullOrEmpty(objVectorServiceFail.ToString()))
                    {
                        objEmailResult.Result = false;
                        objEmailResult.ResultDesc = "Failed to run Service(s) " + objVectorServiceFail.ToString().TrimEnd(',');
                    }

                    return new VectorResponse<object>() { ResponseData = objEmailResult };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Service not avilable to run" } };
                }
            }
        }



        public VectorResponse<object> GetNegotiationBaselineLineitems(string type, Int64 negotiationId, Int64 userId)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                var result = objNegotiationsDL.GetNegotiationBaselineLineitems(type, negotiationId, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to submit Negotiation Details." } };
                }
            }
        }


        public VectorResponse<object> GetNegotiationVendorEmailDetails(SendEmailNegotiation objSendEmailNegotiation, Int64 userId)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                var result = objNegotiationsDL.GetRequestBidEmails(Convert.ToString(userId), objSendEmailNegotiation.negotiationId, objSendEmailNegotiation.vendorId);

                using (EmailDetails objEmailDetails = new EmailDetails())
                {

                    EmailTemplate objEmailTemplate = EmailTemplateManager.GetEmailTemplate(AppDomain.CurrentDomain.BaseDirectory, EmailTemplates.Common, EmailTemplateGUIDs.RequestBidEmailIndividualVendor);

                    var vendorEmailInfo = (from c in result.Tables[0].AsEnumerable()
                                           select new
                                           {
                                               EmailMessageId = (string.IsNullOrEmpty(c.Field<string>("EmailMessageId")) ? GetNegotiationEmailUniqueId(objSendEmailNegotiation.negotiationId)
                                                                    : c.Field<string>("EmailMessageId")),
                                               VendorEmailLink = c.Field<string>("VendorEmailLink"),
                                               EmailUserName = c.Field<string>("EmailUserName"),
                                               PropertyAddress = c.Field<string>("PropertyAddress"),
                                               Subject = c.Field<string>("Subject"),
                                               EmailOne = c.Field<string>("EmailOne")
                                           }).FirstOrDefault();


                    string emailBody = string.Format(objEmailTemplate.EmailBody, vendorEmailInfo.VendorEmailLink,
                            vendorEmailInfo.EmailUserName, vendorEmailInfo.PropertyAddress, vendorEmailInfo.EmailMessageId);
                    emailBody = GenerateNegotiationVendorEmailBody(result, emailBody);

                    string subject = string.Format(objEmailTemplate.Subject, vendorEmailInfo.Subject);

                    string logoPath = HttpContext.Current.Server.MapPath(VectorConstants.TildeSeparator.ToString());
                    logoPath += objEmailTemplate.EmailImagesList[default(int)].Path;

                    objEmailDetails.Signature = objEmailTemplate.Signature;

                    objEmailDetails.Body = emailBody;
                    objEmailDetails.Subject = subject;
                    objEmailDetails.To = vendorEmailInfo.EmailOne;

                    objEmailDetails.AttachmentsFolderPath = null;

                    string contractFileName = string.Empty;

                    return new VectorResponse<object>() { ResponseData = objEmailDetails };
                }
            }
        }

        private string GenerateNegotiationVendorEmailBody(DataSet vendorEmailData, string mailBody)
        {
            string PropertyDetails = Convert.ToString(vendorEmailData.Tables[0].Rows[0]["PropertyAddress"]);
            string PropertyCityStateZip = Convert.ToString(vendorEmailData.Tables[0].Rows[0]["PropertyCityStateZip"]);
            if (!string.IsNullOrEmpty(PropertyCityStateZip.TrimEnd().TrimEnd(',')))
            {
                PropertyDetails += ", " + PropertyCityStateZip;
            }

            mailBody = mailBody.Replace("@PropertyName@", Convert.ToString(vendorEmailData.Tables[0].Rows[0]["PropertyName"]));
            mailBody = mailBody.Replace("@ServiceAddress@", PropertyDetails);
            //mailBody = mailBody.Replace("@HaulerDetails@", Convert.ToString(vendorEmailData.Tables[0].Rows[0]["VendorName"]) + "-" +
            //                                               Convert.ToString(vendorEmailData.Tables[0].Rows[0]["VendorShortName"]));
            //mailBody = mailBody.Replace("@HaulerContact@", Convert.ToString(vendorEmailData.Tables[0].Rows[0]["VendorPhone"]));
            if (!DataManager.IsNullOrEmptyDataTable(vendorEmailData.Tables[1]))
                mailBody = mailBody.Replace("@ServiceLevelItems@", ConvertDataTabletoHTML(vendorEmailData.Tables[1]));
            else
                mailBody = mailBody.Replace("@ServiceLevelItems@", string.Empty);

            if (!DataManager.IsNullOrEmptyDataTable(vendorEmailData.Tables[2]))
            {
                StringBuilder exemptedList = new StringBuilder("<p>Excluded items:</p><p><ul>");
                foreach (DataRow dr in vendorEmailData.Tables[2].Rows)
                {
                    exemptedList.Append("<li>");
                    exemptedList.Append(Convert.ToString(dr["ServiceName"]));
                    exemptedList.Append("</li>");
                }
                exemptedList.Append("</p></ul>");
                mailBody = mailBody.Replace("@ExcludedItemsList@", exemptedList.ToString());
            }
            else
                mailBody = mailBody.Replace("@ExcludedItemsList@", string.Empty);

            return mailBody;
        }

        public static string ConvertDataTabletoHTML(DataTable data)
        { 
            if (data.Rows.Count == 0) return ""; // enter code here

            StringBuilder builder = new StringBuilder();
            builder.Append("<table cellspacing='0' rules='all' border='1' ");
            builder.Append(">");
            builder.Append("<tbody><tr>");
            foreach (DataColumn c in data.Columns)
            {
                if(c.ColumnName == "Service Description")
                { 
                    builder.Append("<td style='padding: 3pt 8pt 3pt 3pt'><b>");
                    builder.Append(c.ColumnName);
                    builder.Append("</b></td>");
                }
                else
                {

                    builder.Append("<td style='padding: 3pt 8pt 3pt 3pt'><b>");
                    builder.Append(c.ColumnName);
                    builder.Append("</b></td>");
                }
            }
            builder.Append("</tr>");
            foreach (DataRow r in data.Rows)
            {
                builder.Append("<tr>");
                foreach (DataColumn c in data.Columns)
                {
                    if (c.ColumnName == "Service Description")
                    {
                        builder.Append("<td  style='padding: 3pt 8pt 3pt 3pt'>");
                        builder.Append(r[c.ColumnName]);
                        builder.Append("</td>");
                    }
                    else
                    {
                        builder.Append("<td  style='padding: 3pt 8pt 3pt 3pt'>");
                        builder.Append(r[c.ColumnName]);
                        builder.Append("</td>");
                    }
                }
                builder.Append("</tr>");
            }
            builder.Append("</tbody></table>");

            return builder.ToString();
        }

        public VectorResponse<object> CalculateBeginAndEndDates(DateEntity objDateEntity)
        {
            DateTime? BeginDate = null;
            DateTime? EndDate = null;
            int? intTerm = null;

            DataSet result = new DataSet();
            DataTable dtData = new DataTable();
            dtData.Columns.Add("Result", typeof(int));
            dtData.Columns.Add("BeginDate", typeof(String));
            dtData.Columns.Add("EndDate", typeof(string));
            dtData.Columns.Add("TermMonths", typeof(int));

            DataRow drRow = dtData.NewRow();
            if (objDateEntity != null)
            {
                try
                {
                    drRow[0] = 1;

                    if (objDateEntity.FromDate != null)
                    {
                        BeginDate = Convert.ToDateTime(objDateEntity.FromDate);
                    }

                    if (objDateEntity.TermMonths != null && objDateEntity.TermMonths != null && BeginDate != null)
                    {
                        DateTime dtFromDate = Convert.ToDateTime(BeginDate);
                        EndDate = dtFromDate.AddMonths(Convert.ToInt32(objDateEntity.TermMonths));
                        intTerm = Convert.ToInt32(objDateEntity.TermMonths);
                    }

                    if (objDateEntity.TermMonths == null && objDateEntity.TermMonths != null && BeginDate != null && EndDate != null)
                    {
                        EndDate = Convert.ToDateTime(objDateEntity.ToDate);
                        DateTime dtFromDate = Convert.ToDateTime(BeginDate);
                        DateTime dtToDate = Convert.ToDateTime(objDateEntity.ToDate);
                        intTerm = ((dtFromDate.Year - dtToDate.Year) * 12) + dtFromDate.Month - dtToDate.Month;
                    }

                    if (BeginDate != null)
                    {
                        var dateField = Convert.ToDateTime(BeginDate);

                        drRow[1] = Convert.ToString(dateField.Month) + '/' + Convert.ToString(dateField.Day) + '/' + Convert.ToString(dateField.Year);
                    }

                    if (EndDate != null)
                    {
                        var dateField = Convert.ToDateTime(EndDate).AddDays(-1);

                        drRow[2] = Convert.ToString(dateField.Month) + '/' + Convert.ToString(dateField.Day) + '/' + Convert.ToString(dateField.Year);
                    }

                    if (intTerm != null)
                        drRow[3] = Convert.ToString(intTerm);

                    dtData.Rows.Add(drRow);
                    result.Tables.Add(dtData);

                    return new VectorResponse<object>() { ResponseData = result };
                }
                catch
                {

                    drRow[0] = 1;
                    drRow[1] = objDateEntity.FromDate;


                    if (objDateEntity.ToDate != null)
                        drRow[2] = objDateEntity.ToDate;

                    if (objDateEntity.TermMonths != null)
                        drRow[3] = objDateEntity.TermMonths;

                    dtData.Rows.Add(drRow);
                    result.Tables.Add(dtData);
                    return new VectorResponse<object>() { ResponseData = result };
                }
            }
            else
            {
                drRow[0] = 1;
                drRow[1] = objDateEntity.FromDate;
                if (objDateEntity.ToDate != null)
                    drRow[2] = objDateEntity.ToDate;

                if (objDateEntity.TermMonths != null)
                    drRow[3] = objDateEntity.TermMonths;

                dtData.Rows.Add(drRow);
                result.Tables.Add(dtData);

                return new VectorResponse<object>() { ResponseData = result };
            }
        }

        public VectorResponse<object> ManageNegotiationDocuments(NegotiationDocuments onjNegotiationDocuments,Int64 userId)
        {
            bool isFilesMovedToFolder = false;
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                //DataSet dsNegotiationInfo =  
                string FileServerPath = SecurityManager.GetConfigValue("FileServerPath")  + "Negotiations";
                string FileServerPathBackUp = SecurityManager.GetConfigValue("FileServerBackUpPath") + "Negotiations";

                try
                {
                    if (!System.IO.Directory.Exists(FileServerPath + "//" + onjNegotiationDocuments.NegotiationNo))
                    {
                        System.IO.Directory.CreateDirectory(FileServerPath + "//" + onjNegotiationDocuments.NegotiationNo);
                    }

                    foreach (var objDocs in onjNegotiationDocuments.documentInfo)
                    {
                        string TempFolderPath = SecurityManager.GetConfigValue("FileServerTempPath") + objDocs.TempFolderName + "\\";

                        if (System.IO.Directory.Exists(TempFolderPath))
                        {
                            if (System.IO.File.Exists(TempFolderPath + objDocs.DocumentName))
                            {
                                if (System.IO.Directory.Exists(FileServerPath + "//" + onjNegotiationDocuments.NegotiationNo))
                                {
                                    if (!System.IO.File.Exists(FileServerPath + "//" + onjNegotiationDocuments.NegotiationNo + "//" + objDocs.DocumentName))
                                    {
                                        System.IO.File.Move(TempFolderPath + objDocs.DocumentName, FileServerPath + "//" + onjNegotiationDocuments.NegotiationNo + "//" + objDocs.DocumentName);
                                        
                                    }
                                    else
                                    {
                                        if (!System.IO.Directory.Exists(FileServerPathBackUp + "//" + onjNegotiationDocuments.NegotiationNo))
                                        {
                                            System.IO.Directory.CreateDirectory(FileServerPathBackUp + "//" + onjNegotiationDocuments.NegotiationNo);
                                        }

                                        System.IO.File.Replace(TempFolderPath + objDocs.DocumentName,
                                                                FileServerPath + "//" + onjNegotiationDocuments.NegotiationNo + "//" + objDocs.DocumentName,
                                                                FileServerPathBackUp + "//" + onjNegotiationDocuments.NegotiationNo + "//" + objDocs.DocumentName);
                                    }

                                    isFilesMovedToFolder = true;
                                }
                                else
                                {
                                    isFilesMovedToFolder = false;
                                    break;
                                }


                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    isFilesMovedToFolder = false;
                }

                if (isFilesMovedToFolder)
                {
                    var result = objNegotiationsDL.ManageNegotiationDocuments(onjNegotiationDocuments, userId);
                    if (DataValidationLayer.isDataSetNotNull(result))
                    {
                        return new VectorResponse<object>() { ResponseData = result };
                    }
                    else
                    {
                        return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to Upload Documents" } };
                    }
                }
                else
                { 
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to Upload Documents,Please try again later." } };
                }
            }
        }



        public VectorResponse<object> GetNegotiationDocuments(Int64 negotiationId,Int64 userId)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {

                var result = objNegotiationsDL.GetNegotiationDocuments(negotiationId, userId);
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

        public VectorResponse<object> UpdateNegotiationLineitem(NegotiationLineitemUpdate ObjNegotiationLineitem, Int64 userId)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {

                var result = objNegotiationsDL.UpdateNegotiationLineitem(ObjNegotiationLineitem, userId);
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
        public VectorResponse<object> GetNegotiation360ReportStatusInfo(NegotiationSearch objNegotiationSearch, Int64 userId)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                objNegotiationSearch.UserId = userId;
                var result = objNegotiationsDL.GetNegotiation360ReportStatusInfo(objNegotiationSearch);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }

                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to submit Negotiation Details." } };
                }
            }
        }

        public VectorResponse<object> ManageBidEmailService(BidServiceInfo objBidServiceInfo, Int64 userId)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            { 
                var result = objNegotiationsDL.ManageBidEmailService(objBidServiceInfo, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }

                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to submit Negotiation Details." } };
                }
            }
        }



        public VectorResponse<object> ManageNegotiaionLineitemState(string action, Int64 baselineLineitemId, Int64 negotiaitonId,Int64 negotiationLineitemId, Int64 accountId, Int64 accountlineitemid, Int64 userId)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                var result = objNegotiationsDL.ManageNegotiaionLineitemState(action, baselineLineitemId, negotiaitonId, negotiationLineitemId, accountId, accountlineitemid, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }

                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to submit Negotiation Details." } };
                }
            }
        }

        public VectorResponse<object> GetNegotiationVendorLineitems(string action, Int64 negotiaitonId, Int64 vendorId, Int64 userId)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                var result = objNegotiationsDL.GetNegotiationVendorLineitems(action, negotiaitonId, vendorId, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }

                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to submit Negotiation Details." } };
                }
            }
        }


        public VectorResponse<object> GetLowestBidInfo(string action, Int64 negotiationLineitemId,Int64 userId)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                var result = objNegotiationsDL.GetLowestBidInfo(action, negotiationLineitemId, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }

                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Found." } };
                }
            }
        }

        public VectorResponse<object> GetAwardedVendors(string action, Int64 Id,Int64 userId, Int16 fromRange, Int16 toRange)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {

                var result = objNegotiationsDL.GetAwardedVendors(action,Id, userId, fromRange, toRange);
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


        public VectorResponse<object> NegotiationCloneLineItems(CloneLineitems objCloneLineitems, Int64 userId)
        {
            using (var objNegotiationsDL = new NegotiationsDL(objVectorDB))
            {
                var result = objNegotiationsDL.NegotiationCloneLineItems(objCloneLineitems, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }

                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to Clone Lineitems, Try again Later." } };
                }
            }
        }

    }


}
