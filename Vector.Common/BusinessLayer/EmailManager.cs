

namespace Vector.Common.BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Configuration;
    using System.Net.Mail;
    using System.Net.Mime;
    using System.Reflection;
    using System.Web;
    using Vector.Common.DataLayer;
    using Vector.Common.Entities;
    using static Vector.Common.BusinessLayer.EmailTemplateEnums;

    public class EmailManager : DisposeLogic
    {
        #region Constants

        private const string DubleSlash = "\\";
        private string attachFileName;
        private const string imageType = "image/png";
        private const string companylogo = "companylogo";

        private const string ContentDisposition = "Content-Disposition";
        private const string attachment_filename = "attachment; filename=\"{0}\"";
        private const string ContentLength = "Content-Length";
        private const string ContentType = "message/rfc822";

        private const string EmailTo = "EmailTo";
        private const string EmailCC = "EmailCC";
        private const string EmailBCC = "EmailBCC";

        private const string ESEmailTo = "ESEmailTo";
        private const string ESEmailCC = "ESEmailCC";
        private const string ESEmailBCC = "ESEmailBCC";
        private const string ESEmailFrom = "ESEmailFrom";
        private const string ValidationMailSubject = "Energy Star :Error Notification(Meter Reading for Energy Star)";

        private const string ServiceSubject = "ES Service Error Notification";
        //private const string LogoPath = "/Images/proutility_demologo.png";
        private const string FileDirectory = "FileDirectory";
        private const string QueryKeyFormat = "(#{0}#)";


        #endregion

        #region Methods

        public static bool SendEmail(SendEmail objSendEmail,SmtpSection smtpSection = null,string fromEmail="")
        {
            EmailTemplate objEmailTemplate = EmailTemplateManager.GetEmailTemplate(AppDomain.CurrentDomain.BaseDirectory, EmailTemplates.Common, EmailTemplateGUIDs.CommonEmail);

            string emailBody = string.Format(objEmailTemplate.EmailBody, objSendEmail.Body, SecurityManager.GetConfigValue("VectorURL"), SecurityManager.GetConfigValue("VectorWebSite"));

            string subject = string.Format(objEmailTemplate.Subject, objSendEmail.Subject);

            string logoPath = HttpContext.Current.Server.MapPath(VectorConstants.TildeSeparator.ToString());
            logoPath += objEmailTemplate.EmailImagesList[default(int)].Path;

            if (!string.IsNullOrEmpty(objSendEmail.FromEmail) && (objSendEmail.FromEmail.ToUpper() == "CONTRACTS"))
            {
                fromEmail = SecurityManager.GetConfigValue("FromEmailContract");
                smtpSection = SecurityManager.GetSmtpMailSection("ContractsMail");
            }
            else if (!string.IsNullOrEmpty(objSendEmail.FromEmail) && (objSendEmail.FromEmail.ToUpper() == "NEGOTIATIONS"))
            {
                fromEmail = SecurityManager.GetConfigValue("FromEmailNegotiation");
                smtpSection = SecurityManager.GetSmtpMailSection("NegotiaitonMail");
            }
            else if (!string.IsNullOrEmpty(objSendEmail.FromEmail) && (objSendEmail.FromEmail.ToUpper() == "INVOICE"))
            { 
                fromEmail = SecurityManager.GetConfigValue("FromEmailInvoice");
                smtpSection = SecurityManager.GetSmtpMailSection("InvoiceMail");
            }
            else if (!string.IsNullOrEmpty(objSendEmail.FromEmail) && (objSendEmail.FromEmail.ToUpper() == "SHORTPAY"))
            {
                fromEmail = SecurityManager.GetConfigValue("FromEmailShortPay");
                smtpSection = SecurityManager.GetSmtpMailSection("ShortPayMail");
            }
            else if (string.IsNullOrEmpty(fromEmail))
            {
                fromEmail = SecurityManager.GetConfigValue("FromEmail");
                smtpSection = SecurityManager.GetSmtpMailSection("DefaultMail");
            } 

            return EmailManager.SendEmailWithMutipleAttachments(objSendEmail.To, fromEmail, subject,
                                                                emailBody, objSendEmail.CC, objSendEmail.BCC, objSendEmail.EmailAttachments,
                                                                objSendEmail.AttachmentsFolderPath, objSendEmail.IsTempFolder, logoPath,
                                                                smtpSection: smtpSection);
        }

        /// <summary>
        /// send email with basic functionality(Attach Logo) linked Resource
        /// </summary>
        /// <param name="toEmailList"></param>
        /// <param name="fromEmail"></param>
        /// <param name="subject"></param>
        /// <param name="emailBody"></param>
        /// <param name="cc"></param>
        /// <param name="bcc"></param>
        /// <param name="attachementPath">Complete Path (E://folder/ExcelFile.xlsx)</param>
        /// <param name="logoPath">Complete Path (E://folder/Image.png)</param>
        public static bool SendEmail(string toEmailList, string fromEmail, string subject, string emailBody, string cc = "", string bcc = "",
                                        string attachementPath = "", string logoPath = "", SmtpSection smtpSection = null)
        {
            try
            {
                using (MailMessage objMailMsg = new MailMessage())
                {
                    //Check if to Email List Exists
                    if (!string.IsNullOrEmpty(toEmailList))
                        objMailMsg.To.Add(toEmailList);

                    objMailMsg.Subject = subject;

                    //Check if From email Exists
                    if (!string.IsNullOrEmpty(fromEmail))
                    {
                        MailAddress objFromAddress = new MailAddress(fromEmail);
                        objMailMsg.From = objFromAddress;
                    }

                    //Check if CC email Exists
                    if (!string.IsNullOrEmpty(cc))
                    {
                        AddEmailsList(cc, objMailMsg, "CC");
                    }

                    //Check if BCC email Exists
                    if (!string.IsNullOrEmpty(bcc))
                        AddEmailsList(bcc, objMailMsg, "Bcc");

                    //Check if attachments path exists
                    if (!string.IsNullOrEmpty(attachementPath))
                    {
                        if (File.Exists(attachementPath))
                        {
                            Attachment attachment = new Attachment(attachementPath);
                            objMailMsg.Attachments.Add(attachment);
                        }
                    }

                    AlternateView aEmailView = AlternateView.CreateAlternateViewFromString(emailBody, null, System.Net.Mime.MediaTypeNames.Text.Html);
                    if (!string.IsNullOrEmpty(logoPath))
                    {
                        LinkedResource logo = GetCompanyLogoForEmailBody(logoPath);
                        aEmailView.LinkedResources.Add(logo);
                    }

                    objMailMsg.AlternateViews.Add(aEmailView);
                    objMailMsg.IsBodyHtml = true;

                    using (SmtpClient mailClient = new SmtpClient())
                    {
                        if (smtpSection != null)
                        {
                            MailAddress objFromAddress = new MailAddress(smtpSection.Network.UserName);
                            objMailMsg.From = objFromAddress;

                            SMTPNetwork(smtpSection, mailClient);
                        }
                        else
                        {
                            smtpSection = SecurityManager.GetSmtpMailSection();
                            SMTPNetwork(smtpSection, mailClient);
                        }
                        mailClient.Send(objMailMsg);
                    }
                }
            }
            catch (SmtpException smtpException)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private static void SMTPNetwork(SmtpSection smtpSection, SmtpClient mailClient)
        {
            if (smtpSection.Network != null)
            {
                mailClient.Host = smtpSection.Network.Host;
                mailClient.Port = smtpSection.Network.Port;
                mailClient.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                mailClient.Credentials = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                mailClient.EnableSsl = smtpSection.Network.EnableSsl;
                
                mailClient.Timeout = 600000;
            }
        }

        public static bool SendEmailWithMutipleAttachments(string toEmailList, string fromEmail, string subject, string emailBody, string cc = "", string bcc = "",
                                                        List<Files> emailAttachments = null, string attachmentsPath = "", bool isTempFolder = false,
                                                        string logoPath = "", SmtpSection smtpSection = null,string MessageID="")
        {
            try
            {
                using (MailMessage objMailMsg = new MailMessage())
                {
                    MailAddress objFromAddress = new MailAddress(fromEmail);
                    objMailMsg.From = objFromAddress;

                    objMailMsg.To.Add(toEmailList);

                    //Check if CC email Exists
                    if (!string.IsNullOrEmpty(cc))
                    {
                        EmailManager.AddEmailsList(cc, objMailMsg, "CC");
                    }

                    //Check if BCC email Exists
                    if (!string.IsNullOrEmpty(bcc))
                        EmailManager.AddEmailsList(bcc, objMailMsg, "Bcc");

                    objMailMsg.Subject = subject;

                    //Check if attachments path exists
                    if (emailAttachments != null)
                    {
                        if (isTempFolder)
                            attachmentsPath = SecurityManager.GetConfigValue("FileServerTempPath") + attachmentsPath + "\\";
                        else
                            attachmentsPath = SecurityManager.GetConfigValue("FileServerPath") + attachmentsPath + "\\";

                        foreach (Files attachement in emailAttachments)
                        {
                            if (attachement.isFilePathUrl)
                            {
                                if (File.Exists(attachmentsPath + attachement.filePath))
                                {
                                    Attachment attachment = new Attachment(attachmentsPath + attachement.filePath);
                                    attachment.Name = attachement.fileName;
                                    objMailMsg.Attachments.Add(attachment);
                                }
                            }
                            else if (attachement.isLocal)
                            {
                                if (File.Exists(attachement.filePath))
                                {
                                    Attachment attachment = new Attachment(attachement.filePath);
                                    attachment.Name = attachement.fileName;
                                    objMailMsg.Attachments.Add(attachment);
                                }
                                else  if (File.Exists(attachmentsPath + attachement.fileName))
                                {
                                    Attachment attachment = new Attachment(attachmentsPath + attachement.fileName);
                                    objMailMsg.Attachments.Add(attachment);
                                }
                            }
                            else
                            {

                                if (File.Exists(attachmentsPath + attachement.fileName))
                                {
                                    Attachment attachment = new Attachment(attachmentsPath + attachement.fileName);
                                    objMailMsg.Attachments.Add(attachment);
                                 }
                             }
                        }
                    }

                    AlternateView aEmailView = AlternateView.CreateAlternateViewFromString(emailBody, null, System.Net.Mime.MediaTypeNames.Text.Html);
                    if (!string.IsNullOrEmpty(logoPath))
                    {
                        LinkedResource logo = GetCompanyLogoForEmailBody(logoPath);
                        aEmailView.LinkedResources.Add(logo);
                    }

                    //Guid objGuid = new Guid();
                    //objGuid = Guid.NewGuid();
                    //String MessageID = "<000~" + objGuid.ToString() + ">";
                    if(!string.IsNullOrEmpty(MessageID))
                        objMailMsg.Headers.Add("Message-Id", MessageID);

                    objMailMsg.AlternateViews.Add(aEmailView);
                    
                    objMailMsg.IsBodyHtml = true;
                    using (SmtpClient mailClient = new SmtpClient())
                    {

                       // SendEmailOutllok(objMailMsg, mailClient, smtpSection);

                        if (smtpSection != null)
                        {
                            SMTPNetwork(smtpSection, mailClient);

                        }
                        else
                        {
                            smtpSection = SecurityManager.GetSmtpMailSection();
                            SMTPNetwork(smtpSection, mailClient);
                        }
                        mailClient.Send(objMailMsg);
                    }
                }
            }
            catch (SmtpException smtpException)
            {
                VectorTextLogger.LogErrortoFile(VectorTextLogger.GetExceptionDetails(1, smtpException)
                , Convert.ToString("Vector"), "Email", "");
                return false;
            }
            catch (Exception ex)
            {
                VectorTextLogger.LogErrortoFile(VectorTextLogger.GetExceptionDetails(1, ex)
              , Convert.ToString("Vector"), "Email", "");
                return false;
            }
            return true;
        }


        //private static void SendEmailOutllok(MailMessage objMailMsg,SmtpClient mailClient, SmtpSection smtpSection = null)
        //{
        //    if (smtpSection != null)
        //    {
        //        SMTPNetwork(smtpSection, mailClient);
        //    }
        //    else
        //    {
        //        smtpSection = SecurityManager.GetSmtpMailSection();
        //        SMTPNetwork(smtpSection, mailClient);
        //    }


        //    SmtpClient smtp = new SmtpClient();
        //        smtp.Host = smtpSection.Network.Host; //---- SMTP Host Details. 
        //        smtp.EnableSsl = smtpSection.Network.EnableSsl; //---- Specify whether host accepts SSL Connections or not.
        //        NetworkCredential NetworkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
        //        //---Your Email and password
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = NetworkCred;
        //        smtp.Port = 587; //---- SMTP Server port number. This varies from host to host. 
        //        smtp.Send(objMailMsg);
          
        //}

        /// <summary>
        /// Get company logo for email body 
        /// </summary>
        /// <returns></returns>
        private static LinkedResource GetCompanyLogoForEmailBody(string path)
        {
            LinkedResource logo = new LinkedResource(path, imageType);
            logo.ContentId = companylogo;
            return logo;
        }

        /// <summary>
        /// Sending an email to the respected persons.
        /// </summary>
        /// <param name="attachFilePath">attachment file path</param>
        /// <param name="emailTo">email to</param>
        /// <param name="emailCC">email cc</param>
        /// <param name="emailBcc">email bcc</param>
        /// <param name="emailFrom">email from</param>
        /// <param name="emailSubject">email subject</param>
        /// <param name="emailBody">email body</param>
        /// <param name="isHtmlBody">is html body</param>
        /// <param name="imagePath">signature image path</param>
        /// <returns>success result</returns>
        public bool SendMailFromService(string attachFilePath, string emailTo, string emailCC, string emailBcc, string emailFrom,
                                        string emailSubject, string emailBody, bool isHtmlBody, string imagePath, SmtpSection smtpSection = null)
        {

            using (MailMessage message = new MailMessage())
            {
                ////Add to mail
                message.To.Add(emailTo);

                ////check for cc
                if (StringManager.IsNotEqual(emailCC, string.Empty))
                {
                    AddEmailsList(emailCC, message, VectorEnums.EmailType.CC.ToString());
                }

                ////Check for bcc
                if (StringManager.IsNotEqual(emailBcc, string.Empty))
                {
                    AddEmailsList(emailBcc, message, VectorEnums.EmailType.Bcc.ToString());
                }

                ////check for attachment
                if (StringManager.IsNotEqual(attachFilePath, string.Empty))
                {
                    this.attachFileName = attachFilePath;
                    Attachment attachment = new Attachment(this.attachFileName);
                    message.Attachments.Add(attachment);
                }

                ////adding subject
                message.Subject = emailSubject;

                ////message.Body = emailBody;
                LinkedResource logo = new LinkedResource(imagePath, MediaTypeNames.Image.Gif);
                logo.ContentId = "companylogo";

                AlternateView emailLogo;
                emailLogo = AlternateView.CreateAlternateViewFromString("<html><body style='border: solid 1px #becddd;font-size: 11px; font-family: Verdana; border:thin'>" + emailBody + "</body></html>", null, System.Net.Mime.MediaTypeNames.Text.Html);
                emailLogo.LinkedResources.Add(logo);
                message.AlternateViews.Add(emailLogo);

                //////Add from address
                //MailAddress address = new MailAddress(emailFrom);
                //message.From = address;

                //Check if From email Exists
                if (!string.IsNullOrEmpty(emailFrom))
                {
                    MailAddress objFromAddress = new MailAddress(emailFrom);
                    message.From = objFromAddress;
                }

                ////Adding html style to body
                message.IsBodyHtml = isHtmlBody;
                using (SmtpClient mailClient = new SmtpClient())
                {
                    if (smtpSection != null)
                    {
                        SMTPNetwork(smtpSection, mailClient);
                    }
                    else
                    {
                        smtpSection = SecurityManager.GetSmtpMailSection();
                        SMTPNetwork(smtpSection, mailClient);
                    }
                    mailClient.Send(message);
                }
                message.Dispose();
                return true;
            }
        }

        /// <summary>
        /// Adding Emails List to email composing
        /// </summary>
        /// <param name="emailCc">email cc</param>
        /// <param name="message">mail message</param>
        /// <param name="emailType">email type</param>
        public static void AddEmailsList(string emailCc, MailMessage message, string emailType)
        {
            string[] emails = null;

            if (!string.IsNullOrEmpty(emailCc))
            {
                emails = emailCc.Split(',');
            }

            foreach (string email in emails)
            {
                if (!string.IsNullOrEmpty(email))
                {
                    if (StringManager.IsEqual(emailType, VectorEnums.EmailType.CC.ToString()))
                    {
                        message.CC.Add(email);
                    }
                    else if (StringManager.IsEqual(emailType, VectorEnums.EmailType.Bcc.ToString()))
                    {
                        message.Bcc.Add(email);
                    }
                }
            }
        }
        public static void TransmitEMLFile(string filePath)
        {
            FileInfo fileinfo = new FileInfo(filePath);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.AddHeader(ContentDisposition, string.Format(attachment_filename, fileinfo.Name));
            HttpContext.Current.Response.AddHeader(ContentLength, fileinfo.Length.ToString());
            HttpContext.Current.Response.ContentType = ContentType;
            HttpContext.Current.Response.TransmitFile(filePath);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        #endregion
    }
}
