using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vector.Intacct.Entities;

namespace Vector.Intacct.BusinessLogic
{
    public static class ErrorLog
    {
        /// <summary>
        /// Log Bulk Error When more then 1 records are trying to be Added/uploaded into intacct
        /// </summary>
        /// <param name="errorDertails"></param>
        /// <returns></returns>
        public static bool LogError(List<ErrorEntity> errorDertails)
        {
            bool result = false;

            //Login To Write Error

            return result;
        }

        public static bool LogError(ErrorEntity errorDertails)
        {
            bool result = false;

            //Login To Write Error

            return result;
        }

        /// <summary>
        /// Generic error log method that will be caught from Try & Catch block..
        /// exceptions like unable to connect to service etc
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="entity"></param>
        /// <param name="sessionId"></param>
        /// <param name="requestXML"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool GenerateErrorDetails(Exception ex, string entity, string sessionId, string requestXML, string type)
        {
            using (ErrorEntity objException = new ErrorEntity())
            {
                objException.EntityName = entity;
                objException.requestXml = requestXML;

                objException.ErrorType = type;
                objException.IntacctSession = sessionId;
                objException.requestXml = requestXML;
                objException.ErrorDescription = ex.Message;
                objException.ErrorInnerException = ex.InnerException != null ? Convert.ToString(ex.InnerException) : string.Empty;
                return LogError(objException);
            }
        }

        public static bool GenerateErrorDetails(string errorXml, string entity, string sessionId, string requestXML, string type)
        {
            using (ErrorEntity objException = new ErrorEntity())
            {
                objException.EntityName = entity;
                objException.requestXml = requestXML;

                objException.ErrorType = type;
                objException.IntacctSession = sessionId;
                objException.requestXml = requestXML;
                objException.errorXml = errorXml;

                return LogError(objException);
            }
        }

        public static void LogIntacctExceptions(string errorLog, string loginid, string password, string customerKey,
                                                                         string intacct, string entityType, string errorDescription,
                                                                         string intacctSession, string systemIp, string errorType, string xml)
        {
            using (IntacctBOLayer objIntacctBOLayer = new IntacctBOLayer())
            {
                using (IntacctException objIntacctException = new IntacctException())
                {
                    //objIntacctException.ErrorLog = errorLog;  
                    //objIntacctException.loginid = loginid;
                    //objIntacctException.password = password;
                    objIntacctException.CustomerKey = customerKey;
                    objIntacctException.Intacct = intacct;
                    objIntacctException.EntityType = entityType;
                    objIntacctException.ErrorDescription = errorDescription;
                    objIntacctException.IntacctSession = intacctSession;
                    objIntacctException.SystemIp = systemIp;
                    objIntacctException.ErrorType = errorType;
                    objIntacctException.Xml = xml;
                    objIntacctException.UserId = SecurityContext.Instance.UserId.ToString();

                    objIntacctBOLayer.LogException(objIntacctException);
                }
            }
        }
    }
}
