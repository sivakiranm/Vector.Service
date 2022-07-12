
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Vector.Intacct.Entities;

namespace Vector.Intacct.BusinessLogic
{
    public class IntacctLogin : BaseLogic
    {
        #region ConnectionParameters

        //public string SessionId;
        //public string EndPoint;
        //public string CompanyId;
        //public string UserId;
        //public string SenderId;
        //public string SenderPassword;
        public bool Transaction = true;
        //public string UserPassword;
        //public string EntityType;
        //public string EntityId;
        //public string LoginUrl;
        public ConnectionEntity objConnectionEntity = new ConnectionEntity();

        public

        #endregion

        #region Constants

 const string defaultLoginUrl = "loginUrl";
        const string xmlNodeapiPath = "/response/operation/result/data/api";
        const string sessionid = "sessionid";
        const string endpoint = "endpoint";
        const string uniqueId = "uniqueId";
        const string dtdVersionThree = "dtdVersionThree";
        const string dtdVersionTwo = "dtdVersionTwo";

        #endregion

        #region Constructor

        public IntacctLogin(ConnectionEntity objConnectionEntity)
        {
            this.objConnectionEntity = objConnectionEntity;
            //this.CompanyId = objConnectionEntity;
            //this.UserId = userId;
            //this.UserPassword = userPassword;
            //this.SenderId = senderId;
            //this.SenderPassword = senderPassword;
            //this.EntityType = entityType;
            //this.EntityId = entityId;
            //this.LoginUrl = string.IsNullOrEmpty(loginUrl) ? ConfigurationManager.AppSettings[defaultLoginUrl] : loginUrl;
        }

        #endregion

        #region Methods

        /// <summary>
        /// BuildXmlRequest which is used to post the xml data.
        /// </summary>
        /// <returns></returns>
        private string BuildXmlRequest()
        {
            #region BuildHeader

            senderid objSenderid = new senderid();
            objSenderid.Text = Common.AddText(objConnectionEntity.SenderId);
            password objPassword = new password();
            objPassword.Text = Common.AddText(objConnectionEntity.SenderPassword);
            controlid objControlid = new controlid();
            objControlid.Text = Common.AddText(Constants.controlId);
            uniqueid objUniqueid = new uniqueid();
            //objUniqueid.Text = IntacctBusinessLogic.AddText(ConfigurationManager.AppSettings[uniqueId]);
            objUniqueid.Text = Common.AddText("false");
            dtdversion objVersion = new dtdversion();
            //objVersion.Text = IntacctBusinessLogic.AddText(ConfigurationManager.AppSettings[dtdVersionTwo]);
            objVersion.Text = Common.AddText("2.1");
            userid objUserid = new userid();
            objUserid.Text = Common.AddText(objConnectionEntity.UserId);
            sessionid objSessionId = new sessionid();
            objSessionId.Text = Common.AddText(objConnectionEntity.SessionId);
            companyid objComp = new companyid();
            objComp.Text = Common.AddText(objConnectionEntity.CompanyId);
            password objPwd = new password();
            objPwd.Text = Common.AddText(objConnectionEntity.UserPassword);
            login objLogin = new login();
            objLogin.userid = objUserid;
            objLogin.companyid = objComp;
            objLogin.password = objPwd;

            #endregion

            #region Build Function

            control objControl = new control();
            objControl.senderid = objSenderid;
            objControl.password = objPassword;
            objControl.controlid = objControlid;
            objControl.uniqueid = objUniqueid;
            objControl.dtdversion = objVersion;
            getAPISession objgetApiSession = new getAPISession();
            authentication objAuth = new authentication();
            objAuth.Item = objLogin;
            function objfunction = new function();
            objfunction.controlid = Constants.controlId;
            objfunction.Item = objgetApiSession;
            objfunction.ItemElementName = ItemChoiceType2.getAPISession;
            List<function> objFunctionList = new List<function>();
            objFunctionList.Add(objfunction);
            content objContent = new content();
            objContent.function = Common.AddFunctions(objFunctionList);
            operation objOperation = new operation();
            objOperation.authentication = objAuth;
            objOperation.content = Common.AddContents(objContent);
            request objRequest = new request();
            objRequest.control = objControl;
            objRequest.operation = Common.AddOperations(objOperation);

            #endregion

            XmlSerializerNamespaces objNamespaces = new XmlSerializerNamespaces();
            objNamespaces.Add(Constants.Empty, Constants.xmlNamespace);
            var ms = new MemoryStream();
            var xw = XmlWriter.Create(ms); // Remember to stop using XmlTextWriter  
            var serializer = new XmlSerializer(typeof(request));
            serializer.Serialize(xw, objRequest, objNamespaces);
            xw.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(ms, Encoding.UTF8);
            string xmlRequest = sr.ReadToEnd();
            sr.Close();
            return xmlRequest;

        }

