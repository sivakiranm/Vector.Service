using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Configuration;
using System;
using Vector.Common.BusinessLayer;

namespace Vector.Common.DataLayer
{
    public class VectorDataConnection : DisposeLogic
    {
        public string vectorConnect { get; set; }

        public VectorDataConnection(string vectorConnectionString)
        {
            this.vectorConnect = vectorConnectionString;
        }

        public VectorDataConnection()
        {
            //this.vectorConnect = "Server=tcp:vectorplatformdevserver.database.windows.net,1433;Initial Catalog=vectorplatform-db;Persist Security Info=False;User ID=dbadmin;Password=vectorplatform@2;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            this.vectorConnect = SecurityManager.GetConfigValue("VectorDbConnection");
        }

        public Database GetVectorDBConnection()
        {
            Database vectorDBConnection = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(this.vectorConnect); 
            return vectorDBConnection;
        }
    }
}
