using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using static Vector.Master.Entities.MasterData;

namespace Vector.Master.DataLayer
{
    public class MasterDataDL : DisposeLogic
    {
        private Database objVectorConnection;
        private VectorDataConnection objVectorDB;

        DataSet dsData = new DataSet();

        public MasterDataDL(VectorDataConnection objVectorDB)
        {
            this.objVectorDB = objVectorDB;
        }
            
        private Database GetVectorDBInstance()
        {
            return objVectorDB.GetVectorDBConnection();
        }

        public DataSet GetAutoCompleteExtenderInfo(VactorMasterDataSearch objVactorMasterDataSearch,Int64 userID)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetSearchCriteria",
                objVactorMasterDataSearch.Action,
                objVactorMasterDataSearch.Id,
                objVactorMasterDataSearch.SearchText,
                 userID);
        }


        public DataSet GetVectorMasterData(VectorMasterData objVectorMasterData, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.GetVactorMasterData.ToString(),
                objVectorMasterData.Action,
                userId,
                objVectorMasterData.Id,
                objVectorMasterData.Status,
                objVectorMasterData.commaSperatedIds
                );
        } 

        public DataSet GetInventoryData(VectorInventoryDataSearch objVectorInventoryDataSearch, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.GetVectorInventoryMasterData.ToString(),
                objVectorInventoryDataSearch.Action,
                userId,
                objVectorInventoryDataSearch.ActionId,
                objVectorInventoryDataSearch.CommaSperatedIds,
                objVectorInventoryDataSearch.Status
                );
        }

        public DataSet GetDocuments(DocumentInfo objDocumentInfo, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorDocuments.ToString(),
                objDocumentInfo.Action, 
                objDocumentInfo.Type,
                objDocumentInfo.Id,
                userId
                );
        }


        public DataSet GetDownloadInfo(string action, string searchText)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetToDownloadFromPR.ToString(),
               action,
               searchText
                );
        }

        public DataSet ManageProcessUpdates(ProcessUpdateEntity objProcessUpdateEntity, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageProcessUpdates.ToString(),
               objProcessUpdateEntity.Action,
               objProcessUpdateEntity.TicketId,
               objProcessUpdateEntity.TaskId,
               objProcessUpdateEntity.InvoiceId,
               userId
                );
        }


        public DataSet RunCalculationsServiceForInvoice(Int64 InvoiceId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorRunCalculationServiceForInvoice.ToString(),
                InvoiceId
                );
        }
    }
}
