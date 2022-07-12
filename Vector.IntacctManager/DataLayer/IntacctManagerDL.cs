using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;

namespace Vector.IntacctManager.DataLayer
{
    public class IntacctManagerDL : DisposeLogic
    {
        private Database objVectorConnection;
        private VectorDataConnection objVectorDB;

        public IntacctManagerDL(VectorDataConnection objVectorDB)
        {
            this.objVectorDB = objVectorDB;
        }

        private Database GetVectorDBInstance()
        {
            return objVectorDB.GetVectorDBConnection();
        }

        public DataSet GetCustomerData(string fromDate, string toDate)
        {
            objVectorConnection = GetVectorDBInstance();
            var cmd = objVectorConnection.GetStoredProcCommand("VectorManageIntacctClientInfo", "CustomerQuery", fromDate, toDate, null, null, null,null);
            return objVectorConnection.ExecuteDataSet(cmd);
        }

        public DataSet GetPropertyData(string fromDate, string toDate, string SyncType = null)
        {
            objVectorConnection = GetVectorDBInstance();
            if (StringManager.IsEqual(SyncType, "Intacct"))
            {
                var cmd = objVectorConnection.GetStoredProcCommand("VectorManageIntacctPropertyInfo", "UniquePropertyQuery", fromDate, toDate, null, null, null,null);
                return objVectorConnection.ExecuteDataSet(cmd);
            }
            else
            {
                var cmd = objVectorConnection.GetStoredProcCommand("VectorManageIntacctPropertyInfo", "PropertyQuery", fromDate, toDate, null, null, null);
                return objVectorConnection.ExecuteDataSet(cmd);
            }
        }

        public DataSet GetInvoiceData(string fromDate, string toDate, string Type = "")
        {
            objVectorConnection = GetVectorDBInstance();
            if (StringManager.IsEqual(Type, "Intacct"))
            {
                var cmd = objVectorConnection.GetStoredProcCommand("VectorManageIntacctInvoiceInfo", "InvoiceQueryForIntacct", fromDate, toDate, null, null);
                return objVectorConnection.ExecuteDataSet(cmd);
            }
            else
            {
                var cmd = objVectorConnection.GetStoredProcCommand("VectorManageIntacctInvoiceInfo", "InvoiceQuery", fromDate, toDate, null, null);
                return objVectorConnection.ExecuteDataSet(cmd);
            }

        }

        public void UpdateCustomerData(string customerKey, string qBID,string keyNo, string intacctId = null)
        {
            objVectorConnection = GetVectorDBInstance();
            if (!string.IsNullOrEmpty(intacctId))
            {
                var cmd = objVectorConnection.GetStoredProcCommand("VectorManageIntacctClientInfo", "UpdateCustomerQueryIntacct", null, null, null, customerKey, intacctId, keyNo);
                objVectorConnection.ExecuteNonQuery(cmd);
            }
            else
            {
                var cmd = objVectorConnection.GetStoredProcCommand("VectorManageIntacctClientInfo", "UpdateCustomerQuery", null, null, qBID, customerKey, null, keyNo);
                objVectorConnection.ExecuteNonQuery(cmd);
            }
        }

        public void UpdatePropertyData(string customerKey, string qBID, string keyNo, string intacctId = null)
        {
            objVectorConnection = GetVectorDBInstance();
            if (!string.IsNullOrEmpty(intacctId))
            {
                var cmd = objVectorConnection.GetStoredProcCommand("VectorManageIntacctPropertyInfo", "UpdatePropertyQueryIntacct", null, null, null, customerKey, intacctId, keyNo);
                objVectorConnection.ExecuteNonQuery(cmd);
            }
            else
            {
                var cmd = objVectorConnection.GetStoredProcCommand("VectorManageIntacctPropertyInfo", "UpdatePropertyQuery", null, null, qBID, customerKey, null, keyNo);
                objVectorConnection.ExecuteNonQuery(cmd);
            }
        }

        public void UpdateHaulerData(string customerKey, string qBID,string KeyNo)
        {
            objVectorConnection = GetVectorDBInstance();
            var cmd = objVectorConnection.GetStoredProcCommand("VectorManageIntacctClientInfo", "UpdateCustomerQuery", null, null, qBID, customerKey, null, KeyNo);
            objVectorConnection.ExecuteNonQuery(cmd);
        }

        public int UpdateIntacctData(string customerKey, string intacctId = null, string type = null)
        {
            objVectorConnection = GetVectorDBInstance();
            int IntacctIdResult = VectorConstants.Zero;
            DataSet ds = objVectorConnection.ExecuteDataSet(objVectorConnection.GetStoredProcCommand("VectorManageIntacctSync", type, customerKey, intacctId));

            if (!DataManager.IsNullOrEmptyDataSet(ds))
                return Convert.ToInt32(ds.Tables[0].Rows[0]["Result"]);
            
            return IntacctIdResult;
        }


        public void UpdateInvoiceData(string vectorInvoiceNo, string qBID, string type = "")
        {
            objVectorConnection = GetVectorDBInstance();
            if (StringManager.IsEqual(type, "Intacct"))
            {
                var cmd = objVectorConnection.GetStoredProcCommand("VectorManageIntacctInvoiceInfo", "UpdateInvoiceIntacctQuery", null, null, qBID, vectorInvoiceNo);
                objVectorConnection.ExecuteNonQuery(cmd);
            }
            else
            {
                var cmd = objVectorConnection.GetStoredProcCommand("VectorManageIntacctInvoiceInfo", "UpdateInvoiceQuery", null, null, qBID, vectorInvoiceNo);
                objVectorConnection.ExecuteNonQuery(cmd);
            }
        }

        public void UpdateInvoicesBillData(string invoiceNumber, string qBID, string type = "")
        {
            objVectorConnection = GetVectorDBInstance();
            var cmd = objVectorConnection.GetStoredProcCommand("VectorManageIntacctInvoiceInfo", "UpdateBillIntacctQuery", null, null, qBID, invoiceNumber);
            objVectorConnection.ExecuteNonQuery(cmd);
        }

        public void LogException(string customerKey, string intacct, string entityType, string errorDescription, string intacctSession,
                                 string systemIp, string errorType, string xml, string userId)
        {
            objVectorConnection = GetVectorDBInstance();
            var cmd = objVectorConnection.GetStoredProcCommand("VectorManageIntacctErrorLog", entityType, string.Empty, errorDescription,
                intacctSession, systemIp, errorType, userId, xml);
            objVectorConnection.ExecuteNonQuery(cmd);
        }
    }
}
