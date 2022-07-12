using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Vector.Common.BusinessLayer
{
    public static class EmailProcessManager
    {
        private const string ServerProtocalData = "Imap4";
        public class MailDetails
        {
            public List<MailDetail> MailDetailsList { get; set; }

            public int TotalCount { get; set; }
        }
        public class MailDetail
        {
            public string UIDL { get; set; }

            public string From { get; set; }

            public string Subject { get; set; }

            public bool IsAttachmentExist { get; set; }

            public string ReceivedDate { get; set; }

            public int AttachmentCount { get; set; }

            public string AttachmentOpen { get; set; }

            public string Action { get; set; }
            public string EWSChangeKey { get; set; }

            public string Body { get; set; }

            public string FromAddress { get; set; }

            public string MailImportance { get; set; }

            public string InternetMessageId { get; set; }
            public string EmailCC { get; set; }

            public string IsEmailRead { get; set; }
            public string EmailTo { get; internal set; }

            public string ConversationId { get; set; }

            public string ConversationTopic { get; set; }
            public string DisplayCc { get; set; }

            public string DisplayTo { get; set; }

            public string ChangeKey { get; set; }
            public string UniqueKey { get; set; }
            public string ToRecipients { get; set; }
            public string BodyText { get; set; }
            public DateTime ReceivedDateTime { get; set; }
        }
        public static MailDetails ReadingMailsToGetMailDetails(string server, string emailId, string password, int noOfMails, string serverProtocal, string priorityType = "All")
        {
            try
            {
                //Ignoring SSL certificate Validation.
                System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

                ExchangeService ews = GetExchangeService(server, emailId, password, serverProtocal);
                ItemView itemView = new ItemView(noOfMails);
                SearchFilter uneadSearchFilter = new SearchFilter.SearchFilterCollection(LogicalOperator.Or, new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, false));
                FindItemsResults<Item> mailList = null;
                mailList = ews.FindItems(WellKnownFolderName.Inbox, uneadSearchFilter, itemView);
                MailDetails listMailDetails = new MailDetails();
                listMailDetails.MailDetailsList = new List<MailDetail>();
                foreach (EmailMessage message in mailList)
                {
                    string a = message.From.Address;
                    EmailMessage mailMessage = EmailMessage.Bind(ews, message.Id);
                    //Importance obj = mailMessage.Importance;
                    string fromAddress = mailMessage.From.Address;
                    if (priorityType == "High" && mailMessage.Importance == Importance.High)
                        listMailDetails.MailDetailsList.Add(new MailDetail() { UIDL = message.Id.UniqueId, From = String.IsNullOrEmpty(message.From.Name) ? fromAddress : message.From.Name, Subject = message.Subject, IsAttachmentExist = message.HasAttachments, AttachmentCount = mailMessage.Attachments.Count, ReceivedDate = mailMessage.DateTimeReceived.ToString(), Body = mailMessage.Body, FromAddress = mailMessage.From.Address, MailImportance = mailMessage.Importance.ToString(), InternetMessageId = mailMessage.InternetMessageId });
                    if (priorityType == "Low" && mailMessage.Importance == Importance.Low)
                        listMailDetails.MailDetailsList.Add(new MailDetail() { UIDL = message.Id.UniqueId, From = String.IsNullOrEmpty(message.From.Name) ? fromAddress : message.From.Name, Subject = message.Subject, IsAttachmentExist = message.HasAttachments, AttachmentCount = mailMessage.Attachments.Count, ReceivedDate = mailMessage.DateTimeReceived.ToString(), Body = mailMessage.Body, FromAddress = mailMessage.From.Address, MailImportance = mailMessage.Importance.ToString(), InternetMessageId = mailMessage.InternetMessageId });
                    if (priorityType == "Normal" && mailMessage.Importance == Importance.Normal)
                        listMailDetails.MailDetailsList.Add(new MailDetail() { UIDL = message.Id.UniqueId, From = String.IsNullOrEmpty(message.From.Name) ? fromAddress : message.From.Name, Subject = message.Subject, IsAttachmentExist = message.HasAttachments, AttachmentCount = mailMessage.Attachments.Count, ReceivedDate = mailMessage.DateTimeReceived.ToString(), Body = mailMessage.Body, FromAddress = mailMessage.From.Address, MailImportance = mailMessage.Importance.ToString(), InternetMessageId = mailMessage.InternetMessageId });
                    if (priorityType == "All")
                        listMailDetails.MailDetailsList.Add(new MailDetail() { UIDL = message.Id.UniqueId, From = String.IsNullOrEmpty(message.From.Name) ? fromAddress : message.From.Name, Subject = message.Subject, IsAttachmentExist = message.HasAttachments, AttachmentCount = mailMessage.Attachments.Count, ReceivedDate = mailMessage.DateTimeReceived.ToString(), Body = mailMessage.Body, FromAddress = mailMessage.From.Address, MailImportance = mailMessage.Importance.ToString(), InternetMessageId = mailMessage.InternetMessageId });
                }
                listMailDetails.TotalCount = mailList.TotalCount;
                return listMailDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static ExchangeService GetExchangeService(string server, string emailId, string password, string serverProtocal)
        {
            try
            {
                ExchangeService ews = new ExchangeService(ExchangeVersion.Exchange2010_SP1);
                ews.Credentials = new WebCredentials(emailId, password);
                if (!string.Equals(serverProtocal, ServerProtocalData))
                    ews.Url = new Uri(server);
                else
                    ews.AutodiscoverUrl(server, RedirectionCallback);

                return ews;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        static bool RedirectionCallback(string url)
        {
            // Return true if the URL is an HTTPS URL.
            return url.ToLower().StartsWith("https://");
        }

        public static DataTable GetEmailParentIdByMessageId(DataTable data, string server, string emailId, string password, string serverProtocal)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            ExchangeService ews = GetExchangeService(server, emailId, password, serverProtocal);
            MailDetails listMailDetails = new MailDetails();
            listMailDetails.MailDetailsList = new List<MailDetail>();
            DataTable emailData = new DataTable();
            emailData.TableName = "EmailData";
            emailData.Columns.Add("VendorId", typeof(string));
            emailData.Columns.Add("MessageId", typeof(string));
            emailData.Columns.Add("UIDL", typeof(string));

            foreach (DataRow item in data.Rows)
            {
                ItemView itemView = new ItemView(50);
                itemView.PropertySet = new PropertySet(BasePropertySet.IdOnly, EmailMessageSchema.InternetMessageId);
                List<SearchFilter> searchFilterCollection = new List<SearchFilter>();
                searchFilterCollection.Add(new SearchFilter.ContainsSubstring(EmailMessageSchema.InternetMessageId, item["MessageId"].ToString()));
                SearchFilter uneadSearchFilter = new SearchFilter.SearchFilterCollection(LogicalOperator.Or, searchFilterCollection.ToArray());
                FindItemsResults<Item> mailList = null;
                mailList = ews.FindItems(WellKnownFolderName.SentItems, uneadSearchFilter, itemView);

                foreach (EmailMessage message in mailList)
                {
                    EmailMessage mailMessage = EmailMessage.Bind(ews, message.Id);
                    AddDataToEmailTable(emailData, mailMessage.InternetMessageId, mailMessage.ParentFolderId.UniqueId);
                }
            }
            return emailData;
        }

        private static void AddDataToEmailTable(DataTable emailData, string messageId, string UIDL)
        {
            DataRow rowDate = emailData.NewRow();
            rowDate["VendorId"] = 0;
            rowDate["MessageId"] = messageId;
            rowDate["UIDL"] = UIDL;
            emailData.Rows.Add(rowDate);
        }

        public static MailDetails GetEmailDetailsByNegotiaion(DataTable data, string server, string emailId, string password, string serverProtocal)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            ExchangeService ews = GetExchangeService(server, emailId, password, serverProtocal);
            MailDetails listMailDetails = new MailDetails();
            listMailDetails.MailDetailsList = new List<MailDetail>();

            foreach (DataRow item in data.Rows)
            {
                if (!string.IsNullOrEmpty(item["EmailMessageId"].ToString()))
                {
                    ItemView itemView = new ItemView(50);
                    itemView.OrderBy.Add(ItemSchema.DateTimeReceived, SortDirection.Descending);
                    List<SearchFilter> searchFilterCollection = new List<SearchFilter>();
                    searchFilterCollection.Add(new SearchFilter.ContainsSubstring(EmailMessageSchema.Body, item["EmailMessageId"].ToString()));
                    SearchFilter uneadSearchFilter = new SearchFilter.SearchFilterCollection(LogicalOperator.Or, searchFilterCollection.ToArray());

                    FindItemsResults<Item> mailList = null;

                    mailList = ews.FindItems(WellKnownFolderName.SentItems, uneadSearchFilter, itemView);

                    foreach (EmailMessage message in mailList)
                    {
                        EmailMessage mailMessage = EmailMessage.Bind(ews, message.Id);
                        string fromAddress = mailMessage.From.Address;
                        listMailDetails.MailDetailsList.Add(new MailDetail()
                        {
                            UIDL = message.Id.UniqueId,
                            From = String.IsNullOrEmpty(message.From.Name) ? fromAddress : message.From.Name,
                            Subject = message.Subject,
                            IsAttachmentExist = message.HasAttachments,
                            AttachmentCount = mailMessage.Attachments.Count,
                            ReceivedDate = mailMessage.DateTimeReceived.ToString(),
                            Body = FormatEmailBody(mailMessage.Body),
                            FromAddress = mailMessage.From.Address,
                            MailImportance = mailMessage.Importance.ToString(),
                            InternetMessageId = mailMessage.InternetMessageId,
                            EmailCC = String.IsNullOrEmpty(mailMessage.DisplayCc) ? "" : string.Join(",", mailMessage.CcRecipients.Select(x => x.Address).ToList()),
                            IsEmailRead = mailMessage.IsRead ? "Read" : "UnRead",
                            EmailTo = String.IsNullOrEmpty(mailMessage.DisplayTo) ? "" : string.Join(",", mailMessage.ToRecipients.Select(x => x.Address).ToList())
                        });
                    }


                    mailList = null;

                    mailList = ews.FindItems(WellKnownFolderName.Outbox, uneadSearchFilter, itemView);

                    foreach (EmailMessage message in mailList)
                    {
                        EmailMessage mailMessage = EmailMessage.Bind(ews, message.Id);
                        string fromAddress = mailMessage.From.Address;
                        listMailDetails.MailDetailsList.Add(new MailDetail()
                        {
                            UIDL = message.Id.UniqueId,
                            From = String.IsNullOrEmpty(message.From.Name) ? fromAddress : message.From.Name,
                            Subject = message.Subject,
                            IsAttachmentExist = message.HasAttachments,
                            AttachmentCount = mailMessage.Attachments.Count,
                            ReceivedDate = mailMessage.DateTimeReceived.ToString(),
                            Body = FormatEmailBody(mailMessage.Body),
                            FromAddress = mailMessage.From.Address,
                            MailImportance = mailMessage.Importance.ToString(),
                            InternetMessageId = mailMessage.InternetMessageId,
                            EmailCC = String.IsNullOrEmpty(mailMessage.DisplayCc) ? "" : string.Join(",", mailMessage.CcRecipients.Select(x => x.Address).ToList()),
                            IsEmailRead = mailMessage.IsRead ? "Read" : "UnRead",
                            EmailTo = String.IsNullOrEmpty(mailMessage.DisplayTo) ? "" : string.Join(",", mailMessage.ToRecipients.Select(x => x.Address).ToList())
                        });
                    }



                    mailList = null;

                    FolderView view = new FolderView(100);
                    view.PropertySet = new PropertySet(BasePropertySet.IdOnly);
                    view.PropertySet.Add(FolderSchema.DisplayName);
                    view.Traversal = FolderTraversal.Deep;
                    FindFoldersResults findFolderResults = ews.FindFolders(WellKnownFolderName.Inbox, view);

                    foreach (Folder f in findFolderResults)
                    {
                        mailList = ews.FindItems(f.Id, uneadSearchFilter, itemView);

                        foreach (EmailMessage message in mailList)
                        {
                            EmailMessage mailMessage = EmailMessage.Bind(ews, message.Id);
                            string fromAddress = mailMessage.From.Address;
                            listMailDetails.MailDetailsList.Add(new MailDetail()
                            {
                                UIDL = message.Id.UniqueId,
                                From = String.IsNullOrEmpty(message.From.Name) ? fromAddress : message.From.Name,
                                Subject = message.Subject,
                                IsAttachmentExist = message.HasAttachments,
                                AttachmentCount = mailMessage.Attachments.Count,
                                ReceivedDate = mailMessage.DateTimeReceived.ToString(),
                                Body = FormatEmailBody(mailMessage.Body),
                                FromAddress = mailMessage.From.Address,
                                MailImportance = mailMessage.Importance.ToString(),
                                InternetMessageId = mailMessage.InternetMessageId,
                                EmailCC = String.IsNullOrEmpty(mailMessage.DisplayCc) ? "" : string.Join(",", mailMessage.CcRecipients.Select(x => x.Address).ToList()),
                                IsEmailRead = mailMessage.IsRead ? "Read" : "UnRead",
                                EmailTo = String.IsNullOrEmpty(mailMessage.DisplayTo) ? "" : string.Join(",", mailMessage.ToRecipients.Select(x => x.Address).ToList())
                            });
                        }

                    }



                }
            }

            return listMailDetails;
        }

        public static MailDetails GetEmailDetailsByTicket(string emailMessageId, string server, string emailId, string password, string serverProtocal, MailDetails existingMailDetails = null)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            ExchangeService ews = GetExchangeService(server, emailId, password, serverProtocal);

            MailDetails listMailDetails = new MailDetails();

            if (existingMailDetails != null)
                listMailDetails = existingMailDetails;

            listMailDetails.MailDetailsList = new List<MailDetail>();


            if (!string.IsNullOrEmpty(emailMessageId))
            {
                ItemView itemView = new ItemView(50);
                itemView.OrderBy.Add(ItemSchema.DateTimeReceived, SortDirection.Descending);
                List<SearchFilter> searchFilterCollection = new List<SearchFilter>();
                searchFilterCollection.Add(new SearchFilter.ContainsSubstring(EmailMessageSchema.Body, emailMessageId));
                SearchFilter uneadSearchFilter = new SearchFilter.SearchFilterCollection(LogicalOperator.Or, searchFilterCollection.ToArray());

                FindItemsResults<Item> mailList = null;

                mailList = ews.FindItems(WellKnownFolderName.Inbox, uneadSearchFilter, itemView);
                foreach (EmailMessage message in mailList)
                {
                    EmailMessage mailMessage = EmailMessage.Bind(ews, message.Id);
                    string fromAddress = mailMessage.From.Address;
                    listMailDetails.MailDetailsList.Add(new MailDetail()
                    {
                        UIDL = message.Id.UniqueId,
                        From = String.IsNullOrEmpty(message.From.Name) ? fromAddress : message.From.Name,
                        Subject = message.Subject,
                        IsAttachmentExist = message.HasAttachments,
                        AttachmentCount = mailMessage.Attachments.Count,
                        ReceivedDate = mailMessage.DateTimeReceived.ToString(),
                        Body = FormatEmailBody(mailMessage.Body),
                        FromAddress = mailMessage.From.Address,
                        MailImportance = mailMessage.Importance.ToString(),
                        InternetMessageId = mailMessage.InternetMessageId,
                        EmailCC = String.IsNullOrEmpty(mailMessage.DisplayCc) ? "" : string.Join(",", mailMessage.CcRecipients.Select(x => x.Address).ToList()),
                        IsEmailRead = mailMessage.IsRead ? "Read" : "UnRead",
                        EmailTo = String.IsNullOrEmpty(mailMessage.DisplayTo) ? "" : string.Join(",", mailMessage.ToRecipients.Select(x => x.Address).ToList())
                    }); ;
                }


                mailList = ews.FindItems(WellKnownFolderName.SentItems, uneadSearchFilter, itemView);
                foreach (EmailMessage message in mailList)
                {
                    EmailMessage mailMessage = EmailMessage.Bind(ews, message.Id);
                    string fromAddress = mailMessage.From.Address;
                    listMailDetails.MailDetailsList.Add(new MailDetail()
                    {
                        UIDL = message.Id.UniqueId,
                        From = String.IsNullOrEmpty(message.From.Name) ? fromAddress : message.From.Name,
                        Subject = message.Subject,
                        IsAttachmentExist = message.HasAttachments,
                        AttachmentCount = mailMessage.Attachments.Count,
                        ReceivedDate = mailMessage.DateTimeReceived.ToString(),
                        Body = FormatEmailBody(mailMessage.Body),
                        FromAddress = mailMessage.From.Address,
                        MailImportance = mailMessage.Importance.ToString(),
                        InternetMessageId = mailMessage.InternetMessageId,
                        EmailCC = String.IsNullOrEmpty(mailMessage.DisplayCc) ? "" : string.Join(",", mailMessage.CcRecipients.Select(x => x.Address).ToList()),
                        IsEmailRead = mailMessage.IsRead ? "Read" : "UnRead",
                        EmailTo = String.IsNullOrEmpty(mailMessage.DisplayTo) ? "" : string.Join(",", mailMessage.ToRecipients.Select(x => x.Address).ToList())
                    }); ;
                }
            }


            return listMailDetails;
        }

        private static string FormatEmailBody(string rawtext)
        {
            string emailBody = "";
            if (!string.IsNullOrEmpty(rawtext))
            {
                int bodyindex = rawtext.IndexOf("<body");
                if (bodyindex > -1)
                    emailBody = rawtext.Substring(bodyindex);
                else
                    emailBody = rawtext;

                //emailBody = Regex.Replace(emailBody.Replace("<!-- P {margin-top:0;margin-bottom:0;} -->", string.Empty), @"<[^>]+>|&nbsp;", "").Trim();
                //emailBody = Regex.Replace(emailBody, @"[\r\n]+", "\r\n").Replace("&#43;", "+");
            }
            else
                emailBody = string.Empty;

            return emailBody;
        }

        public static int MarkMailAsReadFromES(string server, string emailId, string password, string serverProtocal, List<string> mailUniqueIds)
        {
            try
            {
                ExchangeService ews = GetExchangeService(server, emailId, password, serverProtocal);
                foreach (var mailUniqueId in mailUniqueIds)
                {
                    EmailMessage email = EmailMessage.Bind(ews, new ItemId(mailUniqueId));
                    email.IsRead = true;
                    email.Update(ConflictResolutionMode.AlwaysOverwrite);
                }
                return 0;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public static string DownloadAttachmentsAsZip(string server, string emailId, string password, string serverProtocal, string itemId, string mailFolderPath)
        {
            try
            {
                string mainPath = mailFolderPath;
                itemId = itemId.Replace(" ", "+");
                if (!Directory.Exists(mainPath))
                {
                    Directory.CreateDirectory(mainPath);
                }
                ExchangeService ews = GetExchangeService(server, emailId, password, serverProtocal);
                EmailMessage email = EmailMessage.Bind(ews, new ItemId(itemId));
                string tempFolder = mainPath + VectorConstants.DoubleSlash + "temp" + VectorConstants.DoubleSlash;

                if (Directory.Exists(tempFolder))
                {
                    Directory.Delete(tempFolder, true);
                }
                Directory.CreateDirectory(tempFolder);

                foreach (FileAttachment item in email.Attachments)
                {
                    item.Load(tempFolder + VectorConstants.DoubleSlash + item.Name);
                }
                string zipFile = mainPath + VectorConstants.DoubleSlash + DateManager.GenerateTitleWithTimestamp("Attachment") + VectorConstants.ZipExtension;
                FileManager.ZipFolder(zipFile, tempFolder);
                Directory.Delete(tempFolder, true);

                return zipFile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MailDetails GetEmails(string server, string emailId, string password, string serverProtocal, MailDetails existingMailDetails = null)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            ExchangeService ews = GetExchangeService(server, emailId, password, serverProtocal);

            MailDetails listMailDetails = new MailDetails();

            if (existingMailDetails != null)
                listMailDetails = existingMailDetails;

            listMailDetails.MailDetailsList = new List<MailDetail>();


            ItemView itemView = new ItemView(50);
            itemView.OrderBy.Add(ItemSchema.DateTimeReceived, SortDirection.Descending);
            List<SearchFilter> searchFilterCollection = new List<SearchFilter>();
            searchFilterCollection.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, "false"));
            SearchFilter uneadSearchFilter = new SearchFilter.SearchFilterCollection(LogicalOperator.And, searchFilterCollection.ToArray());

            FindItemsResults<Item> mailList = null;

            mailList = ews.FindItems(WellKnownFolderName.Inbox, uneadSearchFilter, itemView);
            foreach (EmailMessage message in mailList)
            {
                if (!message.IsRead)
                {

                    EmailMessage mailMessage = EmailMessage.Bind(ews, message.Id);
                    string fromAddress = mailMessage.From.Address;
                    listMailDetails.MailDetailsList.Add(new MailDetail()
                    {
                        UIDL = message.Id.UniqueId,
                        From = String.IsNullOrEmpty(message.From.Name) ? fromAddress : message.From.Name,
                        Subject = message.Subject,
                        IsAttachmentExist = message.HasAttachments,
                        AttachmentCount = mailMessage.Attachments.Count,
                        ReceivedDate = mailMessage.DateTimeReceived.ToString(),
                        ReceivedDateTime = mailMessage.DateTimeReceived,
                        Body = FormatEmailBody(mailMessage.Body),
                        BodyText = ReplaceEmailHtmltoText(FormatEmailBody(mailMessage.Body)),
                        FromAddress = mailMessage.From.Address,
                        MailImportance = mailMessage.Importance.ToString(),
                        InternetMessageId = mailMessage.InternetMessageId,
                        EmailCC = String.IsNullOrEmpty(mailMessage.DisplayCc) ? "" : string.Join(",", mailMessage.CcRecipients.Select(x => x.Address).ToList()),
                        IsEmailRead = mailMessage.IsRead ? "Read" : "UnRead",
                        EmailTo = String.IsNullOrEmpty(mailMessage.DisplayTo) ? "" : string.Join(",", mailMessage.ToRecipients.Select(x => x.Address).ToList()),
                        ConversationId = mailMessage.ConversationId,
                        ConversationTopic = mailMessage.ConversationTopic,
                        DisplayCc = mailMessage.DisplayCc,
                        DisplayTo = mailMessage.DisplayTo,
                        ChangeKey = mailMessage.Id.ChangeKey,
                        UniqueKey = mailMessage.Id.UniqueId
                    }); ;
                }
            }



            return listMailDetails;
        }

        public static string ReplaceEmailHtmltoText(string text)
        {
            string result = "";

            var document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(text);

            var listText = document.DocumentNode.SelectNodes("//text()[normalize-space()]").ToList();

            result = (from c in listText
                      select c.InnerText
                   ).Aggregate(
                   new StringBuilder(),
                   (sb, s) => sb.Append(" \r" +  s.Replace("&nbsp;"," ")),
                   sb => sb.ToString()
               );


            return result;
        }

        public static bool SetMailAsRead(string server, string emailId, string password, string serverProtocal, string udil)
        {
            bool result = false;

            System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            ExchangeService ews = GetExchangeService(server, emailId, password, serverProtocal);

            MailDetails listMailDetails = new MailDetails();


            listMailDetails.MailDetailsList = new List<MailDetail>();


            ItemView itemView = new ItemView(50);
            itemView.OrderBy.Add(ItemSchema.DateTimeReceived, SortDirection.Descending);
            List<SearchFilter> searchFilterCollection = new List<SearchFilter>();


            searchFilterCollection.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, "false"));
            searchFilterCollection.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.Id, udil));
            SearchFilter uneadSearchFilter = new SearchFilter.SearchFilterCollection(LogicalOperator.And, searchFilterCollection.ToArray());

            FindItemsResults<Item> mailList = null;

            mailList = ews.FindItems(WellKnownFolderName.Inbox, uneadSearchFilter, itemView);
            foreach (EmailMessage message in mailList)
            {
                PropertySet msgPropertySet = new PropertySet(BasePropertySet.IdOnly, ItemSchema.Id, EmailMessageSchema.IsRead);
                EmailMessage mailMessage = EmailMessage.Bind(ews, message.Id, msgPropertySet);

                mailMessage.IsRead = true;
                mailMessage.Update(ConflictResolutionMode.AutoResolve);
            }

            result = true;

            return result;

        }

        public static List<string> DownloadEmailDocuments(string server, string emailId, string password, string serverProtocal, string udil, string ticketFolderPath)
        {
            List<string> docList = new List<string>();

            System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            ExchangeService ews = GetExchangeService(server, emailId, password, serverProtocal);

            MailDetails listMailDetails = new MailDetails();


            listMailDetails.MailDetailsList = new List<MailDetail>();


            ItemView itemView = new ItemView(50);
            itemView.OrderBy.Add(ItemSchema.DateTimeReceived, SortDirection.Descending);
            List<SearchFilter> searchFilterCollection = new List<SearchFilter>();

            searchFilterCollection.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.Id, udil));
            SearchFilter uneadSearchFilter = new SearchFilter.SearchFilterCollection(LogicalOperator.And, searchFilterCollection.ToArray());

            FindItemsResults<Item> mailList = null;

            mailList = ews.FindItems(WellKnownFolderName.Inbox, uneadSearchFilter, itemView);
            foreach (EmailMessage message in mailList)
            {
                EmailMessage mailMessage = EmailMessage.Bind(ews, message.Id);

                if (!Directory.Exists(ticketFolderPath))
                {
                    Directory.CreateDirectory(ticketFolderPath);
                }

                foreach (var i in mailMessage.Attachments)
                {
                    FileAttachment fileAttachment = i as FileAttachment;
                    fileAttachment.Load(ticketFolderPath + "\\" + fileAttachment.Name); 
                    docList.Add(fileAttachment.Name);
                }


            }



            return docList;

        }

        public static DataSet GetEmailDocuments(string server, string emailId, string password, string serverProtocal, MailDetail objMailDetail, string folderPath,string folderPathUrl)
        {
            DataSet dsData = new DataSet();
            DataTable dtData = new DataTable("emailDocuments");
            dtData.Columns.Add("DocumentName", typeof(string));
            dtData.Columns.Add("documentPath", typeof(string)); 

            System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            ExchangeService ews = GetExchangeService(server, emailId, password, serverProtocal);

            MailDetails listMailDetails = new MailDetails();


            listMailDetails.MailDetailsList = new List<MailDetail>();


            ItemView itemView = new ItemView(50);
            itemView.OrderBy.Add(ItemSchema.DateTimeReceived, SortDirection.Descending);
            List<SearchFilter> searchFilterCollection = new List<SearchFilter>();

            searchFilterCollection.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.Id, objMailDetail.UIDL));
            SearchFilter uneadSearchFilter = new SearchFilter.SearchFilterCollection(LogicalOperator.And, searchFilterCollection.ToArray());

            FindItemsResults<Item> mailList = null;

            mailList = ews.FindItems(WellKnownFolderName.Inbox, uneadSearchFilter, itemView);
            foreach (EmailMessage message in mailList)
            {
                EmailMessage mailMessage = EmailMessage.Bind(ews, message.Id);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                foreach (var i in mailMessage.Attachments)
                {
                    DataRow drNow = dtData.NewRow();

                    FileAttachment fileAttachment = i as FileAttachment;
                    fileAttachment.Load(folderPath + "\\" + fileAttachment.Name);
                    drNow[0] = fileAttachment.Name;
                    drNow[1] = folderPathUrl + "//" + fileAttachment.Name; 

                    dtData.Rows.Add(drNow);
                }


            }

            dsData.Tables.Add(dtData);

            return dsData;

        }

    }
}
