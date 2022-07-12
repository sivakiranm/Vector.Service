using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Garage.Entities;

namespace Vector.Garage.DataLayer
{
    public class InvoiceDL : DisposeLogic
    {
        private Database objVectorConnection;
        private VectorDataConnection objVectorDB;

        enum Action
        {
            GetVendorPastDueInfo,
            GetVendorPendingCreditsInfo
        }

        DataSet dsData = new DataSet();

        public InvoiceDL(VectorDataConnection objVectorDB)
        {
            this.objVectorDB = objVectorDB;
        }

        private Database GetVectorDBInstance()
        {
            return objVectorDB.GetVectorDBConnection();
        }


        public DataSet GetBatchInfo(BatchInfoSearch objBatchInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetBatchInfo.ToString(),
                                                objBatchInfo.Action,
                                                objBatchInfo.BatchId,
                                                objBatchInfo.TaskId
                                                );

            return res;
        }

        public DataSet ManageBatchInfo(BatchInfo objBatchInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageBatchInfo.ToString(),
                                                objBatchInfo.Action,
                                                objBatchInfo.BatchId,
                                                objBatchInfo.BatchType,
                                                objBatchInfo.BatchCategory,
                                                objBatchInfo.TaskId,
                                                objBatchInfo.UserId,
                                                objBatchInfo.Comments,
                                                objBatchInfo.SaveOrComplete
                                                );

