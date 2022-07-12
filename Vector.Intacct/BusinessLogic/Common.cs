using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vector.Common.BusinessLayer;
using Vector.Intacct.Entities;

namespace Vector.Intacct.BusinessLogic
{
    public static class Common
    {
        #region Constants

        const string errorMessage = "Malformed error: ";
        const string errorno = "errorno";
        const string description = "description";
        const string descriptionTwo = "description2";
        const string correction = "correction";

        #endregion

        public enum IntacctAPIType
        {
            GET,
            POST,
            PUT
        }

        public static string[] AssignText(string objT)
        {
            string[] objArray = new string[1];
            objArray[0] = objT;
            return objArray;
        }

        public static string[] AddText(string text)
        {
            string[] objValues = new string[1];
            objValues[0] = text;
            return objValues;
        }

        /// <summary>
        /// It is used to add multiple functions to function object to post the data
        /// </summary>
        /// <param name="objFunctionList">List of function used</param>
        /// <returns>returns baseclass function object</returns>
        public static function[] AddFunctions(List<function> objFunctionList)
        {
            if (objFunctionList != null && objFunctionList.Count > 0)
            {
                function[] objFunctionValues = new function[objFunctionList.Count];
                int i = 0;
                foreach (function objFunction in objFunctionList)
                {
                    objFunctionValues[i] = objFunction;
                    i++;
                }
                return objFunctionValues;
            }
            else
                return null;
        }

        /// <summary>
        /// Add List of contents to baseclass content object
        /// </summary>
        /// <param name="objContentList">List of content objects</param>
        /// <returns>returns baseclass content object</returns>
        public static content[] AddContents(content objContentList)
        {
            if (objContentList != null)
            {
                content[] objContentValues = new content[1];
                objContentValues[0] = objContentList;
                return objContentValues;
            }
            else
                return null;
        }

        /// <summary>
        /// List of operations are added to operation object
        /// </summary>
        /// <param name="objOperationList">List of operations that are used to post the method</param>
        /// <returns>returns base class operation object</returns>
        public static operation[] AddOperations(operation objOperationList)
        {
            if (objOperationList != null)
            {
                operation[] objOperationValues = new operation[1];
                objOperationValues[0] = objOperationList;
                return objOperationValues;
            }
            else
                return null;
        }

        public static string GenerateLogFilePath(string logFilePath)
        {
            StringBuilder fileName = new StringBuilder();
            fileName.Append(logFilePath);
            fileName.Append(Constants.LogFileTimeStamp);
            fileName.Append(DateManager.GetTimestamp(System.DateTime.Now));
            fileName.Append(Constants.TxtFiles.TrimStart('*').ToString());
            return fileName.ToString();
        }

        public static string xmlErrorToString(System.Xml.XmlNode xmlNode)
        {

            if (xmlNode == null)
            {
                return errorMessage;
            }
            xmlNode = xmlNode.FirstChild;

            if (xmlNode == null && !xmlNode.HasChildNodes)
            {
                return errorMessage;
            }

            string xmlerrorno = string.IsNullOrEmpty(xmlNode.SelectSingleNode(errorno).InnerXml) ? string.Empty : xmlNode.SelectSingleNode(errorno).InnerXml;
            string xmldescription = string.IsNullOrEmpty(xmlNode.SelectSingleNode(description).InnerXml) ? string.Empty : xmlNode.SelectSingleNode(description).InnerXml;
            string xmldescriptionTwo = string.IsNullOrEmpty(xmlNode.SelectSingleNode(descriptionTwo).InnerXml) ? string.Empty : xmlNode.SelectSingleNode(descriptionTwo).InnerXml;
            string xmlcorrection = string.IsNullOrEmpty(xmlNode.SelectSingleNode(correction).InnerXml) ? string.Empty : xmlNode.SelectSingleNode(correction).InnerXml;

            StringBuilder objErrorMessages = new StringBuilder();
            objErrorMessages.Append(xmlerrorno);
            objErrorMessages.Append(xmldescription);
            objErrorMessages.Append(xmldescriptionTwo);
            objErrorMessages.Append(xmlcorrection);
            return objErrorMessages.ToString();
        }

        public static string GetNoDataFoundLog(EnumManager.IntacctType IntacctType)
        {
            return string.Format(Constants.NoRecoredFound, IntacctType.ToString());
        }

        public static string GetExceptionResponceLog(string errorDeac, EnumManager.IntacctType IntacctDataType)
        {
            StringBuilder errorLog = new StringBuilder();
            errorLog.Append(Constants.Error);
            errorLog.Append(errorDeac);
            errorLog.Append(Environment.NewLine);
            errorLog.Append(Constants.ResponseError);
            errorLog.Append(IntacctDataType.ToString());
            errorLog.Append(Constants.ForwardArrow);
            return errorLog.ToString();
        }

