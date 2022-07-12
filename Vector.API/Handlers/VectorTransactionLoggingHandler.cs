using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vector.API.Handlers
{
    public class VectorTransactionLoggingHandler : MessageHandler
    {
        private const string RequestFormat = "{0} - Request: {1}\r\n{2}";
        private const string ResponseFormat = "{0} - Response: {1}\r\n{2}";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="requestInfo"></param>
        /// <param name="message"></param>
        protected override void IncomingMessageAsync(string correlationId, string requestInfo, byte[] message)
        {
            //await Task.Run(() =>
            //    Debug.WriteLine(string.Format("{0} - Request: {1}\r\n{2}", correlationId, requestInfo, Encoding.UTF8.GetString(message))));            
            // EMSTextLogger.WriteToLogFile(string.Format(RequestFormat, correlationId, requestInfo, Encoding.UTF8.GetString(message) + " SysTime:" + DateTime.Now.ToString("dd/mm/YYYY hh:mm:ss")));
        }


        protected override void OutgoingMessageAsync(string correlationId, string requestInfo, byte[] message)
        {
            // EMSTextLogger.WriteToLogFile(string.Format(ResponseFormat, correlationId, requestInfo, Encoding.UTF8.GetString(message) + " SysTime:" + DateTime.Now.ToString("dd/mm/YYYY hh:mm:ss")));
        }
    }
}