        /// <summary>
        /// It is used to connect to the Intacct and create a session
        /// </summary>
        /// <returns></returns>
        public bool ConnectionCredentials(ConnectionEntity objConnection)
        {
            string LoginUrl = objConnectionEntity.IntacctApiUrl;
            string xml = BuildXmlRequest();

            string response = IntacctConnector.ExecuteRequest(xml, LoginUrl);
            string secondresponse = string.Empty;
            if (string.IsNullOrEmpty(response))
            {
                secondresponse = IntacctConnector.ExecuteRequest(xml, LoginUrl);
            }
            if (!string.IsNullOrEmpty(response))
            {
                return CheckConnection(objConnection, response);

            }
            else
            {     //give a second try for the sessionid
                return CheckConnection(objConnection, secondresponse);
            }
        }

        private bool CheckConnection(ConnectionEntity objConnection, string response)
        {
            if (!string.IsNullOrEmpty(response))
            {
                if (string.IsNullOrEmpty(validateConnection(response)))
                {
                    XmlDocument objDoc = new XmlDocument();
                    objDoc.LoadXml(response);
                    XmlNode responseObj = objDoc.SelectSingleNode(xmlNodeapiPath);
                    objConnectionEntity.SessionId = responseObj.SelectSingleNode(sessionid).InnerXml;
                    objConnection.SessionId = responseObj.SelectSingleNode(sessionid).InnerXml;
                    objConnectionEntity.EndPoint = responseObj.SelectSingleNode(endpoint).InnerXml;
                    objConnection.EndPoint = responseObj.SelectSingleNode(endpoint).InnerXml;
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Validates the xml response
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static string validateConnection(string response)
        {
            XmlDocument objResponseDoc = new XmlDocument();
            objResponseDoc.LoadXml(response);

            if (objResponseDoc == null)
            {
                return Constants.InValidXmlResponse;
            }

            if (string.Equals(objResponseDoc.SelectSingleNode(Constants.ResponseControlStatus).InnerXml, Constants.Failure))
            {
                return Common.xmlErrorToString(objResponseDoc.SelectSingleNode(Constants.ResponseErrorMessage));
            }


            if (!string.IsNullOrEmpty(objResponseDoc.SelectSingleNode(Constants.ResponseOperationAuthenticationStatus).InnerXml))
            {
                if (objResponseDoc.SelectSingleNode(Constants.ResponseOperationAuthenticationStatus).InnerXml != Constants.Success)
                {
                    XmlNode error = objResponseDoc.SelectSingleNode(Constants.ResponseOperationErrorMessage);
                    return Constants.Space + Constants.Error + Constants.Space + error.SelectSingleNode(Constants.ErrorDescriptionTwo).FirstChild;//check whether it is error or error[0]
                }
                else
                    return string.Empty;
            }

            string status = objResponseDoc.SelectSingleNode(Constants.ResponseOperationResultStatus).InnerXml;
            if (!string.IsNullOrEmpty(status))
            {
                if (!string.Equals(status, Constants.Success))
                {
                    XmlNode error = objResponseDoc.SelectSingleNode(Constants.ResponseOperationResultErrorMessage);
                    return Constants.Space + Constants.Error + Constants.Space + error.SelectSingleNode(Constants.ErrorDescriptionTwo).FirstChild;
                }
                else
                    return string.Empty;
            }
            else if (!string.IsNullOrEmpty(objResponseDoc.SelectSingleNode(Constants.ResponseOperation).InnerXml))
            {
                if (string.IsNullOrEmpty(objResponseDoc.SelectSingleNode(Constants.ResponseErrorMessage).InnerXml))
                {
                    return Common.xmlErrorToString(objResponseDoc.SelectSingleNode(Constants.ResponseErrorMessageError).FirstChild);//check whether it is error or error[0]
                }
                else
                    return string.Empty;
            }
            else
                return string.Empty;

        }

        #endregion
    }
}
