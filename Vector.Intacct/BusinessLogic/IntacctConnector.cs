using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Vector.Common.BusinessLayer;
using Vector.Intacct.Entities;

namespace Vector.Intacct.BusinessLogic
{
    public static class IntacctConnector
    {
        const string ContentType = "application/x-www-form-urlencoded";
        const string ReqMethodType = "POST";
        static string lastResponse;
        const string xmlRequest = "xmlrequest={0}";
        const string controlId = "test";
        const string uniqueId = "false";
        const string dtdVersion = "3.0";
        const string pageSize = "10000";
        #region Authenticate Intacct

        public static IntacctLogin EstablishIntacctConnection(ConnectionEntity objConnectionEntity)
        {
            IntacctLogin objLogin = new IntacctLogin(objConnectionEntity);
            if (objLogin.ConnectionCredentials(objConnectionEntity))
            {
                Constants.isAuthenticated = true;
            }
            return objLogin;
        }

        #endregion
        public static string CreateUpdate(IntacctLogin session, List<function> objFunctions)
        {
            senderid objSenderid;
            password objPassword;
            controlid objControlid;
            uniqueid objUniqueid;
            dtdversion objVersion;
            ConstructHeaders(session, out objSenderid, out objPassword, out objControlid, out objUniqueid, out objVersion, "3.0");

            request objRequest = ConstructRequest(session, objSenderid, objPassword, objControlid, objUniqueid, objVersion, objFunctions);
            if (objRequest != null)
            {
                string xmlRequest = BuildXMLRequestWithoutNamespaces(objRequest);
                //var xDocument = XDocument.Load(@"D:\text.xml");
                //string xmlRequest = xDocument.ToString();

                string response = post(xmlRequest, session);
                //return processUpdateResults(response);
                return response;
            }
            else
                return Constants.UnableToConstructXMLRequest;
        }

