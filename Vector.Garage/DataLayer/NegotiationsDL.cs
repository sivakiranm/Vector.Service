using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Garage.Entities;
using System;
using Vector.Common.Entities;

namespace Vector.Garage.DataLayer
{
    public class NegotiationsDL : DisposeLogic
    {
        private Database objVectorConnection;
        private VectorDataConnection objVectorDB;

        public NegotiationsDL(VectorDataConnection objVectorDB)
        {
            this.objVectorDB = objVectorDB;
        }

        private Database GetVectorDBInstance()
        {
            return objVectorDB.GetVectorDBConnection();
        }

        public DataSet VectorManageNegotiationsDL(Negotiations objNegotiations)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageNegotiationsInfo.ToString(),
                            objNegotiations.Action,
                            objNegotiations.NegotiationId,
                            objNegotiations.NegotiationInfoId,
                            objNegotiations.TaskId,
                            objNegotiations.NegotiationNo,
                            objNegotiations.NegotiationType,
                            objNegotiations.ClientId,
                            objNegotiations.Property,
                            objNegotiations.Vendor,
                            objNegotiations.BaseLineItems,
                            objNegotiations.Condition,
                            objNegotiations.Comments,
                            objNegotiations.UserId,
                            objNegotiations.IsFromTask,
                            objNegotiations.SaveOrComplete
                                                 );

