using System;
using System.Web.Http;
using System.Web.Http.Cors;
using Vector.API.Handlers;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.UserManagement.Entities;

namespace Vector.API.Controllers
{
    [RoutePrefix("api/authenticate")]

    [EnableCors(origins: "http://localhost:4200/", headers: "*", methods: "*")]
    public class AuthenticateController : ApiController
    {
        [HttpGet]
        [Route("ValidateUser")]
        public IHttpActionResult Authenticate(string user, string pwd)
        {
            using (var objAuthenticateBL = new UserAuthenticationLogic(new VectorDataConnection()))
            {
                int keyLength = Convert.ToInt32(SecurityManager.GetConfigValue("EncryptionKeyLength"));
                return VectorResponseHandler.GetVectorResponse(objAuthenticateBL.AuthenticateUser(user, pwd, keyLength));
            }
        }
        [HttpGet]
        [Route("MobileValidateUser")]
        public IHttpActionResult MobileAuthenticate(string user, string pwd)
        {
            using (var objAuthenticateBL = new UserAuthenticationLogic(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objAuthenticateBL.MobileAuthenticateUser(user, pwd));
            }
        }

        [HttpGet]
        [Route("ValidateEmail")]
        public IHttpActionResult GetPasswordByEmail(string emailId)
        {
            using (var objAuthenticateBL = new UserAuthenticationLogic(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objAuthenticateBL.GetPasswordByEmail(emailId));
            }
        }
        [HttpPost]
        [Route("ChangePassword")]
        public IHttpActionResult ChangePassword(ChangePassword objChangePassword)
        {
            using (var objAuthenticateBL = new UserAuthenticationLogic(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objAuthenticateBL.ChangePassword(objChangePassword.loginId, objChangePassword.newPassword, objChangePassword.oldPassword));
            }
        }

        //[HttpGet]
        //[Route("changePasswordToken")]
        //public IHttpActionResult changePasswordToken(string user, string pwd, string oldPassword)
        //{
        //    using (var objAuthenticateBL = new UserAuthenticationLogic(new VectorDataConnection()))
        //    {
        //        int keyLength = Convert.ToInt32(SecurityManager.GetConfigValue("EncryptionKeyLength"));
        //        return VectorResponseHandler.GetVectorResponse(objAuthenticateBL.changePasswordToken(user, pwd, keyLength, oldPassword));
        //    }
        //}
    }
}
