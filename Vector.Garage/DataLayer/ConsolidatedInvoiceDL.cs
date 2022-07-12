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
    public class ConsolidatedInvoiceDL : DisposeLogic
    {
        private Database objVectorConnection;
        private VectorDataConnection objVectorDB;

        DataSet dsData = new DataSet();

        public ConsolidatedInvoiceDL(VectorDataConnection objVectorDB)
        {
            this.objVectorDB = objVectorDB;
        }

        private Database GetVectorDBInstance()
        {
            return objVectorDB.GetVectorDBConnection();
        }


        public DataSet CreateConsolidatedInvoice(ConsolidatedInvoiceInfo objConsolidatedInvoice)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorCreateConsolidatedInvoiceInfo.ToString(),
                                                objConsolidatedInvoice.Action,
                                                objConsolidatedInvoice.ClientId,
                                                objConsolidatedInvoice.ConsolidatedDate,
                                                objConsolidatedInvoice.Currency,
                                                objConsolidatedInvoice.FundDate,
                                                objConsolidatedInvoice.ConsolidateBy,
                                                objConsolidatedInvoice.UserId,
                                                objConsolidatedInvoice.TaskId,
                                                  objConsolidatedInvoice.SaveOrComplete);
            return res;
        }
        public DataSet ConsolidatedInvoiceSearch(ConsolidatedInvoiceSearch objConsolidatedInvoice)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorSearchConsolidatedInvoice.ToString(),
                                                objConsolidatedInvoice.Action,
                                                objConsolidatedInvoice.clientName,
                                                objConsolidatedInvoice.propertyName,
                                                objConsolidatedInvoice.consolidatedInvoiceName,
                                                objConsolidatedInvoice.consolidatedInvoiceId,
                                                objConsolidatedInvoice.consolidatedInvoiceRage,
                                                objConsolidatedInvoice.status,
                                                objConsolidatedInvoice.vectorInvoiceNo,
                                                objConsolidatedInvoice.invoiceNumber,
                                                objConsolidatedInvoice.UserId);
            return res;
        }

        public DataSet ApproveFundCI(FundApproveCI objFundApproveCI)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageConsolidatedInvoice.ToString(),
                                                objFundApproveCI.Action,
                                                objFundApproveCI.ConsolidatedInvoiceId,
                                                objFundApproveCI.Comments,
                                                objFundApproveCI.TaskId,
                                                objFundApproveCI.UserId);
            return res;
        }

        public DataSet ReporcessInvoice(FundApproveCI objInvoiceInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageReprocessConsolidatedInvoice.ToString(),
                                                objInvoiceInfo.Action,
                                                objInvoiceInfo.InvoiceId,
                                                objInvoiceInfo.Comments,
                                                objInvoiceInfo.TaskId,
                                                objInvoiceInfo.UserId);
            return res;
        }
        public DataSet RejectCITransactions(RejectCITransactions objInvoiceInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageRejectConsolidatedInvoice.ToString(),
                                                objInvoiceInfo.Action,
                                                objInvoiceInfo.ConsolidatedInvoiceId,
                                                objInvoiceInfo.Transactions,
                                                objInvoiceInfo.TaskId,
                                                objInvoiceInfo.UserId,
                                                objInvoiceInfo.IsFromTask,
                                                objInvoiceInfo.SaveOrComplete);
            return res;
        }

        

        public DataSet PartiallyFundCITransactions(PartiallyFundCITransactions objInvoiceInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManagePartiallyFundConsolidatedInvoice.ToString(),
                                                objInvoiceInfo.Action,
                                                objInvoiceInfo.ConsolidatedInvoiceId,
                                                objInvoiceInfo.Transactions,
                                                objInvoiceInfo.TaskId,
                                                objInvoiceInfo.UserId,
                                                objInvoiceInfo.IsFromTask,
                                                objInvoiceInfo.SaveOrComplete,
                                                objInvoiceInfo.comments);
            return res;
        }


    }
}
