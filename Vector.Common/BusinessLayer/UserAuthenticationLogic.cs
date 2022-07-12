using Vector.Common.DataLayer;
using Vector.Common.Entities;

namespace Vector.Common.BusinessLayer
{
    public class UserAuthenticationLogic : DisposeLogic
    {        
        private VectorDataConnection objVectorDB;
        public UserAuthenticationLogic(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }

        public VectorResponse<object> AuthenticateUser(string userName, string password,int keyLength)
        {
            using (var objUserAuthenticationDA = new UserAuthenticationDL(objVectorDB))

            {
                string decryptedPassword = Common.Decrypt(password, keyLength);
                var result = objUserAuthenticationDA.UserAuthentication(userName, decryptedPassword);
                if (DataValidationLayer.isDataSetNotNull(result))
                {

                    return new VectorResponse<object>() { ResponseData = result.Tables[0] };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Login Failed, Kindly check User Name & Password!" } };

                }
            }
        }

        public VectorResponse<object> MobileAuthenticateUser(string userName, string password)
        {
            using (var objUserAuthenticationDA = new UserAuthenticationDL(objVectorDB))
            {
                var result = objUserAuthenticationDA.UserAuthentication(userName, password);
                if (DataValidationLayer.isDataSetNotNull(result))
                {

                    return new VectorResponse<object>() { ResponseData = result.Tables[0] };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Login Failed, Kindly check User Name & Password!" } };

                }
            }
        }
        public VectorResponse<string> GetPasswordByEmail(string emailId)
        {
            using (var objUserAuthenticationDA = new UserAuthenticationDL(objVectorDB))
            {
                var result = objUserAuthenticationDA.GetPasswordByEmail(emailId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    SendEmail objSendEmail = new SendEmail();
                    objSendEmail.To = emailId;
                    objSendEmail.Subject = "Forgot Password";
                    objSendEmail.Body = "Your Password: " + result.Tables[0].Rows[0]["password"].ToString();
                

                    bool emailResult = EmailManager.SendEmail(objSendEmail);
                    if (emailResult)
                        return new VectorResponse<string>() { ResponseData = "Password sent to your email successfully" };
                    else
                    return new VectorResponse<string>() { Error = new Error() { ErrorDescription = "Unable to send an Email.Please contact Administrator." } };

                }
                else
                {
                    return new VectorResponse<string>() { Error = new Error() { ErrorDescription = "Please Enter Valid EmailId" } };

                }
            }
        }
        public VectorResponse<object> ChangePassword(string loginId, string password, string oldPassword)
        {
            using (var objUserAuthenticationDA = new UserAuthenticationDL(objVectorDB))
            {
                var result = objUserAuthenticationDA.ChangePassword( loginId,  password,  oldPassword);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Password update Failed" } };

                }
            }
        }

        //public VectorResponse<object> changePasswordToken(string userName, string password, int keyLength, string oldPassword)
        //{
        //    using (var objUserAuthenticationDA = new UserAuthenticationDL(objVectorDB))
        //    {
        //        string decryptedPassword = Common.Decrypt(password, keyLength);
        //        var result = objUserAuthenticationDA.changePasswordToken(userName, decryptedPassword, oldPassword);
        //        if (DataValidationLayer.isDataSetNotNull(result))
        //        {

        //            return new VectorResponse<object>() { ResponseData = result.Tables[0] };

        //        }
        //        else
        //        {
        //            return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Login Failed, Kindly check User Name & Password!" } };

        //        }
        //    }
        //}
    }
}
