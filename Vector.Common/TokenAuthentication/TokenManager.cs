using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vector.Common.TokenAuthentication
{
    public class TokenManager : ITokenManager
    {
        List<string> userNames = new List<string>();
      

        #region"                CONSTRUCTOR USED                "

        private List<Token> listTokens;

        public TokenManager()
        {
            listTokens = new List<Token>();

            this.userNames.Add("Stakeholder");
            this.userNames.Add("ClientUser");
            this.userNames.Add("CSPUser");
        }

        #endregion

        #region"                METHOD FOR CHECKING THE USER                "

        public bool Authenticate(string userName, string pwd)
        {
            bool authentication = false;
            if (!string.IsNullOrEmpty(userName) &&
               !string.IsNullOrEmpty(pwd))
            {

                authentication = CheckUserAuthentication(userName, pwd);
            }

                return authentication;

        }

        private bool CheckUserAuthentication(string userName, string pwd)
        {
            bool authentication = false;
          

            if (this.userNames.Contains(userName) && pwd.Equals("Welcome"))
            {
                authentication = true;
            }

            return authentication;
        }

        #endregion

        #region"                METHOD FOR GENERATING THE NEW TOKEN             "

        public Token NewToken(string loginId,string password)
        {

            Token objUserDetails = PrepareUserLoginInfo(loginId);
 

            listTokens.Add(objUserDetails);
            return objUserDetails;

        }


        private Token PrepareUserLoginInfo(string userLoginId)
        {
            //UsersInformation usersinformation = new UsersInformation();
            //userInfo userinfo = new userInfo();
            //usersinformation.userInfo = new System.Collections.Generic.List<userInfo>();
            string tokenValue = "STAKEHOLDERTOKENVECTORENCRYPED";
            string userType = "Internal";
            string userPersona = "Stakeholder";
            string phoneNumber = "+1-(202)-544-1100";
            string email = "elizabeth@vector.com";
            string timeZone = "PST";
            string userName = "Elizabeth Sakala";
            int userRoleId = 1;

            if(userLoginId.Equals("ClientUser"))
            {
                tokenValue = "CLIENTUSERTOKENVECTORENCRYPED";
                userType = "External";
                userPersona = "Property Manager";
                userRoleId = 2;
                userName = "Julian Jimenez";
                phoneNumber = "+1-(202)-588-6500";
                timeZone = "PST";
                email = "Julian@vector.com";
            }
            else
            if (userLoginId.Equals("CSPUser"))
            {
                tokenValue = "CustomerSupportUSERTOKENVECTORENCRYPED";
                userType = "Internal";
                userPersona = "Customer Support";
                userRoleId = 3;
                userName = "Kristin Garrett";
                phoneNumber = "+1-(202)-588-6500";
                timeZone = "EST";
                email = "Kristin@vector.com";
                
            }



            var userInfo = new Token
            {
                Value = tokenValue,
                ExpiryDate = DateTime.Now.AddDays(365), 
                userId = 1,
                userName = userName,
                userType = userType,
                userRole = userPersona,
                userRoleId = userRoleId,
                vectorToken = tokenValue,
                tokenValidityFrom = "12/31/2020",
                tokenValidityto = "01/31/2021",
                phoneNumber = phoneNumber,
                timeZone = timeZone,
                email = email
            }; 

            return userInfo;
        }

        #endregion

        #region"                METHOD FOR VERIFYING THE TOKEN IS ALREADY EXISTS OR NOT             "

        public bool VerifyToken(string token)
        {

           return CheckIfTokenValid(token);

        }

        private bool CheckIfTokenValid(string token)
        {
            List<string> tokes = new List<string>();
            tokes.Add("STAKEHOLDERTOKENVECTORENCRYPED");
            tokes.Add("CLIENTUSERTOKENVECTORENCRYPED");
            tokes.Add("CustomerSupportUSERTOKENVECTORENCRYPED");

            if (tokes.Contains(token))
                return true;
            else
                return false;
        }

        #endregion

    }
}
