using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Garage.Entities;

namespace Vector.Garage.DataLayer
{
    public class BaselineDL : DisposeLogic
    {
        private Database objVectorConnection;
        private VectorDataConnection objVectorDB;

        DataSet dsData = new DataSet();

        public BaselineDL(VectorDataConnection objVectorDB)
        {
            this.objVectorDB = objVectorDB;
        }

        private Database GetVectorDBInstance()
        {
            return objVectorDB.GetVectorDBConnection();
        }
        public DataSet VectorManageBaseLineSupportFilesDL(BaseLineSupportFiles objBaseLineSupportFiles, string supportFileDocs)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageBaselinSupportingFilesInfo.ToString(),
                                                objBaseLineSupportFiles.Action,
                                                  objBaseLineSupportFiles.BaseLineId,
                                                  objBaseLineSupportFiles.TaskId,
                                                  supportFileDocs,
                                                 objBaseLineSupportFiles.Comments,
                                                 objBaseLineSupportFiles.UserId,
                                                 objBaseLineSupportFiles.IsFromTask,
                                                 objBaseLineSupportFiles.SaveOrComplete
                                                 );

            return res;
        }



        public DataSet GetBaseLineSupportFiles(BaselineSupportingFilesInfoSearch objBaselineSupportingFilesInfoSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetBaselinSupportingFilesInfo.ToString(),
          objBaselineSupportingFilesInfoSearch.Action,
          objBaselineSupportingFilesInfoSearch.BaseLineId,
          objBaselineSupportingFilesInfoSearch.TaskId
                                        );

            return res;
        }
        public DataSet GetBaseLineInfoDL(BaselineInfoSearch objBaselineInfoSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetBaselineInfo.ToString(),
                                                objBaselineInfoSearch.Action,
                                                objBaselineInfoSearch.BaseLineId,
                                                  objBaselineInfoSearch.BaseLineInfoId,
                                                  objBaselineInfoSearch.TaskId);

            return res;
        }

        public DataSet GetBaseLineMainInfoDL(BaselineInfoSearch objBaselineInfoSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetBaselineMainInfo.ToString(),
                                                objBaselineInfoSearch.Action,
                                                objBaselineInfoSearch.BaseLineId,
                                                  objBaselineInfoSearch.BaseLineInfoId,
                                                  objBaselineInfoSearch.TaskId);

            return res;
        }

        public DataSet VectorManageBaseLineMainInfoDL(BaseLineMainInfo objBaseLineMainInfo, string baselineDocs)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageBaselineMainInfo.ToString(),
                                                objBaseLineMainInfo.Action,
                                               objBaseLineMainInfo.BaselineId,
                                                objBaseLineMainInfo.BaselineMainInfoId,
                                                objBaseLineMainInfo.TaskId,
                                                objBaseLineMainInfo.SalesPersons,
                                                objBaseLineMainInfo.Negotiator,
                                                objBaseLineMainInfo.RsSavingsSharePercentage,
                                                objBaseLineMainInfo.DiscountPercentage,
                                                objBaseLineMainInfo.AccountType,
                                                objBaseLineMainInfo.BillingCycle,
                                                objBaseLineMainInfo.VendorInvoiceDay,
                                                objBaseLineMainInfo.ModeOfReceipt,
                                                objBaseLineMainInfo.BilledWhen,
                                                baselineDocs,
                                                objBaseLineMainInfo.Comments,
                                                objBaseLineMainInfo.UserId,
                                                objBaseLineMainInfo.IsFromTask,
                                                objBaseLineMainInfo.ProcessStatus
                                                 );

            return res;
        }
        public DataSet VectorManageBaseLineInfoDL(BaseLineInfo objBaseLineInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageBaselineInfo.ToString(),
                                                objBaseLineInfo.Action,
                                                  objBaseLineInfo.BaseLineId,
                                                  objBaseLineInfo.BaseLineInfoId,
                                                   objBaseLineInfo.TaskId,
                                                      objBaseLineInfo.PropertyId,
                                                    objBaseLineInfo.VendorId,
                                                 objBaseLineInfo.Comments,
                                                 objBaseLineInfo.UserId,
                                                 objBaseLineInfo.IsFromTask,
                                                 objBaseLineInfo.SaveOrComplete
                                                 );

            return res;
        }

        public DataSet VectorGetMapCatalogLineItemInfoDL(MapCatalogSearch objMapCatalogSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetMapCatalogLineItemInfo.ToString(),
                                                objMapCatalogSearch.Action,
                                                objMapCatalogSearch.BaselineId,
                                                  objMapCatalogSearch.BaselineMapCatalogLineItemInfoId,
                                                  objMapCatalogSearch.TaskId,
                                                  objMapCatalogSearch.BaselineCatalogLineItemId,
                                                  objMapCatalogSearch.NegotiationLineItemsId
                                                  );

            return res;
        }
        public DataSet VectorManageMapCatalogLineItemInfoDL(MapCatalog objMapCatalog)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageMapCatalogLineItemInfo.ToString(),
                                                objMapCatalog.Action,
                                                  objMapCatalog.BaseLineId,
                                                  objMapCatalog.BaselineMapCatalogLineItemInfoId,
                                                   objMapCatalog.TaskId,
                                                      objMapCatalog.VendorContactId,
                                                    objMapCatalog.LineItemsXml,
                                                 objMapCatalog.Comments,
                                                 objMapCatalog.UserId,
                                                 objMapCatalog.IsFromTask,
                                                 objMapCatalog.SaveOrComplete,
                                                 objMapCatalog.IsAdditionalChargesAdded,
                                                 objMapCatalog.IsNegotiationRequired,
                                                 objMapCatalog.ContractIds
                                                 );

            return res;
        }

        public DataSet VectorGetApproveBaseLineInfoDL(ApproveBaseLineInfoSearch objApproveBaseLineInfoSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetApproveBaseLineInfo.ToString(),
                                                objApproveBaseLineInfoSearch.Action,
                                                objApproveBaseLineInfoSearch.BaselineId,
                                                  objApproveBaseLineInfoSearch.ApproveBaselineInfoId,
                                                  objApproveBaseLineInfoSearch.TaskId);

            return res;
        }
        public DataSet VectorManageApproveBaseLineInfoDL(ApproveBaseLineInfo objApproveBaseLineInfo, string approveBaseLineDocuments)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageApproveBaseLineInfo.ToString(),
                                                  objApproveBaseLineInfo.Action,
                                                  objApproveBaseLineInfo.BaseLineId,
                                                  objApproveBaseLineInfo.ApproveBaselineInfoId,
                                                  objApproveBaseLineInfo.TaskId,
                                                  objApproveBaseLineInfo.SalesPersons,
                                                  objApproveBaseLineInfo.Negotiator,
                                                  objApproveBaseLineInfo.RsSavingsSharePercent,
                                                  objApproveBaseLineInfo.DiscountPercentage,
                                                  objApproveBaseLineInfo.BaseLineAmount,
                                                  objApproveBaseLineInfo.AccountType,
                                                 objApproveBaseLineInfo.BillingCycle,
                                                 objApproveBaseLineInfo.VendorInvoiceDay,
                                                 objApproveBaseLineInfo.ModeOfReceipt,
                                                 objApproveBaseLineInfo.BilledWhen,
                                                 approveBaseLineDocuments,
                                                 objApproveBaseLineInfo.BaseLineItemsXml,
                                                 objApproveBaseLineInfo.BaseLineItemsXmlExtra,
                                                 objApproveBaseLineInfo.BaseLineItemsXmlArchived,
                                                 objApproveBaseLineInfo.Condition,
                                                  objApproveBaseLineInfo.ContractId,
                                                 objApproveBaseLineInfo.Comments,
                                                 objApproveBaseLineInfo.UserId,
                                                 objApproveBaseLineInfo.IsFromTask,
                                                 objApproveBaseLineInfo.SaveOrComplete


                                                 );

            return res;
        }


        public DataSet VectorSearchBaselines(SearchBaseline objSearchBaseline, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorSearchBaselines.ToString(),
                                                objSearchBaseline.Action,
                                                objSearchBaseline.ClientName,
                                                  objSearchBaseline.PropertyName,
                                                  objSearchBaseline.VendorName,
                                                  objSearchBaseline.VendorCorporateName,
                                                  objSearchBaseline.AccountNumber,
                                                  objSearchBaseline.ContractNo,
                                                  objSearchBaseline.BaselineStatus,
                                                  objSearchBaseline.PropertyCity,
                                                  objSearchBaseline.PropertyState,
                                                  objSearchBaseline.BaselineId,
                                                  userId);

            return res;
        }

        public DataSet VectorGetBaseLineInformation(string baseLineId, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorViewBaseLine.ToString(), "ViewBaseLine", baseLineId);

            return res;
        }

        public DataSet VectorAddBaseline(Baseline objBaseline, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageBaseline.ToString(),
                                                objBaseline.Action,
                                                objBaseline.BaseLineId,
                                                objBaseline.PropertyId,
                                                  objBaseline.VendorId,
                                                  objBaseline.SalesPersons,
                                                  objBaseline.salesPersonIds,
                                                  objBaseline.Negotiators,
                                                  objBaseline.NegotiatorIds,
                                                  objBaseline.BillingCycle,
                                                  objBaseline.ModeOfReciept,
                                                  objBaseline.AccountType,
                                                  objBaseline.BilledWhen,
                                                  objBaseline.VendorInvoiceDay,
                                                  objBaseline.VendorContactId,
                                                  objBaseline.LineitemXml,
                                                  objBaseline.LineitemXmlArchive,
                                                  objBaseline.LineitemXmlExtras,
                                                  objBaseline.DocumentXml,
                                                  objBaseline.Comments,
                                                  objBaseline.TaskId,
                                                  objBaseline.IsFromTask,
                                                  objBaseline.SaveOrComplete,
                                                  userId);

            return res;
        }

        public DataSet GetBaseLineDetails(BaselineInfoSearch objBaselineInfoSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetBaseline.ToString(),
                                                objBaselineInfoSearch.Action,
                                                objBaselineInfoSearch.BaseLineId,
                                                  objBaselineInfoSearch.BaseLineInfoId,
                                                  objBaselineInfoSearch.TaskId);

            return res;
        }

        public DataSet MangageLineItemState(LineitemInfo ObjbaselineLineitemInfo, Int64 userID)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageBaselineLineitemState.ToString(),
                                                ObjbaselineLineitemInfo.Action,
                                                ObjbaselineLineitemInfo.State,
                                                ObjbaselineLineitemInfo.EntityId,
                                                  ObjbaselineLineitemInfo.EntityLineitemId,
                                                  ObjbaselineLineitemInfo.TaskId,
                                                  ObjbaselineLineitemInfo.Comments,
                                                  userID);

            return res;
        }
        public DataSet GetNegotiations(string action, string propertyId, Int64 userID)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetNegotiaitons.ToString(),
                action,
                propertyId, userID);

            return res;
        }

    }
}