        public static void LogCompareAndUpdateString(ref StringBuilder logFileContent, string fromRow, string toRow, EnumManager.IntacctType IntacctDataType, DataRow row)
        {
            string entityName = null;
            if (StringManager.Equals(IntacctDataType.ToString(), EnumManager.IntacctType.Customer.ToString()))
                entityName = Convert.ToString(row[Constants.GroupNumber]);
            else if (StringManager.Equals(IntacctDataType.ToString(), EnumManager.IntacctType.Project.ToString()))
                entityName = Convert.ToString(row[Constants.PropertyId]);
            else if (StringManager.Equals(IntacctDataType.ToString(), EnumManager.IntacctType.ArInvoice.ToString()))
                entityName = Convert.ToString(row[Constants.InvoiceNo]);
            if (!string.IsNullOrEmpty(toRow.ToString()))
            {
                logFileContent.AppendLine(IntacctDataType.ToString() + " : " + entityName + Environment.NewLine + Environment.NewLine + Constants.Refuse + fromRow + Environment.NewLine + Constants.Intacct + toRow + Environment.NewLine);

            }
            else
            {

                logFileContent.AppendLine(IntacctDataType.ToString() + " : " + entityName + Environment.NewLine + Environment.NewLine + Constants.Refuse + fromRow + Environment.NewLine + Constants.Intacct + "No data found for this in Intacct" + Environment.NewLine);

            }
        }

        /// <summary>
        /// Log No Changes to The Log file 
        /// </summary>
        /// <param name="logFileContent">content with text format</param>
        /// <param name="entityName">it should be primaryKey value of intacct like "New properties" , GroupNbr/PropertyNbr/InvoiceNbr etc</param>
        /// <param name="entityType">it shoud be the Type Customer/Proeprty/Hauler/Invoice</param>
        public static void LogNoChanges(ref StringBuilder logFileContent, string entityName = null, string entityType = null)
        {
            if (!string.IsNullOrEmpty(entityType))
                logFileContent.AppendLine(entityType + " : " + entityName + Constants.NoChanges + Environment.NewLine);
        }

        public static string GetLogHeader(string fromDate, string toDate, string processOption, string logFilePath)
        {
            return string.Format(Constants.LogHeader, processOption, System.DateTime.Now.ToString(), string.Empty,
                                 string.Empty, logFilePath, string.Empty, fromDate, toDate).ToString();
        }

    }

    public static class IntacctAPIURL
    {
        public static string ValidateUser = "Authenticate/MobileValidateUser?user={0}&pwd={1}";
        public static string GetCustomer = "intacct/Customer?fromDate={0}&toDate={1}";
        public static string GetProperty = "intacct/Property?fromDate={0}&toDate={1}&syncType={2}";
        //public static string GetHauler = "intacct/Hauler?fromDate={0}&toDate={1}";
        public static string GetInvoice = "intacct/Invoice?fromDate={0}&toDate={1}&type={2}";
        public static string LogException = "intacct/LogException";

        public static string UpdateReportDataByEntity = "intacct/UpdateReportDataByEntity?entityType={0}&customerKey={1}&qBID={2}&KeyNo={3}&intacctId={4}";
        public static string UpdateIntacctId = "intacct/UpdateIntacctId?intacctType={0}&entityId={1}&intacctId={2}";
        public static string UpdateInvoice = "intacct/UpdateInvoice?invoiceNumber={0}&qBID={1}&type={2}";
        public static string UpdateBillData = "intacct/UpdateBillData?invoiceNumber={0}&qBID={1}&type={2}";
        public static string UpdateReceiptPayment = "intacct/UpdateReceiptPayment";
    }

    public static class Constants
    {
        public enum intacctColumnNames
        {
            Address1,
            Address2,
            City,
            state,
            PostalCode,
            STATE,
            Street1,
            Street2,
            UniqueContactName,
            Fax,
            Email1,
            Phone1,
            Name,
            Email,
            Phone,
            Inactive,
            GroupStatus,
            ContactEmail,
            ContactPhone
        }

        public const string Add30DaysToInvoiceDate = "Add30DaysToInvoiceDate";
        public const string GlAccountNo = "GlAccountNo";
        public const string GlBillNo = "GlBillNo";
        public const string GlHaulerAcctNo = "GlHaulerAcctNo";
        public const string VendorId = "VendorId";
        public static string IntacctIdFromRefuse = "IntacctIdFromRefuse";
        public const string Contract = "Contract";


