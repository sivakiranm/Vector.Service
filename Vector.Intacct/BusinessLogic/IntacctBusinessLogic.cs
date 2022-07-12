using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Vector.Common.BusinessLayer;
using Vector.Intacct.APIAccess;
using Vector.Intacct.Entities;
namespace Vector.Intacct.BusinessLogic
{
    public static class IntacctBusinessLogic
    {
        private delegate void SafeCallDelegate(string rowsToProcess, string rowsProcessed, string rowsRemaining,
                                       Label lblRowsToProcess, Label lblRowsprocessed, Label lblRowsRemaining);

        public static DataSet vectorData;
        static int totalRecords = Constants.Zero;
        static int iErrors = Constants.Zero;
        //static int iAdded = Constants.Zero;
        static int iUpdated = Constants.Zero;
        static int iUnchanged = Constants.Zero;

        /// <summary>
        /// Run Sync for Reach Intacct Entity Type (Customer, Property,Invoice)
        /// Move to Intacct Connector
        /// </summary>
        /// <param name="intacctDataType"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="processOption"></param>
        /// <param name="loggingOption"></param>
        /// <returns></returns>
        public static string RunProcessForEachType(SelectionEntity objSelectionEntity,
                                            List<function> objFunctionList)
        {
            StringBuilder logFileContent = new StringBuilder();
            //MasterService.ProRefuseServiceResponse res = new MasterService.ProRefuseServiceResponse();
            try
            {
                //Log Header in Log File 
                logFileContent.AppendLine(Environment.NewLine + Constants.SyncReportForEntity + objSelectionEntity.IntactType + Environment.NewLine);

                //Get refuse data for the dates provided in UI from the prorefuse database (We used query manager)
                var res = (VectorResponse<object>)GetReportData(objSelectionEntity.IntactType, objSelectionEntity.FromDate, objSelectionEntity.ToDate);
                if (ValidateResponse(res))
                {
                    //Prorefuse data as pkrmdata
                    vectorData = ValidateAndGetResponseData(res.Response.ToString());

                    if (!DataManager.IsNullOrEmptyDataSet(vectorData))
                    {
                        totalRecords = vectorData.Tables[Constants.Zero].Rows.Count;
                        //Add temporary column to the dataset 
                        vectorData.Tables[Constants.Zero].Columns.Add(Constants.IntacctId, typeof(string));

                        int totalRecordsCount = vectorData.Tables[Constants.Zero].Rows.Count;
                        int i = Constants.Zero;
                        DataSet Intacctds = null;

                        foreach (DataRow row in vectorData.Tables[Constants.Zero].Rows)
                        {
                            //run process for each record of prorefuse to compare/perform Update it with Intacct
                            string processedResult = RunProcessForEachRecord(objSelectionEntity.IntactType, objSelectionEntity.OperationType, ref logFileContent, row, objSelectionEntity.LoginObject, objFunctionList, Intacctds, objSelectionEntity);
                            if (StringManager.IsEqual(processedResult, Constants.SessionError))
                            {
                                using (ConnectionEntity objConnection = new ConnectionEntity())
                                {
                                    objConnection.UserId = SecurityManager.GetConfigValue(Constants.UserName);
                                    objConnection.UserPassword = SecurityManager.GetConfigValue(Constants.Password);
                                    objConnection.SenderId = SecurityManager.GetConfigValue(Constants.SenderId);
                                    objConnection.SenderPassword = SecurityManager.GetConfigValue(Constants.SenderPassword);
                                    objConnection.CompanyId = SecurityManager.GetConfigValue(Constants.CompanyId);
                                    objConnection.IntacctApiUrl = SecurityManager.GetConfigValue(Constants.ApiPath);

                                    IntacctLogin objLogin = IntacctConnector.EstablishIntacctConnection(objConnection);
                                    objSelectionEntity.LoginObject = null;
                                    objSelectionEntity.LoginObject = objLogin;
                                }

                            }

                            i++;

                            if (objSelectionEntity.IntactType == EnumManager.IntacctType.Customer)
                            {
                                SetRowProcessCounts(Convert.ToString(totalRecordsCount),
                                                    Convert.ToString(i),
                                                    Convert.ToString(totalRecordsCount - i),
                                         objSelectionEntity.lblCustomerToProcess, objSelectionEntity.lblCustomersProcessed, objSelectionEntity.lblCustomersRemaning);
                            }

                            else if (objSelectionEntity.IntactType == EnumManager.IntacctType.Project)
                            {

                                SetRowProcessCounts(Convert.ToString(totalRecordsCount),
                                                       Convert.ToString(i),
                                                       Convert.ToString(totalRecordsCount - i),
                                              objSelectionEntity.lblPropertiesToProcess, objSelectionEntity.lblPropertiesProcessed, objSelectionEntity.lblPropertiesRemaning);
                            }
                            else if (objSelectionEntity.IntactType == EnumManager.IntacctType.ArInvoice)
                            {

                                SetRowProcessCounts(Convert.ToString(totalRecordsCount),
                                                       Convert.ToString(i),
                                                       Convert.ToString(totalRecordsCount - i),
                                              objSelectionEntity.lblInvoiceToProcess, objSelectionEntity.lblInvoiceProcessed, objSelectionEntity.lblInvoiceRemaning);
                            }
                        }

                        if (objFunctionList != null && objFunctionList.Count > Constants.Zero)
                        {
                            if (objSelectionEntity.isBulk)
                                return PostDataBulk(objSelectionEntity, objFunctionList, ref logFileContent);
                        }
                        return logFileContent.ToString();
                    }
                    else
                        //Log into Log File : No Data Found
                        logFileContent.AppendLine(Common.GetNoDataFoundLog(objSelectionEntity.IntactType));

                    return logFileContent.ToString();
                }
                else
                {
                    if (res != null)
                    {
                        // Log into Log File : If response type is exception.
                        logFileContent.AppendLine(Common.GetExceptionResponceLog(res.Error.ErrorDescription.ToString(), objSelectionEntity.IntactType));
                    }
                    else
                    {
                        logFileContent.AppendLine(objSelectionEntity.IntactType + "  : No Data to Update/Compare.");
                    }
                }
            }
            catch (Exception ex)
            {
                logFileContent.AppendLine(Constants.Error + ex.Message + Environment.NewLine + Constants.SkippingRemaining + objSelectionEntity.IntactType.ToString());
                ErrorLog.GenerateErrorDetails(ex, string.Empty, string.Empty, string.Empty, Constants.TechnicalError);
                ErrorLog.LogIntacctExceptions(string.Empty, SecurityContext.Instance.LogInUserId,
                                                                 SecurityContext.Instance.LogInPassword,
                                                                 objSelectionEntity.IntactType.ToString(),
                                                                 string.Empty,
                                                                 objSelectionEntity.IntactType.ToString(),
                                                                 ex.Message,
                                                                 string.Empty,
                                                                 SecurityManager.GetIPAddress.ToString(),
                                                                 Constants.TechnicalError,
                                                                 string.Empty);
            }
            return logFileContent.ToString();
        }
        public static string PostDataBulk(SelectionEntity objSelectionEntity, List<function> objFunctionList, ref StringBuilder logFileContent)
        {
            string response = string.Empty;
            string appendResult = string.Empty;

            List<List<function>> listObjFunction = new List<List<function>>();

            for (int i = 0; i < objFunctionList.Count; i += Constants.Hundered)
            {

                listObjFunction.Add(objFunctionList.GetRange(i, Math.Min(Constants.Hundered, objFunctionList.Count - i)));
            }

            if (listObjFunction != null && listObjFunction.Count > 0)
            {
                foreach (List<function> lstFunction in listObjFunction)
                {
                    response = IntacctConnector.CreateUpdate(objSelectionEntity.LoginObject, lstFunction);
                    if (StringManager.IsNotEqual(response, Constants.SessionError))
                    {
                        appendResult = processUpdateResults(response, ref logFileContent, objSelectionEntity.IntactType, true,
                            string.Empty, string.Empty, string.Empty, objSelectionEntity);
                    }
                    if (StringManager.IsEqual(response, Constants.SessionError))
                    {

                        using (ConnectionEntity objConnection = new ConnectionEntity())
                        {
                            objConnection.UserId = SecurityManager.GetConfigValue(Constants.UserName);
                            objConnection.UserPassword = SecurityManager.GetConfigValue(Constants.Password);
                            objConnection.SenderId = SecurityManager.GetConfigValue(Constants.SenderId);
                            objConnection.SenderPassword = SecurityManager.GetConfigValue(Constants.SenderPassword);
                            objConnection.CompanyId = SecurityManager.GetConfigValue(Constants.CompanyId);
                            objConnection.IntacctApiUrl = SecurityManager.GetConfigValue(Constants.ApiPath);

                            IntacctLogin objLogin = IntacctConnector.EstablishIntacctConnection(objConnection);
                            objSelectionEntity.LoginObject = null;
                            objSelectionEntity.LoginObject = objLogin;
                        }

                    }
                }
            }
            return appendResult;

        }
        public static string PostDataSeQuential(SelectionEntity objSelectionEntity, List<function> newSequentialList, ref StringBuilder logFileContent)
        {
            string appendResult = string.Empty;
            string response = IntacctConnector.CreateUpdate(objSelectionEntity.LoginObject, newSequentialList);
            //Process the updated response to generate 
            if (StringManager.IsNotEqual(response, Constants.SessionError))
            {
                appendResult = processUpdateResults(response, ref logFileContent, objSelectionEntity.IntactType, true, string.Empty, string.Empty, string.Empty, objSelectionEntity);
            }
            if (StringManager.IsEqual(response, Constants.SessionError))
            {

                using (ConnectionEntity objConnection = new ConnectionEntity())
                {
                    objConnection.UserId = SecurityManager.GetConfigValue(Constants.UserName);
                    objConnection.UserPassword = SecurityManager.GetConfigValue(Constants.Password);
                    objConnection.SenderId = SecurityManager.GetConfigValue(Constants.SenderId);
                    objConnection.SenderPassword = SecurityManager.GetConfigValue(Constants.SenderPassword);
                    objConnection.CompanyId = SecurityManager.GetConfigValue(Constants.CompanyId);
                    objConnection.IntacctApiUrl = SecurityManager.GetConfigValue(Constants.ApiPath);

                    IntacctLogin objLogin = IntacctConnector.EstablishIntacctConnection(objConnection);
                    objSelectionEntity.LoginObject = null;
                    objSelectionEntity.LoginObject = objLogin;
                }

            }

            return response;
        }

