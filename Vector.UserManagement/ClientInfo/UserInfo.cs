namespace Vector.UserManagement.ClientInfo
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class userInfo
    {
        public Int64 userId { get; set; }

        public string userName { get; set; }

        public string userType { get; set; }

        public string userRole { get; set; }

        public Int64 userRoleId { get; set; }

        public string VectorToken { get; set; }

        public string tokenValidityFrom { get; set; }

        public string tokenValidityto { get; set; }


    }
}