            return res;
        }

        public DataSet MangeFinalizeBatchInfo(BatchInfo objBatchInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageFinalizeBatchInfo.ToString(),
                                                objBatchInfo.Action,
                                                objBatchInfo.BatchId,
                                                objBatchInfo.TaskId,
                                                objBatchInfo.UserId,
                                                objBatchInfo.Comments,
                                                objBatchInfo.SaveOrComplete
                                                );

            return res;
        }

        public DataSet GetInvoiceHeaderInfoSearch(InvoiceHeaderInfoSearch objInvoiceHeaderInfoSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetInvoiceHeaderInfoSearch.ToString(),
                                                objInvoiceHeaderInfoSearch.Action,
                                                objInvoiceHeaderInfoSearch.BatchId,
                                                  objInvoiceHeaderInfoSearch.BatchDate,
                                                    objInvoiceHeaderInfoSearch.ImageStatus,
                                                objInvoiceHeaderInfoSearch.TaskId
                                                );

            return res;
        }
        public DataSet MangeBatchUploadDocumentsInfo(BatchUploadDocumentsInfo objBatchUploadDocumentsInfo, string batchUploadDocuments)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageUploadInvoicesInfo.ToString(),
                                                objBatchUploadDocumentsInfo.Action,
                                                objBatchUploadDocumentsInfo.BatchId,
                                                batchUploadDocuments,
                                                objBatchUploadDocumentsInfo.UserId,
                                                objBatchUploadDocumentsInfo.TaskId,
                                                  objBatchUploadDocumentsInfo.IsFromTask,
                                                objBatchUploadDocumentsInfo.Comments,
                                                objBatchUploadDocumentsInfo.SaveOrComplete
                                                );

            return res;
        }


        public DataSet ManageInvoiceHeaderInfo(InvoiceHeaderInfo objInvoiceHeaderInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageInvoiceHeaderInfo.ToString(),
                                                  objInvoiceHeaderInfo.Action,
                                                  objInvoiceHeaderInfo.InvoiceId,
                                                  objInvoiceHeaderInfo.BatchdetailId,
                                                  objInvoiceHeaderInfo.AccountId,
                                                  objInvoiceHeaderInfo.PreviousBalance,
                                                  objInvoiceHeaderInfo.InvoiceNo,
                                                  objInvoiceHeaderInfo.PaymentsRecieved,
                                                  objInvoiceHeaderInfo.InvoiceStatus,
                                                  objInvoiceHeaderInfo.InvoiceStatusValue,
                                                  objInvoiceHeaderInfo.UnPaidBalance,
                                                  objInvoiceHeaderInfo.ServiceStartDate,
                                                  objInvoiceHeaderInfo.ServiceEndDate,
                                                  objInvoiceHeaderInfo.InvoiceDate,
                                                  objInvoiceHeaderInfo.DueDate,
                                                  objInvoiceHeaderInfo.CurrentCharges,
                                                  objInvoiceHeaderInfo.CurrentDue,
                                                  objInvoiceHeaderInfo.ThirtySixtyDaysDue,
                                                  objInvoiceHeaderInfo.SixtyNintyDaysDue,
                                                  objInvoiceHeaderInfo.NintyPlusDue,
                                                  objInvoiceHeaderInfo.BillAmount,
                                                  objInvoiceHeaderInfo.ErrorsCaughtAndCorrected,
                                                  objInvoiceHeaderInfo.isPassthrough,
                                                  objInvoiceHeaderInfo.isAmendmentInvoice,
                                                  objInvoiceHeaderInfo.TaskId,
                                                  objInvoiceHeaderInfo.Comments,
                                                  objInvoiceHeaderInfo.UserId,
                                                  objInvoiceHeaderInfo.IsFromTask,
                                                  objInvoiceHeaderInfo.SaveOrComplete
                                                );

            return res;
        }


        public DataSet VectorGetInvoicesForProcessing(InvoiceLineItemInfoSearch objInvoiceLineItemInfoSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = new DataSet();
            if (objInvoiceLineItemInfoSearch.Action != null && !string.IsNullOrEmpty(objInvoiceLineItemInfoSearch.Action) &&
                objInvoiceLineItemInfoSearch.Action.ToUpper().Equals("GETMONTHLYINVOICESFORINVOICEGENERATION"))
            {
                res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetMonthlyInvoicesForProcessing.ToString(),
                                                objInvoiceLineItemInfoSearch.Action,
                                                objInvoiceLineItemInfoSearch.BatchNo,
                                                objInvoiceLineItemInfoSearch.AccountNumber,
                                                 objInvoiceLineItemInfoSearch.ClientName,
                                                 objInvoiceLineItemInfoSearch.ProperyName,
                                                 objInvoiceLineItemInfoSearch.InvoiceNumber,
                                                 objInvoiceLineItemInfoSearch.ContractId,
                                                 objInvoiceLineItemInfoSearch.InvoiceDate,
                                                 objInvoiceLineItemInfoSearch.DueDate,
                                                 objInvoiceLineItemInfoSearch.InvoiceStatus,
                                                objInvoiceLineItemInfoSearch.TaskId
                                                );
            }
            else
            {
                res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetInvoicesForProcessing.ToString(),
                                                objInvoiceLineItemInfoSearch.Action,
                                                objInvoiceLineItemInfoSearch.BatchNo,
                                                objInvoiceLineItemInfoSearch.AccountNumber,
                                                 objInvoiceLineItemInfoSearch.ClientName,
                                                 objInvoiceLineItemInfoSearch.ProperyName,
                                                 objInvoiceLineItemInfoSearch.InvoiceNumber,
                                                 objInvoiceLineItemInfoSearch.ContractId,
                                                 objInvoiceLineItemInfoSearch.InvoiceDate,
                                                 objInvoiceLineItemInfoSearch.DueDate,
                                                 objInvoiceLineItemInfoSearch.InvoiceStatus,
                                                objInvoiceLineItemInfoSearch.TaskId
                                                );
            }
            return res;
        }

        public DataSet VectorGetInvoicesForAudit(InvoiceLineItemInfoSearch objInvoiceLineItemInfoSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = new DataSet();
            if (objInvoiceLineItemInfoSearch.Action != null && !string.IsNullOrEmpty(objInvoiceLineItemInfoSearch.Action) &&
                objInvoiceLineItemInfoSearch.Action.ToUpper().Equals("GETMONTHLYINVOICESFORINVOICEGENERATION"))
            {
                res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetMonthlyInvoicesForProcessing.ToString(),
                                                objInvoiceLineItemInfoSearch.Action,
                                                objInvoiceLineItemInfoSearch.BatchNo,
                                                objInvoiceLineItemInfoSearch.AccountNumber,
                                                 objInvoiceLineItemInfoSearch.ClientName,
                                                 objInvoiceLineItemInfoSearch.ProperyName,
                                                 objInvoiceLineItemInfoSearch.InvoiceNumber,
                                                 objInvoiceLineItemInfoSearch.ContractId,
                                                 objInvoiceLineItemInfoSearch.InvoiceDate,
                                                 objInvoiceLineItemInfoSearch.DueDate,
                                                 objInvoiceLineItemInfoSearch.InvoiceStatus,
                                                objInvoiceLineItemInfoSearch.TaskId
                                                );
            }
            else
            {
                res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetInvoicesForAudit.ToString(),
                                                objInvoiceLineItemInfoSearch.Action,
                                                objInvoiceLineItemInfoSearch.BatchNo,
                                                objInvoiceLineItemInfoSearch.AccountNumber,
                                                 objInvoiceLineItemInfoSearch.ClientName,
                                                 objInvoiceLineItemInfoSearch.ProperyName,
                                                 objInvoiceLineItemInfoSearch.InvoiceNumber,
                                                 objInvoiceLineItemInfoSearch.ContractId,
                                                 objInvoiceLineItemInfoSearch.InvoiceDate,
                                                 objInvoiceLineItemInfoSearch.DueDate,
                                                 objInvoiceLineItemInfoSearch.InvoiceStatus,
                                                objInvoiceLineItemInfoSearch.TaskId
                                                );
            }
            return res;
        }


        public DataSet GetInvoiceDetails(InvoiceLineItemInfoSearch objInvoiceLineItemInfoSearch,Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetInvoiceDetails.ToString(),
                                                objInvoiceLineItemInfoSearch.Action,
                                                objInvoiceLineItemInfoSearch.InvoiceId,
                                                objInvoiceLineItemInfoSearch.TaskId,
                                                userId,
                                                objInvoiceLineItemInfoSearch.BatchDetailsId
                                                );

            return res;
        }

        public DataSet ManageInvoiceLineItemsInfo(InvoiceHeaderInfo objInvoiceHeaderInfo,Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageInvoiceAddLineItemInfo.ToString(),
                                                  objInvoiceHeaderInfo.Action,
                                                  objInvoiceHeaderInfo.InvoiceId,
                                                  objInvoiceHeaderInfo.BatchdetailId,
                                                  objInvoiceHeaderInfo.AccountId,
                                                  objInvoiceHeaderInfo.PreviousBalance,
                                                  objInvoiceHeaderInfo.InvoiceNo,
                                                  objInvoiceHeaderInfo.PaymentsRecieved,
                                                  objInvoiceHeaderInfo.InvoiceStatus,
                                                  objInvoiceHeaderInfo.InvoiceStatusValue,
                                                  objInvoiceHeaderInfo.UnPaidBalance,
                                                  objInvoiceHeaderInfo.ServiceStartDate,
                                                  objInvoiceHeaderInfo.ServiceEndDate,
                                                  objInvoiceHeaderInfo.InvoiceDate,
                                                  objInvoiceHeaderInfo.DueDate,
                                                  objInvoiceHeaderInfo.CurrentCharges,  
                                                  objInvoiceHeaderInfo.CurrentDue,
                                                  objInvoiceHeaderInfo.ThirtySixtyDaysDue,
                                                  objInvoiceHeaderInfo.SixtyNintyDaysDue,
                                                  objInvoiceHeaderInfo.NintyPlusDue,
                                                  objInvoiceHeaderInfo.BillAmount,
                                                  objInvoiceHeaderInfo.ErrorsCaughtAndCorrected,
                                                  objInvoiceHeaderInfo.InvoiceLineitemInfo,
                                                  objInvoiceHeaderInfo.AdditionalLineItemInfo,
                                                  objInvoiceHeaderInfo.isPassthrough,
                                                  objInvoiceHeaderInfo.isAmendmentInvoice,
                                                  objInvoiceHeaderInfo.TaskId,
                                                  objInvoiceHeaderInfo.Comments,
                                                  objInvoiceHeaderInfo.UserId,
                                                  objInvoiceHeaderInfo.IsFromTask,
                                                  objInvoiceHeaderInfo.SaveOrComplete
                                                );

            return res;
        }

        public DataSet GetInvoicesForVerify(InvoiceLineItemInfoSearch objInvoiceLineItemInfoSearch) 
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetVerifyInfo.ToString(),
                                                objInvoiceLineItemInfoSearch.Action,
                                                objInvoiceLineItemInfoSearch.InvoiceId,
                                                objInvoiceLineItemInfoSearch.TaskId
                                                );

            return res;
        }

        public DataSet GenerateInvoice(InvoiceInfo objInvoiceInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGenerateInvoice.ToString(),
                                                objInvoiceInfo.Action,
                                                objInvoiceInfo.InvoiceId,
                                                  objInvoiceInfo.TaskId,
                                                  objInvoiceInfo.Comments,
                                                  objInvoiceInfo.UserId,
                                                  objInvoiceInfo.IsFromTask,
                                                  objInvoiceInfo.SaveOrComplete
                                                );

            return res;
        }

        public DataSet GetInvoiceDetailsForInvoiceGeneration(string action, Int64? invoiceId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = new DataSet();

            //if (!string.IsNullOrEmpty(action) && action.ToUpper().Equals("Monthly"))
            //{
            //    res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetMonthlyInvoiceDetailsForInvoiceGeneration.ToString(),
            //                                         action, invoiceId);
            //}
            //else
            //{

                res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetInvoiceDetailsForInvoiceGeneration.ToString(),
                                                      action, invoiceId);
           // }

            return res;
        }


        public DataSet GetExceptionInfo(SearchInfo objSearchInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetExceptionDetails.ToString(),
                                                  objSearchInfo.Action,
                                                    objSearchInfo.ExceptionId,
                                                  objSearchInfo.TaskId
                                                );

            return res;
        }

        public DataSet ManageExceptionInfo(InvoiceHeaderInfo objInvoiceHeaderInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageExceptionInfo.ToString(),
                                                  objInvoiceHeaderInfo.Action,
                                                  objInvoiceHeaderInfo.ExceptionId,
                                                  objInvoiceHeaderInfo.ExceptionStatusId,
                                                  objInvoiceHeaderInfo.ExceptionStatusDescId,
                                                  objInvoiceHeaderInfo.TaskId,
                                                  objInvoiceHeaderInfo.Comments,
                                                  objInvoiceHeaderInfo.UserId,
                                                  objInvoiceHeaderInfo.IsFromTask,
                                                  objInvoiceHeaderInfo.SaveOrComplete
                                                );

            return res;
        }

        public DataSet VectorGetInvoiceLookup(SearchEntity objInvoiceEntity,Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetInvoiceLookUp.ToString(),
                                                  objInvoiceEntity.action,
                                                  objInvoiceEntity.batchId,
                                                  objInvoiceEntity.InvoiceStatus,
                                                  objInvoiceEntity.clientId,
                                                  objInvoiceEntity.propertyId,
                                                  objInvoiceEntity.vendorId,
                                                  objInvoiceEntity.InvoiceId,
                                                  objInvoiceEntity.accountId,
                                                  objInvoiceEntity.contractId,
                                                  objInvoiceEntity.invoiceFromDate,
                                                  objInvoiceEntity.invoiceToDate,
                                                  objInvoiceEntity.auditInvoiceFromDate,
                                                  objInvoiceEntity.auditInvoiceToDate,
                                                  userId
                                                );

            return res;
        }

        public DataSet GetInvoiceForDispatch(SearchEntity objSearchInvoice,Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetDispatchInvoice.ToString(),
                                                  objSearchInvoice.action,
                                                  objSearchInvoice.clientId,
                                                  objSearchInvoice.propertyId,
                                                  objSearchInvoice.vendorId,
                                                  objSearchInvoice.InvoiceId,
                                                  objSearchInvoice.accountId,
                                                  objSearchInvoice.batchId,
                                                  objSearchInvoice.DispatchStatus,
                                                  objSearchInvoice.auditInvoiceFromDate,
                                                  objSearchInvoice.auditInvoiceToDate,
                                                  userId
                                                );

            return res;
        }


        public DataSet GetInvoiceVerification(SearchEntity objSearchInvoice, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetInvoiceVerification.ToString(),
                                                  objSearchInvoice.action,
                                                  objSearchInvoice.clientId,
                                                  objSearchInvoice.propertyId,
                                                  objSearchInvoice.vendorId,
                                                  objSearchInvoice.InvoiceId,
                                                  objSearchInvoice.accountId,
                                                  objSearchInvoice.auditInvoiceFromDate,
                                                  objSearchInvoice.auditInvoiceToDate,
                                                  userId
                                                );

            return res;
        }

        public DataSet GetInvoiceVerificationDetails(SearchEntity objSearchInvoice, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetInvoiceVerificationDetails.ToString(),
                                                  objSearchInvoice.action,
                                                  objSearchInvoice.obfId,
                                                  objSearchInvoice.clientId,
                                                  objSearchInvoice.propertyId,
                                                  objSearchInvoice.vendorCorporateId,
                                                   objSearchInvoice.vendorId,
                                                   objSearchInvoice.city,
                                                   objSearchInvoice.state,
                                                   objSearchInvoice.zip,
                                                   objSearchInvoice.auditInvoiceFromDate,
                                                   objSearchInvoice.auditInvoiceToDate,
                                                   objSearchInvoice.vendorInvoiceFromDate,
                                                   objSearchInvoice.vendorInvoiceToDate,
                                                    objSearchInvoice.accountId,
                                                  objSearchInvoice.InvoiceId,
                                                 objSearchInvoice.vectorInvoiceNumber,
                                                  objSearchInvoice.accountExecutiveId,
                                                  objSearchInvoice.contractId,
                                                   objSearchInvoice.salesPerson,
                                                  objSearchInvoice.negotiator,
                                                  userId
                                                );

            return res;
        }

        public DataSet ManageInvoiceVerificationDetails(InvoiceTransactions objSearchInvoice, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.ManageInvoiceVerificationDetails.ToString(),
                                                  objSearchInvoice.action,
                                                  objSearchInvoice.transactions,
                                                  userId
                                                );

            return res;
        }

        public DataSet InvoiceInfoForDispatch(InvoiceInfo objInvoiceInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetInvoiceDetailsForDispatch.ToString(),
                                                     objInvoiceInfo.Action,
                                                     objInvoiceInfo.InvoiceId,
                                                     objInvoiceInfo.TaskId,
                                                     objInvoiceInfo.UserId
                                                );

            return res;
        }

        public DataSet ManageDispatchInvoice(DispatchInvoice objDispatchInvoice)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageDispatchInvoice.ToString(),
                                                      objDispatchInvoice.Action,
                                                      objDispatchInvoice.InvoiceId,
                                                      objDispatchInvoice.toEmails,
                                                      objDispatchInvoice.ccEmails,
                                                      objDispatchInvoice.bccEmails,
                                                      objDispatchInvoice.bodyHtml,
                                                      objDispatchInvoice.isSummaryInvoice,
                                                      objDispatchInvoice.isVectorInvoice,
                                                      objDispatchInvoice.isVendorInvoice,
                                                      objDispatchInvoice.subject,
                                                      objDispatchInvoice.taskid,
                                                      objDispatchInvoice.UserId,
                                                      objDispatchInvoice.SaveOrComplete);

            return res;
        }

        internal DataSet GetVendorPastDueInfo(Int64 invoiceId, Int64 vendorPastDueInfoId, Int64 taskId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetVendorPastDueInfo.ToString(),
                                                      Action.GetVendorPastDueInfo.ToString(),
                                                      invoiceId,
                                                      vendorPastDueInfoId,
                                                      taskId);
            return res;
        }

        internal DataSet ManageVendorPastDueInfo(VendorPastDue objVendorPastDue)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageVendorPastDueInfo.ToString(),
                                                      objVendorPastDue.Action,
                                                      objVendorPastDue.InvoiceId,
                                                      objVendorPastDue.VendorPastDueInfoId,
                                                      objVendorPastDue.Taskid,
                                                      objVendorPastDue.PendingDueDays,
                                                      objVendorPastDue.PendingCredits,
                                                      objVendorPastDue.CauseOfPastDue,
                                                      objVendorPastDue.Comments,
                                                      objVendorPastDue.UserId,
                                                      objVendorPastDue.SaveOrComplete);
            return res;
        }

        internal DataSet GetVendorPendingCreditsInfo(Int64 invoiceId, Int64 vendorPendingCreditsInfoId, Int64 taskId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetVendorPendingCreditsInfo.ToString(),
                                                      Action.GetVendorPendingCreditsInfo.ToString(),
                                                      invoiceId,
                                                      vendorPendingCreditsInfoId,
                                                      taskId);
            return res;
        }

        internal DataSet ManageVendorPendingCreditsInfo(VendorPendingCredits objVendorPendingCredits)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageVendorPendingCreditsInfo.ToString(),
                                                      objVendorPendingCredits.Action,
                                                      objVendorPendingCredits.InvoiceId,
                                                      objVendorPendingCredits.VendorPendingCreditsInfoId,
                                                      objVendorPendingCredits.Taskid,
                                                      objVendorPendingCredits.CreditsReceived,
                                                      objVendorPendingCredits.Comments,
                                                      objVendorPendingCredits.UserId,
                                                      objVendorPendingCredits.SaveOrComplete);
            return res;
        }

        internal DataSet ManageVendorEmailDataInfo(VendorEmailData objVendorEmailData)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageVendorEmailDataInfo.ToString(),
                                                      objVendorEmailData.Action,
                                                      objVendorEmailData.InvoiceId,
                                                      objVendorEmailData.Documents.ListToDataSet().GetXml().ToString(),
                                                      objVendorEmailData.Taskid,
                                                      objVendorEmailData.EmailSentTo,
                                                      objVendorEmailData.UserId);
            return res;
        }


        internal DataSet AddLineitemToInvoice(LineItems objLineItems,Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorAddLineitemToInvoiceFromAccount.ToString(),
                                                      objLineItems.Action,
                                                      objLineItems.InvoiceId,
                                                      objLineItems.accountLineItemsXml,
                                                       userId);
            return res;
        }

        internal DataSet ManageBillGapComments(BillGapComments objBillGapComments, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageBillGapReport.ToString(),
                                                      objBillGapComments.Action,
                                                      objBillGapComments.AccountId,
                                                      objBillGapComments.Period,
                                                      objBillGapComments.Comments,
                                                       userId);
            return res;
        }

        internal DataSet GetInvoiceNotificaitonsInfo(string action ,InvoiceHeaderInfo objInvoiceHeaderInfo,Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.GetInvoiceNotificaitonsInfo.ToString(),
                                                     action,
                                                     objInvoiceHeaderInfo.InvoiceId,
                                                     userId);
            return res;
        }


        internal DataSet UploadMissingInvoice(MissingInvoice objMissingInvoice,Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageMissingInvoice.ToString(),
                                                     objMissingInvoice.Action,
                                                     objMissingInvoice.AccountId,
                                                     objMissingInvoice.Documents[0].fileName,
                                                     objMissingInvoice.InvoiceDate,
                                                     objMissingInvoice.LastInvoiceDate,
                                                     objMissingInvoice.Comments,
                                                     objMissingInvoice.IRUniqueCode,
                                                     objMissingInvoice.ContractId,
                                                     userId);
            return res;
        }


        internal DataSet GeneratePlaceHolderInvoice(Placeholder objMissingInvoice, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManagePlaceholderInvoice.ToString(),
                                                     objMissingInvoice.Action,
                                                     objMissingInvoice.AccountId, 
                                                     objMissingInvoice.InvoiceDate,
                                                     objMissingInvoice.LastInvoiceDate,
                                                     objMissingInvoice.Comments,
                                                     objMissingInvoice.ServiceStartDate,
                                                     objMissingInvoice.ServiceEndDate,
                                                     objMissingInvoice.InvoiceNumber,
                                                     objMissingInvoice.StagingInvoiceId,
                                                     objMissingInvoice.IRUniqueCode,
                                                     userId);
            return res;
        }



        internal DataSet DeleteInvoiceLineitem(InvoiceLineitemInfo objMissingInvoice, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageInvoicelineitem.ToString(),
                                                     objMissingInvoice.Action,
                                                     objMissingInvoice.InvoiceId,
                                                     objMissingInvoice.InvoiceLineitemId,
                                                     userId);
            return res;
        }

        internal DataSet GetInvoiceReceiptData(SearchEntity objSearchEntity, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();

            if (objSearchEntity.assignedTo != null)
                objSearchEntity.assignedTo = objSearchEntity.assignedTo.TrimEnd().TrimStart();

            if (objSearchEntity.action.Equals("AnalyticsByClient") || objSearchEntity.action.Equals("AnalyticsByUser") || objSearchEntity.action.Equals("AnalyticsByCategory") || objSearchEntity.action.Equals("AnalyticsByVendorCorporate"))
            {
                DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetInvoiceReceiptDataAnalytics.ToString(),
                                                         objSearchEntity.action,
                                                         objSearchEntity.reportBy,
                                                         objSearchEntity.clientId,
                                                         objSearchEntity.clientName,
                                                         objSearchEntity.propertyId,
                                                         objSearchEntity.propertyName,
                                                         objSearchEntity.vendorId,
                                                         objSearchEntity.vendorName,
                                                         objSearchEntity.accountId,
                                                         objSearchEntity.accountNumber,
                                                         objSearchEntity.accountMode,
                                                         objSearchEntity.accountType,
                                                         objSearchEntity.accountStatus,
                                                         objSearchEntity.negotiationId,
                                                         objSearchEntity.negotiationStatus,
                                                         objSearchEntity.contractId,
                                                         objSearchEntity.monthName,
                                                         objSearchEntity.assignedTo,
                                                         objSearchEntity.colorCode, 
                                                         userId);
                return res;
            }
            else
            {
                DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetInvoiceReceiptData.ToString(),
                                                         objSearchEntity.action,
                                                         objSearchEntity.reportBy,
                                                         objSearchEntity.clientId,
                                                         objSearchEntity.clientName,
                                                         objSearchEntity.propertyId,
                                                         objSearchEntity.propertyName,
                                                         objSearchEntity.vendorId,
                                                         objSearchEntity.vendorName,
                                                         objSearchEntity.accountId,
                                                         objSearchEntity.accountNumber,
                                                         objSearchEntity.accountMode,
                                                         objSearchEntity.accountType,
                                                         objSearchEntity.accountStatus,
                                                         objSearchEntity.negotiationId,
                                                         objSearchEntity.negotiationStatus,
                                                         objSearchEntity.contractId,
                                                         objSearchEntity.monthName,
                                                         objSearchEntity.assignedTo,
                                                         objSearchEntity.colorCode,
                                                         objSearchEntity.vendorCoporateName,
                                                         objSearchEntity.serviceCategory,
                                                         userId);
                return res;
            }
            return null;
        }


        internal DataSet UploadIRPDocuments(IRPDocuments objDocuments, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageIRPDocuments.ToString(),
                                                     objDocuments.Action,
                                                     objDocuments.Documents[0].fileName, 
                                                     objDocuments.Comments,
                                                     objDocuments.IRUniqueCode,
                                                     userId);
            return res;
        }

        internal DataSet GetIRPDocuments(string action, string UniqueId, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetIRPDocuments.ToString(),
                                                     action,
                                                     UniqueId,
                                                     userId);
            return res;
        }


        //internal DataSet TriggerShortPayNotification(Int64 userId)
        //{
        //    objVectorConnection = GetVectorDBInstance();
        //    DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetInvoicesForShortPayNotification.ToString());
        //    return res;
        //}

    }
}
    