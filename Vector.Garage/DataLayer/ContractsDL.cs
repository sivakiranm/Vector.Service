using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Garage.Entities;
using System;

namespace Vector.Garage.DataLayer
{
    public class ContractsDL : DisposeLogic
    {
        private Database objVectorConnection;
        private VectorDataConnection objVectorDB;

        public ContractsDL(VectorDataConnection objVectorDB)
        {
            this.objVectorDB = objVectorDB;
        }

        private Database GetVectorDBInstance()
        {
            return objVectorDB.GetVectorDBConnection();
        }

        public DataSet GetContracts(ContractSearch objContractSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetContractsInfo.ToString(),
                                objContractSearch.Action,
                                objContractSearch.Client,
                                objContractSearch.Property,
                                objContractSearch.Vendor,
                                objContractSearch.Negotiation,
                                objContractSearch.ContractNo,
                                objContractSearch.ContractId,
                                objContractSearch.ContractInfoId,
                                objContractSearch.PropertyId,
                                objContractSearch.VendorId,
                                objContractSearch.city,
                                objContractSearch.state,
                                objContractSearch.zip,
                                objContractSearch.Address,
                                objContractSearch.TaskId);

            return res;
        }

        public DataSet GetContractData(ContractData objContractData)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetContractsInfo.ToString(),
                                objContractData.Action, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                                objContractData.ContractId,
                                objContractData.ContractInfoId, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                                objContractData.TaskId);

            return res;
        }

        //public DataSet UpdateContract(Contract objContract)
        //{
        //    objVectorConnection = GetVectorDBInstance();
        //    DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorUpdateContractInfo.ToString(),
        //                        objContract.Action,
        //                        objContract.ContractId,
        //                        objContract.ContractInfoId,
        //                        objContract.ContractBeginDate,
        //                        objContract.ContractEndDate,
        //                        objContract.ContractTerm,
        //                        objContract.RenewalType,
        //                        objContract.RenewalDate,
        //                        objContract.ManagementFee,
        //                        objContract.ApprovedAnnualIncrease,
        //                        objContract.ReceiveVendorInvoices,
        //                        objContract.LineItemDetailsXml,
        //                        objContract.Comments,
        //                        objContract.FilesAttached.ListToDataSet().GetXml().ToString(),
        //                        objContract.UserId,
        //                        objContract.TaskId,
        //                        objContract.IsFromTask,
        //                        objContract.SaveOrComplete);

        //    return res;
        //}

        public DataSet UpdateContract(Contract objContract, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageContractInfo.ToString(),
                                objContract.Action,
                                objContract.ContractId,
                                objContract.ContractInfoId,
                                objContract.ContractBeginDate,
                                objContract.ContractEndDate,
                                objContract.ContractTerm,
                                objContract.RenewalType,
                                objContract.RenewalDate,
                                objContract.ManagementFee,
                                objContract.ApprovedAnnualIncrease,
                                objContract.ReceiveVendorInvoices,
                                objContract.LineItemDetailsXml,
                                objContract.IsSavings,
                                objContract.VendorPaymentTerm,
                                objContract.Comments,
                                objContract.FilesAttached.ListToDataSet().GetXml().ToString(),
                                userId,
                                objContract.TaskId,
                                objContract.IsFromTask,
                                objContract.SaveOrComplete);

            return res;
        }

        public DataSet ApproveOrDeclineAnnualIncrease(ApproveOrDeclineAnnualIncrease objApproveOrDeclineAnnualIncrease)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageContractApproveAnnualIncreaseLineItemsInfo.ToString(),
                                                objApproveOrDeclineAnnualIncrease.Action,
                                                objApproveOrDeclineAnnualIncrease.ContractId,
                                                objApproveOrDeclineAnnualIncrease.ContractApproveAnnualIncrementInfoId,
                                                objApproveOrDeclineAnnualIncrease.TaskId,
                                                objApproveOrDeclineAnnualIncrease.LineItems,
                                                objApproveOrDeclineAnnualIncrease.Comments,
                                                objApproveOrDeclineAnnualIncrease.UserId,
                                                objApproveOrDeclineAnnualIncrease.IsFromTask,
                                                objApproveOrDeclineAnnualIncrease.SaveOrComplete);

            return res;
        }

        public DataSet ManageContractLineItems(ContractLineItems objContractLineItems)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorAddUpdateContractLineItems.ToString(),
                                                objContractLineItems.ContractId,
                                                objContractLineItems.ContractLineItemId,
                                                objContractLineItems.LineItemsXml,
                                                objContractLineItems.Comments,
                                                objContractLineItems.UserId);

            return res;
        }

        public DataSet RenewOrArchiveContract(Contract objContract)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageRenewArchiveContractInfo.ToString(),
                                objContract.Action,
                                objContract.ContractId,
                                objContract.ContractInfoId,
                                objContract.TaskId,
                                objContract.ContractStatus,
                                objContract.Comments,
                                objContract.UserId,
                                objContract.IsFromTask,
                                objContract.SaveOrComplete);

            return res;
        }

        public DataSet UpcomingContractExpiry(UpcomingContractExpiry objUpcomingContractExpiry)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetContractExpiryInfo.ToString(),
                                objUpcomingContractExpiry.Action,
                                objUpcomingContractExpiry.ReportBy,
                                objUpcomingContractExpiry.ClientName,
                                objUpcomingContractExpiry.ClientId,
                                objUpcomingContractExpiry.PropertyName,
                                objUpcomingContractExpiry.PropertyId,
                                objUpcomingContractExpiry.VendorName,
                                objUpcomingContractExpiry.VendorId,
                                objUpcomingContractExpiry.ContractNo,
                                objUpcomingContractExpiry.ContractId,
                                objUpcomingContractExpiry.Negotiator,
                                objUpcomingContractExpiry.NegotiationNo,
                                objUpcomingContractExpiry.AccountNUmber,
                                objUpcomingContractExpiry.AccountId,
                                DateTime.Now,
                               //objUpcomingContractExpiry.ContractEffectiveDateFrom,
                               DateTime.Now,
                                // objUpcomingContractExpiry.ContractEffectiveDateTo,
                                objUpcomingContractExpiry.UserId);

            return res;
        }

        public DataSet TransmitContract(ContractApproval objContract, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageContractTransmitInfo.ToString(),
                                objContract.Action,
                                objContract.ContractId,
                                0,
                                objContract.ContractNo,
                                objContract.VendorApproved,
                                objContract.ClientApproved,
                                objContract.VectorApproved,
                                objContract.Comments,
                                objContract.Documents.ListToDataSet().GetXml().ToString(),
                                objContract.LoggedInUserName,
                                objContract.TaskId,
                                objContract.IsFromTask,
                                objContract.SaveOrComplete,
                                userId);

            return res;
        }

        public DataSet GetContractEmailDetailsForTransmit(string contractId, string action)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.GetVectorContractDetailsForDocAndEmail.ToString(), contractId, action);
        }


        public DataSet GetTrueupPannelContractApprovalData(SearchEntity objSearchEntity, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetTrueupPanelContractApprovalData.ToString(),
                objSearchEntity.action,
                                                      objSearchEntity.clientId,
                                                    objSearchEntity.clientName,
                                                    objSearchEntity.propertyId,
                                                    objSearchEntity.propertyName,
                                                    objSearchEntity.accountId,
                                                    objSearchEntity.accountNumber,
                                                    objSearchEntity.negotiationId,
                                                    objSearchEntity.negotiationStatus,
                                                    objSearchEntity.accountStatus,
                                                    objSearchEntity.accountType,
                                                    objSearchEntity.contractId,
                                                    objSearchEntity.vendorId,
                                                    objSearchEntity.vendorName,
                                                    objSearchEntity.accountMode,
                                                    userId);
        } 
    }
}