        public static object GetReportData(EnumManager.IntacctType IntacctDataType, string fromDate, string toDate)
        {
            using (IntacctBOLayer objIntacctBOLayer = new IntacctBOLayer())
            {
                switch (IntacctDataType)
                {
                    case EnumManager.IntacctType.Customer:
                        return objIntacctBOLayer.GetCustomer(fromDate, toDate);
                    case EnumManager.IntacctType.Project:
                        return objIntacctBOLayer.GetProperty(fromDate, toDate, Constants.intacct);
                    case EnumManager.IntacctType.ArInvoice:
                        return objIntacctBOLayer.GetInvoice(fromDate, toDate, Constants.intacct);
                    default: return null;
                }
            }
        }

        public static DataSet ValidateAndGetResponseData(string response)
        {
            if (!string.IsNullOrEmpty(response))
            {
                using (StringReader stringReader = new StringReader(response))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(stringReader);
                    return ds;
                }
            }
            return null;
        }

        #region ValidateResponse

        public static bool ValidateResponse(VectorResponse<object> res)
        {
            if (res != null && StringManager.IsNotEqual(res.ResponseType, Constants.Exception))
            {
                if (!string.IsNullOrEmpty(res.Response.ToString()))
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
            return false;
        }

        public static string xmlErrorToString(System.Xml.XmlNode xmlNode)
        {

            if (xmlNode == null)
            {
                return Constants.MalformedError;
            }

            xmlNode = xmlNode.FirstChild;
            if (xmlNode == null)
            {
                return Constants.MalformedError;
            }
            string errorno = string.IsNullOrEmpty(xmlNode.SelectSingleNode(Constants.ErrorNo).InnerXml) ? string.Empty : xmlNode.SelectSingleNode(Constants.ErrorNo).InnerXml;
            string description = string.IsNullOrEmpty(xmlNode.SelectSingleNode(Constants.Description).InnerXml) ? string.Empty : xmlNode.SelectSingleNode(Constants.Description).InnerXml;
            string description2 = string.IsNullOrEmpty(xmlNode.SelectSingleNode(Constants.DescriptionTwo).InnerXml) ? string.Empty : xmlNode.SelectSingleNode(Constants.DescriptionTwo).InnerXml;
            string correction = string.IsNullOrEmpty(xmlNode.SelectSingleNode(Constants.Correction).InnerXml) ? string.Empty : xmlNode.SelectSingleNode(Constants.Correction).InnerXml;

            return description + description2 + correction;
        }


        #endregion

        #region Update Response

        /// <summary>
        /// From Response Sync IntacctId to ProRefuse
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string processUpdateResults(string response, ref System.Text.StringBuilder logFileContent, EnumManager.IntacctType IntacctType,
              bool logResponse = true, string selectionNodePath = "", string controlNodePath = "", string responseMessage = "", SelectionEntity ObjLogin = null)
        {
            XmlDocument simpleXml = new XmlDocument();
            simpleXml.LoadXml(response);
            string result = string.Empty;

            //read session id from selection entity.
            string sessionId = string.Empty;
            if (ObjLogin != null)
            {
                sessionId = ObjLogin.LoginObject.objConnectionEntity.SessionId.ToString();
            }

            if (simpleXml == null)
            {
                return Constants.InValidXmlResponse;
            }

            if (simpleXml.SelectSingleNode(Constants.ResponseOperationResultStatus) == null)
            {
                XmlNodeList nodeList = simpleXml.SelectNodes(Constants.ErrorResponse);
                StringBuilder addErrorDescriptions = new StringBuilder();
                string customerOrPropertyNumber = string.Empty;
                foreach (System.Xml.XmlNode node in nodeList)
                {
                    string dbresult = node.InnerText;
                    addErrorDescriptions.Append(dbresult);
                    result = Constants.UserFriendlyError;
                }
                //After reading all the errors send it to log file and db.
                string FormatString = addErrorDescriptions.ToString().Replace("'", "");
                ErrorLog.GenerateErrorDetails(response, string.Empty, string.Empty, string.Empty, Constants.FunctionalError);
                ErrorLog.LogIntacctExceptions(string.Empty, SecurityContext.Instance.LogInUserId,
                                                                                    SecurityContext.Instance.LogInPassword,
                                                                                    IntacctType.ToString(),
                                                                                    string.Empty,
                                                                                    IntacctType.ToString(),
                                                                                    FormatString,
                                                                                    sessionId,
                                                                                    SecurityManager.GetIPAddress.ToString(),
                                                                                    Constants.FunctionalError,
                                                                                    string.Empty);

            }
            else
            {
                // check to see if there's an error in the response
                string status = simpleXml.SelectSingleNode(Constants.ResponseOperationResultStatus).InnerXml;
                if (!string.IsNullOrEmpty(status))
                {
                    if (status != Constants.Success)
                    {
                        if (StringManager.IsNotEqual(IntacctType.ToString(), EnumManager.IntacctType.ArInvoice.ToString()))
                        {
                            //read customer or Property number from controlid for which it is throwing the error and log it 
                            XmlNodeList nodeList = simpleXml.SelectNodes(Constants.UniqueResponseNode);
                            string customerOrPropertyNumber = string.Empty;
                            foreach (System.Xml.XmlNode node in nodeList)
                            {
                                customerOrPropertyNumber = node.SelectSingleNode(Constants.controlid).InnerXml;
                                System.Xml.XmlNode error = simpleXml.SelectSingleNode(Constants.ResponseOperationResultErrorMessage);
                                result = Constants.Space + Constants.Error + Constants.Space + IntacctType.ToString() + Constants.Space + customerOrPropertyNumber + Constants.Space + xmlErrorToString(error);
                                string FormatString = result.Replace("'", "");
                                ErrorLog.GenerateErrorDetails(response, string.Empty, string.Empty, string.Empty, Constants.FunctionalError);
                                ErrorLog.LogIntacctExceptions(string.Empty, SecurityContext.Instance.LogInUserId,
                                                                                                    SecurityContext.Instance.LogInPassword,
                                                                                                    IntacctType.ToString(),
                                                                                                    string.Empty,
                                                                                                    IntacctType.ToString(),
                                                                                                    FormatString,
                                                                                                    sessionId,
                                                                                                    SecurityManager.GetIPAddress.ToString(),
                                                                                                    Constants.FunctionalError,
                                                                                                    string.Empty);
                            }
                        }
                        else
                        {
                            //read Invoice number from controlid for which it is throwing the error and log it 
                            XmlNodeList nodeList = simpleXml.SelectNodes(Constants.UniqueResponseNode);
                            string InvoiceNumber = string.Empty;
                            foreach (System.Xml.XmlNode node in nodeList)
                            {
                                InvoiceNumber = node.SelectSingleNode(Constants.controlid).InnerXml;
                                System.Xml.XmlNode error = simpleXml.SelectSingleNode(Constants.ResponseOperationResultErrorMessage);
                                result = Constants.Space + Constants.Error + Constants.Space + Constants.InvoiceNumber + Constants.Space + InvoiceNumber + Constants.Space + xmlErrorToString(error);
                                string FormatString = result.Replace("'", "");
                                ErrorLog.GenerateErrorDetails(response, string.Empty, string.Empty, string.Empty, Constants.FunctionalError);
                                ErrorLog.LogIntacctExceptions(string.Empty, SecurityContext.Instance.LogInUserId,
                                                                                                    SecurityContext.Instance.LogInPassword,
                                                                                                    IntacctType.ToString(),
                                                                                                    string.Empty,
                                                                                                    IntacctType.ToString(),
                                                                                                    FormatString,
                                                                                                    sessionId,
                                                                                                    SecurityManager.GetIPAddress.ToString(),
                                                                                                    Constants.FunctionalError,
                                                                                                    string.Empty);
                            }
                        }


                    }
                    else if (status == Constants.Success)
                    {
                        if (logResponse)
                        {
                            if (StringManager.IsNotEqual(IntacctType.ToString(), EnumManager.IntacctType.ArInvoice.ToString()))
                            {
                                LogResponseForCustomerAndProperty(logFileContent, IntacctType, simpleXml, Constants.UniqueResponseNode, sessionId);
                            }
                            else
                            {
                                LogResponseForInvoice(logFileContent, IntacctType, simpleXml, Constants.UniqueResponseNode);
                            }
                        }
                        else if (!string.IsNullOrEmpty(selectionNodePath) && !string.IsNullOrEmpty(controlNodePath) && !string.IsNullOrEmpty(responseMessage))
                        {
                            result = LogResponse(simpleXml, result, selectionNodePath, controlNodePath, responseMessage);
                        }
                    }
                }
            }
            string appendResult = logFileContent.Append(Environment.NewLine + result).ToString();
            return appendResult;
        }

        private static string LogResponse(XmlDocument simpleXml, string result, string selectionNodePath, string controlNodePath, string responseMessage)
        {

            string controlNodeValue = string.Empty;

            XmlNodeList controlnodeList = simpleXml.SelectNodes(controlNodePath);
            foreach (System.Xml.XmlNode node in controlnodeList)
            {
                controlNodeValue = node.InnerText;
            }


            string responseData = string.Empty;
            XmlNodeList KeynodeList = simpleXml.SelectNodes(selectionNodePath);

            foreach (System.Xml.XmlNode keynode in KeynodeList)
            {
                responseData = keynode.InnerText;
            }

            result = string.Format(responseMessage, responseData, controlNodeValue);
            return result;
        }

        private static void LogResponseForCustomerAndProperty(System.Text.StringBuilder logFileContent, EnumManager.IntacctType IntacctType,
            XmlDocument simpleXml, string nodeType, string sessionId = null)
        {
            //read each node in nodelist and seperate create and update customers and properties.
            XmlNodeList nodeList = simpleXml.SelectNodes(nodeType);
            string customerOrPropertyNo = string.Empty;
            string typeOfOperation = string.Empty;
            string controlId = string.Empty;
            string nmaeOfCustomerOrProperty = string.Empty;
            //MasterService.ProRefuseServiceResponse res = new MasterService.ProRefuseServiceResponse();
            foreach (System.Xml.XmlNode node in nodeList)
            {
                if (node.SelectSingleNode(Constants.key) == null)
                    customerOrPropertyNo = string.Empty;
                else
                    customerOrPropertyNo = node.SelectSingleNode(Constants.key).InnerXml.ToString();
                typeOfOperation = node.SelectSingleNode(Constants.function).InnerXml;
                controlId = node.SelectSingleNode(Constants.controlid).InnerXml;

                if (StringManager.IsEqual(typeOfOperation, "update_customer") || StringManager.IsEqual(typeOfOperation, "update_project"))
                {
                    //read the CustomerOrPropertyName and customerOrPropertyNo from controlid
                    logFileContent.AppendLine(string.Format(controlId + " : " + Constants.UpdatedSuccessfully));
                }

                else if (StringManager.IsEqual(typeOfOperation, "create_customer") || StringManager.IsEqual(typeOfOperation, "create_project"))
                {
                    string keyNo = "";


                    var query = from a in vectorData.Tables[Constants.Zero].AsEnumerable()
                                where StringManager.IsEqual(a.Field<string>(Constants.IntacctId), customerOrPropertyNo)
                                select a;
                    foreach (var IntacctId in query)
                    {
                        try
                        {

                            if (StringManager.IsEqual(typeOfOperation, "create_customer"))
                            {
                                keyNo = Convert.ToString(IntacctId["GroupNbr"]);
                            }
                            else if (StringManager.IsEqual(typeOfOperation, "create_project"))
                            {
                                keyNo = Convert.ToString(IntacctId["PropertyNo"]);
                            }

                                var res = (VectorResponse<object>)UpdateIntacctIdForReportData(IntacctType,
                                Convert.ToString(IntacctId[Constants.Zero]),
                                customerOrPropertyNo,
                                keyNo);



                            if (StringManager.IsEqual(res.ResponseMessage, Constants.SuccessTwo) || (Convert.ToString(res.ResponseData) == "Success"))
                            {
                                logFileContent.AppendLine(string.Format(customerOrPropertyNo + controlId + " : " + Constants.ANewRecordCreatedSuccessfully, res.ResponseType.ToString()));
                            }
                            else
                            {
                                iErrors++;
                                logFileContent.AppendLine(string.Format(Constants.ErrorUpdateInIntacct, res.Error.ErrorDescription.ToString()));
                                ErrorLog.LogIntacctExceptions(string.Empty, SecurityContext.Instance.LogInUserId,
                                                                                 SecurityContext.Instance.LogInPassword,
                                                                                 IntacctType.ToString(),
                                                                                 string.Empty,
                                                                                 IntacctType.ToString(),
                                                                                 res.Error.ErrorDescription.ToString(),
                                                                                 sessionId,
                                                                                 SecurityManager.GetIPAddress.ToString(),
                                                                                 Constants.FunctionalError,
                                                                                 string.Empty);

                            }

                        }
                        catch (Exception ex)
                        {
                            iErrors++;
                            logFileContent.AppendLine(string.Format(Constants.ErrorUpdateInIntacct, ex.Message.ToString()));
                            ErrorLog.LogIntacctExceptions(string.Empty, SecurityContext.Instance.LogInUserId,
                                                                             SecurityContext.Instance.LogInPassword,
                                                                             IntacctType.ToString(),
                                                                             string.Empty,
                                                                             IntacctType.ToString(),
                                                                             ex.Message,
                                                                             sessionId,
                                                                             SecurityManager.GetIPAddress.ToString(),
                                                                             Constants.TechnicalError,
                                                                             string.Empty);

                        }
                        finally
                        {
                            //Do Something.
                        }
                    }
                }
            }
        }

        private static void LogResponseForInvoice(System.Text.StringBuilder logFileContent, EnumManager.IntacctType IntacctType, XmlDocument simpleXml, string NodeForInvoices)
        {
            XmlNodeList nodeList = simpleXml.SelectNodes(NodeForInvoices);
            string InvoiceKey = string.Empty;
            string InvoiceNumber = string.Empty;
            string function = string.Empty;
            foreach (System.Xml.XmlNode node in nodeList)
            {
                function = node.SelectSingleNode(Constants.function).InnerXml;
                if (StringManager.IsEqual(function, "create_invoice"))
                    InvoiceKey = node.SelectSingleNode(Constants.key).InnerXml;
                if (StringManager.IsEqual(function, "create_bill"))
                    InvoiceKey = node.SelectSingleNode(Constants.key).InnerXml;
                InvoiceNumber = node.SelectSingleNode(Constants.controlid).InnerXml;

                //Writing a ne~w method to Update the intacct Id that we have got from Intacct after inserting a new record
                if (StringManager.IsEqual(function, "create_invoice"))
                {

                    //MasterService.ProRefuseServiceResponse res = new MasterService.ProRefuseServiceResponse();
                    var res = (VectorResponse<object>)UpdateIntacctIdForInvoice(Constants.intacct, InvoiceNumber, InvoiceKey);

                    if (StringManager.IsEqual(res.ResponseMessage, Constants.SuccessTwo) 
                        || res.Error == null)
                    {
                         
                        logFileContent.AppendLine(Constants.InvoiceLog + InvoiceNumber + ": Sync successfully to Intacct.(New)");
                    }
                    else
                    {

                        iErrors++;
                        logFileContent.AppendLine(string.Format(Constants.ErrorUpdateInIntacct, res.Error.ErrorDescription.ToString()));
                        ErrorLog.LogIntacctExceptions(string.Empty, SecurityContext.Instance.LogInUserId,
                                                                         SecurityContext.Instance.LogInPassword,
                                                                         IntacctType.ToString(),
                                                                         string.Empty,
                                                                         IntacctType.ToString(),
                                                                         res.Error.ErrorDescription.ToString(),
                                                                         string.Empty,
                                                                         SecurityManager.GetIPAddress.ToString(),
                                                                         Constants.FunctionalError,
                                                                         string.Empty);

                    }
                }
                else if (StringManager.IsEqual(function, "create_bill"))
                {
                    //MasterService.ProRefuseServiceResponse res = new MasterService.ProRefuseServiceResponse();
                    var res = (VectorResponse<object>)UpdateIntacctIdForBill(Constants.intacct, InvoiceNumber, InvoiceKey);

                    if (StringManager.IsEqual(res.ResponseMessage, Constants.Success) 
                         || res.Error == null)
                    {
                        logFileContent.AppendLine(Constants.InvoiceLog + InvoiceNumber + ": Sync successfully to Intacct.(New)");
                    }
                    else
                    {

                        iErrors++;
                        logFileContent.AppendLine(string.Format(Constants.ErrorUpdateInIntacct, res.Error.ErrorDescription.ToString()));
                        ErrorLog.LogIntacctExceptions(string.Empty, SecurityContext.Instance.LogInUserId,
                                                                         SecurityContext.Instance.LogInPassword,
                                                                         IntacctType.ToString(),
                                                                         string.Empty,
                                                                         IntacctType.ToString(),
                                                                         res.Error.ErrorDescription.ToString(),
                                                                         string.Empty,
                                                                         SecurityManager.GetIPAddress.ToString(),
                                                                         Constants.FunctionalError,
                                                                         string.Empty);

                    }
                }
                else
                {
                    logFileContent.AppendLine(Constants.InvoiceLog + InvoiceNumber + ": Sync Successful (Update).");
                }
            }
        }

        #region Update Intaact Id Back to Refuse

        public static object UpdateIntacctIdForReportData(EnumManager.IntacctType IntacctType, string companyKey, string IntacctId, string keyNo)
        {
            using (IntacctBOLayer objIntacctBOLayer = new IntacctBOLayer())
            {
                switch (IntacctType)
                {
                    case EnumManager.IntacctType.Customer:
                        return objIntacctBOLayer.UpdateReportDataByEntity("Customer", companyKey, null, keyNo, IntacctId);

                    case EnumManager.IntacctType.Project:
                        return objIntacctBOLayer.UpdateReportDataByEntity("Property", companyKey, null, keyNo, IntacctId);

                    default: return null;
                }
            }
        }
        public static object UpdateIntacctIdInRefuse(EnumManager.IntacctType IntacctType, string companyKey, string IntacctId)
        {
            using (IntacctBOLayer objIntacctBOLayer = new IntacctBOLayer())
            {
                return objIntacctBOLayer.UpdateIntacctId(IntacctType.ToString(), companyKey, IntacctId);
            }
        }


        public static object UpdateIntacctIdForInvoice(string IntacctType, string companyKey, string IntacctId)
        {
            using (IntacctBOLayer objIntacctBOLayer = new IntacctBOLayer())
            {
                return objIntacctBOLayer.UpdateInvoice(companyKey, IntacctId, IntacctType);
            }
        }

        public static object UpdateIntacctIdForBill(string IntacctType, string companyKey, string IntacctId)
        {
            using (IntacctBOLayer objIntacctBOLayer = new IntacctBOLayer())
            {
                return objIntacctBOLayer.UpdateIntacctIdForBill(companyKey, IntacctId, IntacctType);
            }
        }

        #endregion

        #endregion

        #region Run Process for each Record (Row by Row)

        /// <summary>
        /// Insert (or) Update for each record in the Intacct
        /// </summary>
        /// <param name="IntacctDataType"></param>
        /// <param name="processOption"></param>
        /// <param name="logFileContent"></param>
        /// <param name="row"></param>
        public static string RunProcessForEachRecord(EnumManager.IntacctType IntacctDataType, EnumManager.ProcessOptions processOption,
                                            ref StringBuilder logFileContent, DataRow row, IntacctLogin objLogin, List<function> objFunctionList, DataSet intacctData, SelectionEntity objSelectionEntity)
        {
            string InvoiceNumber = string.Empty;
            string pkRowString = IntacctConnector.GetPkString(IntacctDataType, row, ref logFileContent);
            string intacctString = Constants.Empty;
            string billString = Constants.Empty;

            if (StringManager.IsEqual(IntacctDataType.ToString(), "Customer") || StringManager.IsEqual(IntacctDataType.ToString(), "Project"))
            {
                intacctString = IntacctConnector.GetintacctString(IntacctDataType, Convert.ToString(row[EnumManager.Columns.IntacctIdFromRefuse.ToString()]), objLogin, ref logFileContent, intacctData);
            }
            else if (StringManager.IsEqual(IntacctDataType.ToString(), "ArInvoice"))
            {
                InvoiceNumber = Convert.ToString(row[EnumManager.Columns.InvoiceNo.ToString()]);
                intacctString = SyncEachRecord.GetInvoiceKeyBasedOnInvoiceNo(InvoiceNumber, string.Empty, objLogin, "Compare");
                billString = SyncEachRecord.GetInvoiceKeyBasedOnInvoiceNo(InvoiceNumber, string.Empty, objLogin, "GetbillKey");
                if (StringManager.IsEqual(intacctString, Constants.SessionError))
                {
                    return Constants.SessionError;
                }
            }
            if (StringManager.IsNotEqual(intacctString, Constants.Skip))
            {
                bool equalFlag = false;

                if (StringManager.IsEqual(pkRowString, intacctString))
                    equalFlag = true;
                else
                    equalFlag = false;
                if (StringManager.IsEqual(IntacctDataType.ToString(), "ArInvoice"))
                {
                    string BillingType = row["BillingType"].ToString();
                    logFileContent = InvocieOperationsOnly(IntacctDataType, processOption, logFileContent, row, objLogin, objFunctionList, objSelectionEntity, pkRowString, BillingType, intacctString, billString, equalFlag);
                }
                else
                {
                    logFileContent = CustomerAndPropertyOperationOnly(IntacctDataType, processOption, logFileContent, row, objLogin, objFunctionList, objSelectionEntity, pkRowString, intacctString, equalFlag);
                }
            }
            else
            {
                if (StringManager.IsEqual(processOption.ToString(), EnumManager.ProcessOptions.Compare.ToString()))
                {
                    //Get Datatable related to the IntacctId;
                    intacctString = SyncEachRecord.GetInvoiceKeyBasedOnInvoiceNo(InvoiceNumber, string.Empty, objLogin, "Compare", Constants.Yes);
                    String IntacctId = Convert.ToString(row[EnumManager.Columns.IntacctIdFromRefuse.ToString()]);
                    RunProcessForIntacctRecord(IntacctDataType, processOption, ref logFileContent, row, pkRowString, intacctString, objLogin, objFunctionList);
                }
                else
                {
                    logFileContent.AppendLine(Constants.InvoiceLog + InvoiceNumber + Constants.Cannotupdate);
                }
            }
            return string.Empty;
        }

        private static StringBuilder InvocieOperationsOnly(EnumManager.IntacctType IntacctDataType, EnumManager.ProcessOptions processOption, StringBuilder logFileContent, DataRow row, IntacctLogin objLogin, List<function> objFunctionList, SelectionEntity objSelectionEntity, string pkRowString, string BillingType, string intacctString, string billString, bool equalFlag)
        {
            if (StringManager.IsEqual(processOption.ToString(), EnumManager.ProcessOptions.Compare.ToString()))
            {
                String IntacctId = Convert.ToString(row[EnumManager.Columns.IntacctIdFromRefuse.ToString()]);
                RunProcessForIntacctRecord(IntacctDataType, processOption, ref logFileContent, row, pkRowString, intacctString, objLogin, objFunctionList);
            }

            else if (StringManager.IsEqual(processOption.ToString(), EnumManager.ProcessOptions.Update.ToString()) &&
                !string.IsNullOrEmpty(intacctString.ToString()) && !string.IsNullOrEmpty(billString.ToString()) && equalFlag == false)
            {
                string GroupNbr = row[Constants.GroupNumber].ToString();
                row[Constants.IntacctId] = GroupNbr;
                UpdateRecordInIntacctDataBase(ref logFileContent, row, IntacctDataType, objLogin, objFunctionList, objSelectionEntity);
                UpdateBillRecordOfInvoice(ref logFileContent, row, IntacctDataType, objLogin, objFunctionList, objSelectionEntity);
            }
            else if (StringManager.IsEqual(processOption.ToString(), EnumManager.ProcessOptions.Update.ToString()) &&
           !string.IsNullOrEmpty(intacctString.ToString()) && string.IsNullOrEmpty(billString.ToString()) && equalFlag == false)
            {
                string GroupNbr = row[Constants.GroupNumber].ToString();
                if (!string.IsNullOrEmpty(GroupNbr))
                {
                    row[Constants.IntacctId] = GroupNbr;
                }
                if (StringManager.IsEqual(BillingType, Constants.BillPayRsHauler) || StringManager.IsEqual(BillingType, Constants.BillPaySavingsShare))
                {
                    UpdateRecordInIntacctDataBase(ref logFileContent, row, IntacctDataType, objLogin, objFunctionList, objSelectionEntity);
                    BusinessLayer.CreateMissingEntity(row, objFunctionList, objSelectionEntity, ref logFileContent, intacctString, billString);
                }
                else
                {
                    BusinessLayer.UpdateInvoiceDirectpay(row, objLogin, objFunctionList, objSelectionEntity, ref logFileContent);
                }
            }
            else if (StringManager.IsEqual(processOption.ToString(), EnumManager.ProcessOptions.Update.ToString()) &&
                    string.IsNullOrEmpty(intacctString.ToString()) && !string.IsNullOrEmpty(billString.ToString()) && equalFlag == false)
            {
                string GroupNbr = row[Constants.GroupNumber].ToString();
                if (!string.IsNullOrEmpty(GroupNbr))
                {
                    row[Constants.IntacctId] = GroupNbr;
                }
                if (StringManager.IsEqual(BillingType, Constants.BillPayRsHauler) || StringManager.IsEqual(BillingType, Constants.BillPaySavingsShare))
                {
                    UpdateRecordInIntacctDataBase(ref logFileContent, row, IntacctDataType, objLogin, objFunctionList, objSelectionEntity);
                    BusinessLayer.CreateMissingEntity(row, objFunctionList, objSelectionEntity, ref logFileContent, intacctString, billString);
                }
            }
            else if (StringManager.IsEqual(processOption.ToString(), EnumManager.ProcessOptions.Update.ToString()) &&
           string.IsNullOrEmpty(intacctString.ToString()) && string.IsNullOrEmpty(billString.ToString()))
            {
                CreateNewEntity(row, objFunctionList, IntacctDataType, ref logFileContent, objSelectionEntity);
            }
            else if (StringManager.IsEqual(processOption.ToString(), EnumManager.ProcessOptions.Update.ToString()) && !string.IsNullOrEmpty(intacctString.ToString()) && equalFlag == true)
            {
                LogNoChangesInFile(IntacctDataType, logFileContent, row, pkRowString, intacctString);
            }
            return logFileContent;
        }
        private static StringBuilder CustomerAndPropertyOperationOnly(EnumManager.IntacctType IntacctDataType, EnumManager.ProcessOptions processOption, StringBuilder logFileContent, DataRow row, IntacctLogin objLogin, List<function> objFunctionList, SelectionEntity objSelectionEntity, string pkRowString, string intacctString, bool equalFlag)
        {
            if (StringManager.IsEqual(processOption.ToString(), EnumManager.ProcessOptions.Compare.ToString()))
            {
                //Get Datatable related to the IntacctId;
                String IntacctId = Convert.ToString(row[EnumManager.Columns.IntacctIdFromRefuse.ToString()]);
                RunProcessForIntacctRecord(IntacctDataType, processOption, ref logFileContent, row, pkRowString, intacctString, objLogin, objFunctionList);
            }

            else if (StringManager.IsEqual(processOption.ToString(), EnumManager.ProcessOptions.Update.ToString()) &&
                !string.IsNullOrEmpty(intacctString.ToString()) && equalFlag == false)
            {
                string GroupNbr = row[Constants.GroupNumber].ToString();
                row[Constants.IntacctId] = GroupNbr;
                UpdateRecordInIntacctDataBase(ref logFileContent, row, IntacctDataType, objLogin, objFunctionList, objSelectionEntity);
            }
            else if (StringManager.IsEqual(processOption.ToString(), EnumManager.ProcessOptions.Update.ToString()) && !string.IsNullOrEmpty(intacctString.ToString()) && equalFlag == true)
            {
                LogNoChangesInFile(IntacctDataType, logFileContent, row, pkRowString, intacctString);
            }
            else
            {
                CreateNewEntity(row, objFunctionList, IntacctDataType, ref logFileContent, objSelectionEntity);
            }
            return logFileContent;
        }

        private static void UpdateBillRecordOfInvoice(ref StringBuilder logFileContent, DataRow pkrmRow, EnumManager.IntacctType IntacctDataType, IntacctLogin objLogin, List<function> objFunctionList, SelectionEntity objSelectionEntity)
        {
            string IntacctBillId = pkrmRow[Constants.IntacctBillId].ToString();
            string BillingType = pkrmRow[Constants.BillingType].ToString();
            if (!string.IsNullOrEmpty(IntacctBillId))
            {
                //Update Bill depending on the bill number
                if (StringManager.IsEqual(BillingType, Constants.BillPayRsHauler) || StringManager.IsEqual(BillingType, Constants.BillPaySavingsShare))
                {
                    //Update Bill with HaulerAmount
                    BusinessLayer.UpdateInvoiceBill(pkrmRow, objLogin, objFunctionList, objSelectionEntity, ref logFileContent);
                }
                else
                {
                    //Update Bill with Rs Amount
                    BusinessLayer.UpdateBillRsValue(pkrmRow, objLogin, objFunctionList, objSelectionEntity, ref logFileContent);
                }
            }
            else
            {
                //Create a new Bill with HaulerAmount and update IntacctBillId back to Refuse.
                BusinessLayer.createInvoiceRecord(pkrmRow, objFunctionList, objSelectionEntity, ref logFileContent);
            }
        }

        private static void CreateNewEntity(DataRow row, List<function> objFunctionList, EnumManager.IntacctType IntacctDataType, ref StringBuilder logFileContent, SelectionEntity objSelectionEntity)
        {
            try
            {
                switch (IntacctDataType)
                {
                    case EnumManager.IntacctType.Customer:
                        string GroupNbr = row[Constants.GroupNumber].ToString();
                        string CustomerName = row[Constants.Name].ToString();
                        row[Constants.IntacctId] = GroupNbr;
                        BusinessLayer.createCustomerRecord(row, CustomerName, GroupNbr, objFunctionList, objSelectionEntity, ref logFileContent);
                        break;

                    case EnumManager.IntacctType.Project:
                        string PropertyId = row[Constants.PropertyId].ToString();
                        row[Constants.IntacctId] = PropertyId;
                        BusinessLayer.createPropertyRecord(row, objFunctionList, objSelectionEntity, ref logFileContent);
                        break;
                    case EnumManager.IntacctType.ArInvoice:
                        BusinessLayer.createInvoiceRecord(row, objFunctionList, objSelectionEntity, ref logFileContent);
                        break;
                }
                iUpdated++;
            }
            catch (Exception ex)
            {
                iErrors++;
                logFileContent.AppendLine(string.Format(Constants.ErrorUpdateInIntacct, ex.Message.ToString()));
                ErrorLog.LogIntacctExceptions(string.Empty, SecurityContext.Instance.LogInUserId,
                                                                 SecurityContext.Instance.LogInPassword,
                                                                 IntacctDataType.ToString(),
                                                                 string.Empty,
                                                                 IntacctDataType.ToString(),
                                                                 ex.Message,
                                                                 string.Empty,
                                                                 SecurityManager.GetIPAddress.ToString(),
                                                                 Constants.TechnicalError,
                                                                 string.Empty);
            }
            finally
            {
            }
        }

        private static void RunProcessForIntacctRecord(EnumManager.IntacctType IntacctDataType, EnumManager.ProcessOptions processOption,
                                            ref StringBuilder logFileContent, DataRow row, string pkStringRow, string IntacctStringRow
                                            , IntacctLogin objLogin, List<function> objFunctionList)
        {
            if (StringManager.IsEqual(pkStringRow, IntacctStringRow))
            {
                LogNoChangesInFile(IntacctDataType, logFileContent, row, pkStringRow, IntacctStringRow);
            }
            if (StringManager.IsEqual(processOption.ToString(), EnumManager.ProcessOptions.Compare.ToString()) && !StringManager.IsEqual(pkStringRow, IntacctStringRow))
            {
                iUpdated++;
                Common.LogCompareAndUpdateString(ref logFileContent, pkStringRow, IntacctStringRow, IntacctDataType, row);
            }
        }

        private static StringBuilder LogNoChangesInFile(EnumManager.IntacctType IntacctDataType, StringBuilder logFileContent, DataRow row,
            string pkStringRow, string IntacctStringRow)
        {

            string entityName = string.Empty;
            string CustomerPropertyName = string.Empty;
            if (StringManager.Equals(IntacctDataType.ToString(), EnumManager.IntacctType.Customer.ToString()))
            {
                entityName = Convert.ToString(row[Constants.GroupNumber]);
                CustomerPropertyName = Convert.ToString(row[Constants.Name]);
            }
            else if (StringManager.Equals(IntacctDataType.ToString(), EnumManager.IntacctType.Project.ToString()))
            {
                entityName = Convert.ToString(row[Constants.PropertyId]);
                CustomerPropertyName = Convert.ToString(row[Constants.Name]);
            }
            else if (StringManager.Equals(IntacctDataType.ToString(), EnumManager.IntacctType.ArInvoice.ToString()))
            {
                entityName = Convert.ToString(row[Constants.InvoiceNumber]);
            }

            //Log the notes and say they are equal
            iUnchanged++;
            Common.LogNoChanges(ref logFileContent, entityType: IntacctDataType.ToString(), entityName: entityName + '-' + CustomerPropertyName);

            return logFileContent;
        }
        public static void UpdateRecordInIntacctDataBase(ref StringBuilder logFileContent, DataRow pkrmRow, EnumManager.IntacctType IntacctDataType, IntacctLogin objLogin, List<function> objFunctionList, SelectionEntity objSelectionEntity)
        {
            try
            {
                switch (IntacctDataType)
                {
                    case EnumManager.IntacctType.Customer:
                        BusinessLayer.UpdateCustomer(pkrmRow, objLogin, objFunctionList, objSelectionEntity, ref logFileContent);
                        break;
                    case EnumManager.IntacctType.Project:
                        BusinessLayer.UpdateProperty(pkrmRow, objLogin, objFunctionList, objSelectionEntity, ref logFileContent);
                        break;
                    case EnumManager.IntacctType.ArInvoice:
                        BusinessLayer.UpdateInvoice(pkrmRow, objLogin, objFunctionList, objSelectionEntity, ref logFileContent);
                        break;
                }

                iUpdated++;
            }
            catch (Exception ex)
            {
                iErrors++;
                logFileContent.AppendLine(string.Format(Constants.ErrorUpdateInIntacct, ex.Message.ToString()));
                ErrorLog.LogIntacctExceptions(string.Empty, SecurityContext.Instance.LogInUserId,
                                                                 SecurityContext.Instance.LogInPassword,
                                                                 IntacctDataType.ToString(),
                                                                 string.Empty,
                                                                 IntacctDataType.ToString(),
                                                                 ex.Message,
                                                                 string.Empty,
                                                                 SecurityManager.GetIPAddress.ToString(),
                                                                 Constants.TechnicalError,
                                                                 string.Empty);
            }
            finally
            {
            }
        }

        #endregion

        /// <summary>
        /// Generate count for the UI panel to show the report process inorder avoid idle page behaviour.
        /// </summary>
        /// <param name="rowsToProcess"></param>
        /// <param name="rowsProcessed"></param>
        /// <param name="rowsRemaining"></param>
        /// <param name="lblRowsToProcess"></param>
        /// <param name="lblRowsprocessed"></param>
        /// <param name="lblRowsRemaining"></param>
        public static void SetRowProcessCounts(string rowsToProcess, string rowsProcessed, string rowsRemaining,
                                       Label lblRowsToProcess, Label lblRowsprocessed, Label lblRowsRemaining)
        {
            if (lblRowsToProcess.InvokeRequired)
            {
                var d = new SafeCallDelegate(SetRowProcessCounts);
                lblRowsToProcess.Invoke(d, new object[] {  rowsToProcess,  rowsProcessed,  rowsRemaining,
                                        lblRowsToProcess,  lblRowsprocessed,  lblRowsRemaining });
            }
            else
            {
                lblRowsToProcess.Text = rowsToProcess;
            }

            if (lblRowsprocessed.InvokeRequired)
            {
                var d = new SafeCallDelegate(SetRowProcessCounts);
                lblRowsprocessed.Invoke(d, new object[] {  rowsToProcess,  rowsProcessed,  rowsRemaining,
                                        lblRowsToProcess,  lblRowsprocessed,  lblRowsRemaining });
            }
            else
            {
                lblRowsprocessed.Text = rowsProcessed;
            }

            if (lblRowsRemaining.InvokeRequired)
            {
                var d = new SafeCallDelegate(SetRowProcessCounts);
                lblRowsRemaining.Invoke(d, new object[] {  rowsToProcess,  rowsProcessed,  rowsRemaining,
                                        lblRowsToProcess,  lblRowsprocessed,  lblRowsRemaining });
            }
            else
            {
                lblRowsRemaining.Text = rowsRemaining;
            }


            //lblRowsToProcess.Text = rowsToProcess;
            //lblRowsprocessed.Text = rowsProcessed;
            //lblRowsRemaining.Text = rowsRemaining;
        }

        public static string CheckForEntityInIntacct(string entityId, SelectionEntity selectionEntity)
        {
            switch (selectionEntity.IntactType)
            {
                case EnumManager.IntacctType.Customer:
                    return IntacctConnector.GetIntacctCustomerString(selectionEntity.IntactType, entityId, selectionEntity.LoginObject, null);

                case EnumManager.IntacctType.Project:
                    return IntacctConnector.GetIntacctPropertyString(selectionEntity.IntactType, entityId, selectionEntity.LoginObject, null);

                case EnumManager.IntacctType.ArInvoice:
                    return SyncEachRecord.GetInvoiceKeyBasedOnInvoiceNo(entityId, string.Empty, selectionEntity.LoginObject, Constants.IntacctId);
            }

            return string.Empty;
        }
    }
}

