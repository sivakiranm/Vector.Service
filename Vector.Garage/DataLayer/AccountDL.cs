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
    public class AccountDL : DisposeLogic
    {
        private Database objVectorConnection;
        private VectorDataConnection objVectorDB;

        DataSet dsData = new DataSet();

        public AccountDL(VectorDataConnection objVectorDB)
        {
            this.objVectorDB = objVectorDB;
        }

        private Database GetVectorDBInstance()
        {
            return objVectorDB.GetVectorDBConnection();
        }


        public DataSet ManageAccountInfo(AccountInfo objAccountInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageAccountInfo.ToString(),
                                                objAccountInfo.Action,
                                                  objAccountInfo.AccountId,
                                                objAccountInfo.AccountInfoId,
                                                 objAccountInfo.TaskId,
                                                 objAccountInfo.PropertyId,
                                                 objAccountInfo.ContractId,
                                                 objAccountInfo.VendorId,
                                                 objAccountInfo.AccountMode,
                                                 objAccountInfo.AccountNumber,
                                                 objAccountInfo.ContractVersion,
                                                 objAccountInfo.AccountStatus,
                                                 objAccountInfo.PaymentTerm,
                                                 objAccountInfo.AccountInActivationDate,
                                                 objAccountInfo.AccountType,
                                                 objAccountInfo.BillingCycle,
                                                 objAccountInfo.HaulerPaymentTerm,
                                                 objAccountInfo.VectorPaymentTerm,
                                                 objAccountInfo.VendorOriginalInvoice,
                                                 objAccountInfo.VendorInvoiceId,
                                                 objAccountInfo.UniqueVendorCode,
                                                 objAccountInfo.RegisteredOnline,
                                                 objAccountInfo.VendorInvoiceDay
                                                 ,
                                                 objAccountInfo.ModeOfReceipt,
                                                 objAccountInfo.BilledWhen,
                                                 objAccountInfo.ManagementFee,
                                                 objAccountInfo.Savings,
                                                 objAccountInfo.IsSeasonal,
                                                 objAccountInfo.SeasonalFrom,
                                                 objAccountInfo.SeasonalTo,
                                                 objAccountInfo.LineItemsXml,
                                                 objAccountInfo.LineItemsXmlExtras,
                                                 objAccountInfo.LineItemsXmlArchived,
                                                 objAccountInfo.taskCloseType,
                                                 objAccountInfo.Comments,
                                                 objAccountInfo.UserId,
                                                 objAccountInfo.IsFromTask,
                                                 objAccountInfo.SaveOrComplete);

            return res;
        }



        public DataSet GetAccountInfo(AccountInfoSearch objAccountInfoSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetAccountInfo.ToString(),
                                                objAccountInfoSearch.Action,
                                                  objAccountInfoSearch.AccountId,
                                                objAccountInfoSearch.AccountInfoId,
                                                objAccountInfoSearch.TaskId);

            return res;
        }


        public DataSet ManageAccountRemitInfo(AccountRemitInfo objAccountInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageAccountRemitInfo.ToString(),
                                                objAccountInfo.Action,
                                                  objAccountInfo.AccountId,
                                                objAccountInfo.AccountRemitInfoId,
                                                 objAccountInfo.TaskId,
                                                 objAccountInfo.PaymentType,
                                                 objAccountInfo.VendorPaymentDetailId,
                                                 objAccountInfo.VendorPaymentACHDetailId,
                                                 objAccountInfo.VendorPaymentCheckDetailId,
                                                 objAccountInfo.VendorPaymentPCardDetailId,
                                                 objAccountInfo.Currency,
                                                 objAccountInfo.Comments,
                                                 objAccountInfo.UserId,
                                                 objAccountInfo.IsFromTask,
                                                 objAccountInfo.SaveOrComplete);

            return res;
        }



        public DataSet GetAccountRemitInfo(AccountRemitInfoSearch objAccountInfoSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetAccountRemitInfo.ToString(),
                                                objAccountInfoSearch.Action,
                                                  objAccountInfoSearch.AccountId,
                                                objAccountInfoSearch.AccountRemitInfoId,
                                                objAccountInfoSearch.TaskId);

            return res;
        }


        public DataSet VectorSearchAccounts(SearchEntity objAccountInfoSearch, Int64 userId)
        {
            if (objAccountInfoSearch != null && objAccountInfoSearch.action != null && string.Equals(objAccountInfoSearch.action.ToUpper(), "EXCEPTIONTICKETACCOUNTS"))
            {
                objVectorConnection = GetVectorDBInstance();
                DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorSearchAccountsExceptions.ToString(),
                                                    objAccountInfoSearch.action,
                                                      objAccountInfoSearch.accountId,
                                                    objAccountInfoSearch.accountStatus,
                                                    objAccountInfoSearch.clientId,
                                                    objAccountInfoSearch.propertyId,                                                    
                                                    objAccountInfoSearch.vendorId,
                                                    objAccountInfoSearch.vendorCorporateId,
                                                    objAccountInfoSearch.contractId,
                                                    objAccountInfoSearch.accountExecutiveId,
                                                    userId);

                return res;
            }
            else
            {
                objVectorConnection = GetVectorDBInstance();
                DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorSearchAccounts.ToString(),
                                                    objAccountInfoSearch.action,
                                                      objAccountInfoSearch.accountId,
                                                    objAccountInfoSearch.accountStatus,
                                                    objAccountInfoSearch.clientId,
                                                    objAccountInfoSearch.propertyId,
                                                    objAccountInfoSearch.propertyAddress,
                                                    objAccountInfoSearch.vendorId,
                                                    objAccountInfoSearch.vendorCorporateId,
                                                    objAccountInfoSearch.contractId,
                                                    objAccountInfoSearch.accountExecutiveId,
                                                    userId);

                return res;
            }
        }


        public DataSet GetAccountLineitemInfo(AccountLineItemInfo objAccountInfoSearch, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetAccountLineitemInfo.ToString(),
                                                objAccountInfoSearch.action,
                                                objAccountInfoSearch.accountId,
                                                objAccountInfoSearch.accountLineItemId,
                                                userId);

            return res;
        }


        public DataSet ManageAccountLineItem(UpdateAccountLineitemInfo objLineitemInfo, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageAccountLineItemInfo.ToString(),
                                                objLineitemInfo.action,
                                                objLineitemInfo.accountId,
                                                objLineitemInfo.accountLineitemId,
                                                objLineitemInfo.serviceCatalogueId,
                                                objLineitemInfo.baselineLineitem.baselineCatalogLineItemId,
                                                objLineitemInfo.baselineLineitem.serviceCatalogueId,
                                                objLineitemInfo.baselineLineitem.vendorDescription,
                                                objLineitemInfo.baselineLineitem.serviceBehaviour,
                                                objLineitemInfo.baselineLineitem.serviceQuantity,
                                                objLineitemInfo.baselineLineitem.serviceQuantityType,
                                                objLineitemInfo.baselineLineitem.minimumUnits,
                                                objLineitemInfo.baselineLineitem.maximumUnits,
                                                objLineitemInfo.baselineLineitem.perYardRate,
                                                objLineitemInfo.baselineLineitem.totalYardRate,
                                                objLineitemInfo.baselineLineitem.isSurchargable,
                                                objLineitemInfo.baselineLineitem.isTaxable,
                                                objLineitemInfo.baselineLineitem.serviceFrequency,
                                                objLineitemInfo.baselineLineitem.noOfTimes,
                                                objLineitemInfo.baselineLineitem.servicedOn,
                                                objLineitemInfo.baselineLineitem.perUnitCost,
                                                objLineitemInfo.baselineLineitem.totalCost,
                                                objLineitemInfo.baselineLineitem.monthlyRecurringCharge,
                                                objLineitemInfo.baselineLineitem.sixMonthsAverage,
                                                objLineitemInfo.baselineLineitem.size,
                                                objLineitemInfo.baselineLineitem.lineItemType,
                                                objLineitemInfo.baselineLineitem.capPercent,
                                                objLineitemInfo.baselineLineitem.offeredCapPercent,
                                                objLineitemInfo.contractLineitem.contractLineitemId,
                                                objLineitemInfo.contractLineitem.serviceCatalogueId,
                                                objLineitemInfo.contractLineitem.vendorDescription,
                                                objLineitemInfo.contractLineitem.serviceBehaviour,
                                                objLineitemInfo.contractLineitem.serviceQuantity,
                                                objLineitemInfo.contractLineitem.serviceQuantityType,
                                                objLineitemInfo.contractLineitem.minimumUnits,
                                                objLineitemInfo.contractLineitem.maximumUnits,
                                                objLineitemInfo.contractLineitem.perYardRate,
                                                objLineitemInfo.contractLineitem.totalYardRate,
                                                objLineitemInfo.contractLineitem.isSurchargable,
                                                objLineitemInfo.contractLineitem.isTaxable,
                                                objLineitemInfo.contractLineitem.serviceFrequency,
                                                objLineitemInfo.contractLineitem.noOfTimes,
                                                objLineitemInfo.contractLineitem.servicedOn,
                                                objLineitemInfo.contractLineitem.perUnitCost,
                                                objLineitemInfo.contractLineitem.totalCost,
                                                objLineitemInfo.contractLineitem.monthlyRecurringCharge,
                                                objLineitemInfo.contractLineitem.sixMonthsAverage,
                                                objLineitemInfo.contractLineitem.size,
                                                objLineitemInfo.contractLineitem.lineItemType,
                                                objLineitemInfo.contractLineitem.capPercent,
                                                objLineitemInfo.contractLineitem.offeredCapPercent,
                                                objLineitemInfo.comments,
                                                userId,
                                                1,
                                                "Complete");

            return res;
        }

        public DataSet GetAccountLineitems(string action, Int64 accountid, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetAccountLineitems.ToString(),
                                                action,
                                                accountid,
                                                userId);

            return res;
        }

        public DataSet GetAccountComments(AccountComments accountComment, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetAccountComments.ToString(),
                                                accountComment.accountId,
                                                accountComment.CommentsDate,
                                                userId);

            return res;
        }


        public DataSet ArchiveAccountVerificationRecord(ArchiveAccountVericationRecord objArchiveAccountVericationRecord, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorArchiveAccountVerificationRecord.ToString(),
                                                objArchiveAccountVericationRecord.accountVerificationId,
                                                objArchiveAccountVericationRecord.Comments,
                                                userId);

            return res;
        }


        public DataSet GetAccountListReport(SearchEntity objSearchEntity, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetAccountListReport.ToString(),
                                                objSearchEntity.action,
                                                objSearchEntity.clientName,
                                                objSearchEntity.clientId,
                                                objSearchEntity.propertyName,
                                                objSearchEntity.propertyId,
                                                objSearchEntity.vendorName,
                                                objSearchEntity.vendorId,
                                                objSearchEntity.vendorCoporateName,
                                                objSearchEntity.vendorCorporateId,
                                                objSearchEntity.accountNumber,
                                                objSearchEntity.accountId,
                                                objSearchEntity.contractId,
                                                objSearchEntity.baselineNo,
                                                objSearchEntity.negotiationId,
                                                objSearchEntity.salesPerson,
                                                objSearchEntity.accountExecutiveId,
                                                objSearchEntity.accountMode,
                                                objSearchEntity.accountType,
                                                objSearchEntity.accountStatus,
                                                objSearchEntity.registeredOnline,
                                                objSearchEntity.propertyAddress,
                                                objSearchEntity.city,
                                                objSearchEntity.state,
                                                objSearchEntity.zip,
                                                objSearchEntity.fromDate,
                                                objSearchEntity.toDate,
                                                userId);

            return res;
        }


        public DataSet GetMasterDataTrueUpPanelData(SearchEntity objAccountInfoSearch, Int64 userId)
        { 
                objVectorConnection = GetVectorDBInstance();
                DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetMasterDataTrueUpPanelData.ToString(),
                                                    objAccountInfoSearch.action,
                                                    objAccountInfoSearch.clientId,
                                                    objAccountInfoSearch.clientName,
                                                    objAccountInfoSearch.propertyId,
                                                    objAccountInfoSearch.propertyName,
                                                    objAccountInfoSearch.accountId,
                                                    objAccountInfoSearch.accountNumber,
                                                    objAccountInfoSearch.negotiationId,
                                                    objAccountInfoSearch.negotiationStatus,
                                                    objAccountInfoSearch.accountStatus,
                                                    objAccountInfoSearch.accountType,
                                                    objAccountInfoSearch.contractId,
                                                    objAccountInfoSearch.vendorId,
                                                    objAccountInfoSearch.vendorName,
                                                    objAccountInfoSearch.accountMode,
                                                    objAccountInfoSearch.IsVerified,
                                                    userId);

                return res; 
        }

        public DataSet ManageMasterTrueUpDataLineitem(UpdateAccountLineitemInfo objLineitemInfo, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();

             
                DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageMasteTrueupDataLineitem.ToString(),
                                                    objLineitemInfo.action,
                                                    objLineitemInfo.ContractId,
                                                    objLineitemInfo.equipmentTypeName,
                                                    objLineitemInfo.equipmentSize,
                                                    objLineitemInfo.uom,
                                                    objLineitemInfo.serviceCategory,
                                                    objLineitemInfo.wasteType,
                                                    objLineitemInfo.serviceCatalogueId,
                                                    objLineitemInfo.baselineLineitem.baselineCatalogLineItemId,
                                                    objLineitemInfo.baselineLineitem.serviceCatalogueId,
                                                    objLineitemInfo.baselineLineitem.vendorDescription,
                                                    objLineitemInfo.baselineLineitem.serviceBehaviour,
                                                    objLineitemInfo.baselineLineitem.serviceQuantity,
                                                    objLineitemInfo.baselineLineitem.serviceQuantityType,
                                                    objLineitemInfo.baselineLineitem.minimumUnits,
                                                    objLineitemInfo.baselineLineitem.maximumUnits,
                                                    objLineitemInfo.baselineLineitem.perYardRate,
                                                    objLineitemInfo.baselineLineitem.totalYardRate,
                                                    objLineitemInfo.baselineLineitem.isSurchargable,
                                                    objLineitemInfo.baselineLineitem.isTaxable,
                                                    objLineitemInfo.baselineLineitem.serviceFrequency,
                                                    objLineitemInfo.baselineLineitem.noOfTimes,
                                                    objLineitemInfo.baselineLineitem.servicedOn,
                                                    objLineitemInfo.baselineLineitem.perUnitCost,
                                                    objLineitemInfo.baselineLineitem.totalCost,
                                                    objLineitemInfo.baselineLineitem.monthlyRecurringCharge,
                                                    objLineitemInfo.baselineLineitem.sixMonthsAverage,
                                                    objLineitemInfo.baselineLineitem.size,
                                                    objLineitemInfo.baselineLineitem.lineItemType,
                                                    objLineitemInfo.baselineLineitem.capPercent,
                                                    objLineitemInfo.baselineLineitem.offeredCapPercent,
                                                    objLineitemInfo.contractLineitem.contractLineitemId,
                                                    objLineitemInfo.contractLineitem.serviceCatalogueId,
                                                    objLineitemInfo.contractLineitem.vendorDescription,
                                                    objLineitemInfo.contractLineitem.serviceBehaviour,
                                                    objLineitemInfo.contractLineitem.serviceQuantity,
                                                    objLineitemInfo.contractLineitem.serviceQuantityType,
                                                    objLineitemInfo.contractLineitem.minimumUnits,
                                                    objLineitemInfo.contractLineitem.maximumUnits,
                                                    objLineitemInfo.contractLineitem.perYardRate,
                                                    objLineitemInfo.contractLineitem.totalYardRate,
                                                    objLineitemInfo.contractLineitem.isSurchargable,
                                                    objLineitemInfo.contractLineitem.isTaxable,
                                                    objLineitemInfo.contractLineitem.serviceFrequency,
                                                    objLineitemInfo.contractLineitem.noOfTimes,
                                                    objLineitemInfo.contractLineitem.servicedOn,
                                                    objLineitemInfo.contractLineitem.perUnitCost,
                                                    objLineitemInfo.contractLineitem.totalCost,
                                                    objLineitemInfo.contractLineitem.monthlyRecurringCharge,
                                                    objLineitemInfo.contractLineitem.sixMonthsAverage,
                                                    objLineitemInfo.contractLineitem.size,
                                                    objLineitemInfo.contractLineitem.lineItemType,
                                                    objLineitemInfo.contractLineitem.capPercent,
                                                    objLineitemInfo.contractLineitem.offeredCapPercent,
                                                    objLineitemInfo.contractLineitem.apiApplicable,
                                                    objLineitemInfo.negotiationLineitem.negotiationLineitemId,
                                                    objLineitemInfo.negotiationLineitem.serviceCatalogueId,
                                                    objLineitemInfo.negotiationLineitem.vendorDescription,
                                                    objLineitemInfo.negotiationLineitem.serviceBehaviour,
                                                    objLineitemInfo.negotiationLineitem.serviceQuantity,
                                                    objLineitemInfo.negotiationLineitem.serviceQuantityType,
                                                    objLineitemInfo.negotiationLineitem.minimumUnits,
                                                    objLineitemInfo.negotiationLineitem.maximumUnits,
                                                    objLineitemInfo.negotiationLineitem.perYardRate,
                                                    objLineitemInfo.negotiationLineitem.totalYardRate,
                                                    objLineitemInfo.negotiationLineitem.isSurchargable,
                                                    objLineitemInfo.negotiationLineitem.isTaxable,
                                                    objLineitemInfo.negotiationLineitem.serviceFrequency,
                                                    objLineitemInfo.negotiationLineitem.noOfTimes,
                                                    objLineitemInfo.negotiationLineitem.servicedOn,
                                                    objLineitemInfo.negotiationLineitem.perUnitCost,
                                                    objLineitemInfo.negotiationLineitem.totalCost,
                                                    objLineitemInfo.negotiationLineitem.monthlyRecurringCharge,
                                                    objLineitemInfo.negotiationLineitem.sixMonthsAverage,
                                                    objLineitemInfo.negotiationLineitem.size,
                                                    objLineitemInfo.negotiationLineitem.lineItemType,
                                                    objLineitemInfo.negotiationLineitem.capPercent,
                                                    objLineitemInfo.negotiationLineitem.offeredCapPercent,
                                                    objLineitemInfo.negotiationLineitem.isNegotiated,
                                                    objLineitemInfo.comments,
                                                    userId,
                                                    1,
                                                    objLineitemInfo.saveOrComplete);

                return res; 
        }


        public DataSet VectorGetMasterDataTrueupAccountLineitemInfo(AccountLineItemInfo objAccountInfoSearch, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();

            if(objAccountInfoSearch.type.Equals("Contract") && objAccountInfoSearch.contractLineitemId != null && objAccountInfoSearch.contractLineitemId != 0)
            {
                DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetMasterDataTrueupContractLineitemInfo.ToString(),
                                                   objAccountInfoSearch.action,
                                                   objAccountInfoSearch.contractLineitemId,
                                                   userId);

                return res;
            }
            else if(objAccountInfoSearch.contractLineitemId != null && objAccountInfoSearch.contractLineitemId !=0)
            {

                DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetMasterDataTrueupAccountLineitemInfo.ToString(),
                                                    objAccountInfoSearch.action,
                                                    objAccountInfoSearch.contractLineitemId, 
                                                    userId);


                return res;
            }
            return null;
        }

    }
}
