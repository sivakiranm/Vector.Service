using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Garage.DataLayer;
using Vector.Garage.Entities;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Word;
//using WordDocument = Microsoft.Office.Interop.Word.Document;
using System.Web;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;
using iTextSharp.tool.xml;

namespace Vector.Garage.BusinessLayer
{
    public class InvoiceBL : DisposeLogic
    {
        private VectorDataConnection objVectorDB;
        public InvoiceBL(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }

        private const string HaulerTemplate = "HaulerAuditedInvoice.docx";
        private const string HaulerTemplateAmd = "HaulerAuditedInvoiceAmd.docx";
        private const string RsTemplate = "RSSavingInvoice.docx";
        private const string BillPayRsHauler_RsSavings = "BillPayRsHauler_RsSavings.docx";
        private const string BillPayRsSavings_RsSavings = "BillPayRsSavings_RsSavings.docx";
        private const string HaulerSummaryTemplate = "HaulerAuditedSummary.docx";
        private const string RSManagementFeeInvoices = "RSManagementFeeInvoices.docx";
        private const string RSSavingInvoiceNoHaulerInvoice = "RSSavingInvoiceNoHaulerInvoice.docx";
        private const string RsManagementFeeTemplate2 = "RSManagementFeeInvoices.docx";
        private const string InternalUserType = "INTERNAL";
        private const string ExternalUserType = "EXTERNAL";
        private const string HaulerInvoicePoint1 = "* For past due amounts, payments posted and credits issued, please see the hauler original invoice below.";
        private const string BillPaySummary = "BillPaySummary.docx";
        private const string unpaidNote = "PAST DUE AMOUNTS MAY CAUSE SERVICE INTERUPTION";
        private const string noDueNote = "No Payment Due";
        private const string noNote = "";


        public VectorResponse<object> GetBatchInfo(BatchInfoSearch objBatchInfo)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.GetBatchInfo(objBatchInfo);
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

        public VectorResponse<object> ManageBatchInfo(BatchInfo objBatchInfo)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.ManageBatchInfo(objBatchInfo);
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

        public VectorResponse<object> MangeFinalizeBatchInfo(BatchInfo objBatchInfo)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.MangeFinalizeBatchInfo(objBatchInfo);
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

        public VectorResponse<object> GetInvoiceHeaderInfoSearch(InvoiceHeaderInfoSearch objInvoiceHeaderInfoSearch)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.GetInvoiceHeaderInfoSearch(objInvoiceHeaderInfoSearch);
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
        public VectorResponse<object> MangeBatchUploadDocumentsInfo(BatchUploadDocumentsInfo batchUploadDocumentsInfo)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                XDocument batchDocs = new XDocument(
                     new XElement("ROOT",
                     from batchDoc in batchUploadDocumentsInfo.BatchUploadDocuments
                     select new XElement("Images",
                                 new XElement("ImageName", batchDoc.ImageName),
                                 new XElement("ImagePath", batchDoc.ImagePath)

                     )));
                var result = objInvoiceDL.MangeBatchUploadDocumentsInfo(batchUploadDocumentsInfo, batchDocs.ToString());
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


        public VectorResponse<object> ManageInvoiceHeaderInfo(InvoiceHeaderInfo objInvoiceHeaderInfo)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.ManageInvoiceHeaderInfo(objInvoiceHeaderInfo);
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

        public VectorResponse<object> VectorGetInvoicesForProcessing(InvoiceLineItemInfoSearch objInvoiceLineItemInfoSearch)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.VectorGetInvoicesForProcessing(objInvoiceLineItemInfoSearch);
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

        public VectorResponse<object> VectorGetInvoicesForAudit(InvoiceLineItemInfoSearch objInvoiceLineItemInfoSearch)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.VectorGetInvoicesForAudit(objInvoiceLineItemInfoSearch);
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

        public VectorResponse<object> GetInvoiceDetails(InvoiceLineItemInfoSearch objInvoiceLineItemInfoSearch, Int64 userId)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.GetInvoiceDetails(objInvoiceLineItemInfoSearch, userId);
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

