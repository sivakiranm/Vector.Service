using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Garage.DataLayer;
using Vector.Garage.Entities;
using static Vector.Common.BusinessLayer.EmailTemplateEnums;
using Word = Microsoft.Office.Interop.Word;

namespace Vector.Garage.BusinessLayer
{
    public class ContractsBL : DisposeLogic
    {
        private const string QueryKeyFormat = "(#{0}#)";
        private VectorDataConnection objVectorDB;
        public ContractsBL(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }

        public VectorResponse<object> GetContracts(ContractSearch objContractSearch)
        {
            using (var objContractsDL = new ContractsDL(objVectorDB))
            {
                var result = objContractsDL.GetContracts(objContractSearch);
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

        public VectorResponse<object> GetContractData(ContractData objContractData)
        {
            using (var objContractsDL = new ContractsDL(objVectorDB))
            {
                var result = objContractsDL.GetContractData(objContractData);
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

        public VectorResponse<object> UpdateContract(Contract objContract, Int64 userId)
        {
            using (var objContractsDL = new ContractsDL(objVectorDB))
            {
                var result = objContractsDL.UpdateContract(objContract, userId);
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

        public VectorResponse<object> ApproveOrDeclineAnnualIncrease(ApproveOrDeclineAnnualIncrease objApproveOrDeclineAnnualIncrease)
        {
            using (var objContractsDL = new ContractsDL(objVectorDB))
            {
                var result = objContractsDL.ApproveOrDeclineAnnualIncrease(objApproveOrDeclineAnnualIncrease);
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

        public VectorResponse<object> ManageContractLineItems(ContractLineItems objContractLineItems)
        {
            using (var objContractsDL = new ContractsDL(objVectorDB))
            {
                var result = objContractsDL.ManageContractLineItems(objContractLineItems);
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

        public VectorResponse<object> RenewOrArchiveContract(Contract objContract)
        {
            using (var objContractsDL = new ContractsDL(objVectorDB))
            {
                var result = objContractsDL.RenewOrArchiveContract(objContract);
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

        public VectorResponse<object> TransmitContract(ContractApproval objContractApproval, Int64 userId)
        {
            using (var objContractsDL = new ContractsDL(objVectorDB))
            {
                var result = objContractsDL.TransmitContract(objContractApproval, userId);

                foreach (Files file in objContractApproval.Documents)
                {
                    string tempFolderpath = SecurityManager.GetConfigValue("FileServerTempPath") + file.filePath + "\\" + file.fileName;

                    if (File.Exists(tempFolderpath))
                    {
                        string contractFilesPath = SecurityManager.GetConfigValue("FileServerPath") + "Contracts//" + objContractApproval.ContractNo + "//";

                        if (!Directory.Exists(contractFilesPath))
                            FileManager.CreateDirectory(contractFilesPath);

                        File.Copy(tempFolderpath, contractFilesPath + file.fileName);
                    }
                }

                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    string emailResult = string.Empty;
                    if (objContractApproval.ClientApproved == "Approved" || objContractApproval.VectorApproved == "Approved" || objContractApproval.VendorApproved == "Approved")
                        emailResult = SendContractEmailtoNegotiatorSalesPerson(objContractApproval);
                    else if (objContractApproval.VendorApproved == "Rejected")
                        emailResult = SendContractVendorRejectedEmail(objContractApproval);

                    if (string.IsNullOrEmpty(emailResult))
                        return new VectorResponse<object>() { ResponseData = result };
                    else
                        return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Contract details transmitted successfully.Failed to send Email" } };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> GetContractEmailDetailsForTransmit(ContractApproval objContractApproval)
        {
            using (var objContractsDL = new ContractsDL(objVectorDB))
            {
                DataSet emailBodyData = objContractsDL.GetContractEmailDetailsForTransmit(objContractApproval.ContractId.ToString(), "VectorContractEmailDetails");

                using (EmailDetails objEmailDetails = new EmailDetails())
                {
                    var objEmailTemplateGUIDs = EmailTemplateGUIDs.TransmittingContractToHauler;

                    if (StringManager.IsEqual(objContractApproval.Action, "TransmitContractClientApproval") || StringManager.IsEqual(objContractApproval.Action, "TransmitContractClientApprovalFromTask"))
                        objEmailTemplateGUIDs = EmailTemplateGUIDs.TransmittingContractToClient;

                    else if (StringManager.IsEqual(objContractApproval.Action, "TransmitContractVectorApproval") || StringManager.IsEqual(objContractApproval.Action, "TransmitContractVectorApprovalFromTask"))
                        objEmailTemplateGUIDs = EmailTemplateGUIDs.TransmittingContractToVector;

                    EmailTemplate objEmailTemplate = EmailTemplateManager.GetEmailTemplate(AppDomain.CurrentDomain.BaseDirectory, EmailTemplates.Contracts, objEmailTemplateGUIDs);
                    objEmailDetails.Subject = string.Format(objEmailTemplate.Subject,
                                                            emailBodyData.Tables[0].Rows[0]["ClientName"],
                                                            emailBodyData.Tables[0].Rows[0]["PropertyName"],
                                                            objContractApproval.ContractNo);
                    objEmailDetails.Body = ReplaceWithContractData(emailBodyData, objEmailTemplate.EmailBody, objContractApproval);
                    objEmailDetails.Signature = objEmailTemplate.Signature;

                    if (StringManager.IsEqual(objContractApproval.Action, "TransmitContractClientApproval") || StringManager.IsEqual(objContractApproval.Action, "TransmitContractClientApprovalFromTask"))
                    {
                        if (!DataManager.IsNullOrEmptyDataSet(emailBodyData, emailBodyData.Tables.Count, 4))
                        {
                            objEmailDetails.To = Convert.ToString(emailBodyData.Tables[3].Rows[0]["ClientEmails"]).Replace(" ", "").TrimEnd(',');
                        }
                    }

                    if (StringManager.IsEqual(objContractApproval.Action, "TransmitContractVendorApproval") || StringManager.IsEqual(objContractApproval.Action, "TransmitContractVendorApprovalFromTask"))
                    {
                        if (!DataManager.IsNullOrEmptyDataSet(emailBodyData, emailBodyData.Tables.Count, 5))
                        {
                            objEmailDetails.To = Convert.ToString(emailBodyData.Tables[4].Rows[0]["VendorEmails"]).TrimEnd(',');
                        }
                    }

                    string tempFolderName = string.IsNullOrEmpty(objContractApproval.TempFolderName) ? DateManager.GenerateTitleWithTimestamp(objContractApproval.LoggedInUserName) : objContractApproval.TempFolderName;

                    objEmailDetails.AttachmentsFolderPath = tempFolderName;

                    string approvedDocName = Convert.ToString(emailBodyData.Tables[0].Rows[0]["ApprovedContractDocumentName"]);
                    string approvedDocPath = Convert.ToString(emailBodyData.Tables[0].Rows[0]["ApprovedContractDocumentPath"]);

                    string contractFileName = string.Empty;

                    if (string.IsNullOrEmpty(approvedDocName))
                    {
                        contractFileName = GenerateContract(objContractApproval.ContractId.ToString(), objContractApproval.ContractNo, objContractApproval.ContractDocument, tempFolderName);
                    }

                    objEmailDetails.Files = AddContractDocToFiles(objContractApproval.Documents, contractFileName, approvedDocPath, approvedDocName);



                    return new VectorResponse<object>() { ResponseData = objEmailDetails };
                }
            }
        }

        private List<Files> AddContractDocToFiles(List<Files> documents, string contractFile, string approvedDocPath, string approvedDocName)
        {
            List<Files> objFiles = new List<Files>();

            foreach (var item in documents)
                objFiles.Add(new Files { fileName = item.fileName, fileType = item.fileType, filePath = item.filePath, isLocal = item.isLocal });


            if (!string.IsNullOrEmpty(contractFile) && (documents.Count == 0))
                objFiles.Add(new Files { fileName = contractFile, filePath = "", fileType = "Contract Document", isLocal = false });


            if (!string.IsNullOrEmpty(approvedDocPath))
                objFiles.Add(new Files { fileName = approvedDocName, filePath = approvedDocPath, fileType = "Contract Document", isLocal = false });


            return objFiles;
        }

        private string ReplaceWithContractData(DataSet contractData, string mailBody, ContractApproval objContractApproval)
        {
            string PropertyDetails = Convert.ToString(contractData.Tables[0].Rows[0]["PropertyAddress1"]);
            if (!string.IsNullOrEmpty(Convert.ToString(contractData.Tables[0].Rows[0]["PropertyAddress2"])))
            {
                PropertyDetails += " " + Convert.ToString(contractData.Tables[0].Rows[0]["PropertyAddress2"]);
                PropertyDetails += ", " + Convert.ToString(contractData.Tables[0].Rows[0]["PropertyCity"]) + ", " +
                Convert.ToString(contractData.Tables[0].Rows[0]["PropertyState"]) + " " + Convert.ToString(contractData.Tables[0].Rows[0]["PropertyZip"]);
            }

            mailBody = mailBody.Replace("@Property Name@", Convert.ToString(contractData.Tables[0].Rows[0]["PropertyName"]));
            mailBody = mailBody.Replace("@AM Name@", Convert.ToString(contractData.Tables[0].Rows[0]["AccountManager"]));
            mailBody = mailBody.Replace("@AM Email Address@", Convert.ToString(contractData.Tables[0].Rows[0]["AMEMAIL"]));
            mailBody = mailBody.Replace("@AM Phone@", Convert.ToString(contractData.Tables[0].Rows[0]["AMPHONE"]));
            mailBody = mailBody.Replace("@EffectiveDate@", Convert.ToString(contractData.Tables[0].Rows[0]["BeginDate"]));
            //mailBody.Replace("@EndDate@", contractData.Tables[0].Rows[0]["EndDate"].ToString());
            //mailBody.Replace("@Terms@", contractData.Tables[0].Rows[0]["Terms"].ToString());
            mailBody = mailBody.Replace("@ServiceAddress@", PropertyDetails);
            mailBody = mailBody.Replace("@HaulerDetails@", Convert.ToString(contractData.Tables[0].Rows[0]["VendorName"]) + "-" + Convert.ToString(contractData.Tables[0].Rows[0]["VendorNameShortName"]));
            mailBody = mailBody.Replace("@HaulerContact@", Convert.ToString(contractData.Tables[0].Rows[0]["Phone"]));
            mailBody = mailBody.Replace("@ApValue@", Convert.ToString(contractData.Tables[0].Rows[0]["ApprovedAnualIncrease"]));

            GridView gvservice = new GridView();
            gvservice.ShowHeader = true;
            gvservice.GridLines = GridLines.Both;
            contractData.Tables[1].Columns.RemoveAt(contractData.Tables[1].Columns.Count - 1);
            gvservice.DataSource = contractData.Tables[1];
            gvservice.DataBind();
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gvservice.RenderControl(htmlWrite);
            mailBody = mailBody.Replace("@ServiceLevelItems@", htmlWrite.InnerWriter.ToString());
            //mailBody = mailBody.Replace("@ServiceLevelItems@", ConvertDataTableToHTML(contractData.Tables[1]));
            if (!DataManager.IsNullOrEmptyDataTable(contractData.Tables[2]))
            {
                StringBuilder exemptedList = new StringBuilder("<p>Excluded items:</p><p><ul>");
                foreach (DataRow dr in contractData.Tables[2].Rows)
                {
                    exemptedList.Append("<li>");
                    exemptedList.Append(Convert.ToString(dr["VendorDescription"]));
                    exemptedList.Append("</li>");
                }
                exemptedList.Append("</p></ul>");
                mailBody = mailBody.Replace("@ExcludedItemsList@", exemptedList.ToString());
            }
            else
                mailBody = mailBody.Replace("@ExcludedItemsList@", string.Empty);

       //     mailBody = mailBody.Replace("@Comments@", objContractApproval.Comments);

            mailBody = mailBody.Replace("@LoginUser@", objContractApproval.LoggedInUserName);
            mailBody = mailBody.Replace("@LoginUserPhone@", objContractApproval.LoggedInUserPhone);
            mailBody = mailBody.Replace("@LoginUserEmail@", objContractApproval.LoggedInUserEmail);


            return mailBody;
        }



        //public string ConvertDataTableToHTML(DataTable dt)
        //{
        //    string html = "<table>";
        //    //add header row
        //    html += "<tr>";
        //    for (int i = 0; i < dt.Columns.Count; i++)
        //        html += "<td>" + dt.Columns[i].ColumnName + "</td>";
        //    html += "</tr>";
        //    //add rows
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        html += "<tr>";
        //        for (int j = 0; j < dt.Columns.Count; j++)
        //            html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
        //        html += "</tr>";
        //    }
        //    html += "</table>";
        //    return html;
        //}

        private string GenerateContract(string contractId, string contractNbr, string templateFileName, string tempFolderName = "")
        {
            string templatesSourceFolder = SecurityManager.GetConfigValue("FileServerPath") + "Templates\\";
            string templateSourceFile = string.Empty;
            if (!string.IsNullOrEmpty(templateFileName))
                templateSourceFile = templatesSourceFolder + "\\" + templateFileName;
            else
                templateSourceFile = templatesSourceFolder + "//Three Page Hauler Agreement - v 2.1.docx";


            string tempFolderPath = SecurityManager.GetConfigValue("FileServerTempPath") + tempFolderName + "\\";

            if (!Directory.Exists(tempFolderPath))
                FileManager.CreateDirectory(tempFolderPath);


            string contractFileName = DateManager.GenerateTitleWithTimestamp(contractNbr + "_HC");
            string templateSourceFileTempPath = tempFolderPath + "\\" + contractFileName + ".docx";

            bool result = ExportContract(templateSourceFile, templateSourceFileTempPath, contractId);

            templateSourceFileTempPath = Path.ChangeExtension(templateSourceFileTempPath, "pdf");
            if (File.Exists(templateSourceFileTempPath))
            {
                string contractFilesPath = SecurityManager.GetConfigValue("FileServerPath") + "Contracts//" + contractNbr + "//";

                if (!Directory.Exists(contractFilesPath))
                    FileManager.CreateDirectory(contractFilesPath);

                File.Copy(templateSourceFileTempPath, contractFilesPath + contractFileName + ".Pdf");
            }
            if (result)
                contractFileName = contractFileName + ".Pdf";
            else
            {
                contractFileName = string.Empty;
            }
            return contractFileName;
        }

        private bool ExportContract(string SourcePath, string targetPath, string contractId)
        {
            using (var objContractsDL = new ContractsDL(objVectorDB))
            {
                try
                {
                    if (File.Exists(targetPath))
                        File.Delete(targetPath);

                    File.Copy(SourcePath, targetPath, false);
                }
                catch (IOException ex)
                {
                    return false;
                }
                DataSet contractData = objContractsDL.GetContractEmailDetailsForTransmit(contractId, "VectorContractDocumentDetails");
                Word.Document aDoc = null;
                object missing = Missing.Value;
                object saveChanges = Word.WdSaveOptions.wdDoNotSaveChanges;
                Word.Application wordApp = new Word.Application();
                try
                {
                    object fileName = (object)targetPath;
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

                    ReplaceWithContractDetails(ref wordApp, ref contractData);
                    aDoc.Save();
                    string pdfFileName = string.Empty;
                    object outputFileName = pdfFileName = Path.ChangeExtension(targetPath, "pdf");
                    object fileFormat = Word.WdSaveFormat.wdFormatPDF;
                    // Save document into PDF Format
                    aDoc.SaveAs(ref outputFileName,
                     ref fileFormat, ref missing, ref missing,
                     ref missing, ref missing, ref missing, ref missing,
                     ref missing, ref missing, ref missing, ref missing,
                     ref missing, ref missing, ref missing, ref missing);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {

                    aDoc.Close(ref saveChanges, ref missing, ref missing);
                    aDoc = null;
                    wordApp.Quit(ref missing, ref missing, ref missing);
                }
            }
        }

        private void ReplaceWithContractDetails(ref Microsoft.Office.Interop.Word.Application wordApp, ref DataSet contractData)
        {

            this.FindAndReplaceServiceLevelItems(wordApp, "<List of Service Level Items>", ref contractData);
            this.FindAndReplaceServiceLevelItems(wordApp, "<List of Exempted Items>", ref contractData);

            this.FindAndReplaceServiceLevelItems(wordApp, "<List of Service Level Items1>", ref contractData);
            this.FindAndReplaceServiceLevelItems(wordApp, "<List of Exempted Items1>", ref contractData);

            this.FindAndReplaceServiceLevelItems(wordApp, "<List of Service Level Items2>", ref contractData);
            this.FindAndReplaceServiceLevelItems(wordApp, "<List of Exempted Items2>", ref contractData);

            string haulerAddress = Convert.ToString(contractData.Tables[0].Rows[0]["HaulerAddress1"]);
            if (!string.IsNullOrEmpty(Convert.ToString(contractData.Tables[0].Rows[0]["HaulerAddress2"])))
                haulerAddress += " " + Convert.ToString(contractData.Tables[0].Rows[0]["HaulerAddress2"]);
            this.FindAndReplaceText(wordApp, "<Hauler (Corporate)>", Convert.ToString(contractData.Tables[0].Rows[0]["VendorCorporate"]));

            this.FindAndReplaceText(wordApp, "<HaulerCorporate>", Convert.ToString(contractData.Tables[0].Rows[0]["VendorCorporate"]));

            this.FindAndReplaceText(wordApp, "<HaulerLocal>", Convert.ToString(contractData.Tables[0].Rows[0]["VendorName"]));
            this.FindAndReplaceText(wordApp, "<HaulerAddress>", haulerAddress);
            this.FindAndReplaceText(wordApp, "<HaulerCityStateZipCode>", Convert.ToString(contractData.Tables[0].Rows[0]["HaulerCity"]) + ", " +
                Convert.ToString(contractData.Tables[0].Rows[0]["haulerstate"]) + " " + Convert.ToString(contractData.Tables[0].Rows[0]["HaulerZip"]));
            string clientAddress = Convert.ToString(contractData.Tables[0].Rows[0]["ClientAddress1"]);
            if (!string.IsNullOrEmpty(Convert.ToString(contractData.Tables[0].Rows[0]["ClientAddress2"])))
                clientAddress += " " + Convert.ToString(contractData.Tables[0].Rows[0]["ClientAddress2"]);
            string onHaulerContract = Convert.ToString(contractData.Tables[0].Rows[0]["NameOnVendorContract"]).Replace(" ", "").ToUpper();
            string clientName = string.Empty;
            string propertyName = Convert.ToString(contractData.Tables[0].Rows[0]["PropertyName"]);
            switch (onHaulerContract)
            {
                case "CLIENTNAME":
                    clientName = Convert.ToString(contractData.Tables[0].Rows[0]["ClientName"]);
                    break;
                case "CLIENTLEGALNAME":
                    clientName = Convert.ToString(contractData.Tables[0].Rows[0]["ClientLegalName"]);
                    break;
                case "PROPERTYNAME":
                    clientName = Convert.ToString(contractData.Tables[0].Rows[0]["PropertyName"]);
                    break;
                case "PROPERTYLEGALNAME":
                    clientName = Convert.ToString(contractData.Tables[0].Rows[0]["PropertyLegalName"]);
                    break;
                case "REFUSESPECIALISTS":
                    clientName = "RefuseSpecialists";
                    break;
                default:
                    clientName = Convert.ToString(contractData.Tables[0].Rows[0]["ClientName"]);
                    break;
            }
            this.FindAndReplaceText(wordApp, "<ClientName>", clientName);
            this.FindAndReplaceText(wordApp, "<ClientAddress>", clientAddress);
            this.FindAndReplaceText(wordApp, "<ClientCityStateZipCode>", Convert.ToString(contractData.Tables[0].Rows[0]["ClientCity"]) + ", " +
                Convert.ToString(contractData.Tables[0].Rows[0]["ClientState"]) + " " + Convert.ToString(contractData.Tables[0].Rows[0]["ClientZip"]));
            this.FindAndReplaceText(wordApp, "<ContractDuration>", Convert.ToString(contractData.Tables[0].Rows[0]["Terms"]));
            this.FindAndReplaceText(wordApp, "<APValue>", Convert.ToString(contractData.Tables[0].Rows[0]["ApprovedAnualIncrease"]));
            string PropertyAddress = Convert.ToString(contractData.Tables[0].Rows[0]["PropertyAddress1"]);
            if (!string.IsNullOrEmpty(Convert.ToString(contractData.Tables[0].Rows[0]["PropertyAddress2"])))
                PropertyAddress += " " + Convert.ToString(contractData.Tables[0].Rows[0]["PropertyAddress2"]);
            this.FindAndReplaceText(wordApp, "<PropertyAddress>", PropertyAddress);
            this.FindAndReplaceText(wordApp, "<PropertyCityStateZipCode>", Convert.ToString(contractData.Tables[0].Rows[0]["PropertyCity"]) + ", " +
                Convert.ToString(contractData.Tables[0].Rows[0]["PropertyState"]) + " " + Convert.ToString(contractData.Tables[0].Rows[0]["PropertyZip"]));
            this.FindAndReplaceText(wordApp, "<ContractBeginDate>", Convert.ToString(contractData.Tables[0].Rows[0]["BeginDate"]));
            this.FindAndReplaceText(wordApp, "<ContractEndDate>", Convert.ToString(contractData.Tables[0].Rows[0]["EndDate"]));
            this.FindAndReplaceText(wordApp, "<HaulerPaymentTerms>", Convert.ToString(contractData.Tables[0].Rows[0]["ClientPaymentTerm"]));

            //NEWLY dded for templates
            this.FindAndReplaceText(wordApp, "<haulerstate>", Convert.ToString(contractData.Tables[0].Rows[0]["haulerstate"]), true);
            this.FindAndReplaceText(wordApp, "<MonthDayYear>", System.DateTime.Now.ToString("MMMM dd yyyy"), true);
            this.FindAndReplaceText(wordApp, "<MMDDYYYY>", System.DateTime.Now.ToString("MMddyyyy"), true);
            this.FindAndReplaceText(wordApp, "<MM/DD/YYYY>", System.DateTime.Now.ToString("MM/dd/yyyy"), true);

            this.FindAndReplaceText(wordApp, "<TodaysDay>", Convert.ToString(System.DateTime.Now.Day), true);
            this.FindAndReplaceText(wordApp, "<ThisMonth>", Convert.ToString(System.DateTime.Now.Month), true);

            this.FindAndReplaceText(wordApp, "<CurrentYear>", Convert.ToString(System.DateTime.Now.Year), true);


            this.FindAndReplaceText(wordApp, "<PropertyName>", Convert.ToString(contractData.Tables[0].Rows[0]["PropertyName"]), true);
            this.FindAndReplaceText(wordApp, "<HaulerFax>", Convert.ToString(contractData.Tables[0].Rows[0]["HaulerFax"]), true);
            this.FindAndReplaceText(wordApp, "<OriginalClientName>", Convert.ToString(contractData.Tables[0].Rows[0]["ClientName"]), true);

            this.FindAndReplaceText(wordApp, "<HaulerPhone>", Convert.ToString(contractData.Tables[0].Rows[0]["HaulerPhone"]), true);
            this.FindAndReplaceText(wordApp, "<HaulerEmail>", Convert.ToString(contractData.Tables[0].Rows[0]["HaulerEmail"]), true);
            this.FindAndReplaceText(wordApp, "<RSAccountManagerPhone>", Convert.ToString(contractData.Tables[0].Rows[0]["AccountManagerPhone"]), true);
            this.FindAndReplaceText(wordApp, "<RSAccountManager>", Convert.ToString(contractData.Tables[0].Rows[0]["AccountManager"]), true);
            this.FindAndReplaceText(wordApp, "<RSAccountManagerEmail>", Convert.ToString(contractData.Tables[0].Rows[0]["RSAccountManagerEmail"]), true);

            this.FindAndReplaceText(wordApp, "<BLHaulerName>", Convert.ToString(contractData.Tables[0].Rows[0]["BaseLineHauler"]), true);
            this.FindAndReplaceText(wordApp, "<BLineHaulerEmail>", Convert.ToString(contractData.Tables[0].Rows[0]["BLineHaulerEmail"]), true);
            this.FindAndReplaceText(wordApp, "<RSAccountManagerEmail>", Convert.ToString(contractData.Tables[0].Rows[0]["RSAccountManagerEmail"]), true);
            this.FindAndReplaceText(wordApp, "<PropertyPrimaryContact>", Convert.ToString(contractData.Tables[0].Rows[0]["PropertyContact"]), true);
            this.FindAndReplaceText(wordApp, "<HaulerContact>", Convert.ToString(contractData.Tables[0].Rows[0]["HaulerContact"]), true);
            this.FindAndReplaceText(wordApp, "<PropertyLeagelNameOnTemplate>", Convert.ToString(contractData.Tables[0].Rows[0]["PropertyLegalName"]), true);
            this.FindAndReplaceText(wordApp, "<PropertyLegalName>", Convert.ToString(contractData.Tables[0].Rows[0]["PropertyLegalName"]), true);
            this.FindAndReplaceText(wordApp, "<PropertyBillingFax>", Convert.ToString(contractData.Tables[0].Rows[0]["PropertyBillingFax"]), true);
            this.FindAndReplaceText(wordApp, "<PropertyPhone>", Convert.ToString(contractData.Tables[0].Rows[0]["PropertyPhone"]), true);
            this.FindAndReplaceText(wordApp, "<ClientLegalName>", Convert.ToString(contractData.Tables[0].Rows[0]["ClientLegalName"]));
            //Sprint 54 new tags
            this.FindAndReplaceText(wordApp, "<PropertyPrimaryContact>", Convert.ToString(contractData.Tables[0].Rows[0]["PropertyContact"]), true);
            this.FindAndReplaceText(wordApp, "<PropertyPrimaryContactEmail>", Convert.ToString(contractData.Tables[0].Rows[0]["PropertyPrimaryContactEmail"]), true);
            this.FindAndReplaceText(wordApp, "<PropertyPrimaryContactPhone>", Convert.ToString(contractData.Tables[0].Rows[0]["PropertyPrimaryContactPhone"]), true);
            this.FindAndReplaceText(wordApp, "<HaulerPrimaryContact>", Convert.ToString(contractData.Tables[0].Rows[0]["HaulerContact"]), true);
            this.FindAndReplaceText(wordApp, "<HaulerLocPrimaryContactEmail>", Convert.ToString(contractData.Tables[0].Rows[0]["HaulerLocPrimaryContactEmail"]), true);
            this.FindAndReplaceText(wordApp, "<HaulerLocPrimaryContactPhone>", Convert.ToString(contractData.Tables[0].Rows[0]["HaulerLocPrimaryContactPhone"]), true);
            this.FindAndReplaceText(wordApp, "<PropBillingAddress1>", Convert.ToString(contractData.Tables[0].Rows[0]["PropBillingAddress1"]), true);
            this.FindAndReplaceText(wordApp, "<PropBillingAddress2>", Convert.ToString(contractData.Tables[0].Rows[0]["PropBillingAddress2"]), true);
            this.FindAndReplaceText(wordApp, "<PropBillingCity>", Convert.ToString(contractData.Tables[0].Rows[0]["PropBillingCity"]), true);
            this.FindAndReplaceText(wordApp, "<PropBillingState>", Convert.ToString(contractData.Tables[0].Rows[0]["PropBillingState"]), true);
            this.FindAndReplaceText(wordApp, "<PropBillingZip>", Convert.ToString(contractData.Tables[0].Rows[0]["PropBillingZip"]), true);

            if (contractData.Tables[0].Columns.Contains("MonthDayYear")) 
                 this.FindAndReplaceText(wordApp, "<MonthDayYear>", Convert.ToString(contractData.Tables[0].Rows[0]["MonthDayYear"]), true);

            if (contractData.Tables[0].Columns.Contains("ContractDay")) 
            this.FindAndReplaceText(wordApp, "<ContractDay>", Convert.ToString(contractData.Tables[0].Rows[0]["ContractDay"]), true);

            if (contractData.Tables[0].Columns.Contains("ContractMonth"))
                this.FindAndReplaceText(wordApp, "<ContractMonth>", Convert.ToString(contractData.Tables[0].Rows[0]["ContractMonth"]), true);

            if (contractData.Tables[0].Columns.Contains("ContractYear"))
                this.FindAndReplaceText(wordApp, "<ContractYear>", Convert.ToString(contractData.Tables[0].Rows[0]["ContractYear"]), true);

            if (contractData.Tables[0].Columns.Contains("ContractBeginDateYearLast2Digits"))
                this.FindAndReplaceText(wordApp, "<ContractBeginDateYearLast2Digits>", Convert.ToString(contractData.Tables[0].Rows[0]["ContractBeginDateYearLast2Digits"]), true);

        }

        private void FindAndReplaceText(Word.Application WordApp, object findText, object replaceWithText, bool checkIfExists = false)
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
            object replace = 2;
            object wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
            object replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
            WordApp.Selection.Range.HighlightColorIndex = Word.WdColorIndex.wdDarkRed;
            Word.Range rng = WordApp.Selection.Range;
            rng.Font.Color = Word.WdColor.wdColorBlue;

            bool isFound = true;

            if (checkIfExists)
            {
                object findStr = Convert.ToString(findText);

                isFound = WordApp.Selection.Find.Execute(
               ref findText,
               ref matchCase, ref matchWholeWord,
               ref matchWildCards, ref matchSoundsLike,
               ref nmatchAllWordForms, ref forward,
               ref wrap, ref format, ref matchKashida,
               ref matchDiacritics, ref matchAlefHamza,
               ref matchControl);
            }
            else
                isFound = true;

            //sonething to find

            if (isFound)
            {
                //if(WordApp.Selection.Find.Found(
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


        }

        private void FindAndReplaceServiceLevelItems(Word.Application WordApp, object findText, ref DataSet contractData)
        {
            bool isFound = true;
            object findStr = Convert.ToString(findText);
            isFound = WordApp.Selection.Find.Execute(ref findStr);

            if (isFound)
            {
                object missing = System.Reflection.Missing.Value;


                WordApp.Application.Selection.Find.ClearFormatting();
                WordApp.Application.Selection.Find.Text = (string)findText;
                WordApp.Application.Selection.Find.Execute(
                ref findText, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing);
                Word.Range SelectionRange = WordApp.Application.Selection.Range;
                if (StringManager.IsEqual(findText.ToString(), "<List of Service Level Items>") || StringManager.IsEqual(findText.ToString(), "<List of Service Level Items1>") || StringManager.IsEqual(findText.ToString(), "<List of Service Level Items2>"))
                {
                    Word.Table newTable;
                    newTable = WordApp.Application.Selection.Tables.Add(SelectionRange, 1, 8, ref missing, ref missing);

                    newTable.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone;
                    newTable.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone;
                    newTable.AllowAutoFit = true;
                    newTable.Rows.SetHeight(0f, Word.WdRowHeightRule.wdRowHeightAtLeast);
                    //newTable.Rows.Height = 0;
                    newTable.Cell(newTable.Rows.Count, 1).SetWidth(11f, Word.WdRulerStyle.wdAdjustNone);
                    newTable.Cell(newTable.Rows.Count, 2).SetWidth(130f, Word.WdRulerStyle.wdAdjustNone);
                    InvoiceSummaryHeader(newTable.Rows.Count, newTable);
                    int counter = 0;
                    foreach (DataRow row in contractData.Tables[1].Rows)
                    {
                        newTable.Rows.Add();
                        newTable.Cell(newTable.Rows.Count, 1).Range.Text = (counter + 1).ToString();
                        newTable.Cell(newTable.Rows.Count, 2).Range.Text = Convert.ToString(row["Hauler's Description"]);
                        newTable.Cell(newTable.Rows.Count, 3).Range.Text = Convert.ToString(row["Quantity Behaviour"]);
                        newTable.Cell(newTable.Rows.Count, 4).Range.Text = Convert.ToString(row["Quantity"]);
                        newTable.Cell(newTable.Rows.Count, 5).Range.Text = Convert.ToString(row["Frequency"]);
                        newTable.Cell(newTable.Rows.Count, 6).Range.Text = Convert.ToString(row["On"]);
                        newTable.Cell(newTable.Rows.Count, 7).Range.Text = Convert.ToString(row["($)/Item"]);
                        newTable.Cell(newTable.Rows.Count, 8).Range.Text = Convert.ToString(row["Total($)"]);
                        //newTable.Range.Rows[newTable.Rows.Count].Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalBottom;
                        //newTable.Range.ParagraphFormat.BaseLineAlignment = Word.WdBaselineAlignment.wdBaselineAlignCenter;
                        //newTable.Rows[newTable.Rows.Count].Alignment = Word.WdRowAlignment.wdAlignRowCenter;
                        // newTable.Rows.Alignment = Word.WdRowAlignment.wdAlignRowCenter;
                        //newTable.Cell(newTable.Rows.Count, j).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        //newTable.Cell(newTable.Rows.Count, 2).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalBottom;
                        if (counter == 0)
                        {
                            for (int j = 1; j < 9; j++)
                                newTable.Cell(newTable.Rows.Count, j).Range.Bold = 0;
                        }
                        for (int j = 1; j < 9; j++)
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
                        counter++;
                    }
                }
                else
                {
                    SelectionRange.Text = "";
                    if (!DataManager.IsNullOrEmptyDataTable(contractData.Tables[2]))
                    {
                        SelectionRange.Text = "•    Non-Chargeable Items";
                        foreach (DataRow dr in contractData.Tables[2].Rows)
                        {
                            SelectionRange.InsertAfter("\r");
                            SelectionRange.InsertAfter("    o   " + Convert.ToString(dr["Hauler's Description"]));
                        }
                    }
                }
            }

        }

        private static void InvoiceSummaryHeader(int i, Word.Table newTable)
        {
            newTable.Cell(i, 1).Range.Text = "";
            newTable.Cell(i, 2).Range.Text = "Hauler's Description";
            newTable.Cell(i, 3).Range.Text = "Billing Type";
            newTable.Cell(i, 4).Range.Text = "Quantity";
            newTable.Cell(i, 5).Range.Text = "Frequency";
            newTable.Cell(i, 6).Range.Text = "On";
            newTable.Cell(i, 7).Range.Text = "($)/Item";
            newTable.Cell(i, 8).Range.Text = "Total($)";
            for (int j = 1; j < 9; j++)
            {
                newTable.Cell(i, j).Range.Bold = 1;
                // newTable.Cell(i, j).VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalBottom;
            }
        }

        public string SendContractEmailtoNegotiatorSalesPerson(ContractApproval objContractApproval)
        {
            using (var objContractsDL = new ContractsDL(objVectorDB))
            {
                string emailResult = string.Empty;
                DataSet emailBodyData = objContractsDL.GetContractEmailDetailsForTransmit(objContractApproval.ContractId.ToString(), "ContractNegotiatorSalesPersonEmail");

                if (!DataManager.IsNullOrEmptyDataSet(emailBodyData))
                {
                    string ContractNo = emailBodyData.Tables[0].Rows[0]["ContractNo"].ToString();
                    string ContractNegotiator = emailBodyData.Tables[0].Rows[0]["ContractNegotiator"].ToString();
                    string ContractSalesPerson = emailBodyData.Tables[0].Rows[0]["ContractSalesPerson"].ToString();

                    EmailTemplate objEmailTemplate = EmailTemplateManager.GetEmailTemplate(AppDomain.CurrentDomain.BaseDirectory, EmailTemplates.Contracts,
                                                                    EmailTemplateGUIDs.ContractStatusToNegotiatorSP);

                    string contractStatus = string.Empty;

                    if (objContractApproval.ClientApproved == "Approved")
                        contractStatus = "Client Approved";
                    else if (objContractApproval.VendorApproved == "Approved" && objContractApproval.VectorApproved == "")
                        contractStatus = "Vendor Approved";
                    else
                        contractStatus = "Vector Approved";

                    string emailBody = string.Format(objEmailTemplate.EmailBody, ContractNo, contractStatus, DateTime.Now.ToString(), SecurityManager.GetConfigValue("VectorWebSite"));

                    string subject = string.Format(objEmailTemplate.Subject, ContractNo, "Status");

                    string logoPath = HttpContext.Current.Server.MapPath(VectorConstants.TildeSeparator.ToString());
                    logoPath += objEmailTemplate.EmailImagesList[default(int)].Path;


                    bool email = EmailManager.SendEmailWithMutipleAttachments((contractStatus == "Vendor Approved") ? ContractNegotiator : ContractSalesPerson,
                                                                    SecurityManager.GetConfigValue("FromEmailContract"), subject,
                                                                        emailBody, string.Empty, string.Empty, null, string.Empty, false, logoPath,
                                                                        smtpSection: SecurityManager.GetSmtpMailSection("ContractsMail"));
                    if (!email)
                    {
                        emailResult = "Failed";
                    }

                }
                else
                {
                    emailResult = "Failed";
                }

                return emailResult;

            }
        }

        public string SendContractVendorRejectedEmail(ContractApproval objContractApproval)
        {
            using (var objContractsDL = new ContractsDL(objVectorDB))
            {
                string emailResult = string.Empty;
                DataSet emailBodyData = objContractsDL.GetContractEmailDetailsForTransmit(objContractApproval.ContractId.ToString(), "ContractVendorRejectedEmail");

                if (!DataManager.IsNullOrEmptyDataSet(emailBodyData))
                {
                    string ContractNo = emailBodyData.Tables[0].Rows[0]["ContractNo"].ToString();
                    //string ContractNegotiator = emailBodyData.Tables[0].Rows[0]["ContractNegotiator"].ToString();
                    //string ContractSalesPerson = emailBodyData.Tables[0].Rows[0]["ContractSalesPerson"].ToString();
                    string ClientName = emailBodyData.Tables[0].Rows[0]["ClientName"].ToString();
                    string PropertyName = emailBodyData.Tables[0].Rows[0]["PropertyName"].ToString();
                    string VendorName = emailBodyData.Tables[0].Rows[0]["VendorName"].ToString();
                    string NegotiationNo = emailBodyData.Tables[0].Rows[0]["NegotiationNo"].ToString();
                    string Comments = objContractApproval.Comments;//emailBodyData.Tables[0].Rows[0]["NegotiationNo"].ToString();

                    EmailTemplate objEmailTemplate = EmailTemplateManager.GetEmailTemplate(AppDomain.CurrentDomain.BaseDirectory, EmailTemplates.Contracts,
                                                                    EmailTemplateGUIDs.ContractVendorRejectedEmail);

                    string contractStatus = string.Empty;

                    string emailBody = string.Format(objEmailTemplate.EmailBody, ContractNo, NegotiationNo, PropertyName, ClientName, VendorName, Comments);

                    string subject = string.Format(objEmailTemplate.Subject, ContractNo);

                    string logoPath = HttpContext.Current.Server.MapPath(VectorConstants.TildeSeparator.ToString());
                    logoPath += objEmailTemplate.EmailImagesList[default(int)].Path;


                    bool email = EmailManager.SendEmailWithMutipleAttachments(SecurityManager.GetConfigValue("ContractVendorRejectedTo"),
                                                                    SecurityManager.GetConfigValue("FromEmailContract"), subject,
                                                                        emailBody, SecurityManager.GetConfigValue("ContractVendorRejectedCC"),
                                                                        string.Empty, null, string.Empty, false, logoPath,
                                                                        smtpSection: SecurityManager.GetSmtpMailSection("ContractsMail"));
                    if (!email)
                    {
                        emailResult = "Failed";
                    }

                }
                else
                {
                    emailResult = "Failed";
                }

                return emailResult;

            }
        }

        public VectorResponse<object> UpcomingContractExpiry(UpcomingContractExpiry objUpcomingContractExpiry)
        {
            using (var objContractsDL = new ContractsDL(objVectorDB))
            {
                var result = objContractsDL.UpcomingContractExpiry(objUpcomingContractExpiry);
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

        public VectorResponse<object> GetTrueupPannelContractApprovalData(SearchEntity objSearchEntity,Int64 userId)
        {
            using (var objContractsDL = new ContractsDL(objVectorDB))
            {
                var result = objContractsDL.GetTrueupPannelContractApprovalData(objSearchEntity,userId);
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

    }
}