            return res;
        }

        public DataSet GetNegotiationsInfoDL(NegotiationSearch objNegotiationSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetNegotiationsInfo.ToString(),
                                                objNegotiationSearch.Action,
                                                objNegotiationSearch.NegotiationId,
                                                  objNegotiationSearch.NegotiationIdInfoId,
                                                  objNegotiationSearch.PropertyId,
                                                  objNegotiationSearch.TaskId);

            return res;
        }

        public DataSet GetNegotiations(NegotiationSearch objNegotiationSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetSearchNegotiationsData.ToString(),
                                                            objNegotiationSearch.Client,
                                                            objNegotiationSearch.PropertyName,
                                                            objNegotiationSearch.HaulerName,
                                                            objNegotiationSearch.AccountManager,
                                                            objNegotiationSearch.Address,
                                                            objNegotiationSearch.City,
                                                            objNegotiationSearch.State,
                                                            objNegotiationSearch.ZipCode,
                                                            objNegotiationSearch.CreatedDate,
                                                            objNegotiationSearch.EffectiveDate,
                                                            objNegotiationSearch.EndDate,
                                                            objNegotiationSearch.Negotiator,
                                                            objNegotiationSearch.NegotiationStatus,
                                                            objNegotiationSearch.PropertyMode,
                                                            objNegotiationSearch.NegotiationNo,
                                                            objNegotiationSearch.RemainderDate);

            return res;
        }

        public DataSet VectorGetNegotiationsBidSheetInfoDL(NegotiationsBidSheetInfo objNegotiationsBidSheetInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetNegotiationsBidSheetInfo.ToString(),
                                                            objNegotiationsBidSheetInfo.Action,
                                                            objNegotiationsBidSheetInfo.NegotiationId,
                                                            objNegotiationsBidSheetInfo.NegotiationBidSheetInfoId,
                                                            objNegotiationsBidSheetInfo.PropertyId,
                                                            objNegotiationsBidSheetInfo.TaskId);

            return res;
        }

        public DataSet GetDraftNegotiationsDL(NegotiationSearch objNegotiationSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetDraftNegotiationsInfo.ToString(),
                                                objNegotiationSearch.Action,
                                                objNegotiationSearch.NegotiationId,
                                                  objNegotiationSearch.NegotiationIdInfoId, //Draft NegotiationId
                                                  objNegotiationSearch.PropertyId,
                                                  objNegotiationSearch.TaskId);

            return res;
        }

        public DataSet GetNegotiationBaselineDetails(NegotiationSearch objNegotiationSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetNegotiationDetails.ToString(),
                                                            objNegotiationSearch.Action,
                                                            objNegotiationSearch.NegotiationId,
                                                            objNegotiationSearch.PropertyId,
                                                            objNegotiationSearch.VendorId,
                                                            objNegotiationSearch.UserId,
                                                            objNegotiationSearch.NegotiationLineItemsId);

            return res;
        }

        public DataSet VectorAddUpdateNegotiationBidValues(NegotiationsBidValues objNegotiationsBidValues)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorAddUpdateNegotiationBidValues.ToString(),
                                                            objNegotiationsBidValues.NegotiationId,
                                                            objNegotiationsBidValues.VendorId,
                                                            objNegotiationsBidValues.BidValuesXml,
                                                            objNegotiationsBidValues.UserId,
                                                            objNegotiationsBidValues.Comments);

            return res;
        }

        public DataSet VectorManageDraftNegotiationsDL(DraftNegotiations objDraftNegotiations)
        {
            string vendorXML = objDraftNegotiations.RequestbidVendorDetails.ListToDataSet().GetXml().ToString();

            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageDraftNegotiationsInfo.ToString(),
                 objDraftNegotiations.Action,
                                                                     objDraftNegotiations.NegotiationId,
                                                                     objDraftNegotiations.DraftNegotiationInfoId,
                                                                     objDraftNegotiations.TaskId,
                                                                     objDraftNegotiations.ClientId,
                                                                     objDraftNegotiations.NegotiationNo,
                                                                     objDraftNegotiations.NegotiationType,
                                                                     objDraftNegotiations.NegotiationStatus,
                                                                     objDraftNegotiations.Property,
                                                                     vendorXML,
                                                                     objDraftNegotiations.BaseLineItems,
                                                                     objDraftNegotiations.ApprovedAnnualIncrease,
                                                                     objDraftNegotiations.ManagementFee,
                                                                     objDraftNegotiations.ExistingContractExpiryDate,
                                                                     objDraftNegotiations.Negotiator,
                                                                     objDraftNegotiations.RevisitReminderDate,
                                                                     objDraftNegotiations.IsSavings,
                                                                     objDraftNegotiations.VendorPaymentTerm,
                                                                     objDraftNegotiations.Comments,
                                                                     objDraftNegotiations.UserId,
                                                                     objDraftNegotiations.IsFromTask,
                                                                     objDraftNegotiations.SaveOrComplete
                );
            return res;
        }
        public DataSet VectorManageNegotiationBidSheetInfoDL(NegotiationBidSheetInfo objNegotiationBidSheetInfo)
        {
            objVectorConnection = GetVectorDBInstance();
             
          
                
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageNegotiationBidSheetInfo.ToString(),
                                                                     objNegotiationBidSheetInfo.Action,
                                                                     objNegotiationBidSheetInfo.NegotiationId,
                                                                     objNegotiationBidSheetInfo.NegotiationBidSheetInfoId,
                                                                     objNegotiationBidSheetInfo.TaskId,
                                                                     objNegotiationBidSheetInfo.NegotiationStatus,
                                                                     objNegotiationBidSheetInfo.NegotiationLineItems,
                                                                     objNegotiationBidSheetInfo.NegotiationLineExtra,
                                                                     objNegotiationBidSheetInfo.NegotiationLineArchived,
                                                                     objNegotiationBidSheetInfo.ApprovedAnnualIncrease,
                                                                     objNegotiationBidSheetInfo.ManagementFee,
                                                                     objNegotiationBidSheetInfo.DiscountPercentage,
                                                                     objNegotiationBidSheetInfo.SavingSharePercentage,
                                                                     objNegotiationBidSheetInfo.ExistingContractExpiryDate,
                                                                     objNegotiationBidSheetInfo.ContractBeginDate ,
                                                                     objNegotiationBidSheetInfo.ContractEndDate,
                                                                     objNegotiationBidSheetInfo.NegotiatorId,
                                                                     objNegotiationBidSheetInfo.MonthTerm,
                                                                     objNegotiationBidSheetInfo.ChangesNeedClientApproval,
                                                                     objNegotiationBidSheetInfo.ClientApprovedChanges,
                                                                     objNegotiationBidSheetInfo.Reasons,
                                                                     objNegotiationBidSheetInfo.RevisitReminderDate,
                                                                     objNegotiationBidSheetInfo.nonRenewalRemainderDate,
                                                                     objNegotiationBidSheetInfo.IsSavings,
                                                                     objNegotiationBidSheetInfo.VendorPaymentTerm, 
                                                                     objNegotiationBidSheetInfo.Comments,
                                                                     objNegotiationBidSheetInfo.UserId,
                                                                     objNegotiationBidSheetInfo.IsFromTask,
                                                                     objNegotiationBidSheetInfo.SaveOrComplete
                );
            return res;
        }
        public DataSet VectorAddUpdateBaselineNegotiationLineItemsDL(BaselineNegotiationLineItems objBaselineNegotiationLineItems)
        {
            objVectorConnection = GetVectorDBInstance();

            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorAddUpdateBaselineNegotiationLineItems.ToString(),
                                                                     objBaselineNegotiationLineItems.NegotiationId,
                                                                     objBaselineNegotiationLineItems.BaselineCatalogLineItemId,
                                                                     objBaselineNegotiationLineItems.LineItemsXml,
                                                                     objBaselineNegotiationLineItems.Comments,
                                                                     objBaselineNegotiationLineItems.UserId,
                                                                     objBaselineNegotiationLineItems.NegotiationLineItemId
                );
            return res;
        }

        public DataSet UpdateNegotiationLineItem(NegotiationLineItemData objNegotiationLineItem)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorAddUpdateNegotiationLineItems.ToString(),
                                                            objNegotiationLineItem.NegotiationId,
                                                            objNegotiationLineItem.NegotiationLineItemsId,
                                                            objNegotiationLineItem.VendorId,
                                                            objNegotiationLineItem.PropertyId,
                                                            objNegotiationLineItem.BidValue,
                                                            objNegotiationLineItem.ServiceQuantity,
                                                            objNegotiationLineItem.ServiceFrequency,
                                                            objNegotiationLineItem.NoOfTimes,
                                                            objNegotiationLineItem.Total,
                                                            objNegotiationLineItem.PerUnitCost,
                                                            objNegotiationLineItem.Savings,
                                                            objNegotiationLineItem.UserId,
                                                            objNegotiationLineItem.Comments);
            return res;
        }

        public DataSet GetRequestBidEmails(string userId, string negotiationId, string vendorId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGenerateVendorBidLink.ToString(),
                                                            negotiationId,
                                                            vendorId,
                                                            userId);
            return res;
        }


        public DataSet VectorManagePreBidRequest(DraftNegotiations objDraftNegotiations)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManagePreBidRequest.ToString(),
                 objDraftNegotiations.Action,
                                                                     objDraftNegotiations.NegotiationId,
                                                                     objDraftNegotiations.DraftNegotiationInfoId,
                                                                     objDraftNegotiations.TaskId,
                                                                     objDraftNegotiations.NegotiationStatus,
                                                                     objDraftNegotiations.ApprovedAnnualIncrease,
                                                                     objDraftNegotiations.ManagementFee,
                                                                     objDraftNegotiations.ExistingContractExpiryDate,
                                                                     objDraftNegotiations.Negotiator,
                                                                     objDraftNegotiations.RevisitReminderDate,
                                                                     objDraftNegotiations.nonRenewalRemainderDate,
                                                                     objDraftNegotiations.ProcessInvoicePreriorToNegotiation,
                                                                     objDraftNegotiations.BillProcessing,
                                                                     objDraftNegotiations.ProcessingStartDate,
                                                                     objDraftNegotiations.DocumentXml,
                                                                     objDraftNegotiations.Reason,
                                                                     objDraftNegotiations.createDraftNegotaition,
                                                                     objDraftNegotiations.IsSavings,
                                                                     objDraftNegotiations.VendorPaymentTerm,
                                                                     objDraftNegotiations.Comments,
                                                                     objDraftNegotiations.UserId,
                                                                     objDraftNegotiations.IsFromTask,
                                                                     objDraftNegotiations.SaveOrComplete
                );
            return res;
        }

        internal void UpdateNegotiationEmailUniqueId(string action, string negotiationId, DataSet emailDataSet)
        {
            objVectorConnection = GetVectorDBInstance();
            objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorAddUpdateNegotiationEmailUniqueId.ToString(),
                                                            action, negotiationId,
                                                            emailDataSet.GetXml());
        }

        public DataSet GetNegotiatonRequestBidMails(string negotiationId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetNegotiationDetails.ToString(),
                                                            VectorEnums.StoredProcedures.GetNegotiationEmailDetails.ToString(),
                                                            negotiationId, "", "", 0, 0);

            return res;
        }

        public DataSet GetNegotiationRevisitEmail()
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetNegotiationDetails.ToString(),
                                                            VectorEnums.StoredProcedures.GetNegotiationRevisitEmailDetails.ToString(), 0, "", "", 0, 0);

            return res;
        }

        public DataSet ManageVectorServiceDetails(string procName)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(procName,"");

            return res;
        }

        public DataSet GetNegotiationBaselineLineitems(string type,Int64 negotiationId, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetNegotationBaselineLineitemInfo.ToString(),
                                                            type,
                                                            negotiationId,
                                                            userId);
            return res;
        }





        public DataSet GetNegotiationVendorEmailDetails(SendEmailNegotiation objSendEmailNegotiation,string action,Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.GetVectorVendorBidRequestEmailInfo.ToString(), action, 
                objSendEmailNegotiation.vendorId,
                objSendEmailNegotiation.negotiationId,
                userId);
        }


        public DataSet ManageNegotiationDocuments(NegotiationDocuments onjNegotiationDocuments, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageNegotiationDocuments.ToString(),
                            onjNegotiationDocuments.Action,
                            onjNegotiationDocuments.NegotiationId,
                            onjNegotiationDocuments.documentXml,
                            onjNegotiationDocuments.taskId,  
                            userId
                                                 );

            return res;
        }


        public DataSet GetNegotiationDocuments(Int64 negotiationId, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetNegotiationDocuments.ToString(),
                             negotiationId,
                            userId);

            return res;
        }


        public DataSet UpdateNegotiationLineitem(NegotiationLineitemUpdate ObjNegotiationLineitem, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageNegotiationLineitem.ToString(),
                             ObjNegotiationLineitem.negotiationId,
                             ObjNegotiationLineitem.negotiaitonLineitemId,
                             ObjNegotiationLineitem.lineItemsXml,
                             ObjNegotiationLineitem.comments,
                            userId);

            return res;
        }
        public DataSet GetNegotiation360ReportStatusInfo(NegotiationSearch objNegotiationSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetNegotiaitonWidget.ToString(),
                                                            objNegotiationSearch.Action,
                                                            objNegotiationSearch.ReportBy,
                                                            objNegotiationSearch.NegotiationStatus,
                                                            objNegotiationSearch.NegotiationId,
                                                            objNegotiationSearch.UserId,
                                                            objNegotiationSearch.ClientId,
                                                            objNegotiationSearch.PropertyId,
                                                            objNegotiationSearch.Address,
                                                            objNegotiationSearch.State,
                                                            objNegotiationSearch.City,
                                                            objNegotiationSearch.AccountExecutiveId,
                                                            objNegotiationSearch.Negotiator,
                                                            objNegotiationSearch.NegotiationBeginDate,
                                                            objNegotiationSearch.NegotiationEndDate,
                                                            objNegotiationSearch.ContarctEffectiveFromDate,
                                                            objNegotiationSearch.ContarctEffectiveToDate,
                                                            objNegotiationSearch.ContarctEndFromDate,
                                                            objNegotiationSearch.ContarctEndToDate

                                                           );

            return res;
        }

        public DataSet ManageBidEmailService(BidServiceInfo objBidServiceInfo, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageBidEmailServices.ToString(),
                             objBidServiceInfo.Action,
                             objBidServiceInfo.NegotiationId,
                             objBidServiceInfo.VendorId,
                             objBidServiceInfo.Lineitems,
                            userId);

            return res;
        }


        public DataSet ManageNegotiaionLineitemState(string action, Int64 baselineLineitemId, Int64 negotiaitonId,Int64 negotiationLineitemId,  Int64 accountId, Int64 accountlineitemid, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.ManageNegotiaionLineitemState.ToString(),
                             action,
                             baselineLineitemId,
                             negotiaitonId,
                             negotiationLineitemId,
                             accountId,
                             accountlineitemid,
                            userId);

            return res;
        }

        public DataSet GetNegotiationVendorLineitems(string action, Int64 negotiaitonId, Int64 vendorId, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetNegotiationVendorLineitems.ToString(),
                             action, 
                             negotiaitonId,
                             vendorId,
                            userId);

            return res;
        }



        public DataSet GetLowestBidInfo(string action, Int64 negotiationLineitemId, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectoGetLowestBidInfo.ToString(),
                             action,
                             negotiationLineitemId, 
                            userId);

            return res;
        }

        public DataSet GetAwardedVendors(string action, Int64 Id, Int64 userId, Int16 fromRange, Int16 toRange)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetAwardedVendors.ToString(),
                             action,
                             Id,
                            userId,
                            fromRange,
                            toRange);

            return res;
        }

        public DataSet NegotiationCloneLineItems(CloneLineitems objCloneLineitems, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorNegotiationCloneLineitems.ToString(),
                             objCloneLineitems.Action,
                             objCloneLineitems.VendorId,
                             objCloneLineitems.NegotiationId,
                            userId);

            return res;
        }

    }
}