        public static DataSet read(object objectName,
                               IntacctLogin session,
                               ItemChoiceType2 objItemChoice,
                               string id = "",
                               string fields = "*",
                               string dtdVersion = dtdVersion,
                               string type = Constants.Read,
                               string relation = null,
                               string returnFormat = Constants.Xml,
                               string query = "",
                               string pageSize = pageSize,
                               bool isReadMore = false,
                               string expValue1 = "",
                               string expValue2 = "",
                               string replaceString1 = "",
                               string replaceString2 = "",
                               string replaceWithString1 = "",
                               string replaceWithString2 = "",
                               string field1 = "",
                               string key = "")
        {
            senderid objSenderid;
            password objPassword;
            controlid objControlid;
            uniqueid objUniqueid;
            dtdversion objVersion;
            ConstructHeaders(session, out objSenderid, out objPassword, out objControlid, out objUniqueid, out objVersion, dtdVersion);

            request objRequest = null;
            object objectTypeName = null;

            List<function> objFunctionList = new List<function>();
            function objFunction = new function();
            objectTypeName = BuildXMLReadRequests(objectName, id, fields, type, relation, returnFormat, query, pageSize, objectTypeName, objFunction, expValue1, expValue2, field1, key);

            if (objectTypeName != null)
            {
                objFunctionList.Add(objFunction);
                if (isReadMore)
                {
                    readMore objReadMore = new readMore();
                    objReadMore.Item = (string)objectName;
                    objReadMore.ItemElementName = ItemChoiceType1.@object;
                    function objFunctionReadMore = new function();
                    objFunctionReadMore.controlid = Constants.controlId;
                    objFunctionReadMore.Item = objReadMore;
                    objFunctionReadMore.ItemElementName = ItemChoiceType2.readMore;
                    objFunctionList.Add(objFunctionReadMore);
                }
                objRequest = ConstructRequest(session, objSenderid, objPassword, objControlid, objUniqueid, objVersion, objFunctionList);



                string writer = BuildXMLRequestWithoutNamespaces(objRequest);

                writer = writer.Replace("QueryValue1", replaceWithString1);
                writer = writer.Replace("QueryValue2", replaceWithString2);
                string objCsv = post(writer, session);
                XmlSerializer xs = new XmlSerializer(typeof(request));


                if (StringManager.IsNotEqual(objCsv, Constants.SessionError))
                {
                    if (string.IsNullOrEmpty(processUpdateResults(objCsv)))
                    {
                        XmlDocument simpleXml = new XmlDocument();
                        simpleXml.PreserveWhitespace = false;
                        simpleXml.LoadXml(objCsv);

                        XmlNodeList nodeList = simpleXml.SelectNodes(Constants.ResponseOperationResultData + Convert.ToString(objectName).ToLower());
                        if (nodeList.Count == 0)
                        {
                            nodeList = simpleXml.SelectNodes(Constants.ResponseOperationResultData + Convert.ToString(objectName));
                        }

                        string xmlDocumentString = "<?xml version='1.0' encoding='UTF-8'?>" +
                                                    "<CommonDocument></CommonDocument>";
                        XmlDocument newXmlDocument = new XmlDocument();
                        newXmlDocument.LoadXml(xmlDocumentString);
                        foreach (XmlNode nodes in nodeList)
                        {
                            XmlNode copiedNode = newXmlDocument.ImportNode(nodes, true);
                            newXmlDocument.DocumentElement.AppendChild(copiedNode);
                        }
                        StringWriter sw = new StringWriter();
                        XmlTextWriter xwriter = new XmlTextWriter(sw);
                        newXmlDocument.WriteTo(xwriter);
                        string enkannaString = sw.ToString();
                        DataSet resultSet = new DataSet();
                        resultSet.ReadXml(new StringReader(enkannaString));
                        return resultSet;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }
        private static object BuildXMLReadRequests(object objectName, string id, string fields, string type, string relation,
                                                    string returnFormat, string query, string pageSize, object objectTypeName,
                                                    function objFunction, string expValue1 = "", string expValue2 = "", string field1 = "", string Key = "")
        {

            if (string.Equals(type, Constants.Read))
            {
                read objRead = new read();
                objRead.@object = (string)objectName;
                objRead.fields = fields;
                objRead.keys = id;
                objRead.returnFormat = returnFormat;
                objectTypeName = objRead;
                objFunction.controlid = Constants.controlId;
                objFunction.Item = objRead;
                objFunction.ItemElementName = ItemChoiceType2.read;
                readMore objReadMore = new readMore();
                objReadMore.Item = (string)objectName;
                objReadMore.ItemElementName = ItemChoiceType1.@object;
            }
            else if (string.Equals(type, Constants.ReadRelated))
            {
                readRelated objReadRelated = new readRelated();
                objReadRelated.@object = (string)objectName;
                objReadRelated.fields = fields;
                objReadRelated.keys = id;
                objReadRelated.returnFormat = returnFormat;
                objReadRelated.relation = relation;
                objectTypeName = objReadRelated;
                objFunction.controlid = Constants.controlId;
                objFunction.Item = objReadRelated;
                objFunction.ItemElementName = ItemChoiceType2.readRelated;
            }
            else if (string.Equals(type, Constants.ReadByQuery))
            {
                readByQuery objReadByQuery = new readByQuery();
                objReadByQuery.@object = (string)objectName;
                objReadByQuery.fields = fields;
                objReadByQuery.returnFormat = returnFormat;
                objReadByQuery.query = query;
                objReadByQuery.pagesize = pageSize;
                objectTypeName = objReadByQuery;
                objFunction.controlid = Constants.controlId;
                objFunction.Item = objReadByQuery;
                objFunction.ItemElementName = ItemChoiceType2.readByQuery;
            }
            else if (string.Equals(type, Constants.ReadByName))
            {
                readByName objReadByName = new readByName();
                objReadByName.@object = (string)objectName;
                objReadByName.fields = fields;
                objReadByName.returnFormat = returnFormat;
                objReadByName.keys = Key;
                //objReadByName.query = query;
                //objReadByName.pagesize = pageSize;
                objectTypeName = objReadByName;
                objFunction.controlid = Constants.controlId;
                objFunction.Item = objReadByName;
                objFunction.ItemElementName = ItemChoiceType2.readByName;
            }

            //Get_List Method to get invoice key for a particular invoice or customer
            else if (StringManager.IsEqual(type, Constants.get_invoice))
            {
                get_invoice objGetInvoice = new get_invoice();
                objGetInvoice.key = Key;
                objFunction.controlid = Constants.controlId;
                objFunction.Item = objGetInvoice;
                objFunction.ItemElementName = ItemChoiceType2.get_invoice;
                read objRead = new read();

                objectTypeName = objRead;
            }
            else if (string.Equals(type, Constants.get_list))
            {
                get_list objGetList = new get_list();

                if ((StringManager.IsEqual((string)objectName, Constants.Invoice) || StringManager.IsEqual((string)objectName, "Project") || StringManager.IsEqual((string)objectName, "Customer")) || StringManager.IsEqual((string)objectName, Constants.Bill) && (!string.IsNullOrEmpty(expValue1)))
                {
                    if (StringManager.IsEqual((string)objectName, Constants.Invoice))
                        objGetList.@object = get_listObject.invoice;
                    else if (StringManager.IsEqual((string)objectName, "Project"))
                        objGetList.@object = get_listObject.project;
                    else if (StringManager.IsEqual((string)objectName, "Customer"))
                        objGetList.@object = get_listObject.customer;
                    else if (StringManager.IsEqual((string)objectName, "bill"))
                        objGetList.@object = get_listObject.bill;

                    List<expression> lstExpr = new List<expression>();
                    filter fltr = new filter();
                    expression expr = new expression();
                    field fld = new field();
                    // fld.Text = Common.AssignText(Constants.InvoiceNumber);
                    fld.Text = Common.AssignText(field1);
                    @operator opt = new @operator();

                    //For Equal operator
                    opt.Text = Common.AssignText(Constants.Equaloperator);
                    value val = new value();
                    val.Text = Common.AssignText(expValue1);
                    expr.field = fld;
                    expr.@operator = opt;
                    expr.value = val;
                    //If Multiple expressions need to be added  TODO


                    lstExpr.Add(expr);
                    fltr.Items = lstExpr.ToArray();
                    objGetList.filter = fltr;
                    objFunction.controlid = Constants.controlId;
                    objFunction.Item = objGetList;
                    objFunction.ItemElementName = ItemChoiceType2.get_list;
                    read objRead = new read();

                    objectTypeName = objRead;
                }

                else if ((string.Equals((string)objectName, "ARPAYMENT")))
                {

                    objGetList.@object = get_listObject.arpayment;
                    List<expression> lstExpr = new List<expression>();
                    List<logical> lstLogical = new List<logical>();
                    filter fltr = new filter();
                    logical logicalExpression = new logical();

                    //Logical operator and is used for combaining 2 expressions in a filter
                    logicalExpression.logical_operator = logicalLogical_operator.and;

                    expression expr1 = new expression();
                    expression expr2 = new expression();

                    field fld1 = new field();
                    field fld2 = new field();

                    @operator opt1 = new @operator();
                    @operator opt2 = new @operator();
                    value val1 = new value();
                    value val2 = new value();

                    fld1.Text = Common.AssignText("datereceived");
                    fld2.Text = Common.AssignText("datereceived");


                    //For Equal operator
                    opt1.Text = Common.AssignText("QueryValue1");
                    opt2.Text = Common.AssignText("QueryValue2");

                    val1.Text = Common.AssignText(expValue1);
                    val2.Text = Common.AssignText(expValue2);


                    expr1.field = fld1;
                    expr1.@operator = opt1;
                    expr1.value = val1;

                    expr2.field = fld2;
                    expr2.@operator = opt2;
                    expr2.value = val2;

                    //If Multiple expressions need to be added  TODO


                    lstExpr.Add(expr1);
                    lstExpr.Add(expr2);

                    logicalExpression.Items = lstExpr.ToArray();

                    lstLogical.Add(logicalExpression);

                    fltr.Items = lstLogical.ToArray();

                    objGetList.filter = fltr;
                    objFunction.controlid = Constants.controlId;
                    objFunction.Item = objGetList;
                    objFunction.ItemElementName = ItemChoiceType2.get_list;
                    read objRead = new read();

                    objectTypeName = objRead;
                }


            }
            return objectTypeName;
        }

        private static void ConstructHeaders(IntacctLogin session, out senderid objSenderid, out password objPassword, out controlid objControlid, out uniqueid objUniqueid, out dtdversion objVersion, string version = "3.0")
        {
            objSenderid = new senderid();
            objSenderid.Text = Common.AddText(session.objConnectionEntity.SenderId);
            objPassword = new password();
            objPassword.Text = Common.AddText(session.objConnectionEntity.SenderPassword);
            objControlid = new controlid();
            objControlid.Text = Common.AddText(controlId);
            objUniqueid = new uniqueid();
            objUniqueid.Text = Common.AddText(uniqueId);
            objVersion = new dtdversion();
            objVersion.Text = Common.AddText(version);
        }
        private static request ConstructRequest(IntacctLogin session, senderid objSenderid, password objPassword, controlid objControlid, uniqueid objUniqueid, dtdversion objVersion, List<function> objfunctionList)
        {
            control objControl = new control();
            objControl.senderid = objSenderid;
            objControl.password = objPassword;
            objControl.controlid = objControlid;
            objControl.uniqueid = objUniqueid;
            objControl.dtdversion = objVersion;
            sessionid objsessionid = new sessionid();
            objsessionid.Text = Common.AddText(session.objConnectionEntity.SessionId);
            authentication objAuth = new authentication();
            objAuth.Item = objsessionid;
            content objContent = new content();
            objContent.function = Common.AddFunctions(objfunctionList);
            operation objOperation = new operation();
            objOperation.authentication = objAuth;
            objOperation.transaction = operationTransaction.@true;
            objOperation.content = Common.AddContents(objContent);
            request objRequest = new request();
            objRequest.control = objControl;
            objRequest.operation = Common.AddOperations(objOperation);
            return objRequest;
        }
        private static string BuildXMLRequestWithoutNamespaces(request objRequest)
        {
            XmlSerializerNamespaces objNameSpaces = new XmlSerializerNamespaces();
            objNameSpaces.Add(Constants.Empty, Constants.xmlNamespace);
            var ms = new MemoryStream();
            var xw = XmlWriter.Create(ms); // Remember to stop using XmlTextWriter  
            var serializer = new XmlSerializer(typeof(request));



            serializer.Serialize(xw, objRequest, objNameSpaces);
            xw.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(ms, Encoding.UTF8);
            string xmlRequest = sr.ReadToEnd();
            return xmlRequest;
        }

        public static string processUpdateResults(string response)
        {
            XmlDocument simpleXml = new XmlDocument();
            simpleXml.LoadXml(response);
            string result = string.Empty;
            if (simpleXml == null)
            {
                return Constants.InValidXmlResponse;
            }

            // check to see if there's an error in the response
            string status = simpleXml.SelectSingleNode(Constants.ResponseOperationResultStatus).InnerXml;
            if (!string.IsNullOrEmpty(status))
            {

                if (!string.IsNullOrEmpty(status))
                {
                    if (status != Constants.Success)
                    {
                        //find the problem and raise an exception
                        XmlNode error = simpleXml.SelectSingleNode(Constants.ResponseOperationResultErrorMessage);
                        result = Constants.Space + Constants.Error + Constants.Space + xmlErrorToString(error);
                    }
                    else if (status == Constants.Success)
                    {
                        //return string.Empty;
                        //XmlNode node = simpleXml.SelectSingleNode("/operation[@*]/result");
                        XmlNodeList nodeList = simpleXml.SelectNodes("/response/operation/result/key");
                        string keyCustomerId = string.Empty;
                        foreach (XmlNode node in nodeList)
                        {
                            keyCustomerId = node.InnerText;
                            //createIntacctIdwithCustomerId(keyCustomerId);

                        }
                    }
                }
            }

            return result;
        }

        private static string post(string xml, IntacctLogin session)
        {

            string endPoint = session.objConnectionEntity.IntacctApiUrl;

            string res = string.Empty;
            res = ExecuteRequest(xml, endPoint);

            // If we didn't get a response, we had a poorly constructed XML request.

            try
            {
                string validatedResponse = validateResponse(res);
                if (StringManager.IsNotEqual(validatedResponse, Constants.SessionError))
                {
                    if (!string.IsNullOrEmpty(validatedResponse))
                        return validatedResponse;
                }
                else
                {
                    return Constants.SessionError;
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }


            return res;
        }
        public static string ExecuteRequest(string body, string endPoint)
        {
            try
            {
                System.Net.WebRequest req = System.Net.WebRequest.Create(endPoint);
                req.ContentType = ContentType;
                req.Method = ReqMethodType;
                req.Timeout = 10000000;
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(string.Format(xmlRequest, body));
                //byte[] bytes = System.Text.Encoding.ASCII.GetBytes(body);
                req.ContentLength = bytes.Length;

                System.IO.Stream responseStream = req.GetRequestStream();
                responseStream.Write(bytes, Constants.Zero, bytes.Length);
                responseStream.Close();

                System.Net.WebResponse resp = req.GetResponse();
                if (resp == null)
                {
                    lastResponse = null;
                }
                else
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                    lastResponse = sr.ReadToEnd().Trim();
                }

            }
            catch (Exception ex)
            {

                ErrorLog.LogIntacctExceptions(string.Empty, SecurityContext.Instance.LogInUserId,
                                                                                    SecurityContext.Instance.LogInPassword,
                                                                                    "TrailandError",
                                                                                    string.Empty,
                                                                                    "TrailandError",
                                                                                    ex + "This is the problem with intacct response, a new try catch implemented in ExecuteRequest method",
                                                                                    "TrailandError",
                                                                                    SecurityManager.GetIPAddress.ToString(),
                                                                                    Constants.FunctionalError,
                                                                                    string.Empty);


            }
            return lastResponse;
        }
        private static string validateResponse(string response)
        {

            XmlDocument objResponse = new XmlDocument();
            objResponse.LoadXml(response);

            if (objResponse == null && !objResponse.HasChildNodes)
                return string.Empty;

            // look for a failure in the operation, but not the result
            if (StringManager.IsEqual(objResponse.SelectSingleNode(Constants.ResponseOperationauthenticationStatus).InnerXml.ToString(), Constants.Failure))
            {
                //Generate a new session since the existing session is failing.
                return Constants.SessionError;
            }
            if (objResponse.SelectSingleNode(Constants.ResponseOperationErrorMessage) != null)
            {
                XmlNode error = objResponse.SelectSingleNode(Constants.OperationErrorMessageError);
                return Constants.Error + Constants.Space + error.SelectSingleNode(Constants.ErrorNo) + error.SelectSingleNode(Constants.DescriptionTwo);
            }

            // if we didn't get an operation, the request failed and we should raise an exception
            // with the error details
            // did the method invocation fail?
            if (objResponse.SelectSingleNode(Constants.Operation) == null)
            {
                if (objResponse.SelectSingleNode(Constants.ErrorMessage) != null)
                {
                    return Constants.Error + Constants.Space + Common.xmlErrorToString(objResponse.SelectSingleNode(Constants.ErrorMessage));
                }
                else
                {
                    XmlNodeList results = objResponse.SelectNodes(Constants.OperationResult);
                    foreach (XmlNode node in results)
                    {
                        if (node.SelectSingleNode(Constants.Status).InnerXml == Constants.Failure)
                        {
                            return Constants.Error + Constants.Space + Common.xmlErrorToString(node.SelectSingleNode(Constants.ErrorMessage));
                        }
                    }
                }
            }
            return string.Empty;
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

        #region Comparing data from Refuse to intacct

        public static string GetPkString(EnumManager.IntacctType IntacctDataType, DataRow row, ref StringBuilder logFileContent)
        {
            try
            {
                switch (IntacctDataType)
                {
                    //TO DO : String Builder
                    case EnumManager.IntacctType.Customer:
                        return GetPKCustomerOrHaulerString(row); // Common for both Customer & Hauler

                    case EnumManager.IntacctType.Project:
                        return GetPKPropertyString(row); //Only for Property

                    //case EnumManager.IntacctDataType.Hauler:
                    //    return GetPKCustomerOrHaulerString(row); // Common for both Customer & Hauler

                    case EnumManager.IntacctType.ArInvoice:
                        return GetPKInvoiceString(row);

                    default: return string.Empty;
                }
            }
            catch (Exception ex)
            {
                // Log Error
                logFileContent.AppendLine(Constants.CompareError + ex.Message);
            }
            return string.Empty;
        }

        public static string GetPKCustomerOrHaulerString(DataRow reportRow)
        {
            return string.Format(Constants.CustomerOrHaulerString, Convert.ToString(reportRow[EnumManager.Columns.GroupNbr.ToString()]).Trim(),
                                                                        Convert.ToString(reportRow[EnumManager.Columns.Name.ToString()]).Trim(),
                                                                        Convert.ToString(reportRow[EnumManager.Columns.Name.ToString()]).Trim(),
                                                                        Convert.ToString(reportRow[EnumManager.Columns.Street1.ToString()]).Trim(),

                                                                        Convert.ToString(reportRow[EnumManager.Columns.Street2.ToString()]).Trim(),
                                                                        Convert.ToString(reportRow[EnumManager.Columns.City.ToString()]).Trim(),
                                                                        Convert.ToString(reportRow[EnumManager.Columns.state.ToString()]).Trim(),
                                                                        Convert.ToString(reportRow[EnumManager.Columns.PostalCode.ToString()]).Trim(),
                                                                        Convert.ToString(reportRow[EnumManager.Columns.Name.ToString()]).Trim(),
                                                                        Convert.ToString(reportRow[EnumManager.Columns.ContactFax.ToString()]).Trim(),
                                                                        Convert.ToString(reportRow[EnumManager.Columns.ContactEmail.ToString()]).Trim(),
                                                                        Convert.ToString(reportRow[EnumManager.Columns.ContactPhone.ToString()]).Trim(),
                                                                        Convert.ToString(reportRow[EnumManager.Columns.GroupStatus.ToString()]).Trim()
                                                       );
        }
        private static string GetPKPropertyString(DataRow reportRow)
        {
            return string.Format(Constants.UniquePropertyString, Convert.ToString(reportRow[EnumManager.Columns.GroupNbr.ToString()]).Trim(),
                                                                         Convert.ToString(reportRow[EnumManager.Columns.Name.ToString()]).Trim(),
                                                                         Convert.ToString(reportRow[EnumManager.Columns.FrachiseOrOpenMarket.ToString()]).Trim(),
                                                                         Convert.ToString(reportRow[EnumManager.Columns.Inactive.ToString()]).Trim(),
                                                                         Convert.ToString(reportRow[EnumManager.Columns.IntacctIdFromRefuse.ToString()]).Trim(),
                                                                         Convert.ToString(reportRow[EnumManager.Columns.PropertyId.ToString()]).Trim(),
                                                                         Convert.ToString(reportRow[EnumManager.Columns.Currency.ToString()]).Trim()
                                                                         );
        }
        private static string GetPKInvoiceString(DataRow reportRow)
        {
            return string.Format(Constants.UniqueInvoiceString, Convert.ToString(reportRow[EnumManager.Columns.InvoiceNo.ToString()]).Trim(),
                                                                         Convert.ToString(reportRow[EnumManager.Columns.AmountDue.ToString()]).Trim(),
                                                                         Convert.ToString(reportRow[EnumManager.Columns.HaulerName.ToString()]).Trim(),
                                                                         Convert.ToString(reportRow[EnumManager.Columns.AuditedDate.ToString()]).Trim(),
                                                                         Convert.ToString(reportRow[EnumManager.Columns.DueDate.ToString()]).Trim(),
                                                                         Convert.ToString(reportRow[EnumManager.Columns.GroupNbr.ToString()]).Trim(),
                                                                         Convert.ToString(reportRow[EnumManager.Columns.Item.ToString()]).Trim(),
                                                                         Convert.ToString(reportRow[EnumManager.Columns.AccountNO.ToString()]).Trim(),
                                                                         Convert.ToString(reportRow[EnumManager.Columns.HaulerInvoiceNo.ToString()]).Trim(),
                                                                         Convert.ToString(reportRow[EnumManager.Columns.IntacctIdFromRefuse.ToString()]).Trim(),
                                                                         Convert.ToString(reportRow[EnumManager.Columns.GroupNbr.ToString()]).Trim()
                                                                         );
        }
        public static string GetintacctString(EnumManager.IntacctType IntacctDataType, string intId, IntacctLogin objLogin, ref StringBuilder logFileContent, DataSet intacctData)
        {

            try
            {
                switch (IntacctDataType)
                {
                    case EnumManager.IntacctType.Customer:
                        return GetIntacctCustomerString(IntacctDataType, intId, objLogin, intacctData);

                    case EnumManager.IntacctType.Project:
                        return GetIntacctPropertyString(IntacctDataType, intId, objLogin, intacctData);

                    //case EnumManager.IntacctDataType.Hauler:
                    //    return GetIntacctHaulerString(IntacctId, this.IntacctContext.Session, this.IntacctContext.Request, this.IntacctContext.IntacctFCVersion);


                    default: return string.Empty;
                }
            }
            catch (Exception ex)
            {
                // Log Error
                logFileContent.AppendLine(Constants.CompareError + ex.Message);
            }
            return string.Empty;
        }

        public static string GetIntacctCustomerString(EnumManager.IntacctType IntacctDataType, string intId, IntacctLogin objLogin, DataSet Intacctds)
        {

            //Intacctds = IntacctConnector.read(IntacctDataType.ToString, objLogin.objConnectionEntity.SessionId, ItemChoiceType2.get_list, type: "get_list", expValue1: intId, field1: Constants.InvoiceNo);
            Intacctds = IntacctConnector.read("CUSTOMER", objLogin, ItemChoiceType2.get_list, type: "get_list", expValue1: intId, field1: "CUSTOMERID");
            if (!string.IsNullOrEmpty(intId))
            {

                System.Data.DataTable data = new DataTable();

                if (!DataManager.IsNullOrEmptyDataSet(Intacctds))
                    data = Intacctds.Tables[Constants.Zero];

                //refine dataset using intId

                if (!DataManager.IsNullOrEmptyDataTable(data))
                {
                    //int ConvertedIntId = Convert.ToInt32(intId);
                    //IEnumerable<DataRow> rows = data.AsEnumerable().Where(r => r.Field<int>("CUSTOMERID") == ConvertedIntId);
                    try
                    {
                        DataRow[] foundRows;
                        DataRow displayContactRow, contactRow;
                        foundRows = Intacctds.Tables["customer"].Select("CUSTOMERID = '" + intId + "'");
                        displayContactRow = Intacctds.Tables["mailaddress"].Rows[Constants.Zero];
                        contactRow = Intacctds.Tables["contact"].Rows[Constants.Zero];
                        if (DataManager.IsDataRowHasData(foundRows))
                        {
                            DataRow reportRow = foundRows[Constants.Zero];

                            return string.Format(Constants.CustomerOrHaulerString,
                                                                            Convert.ToString(reportRow[EnumManager.Columns.customerid.ToString().ToLower()]).Trim(),
                                                                            Convert.ToString(reportRow[EnumManager.Columns.NAME.ToString().ToLower()]).Trim(),
                                                                            Convert.ToString(reportRow[EnumManager.Columns.NAME.ToString().ToLower()]).Trim(),
                                                                            Convert.ToString(displayContactRow["address1"]).Trim(),
                                                                            Convert.ToString(displayContactRow["address2"]).Trim(),
                                                                            Convert.ToString(displayContactRow["city"]).Trim(),
                                                                            Convert.ToString(displayContactRow["state"]).Trim(),
                                                                            Convert.ToString(displayContactRow["zip"]).Trim(),
                                                                            Convert.ToString(reportRow[EnumManager.Columns.NAME.ToString()]).Trim(),
                                                                            Convert.ToString(contactRow["fax"]).Trim(),
                                                                            Convert.ToString(contactRow["email1"]).Trim(),
                                                                            Convert.ToString(contactRow["phone1"]).Trim(),
                                                                            Convert.ToString(reportRow["status"]).Trim()
                                                                            );

                        }

                    }




                    catch (Exception ex)
                    {
                        ErrorLog.GenerateErrorDetails(ex, string.Empty, string.Empty, string.Empty, Constants.TechnicalError);
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
                }
                else
                {
                    ErrorLog.GenerateErrorDetails(intId + Constants.NotFouindInIntacct, string.Empty, string.Empty, string.Empty, Constants.FunctionalError);
                    ErrorLog.LogIntacctExceptions(string.Empty, SecurityContext.Instance.LogInUserId,
                                                                     SecurityContext.Instance.LogInPassword,
                                                                     IntacctDataType.ToString(),
                                                                     string.Empty,
                                                                     IntacctDataType.ToString(),
                                                                     intId + Constants.NotFouindInIntacct,
                                                                     string.Empty,
                                                                     SecurityManager.GetIPAddress.ToString(),
                                                                     Constants.FunctionalError,
                                                                     string.Empty);


                }

            }
            else
            {
                ErrorLog.GenerateErrorDetails(intId + Constants.NotFouindInIntacct, string.Empty, string.Empty, string.Empty, Constants.FunctionalError);
                ErrorLog.LogIntacctExceptions(string.Empty, SecurityContext.Instance.LogInUserId,
                                                                 SecurityContext.Instance.LogInPassword,
                                                                 IntacctDataType.ToString(),
                                                                 string.Empty,
                                                                 IntacctDataType.ToString(),
                                                                 intId + Constants.NotFouindInIntacct,
                                                                 string.Empty,
                                                                 SecurityManager.GetIPAddress.ToString(),
                                                                 Constants.FunctionalError,
                                                                 string.Empty);


            }
            return string.Empty;
        }

        public static string GetIntacctPropertyString(EnumManager.IntacctType IntacctDataType, string intId, IntacctLogin objLogin, DataSet Intacctds)
        {
            Intacctds = IntacctConnector.read("PROJECT", objLogin, ItemChoiceType2.get_list, type: "get_list", expValue1: intId, field1: "PROJECTID");
            if (!string.IsNullOrEmpty(intId))
            {
                System.Data.DataTable data = new DataTable();

                //System.Data.DataSet Intacctds = CustomerCommon.read("PROJECT", objLogin, ItemChoiceType2.read, type: "readByQuery");
                if (!DataManager.IsNullOrEmptyDataSet(Intacctds))
                    data = Intacctds.Tables[Constants.Zero];

                //refine dataset using intId

                if (!DataManager.IsNullOrEmptyDataTable(data))
                {
                    DataRow[] foundRows;
                    foundRows = Intacctds.Tables["project"].Select("key = '" + intId + "'");
                    if (DataManager.IsDataRowHasData(foundRows))
                    {
                        DataRow reportRow = foundRows[Constants.Zero];
                        return string.Format(Constants.UniquePropertyString,
                                                                       Convert.ToString(reportRow[EnumManager.Columns.CUSTOMERID.ToString().ToLower()]).Trim(),
                                                                        Convert.ToString(reportRow[EnumManager.Columns.NAME.ToString().ToLower()]).Trim(),
                                                                        Convert.ToString(reportRow[EnumManager.Columns.PROJECTTYPE.ToString().ToLower()]).Trim(),
                                                                        Convert.ToString(reportRow[EnumManager.Columns.STATUS.ToString().ToLower()]).Trim(),
                                                                        Convert.ToString(reportRow[EnumManager.Columns.Key.ToString().ToLower()]).Trim(),
                                                                        Convert.ToString(reportRow[EnumManager.Columns.Key.ToString().ToLower()]).Trim(),
                                                                        Convert.ToString(reportRow[EnumManager.Columns.Currency.ToString().ToLower()]).Trim()
                                                                        );


                    }




                }
            }
            return string.Empty;
        }
        #endregion
    }
}
