using ExcelDataReader;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Garage.DataLayer;
using Vector.Garage.Entities;
using static Vector.Common.BusinessLayer.EmailTemplateEnums;

namespace Vector.Garage.BusinessLayer
{
    public class ClientBL : DisposeLogic
    {
        private VectorDataConnection objVectorDB;
        public ClientBL(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }


        public VectorResponse<object> ManageClientAddressInfo(ClientAddressInfo objClientAddressInfo, Int64 userId)
        {
            using (var objClientDL = new ClientDL(objVectorDB))
            {
                var result = objClientDL.ManageClientAddressInfo(objClientAddressInfo, userId);
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

        public VectorResponse<object> GetClientAddressInfo(ClientAddressInfoSearch objSearchClientAddress, Int64 userID)
        {
            using (var objClientDL = new ClientDL(objVectorDB))
            {
                var result = objClientDL.GetClientAddressInfo(objSearchClientAddress, userID);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "ClientAddressInfo";
                    result.Tables[1].TableName = "ActivityLog";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> VectorGetClientInvoicePreferencesBL(ClientInvoicePreferenceSearch objClientInvoicePreferenceSearch)
        {
            using (var objClientDL = new ClientDL(objVectorDB))
            {
                var result = objClientDL.VectorGetClientInvoicePreferencesDL(objClientInvoicePreferenceSearch);
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

        public VectorResponse<object> VectorManageClientInvoicePreferencesBL(ClientInvoicePreferences objClientInvoicePreferences)
        {
            using (var objClientDL = new ClientDL(objVectorDB))
            {
                var result = objClientDL.VectorManageClientInvoicePreferencesDL(objClientInvoicePreferences);
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

        public VectorResponse<object> VectorGetClientContactInfoBL(ClientContactInfoSearch objClientContactInfoSearch)
        {
            using (var objClientDL = new ClientDL(objVectorDB))
            {
                var result = objClientDL.VectorGetClientContactInfoDL(objClientContactInfoSearch);
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

        public VectorResponse<object> VectorManageClientRoleBL(ClientContactRole objClientContactRole)
        {
            using (var objClientDL = new ClientDL(objVectorDB))
            {
                var result = objClientDL.VectorManageClientRoleDL(objClientContactRole);
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

        public VectorResponse<object> VectorManageClientContactInfoBL(ClientContactInfo objClientContactInfo)
        {
            using (var objClientDL = new ClientDL(objVectorDB))
            {
                var result = objClientDL.VectorManageClientContactInfoDL(objClientContactInfo);
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

        public VectorResponse<object> VectorManageClientContractInfoBL(ClientContractInfo objClientContractInfo)
        {
            using (var objClientDL = new ClientDL(objVectorDB))
            {
                XDocument contractDocs = new XDocument(
                     new XElement("ROOT",
                     from clientDoc in objClientContractInfo.ClientDocuments
                     select new XElement("ClientContractDocs",
                                 //new XElement("ClientDocumentId", clientDoc.ClientDocumentId),
                                 //new XElement("ClientContractInfoDocumentsID", clientDoc.ClientContractInfoDocumentsID),
                                 new XElement("ClientId", clientDoc.ClientId),
                                 new XElement("DocumentName", clientDoc.DocumentName),
                                 new XElement("DocumentPath", clientDoc.DocumentPath),
                                 new XElement("Version", clientDoc.Version),
                                 new XElement("ChooseDocumentType", clientDoc.ChooseDocumentType),
                                 new XElement("Status", clientDoc.Status)
                     )));
                var result = objClientDL.VectorManageClientContractInfoDL(objClientContractInfo, contractDocs.ToString());
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

        public VectorResponse<object> GetClientContractInfo(Int64 clientID, Int64 taskId)
        {
            using (var objClientDL = new ClientDL(objVectorDB))
            {
                var result = objClientDL.GetClientContractInfo(clientID, taskId);
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

        public VectorResponse<object> ManageClientInfo(ClientInfo objClientInfo)
        {
            using (var clientsDL = new ClientDL(objVectorDB))
            {
                var result = clientsDL.ManageClientInfo(objClientInfo);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    string emailResult = string.Empty;
                    bool clientCreated = Convert.ToBoolean(result.Tables[0].Rows[0]["Result"].ToString());

                    if (clientCreated && (objClientInfo.ClientId == null || objClientInfo.ClientId == 0))
                    {
                        string toEmail = result.Tables[VectorConstants.Zero].Rows[0]["ToEmails"].ToString();
                        string ccEmail = result.Tables[VectorConstants.Zero].Rows[0]["CCEmails"].ToString();
                        string ActionType = result.Tables[VectorConstants.Zero].Rows[0]["Type"].ToString();

                        if (string.Equals(ActionType, "Add"))
                            emailResult = SendClientCreatonEmail(objClientInfo.ClientName, objClientInfo.ContractVersion, ccEmail, toEmail);
                    }

                    if (string.IsNullOrEmpty(emailResult))
                        return new VectorResponse<object>() { ResponseData = result };
                    else
                        return new VectorResponse<object>() { Error = new Error() { ErrorDescription = Convert.ToString(result.Tables[0].Rows[0]["ResultMessage"]) + "Failed to send email." } };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> GetClientsSearch(ClientInfoSearch clientInfoSearch)
        {
            using (var clientsDL = new ClientDL(objVectorDB))
            {
                var result = clientsDL.GetClientsSearch(clientInfoSearch);
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

        public VectorResponse<object> GetClientByClientInfoId(long clientId)
        {
            using (var clientsDL = new ClientDL(objVectorDB))
            {
                var result = clientsDL.GetClientByClientInfoId(clientId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "ClientInfo";
                    result.Tables[1].TableName = "ActivityLog";

                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }
        public VectorResponse<object> GetClientByClientId(long clientId)
        {
            using (var clientsDL = new ClientDL(objVectorDB))
            {
                var result = clientsDL.GetClientByClientId(clientId);
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

        public VectorResponse<object> GetClientAgreementInfo(ClientAgreementInfoSearch objSearchClientAgreement, Int64 userID)
        {
            using (var objClientDL = new ClientDL(objVectorDB))
            {
                var result = objClientDL.GetClientAgreementInfo(objSearchClientAgreement, userID);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "ClientAgreementInfo";
                    result.Tables[1].TableName = "ActivityLog";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> VectorManageClientRegion(Region objRegion)
        {
            using (var objClientDL = new ClientDL(objVectorDB))
            {
                var result = objClientDL.VectorManageClientRegion(objRegion);
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

        public VectorResponse<object> VectorManageClientVertical(Vertical objVertical)
        {
            using (var objClientDL = new ClientDL(objVectorDB))
            {
                var result = objClientDL.VectorManageClientVertical(objVertical);
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

        public VectorResponse<object> ManageClientAgreementInfo(ClientAgreementInfo objClientAgreementInfo, Int64 userId)
        {
            using (var objClientDL = new ClientDL(objVectorDB))
            {
                var result = objClientDL.ManageClientAgreementInfo(objClientAgreementInfo, userId);
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

        public VectorResponse<object> GetVectorViewClientInformation(string action, Int64 clientId)
        {
            using (var objClientDL = new ClientDL(objVectorDB))
            {
                var result = objClientDL.GetVectorViewClientInformation(action, clientId);
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

        private const string AddendumColumns = "select * from [Master Properties List$]  where F4 <> ''"; //
        public VectorResponse<object> VectotManageUDealPackage(DealPackage objDealPackage)
        {
            using (var objClientDL = new ClientDL(objVectorDB))
            {
                string filePath = string.Empty;
                //if (objDealPackage.IsTempFolder)
                //    filePath = HttpContext.Current.Server.MapPath(SecurityManager.GetConfigValue("FileServerTempPath") + objDealPackage.FolderName + "\\" + objDealPackage.FileName);
                //else
                //    filePath = HttpContext.Current.Server.MapPath(SecurityManager.GetConfigValue("FileServerPath") + objDealPackage.FolderName + "\\" + objDealPackage.FileName);

                if (objDealPackage.IsTempFolder)
                    filePath = SecurityManager.GetConfigValue("FileServerTempPath") + objDealPackage.FolderName + "\\" + objDealPackage.FileName;
                else
                    filePath = SecurityManager.GetConfigValue("FileServerPath") + objDealPackage.FolderName + "\\" + objDealPackage.FileName;


                DataSet fileData = ExcelToDataTable(filePath);

                string clientName = string.Empty;
                string ContractVersion = string.Empty;

                String dealSheetXML = GenerateDealSheetXML(fileData.Tables["Deal Sheet"]);
                String invoicePreferencesXML = GenerateInvoicePreferencesXML(fileData.Tables["Invoice Preferences"]);
                String propertyInfoListXML = GeneratePropertyInfoListXML(fileData.Tables["Property Info List"]);
                String propertyInvDistributionListXML = GeneratePropertyInvDistributionListXML(fileData.Tables["Property Inv Distribution List"]);
                String propertyBLInvoiceListXML = GeneratePropertyBLInvoiceListXML(fileData.Tables["Property BL Invoice List"]);


                //String dealSheetXML = GenerateDealSheetXML(GetExceldata(filePath, "select * from [Deal Sheet$]"));
                //String invoicePreferencesXML = GenerateInvoicePreferencesXML(GetExceldata(filePath, "select * from [Invoice Preferences$]"));
                //String propertyInfoListXML = GeneratePropertyInfoListXML(GetExceldata(filePath, "select * from [Property Info List$]"));
                //String propertyInvDistributionListXML = GeneratePropertyInvDistributionListXML(GetExceldata(filePath, "select * from [Property Inv Distribution List$]"));
                //String propertyBLInvoiceListXML = GeneratePropertyBLInvoiceListXML(GetExceldata(filePath, "select * from [Property BL Invoice List$]"));

                //String masterDataXML = GenerateMasterDataXML(GetExceldata(filePath, "select * from [Master Data$]"));
                //String haulerNamesXML = GenerateHaulerNamesXML(GetExceldata(filePath, "select * from [Hauler Names$]"));

                if (StringManager.IsEqual(dealSheetXML, "ERROR") || StringManager.IsEqual(invoicePreferencesXML, "ERROR") ||
                    StringManager.IsEqual(propertyInfoListXML, "ERROR") || StringManager.IsEqual(propertyInvDistributionListXML, "ERROR") ||
                    StringManager.IsEqual(propertyBLInvoiceListXML, "ERROR"))
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to Read File, Please contact Administrator." } };
                }
                else
                {
                    var result = objClientDL.ManageDealPackage(dealSheetXML, invoicePreferencesXML, propertyInfoListXML, propertyInvDistributionListXML,
                        propertyBLInvoiceListXML, objDealPackage);
                    if (!DataManager.IsNullOrEmptyDataSet(result))
                    {
                        if (StringManager.IsEqual(Convert.ToString(result.Tables[0].Rows[0]["Result"]), "1"))
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(result.Tables[0].Rows[0]["ClientNo"])))
                            {
                                clientName = result.Tables[0].Rows[0]["ClientName"].ToString();
                                ContractVersion = result.Tables[0].Rows[0]["ContractVersion"].ToString();

                                string tempFolderName = SecurityManager.GetConfigValue("FileServerTempPath") + objDealPackage.FolderName + "\\";
                                // string parentFolderName = SecurityManager.GetConfigValue("FileServerPath") + "Client//" + Convert.ToString(result.Tables[0].Rows[0]["ClientNo"]) + "//DealPackage" + "\\";
                                string parentFolderName = SecurityManager.GetConfigValue("FileServerPath") + "Client//" + Convert.ToString(result.Tables[0].Rows[0]["ClientNo"]) + "\\";
                                string toEmail = result.Tables[VectorConstants.Zero].Rows[0]["ToEmails"].ToString();
                                string ccEmail = result.Tables[VectorConstants.Zero].Rows[0]["CCEmails"].ToString();


                                string emailResult = string.Empty;
                                if (objDealPackage.ClientId == 0)
                                    emailResult = SendClientCreatonEmail(clientName, ContractVersion, ccEmail, toEmail);

                                if (FileManager.MoveFiles(tempFolderName, parentFolderName))
                                {
                                    if (string.IsNullOrEmpty(emailResult))
                                        return new VectorResponse<object>() { ResponseData = result };
                                    else
                                        return new VectorResponse<object>() { ResponseData = result };
                                }
                                else
                                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = Convert.ToString(result.Tables[0].Rows[0]["ResultMessage"]) + "But Deal Package File upload faild." } };
                            }
                            else
                            {
                                return new VectorResponse<object>() { Error = new Error() { ErrorDescription = Convert.ToString(result.Tables[0].Rows[0]["ResultMessage"]) + "But Deal Package File upload faild." } };
                            }
                        }
                        else
                            return new VectorResponse<object>() { Error = new Error() { ErrorDescription = Convert.ToString(result.Tables[0].Rows[0]["ResultMessage"]) } };
                    }
                    else
                    {
                        return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                    }
                }
            }
        }

        private string SendClientCreatonEmail(string clientName, string ClientBillingType, string ccEmail, string toEmail)
        {
            string Result = string.Empty;
            EmailTemplate objEmailTemplate = EmailTemplateManager.GetEmailTemplate(AppDomain.CurrentDomain.BaseDirectory, EmailTemplates.Common, EmailTemplateGUIDs.ClientCreationEmail);
            string emailBody = string.Format(objEmailTemplate.EmailBody, clientName, DateTime.Now.ToString(),
                                        SecurityManager.GetConfigValue("VectorWebSite"), ClientBillingType);

            string subject = string.Format(objEmailTemplate.Subject);

            string logoPath = HttpContext.Current.Server.MapPath(VectorConstants.TildeSeparator.ToString());
            logoPath += objEmailTemplate.EmailImagesList[default(int)].Path;



            bool emailResult = EmailManager.SendEmailWithMutipleAttachments(string.IsNullOrEmpty(toEmail) ? SecurityManager.GetConfigValue("ClientCreationTo") : toEmail,
                                                                            SecurityManager.GetConfigValue("FromEmail"), subject,
                                                                emailBody,
                                                                string.IsNullOrEmpty(ccEmail) ? SecurityManager.GetConfigValue("ClientCreationCC") : ccEmail,
                                                                SecurityManager.GetConfigValue("ClientCreationBcc"),
                                                                null, "", false, logoPath);
            if (!emailResult)
            {
                Result = "Failed";
            }
            return Result;
        }

        private string GenerateDealSheetXML(DataTable dealSheetList)
        {
            if (!DataManager.IsNullOrEmptyDataTable(dealSheetList))
            {
                try
                {
                    dealSheetList = dealSheetList.Rows
                                .Cast<DataRow>()
                                .Where(row => !row.ItemArray.All(field => field is DBNull ||
                                                                 string.IsNullOrWhiteSpace(field as string)))
                                .CopyToDataTable();
                    DataTable dealSheetData = new DataTable();

                    int i = 0;
                    DataColumn dcSp = null;
                    DataRow dealSheetListtRow = null;
                    foreach (DataRow dr in dealSheetList.Rows)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dr[0])) || !string.IsNullOrEmpty(Convert.ToString(dr[1])))
                        {
                            if (i != 0)
                            {
                                string columnName = StringManager.RemoveSpecialCharacters(Convert.ToString(dr[0]).ToUpper()).TrimStart(',').TrimEnd(',').Trim();
                                if (i > 40 & i < 49)
                                    columnName = "P" + columnName;
                                if (i > 49 & i < 58)
                                    columnName = "B" + columnName;
                                if (i == 59 && StringManager.IsNotEqual(columnName, "GOTONEXTSTEP"))
                                {
                                    dealSheetData.Rows[0].BeginEdit();
                                    dealSheetListtRow["COMMENTSANDNOTES"] = Convert.ToString(dr[0]).Trim();
                                    dealSheetData.Rows[0].EndEdit();
                                }
                                else
                                {
                                    if (StringManager.IsNotEqual(columnName, "GOTONEXTSTEP"))
                                    {
                                        dcSp = new DataColumn(columnName);
                                        dealSheetData.Columns.Add(dcSp);
                                        if (dealSheetData.Rows.Count == 0)
                                        {
                                            dealSheetListtRow = dealSheetData.NewRow();
                                            dealSheetData.Rows.Add(dealSheetListtRow);
                                        }
                                        dealSheetData.Rows[0].BeginEdit();
                                        dealSheetListtRow[i - 1] = dr[1].ToString().Trim();
                                        dealSheetData.Rows[0].EndEdit();
                                    }
                                }
                            }
                        }
                        i++;
                    }


                    DataSet ds = new DataSet("ROOT");
                    ds.Tables.Add(dealSheetData);
                    ds.Tables[0].TableName = "DEALSHEET";
                    return ds.GetXml();
                }
                catch (Exception ex)
                {
                    return "ERROR";
                }
            }
            return "ERROR";
        }

        public string GeneratePropertyBLInvoiceListXML(DataTable propertyBLInvoiceList)
        {
            if (!DataManager.IsNullOrEmptyDataTable(propertyBLInvoiceList))
            {
                try
                {
                    DataTable propertyBLInvoiceListData = new DataTable();

                    int i = 0;
                    foreach (DataRow dr in propertyBLInvoiceList.Rows)
                    {
                        if (i == 0)
                        {
                            DataColumn propertyName = new DataColumn(Convert.ToString(dr[0]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                            DataColumn halurName = new DataColumn(Convert.ToString(dr[1]).ToUpper().Replace(" ", "").Replace("(", "").Replace(")", "").TrimStart(',').TrimEnd(',').Trim());
                            DataColumn baselineAmount = new DataColumn(Convert.ToString(dr[2]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                            DataColumn franchise = new DataColumn(Convert.ToString(dr[3]).ToUpper().Replace(" ", "").Replace("(YES/NO)", "").TrimStart(',').TrimEnd(',').Trim());
                            DataColumn baselineInvoiceFileName = new DataColumn(Convert.ToString(dr[4]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());

                            propertyBLInvoiceListData.Columns.Add(propertyName);
                            propertyBLInvoiceListData.Columns.Add(halurName);
                            propertyBLInvoiceListData.Columns.Add(baselineAmount);
                            propertyBLInvoiceListData.Columns.Add(franchise);
                            propertyBLInvoiceListData.Columns.Add(baselineInvoiceFileName);
                        }
                        if (i > 0)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(dr[0])) || !string.IsNullOrEmpty(Convert.ToString(dr[1])) || !string.IsNullOrEmpty(Convert.ToString(dr[2])) ||
                                !string.IsNullOrEmpty(Convert.ToString(dr[3])) || !string.IsNullOrEmpty(Convert.ToString(dr[4])))
                            {
                                DataRow propertyBLInvoiceListRow = propertyBLInvoiceListData.NewRow();
                                propertyBLInvoiceListRow[0] = Convert.ToString(dr[0]).Trim();
                                propertyBLInvoiceListRow[1] = Convert.ToString(dr[1]).Trim();
                                propertyBLInvoiceListRow[2] = Convert.ToString(dr[2]).Trim();
                                propertyBLInvoiceListRow[3] = Convert.ToString(dr[3]).Replace("Yes", "True").Trim().Replace("No", "False").Trim();
                                propertyBLInvoiceListRow[4] = Convert.ToString(dr[4]).Trim();
                                propertyBLInvoiceListData.Rows.Add(propertyBLInvoiceListRow);
                            }
                        }

                        i++;
                    }

                    DataSet ds = new DataSet("ROOT");
                    ds.Tables.Add(propertyBLInvoiceListData);
                    ds.Tables[0].TableName = "PROPERTYBLINVOICELIST";
                    return ds.GetXml();
                }
                catch (Exception)
                {
                    return "ERROR";
                }
            }
            return "ERROR";
        }

        public string GenerateInvoicePreferencesXML(DataTable invoicePreferencesList)
        {
            if (!DataManager.IsNullOrEmptyDataTable(invoicePreferencesList))
            {
                try
                {
                    DataTable invoicePreferencesData = new DataTable();

                    int i = 0;
                    foreach (DataRow dr in invoicePreferencesList.Rows)
                    {
                        if (i == 12)
                        {
                            DataColumn combinedInvoice = new DataColumn(Convert.ToString(dr[2]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                            DataColumn vendorInvoice = new DataColumn(Convert.ToString(dr[3]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                            DataColumn v97Invoice = new DataColumn(Convert.ToString(dr[4]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                            DataColumn name = new DataColumn(Convert.ToString(dr[5]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                            DataColumn phone = new DataColumn(Convert.ToString(dr[6]).ToUpper().Replace("(XXXXXXXXXX)", "").TrimStart(',').TrimEnd(',').Trim());
                            DataColumn title = new DataColumn(Convert.ToString(dr[7]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                            DataColumn email = new DataColumn(Convert.ToString(dr[8]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                            DataColumn v97ClientAgreementBillingType = new DataColumn("V97CLIENTAGREEMENTBILLINGTYPE");
                            DataColumn invoiceType = new DataColumn("INVOICETYPE");
                            DataColumn bulkInvoiceonSchedule = new DataColumn("BULKINVOICEONSCHEDULE");
                            DataColumn deliveryFrequency = new DataColumn("DELIVERYFREQUENCY");
                            DataColumn deliveryDay = new DataColumn("DELIVERYDAY");
                            DataColumn invoiceDistribution = new DataColumn("INVOICEDISTRIBUTION");
                            DataColumn modeofReceipt = new DataColumn("MODEOFRECEIPT");

                            invoicePreferencesData.Columns.Add(combinedInvoice);
                            invoicePreferencesData.Columns.Add(vendorInvoice);
                            invoicePreferencesData.Columns.Add(v97Invoice);
                            invoicePreferencesData.Columns.Add(name);
                            invoicePreferencesData.Columns.Add(phone);
                            invoicePreferencesData.Columns.Add(title);
                            invoicePreferencesData.Columns.Add(email);
                            invoicePreferencesData.Columns.Add(v97ClientAgreementBillingType);
                            invoicePreferencesData.Columns.Add(invoiceType);
                            invoicePreferencesData.Columns.Add(bulkInvoiceonSchedule);
                            invoicePreferencesData.Columns.Add(deliveryFrequency);
                            invoicePreferencesData.Columns.Add(deliveryDay);
                            invoicePreferencesData.Columns.Add(invoiceDistribution);
                            invoicePreferencesData.Columns.Add(modeofReceipt);
                        }
                        if (i > 12 & i < 25)
                        {
                            if ((!string.IsNullOrEmpty(Convert.ToString(dr[2])) && !string.IsNullOrEmpty(Convert.ToString(dr[3])) && !string.IsNullOrEmpty(Convert.ToString(dr[4]))) ||
                                !string.IsNullOrEmpty(Convert.ToString(dr[5])) || !string.IsNullOrEmpty(Convert.ToString(dr[6])) || !string.IsNullOrEmpty(Convert.ToString(dr[7]))
                                || !string.IsNullOrEmpty(Convert.ToString(dr[8])))
                            {
                                DataRow invoicePreferencesRow = invoicePreferencesData.NewRow();
                                invoicePreferencesRow[0] = Convert.ToString(dr[2]).Trim();
                                invoicePreferencesRow[1] = Convert.ToString(dr[3]).Trim();
                                invoicePreferencesRow[2] = Convert.ToString(dr[4]).Trim();
                                invoicePreferencesRow[3] = Convert.ToString(dr[5]).Trim();
                                invoicePreferencesRow[4] = Convert.ToString(dr[6]).Trim();
                                invoicePreferencesRow[5] = Convert.ToString(dr[7]).Trim();
                                invoicePreferencesRow[6] = Convert.ToString(dr[8]).Trim();

                                invoicePreferencesRow[7] = Convert.ToString(invoicePreferencesList.Rows[1][2].ToString().Trim()).Trim();
                                invoicePreferencesRow[8] = Convert.ToString(invoicePreferencesList.Rows[2][2].ToString().Trim()).Trim();
                                invoicePreferencesRow[9] = Convert.ToString(invoicePreferencesList.Rows[5][2].ToString().Trim()).Trim();
                                invoicePreferencesRow[10] = Convert.ToString(invoicePreferencesList.Rows[6][2].ToString().Trim()).Trim();
                                invoicePreferencesRow[11] = Convert.ToString(invoicePreferencesList.Rows[7][2].ToString().Trim()).Trim();
                                invoicePreferencesRow[12] = Convert.ToString(invoicePreferencesList.Rows[8][2].ToString().Trim()).Trim();
                                invoicePreferencesRow[13] = Convert.ToString(invoicePreferencesList.Rows[9][2].ToString().Trim()).Trim();
                                invoicePreferencesData.Rows.Add(invoicePreferencesRow);
                            }
                        }
                        i++;
                    }

                    DataSet ds = new DataSet("ROOT");
                    ds.Tables.Add(invoicePreferencesData);
                    ds.Tables[0].TableName = "INVOICEPREFERENCES";
                    return ds.GetXml();
                }
                catch (Exception ex)
                {
                    return "ERROR";
                }
            }
            return "ERROR";
        }

        public string GeneratePropertyInfoListXML(DataTable propertyBLInvoiceList)
        {
            if (!DataManager.IsNullOrEmptyDataTable(propertyBLInvoiceList))
            {
                try
                {
                    DataTable propertyInfoListData = new DataTable();

                    //"Date Added(MM / DD / YYYY)"	Addendum #	Ownership Transfer	Transferred From - Client Name (exact spelling in PR)	
                    //Transfer Type	Currency Type	Property Name	Ledger#	Legal Name	Service Address	City	State	Zip Code	
                    //Total Units/Lots	Occupied Units/Lots	S/F of Commercial	RV Units/Lots	Seasonal (Y/N)	Name	Location	Title	"Phone(xxxxxxxxxx)"	Email	Salesperson 01	Salesperson 02

                    int i = 0;
                    foreach (DataRow dr in propertyBLInvoiceList.Rows)
                    {
                        if (i != 0)
                        {
                            if (i == 1)
                            {
                                DataColumn dateAdded = new DataColumn(Convert.ToString(dr[0]).Replace("(MM/DD/YYYY)", "").ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn addendum = new DataColumn(Convert.ToString(dr[1]).Replace("#", "").ToUpper().Replace(" ", "").Replace("(", "").Replace(")", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn ownershipTransfer = new DataColumn(Convert.ToString(dr[2]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn transferredFrom = new DataColumn(Convert.ToString(dr[3]).ToUpper().Replace("- CLIENT NAME (EXACT SPELLING IN PR)", "").Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn TransferType = new DataColumn(Convert.ToString(dr[4]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn CurrencyType = new DataColumn(Convert.ToString(dr[5]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn PropertyName = new DataColumn(Convert.ToString(dr[6]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn Ledger = new DataColumn(Convert.ToString(dr[7]).ToUpper().Replace("#", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn LegalName = new DataColumn(Convert.ToString(dr[8]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn ServiceAddress = new DataColumn(Convert.ToString(dr[9]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn City = new DataColumn(Convert.ToString(dr[10]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn State = new DataColumn(Convert.ToString(dr[11]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn ZipCode = new DataColumn(Convert.ToString(dr[12]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn TotalUnitsLots = new DataColumn(Convert.ToString(dr[13]).ToUpper().Replace("/", "").Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn OccupiedUnitsLots = new DataColumn(Convert.ToString(dr[14]).ToUpper().Replace("/", "").Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn SFofCommercial = new DataColumn(Convert.ToString(dr[15]).ToUpper().Replace("/", "").Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn RVUnitsLots = new DataColumn(Convert.ToString(dr[16]).ToUpper().Replace("/", "").Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn Seasonal = new DataColumn(Convert.ToString(dr[17]).ToUpper().Replace("(Y/N)", "").Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn Name = new DataColumn(Convert.ToString(dr[18]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn Location = new DataColumn(Convert.ToString(dr[19]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn Title = new DataColumn(Convert.ToString(dr[20]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn Phone = new DataColumn(Convert.ToString(dr[21]).ToUpper().Replace("(XXXXXXXXXX)", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn Email = new DataColumn(Convert.ToString(dr[22]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn Salesperson01 = new DataColumn(Convert.ToString(dr[23]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn Salesperson02 = new DataColumn(Convert.ToString(dr[24]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());

                                propertyInfoListData.Columns.Add(dateAdded);
                                propertyInfoListData.Columns.Add(addendum);
                                propertyInfoListData.Columns.Add(ownershipTransfer);
                                propertyInfoListData.Columns.Add(transferredFrom);
                                propertyInfoListData.Columns.Add(TransferType);
                                propertyInfoListData.Columns.Add(CurrencyType);
                                propertyInfoListData.Columns.Add(PropertyName);
                                propertyInfoListData.Columns.Add(Ledger);
                                propertyInfoListData.Columns.Add(LegalName);
                                propertyInfoListData.Columns.Add(ServiceAddress);
                                propertyInfoListData.Columns.Add(City);
                                propertyInfoListData.Columns.Add(State);
                                propertyInfoListData.Columns.Add(ZipCode);
                                propertyInfoListData.Columns.Add(TotalUnitsLots);
                                propertyInfoListData.Columns.Add(OccupiedUnitsLots);
                                propertyInfoListData.Columns.Add(SFofCommercial);
                                propertyInfoListData.Columns.Add(RVUnitsLots);
                                propertyInfoListData.Columns.Add(Seasonal);
                                propertyInfoListData.Columns.Add(Name);
                                propertyInfoListData.Columns.Add(Location);
                                propertyInfoListData.Columns.Add(Title);
                                propertyInfoListData.Columns.Add(Phone);
                                propertyInfoListData.Columns.Add(Email);
                                propertyInfoListData.Columns.Add(Salesperson01);
                                propertyInfoListData.Columns.Add(Salesperson02);
                            }

                            if (i > 1)
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(dr[0])) || !string.IsNullOrEmpty(Convert.ToString(dr[1])) || !string.IsNullOrEmpty(Convert.ToString(dr[2])) ||
                                    !string.IsNullOrEmpty(Convert.ToString(dr[3])) || !string.IsNullOrEmpty(Convert.ToString(dr[4])) ||
                                    !string.IsNullOrEmpty(Convert.ToString(dr[5])) || !string.IsNullOrEmpty(Convert.ToString(dr[6])) || !string.IsNullOrEmpty(Convert.ToString(dr[7])) ||
                                    !string.IsNullOrEmpty(Convert.ToString(dr[8])) || !string.IsNullOrEmpty(Convert.ToString(dr[9])) ||
                                    !string.IsNullOrEmpty(Convert.ToString(dr[10])) || !string.IsNullOrEmpty(Convert.ToString(dr[11])) || !string.IsNullOrEmpty(Convert.ToString(dr[12])) ||
                                    !string.IsNullOrEmpty(Convert.ToString(dr[13])) ||
                                    !string.IsNullOrEmpty(Convert.ToString(dr[14])) || !string.IsNullOrEmpty(Convert.ToString(dr[15])) ||
                                    !string.IsNullOrEmpty(Convert.ToString(dr[16])) || !string.IsNullOrEmpty(Convert.ToString(dr[17])) || !string.IsNullOrEmpty(Convert.ToString(dr[18])) ||
                                    !string.IsNullOrEmpty(Convert.ToString(dr[19])) || !string.IsNullOrEmpty(Convert.ToString(dr[20])) ||
                                    !string.IsNullOrEmpty(Convert.ToString(dr[21])) || !string.IsNullOrEmpty(Convert.ToString(dr[22])) || !string.IsNullOrEmpty(Convert.ToString(dr[23])) || !string.IsNullOrEmpty(Convert.ToString(dr[24])))
                                {
                                    DataRow propertyInfoListRow = propertyInfoListData.NewRow();
                                    propertyInfoListRow[0] = Convert.ToString(dr[0]).Trim();
                                    propertyInfoListRow[1] = Convert.ToString(dr[1]).Trim();
                                    propertyInfoListRow[2] = Convert.ToString(dr[2]).Trim();
                                    propertyInfoListRow[3] = Convert.ToString(dr[3]).Trim();
                                    propertyInfoListRow[4] = Convert.ToString(dr[4]).Trim();
                                    propertyInfoListRow[5] = Convert.ToString(dr[5]).Trim();
                                    propertyInfoListRow[6] = Convert.ToString(dr[6]).Trim();
                                    propertyInfoListRow[7] = Convert.ToString(dr[7]).Trim();
                                    propertyInfoListRow[8] = Convert.ToString(dr[8]).Trim();
                                    propertyInfoListRow[9] = Convert.ToString(dr[9]).Trim();
                                    propertyInfoListRow[10] = Convert.ToString(dr[10]).Trim();
                                    propertyInfoListRow[11] = Convert.ToString(dr[11]).Trim();
                                    propertyInfoListRow[12] = Convert.ToString(dr[12]).Trim();
                                    propertyInfoListRow[13] = Convert.ToString(dr[13]).Trim();
                                    propertyInfoListRow[14] = Convert.ToString(dr[14]).Trim();
                                    propertyInfoListRow[15] = Convert.ToString(dr[15]).Trim();
                                    propertyInfoListRow[16] = Convert.ToString(dr[16]).Trim();
                                    propertyInfoListRow[17] = Convert.ToString(dr[17]).Trim();
                                    propertyInfoListRow[18] = Convert.ToString(dr[18]).Trim();
                                    propertyInfoListRow[19] = Convert.ToString(dr[19]).Trim();
                                    propertyInfoListRow[20] = Convert.ToString(dr[20]).Trim();
                                    propertyInfoListRow[21] = Convert.ToString(dr[21]).Trim();
                                    propertyInfoListRow[22] = Convert.ToString(dr[22]).Trim();
                                    propertyInfoListRow[23] = Convert.ToString(dr[23]).Trim();
                                    propertyInfoListRow[24] = Convert.ToString(dr[24]).Trim();
                                    propertyInfoListData.Rows.Add(propertyInfoListRow);
                                }
                            }
                        }
                        i++;
                    }



                    DataSet ds = new DataSet("ROOT");
                    ds.Tables.Add(propertyInfoListData);
                    ds.Tables[0].TableName = "PROPERTYINFOLIST";
                    return ds.GetXml();
                }
                catch (Exception)
                {
                    return "ERROR";
                }
            }
            return "ERROR";
        }

        public string GeneratePropertyInvDistributionListXML(DataTable propertyInvDistributionList)
        {
            if (!DataManager.IsNullOrEmptyDataTable(propertyInvDistributionList))
            {
                try
                {
                    DataTable propertyInvDistributionListData = new DataTable();

                    int i = 0;
                    foreach (DataRow dr in propertyInvDistributionList.Rows)
                    {
                        if (i != 0)
                        {
                            if (i == 1)
                            {
                                DataColumn propertyName = new DataColumn(Convert.ToString(dr[0]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn billingAddress = new DataColumn(Convert.ToString(dr[1]).ToUpper().Replace(" ", "").Replace("(", "").Replace(")", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn city = new DataColumn(Convert.ToString(dr[2]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn state = new DataColumn(Convert.ToString(dr[3]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn zipCode = new DataColumn(Convert.ToString(dr[4]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn billingContactName = new DataColumn(Convert.ToString(dr[5]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn contactTitle = new DataColumn(Convert.ToString(dr[6]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn phone = new DataColumn(Convert.ToString(dr[7]).ToUpper().Replace("(XXXXXXXXXX)", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn email = new DataColumn(Convert.ToString(dr[8]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn combinedInvoice = new DataColumn(Convert.ToString(dr[9]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn rSInvoice = new DataColumn(Convert.ToString(dr[10]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn haulerInvoice = new DataColumn(Convert.ToString(dr[11]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                                DataColumn modeOfReceipt = new DataColumn(Convert.ToString(dr[12]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());

                                propertyInvDistributionListData.Columns.Add(propertyName);
                                propertyInvDistributionListData.Columns.Add(billingAddress);
                                propertyInvDistributionListData.Columns.Add(city);
                                propertyInvDistributionListData.Columns.Add(state);
                                propertyInvDistributionListData.Columns.Add(zipCode);

                                propertyInvDistributionListData.Columns.Add(billingContactName);
                                propertyInvDistributionListData.Columns.Add(contactTitle);
                                propertyInvDistributionListData.Columns.Add(phone);
                                propertyInvDistributionListData.Columns.Add(email);
                                propertyInvDistributionListData.Columns.Add(combinedInvoice);
                                propertyInvDistributionListData.Columns.Add(rSInvoice);
                                propertyInvDistributionListData.Columns.Add(haulerInvoice);
                                propertyInvDistributionListData.Columns.Add(modeOfReceipt);
                            }

                            if (i > 1)
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(dr[0])) || !string.IsNullOrEmpty(Convert.ToString(dr[1])) || !string.IsNullOrEmpty(Convert.ToString(dr[2])) ||
                                    !string.IsNullOrEmpty(Convert.ToString(dr[3])) || !string.IsNullOrEmpty(Convert.ToString(dr[4])) ||
                                    !string.IsNullOrEmpty(Convert.ToString(dr[5])) || !string.IsNullOrEmpty(Convert.ToString(dr[6])) || !string.IsNullOrEmpty(Convert.ToString(dr[7])) ||
                                    !string.IsNullOrEmpty(Convert.ToString(dr[8])) || !string.IsNullOrEmpty(Convert.ToString(dr[9])) ||
                                    !string.IsNullOrEmpty(Convert.ToString(dr[10])) || !string.IsNullOrEmpty(Convert.ToString(dr[11])) || !string.IsNullOrEmpty(Convert.ToString(dr[12])))
                                {
                                    DataRow propertyInvDistributionListRow = propertyInvDistributionListData.NewRow();
                                    propertyInvDistributionListRow[0] = Convert.ToString(dr[0]).Trim();
                                    propertyInvDistributionListRow[1] = Convert.ToString(dr[1]).Trim();
                                    propertyInvDistributionListRow[2] = Convert.ToString(dr[2]).Trim();
                                    propertyInvDistributionListRow[3] = Convert.ToString(dr[3]).Trim();
                                    propertyInvDistributionListRow[4] = Convert.ToString(dr[4]).Trim();

                                    propertyInvDistributionListRow[5] = Convert.ToString(dr[5]).Trim();
                                    propertyInvDistributionListRow[6] = Convert.ToString(dr[6]).Trim();
                                    propertyInvDistributionListRow[7] = Convert.ToString(dr[7]).Trim();
                                    propertyInvDistributionListRow[8] = Convert.ToString(dr[8]).Trim();
                                    propertyInvDistributionListRow[9] = Convert.ToString(dr[9]).Trim();

                                    propertyInvDistributionListRow[10] = Convert.ToString(dr[10]).Trim();
                                    propertyInvDistributionListRow[11] = Convert.ToString(dr[11]).Trim();
                                    propertyInvDistributionListRow[12] = Convert.ToString(dr[12]).Trim();
                                    propertyInvDistributionListData.Rows.Add(propertyInvDistributionListRow);
                                }
                            }
                        }

                        i++;
                    }



                    DataSet ds = new DataSet("ROOT");
                    ds.Tables.Add(propertyInvDistributionListData);
                    ds.Tables[0].TableName = "PROPERTYINVDISTRIBUTIONLIST";
                    return ds.GetXml();
                }
                catch (Exception)
                {
                    return "ERROR";
                }
            }
            return "ERROR";
        }

        public string GenerateMasterDataXML(DataTable propertyBLInvoiceList)
        {
            if (!DataManager.IsNullOrEmptyDataTable(propertyBLInvoiceList))
            {
                try
                {
                    DataTable propertyBLInvoiceListData = new DataTable();

                    int i = 0;
                    foreach (DataRow dr in propertyBLInvoiceList.Rows)
                    {
                        DataColumn propertyName = new DataColumn(Convert.ToString(dr[0]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                        DataColumn billingAddress = new DataColumn(Convert.ToString(dr[1]).ToUpper().Replace(" ", "").Replace("(", "").Replace(")", "").TrimStart(',').TrimEnd(',').Trim());
                        DataColumn city = new DataColumn(Convert.ToString(dr[2]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                        DataColumn state = new DataColumn(Convert.ToString(dr[3]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                        DataColumn zipCode = new DataColumn(Convert.ToString(dr[4]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                        DataColumn billingContactName = new DataColumn(Convert.ToString(dr[5]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                        DataColumn contactTitle = new DataColumn(Convert.ToString(dr[6]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                        DataColumn phone = new DataColumn(Convert.ToString(dr[7]).ToUpper().Replace("(XXXXXXXXXX)", "").TrimStart(',').TrimEnd(',').Trim());
                        DataColumn email = new DataColumn(Convert.ToString(dr[8]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                        DataColumn combinedInvoice = new DataColumn(Convert.ToString(dr[9]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                        DataColumn rSInvoice = new DataColumn(Convert.ToString(dr[10]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                        DataColumn haulerInvoice = new DataColumn(Convert.ToString(dr[11]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                        DataColumn modeOfReceipt = new DataColumn(Convert.ToString(dr[12]).ToUpper().Replace(" ", "").TrimStart(',').TrimEnd(',').Trim());
                        i++;
                    }



                    DataSet ds = new DataSet("ROOT");
                    ds.Tables.Add(propertyBLInvoiceListData);
                    ds.Tables[0].TableName = "MASTERDATA";
                    return ds.GetXml();
                }
                catch (Exception)
                {
                    return "ERROR";
                }
            }
            return "ERROR";
        }

        public string GenerateHaulerNamesXML(DataTable propertyBLInvoiceList)
        {
            if (!DataManager.IsNullOrEmptyDataTable(propertyBLInvoiceList))
            {
                try
                {
                    DataTable propertyBLInvoiceListData = new DataTable();

                    int i = 0;
                    foreach (DataRow dr in propertyBLInvoiceList.Rows)
                    {

                        i++;
                    }



                    DataSet ds = new DataSet("ROOT");
                    ds.Tables.Add(propertyBLInvoiceListData);
                    ds.Tables[0].TableName = "HAULERNAMES";
                    return ds.GetXml();
                }
                catch (Exception)
                {
                    return "ERROR";
                }
            }
            return "ERROR";
        }

        private string GenerateContractDetailsXML(ref DataTable addendumData)
        {
            XDocument doc = new XDocument();
            doc.Add(new XElement("ROOT"));
            int i = 0;
            foreach (DataRow dr in addendumData.Rows)
            {
                if (i > 0)
                {
                    string sp = string.Empty;
                    sp = dr["F49"].ToString() + "," + dr["F50"].ToString().Replace("0", "");
                    sp = sp.TrimStart(',').TrimEnd(',');
                    XElement data =
                      new XElement("PROP",
                            new XElement("DATEADDED", dr["F2"].ToString().Trim()),
                            new XElement("ADDENDUMNBR", dr["F3"].ToString().Trim()),
                            new XElement("CLIENTNAME", dr["F4"].ToString().Trim()),
                            new XElement("BILLINGTYPE", dr["F5"].ToString().Trim()),
                            new XElement("DISCOUNTPERCENT", dr["F6"].ToString().Trim().Replace("%", "")),
                            new XElement("OWNERSHIPTRANSFER", dr["F7"].ToString().Trim()),
                            new XElement("TRANSFERREDFROMCLIENT", dr["F8"].ToString().Trim()),
                        //new XElement("REGION", dr["F5"].ToString().Trim()),
                        //new XElement("LEDGER", dr["F6"].ToString()),
                        new XElement("PROPERTYCURRENCY", dr["F9"].ToString().Trim()),
                            //Property info
                            new XElement("PROPERTYNAME", dr["F11"].ToString().Trim()),
                            new XElement("LEDGER", dr["F12"].ToString()),
                            new XElement("LEGALNAME", dr["F13"].ToString().Trim()),
                            new XElement("SERVICEADDRESS", dr["F14"].ToString().Trim()),
                            new XElement("CITY", dr["F15"].ToString().Trim()),
                            new XElement("STATE", dr["F16"].ToString().Trim()),
                            new XElement("ZIPCODE", dr["F17"].ToString().PadLeft(5, '0').ToString().Trim()),
                            //Removed by marc on 24/8/2016
                            //new XElement("MANGAEMENTFEE", dr["F17"].ToString().Trim()),
                            new XElement("TOTALUNITS", dr["F18"].ToString().Trim()),
                            new XElement("OCCUPIEDUNITS", dr["F19"].ToString().Trim()),
                            new XElement("SFOFCOMMERCIAL", dr["F20"].ToString().Trim()),
                            new XElement("RVUNITSLOTS", dr["F21"].ToString().Trim()),
                             new XElement("SEASONAL", dr["F22"].ToString().Trim()),
                             //Property billingAddress
                             new XElement("PVSBADDRESS", dr["F24"].ToString().Trim()),
                            new XElement("BADDRESS", dr["F25"].ToString().Trim()),
                            new XElement("BCITY", dr["F26"].ToString().Trim()),
                            new XElement("BSTATE", dr["F27"].ToString().Trim()),
                            new XElement("BZIPCODE", dr["F28"].ToString().Trim()),
                             new XElement("BCNAME", dr["F29"].ToString().ToString().Trim()),
                            //new XElement("BCLOCATION", "Property"),
                            new XElement("BCTITLE", dr["F30"].ToString().Trim()),
                            new XElement("BCPHONE", FormatPhoneNbr(dr["F31"].ToString().Trim())),
                            new XElement("BCEMAIL", dr["F32"].ToString().Trim()),
                            new XElement("BCSUMMARYINVOICE", dr["F33"].ToString().Trim()),
                            new XElement("BCRSINVOICE", dr["F34"].ToString().Trim()),
                            new XElement("BCHAULERINVOICE", dr["F35"].ToString().Trim()),

                            //Primary Contact
                            new XElement("PCNAME", dr["F38"].ToString().ToString().Trim()),
                            new XElement("PCLOCATION", dr["F39"].ToString().Trim()),
                            new XElement("PCTITLE", dr["F40"].ToString().Trim()),
                            new XElement("PCPHONE", FormatPhoneNbr(dr["F41"].ToString().Trim())),
                            new XElement("PCEMAIL", dr["F42"].ToString().Trim()),
                            new XElement("PCSUMMARYINVOICE", dr["F43"].ToString().Trim()),
                            new XElement("PCRSINVOICE", dr["F44"].ToString().Trim()),
                            new XElement("PCHAULERINVOICE", dr["F45"].ToString().Trim()),

                            //Hauler Info
                            new XElement("HAULER", dr["F47"].ToString().Trim()),
                            new XElement("BASELINEAMOUNT", dr["F48"].ToString().Replace("$", "").Replace(",", "").Trim()),
                            new XElement("FRANCHISE", dr["F49"].ToString().Trim()),
                            new XElement("BASELINEINVOICE", dr["F50"].ToString().Replace(" ", "").Trim()),
                             //Sales Info.
                             new XElement("SALESPERSON1", dr["F53"].ToString().Trim()),
                             new XElement("SALESPERSON2", dr["F54"].ToString().Trim()),
                             new XElement("NOTES", dr["F55"].ToString().Trim()),
                             new XElement("BASELINETOTAL", dr["F56"].ToString().Trim()),
                            new XElement("SP", sp)
                            );
                    doc.Element("ROOT").Add(data);
                }
                i++;
            }

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlTextWriter.Create(sb, settings);
            doc.WriteTo(writer);
            writer.Flush();
            return sb.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "").ToString();
        }

        private const string dataSheetColumns = "Select * from [Deal Sheet$]";
        public string GenerateClientDataTable(string path)
        {
            DataTable dataSheet = GetExceldata(path, dataSheetColumns);
            if (!DataManager.IsNullOrEmptyDataTable(dataSheet))
            {
                try
                {
                    DataTable dtnew = new DataTable();
                    int i = 0;
                    int j = 0;
                    foreach (DataRow dr in dataSheet.Rows)
                    {
                        if (i < 61)
                        {
                            if (!string.IsNullOrEmpty(dr["F3"].ToString()))
                            {
                                StringBuilder columnName = new StringBuilder("C");
                                columnName.Append(i.ToString());
                                columnName.Append("_");
                                columnName.Append(Regex.Replace(dr["F3"].ToString(), "[^0-9a-zA-Z]+", ""));
                                DataColumn dc = new DataColumn(columnName.ToString().ToUpper());
                                dtnew.Columns.Add(dc);
                                if (dtnew.Rows.Count == 0)
                                {
                                    DataRow drNew = dtnew.NewRow();
                                    dtnew.Rows.Add(drNew);
                                }
                                dtnew.Rows[0].BeginEdit();
                                if (i == 4)
                                    if (StringManager.IsEqual(dr["F4"].ToString(), "Yes"))
                                    {
                                        dtnew.Rows[0][i] = dr["F4"].ToString().Replace("Yes", "True").Trim();
                                    }
                                    else
                                    {
                                        dtnew.Rows[0][i] = dr["F4"].ToString().Replace("No", "False").Trim();
                                    }
                                else if (i == 2)
                                {
                                    dtnew.Rows[0][i] = dr["F4"].ToString().Replace("%", "").Trim();
                                }
                                else if (i == 6)
                                {
                                    dtnew.Rows[0][i] = dr["F4"].ToString().Replace("$", "").Trim();
                                }
                                else if (i == 44)
                                {
                                    dtnew.Rows[0][i] = dr["F4"].ToString().Replace("%", "").Trim();
                                }
                                else
                                    dtnew.Rows[0][i] = dr["F4"].ToString().Trim();
                                dtnew.Rows[0].EndEdit();
                                i++;
                            }
                        }
                        j++;
                    }

                    //Add new columns like Hauler,Rs,Summary Frequency, Desc, approved by etc.

                    DataColumn dcSp = null;
                    dcSp = new DataColumn("SUMMARYINVOICEFREQUENCY");
                    dtnew.Columns.Add(dcSp);
                    dtnew.Rows[0].BeginEdit();
                    dtnew.Rows[0][i] = dataSheet.Rows[60]["F6"].ToString().Trim();
                    dtnew.Rows[0].EndEdit();
                    i++;

                    dcSp = new DataColumn("SUMMARYINVOICEFREQUENCYTERMDESC");
                    dtnew.Columns.Add(dcSp);
                    dtnew.Rows[0].BeginEdit();
                    dtnew.Rows[0][i] = dataSheet.Rows[60]["F8"].ToString().Trim();
                    dtnew.Rows[0].EndEdit();
                    i++;

                    dcSp = new DataColumn("RSINVOICEFREQUENCY");
                    dtnew.Columns.Add(dcSp);
                    dtnew.Rows[0].BeginEdit();
                    dtnew.Rows[0][i] = dataSheet.Rows[64]["F6"].ToString().Trim();
                    dtnew.Rows[0].EndEdit();
                    i++;

                    dcSp = new DataColumn("RSINVOICEFREQUENCYTERMDESC");
                    dtnew.Columns.Add(dcSp);
                    dtnew.Rows[0].BeginEdit();
                    dtnew.Rows[0][i] = dataSheet.Rows[64]["F8"].ToString().Trim();
                    dtnew.Rows[0].EndEdit();
                    i++;

                    dcSp = new DataColumn("HAULERINVOICEFREQUENCY");
                    dtnew.Columns.Add(dcSp);
                    dtnew.Rows[0].BeginEdit();
                    dtnew.Rows[0][i] = dataSheet.Rows[68]["F6"].ToString().Trim();
                    dtnew.Rows[0].EndEdit();
                    i++;

                    dcSp = new DataColumn("HAULERINVOICEFREQUENCYTERMDESC");
                    dtnew.Columns.Add(dcSp);
                    dtnew.Rows[0].BeginEdit();
                    dtnew.Rows[0][i] = dataSheet.Rows[68]["F8"].ToString().Trim();
                    dtnew.Rows[0].EndEdit();
                    i++;

                    //Add Special notes and Approved by 
                    dcSp = new DataColumn("SPECIALNOTES");
                    dtnew.Columns.Add(dcSp);
                    dtnew.Rows[0].BeginEdit();
                    dtnew.Rows[0][i] = dataSheet.Rows[73]["F3"].ToString().Trim();
                    dtnew.Rows[0].EndEdit();
                    i++;

                    dcSp = new DataColumn("APPROVEDBY");
                    dtnew.Columns.Add(dcSp);
                    dtnew.Rows[0].BeginEdit();
                    dtnew.Rows[0][i] = dataSheet.Rows[49]["F6"].ToString().Trim();
                    dtnew.Rows[0].EndEdit();
                    i++;

                    //DataColumn dcSp = null;
                    //dcSp = new DataColumn("RSOTHERCOMMENTS");
                    //dtnew.Columns.Add(dcSp);
                    //dtnew.Rows[0].BeginEdit();
                    //dtnew.Rows[0][i] = dataSheet.Rows[58]["F6"].ToString().Trim();
                    //dtnew.Rows[0].EndEdit();
                    //i++;

                    //dcSp = new DataColumn("RSDAY");
                    //dtnew.Columns.Add(dcSp);
                    //dtnew.Rows[0].BeginEdit();
                    //dtnew.Rows[0][i] = dataSheet.Rows[59]["F6"].ToString().Trim();
                    //dtnew.Rows[0].EndEdit();
                    //i++;

                    //dcSp = new DataColumn("HSOTHERCOMMENTS");
                    //dtnew.Columns.Add(dcSp);
                    //dtnew.Rows[0].BeginEdit();
                    //dtnew.Rows[0][i] = dataSheet.Rows[62]["F6"].ToString().Trim();
                    //dtnew.Rows[0].EndEdit();
                    //i++;

                    //dcSp = new DataColumn("HSDAY");
                    //dtnew.Columns.Add(dcSp);
                    //dtnew.Rows[0].BeginEdit();
                    //dtnew.Rows[0][i] = dataSheet.Rows[63]["F6"].ToString().Trim();
                    //dtnew.Rows[0].EndEdit();
                    //i++;

                    //dcSp = new DataColumn("SPECIALNOTES");
                    //dtnew.Columns.Add(dcSp);
                    //dtnew.Rows[0].BeginEdit();
                    //dtnew.Rows[0][i] = dataSheet.Rows[68]["F3"].ToString().Trim();
                    //dtnew.Rows[0].EndEdit();
                    //i++;

                    //dcSp = new DataColumn("APPROVEDBY");
                    //dtnew.Columns.Add(dcSp);
                    //dtnew.Rows[0].BeginEdit();
                    //dtnew.Rows[0][i] = dataSheet.Rows[44]["F6"].ToString().Trim();
                    //dtnew.Rows[0].EndEdit();


                    dtnew.Rows[0].BeginEdit();
                    dtnew.Rows[0][20] = FormatPhoneNbr(dtnew.Rows[0][20].ToString());
                    dtnew.Rows[0][28] = FormatPhoneNbr(dtnew.Rows[0][28].ToString());
                    dtnew.Rows[0][36] = FormatPhoneNbr(dtnew.Rows[0][36].ToString());
                    dtnew.Rows[0].EndEdit();

                    dtnew.AcceptChanges();
                    DataSet ds = new DataSet("ROOT");
                    ds.Tables.Add(dtnew);
                    ds.Tables[0].TableName = "CLIENT";
                    return ds.GetXml();
                }
                catch (Exception)
                {
                    return "ERROR";
                }
            }
            return "ERROR";
        }
        private string FormatPhoneNbr(string PhoneNbr)
        {
            if (!string.IsNullOrEmpty(PhoneNbr))
            {
                PhoneNbr = PhoneNbr.Replace("-", "");
                if (PhoneNbr.Length == 10)
                {
                    PhoneNbr = PhoneNbr.Substring(0, 3) + "-" + PhoneNbr.Substring(3, 3) + "-" + PhoneNbr.Substring(6, 4);
                }
            }
            return PhoneNbr;
        }
        public DataTable GetExceldata(string AddendaFilePath, string addendumColumns)
        {
            DataTable dt = new DataTable();
            try
            {

                OleDbConnection oledbConn;
                string connectionString = ExcelConnection(AddendaFilePath);
                // string connectionString = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=YES;IMEX=1;""", AddendaFilePath);
                oledbConn = new OleDbConnection(connectionString);
                // Open connection
                oledbConn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(addendumColumns, oledbConn);
                adapter.Fill(dt);
                oledbConn.Close();

            }
            catch (Exception ex)
            {
                dt = null;
            }

            return dt;
        }

        public DataSet ExcelToDataTable(String AddendaFilePath)
        {

            using (var stream = File.Open(AddendaFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (data) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });


                    return result;
                }
            }

        }

        private string ExcelConnection(string fileName)
        {
            return @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                   @"Data Source=" + fileName + ";" +
                   @"Extended Properties=" + Convert.ToChar(34).ToString() +
                   @"Excel 8.0" + Convert.ToChar(34).ToString() + ";";


        }

        public VectorResponse<object> VectorGetClientSearchBL(ClientInfo objClientInfo, Int64 userId)
        {
            using (var clientsDL = new ClientDL(objVectorDB))
            {
                var result = clientsDL.VectorGetClientSearchDL(objClientInfo, userId);
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

        public VectorResponse<object> GetClientDealPackageInfo(DealPackage objClientInfo)
        {
            using (var clientsDL = new ClientDL(objVectorDB))
            {
                var result = clientsDL.GetClientDealPackageInfo(objClientInfo);
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

        public VectorResponse<object> CompleteDealPackage(DealPackage objDealPackage, Int64 userId)
        {
            using (var objClientDL = new ClientDL(objVectorDB))
            {
                var result = objClientDL.CompleteDealPackage(objDealPackage.TaskId, userId);
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



        public VectorResponse<object> GetClientRelatedProperties(string action, Int64 clientId, Int64 userId)
        {
            using (var objClientDL = new ClientDL(objVectorDB))
            {
                var result = objClientDL.GetClientRelatedProperties(action, clientId, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "Properties";
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
