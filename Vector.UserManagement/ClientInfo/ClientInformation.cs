namespace Vector.UserManagement.ClientInfo
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ClientInformation : IDisposable
    {

        #region GET / SET PROPERTIES 

        public int ClientID { get; set; }

        public string ClientName { get; set; }

        public string ClientPhoneNumber { get; set; }

        public string ClientDOB { get; set; }

        public string ClientGender { get; set; }

        public string ClientAddress { get; set; }

        #endregion

        #region"                DISPOSING               "

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool dispose)
        {

            if (dispose)
            {
                ClientID = 0;
                ClientName = string.Empty;
                ClientPhoneNumber = string.Empty;
                ClientDOB = string.Empty;
                ClientGender = string.Empty;
                ClientAddress = string.Empty;
            }

        }

        #endregion

    }
}
