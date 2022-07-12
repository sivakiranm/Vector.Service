using System;
using System.Data;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Master.DataLayer;
using static Vector.Master.Entities.MasterData;

namespace Vector.Master.BusinessLayer
{
    public class MasterDataBL : DisposeLogic
    {
        private VectorDataConnection objVectorDB;
        public MasterDataBL(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }


        public VectorResponse<object> GetAutoCompleteExtenderInfo(VactorMasterDataSearch objVactorMasterDataSearch,Int64 userID)
        {
            using (var objMasterDataDL = new MasterDataDL(objVectorDB))
            {
                var result = objMasterDataDL.GetAutoCompleteExtenderInfo(objVactorMasterDataSearch, userID);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> GetVectorMasterData(VectorMasterData objVectorMasterData, Int64 userId)
        {
            using (var objMasterDataDL = new MasterDataDL(objVectorDB))
            {
                var result = objMasterDataDL.GetVectorMasterData(objVectorMasterData, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        } 

        public VectorResponse<object> GetInventoryData(VectorInventoryDataSearch objVectorInventoryDataSearch, Int64 userId)
        {
            using (var objMasterDataDL = new MasterDataDL(objVectorDB))
            {
                var result = objMasterDataDL.GetInventoryData(objVectorInventoryDataSearch, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }


        public VectorResponse<object> GetDocuments(DocumentInfo objDocumentInfo, Int64 userId)
        {
            using (var objMasterDataDL = new MasterDataDL(objVectorDB))
            {
                var result = objMasterDataDL.GetDocuments(objDocumentInfo, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }


        public DataSet GetDownloadInfo(string action,string searchText)
        {
            using (var objMasterDataDL = new MasterDataDL(objVectorDB))
            {
               return objMasterDataDL.GetDownloadInfo(action, searchText);
               
            }
        }

        public VectorResponse<object> ManageProcessUpdates(ProcessUpdateEntity objProcessUpdateEntity,Int64 userId)
        {
            using (var objMasterDataDL = new MasterDataDL(objVectorDB))
            {
                var result = objMasterDataDL.ManageProcessUpdates(objProcessUpdateEntity, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }


        public DataSet RunCalculationsServiceForInvoice(Int64 InvoiceID)
        {
            using (var objMasterDataDL = new MasterDataDL(objVectorDB))
            {
                return objMasterDataDL.RunCalculationsServiceForInvoice(InvoiceID);

            }
        }
    }
}