        public VectorResponse<object> ManageInvoiceLineItemsInfo(InvoiceHeaderInfo objInvoiceHeaderInfo,Int64 userId)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.ManageInvoiceLineItemsInfo(objInvoiceHeaderInfo, userId);
                string shortPayNotificationResult = "";
                if (StringManager.IsEqual(objInvoiceHeaderInfo.InvoiceStatus.ToUpper(), "AUDITED"))
                {
                    shortPayNotificationResult = ShortPayNotification(objInvoiceHeaderInfo,userId);
                }

                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result, ResponseMessage = shortPayNotificationResult };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to Process Request, Please Contact Support." } };
                }
            }
        }

        private string ShortPayNotification(InvoiceHeaderInfo objInvoiceHeaderInfo,Int64 userId)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.GetInvoiceNotificaitonsInfo("ShortPayEmailInfo", objInvoiceHeaderInfo,userId);

                if (!DataManager.IsNullOrEmptyDataSet(result, 2, 2))
                {


                    using (EmailDetails objEmailDetails = new EmailDetails())
                    {

                        var emailInfo = (from c in result.Tables[0].AsEnumerable()
                                         select new
                                         {
                                             EmailBody = c.Field<string>("emailBody"),
                                             EmailSubject = c.Field<string>("emailSubject"),
                                             ToEmail = c.Field<string>("EmailTo"),
                                             CCEmail = c.Field<string>("EmailCC"),
                                             EmailServiceName = c.Field<string>("EmailServiceName"),
                                             ContractDocumentName = c.Field<string>("ContractDocumentName"),
                                             ContractDocumentPath = c.Field<string>("ContractDocumentPath")
                                         }).FirstOrDefault();

                        string body = emailInfo.EmailBody.Replace("@ServiceLevelItems@", ConvertDataTabletoHTML(result.Tables[1]));
                        SendEmail objSendEmail = new SendEmail();
                        objSendEmail.To = emailInfo.ToEmail;
                        objSendEmail.CC = emailInfo.CCEmail;
                        objSendEmail.Subject = emailInfo.EmailSubject;
                        objSendEmail.FromEmail = "SHORTPAY";
                        objSendEmail.Body = body;

                        if (!string.IsNullOrEmpty(emailInfo.ContractDocumentPath))
                        {
                            List<Files> objFileList = new List<Files>();

                            Files objFile = new Files();
                            objFile.fileName = emailInfo.ContractDocumentName;
                            objFile.filePath = emailInfo.ContractDocumentPath;
                            objFile.isLocal = true;
                            objFileList.Add(objFile);
                            objSendEmail.EmailAttachments = objFileList;
                        }


                        bool emailResult = EmailManager.SendEmail(objSendEmail, smtpSection: SecurityManager.GetSmtpMailSection(emailInfo.EmailServiceName), fromEmail: "SHORTPAY");

                        if (emailResult)
                        {
                            return "Short Pay Notification Sent Successfully";
                        }
                        else
                        {

                            return "Unable to send Short Pay Notification.";
                        }
                    }
                }
                else
                    return string.Empty;
            }
        }

        public static string ConvertDataTabletoHTML(System.Data.DataTable data)
        {
            if (data.Rows.Count == 0) return ""; // enter code here

            StringBuilder builder = new StringBuilder();
            builder.Append("<table cellspacing='0' rules='all' border='1' ");
            builder.Append(">");
            builder.Append("<tbody><tr>");
            foreach (DataColumn c in data.Columns)
            {
                if (c.ColumnName == "Service Description")
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


        public VectorResponse<object> GetInvoicesForVerify(InvoiceLineItemInfoSearch objInvoiceLineItemInfoSearch)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.GetInvoicesForVerify(objInvoiceLineItemInfoSearch);
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

        public VectorResponse<object> GenerateInvoice(InvoiceInfo objInvoiceInfo, string folderPath, string batchFolderPath)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {

                //TODO : Logic to generage Invoice Templates
                var invoiceData = objInvoiceDL.GetInvoiceDetailsForInvoiceGeneration(objInvoiceInfo.Action, objInvoiceInfo.InvoiceId);


                bool invoiceResult = GetInvoiceDataForGeneratingRSInvoiceBO(invoiceData, folderPath, batchFolderPath, objInvoiceInfo.Action);

                if (invoiceResult)
                {
                    var result = objInvoiceDL.GenerateInvoice(objInvoiceInfo);
                    if (DataValidationLayer.isDataSetNotNull(result))
                    {
                        return new VectorResponse<object>() { ResponseData = result };
                    }
                    else
                    {
                        return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                    }
                }
                else
                {
                    DataSet DsData = new DataSet();
                    System.Data.DataTable dtData = new System.Data.DataTable();
                    dtData.Columns.Add("Result", typeof(int));
                    dtData.Columns.Add("ResultMessage", typeof(string));

                    DataRow drRow = dtData.NewRow();
                    drRow["Result"] = 0;
                    drRow["ResultMessage"] = "Unable to Generate Invoice, Contact IT Support.";
                    dtData.Rows.Add(drRow);
                    DsData.Tables.Add(dtData);

                    return new VectorResponse<object>() { ResponseData = DsData };
                }
            }
        }

        public VectorResponse<object> GetExceptionInfo(SearchInfo objSearchInfo)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.GetExceptionInfo(objSearchInfo);
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
        public VectorResponse<object> ManageExceptionInfo(InvoiceHeaderInfo objInvoiceHeaderInfo)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.ManageExceptionInfo(objInvoiceHeaderInfo);
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

        public VectorResponse<object> VectorGetInvoiceLookup(SearchEntity objInvoiceEntity, Int64 userId)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.VectorGetInvoiceLookup(objInvoiceEntity, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[VectorConstants.Zero].TableName = "invoices";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> GetInvoiceForDispatch(SearchEntity objSearchInvoice, Int64 userID)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.GetInvoiceForDispatch(objSearchInvoice, userID);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[VectorConstants.Zero].TableName = "invoices";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> GetInvoiceVerification(SearchEntity objSearchInvoice, Int64 userID)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.GetInvoiceVerification(objSearchInvoice, userID);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[VectorConstants.Zero].TableName = "invoiceverification";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> GetInvoiceVerificationDetails(SearchEntity objSearchInvoice, Int64 userID)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.GetInvoiceVerificationDetails(objSearchInvoice, userID);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[VectorConstants.Zero].TableName = "invoiceverificationdetails";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> ManageInvoiceVerificationDetails(InvoiceTransactions objInvoiceTransactions, Int64 userID)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.ManageInvoiceVerificationDetails(objInvoiceTransactions, userID);
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

        public VectorResponse<object> InvoiceInfoForDispatch(InvoiceInfo objInvoiceInfo)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.InvoiceInfoForDispatch(objInvoiceInfo);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[VectorConstants.Zero].TableName = "invoiceInfo";
                    result.Tables[VectorConstants.One].TableName = "contacts";
                    result.Tables[VectorConstants.Two].TableName = "history";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> ManageDispatchInvoice(DispatchInvoice objDispatchInvoice)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                DispatchInvoice newObj = new DispatchInvoice();
                newObj.InvoiceId = objDispatchInvoice.InvoiceId;
                newObj.Action = "GetDispatchInvoiceInfo";

                var invoiceInfo = objInvoiceDL.ManageDispatchInvoice(newObj);

                bool emailResult = SendEmailForDispatch(objDispatchInvoice, invoiceInfo);

                if (emailResult)
                {
                    var result = objInvoiceDL.ManageDispatchInvoice(objDispatchInvoice);

                    if (DataValidationLayer.isDataSetNotNull(result))
                    {
                        return new VectorResponse<object>() { ResponseData = result };
                    }
                    else
                    {
                        return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                    }
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to Dispatch Invoice, Please Contact Administrator." } };
                }
            }
        }

        private Boolean SendEmailForDispatch(DispatchInvoice objDispatchInvoice, DataSet dsInvoiceInfo)
        {
            SendEmail objSendEmail = new SendEmail();
            objSendEmail.To = objDispatchInvoice.toEmails;
            objSendEmail.CC = objDispatchInvoice.ccEmails;
            objSendEmail.BCC = objDispatchInvoice.bccEmails;
            objSendEmail.Subject = objDispatchInvoice.subject;
            objSendEmail.Body = objDispatchInvoice.bodyHtml;

            List<Files> objFiles = new List<Files>();

            //if (objDispatchInvoice.isSummaryInvoice != null && objDispatchInvoice.isSummaryInvoice.ToUpper().Equals("SUMMARY"))
            if (objDispatchInvoice.isSummaryInvoice)
            {
                Files ObjvectorFile = new Files();
                ObjvectorFile.fileName = Convert.ToString(dsInvoiceInfo.Tables[VectorConstants.Zero].Rows[0]["SummaryInvoiceName"]);
                ObjvectorFile.filePath = Convert.ToString(dsInvoiceInfo.Tables[VectorConstants.Zero].Rows[0]["SummaryInvoicePath"]);
                ObjvectorFile.fileType = "pdf";
                ObjvectorFile.isFilePathUrl = true;

                objFiles.Add(ObjvectorFile);
            }
            //if (objDispatchInvoice.isVectorInvoice != null && objDispatchInvoice.isVectorInvoice.ToUpper().Equals("VECTOR"))
            if (objDispatchInvoice.isVectorInvoice)
            {
                Files ObjvectorFile = new Files();
                ObjvectorFile.fileName = Convert.ToString(dsInvoiceInfo.Tables[VectorConstants.Zero].Rows[0]["VectorInvoiceName"]);
                ObjvectorFile.filePath = Convert.ToString(dsInvoiceInfo.Tables[VectorConstants.Zero].Rows[0]["VectorInvoicePath"]);
                ObjvectorFile.fileType = "pdf";
                ObjvectorFile.isFilePathUrl = true;

                objFiles.Add(ObjvectorFile);
            }
            // if (objDispatchInvoice.isVendorInvoice != null && objDispatchInvoice.isVendorInvoice.ToUpper().Equals("VENDOR"))
            if (objDispatchInvoice.isVendorInvoice)
            {
                Files ObjvectorFile = new Files();
                ObjvectorFile.fileName = Convert.ToString(dsInvoiceInfo.Tables[VectorConstants.Zero].Rows[0]["VendorInvoiceName"]);
                ObjvectorFile.filePath = Convert.ToString(dsInvoiceInfo.Tables[VectorConstants.Zero].Rows[0]["VendorInvoicePath"]);
                ObjvectorFile.fileType = "pdf";
                ObjvectorFile.isFilePathUrl = true;

                objFiles.Add(ObjvectorFile);
            }

            string defaultFilePath = SecurityManager.GetConfigValue("dispatchDefaultFilePath");
            string defaultFileName = SecurityManager.GetConfigValue("dispatchDefaultFileName");

            if (!string.IsNullOrEmpty(defaultFilePath))
            {
                Files ObjDefaultFile = new Files();
                ObjDefaultFile.fileName = defaultFileName;
                ObjDefaultFile.filePath = defaultFilePath;
                ObjDefaultFile.fileType = "pdf";
                ObjDefaultFile.isFilePathUrl = true;

                objFiles.Add(ObjDefaultFile);
            }

            objSendEmail.EmailAttachments = objFiles;
            //objSendEmail.IsTempFolder = true;

            return EmailManager.SendEmail(objSendEmail, smtpSection: SecurityManager.GetSmtpMailSection("InvoiceMail"), SecurityManager.GetConfigValue("FromEmailInvoice"));
            //return EmailManager.SendEmail(objSendEmail);
        }


        #region Invoice Generation

        public bool GetInvoiceDataForGeneratingRSInvoiceBO(DataSet invoiceData, string fodlerPath, string batchFolderPath, string invType)
        {
            bool resultInfo = true;
            Word.Document aDoc = null;
            object missing = Missing.Value;
            object saveChanges = Word.WdSaveOptions.wdDoNotSaveChanges;
            Word.Application wordApp = new Microsoft.Office.Interop.Word.
                Application();

            try
            {


                string sourceFilePath = fodlerPath + Convert.ToString(invoiceData.Tables[1].Rows[0]["InvoiceTemplatePath"]);
                string destinationFilePath = batchFolderPath + Convert.ToString(invoiceData.Tables[1].Rows[0]["DestinationPath"]);

                string destinationFileName = Convert.ToString(invoiceData.Tables[1].Rows[0]["DestinationFileName"]);

                string finalFilePath = destinationFilePath + "//" + destinationFileName + ".docx";
                string finalOutputFilePath = destinationFilePath + "//" + destinationFileName + ".pdf";



                if (!Directory.Exists(destinationFilePath))
                {
                    Directory.CreateDirectory(destinationFilePath);
                }
                else
                {
                    if (File.Exists(finalOutputFilePath))
                    {
                        File.Delete(finalFilePath);
                        File.Delete(finalOutputFilePath);
                    }

                }

                GC.Collect();

                File.Copy(sourceFilePath, finalFilePath, true);

                //  Microsoft.Office.Interop.Word.HeaderFooter headerFooter;                    
                string pdfFileName = string.Empty;
                try
                {
                    object fileName = (object)(finalFilePath);
                    object readOnly = false;
                    object isVisible = false;
                    wordApp.Visible = false;
                    wordApp.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;
                    aDoc = wordApp.Documents.Open(ref fileName, ref missing,
                    ref readOnly, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref isVisible, ref missing, ref missing,
                    ref missing, ref missing);
                    aDoc.Activate();
                    ReplaceFields(ref invoiceData, aDoc, ref wordApp, invType);



                    aDoc.Save();

                    object outputFileName = pdfFileName = finalOutputFilePath;
                    object fileFormat = Word.WdSaveFormat.wdFormatPDF;


                    aDoc.SaveAs(ref outputFileName,
                     ref fileFormat, ref missing, ref missing,
                     ref missing, ref missing, ref missing, ref missing,
                     ref missing, ref missing, ref missing, ref missing,
                     ref missing, ref missing, ref missing, ref missing);

                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    aDoc.Close(true);
                    //   aDoc.Close(ref saveChanges, ref missing, ref missing);
                    aDoc = null;
                    wordApp.Quit(true);
                    // wordApp.Quit(ref missing, ref missing, ref missing);
                    Marshal.ReleaseComObject(wordApp);

                    if (File.Exists(finalFilePath))
                    {
                        File.Delete(finalFilePath);
                    }

                }
                try
                {

                    if (invType.Equals("Vendor"))
                    {


                        string originalINvoicePath = invoiceData.Tables[1].Rows[0]["OriginalInvoiceDictionaryPath"].ToString();
                        bool isTimeStampRequired = (from c in invoiceData.Tables[1].AsEnumerable()
                                                    select c.Field<bool>("isTimeStampRequired")).FirstOrDefault();


                        GenerateHaulerOrginal(pdfFileName, originalINvoicePath, ref invoiceData, isTimeStampRequired);
                        //  File.Copy(pdfFileName, RSInvoiceTagetPath + "//" + InvoiceFileName + ".pdf", true);

                        // AddWaterMarkOnOriginal(pdfFileName, originalINvoicePath, invoiceData, isTimeStampRequired, VendorInvoiceWithOIStamp);
                        // ReplaceFile(VendorInvoiceWithOIStamp, originalINvoicePath, backfinalOutputFilePath);
                        // MergePDF(VendorInvoiceWithOIStamp, originalINvoicePath, finalOutputFilePath);
                    }
                }
                catch (IOException ex)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {

                aDoc.Close(true);
                aDoc = null;
                wordApp.Quit(true);
                //wordApp.Quit(ref missing, ref missing, ref missing);
                Marshal.ReleaseComObject(wordApp);
                string expValue = ex.Message;
                return true;
            }


            //delete files with .docx/Any temperory files we have
        }




        private void ReplaceFields(ref DataSet invoiceData, Word.Document aDoc, ref Word.Application wordApp, string invType)
        {
            // Gets a NumberFormatInfo associated with the en-US culture.
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

            var headerFields = (from c in invoiceData.Tables[0].AsEnumerable()
                                where c.Field<string>("FieldType").Equals("Header")
                                select new
                                {
                                    ColumnName = c.Field<string>("FieldValue"),
                                    TagName = c.Field<string>("FieldName")
                                }).ToList();

            var footerFields = (from c in invoiceData.Tables[0].AsEnumerable()
                                where c.Field<string>("FieldType").Equals("Footer")
                                select new
                                {
                                    ColumnName = c.Field<string>("FieldValue"),
                                    TagName = c.Field<string>("FieldName")
                                }).ToList();


            //var summaryIN = (from c in invoiceData.Tables[1].AsEnumerable() 
            //                         select c.Field<string>("VendorInvoicePath")).FirstOrDefault();

            var VectorInvoicePath = (from c in invoiceData.Tables[1].AsEnumerable()
                                     select c.Field<string>("VectorInvoicePath")).FirstOrDefault();

            var VendorInvoicePath = (from c in invoiceData.Tables[1].AsEnumerable()
                                     select c.Field<string>("VendorInvoicePath")).FirstOrDefault();




            //First Replace header Fields
            foreach (var field in headerFields)
            {
                if (invoiceData.Tables[1].Columns.Contains(field.ColumnName))
                    this.FindAndReplaceText(wordApp, field.TagName, Convert.ToString(invoiceData.Tables[1].Rows[0][field.ColumnName]));

            }

            // BindFooter(ref aDoc, ref invoiceData, footerFields);

            object missing = Missing.Value;
            object saveChanges = Word.WdSaveOptions.wdDoNotSaveChanges;


            object replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
            foreach (Microsoft.Office.Interop.Word.Section section in aDoc.Sections)
            {
                Microsoft.Office.Interop.Word.Range footerRange = section.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                foreach (var field in headerFields)
                {
                    if (invoiceData.Tables[1].Columns.Contains(field.ColumnName))
                    {
                        footerRange.Find.Text = field.TagName;
                        footerRange.Find.Replacement.Text = Convert.ToString(invoiceData.Tables[1].Rows[0][field.ColumnName]);
                        footerRange.Find.Execute(ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceAll, ref missing, ref missing, ref missing, ref missing);
                    }
                }
            }


            foreach (Word.Hyperlink hyperlink in aDoc.Hyperlinks)
            {
                if (hyperlink.Address.Contains(@"hauler"))
                    hyperlink.Address = VendorInvoicePath;
                else if (hyperlink.Address.Contains(@"saving"))
                    hyperlink.Address = VectorInvoicePath;
                else if (hyperlink.Address.Contains(@"BillPaySavings"))
                {
                    hyperlink.Address = VectorInvoicePath;
                    this.FindAndReplaceText(wordApp, "<ClickHereToRedirect>", "Click  here to see additional invoice detail");
                }


            }

            if (invoiceData.Tables.Count > 2)
                this.FindAndReplaceServiceLevelItems(wordApp, "<InvoiceSummarySavingData>", ref invoiceData, invType);
        }

        private void FindAndReplaceText(Word.Application WordApp, object findText, object replaceWithText)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object nmatchAllWordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            //object replace = 2;
            object wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
            object replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
            WordApp.Selection.Range.HighlightColorIndex = Word.WdColorIndex.wdDarkRed;
            Word.Range rng = WordApp.Selection.Range;
            rng.Font.Color = Word.WdColor.wdColorBlue;

            WordApp.Selection.Find.Execute(
            ref findText,
            ref matchCase, ref matchWholeWord,
            ref matchWildCards, ref matchSoundsLike,
            ref nmatchAllWordForms, ref forward,
            ref wrap, ref format, ref replaceWithText,
            ref replaceAll, ref matchKashida,
            ref matchDiacritics, ref matchAlefHamza,
            ref matchControl);
        }
        private void FindAndReplaceServiceLevelItems(Word.Application WordApp, object findText, ref DataSet InvoiceData, string invType)
        {
            object missing = System.Reflection.Missing.Value;
            object autoFit = WdAutoFitBehavior.wdAutoFitWindow;
            WordApp.Application.Selection.Find.ClearFormatting();
            WordApp.Application.Selection.Find.Text = (string)findText;
            WordApp.Application.Selection.Find.Execute(
            ref findText, ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing, ref missing, ref missing);
            Word.Range SelectionRange = WordApp.Application.Selection.Range;

            int rowCount = InvoiceData.Tables[2].Rows.Count + 2;
            int colCount = InvoiceData.Tables[2].Columns.Count;
            Word.Table newTable = null;
            newTable = WordApp.Application.Selection.Tables.Add(SelectionRange, 1, colCount, ref missing, ref autoFit);

            newTable.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone;
            newTable.Rows.Alignment = WdRowAlignment.wdAlignRowLeft;

            if (string.Equals(invType, "Summary"))
                newTable.Cell(newTable.Rows.Count, 1).SetWidth(375f, Word.WdRulerStyle.wdAdjustProportional);
            else if (string.Equals(invType, "Vector"))
                newTable.Cell(newTable.Rows.Count, 1).SetWidth(153f, Word.WdRulerStyle.wdAdjustProportional);
            else if (string.Equals(invType, "Vendor"))
                newTable.Cell(newTable.Rows.Count, 1).SetWidth(275f, Word.WdRulerStyle.wdAdjustProportional);
            else if (string.Equals(invType, "Monthly"))
                newTable.Cell(newTable.Rows.Count, 1).SetWidth(275f, Word.WdRulerStyle.wdAdjustProportional);


            //newTable.Cell(newTable.Rows.Count, 2).SetWidth(53f, Word.WdRulerStyle.wdAdjustProportional);
            //newTable.Cell(newTable.Rows.Count, 3).SetWidth(53f, Word.WdRulerStyle.wdAdjustProportional);

            //if (string.Equals(invType, "Vendor"))
            //    newTable.Cell(newTable.Rows.Count, 6).SetWidth(58f, Word.WdRulerStyle.wdAdjustProportional);


            newTable.AllowAutoFit = true;
            int totalColumnsCount = InvoiceData.Tables[2].Columns.Count;

            GenerateTableHeader(rowCount, newTable, ref InvoiceData, invType);

            int counter = 0;
            foreach (DataRow row in InvoiceData.Tables[2].Rows)
            {
                newTable.Rows.Add();
                int columnCounter = 1;
                foreach (DataColumn columnName in InvoiceData.Tables[2].Columns)
                {
                    if(totalColumnsCount > 5) 
                    newTable.Cell(rowCount, columnCounter).Range.Font.Size = 7;
                    else 
                        newTable.Cell(rowCount, columnCounter).Range.Font.Size = 9;


                    newTable.Cell(rowCount, columnCounter).Range.Text = row[columnName.ColumnName].ToString().Replace("\t", "");
                    newTable.Cell(rowCount, columnCounter).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    newTable.Cell(rowCount, columnCounter).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;


                    if (columnName.ColumnName.Contains("Desc"))
                    {
                        newTable.Cell(rowCount, columnCounter).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    }


                    if (counter == 0)
                    {
                        for (int j = 1; j <= totalColumnsCount; j++)
                        {
                            newTable.Cell(rowCount, j).Range.Bold = 0;

                            if (totalColumnsCount > 5)
                                newTable.Cell(rowCount, j).Range.Font.Size = 7;
                            else
                                newTable.Cell(rowCount, j).Range.Font.Size = 9;


                            newTable.Cell(rowCount, j).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                            if (string.Equals(invType, "Monthly"))
                            {
                                if (j > 1)
                                    newTable.Cell(rowCount, j).SetWidth(60f, Word.WdRulerStyle.wdAdjustProportional);
                            }
                            newTable.Cell(rowCount, j).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        }
                    }

                    GenerateAlternateRowStyle(ref newTable, counter, totalColumnsCount);
                    // newTable.Cell(rowCount, columnCounter).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    counter++;
                    columnCounter++;
                }
            }
            SetBorderForFirstLine(ref newTable);
            //    GenerateAlternateRowStyle(ref newTable, counter, 3);
        }



        private static void GenerateAlternateRowStyle(ref Word.Table newTable, int counter, int size)
        {
            for (int j = 1; j <= size; j++)
            {
                if (counter % 2 == 0)
                {
                    newTable.Cell(newTable.Rows.Count, j).Range.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray05;
                }
                else
                {
                    newTable.Cell(newTable.Rows.Count, j).Range.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorWhite;
                }
            }
        }

        private static void GenerateTableHeader(int i, Word.Table newTable, ref DataSet InvoiceData, string invType)
        {
            int columnCounter = 0;
            int colCount = 0;
            foreach (DataColumn columnName in InvoiceData.Tables[2].Columns)
            {

                colCount = InvoiceData.Tables[2].Columns.Count;

                columnCounter++;
                newTable.Cell(i, columnCounter).Range.Text = columnName.ColumnName;
                newTable.Cell(i, columnCounter).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                newTable.Cell(i, columnCounter).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

                if (columnName.ColumnName.Contains("Desc") && colCount > 5)
                {
                    newTable.Cell(i, columnCounter).SetWidth(170f, Word.WdRulerStyle.wdAdjustProportional);
                    newTable.Cell(i, columnCounter).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

                }
                else if (columnName.ColumnName.Contains("Desc"))
                {
                    newTable.Cell(i, columnCounter).SetWidth(185f, Word.WdRulerStyle.wdAdjustProportional);
                    newTable.Cell(i, columnCounter).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

                }
                else if (columnName.ColumnName.Contains("Approved ($)"))
                {
                    newTable.Cell(i, columnCounter).SetWidth(53f, Word.WdRulerStyle.wdAdjustProportional);
                }


            }
            for (int j = 1; j <= columnCounter; j++)
            {
                newTable.Cell(i, j).Range.Bold = 1;

                if (colCount > 5)
                    newTable.Cell(i, j).Range.Font.Size = 8;
                else
                    newTable.Cell(i, j).Range.Font.Size = 9;




                if (string.Equals(invType, "Monthly"))
                {
                    if (j > 1)
                        newTable.Cell(i, j).SetWidth(50f, Word.WdRulerStyle.wdAdjustProportional);
                }

                // newTable.Cell(i, j).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            }
        }


        private static void SetBorderForFirstLine(ref Word.Table newTable)
        {
            int columnCount = newTable.Columns.Count;
            int rowCount = newTable.Rows.Count;

            for (int i = 0; i <= columnCount; i++)
            {
                newTable.Cell(1, i).Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
                newTable.Cell(1, i).Borders.OutsideLineWidth = WdLineWidth.wdLineWidth100pt;
                newTable.Cell(1, i).Borders.OutsideColor = WdColor.wdColorBlack;
                newTable.Cell(1, i).Borders[Word.WdBorderType.wdBorderLeft].LineStyle = Word.WdLineStyle.wdLineStyleNone;
                newTable.Cell(1, i).Borders[Word.WdBorderType.wdBorderRight].LineStyle = Word.WdLineStyle.wdLineStyleNone;

                newTable.Cell(rowCount, i).Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
                newTable.Cell(rowCount, i).Borders.OutsideLineWidth = WdLineWidth.wdLineWidth100pt;
                newTable.Cell(rowCount, i).Borders.OutsideColor = WdColor.wdColorBlack;
                newTable.Cell(rowCount, i).Borders[Word.WdBorderType.wdBorderLeft].LineStyle = Word.WdLineStyle.wdLineStyleNone;
                newTable.Cell(rowCount, i).Borders[Word.WdBorderType.wdBorderRight].LineStyle = Word.WdLineStyle.wdLineStyleNone;
            }
        }

        #endregion

        #region old

        private void GenerateHaulerOrginal(String HaulerAuditedPath, String HaulerOrginal, ref DataSet invoiceData, bool IsStampRequired)
        {
            if (File.Exists(HaulerOrginal))
            {
                AddWaterMarkOnOriginal(HaulerOrginal, invoiceData, IsStampRequired);
                using (var output = new MemoryStream())
                {
                    var document = new iTextSharp.text.Document();
                    var writer = new PdfCopy(document, output);
                    document.Open();
                    int count = 0;
                    foreach (var file in new[] { HaulerAuditedPath, HaulerOrginal })
                    {
                        var reader = new PdfReader(RemoveWaterMark(file, "HAULER CURRENT AMOUNT DUE"));
                        // PdfReader reader = new PdfReader(new RandomAccessFileOrArray(file, true), null);
                        int n = reader.NumberOfPages;
                        PdfImportedPage page;
                        for (int p = 1; p <= n; p++)
                        {
                            page = writer.GetImportedPage(reader, p);
                            if (count == 1 && p == 1)
                                if (IsStampRequired)
                                {
                                    AddWaterMark(ref page, ref writer, ref invoiceData);
                                }
                            writer.AddPage(page);
                        }
                        reader.Close();

                        count++;
                    }

                    writer.CompressionLevel = PdfStream.BEST_COMPRESSION;
                    document.Close();
                    writer.SetFullCompression();
                    File.WriteAllBytes(HaulerAuditedPath, output.ToArray());
                }
            }
        }

        private void AddWaterMarkOnOriginal(string HaulerOrginal, DataSet invoiceData, bool IsStampRequired)
        {
            using (var output = new MemoryStream())
            {
                var document = new iTextSharp.text.Document();
                var writer = new PdfCopy(document, output);
                document.Open();
                int count = 0;
                foreach (var file in new[] { HaulerOrginal })
                {
                    // var reader = new PdfReader(); 
                    PdfReader reader = new PdfReader(RemoveWaterMark(file, "HAULER CURRENT AMOUNT DUE"));
                    int n = reader.NumberOfPages;
                    PdfImportedPage page;
                    for (int p = 1; p <= n; p++)
                    {
                        //WaterMark(reader.GetPageN(p), "REFUSE SPECIALISTS CURRENT AMOUNT DUE");
                        page = writer.GetImportedPage(reader, p);
                        if (count == 0 && p == 1)
                            if (IsStampRequired)
                            {
                                AddWaterMark(ref page, ref writer, ref invoiceData);
                            }
                        writer.AddPage(page);
                    }
                    reader.Close();

                    count++;
                }
                writer.CompressionLevel = PdfStream.BEST_COMPRESSION;

                document.Close();
                writer.SetFullCompression();
                File.WriteAllBytes(HaulerOrginal, output.ToArray());
            }
        }

        private PdfReader RemoveWaterMark(string fileName, string waterMarkText)
        {
            PdfReader reader = new PdfReader(new RandomAccessFileOrArray(fileName, true), null);


            reader.RemoveUnusedObjects();

            //Placeholder variables
            PRStream stream;
            String content;
            PdfDictionary page;
            PdfArray contentarray;

            //Get the page count
            int pageCount2 = reader.NumberOfPages;
            //Loop through each page
            for (int i = 1; i <= pageCount2; i++)
            {
                //Get the page
                page = reader.GetPageN(i);
                //Get the raw content
                contentarray = page.GetAsArray(PdfName.CONTENTS);
                if (contentarray != null)
                {
                    //Loop through content
                    for (int j = 0; j < contentarray.Size; j++)
                    {
                        //Get the raw byte stream
                        stream = (PRStream)contentarray.GetAsStream(j);
                        //Convert to a string. NOTE, you might need a different encoding here
                        content = System.Text.Encoding.ASCII.GetString(PdfReader.GetStreamBytes(stream));
                        //Look for the OCG token in the stream as well as our watermarked text
                        if (content.IndexOf(waterMarkText) >= 0)
                        {
                            //Remove it by giving it zero length and zero data
                            stream.Put(PdfName.LENGTH, new PdfNumber(0));
                            stream.SetData(new byte[0]);
                        }
                    }
                }
            }

            return reader;
        }

        private void AddWaterMark(ref PdfImportedPage page, ref PdfCopy writer, ref DataSet invoiceData)
        {
            PdfCopy.PageStamp stamp = writer.CreatePageStamp(page);
            PdfContentByte pdfPageContents = stamp.GetOverContent();
            pdfPageContents.BeginText();
            BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false);
            pdfPageContents.SetFontAndSize(baseFont, 12);
            // pdfPageContents.SetRGBColorFill(213, 213, 213);
            pdfPageContents.SetRGBColorFill(197, 0, 0);
            float xPosition = (page.Width / 2) + 50;
            pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "APPROVED", xPosition, page.Height / 10, 0f);
            pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "HAULER CURRENT AMOUNT DUE $ " + string.Format("{0:C2}", Convert.ToDouble(invoiceData.Tables[1].Rows[0]["HaulerCurrentAmountDue"].ToString())), xPosition, (page.Height / 10) - 15, 0f);
            pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "VECTOR97 CURRENT AMOUNT DUE $ " + string.Format("{0:C2}", Convert.ToDouble(invoiceData.Tables[1].Rows[0]["RsCurrentAmountDue"].ToString())), xPosition, (page.Height / 10) - 30, 0f);
            pdfPageContents.EndText();
            stamp.AlterContents();
        }

        #endregion

        //#region  NEw


        //private void AddWaterMarkOnOriginal(string HaulerAuditedInvoice, string HaulerOrginal, DataSet invoiceData, bool IsStampRequired, string finalInvoiceFile)
        //{
        //    using (var output = new MemoryStream())
        //    {
        //        var document = new iTextSharp.text.Document();
        //        var writer = new PdfCopy(document, output);
        //        document.Open();
        //        int count = 0;
        //        foreach (var file in new[] { HaulerOrginal })
        //        {
        //            // var reader = new PdfReader(RemoveWaterMark(new PdfReader(file), "HAULER CURRENT AMOUNT DUE"));
        //            var reader = new PdfReader(new RandomAccessFileOrArray(file, true), null);
        //            int n = reader.NumberOfPages;
        //            PdfImportedPage page;
        //            for (int p = 1; p <= n; p++)
        //            {
        //                page = writer.GetImportedPage(reader, p);
        //                if (IsStampRequired)
        //                {
        //                    AddWaterMark(ref page, ref writer, ref invoiceData);
        //                }

        //                writer.AddPage(page);
        //                page = writer.GetImportedPage(reader, p);

        //            }
        //            reader.Close();
        //            reader.Dispose();

        //            count++;
        //        }

        //        writer.CompressionLevel = PdfStream.BEST_COMPRESSION;

        //        document.Close();

        //        writer.SetFullCompression();
        //        File.WriteAllBytes(finalInvoiceFile, output.ToArray());
        //        writer.Close();
        //        writer.Dispose();
        //        output.Close();
        //        output.Dispose();
        //        document.Dispose();
        //    }
        //}

        //public void ReplaceFile(string fileToMoveAndDelete, string fileToReplace, string backupOfFileToReplace)
        //{
        //    // Create a new FileInfo object.    
        //    FileInfo fInfo = new FileInfo(fileToMoveAndDelete);

        //    // replace the file.    
        //    fInfo.Replace(fileToReplace, backupOfFileToReplace, false);
        //}

        //private PdfReader RemoveWaterMark(PdfReader reader, string waterMarkText)
        //{
        //    reader.RemoveUnusedObjects();

        //    //Placeholder variables
        //    PRStream stream;
        //    String content;
        //    PdfDictionary page;
        //    PdfArray contentarray;

        //    //Get the page count
        //    int pageCount2 = reader.NumberOfPages;
        //    //Loop through each page
        //    for (int i = 1; i <= pageCount2; i++)
        //    {
        //        //Get the page
        //        page = reader.GetPageN(i);
        //        //Get the raw content
        //        contentarray = page.GetAsArray(PdfName.CONTENTS);
        //        if (contentarray != null)
        //        {
        //            //Loop through content
        //            for (int j = 0; j < contentarray.Size; j++)
        //            {
        //                //Get the raw byte stream
        //                stream = (PRStream)contentarray.GetAsStream(j);
        //                //Convert to a string. NOTE, you might need a different encoding here
        //                content = System.Text.Encoding.ASCII.GetString(PdfReader.GetStreamBytes(stream));
        //                //Look for the OCG token in the stream as well as our watermarked text
        //                if (content.IndexOf(waterMarkText) >= 0)
        //                {
        //                    //Remove it by giving it zero length and zero data
        //                    stream.Put(PdfName.LENGTH, new PdfNumber(0));
        //                    stream.SetData(new byte[0]);
        //                }
        //            }
        //        }
        //    }

        //    return reader;
        //}

        //private void AddWaterMark(ref PdfImportedPage page, ref PdfCopy writer, ref DataSet invoiceData)
        //{
        //    PdfCopy.PageStamp stamp = writer.CreatePageStamp(page);
        //    PdfContentByte pdfPageContents = stamp.GetOverContent();
        //    pdfPageContents.BeginText();
        //    BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false);
        //    pdfPageContents.SetFontAndSize(baseFont, 12);
        //    // pdfPageContents.SetRGBColorFill(213, 213, 213);
        //    pdfPageContents.SetRGBColorFill(197, 0, 0);
        //    float xPosition = (300) + 50;
        //    pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "APPROVED $ ", xPosition, page.Height / 10, 0f);
        //    pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "HAULER CURRENT AMOUNT DUE $ " + string.Format("{0:C2}", Convert.ToDouble(invoiceData.Tables[1].Rows[0]["HaulerCurrentAmountDue"].ToString())), xPosition, (page.Height / 10) - 15, 0f);
        //    pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "REFUSE SPECIALISTS CURRENT AMOUNT DUE $ " + string.Format("{0:C2}", Convert.ToDouble(invoiceData.Tables[1].Rows[0]["RsCurrentAmountDue"].ToString())), xPosition, (page.Height / 10) - 30, 0f);


        //    pdfPageContents.EndText();
        //    stamp.AlterContents();
        //}


        //private void MergePDF(string File1, string File2, string destinationFolderPath)
        //{
        //    string[] fileArray = new string[3];
        //    fileArray[0] = File1;
        //    fileArray[1] = File2;

        //    PdfReader reader = null;
        //    iTextSharp.text.Document sourceDocument = null;
        //    PdfCopy pdfCopyProvider = null;
        //    PdfImportedPage importedPage;
        //    string outputPdfPath = destinationFolderPath;

        //    sourceDocument = new iTextSharp.text.Document();
        //    pdfCopyProvider = new PdfCopy(sourceDocument, new System.IO.FileStream(outputPdfPath, System.IO.FileMode.Create));

        //    //output file Open  
        //    sourceDocument.Open();


        //    //files list wise Loop  
        //    for (int f = 0; f < fileArray.Length - 1; f++)
        //    {
        //        int pages = TotalPageCount(fileArray[f].ToString());

        //        reader = new PdfReader(fileArray[f]);
        //        //Add pages in new file  
        //        for (int i = 1; i <= pages; i++)
        //        {
        //            importedPage = pdfCopyProvider.GetImportedPage(reader, i);

        //            pdfCopyProvider.AddPage(importedPage);
        //        }

        //        reader.Close();
        //    }
        //    //save the output file  
        //    sourceDocument.Close();
        //}

        //public int TotalPageCount(string file)
        //{
        //    using (StreamReader sr = new StreamReader(System.IO.File.OpenRead(file)))
        //    {
        //        Regex regex = new Regex(@"/Type\s*/Page[^s]");
        //        MatchCollection matches = regex.Matches(sr.ReadToEnd());

        //        return matches.Count;
        //    }
        //}


        //#endregion

        public VectorResponse<object> GetVendorPastDueInfo(Int64 invoiceId, Int64 vendorPastDueInfoId, Int64 taskId)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.GetVendorPastDueInfo(invoiceId, vendorPastDueInfoId, taskId);
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

        public VectorResponse<object> ManageVendorPastDueInfo(VendorPastDue objVendorPastDue)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.ManageVendorPastDueInfo(objVendorPastDue);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to Add/Update info, Please Contact Administrator." } };
                }
            }
        }

        public VectorResponse<object> GetVendorPendingCreditsInfo(Int64 invoiceId, Int64 vendorPendingCreditsInfoId, Int64 taskId)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.GetVendorPendingCreditsInfo(invoiceId, vendorPendingCreditsInfoId, taskId);
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

        public VectorResponse<object> ManageVendorPendingCreditsInfo(VendorPendingCredits objVendorPendingCredits)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.ManageVendorPendingCreditsInfo(objVendorPendingCredits);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to Add/Update info, Please Contact Administrator." } };
                }
            }
        }

        public VectorResponse<object> ManageVendorEmailDataInfo(VendorEmailData objVendorEmailData)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.ManageVendorEmailDataInfo(objVendorEmailData);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to Add/Update info, Please Contact Administrator." } };
                }
            }
        }

        public VectorResponse<object> AddLineitemToInvoice(LineItems objLineItems, Int64 userId)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.AddLineitemToInvoice(objLineItems, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to Add/Update info, Please Contact Administrator." } };
                }
            }
        }


        public VectorResponse<object> ManageBillGapComments(BillGapComments objBillGapComments, Int64 userId)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.ManageBillGapComments(objBillGapComments, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to Add/Update info, Please Contact Administrator." } };
                }
            }
        }



        public VectorResponse<object> UploadMissingInvoice(MissingInvoice objMissingInvoice, Int64 userId)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.UploadMissingInvoice(objMissingInvoice, userId);


                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    var resultInfo = (from c in result.Tables[0].AsEnumerable()
                                      select new
                                      {
                                          result = c.Field<bool>("Result"),
                                          resultMessage = c.Field<string>("ResultMessage"),
                                          DestinationFilePath = c.Field<string>("DestinationFilePath"),
                                          DestinationFolder = c.Field<string>("DestinationFolder"),
                                          DestinationFileName = c.Field<string>("DestinationFileName")
                                      }).FirstOrDefault();


                    if (resultInfo.result)
                    {
                        string sourceFilePath = SecurityManager.GetConfigValue("FileServerTempPath") + objMissingInvoice.TempFolderName + "\\" + objMissingInvoice.Documents[0].fileName;
                        string desitinationFilePath = SecurityManager.GetConfigValue("FileServerPath") + resultInfo.DestinationFolder;

                        if (!Directory.Exists(resultInfo.DestinationFolder))
                        {
                            Directory.CreateDirectory(desitinationFilePath);
                        }

                        if (File.Exists(sourceFilePath))
                        {
                            File.Move(sourceFilePath, desitinationFilePath + "\\" + resultInfo.DestinationFileName);
                        }
                    }

                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to Upload Invoice, Please Contact IT Support." } };
                }
            }
        }


        public VectorResponse<object> GeneratePlaceHolderInvoice(Placeholder objMissingInvoice, Int64 userId, string folderPath, string batchFolderPath)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.GeneratePlaceHolderInvoice(objMissingInvoice, userId);


                if (DataValidationLayer.isDataSetNotNull(result))
                {

                    if (objMissingInvoice.Action.ToUpper().Equals("APPROVE"))
                    {
                        var resultInfo = (from c in result.Tables[0].AsEnumerable()
                                          select new
                                          {
                                              result = c.Field<bool>("Result"),
                                              resultMessage = c.Field<string>("ResultMessage"),
                                              DestinationFilePath = c.Field<string>("DestinationFilePath"),
                                              DestinationFolder = c.Field<string>("DestinationFolder"),
                                              FileName = c.Field<string>("FileName"),
                                              InvoiceId = c.Field<Int64>("InvoiceId"),
                                          }).FirstOrDefault();


                        if (resultInfo.result)
                        {
                            // bool isFileGenerated = false;
                            var invoiceData = objInvoiceDL.GetInvoiceDetailsForInvoiceGeneration("PlaceholderInvoice", resultInfo.InvoiceId);
                            //  string invGenREsult = GeneratePlaceHolderInvoiceFromHtml(resultInfo.InvoiceId, objMissingInvoice, folderPath, batchFolderPath);
                            bool invoiceResult = GetInvoiceDataForGeneratingRSInvoiceBO(invoiceData, folderPath, batchFolderPath, "PlaceholderInvoice");

                        }
                    }

                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to generate placeholder invoice, Please Contact IT Support." } };
                }
            }
        }




        private string GeneratePlaceHolderInvoiceFromHtml(Int64 invoiceId, Placeholder objMissingInvoice, string folderPath, string batchFolderPath)
        {
            string Result = "";
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var invoiceData = objInvoiceDL.GetInvoiceDetailsForInvoiceGeneration("PlaceholderInvoice", invoiceId);

                bool invoiceResult = GenerateInvoiceFromHTML(invoiceData, folderPath, batchFolderPath);

                if (invoiceResult)
                {
                    Result = "PH Invoice Generated Successfully.";
                }
                else
                {
                    Result = "Unable to Generated PH Invoice, Please Contact Support.";
                }
            }

            return Result;
        }

        public VectorResponse<object> DeleteInvoiceLineitem(InvoiceLineitemInfo objInvoiceLineitemInfo, Int64 userId)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.DeleteInvoiceLineitem(objInvoiceLineitemInfo, userId);


                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to Delete Lineitem, Please Contact IT Support." } };
                }
            }
        }

        public bool GenerateInvoiceFromHTML(DataSet invoiceData, string fodlerPath, string batchFolderPath)
        {
            bool resultInfo = true;

            try
            {
                string sourceFilePathHtml = fodlerPath + Convert.ToString(invoiceData.Tables[1].Rows[0]["InvoiceTemplatePath"]);
                string sourceFilePathCSS = fodlerPath + Convert.ToString(invoiceData.Tables[1].Rows[0]["InvoiceCSSTemplatePath"]);
                string destinationFilePath = batchFolderPath + Convert.ToString(invoiceData.Tables[1].Rows[0]["DestinationPath"]);

                string destinationFileName = Convert.ToString(invoiceData.Tables[1].Rows[0]["DestinationFileName"]);

                string finalFilePath = destinationFilePath + "//" + destinationFileName + ".docx";
                string finalOutputFilePath = destinationFilePath + "//" + destinationFileName + ".pdf";

                if (!Directory.Exists(destinationFilePath))
                {
                    Directory.CreateDirectory(destinationFilePath);
                }
                else
                {
                    if (File.Exists(finalOutputFilePath))
                    {
                        File.Delete(finalFilePath);
                        File.Delete(finalOutputFilePath);
                    }

                }

                //  Microsoft.Office.Interop.Word.HeaderFooter headerFooter;                    
                string pdfFileName = string.Empty;
                try
                {

                    Byte[] bytes;
                    using (var ms = new MemoryStream())
                    {
                        using (var doc = new iTextSharp.text.Document())
                        {
                            using (var writer = PdfWriter.GetInstance(doc, ms))
                            {
                                doc.Open();
                                var html = File.ReadAllText(sourceFilePathHtml);
                                var cssFilePath = File.ReadAllText(sourceFilePathCSS);

                                string finalResult = ReplaceHtmlTextWithData(html, invoiceData);

                                using (var fileCSS = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(sourceFilePathCSS)))
                                {
                                    using (var fileHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(finalResult)))
                                    {
                                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, fileHtml, fileCSS);
                                    }
                                }
                                doc.Close();
                            }

                        }

                        bytes = ms.ToArray();
                    }

                    System.IO.File.WriteAllBytes(finalOutputFilePath, bytes);


                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    if (File.Exists(finalFilePath))
                    {
                        File.Delete(finalFilePath);
                    }
                }
                try
                {

                    //if (invType.Equals("Vendor"))
                    //{


                    //    string originalINvoicePath = invoiceData.Tables[1].Rows[0]["OriginalInvoiceDictionaryPath"].ToString();
                    //    bool isTimeStampRequired = (from c in invoiceData.Tables[1].AsEnumerable()
                    //                                select c.Field<bool>("isTimeStampRequired")).FirstOrDefault();


                    //    GenerateHaulerOrginal(pdfFileName, originalINvoicePath, ref invoiceData, isTimeStampRequired);
                    //    //  File.Copy(pdfFileName, RSInvoiceTagetPath + "//" + InvoiceFileName + ".pdf", true);

                    //    // AddWaterMarkOnOriginal(pdfFileName, originalINvoicePath, invoiceData, isTimeStampRequired, VendorInvoiceWithOIStamp);
                    //    // ReplaceFile(VendorInvoiceWithOIStamp, originalINvoicePath, backfinalOutputFilePath);
                    //    // MergePDF(VendorInvoiceWithOIStamp, originalINvoicePath, finalOutputFilePath);
                    //}
                }
                catch (IOException ex)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                string expValue = ex.Message;
                return true;
            }
            //delete files with .docx/Any temperory files we have
        }

        private string ReplaceHtmlTextWithData(string html, DataSet invoiceData)
        {
            // Gets a NumberFormatInfo associated with the en-US culture.
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

            var headerFields = (from c in invoiceData.Tables[0].AsEnumerable()
                                where c.Field<string>("FieldType").Equals("Header")
                                select new
                                {
                                    ColumnName = c.Field<string>("FieldValue"),
                                    TagName = c.Field<string>("FieldName")
                                }).ToList();

            var footerFields = (from c in invoiceData.Tables[0].AsEnumerable()
                                where c.Field<string>("FieldType").Equals("Footer")
                                select new
                                {
                                    ColumnName = c.Field<string>("FieldValue"),
                                    TagName = c.Field<string>("FieldName")
                                }).ToList();


            //var summaryIN = (from c in invoiceData.Tables[1].AsEnumerable() 
            //                         select c.Field<string>("VendorInvoicePath")).FirstOrDefault();

            var VectorInvoicePath = (from c in invoiceData.Tables[1].AsEnumerable()
                                     select c.Field<string>("VectorInvoicePath")).FirstOrDefault();

            var VendorInvoicePath = (from c in invoiceData.Tables[1].AsEnumerable()
                                     select c.Field<string>("VendorInvoicePath")).FirstOrDefault();




            //First Replace header Fields
            foreach (var field in headerFields)
            {
                if (invoiceData.Tables[1].Columns.Contains(field.ColumnName))
                    html = html.Replace(field.TagName, Convert.ToString(invoiceData.Tables[1].Rows[0][field.ColumnName]));
            }

            //foreach (Microsoft.Office.Interop.Word.Section section in aDoc.Sections)
            //{
            //    Microsoft.Office.Interop.Word.Range footerRange = section.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
            //    foreach (var field in headerFields)
            //    {
            //        if (invoiceData.Tables[1].Columns.Contains(field.ColumnName))
            //        {
            //            footerRange.Find.Text = field.TagName;
            //            footerRange.Find.Replacement.Text = Convert.ToString(invoiceData.Tables[1].Rows[0][field.ColumnName]);
            //            footerRange.Find.Execute(ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceAll, ref missing, ref missing, ref missing, ref missing);
            //        }
            //    }
            //} 

            //foreach (Word.Hyperlink hyperlink in aDoc.Hyperlinks)
            //{
            //    if (hyperlink.Address.Contains(@"hauler"))
            //        hyperlink.Address = VendorInvoicePath;
            //    else if (hyperlink.Address.Contains(@"saving"))
            //        hyperlink.Address = VectorInvoicePath;
            //    else if (hyperlink.Address.Contains(@"BillPaySavings"))
            //    {
            //        hyperlink.Address = VectorInvoicePath;
            //        this.FindAndReplaceText(wordApp, "<ClickHereToRedirect>", "Click  here to see additional invoice detail");
            //    }


            //}

            if (invoiceData.Tables.Count > 2)
                html = html.Replace("[ServiceLineItems]", GetServiceLevelItemsData(invoiceData));

            return html;
        }

        private string GetServiceLevelItemsData(DataSet InvoiceData)
        {
            string finalTable = "<thead class='card - header'><tr>";

            foreach (DataColumn columnName in InvoiceData.Tables[2].Columns)
            {
                finalTable = finalTable + "<td><strong>" + columnName.ColumnName + "</strong></td>";
            }

            finalTable = finalTable + "</tr></thead><tbody>";

            foreach (DataRow row in InvoiceData.Tables[2].Rows)
            {
                finalTable = finalTable + "<tr>";
                foreach (DataColumn columnName in InvoiceData.Tables[2].Columns)
                {
                    finalTable = finalTable + "<td>" + row[columnName.ColumnName].ToString() + "</td>";
                }
                finalTable = finalTable + "</tr>";
            }

            finalTable = finalTable + "</tbody>";




            //Foolter
            if (InvoiceData.Tables.Count > 3)
            {
                finalTable = finalTable + "<tfoot class='card - footer'>";
                //< tr >
                //               < td colspan = "4" class="text-end"><strong>Pay This Amount:</strong></td>
                //               <td class="text-end">$2150.00</td>
                //           </tr>
                //       </tfoot>
                foreach (DataRow row in InvoiceData.Tables[3].Rows)
                {
                    finalTable = finalTable + "<tr>";
                    foreach (DataColumn columnName in InvoiceData.Tables[3].Columns)
                    {
                        finalTable = finalTable + "<td class='text - end'>" + row[columnName.ColumnName].ToString() + "</td>";
                    }
                    finalTable = finalTable + "</tr>";
                }


                finalTable = finalTable + "</tfoot>";
            }

            return finalTable;
        }

        public VectorResponse<object> GetInvoiceReceiptData(SearchEntity objSearchEntity, Int64 userId)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {

                var result = objInvoiceDL.GetInvoiceReceiptData(objSearchEntity, userId);


                if (string.Equals(objSearchEntity.action, "InitialMissingInvoice")
                    || string.Equals(objSearchEntity.action, "AnalyticsByVendorCorporate")
                    || string.Equals(objSearchEntity.action, "AnalyticsByCategory")
                    || string.Equals(objSearchEntity.action, "AnalyticsByUser")
                    || string.Equals(objSearchEntity.action, "AnalyticsByClient"))
                {
                    if (DataValidationLayer.isDataSetNotNull(result))
                    {
                        return new VectorResponse<object>() { ResponseData = result };
                    }
                    else
                    {
                        return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Found !." } };
                    }
                }
                else
                {
                    var formatResult = FormatIRPDataPivotingView(result);

                    if (DataValidationLayer.isDataSetNotNull(formatResult))
                    {
                        return new VectorResponse<object>() { ResponseData = formatResult };
                    }
                    else
                    {
                        return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Found !." } };
                    }
                }


            }
        }

        private DataSet FormatIRPDataPivotingView(DataSet result)
        {
            DataSet dsFinalResult = new DataSet();
            System.Data.DataTable dtResult = result.Tables[VectorConstants.Zero].Clone();
            System.Data.DataTable newPivotedColumns = new System.Data.DataTable();
            newPivotedColumns.Columns.Add("DisplayColumnName", typeof(String));
            newPivotedColumns.Columns.Add("DisplayColumnValue", typeof(String));
            newPivotedColumns.Columns.Add("GridColumnIdentity", typeof(String));


            if (!DataManager.IsNullOrEmptyDataSet(result))
            {
                var distinctPivMonths = (from c in result.Tables[VectorConstants.Zero].AsEnumerable()
                                         select c.Field<String>("MonthYear")).ToList().Distinct();

                int ColCount = 0;
                foreach (var val in distinctPivMonths)
                {
                    if (!dtResult.Columns.Contains(val))
                    {
                        ColCount = ColCount + 1;
                        DataRow drNewRow = newPivotedColumns.NewRow();
                        drNewRow["DisplayColumnName"] = StringManager.ToCamelCase(val);
                        drNewRow["DisplayColumnValue"] = val;
                        drNewRow["GridColumnIdentity"] = "month" + ColCount;
                        newPivotedColumns.Rows.Add(drNewRow);

                        if (!newPivotedColumns.Columns.Contains(val))
                        {
                            dtResult.Columns.Add(val, typeof(string));
                            dtResult.Columns.Add(val + "_IRPUniqueCode", typeof(string));
                            dtResult.Columns.Add(val + "_IsOpen", typeof(string));
                            dtResult.Columns.Add(val + "_ColorCode", typeof(string));
                            dtResult.Columns.Add(val + "_DisplayColourCode", typeof(string));
                            dtResult.Columns.Add(val + "_NextExpectedDate", typeof(string));
                            dtResult.Columns.Add(val + "_IsFileUploaded", typeof(Boolean));

                            dtResult.Columns.Add("month" + ColCount, typeof(string));
                            dtResult.Columns.Add("month" + ColCount + "_IRPUniqueCode", typeof(string));
                            dtResult.Columns.Add("month" + ColCount + "_IsOpen", typeof(string));
                            dtResult.Columns.Add("month" + ColCount + "_ColorCode", typeof(string));
                            dtResult.Columns.Add("month" + ColCount + "_DisplayColourCode", typeof(string));
                            dtResult.Columns.Add("month" + ColCount + "_NextExpectedDate", typeof(string));
                            dtResult.Columns.Add("month" + ColCount + "_IsFileUploaded", typeof(Boolean));
                        }
                    }
                }

                var distinctAccountIds = (from c in result.Tables[VectorConstants.Zero].AsEnumerable()
                                          select c.Field<Int64>("IRAccountId")).ToList().Distinct();


                foreach (var actId in distinctAccountIds)
                {
                    DataRow drNR = dtResult.NewRow();

                    var rows = (from c in result.Tables[VectorConstants.Zero].AsEnumerable()
                                where c.Field<Int64>("IRAccountId") == actId
                                select new
                                {
                                    AccountId = c.Field<Int64>("AccountId"),
                                    ClientId = c.Field<Int64>("ClientId"),
                                    ClientName = c.Field<string>("ClientName"),
                                    PropertyId = c.Field<Int64>("PropertyId"),
                                    PropertyName = c.Field<string>("PropertyName"),
                                    AccountNumber = c.Field<string>("AccountNumber"),
                                    //PreviousVectorInvoiceNo = c.Field<string>("PreviousVectorInvoiceNo"),
                                    //PreviousInvoiceNumber = c.Field<string>("PreviousInvoiceNumber"),
                                    //PreviousInvoiceId = c.Field<Int64>("PreviousInvoiceId"),
                                    PreviousInvoiceDate = c.Field<string>("PreviousInvoiceDate"),
                                    NextExpectedDate = c.Field<string>("NextExpectedDate"),
                                    EffectiveDate = c.Field<string>("EffectiveDate"),
                                    //MissingStatus = c.Field<string>("MissingStatus"),
                                    VendorId = c.Field<Int64>("VendorId"),
                                    VendorName = c.Field<string>("VendorName"),
                                    VendorCorporateId = c.Field<Int64?>("VendorCorporateId"),
                                    VendorCorporateName = c.Field<string>("VendorCorporateName"),
                                    BillingCycle = c.Field<string>("BillingCycle"),
                                    //AccountStatus = c.Field<string>("AccountStatus"),
                                    VendorInvoiceDay = c.Field<int>("VendorInvoiceDay"),
                                    ReceiptMode = c.Field<string>("ReceiptMode"),
                                    AccountManager = c.Field<string>("AccountManager"),
                                    SalesPerson = c.Field<string>("SalesPerson"),
                                    //Negotiator = c.Field<string>("Negotiator"),
                                    ContractId = c.Field<Int64>("ContractId"),
                                    ContractNo = c.Field<string>("ContractNo"),
                                    //ContractStatus = c.Field<string>("ContractStatus"),
                                    //RegisteredOnline = c.Field<string>("RegisteredOnline"),
                                    BilledWhen = c.Field<string>("BilledWhen"),
                                    //Seasonal = c.Field<string>("Seasonal"),
                                    LatestInvoiceDate = c.Field<string>("LatestInvoiceDate"),
                                    //Comments = c.Field<string>("Comments"),
                                    InvoiceStatus = c.Field<string>("InvoiceStatus"),
                                    //Status = c.Field<bool>("Status"),
                                    //CreatedDate = c.Field<string>("CreatedDate"),
                                    //InvoiceId = c.Field<Int64>("InvoiceId"),
                                    //BatchDetailId = c.Field<Int64>("BatchDetailId"),
                                    AccountCategory = c.Field<string>("AccountCategory"),
                                    //RecentExceptionDate = c.Field<string>("RecentExceptionDate"),
                                    InvoiceMonth = c.Field<string>("InvoiceMonth"),
                                    InvoiceYear = c.Field<int>("InvoiceYear"),
                                    IsOpen = c.Field<Boolean>("IsOpen"),
                                    ColorCode = c.Field<string>("ColorCode"),
                                    CurrentInvoiceStatus = c.Field<string>("CurrentInvoiceStatus"),
                                    //ExceptionId = c.Field<Int64>("ExceptionId"),s
                                    //CurrentExceptionDate = c.Field<string>("CurrentExceptionDate"),
                                    IRPUniqueCode = c.Field<string>("IRPUniqueCode"),
                                    DisplayColourCode = c.Field<string>("DisplayColourCode"),
                                    DisplayMonthName = c.Field<string>("DisplayMonthName"),
                                    InvoiceReceipient = c.Field<string>("InvoiceReceipient"),
                                    AccountType = c.Field<string>("AccountType"),
                                    AccountMode = c.Field<string>("AccountMode"),
                                    IRAccountId = c.Field<Int64>("IRAccountId"),
                                    IsIRPannel = c.Field<Int32>("IsIRPannel"),
                                    MonthYear = c.Field<string>("MonthYear"),
                                    AcctCreatedDate = c.Field<string>("AcctCreatedDate"),
                                    Aging = c.Field<Int32>("Aging"),
                                    IsFileUploaded = c.Field<Boolean>("IsFileUploaded")
                                }).ToList();

                    drNR["AccountId"] = rows[0].AccountId;
                    drNR["ClientId"] = rows[0].ClientId;
                    drNR["ClientName"] = rows[0].ClientName;
                    drNR["PropertyId"] = rows[0].PropertyId;
                    drNR["PropertyName"] = rows[0].PropertyName;
                    drNR["AccountNumber"] = rows[0].AccountNumber;
                    drNR["AccountType"] = rows[0].AccountType;
                    drNR["AccountMode"] = rows[0].AccountMode;
                    drNR["PreviousInvoiceDate"] = rows[0].PreviousInvoiceDate;
                    drNR["NextExpectedDate"] = rows[0].NextExpectedDate;
                    drNR["EffectiveDate"] = rows[0].EffectiveDate;
                    // drNR["MissingStatus"] = rows[0].MissingStatus;
                    drNR["VendorId"] = rows[0].VendorId;
                    drNR["VendorName"] = rows[0].VendorName;
                    drNR["VendorCorporateId"] = rows[0].VendorCorporateId;
                    drNR["VendorCorporateName"] = rows[0].VendorCorporateName;
                    drNR["BillingCycle"] = rows[0].BillingCycle;
                    //drNR["AccountStatus"] = rows[0].AccountStatus;
                    drNR["VendorInvoiceDay"] = rows[0].VendorInvoiceDay;
                    drNR["ReceiptMode"] = rows[0].ReceiptMode;
                    drNR["AccountManager"] = rows[0].AccountManager;
                    drNR["SalesPerson"] = rows[0].SalesPerson;
                    //drNR["Negotiator"] = rows[0].Negotiator;
                    drNR["ContractId"] = rows[0].ContractId;
                    drNR["ContractNo"] = rows[0].ContractNo;
                    //drNR["ContractStatus"] = rows[0].ContractStatus;
                    //drNR["RegisteredOnline"] = rows[0].RegisteredOnline;
                    drNR["BilledWhen"] = rows[0].BilledWhen;
                    //drNR["Seasonal"] = rows[0].Seasonal;
                    drNR["LatestInvoiceDate"] = rows[0].LatestInvoiceDate;
                    //drNR["Comments"] = rows[0].Comments;
                    //drNR["InvoiceStatus"] = rows[0].InvoiceStatus;
                    //drNR["Status"] = rows[0].Status;
                    //drNR["CreatedDate"] = rows[0].CreatedDate;
                    //drNR["InvoiceId"] = rows[0].InvoiceId;
                    //drNR["BatchDetailId"] = rows[0].BatchDetailId;
                    drNR["AccountCategory"] = rows[0].AccountCategory;
                    //drNR["RecentExceptionDate"] = rows[0].RecentExceptionDate;
                    //drNR["InvoiceMonth"] = rows[0].InvoiceMonth;
                    //drNR["InvoiceYear"] = rows[0].InvoiceYear;
                    drNR["InvoiceReceipient"] = rows[0].InvoiceReceipient;
                    drNR["AcctCreatedDate"] = rows[0].AcctCreatedDate;
                    drNR["IRAccountId"] = rows[0].IRAccountId;
                    drNR["IsIRPannel"] = rows[0].IsIRPannel;


                    drNR["IsOpen"] = rows[0].IsOpen;

                    drNR["IRPUniqueCode"] = rows[0].IRPUniqueCode;
                    drNR["DisplayColourCode"] = rows[0].DisplayColourCode;
                    drNR["IRPUniqueCode"] = rows[0].IRPUniqueCode;
                    drNR["DisplayMonthName"] = rows[0].DisplayMonthName;
                    drNR["MonthYear"] = rows[0].MonthYear;


                    ColCount = 0;
                    foreach (var val in distinctPivMonths)
                    {
                        ColCount = ColCount + 1;
                        var internVal = (from r in rows.AsEnumerable()
                                         where r.AccountId == actId && r.MonthYear == val
                                         select r).ToList().FirstOrDefault();

                        drNR["ColorCode"] = rows[0].ColorCode;

                        if (internVal != null)
                        {
                            drNR[val] = internVal.InvoiceStatus;
                            drNR[val + "_IRPUniqueCode"] = internVal.IRPUniqueCode;
                            drNR[val + "_IsOpen"] = internVal.IsOpen;
                            drNR[val + "_ColorCode"] = internVal.ColorCode;
                            drNR[val + "_DisplayColourCode"] = internVal.DisplayColourCode;
                            drNR[val + "_NextExpectedDate"] = internVal.NextExpectedDate;
                            drNR[val + "_IsFileUploaded"] = internVal.IsFileUploaded;
                            

                            drNR["month" + ColCount] = internVal.InvoiceStatus;
                            drNR["month" + ColCount + "_IRPUniqueCode"] = internVal.IRPUniqueCode;
                            drNR["month" + ColCount + "_IsOpen"] = internVal.IsOpen;
                            drNR["month" + ColCount + "_ColorCode"] = internVal.ColorCode;
                            drNR["month" + ColCount + "_DisplayColourCode"] = internVal.DisplayColourCode;
                            drNR["month" + ColCount + "_NextExpectedDate"] = internVal.NextExpectedDate;
                            drNR["month" + ColCount + "_IsFileUploaded"] = internVal.IsFileUploaded; 
                        }


                    }

                    dtResult.Rows.Add(drNR);
                }


                dsFinalResult.Tables.Add(dtResult);
                dsFinalResult.Tables.Add(result.Tables[VectorConstants.One].Copy());
                dsFinalResult.Tables.Add(result.Tables[VectorConstants.Two].Copy());
                dsFinalResult.Tables.Add(newPivotedColumns);

            }

            return dsFinalResult;
        }

        public VectorResponse<object> UploadIRPDocuments(IRPDocuments objDocuments, Int64 userId)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.UploadIRPDocuments(objDocuments, userId);


                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    var resultInfo = (from c in result.Tables[0].AsEnumerable()
                                      select new
                                      {
                                          result = c.Field<bool>("Result"),
                                          resultMessage = c.Field<string>("ResultMessage"),
                                          DestinationFilePath = c.Field<string>("DestinationFilePath"),
                                          DestinationFolder = c.Field<string>("DestinationFolder"),
                                          DestinationFileName = c.Field<string>("DestinationFileName")
                                      }).FirstOrDefault();


                    if (resultInfo.result)
                    {
                        string sourceFilePath = SecurityManager.GetConfigValue("FileServerTempPath") + objDocuments.TempFolderName + "\\" + objDocuments.Documents[0].fileName; ;
                        string desitinationFilePath = SecurityManager.GetConfigValue("FileServerPath") + resultInfo.DestinationFolder;

                        if (!Directory.Exists(resultInfo.DestinationFolder))
                        {
                            Directory.CreateDirectory(desitinationFilePath);
                        }

                        if (File.Exists(sourceFilePath))
                        {
                            File.Move(sourceFilePath, desitinationFilePath + "\\" + resultInfo.DestinationFileName);
                        }
                    }

                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to Upload Invoice, Please Contact IT Support." } };
                }
            }
        }

        public VectorResponse<object> GetIRPDocuments(string action, string IRPUniqueId, Int64 userId)
        {
            using (var objInvoiceDL = new InvoiceDL(objVectorDB))
            {
                var result = objInvoiceDL.GetIRPDocuments(action, IRPUniqueId, userId);


                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Found !." } };
                }
            }
        }


        //public VectorResponse<object> TriggerShortPayNotification(Int64 userId)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    DataSet resultData = new DataSet();

        //    using (var objInvoiceDL = new InvoiceDL(objVectorDB))
        //    {
        //        resultData = objInvoiceDL.TriggerShortPayNotification(userId);

        //        foreach(DataRow dr in resultData.Tables[0].Rows)
        //        {
        //            Int64 invoiceId = Convert.ToInt64(dr["InvoiceId"]);
        //            InvoiceHeaderInfo objInvoiceHeaderInfo = new InvoiceHeaderInfo();
        //            objInvoiceHeaderInfo.InvoiceId = invoiceId;

        //           var result = objInvoiceDL.GetInvoiceNotificaitonsInfo("ShortPayEmailInfo", objInvoiceHeaderInfo);

        //            if (!DataManager.IsNullOrEmptyDataSet(result, 2, 2))
        //            { 
        //                using (EmailDetails objEmailDetails = new EmailDetails())
        //                { 
        //                    var emailInfo = (from c in result.Tables[0].AsEnumerable()
        //                                     select new
        //                                     {
        //                                         EmailBody = c.Field<string>("emailBody"),
        //                                         EmailSubject = c.Field<string>("emailSubject"),
        //                                         ToEmail = c.Field<string>("EmailTo"),
        //                                         CCEmail = c.Field<string>("EmailCC"),
        //                                         EmailServiceName = c.Field<string>("EmailServiceName")
        //                                     }).FirstOrDefault();

        //                    string body = emailInfo.EmailBody.Replace("@ServiceLevelItems@", ConvertDataTabletoHTML(result.Tables[1]));
        //                    SendEmail objSendEmail = new SendEmail();
        //                    objSendEmail.To = emailInfo.ToEmail;
        //                    objSendEmail.CC = emailInfo.CCEmail;
        //                    objSendEmail.Subject = emailInfo.EmailSubject;
        //                    objSendEmail.FromEmail = "SHORTPAY";
        //                    objSendEmail.Body = body;


        //                    bool emailResult = EmailManager.SendEmail(objSendEmail, smtpSection: SecurityManager.GetSmtpMailSection(emailInfo.EmailServiceName), fromEmail: "SHORTPAY");

        //                    if (emailResult)
        //                    {
        //                        sb.Append(invoiceId); 
        //                        sb.AppendLine();
        //                    }
        //                    else
        //                    {

        //                        sb.Append(invoiceId +  " - Unable to Send Email.");
        //                        sb.AppendLine(); 
        //                    }
        //                }
        //            }


        //        }

        //    }

        //    if (DataValidationLayer.isDataSetNotNull(resultData))
        //    {
        //        return new VectorResponse<object>() { ResponseData = resultData };
        //    }
        //    else
        //    {
        //        return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Found !." } };
        //    }
        //}

    }



}