        public static string Refuse = "   In Refuse: ";
        public static string Intacct = "   In Intacct: ";
        public static string NoChanges = "  [No Changes]";
        public static string FindSelectedNode = "/response/operation/result/key";
        public const string FindControlId = "/response/operation/result/controlid";
        public const string Posted = "Posted";
        public const string SyncReportForEntity = "SyncReportForEntity  :  ";
        public const int Hundered = 100;
        public const string SuccessTwo = "Success";
        public static string BillPayRsHauler = "Bill Pay - RS Hauler";
        public static string BillPaySavingsShare = "Bill Pay - Savings Share";
        public static string DirectPaySavingsShare = "Direct Pay - Savings Share";
        public static string NoRecoredFound = @"No {0} Records to download.";
        public static string ResponseError = "<<< Response Error ";
        public static string ForwardArrow = " >>>";
        public static string SessionError = "SessionError";
        public const string ReadRelated = "readRelated";
        public const string ReadByQuery = "readByQuery";
        public const string ReadByName = "readByName";
        public const string get_list = "get_list";
        public const string get_invoice = "get_invoice";
        public const string Invoice = "invoice";
        public const string ErrorNo = "errorno";
        public const string Description = "description";
        public const string DescriptionTwo = "description2";
        public const string Correction = "correction";
        public const string MalformedError = "Malformed error: ";
        public static string CompareError = "     ERROR: An error occurred while comparing.  Message is: ";
        public static string NotFouindInIntacct = "Not found in Intacct";
        public static string NoDataFoundIntacct = "No Data Found In Intacct";
        public static string UniqueResponseNode = "/response/operation/result";
        public static string controlid = "controlid";
        public static string InvoiceNumber = "InvoiceNo";
        public static string function = "function";
        public static string key = "key";
        public static string IntacctId = "INTACCT_ID";
        public static string ANewRecordCreatedSuccessfully = "A New Record Created Successfully";
        public static string ErrorUpdateInIntacct = "Failed to update intacctid in Vector";
        public const string intacct = "Intacct";
        public const string InvoiceLog = "ArInvoice - ";
        public const string Skip = "SkipExecution";
        public static string Cannotupdate = ": Line items can't be added or removed to the partially editable transaction (or) Can't alter the Invoice for which payment is fully made";
        public const string Yes = "Yes";
        public static string GroupNumber = "GroupNbr";
        public static string IntacctBillId = "IntacctBillId";
        public static string BillingType = "BillingType";
        public static string PropertyId = "PropertyId";
        public static string InvoiceNo = "InvoiceNo";
        public static string BillNo = "billno";
        public static string Name = "NAME";
        public static string Exception = "Exception";

        public static string CustomerOrHaulerString = @"CustomerId:{0}; CustomerName:{1};CustomerName:{2};CustomerAddress1:{3};CustomerAddress2:{4};CustomerCity:{5};CustomerState:{6};CustomerPostalCode:{7};UniqueContactName:{8};ContactFax:{9};ContactEmail:{10};ContactPhone:{11};GroupStatus:{12}";
        public static string PropertyString = @"parent_Name:{0}; propname:{1}; parent_ListID:{2}; acctno:{3}; adr1:{4}; adr2:{5}; city:{6}; state:{7}; zip:{8}; contact:{9}; email:{10}; phone:{11}; fax:{12}; isactive:{13}";
        public static string UniquePropertyString = @"CustomerId:{0};PropertyName:{1};PropertyType:{2};PropertyStatus:{3};IntacctIdFromRefuse:{4};PropertyId:{5};Currency:{6}";
        public static string UniqueInvoiceString = @"InvoiceNo:{0};TotalCommission:{1};HaulerName:{2};AuditedDate:{3};DueDate:{4};PropertyManagementName:{5};Item:{6};HaulerAccountNumber:{7};HaulerInvoiceNo:{8};IntacctIdFromRefuse:{9};GroupNbr:{10}";
        public static string InvoiceString = "InvoiceNo:{0}; InvoiceDate:{1}; PropertyName:{2}:{3}; Hauler:{4}; InvAmt:{5}; DueDate:{6}; Invoice_QBID: '{7}; Property_QBID: '{8}";
        public static string QBInvoiceString = @"InvoiceNo:{0}; InvoiceDate:{1}; PropertyName:{2}; Hauler:{3}; InvAmt:{4}; DueDate:{5}; Invoice_QBID: '{6}; Property_QBID: '{7}";

