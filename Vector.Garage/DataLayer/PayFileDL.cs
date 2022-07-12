using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Garage.Entities;

namespace Vector.Garage.DataLayer
{
    public class PayFileDL : DisposeLogic
    {
        private Database objVectorConnection;
        private VectorDataConnection objVectorDB;

        public PayFileDL(VectorDataConnection objVectorDB)
        {
            this.objVectorDB = objVectorDB;
        }

        private Database GetVectorDBInstance()
        {
            return objVectorDB.GetVectorDBConnection();
        }

        public DataSet GetClientPayConfigurationInfo(ClientPayAccountInfo objClientPayAccountInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetClientGetConfigurationInfo.ToString(),
                                                objClientPayAccountInfo.Action,
                                                objClientPayAccountInfo.ClientId,
                                                objClientPayAccountInfo.ClientPayAccountId,
                                                objClientPayAccountInfo.DisbursementAccountId,
                                                 objClientPayAccountInfo.Currency,
                                                objClientPayAccountInfo.TaskId,
                                                objClientPayAccountInfo.UserId);
            return res;
        }

        public DataSet ManageClientPayAccount(ClientPayAccountInfo objClientPayAccountInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageClientPayAccoutInfo.ToString(),
                                                objClientPayAccountInfo.Action,
                                                objClientPayAccountInfo.ClientId,
                                                objClientPayAccountInfo.ClientPayAccountId,
                                                objClientPayAccountInfo.AbaRoutingNumber,
                                                objClientPayAccountInfo.Currency,
                                                objClientPayAccountInfo.PayAccountNumber,
                                                objClientPayAccountInfo.PayAccountType,
                                                objClientPayAccountInfo.BankName,
                                                objClientPayAccountInfo.DisbursementAccountId,
                                                objClientPayAccountInfo.DisbursementAccountNumber,
                                                objClientPayAccountInfo.BankId,
                                                objClientPayAccountInfo.Address1,
                                                objClientPayAccountInfo.Address2,
                                                objClientPayAccountInfo.City,
                                                objClientPayAccountInfo.State,
                                                objClientPayAccountInfo.Zip,
                                                objClientPayAccountInfo.Country,
                                                objClientPayAccountInfo.PayAccountStatus,
                                                objClientPayAccountInfo.CheckDocNumber,
                                                objClientPayAccountInfo.CheckPrefix,
                                                objClientPayAccountInfo.LastSequenceNumber,
                                                objClientPayAccountInfo.NextSequenceNumber,
                                                objClientPayAccountInfo.MessageToPrint,
                                                objClientPayAccountInfo.TaskId,
                                                objClientPayAccountInfo.UserId,
                                                objClientPayAccountInfo.IsFromTask,
                                                objClientPayAccountInfo.SaveOrComplete
                                               );
            return res;
        }

        public DataSet GetConsolidatedInvoiceForPayFile(PayFileInfo objPayFileInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetConsolidatedInvoiceForPayFile.ToString(),
                                                objPayFileInfo.Action,
                                                objPayFileInfo.DisbursementAccountId,
                                                objPayFileInfo.Currency,
                                                objPayFileInfo.ConsolidatedInvoiceIds);
            return res;
        }

        public DataSet ManagePayFileGeneration(PayFileInfo objPayFileInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManagePayFileGeneration.ToString(),
                                                objPayFileInfo.Action,
                                                objPayFileInfo.DisbursementAccountId,
                                                objPayFileInfo.Currency,
                                                objPayFileInfo.ConsolidatedInvoiceDetailIds,
                                                objPayFileInfo.TaskId,
                                                objPayFileInfo.UserId,
                                                objPayFileInfo.IsFromTask,
                                                objPayFileInfo.SaveOrComplete);
            return res;
        }

        public DataSet ManageElectronicPayFileGeneration(PayFileInfo objPayFileInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.ManageElectronicPayFileGeneration.ToString(),
                                                objPayFileInfo.Action,
                                                objPayFileInfo.DisbursementAccountId,
                                                objPayFileInfo.Currency,
                                                objPayFileInfo.ConsolidatedInvoiceDetailIds,
                                                objPayFileInfo.TaskId,
                                                objPayFileInfo.UserId,
                                                objPayFileInfo.IsFromTask,
                                                objPayFileInfo.SaveOrComplete);
            return res;
        }

        public DataSet GetPayFiles(SearchPayFile objSearchPayFile)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetPayFiles.ToString(),
                                                objSearchPayFile.Action,
                                                objSearchPayFile.status,
                                                objSearchPayFile.PayFileId);
            return res;
        }
        public DataSet ManagePayFile(ManagePayFile objManagePayFile)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManagePayFile.ToString(),
                                                objManagePayFile.Action,
                                                objManagePayFile.PayFileId,
                                                objManagePayFile.Comments, 
                                                objManagePayFile.TaskId,
                                                objManagePayFile.UserId,
                                                objManagePayFile.IsFromTask,
                                                objManagePayFile.SaveOrComplete);
            return res;
        }

        public DataSet ManageEletronicTransaction(EletronicTransaction objEletronicTransaction)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageEletronicPayFile.ToString(),
                                                objEletronicTransaction.Action,
                                                objEletronicTransaction.PayFileDetailId,
                                                objEletronicTransaction.Comments, 
                                                objEletronicTransaction.Type,
                                                objEletronicTransaction.TaskId,
                                                objEletronicTransaction.UserId,
                                                objEletronicTransaction.IsFromTask,
                                                objEletronicTransaction.SaveOrComplete);
            return res;
        }

        public DataSet RejectPayFileTransactions(ManagePayFile objManagePayFile)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorRejectPayFileTransactions.ToString(),
                                                objManagePayFile.Action,
                                                objManagePayFile.PayFileId,
                                                objManagePayFile.Comments,
                                                objManagePayFile.PayFileDetailIds,
                                                objManagePayFile.TaskId,
                                                objManagePayFile.UserId,
                                                objManagePayFile.IsFromTask,
                                                objManagePayFile.SaveOrComplete);
            return res;
        }

        public DataSet GetPFDetailsForXML(Int64 PfIdfr)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetPayFileInfoforXML.ToString(),
                                                PfIdfr);
            return res; 
        }

        public DataSet UpdatePayFileStatus(Int64 PfIdfr)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetPayFileInfoforXML.ToString(),
                                                PfIdfr);
            return res;
        }

    }
}
