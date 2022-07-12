using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vector.API.Handlers;
using Vector.Common.BusinessLayer;
using Vector.Common.Entities;

namespace Vector.API.Controllers
{
    [RoutePrefix("api/common")]
    [VectorActionAutorizationFilter]
    public class CommonController : ApiController
    {
        [HttpPost]
        [Route("SendEmail")]
        public IHttpActionResult SendEmail(SendEmail objSendEmail)
        {
            bool emailResult = EmailManager.SendEmail(objSendEmail);
            if (emailResult)
                return VectorResponseHandler.GetVectorResponse(new VectorResponse<string>() { ResponseData = "Email sent successfully" });
            else
                return VectorResponseHandler.GetVectorResponse(new VectorResponse<string>() { Error = new Error() { ErrorDescription = "Unable to send an Email.Please contact Administrator." } });
        }

        [HttpGet]
        [Route("ReadEmailsFromServer")]
        [AllowAnonymous]
        public IHttpActionResult ReadEmailsFromServer()
        {
            Boolean resultFinal = true;

            string server = SecurityManager.GetConfigValue("EmailServer");
            string emailId = SecurityManager.GetConfigValue("EmailId");
            string password = SecurityManager.GetConfigValue("Password");
            string serverProtocol = SecurityManager.GetConfigValue("ServerProtocol");

            EmailProcessManager.MailDetails result = new EmailProcessManager.MailDetails();

            result = EmailProcessManager.ReadingMailsToGetMailDetails(server, emailId, password, 100, serverProtocol, "All");

            if (resultFinal)
            {
                return VectorResponseHandler.GetVectorResponse(new VectorResponse<string>() { ResponseData = "Files moved successfully" });

            }
            else
                return VectorResponseHandler.GetVectorResponse(new VectorResponse<string>() { Error = new Error() { ErrorDescription = "Files moving failed" } });
        }

         

    }
}
