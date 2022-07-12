using System;
using System.Collections.Generic;
using System.Text;

namespace Vector.Common.TokenAuthentication
{

    public class TokenInfo
    {
        public List<Token>Token { get; set; }
    }

    public class Token
    {

        public string Value { get; set; }

        public DateTime ExpiryDate { get; set; } 

        public Int64 userId { get; set; }
        public string userName { get; set; }
        public string userType { get; set; }
        public string userRole { get; set; }
        public Int64 userRoleId { get; set; }
        public string vectorToken { get; set; }
        public string tokenValidityFrom { get; set; }
        public string tokenValidityto { get; set; }

        public string phoneNumber { get; set; }
        public string email { get; set; }

        public string timeZone { get; set; }



    }
}
