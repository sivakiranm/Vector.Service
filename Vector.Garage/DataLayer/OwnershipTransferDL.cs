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
    public class OwnershipTransferDL : DisposeLogic
    {
        private Database objVectorConnection;
        private VectorDataConnection objVectorDB;

        DataSet dsData = new DataSet();

        public OwnershipTransferDL(VectorDataConnection objVectorDB)
        {
            this.objVectorDB = objVectorDB;
        }

        private Database GetVectorDBInstance()
        {
            return objVectorDB.GetVectorDBConnection();
        }

        public DataSet VectorManageInitiateOwnerShipTransferInfo(OwnerShipTransferEntity objInitiateOwnershipTransfer,Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorOwnerShipTransfer.ToString(),
                                                  "",
                                                  objInitiateOwnershipTransfer.FromClientId,
                                                  objInitiateOwnershipTransfer.PropertyId,
                                                  objInitiateOwnershipTransfer.ToClientId,
                                                  objInitiateOwnershipTransfer.Address1,
                                                  objInitiateOwnershipTransfer.Address2,
                                                  objInitiateOwnershipTransfer.City,
                                                  objInitiateOwnershipTransfer.State,
                                                  objInitiateOwnershipTransfer.Zip,
                                                  objInitiateOwnershipTransfer.Email,
                                                  objInitiateOwnershipTransfer.Phone,
                                                  objInitiateOwnershipTransfer.Mobile,
                                                  objInitiateOwnershipTransfer.BillingAddress1,
                                                  objInitiateOwnershipTransfer.BillingAddress2,
                                                  objInitiateOwnershipTransfer.BillingCity,
                                                  objInitiateOwnershipTransfer.BillingState,
                                                  objInitiateOwnershipTransfer.BillingZip,
                                                  objInitiateOwnershipTransfer.BillingEmail,
                                                  objInitiateOwnershipTransfer.BillingPhone,
                                                  objInitiateOwnershipTransfer.BillingMobile,
                                                  objInitiateOwnershipTransfer.PropertyTransferDate,
                                                  objInitiateOwnershipTransfer.Comments,
                                                   userId,
                                                  objInitiateOwnershipTransfer.TaskId,
                                                  objInitiateOwnershipTransfer.IsFromTask,
                                                  objInitiateOwnershipTransfer.SaveOrComplete
                                                );
            return res;
        }

        public DataSet GetOwnershipTransfers(string Action)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.GetOwnershipTransfers.ToString(),
                                               Action
                                                );
            return res;
        }

        public DataSet VectorManageOwnershipTransferClientApproval(OTClientApprovalRequest objOTClientApprovalRequest)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageOwnershipTransferClientApproval.ToString(),
                                                 objOTClientApprovalRequest.Action,
                                                objOTClientApprovalRequest.OwnerShipTransferId,
                                                objOTClientApprovalRequest.NewClientId,
                                                objOTClientApprovalRequest.NewClientName,
                                                objOTClientApprovalRequest.ContactPerson,
                                                objOTClientApprovalRequest.ContactPhone,
                                                objOTClientApprovalRequest.ContactEmail,
                                                objOTClientApprovalRequest.Comments,
                                                objOTClientApprovalRequest.TaskId,
                                                objOTClientApprovalRequest.UserId,
                                                objOTClientApprovalRequest.IsFromTask,
                                                objOTClientApprovalRequest.SaveOrComplete
                                                );
            return res;
        }

        public DataSet VectorManageOTApproveClientApproval(OTClientApprovalRequest objOTClientApprovalRequest)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageOwnershipTransferApprove.ToString(),
                                                 objOTClientApprovalRequest.Action,
                                                objOTClientApprovalRequest.OwnerShipTransferId,
                                                objOTClientApprovalRequest.NewClientId,
                                                objOTClientApprovalRequest.NewClientName,
                                                objOTClientApprovalRequest.ContactPerson,
                                                objOTClientApprovalRequest.ContactPhone,
                                                objOTClientApprovalRequest.ContactEmail,
                                                objOTClientApprovalRequest.ClientApproval,
                                                objOTClientApprovalRequest.Comments,
                                                objOTClientApprovalRequest.TaskId,
                                                objOTClientApprovalRequest.UserId,
                                                objOTClientApprovalRequest.IsFromTask,
                                                objOTClientApprovalRequest.SaveOrComplete
                                                );
            return res;
        }

        public DataSet VectorManageOwnershipTransferLogEmail(OwnershipTransferLogEmail objOwnershipTransferLogEmail)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageInitiateOwnerShipTransgerInfo.ToString(),
                                                  objOwnershipTransferLogEmail.Action,
                                                  objOwnershipTransferLogEmail.OwnerShipTransferId,
                                                  objOwnershipTransferLogEmail.FromEmail,
                                                  objOwnershipTransferLogEmail.ToEmail,
                                                  objOwnershipTransferLogEmail.CCEmail,
                                                  objOwnershipTransferLogEmail.BCCEmail,
                                                  objOwnershipTransferLogEmail.Body,
                                                  objOwnershipTransferLogEmail.BodyHtml,
                                                  objOwnershipTransferLogEmail.EmlFilePath,
                                                  objOwnershipTransferLogEmail.EmlFileName,
                                                  objOwnershipTransferLogEmail.EmlFileUniqueId,
                                                  objOwnershipTransferLogEmail.Comments,
                                                  objOwnershipTransferLogEmail.UserId,
                                                  objOwnershipTransferLogEmail.IsFromTask,
                                                  objOwnershipTransferLogEmail.SaveOrComplete
                                                );
            return res;
        }
    }
}
