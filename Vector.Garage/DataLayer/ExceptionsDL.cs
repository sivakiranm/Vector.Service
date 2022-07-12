using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Garage.Entities;


namespace Vector.Garage.DataLayer
{
    public class ExceptionsDL : DisposeLogic
    {
        private Database objVectorConnection;
        private VectorDataConnection objVectorDB;

        public ExceptionsDL(VectorDataConnection objVectorDB)
        {
            this.objVectorDB = objVectorDB;
        }

        private Database GetVectorDBInstance()
        {
            return objVectorDB.GetVectorDBConnection();
        }

        public DataSet GetExceptions(ExceptionsSearch objExceptionsSearch)
        
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetExceptions.ToString(),
                                                                objExceptionsSearch.BatchId,
                                                                objExceptionsSearch.ExpType,
                                                                objExceptionsSearch.ExpPriority,
                                                                objExceptionsSearch.ExpStatus,
                                                                objExceptionsSearch.ClientID,
                                                                objExceptionsSearch.PropertyId,
                                                                objExceptionsSearch.VendorId,
                                                                objExceptionsSearch.InvoiceId,
                                                                objExceptionsSearch.AccountId,
                                                                objExceptionsSearch.AssigneTo,
                                                                objExceptionsSearch.RaisedFromDate,
                                                                objExceptionsSearch.RaisedToDate,
                                                                objExceptionsSearch.ClosedFromDate,
                                                                objExceptionsSearch.ClosedToDate,
                                                                objExceptionsSearch.IsIncludeDuplicateBill,
                                                                objExceptionsSearch.InvoiceDateFrom,
                                                                objExceptionsSearch.InvoiceDateTo,
                                                                objExceptionsSearch.BillingAnalyst,
                                                                objExceptionsSearch.UnassignedExceptions,
                                                                objExceptionsSearch.AccountExecutive,
                                                                objExceptionsSearch.ExceptionDescription,
                                                                objExceptionsSearch.Aging,
                                                                objExceptionsSearch.ExceptionStatusName);

            return res;
        }

        public Tuple<int, string, string> CreateException(CreateException objCreateException)
        {
            objVectorConnection = GetVectorDBInstance();
            var cmdExp = objVectorConnection.GetStoredProcCommand(VectorEnums.StoredProcedures.VectorManageEnteredException.ToString(),
                                                                objCreateException.BatchDetailId,
                                                                objCreateException.InvoiceId,
                                                                objCreateException.InvoiceNbr,
                                                                objCreateException.InvoiceDate,
                                                                objCreateException.DocId,
                                                                objCreateException.AccountId,
                                                                objCreateException.AcctNbr,
                                                                objCreateException.PropertyId,
                                                                objCreateException.ProPertyName,
                                                                objCreateException.VendorId,
                                                                objCreateException.VendorName,
                                                                objCreateException.ClientId,
                                                                objCreateException.ClientName,
                                                                objCreateException.ExceptionType,
                                                                objCreateException.ExceptionDesc,
                                                                objCreateException.ExceptionCategory,
                                                                objCreateException.BillingAnalyst,
                                                                objCreateException.Comments,
                                                                objCreateException.ImpComment,
                                                                objCreateException.DueDate,
                                                                objCreateException.LoginId,
                                                                objCreateException.Result,
                                                                objCreateException.ReOpenResult,
                                                                objCreateException.ExceptionId);
            objVectorConnection.ExecuteNonQuery(cmdExp);

            int result = Convert.ToInt32(objVectorConnection.GetParameterValue(cmdExp, "@RESULT"));
            if (result == -2)
                return new Tuple<int, string, string>(result, Convert.ToString(objVectorConnection.GetParameterValue(cmdExp, "@ReOpenResult")), Convert.ToString(objVectorConnection.GetParameterValue(cmdExp, "@ExceptionId")));

            return new Tuple<int, string, string>(result, string.Empty, Convert.ToString(objVectorConnection.GetParameterValue(cmdExp, "@ExceptionId")));
        }

        public DataSet RaiseException(CreateException objCreateException)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorCreateException.ToString(),
                                                                objCreateException.BatchDetailId,
                                                                objCreateException.InvoiceId,
                                                                objCreateException.InvoiceNbr,
                                                                objCreateException.InvoiceDate,
                                                                objCreateException.DocId,
                                                                objCreateException.AccountId,
                                                                objCreateException.AcctNbr,
                                                                objCreateException.PropertyId,
                                                                objCreateException.ProPertyName,
                                                                objCreateException.VendorId,
                                                                objCreateException.VendorName,
                                                                objCreateException.ClientId,
                                                                objCreateException.ClientName,
                                                                objCreateException.ExceptionType,
                                                                objCreateException.ExceptionDesc,
                                                                objCreateException.ExceptionCategory,
                                                                objCreateException.BillingAnalyst,
                                                                objCreateException.Comments,
                                                                objCreateException.ImpComment,
                                                                objCreateException.DueDate,
                                                                objCreateException.LoginId,
                                                                objCreateException.TaskId,
                                                                objCreateException.IsFromTask,
                                                                objCreateException.SaveOrComplete);

            return res;  
        }

        public DataSet CreateExceptionTicket(ExceptionTicketEntity objCreateException,Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorCreateTicketForException.ToString(),
                                                                objCreateException.action, 
                                                                objCreateException.exceptionId,
                                                                objCreateException.batchDetailid,
                                                                objCreateException.invoiceId,
                                                                objCreateException.ticketId,
                                                                objCreateException.taskid,
                                                                objCreateException.manifestId,
                                                                objCreateException.type,
                                                                objCreateException.clientId,
                                                                objCreateException.propertyId,
                                                                objCreateException.accountId,
                                                                objCreateException.vendorId,
                                                                objCreateException.subject,
                                                                objCreateException.description, 
                                                                objCreateException.comments,
                                                                userId
                                                                );

            return res;
        }

        public DataSet GetExceptionAnalyticsData(string fromDate,string toDate,Int64 UserId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetExceptionAnalyticsData.ToString(),
                                                                fromDate,
                                                                toDate,
                                                                UserId);

            return res;
        }

        public DataSet GetExceptionHistory(Int64 exceptionId, Int64 UserId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetExceptionHistoryData.ToString(),
                                                                exceptionId,  
                                                                UserId);

            return res;
        }
    }
}
