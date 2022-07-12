using System.Data;

namespace Vector.Intacct.BusinessLogic
{
    public class SecurityContext
    {
        private static SecurityContext instance;
        private string logInUserId;
        private string logInPassword;
        private DataTable userDetails;
        private int userId;

        private string token;
        private string version = string.Empty;

        public DataTable UserDetails
        {
            set
            {
                this.userDetails = value;
            }
            get
            {
                return userDetails;
            }
        }
        public string LogInUserId
        {
            get
            {
                return logInUserId;
            }
            set
            {
                this.logInUserId = value;
            }
        }

        public int UserId
        {
            get
            {
                return userId;
            }
            set
            {
                this.userId = value;
            }
        }

        public string LogInPassword
        {
            get
            {
                return logInPassword;
            }
            set
            {
                this.logInPassword = value;
            }
        }
        public string AppVersion
        {
            set
            {
                this.version = value;
            }
            get
            {
                return version;
            }
        }

        public string VectorToken
        {
            set
            {
                this.token = value;
            }
            get
            {
                return token;
            }
        }

        public static SecurityContext Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SecurityContext();
                }

                return instance;
            }
        }
    }

}