        public const string Bill = "bill";
        public const string Equaloperator = "=";
        public const string UnableToConstructXMLRequest = "Unable to construct the request.Please try again with valid data";
        public const string Read = "read";
        public const string Xml = "xml";
        public static bool isAuthenticated = false;
        public const string Space = " ";
        public static string TxtFiles = "*.txt";
        public static string DateFormat = "dd/MM/yyyy hh:mm:ss tt";
        public const char CharUnderscore = '_';
        public static string LogFileNotFound = "File not found.";
        public const string UploadExcelType = "Excel Files|*.xls;*.xlsx;*.xlsm";
        public static string SkippingRemaining = "<<< Skipping remaining ";
        public static string TechnicalError = "Technical";
        public static string Error = "  ERROR: ";
        public static string LogFileTimeStamp = "\\Refuse_Specialists_Intacct_Log_";
        public static string UserName = "UserName";
        public static string Password = "Password";
        public static string SenderId = "senderId";
        public static string SenderPassword = "senderPassword";
        public static string CompanyId = "companyId";
        public static string ApiPath = "ApiPath";
        public const string ArPaymentsUpdated = "All records present in template are updated to Intacct";
        public const string NoCustomersOrInvoices = "No Customers or Invoices present with the specified data";
        public const string PleaseSelectFilePath = "Please select a file path";
        public const string PleaseSelectOnlyExcelFile = "Please Select an excel file to Upload AR Payments";
        public static string ArEntity = "AR Payments";
        public static string ReplacePath = "\\bin\\Release\\";
        public static string SettingsFillDetailsError = "Please  fill all the details ";
        public static string SettingUpdateSuccess = "Configuration saved successfully.";
        public static string ConfigurationNotFound = "Configuration path not found.";
        public static string LogFileNotFoundError = "Log files path not found. Select the folder where you would like to store your log files.  Each log file will contain one week's worth of Import/Export activity.";
        public static string UpdatedSuccessfully = " Updated Successfully";
        public static string CannotUpdateIntacctId = " Can't update since the EntityId you entered doesn't exists in ProRefuse";
        public const string One = "1";
        public const string Two = "2";
        public const string PROPERTYNAME = "PROPERTYNAME";

        public const string MinusOne = "-1";
        public const string Technical = "Technical";
        public const int Zero = 0;
        public static string Empty = string.Empty;
        public static string CheckIntacctDataTypeMessage = "Please select Intacct Sync Type.";
        public static string SettingsNotFound = "Please use the 'Settings' tab to specify your  settings first.";
        public static string SettingsNotFoundError = "Waste Remedies Intacct Integration.";
        public static string DashLine = "----------------------------------------------------------------";
        public static string RunTime = "Run Time :";
        public static string FunctionalError = "Functional";
        public const string xmlNamespace = "http://schemas.ali.com/lib/";
        public const string controlId = "prokarma";
        public const string InValidXmlResponse = "Invalid XML response: \n";
        public const string ResponseControlStatus = "/response/control/status";
        public const string ResponseErrorMessage = "/response/errormessage";
        public const string ResponseOperationAuthenticationStatus = "/response/operation/authentication/status";
        public const string ResponseOperationErrorMessage = "/response/operation/errormessage";
        public const string ResponseOperationauthenticationStatus = "/response/operation/authentication/status";
        public const string ErrorDescriptionTwo = "error/description2";
        public const string ResponseOperationResultStatus = "/response/operation/result/status";
        public const string UserFriendlyError = "A fatal error occured which can't be handled in codebase while processing, please check database for the detailed error";
        public const string readErrorDescription = "/errormessage/error/description/";
        public const string readErrorDescriptiontwo = "/errormessage/error/description2/";
        public const string ErrorResponse = "/response/errormessage";
        public const string ResponseOperationResultErrorMessage = "/response/operation/result/errormessage";
        public const string ResponseOperation = "/response/operation";
        public const string ResponseErrorMessageError = "/response/errormessage/error";
        public const string OperationErrorMessageError = "/operation/errormessage/error";
        public const string ResponseOperationResultData = "/response/operation/result/data/";
        public const string OperationResult = "/operation/result";
        public const string ErrorMessage = "/errormessage";
        public const string Operation = "/operation";
        public const string Status = "/status";
        public const string Failure = "failure";
        public const string Success = "success";

        public static string LogHeader = @"
******************************************************************************************

ProRefuse/Intacct - {0}

******************************************************************************************

Run Date/Time: {1}
SQL Database: {2}
Intacct File: {3}
Log File: {4}
Logging Level: {5}
Transaction Begin: {6}
Transaction End: {7}

*****************************************************************************************";
    }
}
