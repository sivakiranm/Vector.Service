using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using Vector.Common.BusinessLayer;
using Vector.Common.Entities;

namespace Vector.Common.DataLayer
{
    public class UserAuthenticationDL : DisposeLogic
    {
        private Database objVectorConnection;
        private VectorDataConnection objVectorDB;
        DataSet dsData = new DataSet();

        public UserAuthenticationDL(VectorDataConnection objVectorDB)
        {
            this.objVectorDB = objVectorDB;
        }

        private Database GetVectorDBInstance()
        {
            return objVectorDB.GetVectorDBConnection();
        }

        #region UserAuthentication Layer

        public DataSet UserAuthentication(string loginId, string pswd)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorUserManagement_New", "UserLoginCheck", loginId, pswd, 0, null);
        }

        public DataSet UserTokenAuthentication(string userToken)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorUserManagement_New", "UserTokenCheck", "", "", 0, userToken);
        }
        public DataSet GetPasswordByEmail(string emailId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorUserManagement_New", "GetPasswordByEmail", emailId, "", 0, null);
        }
        public DataSet ChangePassword(string loginId, string password, string oldPassword)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorUserChangePassword.ToString(), loginId, password, oldPassword);
        }
        //public DataSet changePasswordToken(string loginId, string pswd, string oldPassword)
        //{
        //    objVectorConnection = GetVectorDBInstance();
        //    return objVectorConnection.ExecuteDataSet("VectorUserChangePassword", "ChangePassword", loginId, pswd, null, oldPassword);
        //}
        #endregion

    }
}
