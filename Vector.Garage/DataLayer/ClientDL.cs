using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Garage.Entities;

namespace Vector.Garage.DataLayer
{
    public class ClientDL : DisposeLogic
    {
        private Database objVectorConnection;
        private VectorDataConnection objVectorDB;

        DataSet dsData = new DataSet();

        public ClientDL(VectorDataConnection objVectorDB)
        {
            this.objVectorDB = objVectorDB;
        }

        private Database GetVectorDBInstance()
        {
            return objVectorDB.GetVectorDBConnection();
        }

        public DataSet GetClientAgreementInfo(ClientAgreementInfoSearch objSearchClientAgreement, Int64 userID)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetClientAgreementInfo.ToString(),
                objSearchClientAgreement.Action,
                objSearchClientAgreement.ClientId,
                objSearchClientAgreement.ClientAgreementInfoId,
                objSearchClientAgreement.TaskId);
        }

        public DataSet ManageClientAgreementInfo(ClientAgreementInfo objClientAgreementInfo, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageClientAgreementInfo.ToString(),
                                                objClientAgreementInfo.Action,
                                                  objClientAgreementInfo.ClientId,
                                                  objClientAgreementInfo.ClientAgreementInfoId,
                                                  objClientAgreementInfo.TaskId,
                                                  objClientAgreementInfo.RenewalType,
                                                  objClientAgreementInfo.AgreementVenue,
                                                  objClientAgreementInfo.ChangesNeededClientApproval,
                                                  objClientAgreementInfo.CanVectorExecuteVendorContracts,
                                                  objClientAgreementInfo.RetroActiveAuditAllowed,
                                                  objClientAgreementInfo.TermToNegotiateAndProcessInvoices,
                                                  objClientAgreementInfo.OutClauseForVectorClientAgreement,
                                                  objClientAgreementInfo.NameOnVendorContract,
                                                  objClientAgreementInfo.NameOnVendorInvoice,
                                                  objClientAgreementInfo.NameOnRSInvoice,
                                                  objClientAgreementInfo.LOAEditsAllowed,
                                                  objClientAgreementInfo.PartialPortfolioAgreement,
                                                  objClientAgreementInfo.ProcessInvoicesPriorToNegotiation,
                                                  objClientAgreementInfo.ManageBill,
                                                  objClientAgreementInfo.StartDate,
                                                  objClientAgreementInfo.PaymentTerms,
                                                  objClientAgreementInfo.RenewalTerms,
                                                  objClientAgreementInfo.TermMonths,
                                                  objClientAgreementInfo.Comments,
                                                  objClientAgreementInfo.UserId,
                                                  objClientAgreementInfo.IsFromTask,
                                                  objClientAgreementInfo.SaveOrComplete);
            return res;
        }

        public DataSet VectorManageClientRegion(Region objRegion)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageRegion.ToString(),
                                                objRegion.RegionName,
                                                  objRegion.RegionAddedPage,
                                                  objRegion.UserId);

            return res;
        }

        public DataSet VectorManageClientVertical(Vertical objVertical)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageVerticals.ToString(),
                                                objVertical.VerticalName,
                                                  objVertical.VerticalAddedPage,
                                                  objVertical.UserId);

            return res;
        }


        public DataSet ManageClientInfo(ClientInfo objClientInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorManageClientInfo", objClientInfo.Action,
                objClientInfo.ClientId,
                objClientInfo.TaskId,
                objClientInfo.UserId,
                objClientInfo.ClientNo,
                objClientInfo.ClientName,
                objClientInfo.ClientLegalName,
                objClientInfo.ContractVersion,
                objClientInfo.VectorRevenueStream,
                objClientInfo.BasicOfManagementFee,
                objClientInfo.ManagementFee,
                objClientInfo.DiscountPercentage,
                objClientInfo.ClientStatus,
                objClientInfo.InActivationReason,
                objClientInfo.VectorSavingShare,
                objClientInfo.ClientRegions,
                objClientInfo.Vertical,
                objClientInfo.Currency,
                objClientInfo.Comments,
                objClientInfo.SaveOrComplete
                );
        }

        public DataSet GetClientsSearch(ClientInfoSearch clientInfoSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetClientInfo",
                clientInfoSearch.Action,
                clientInfoSearch.ClientId,
                clientInfoSearch.ClientInfoId,
                clientInfoSearch.TaskId);

        }

        public DataSet GetClientByClientInfoId(long clientId)
        {
            objVectorConnection = GetVectorDBInstance();
            var test = objVectorConnection.ExecuteDataSet("VectorGetClientInfo2", "GetClientInfoByClientInfoId", clientId, 0, 0, "");
            return test;

        }
        public DataSet GetClientByClientId(long clientId)
        {
            objVectorConnection = GetVectorDBInstance();
            var test = objVectorConnection.ExecuteDataSet("VectorGetClientInfo2", "GetClientInfoByClientId", 0, clientId, 0, "");
            return test;

        }
        public DataSet GetClientAddressInfo(ClientAddressInfoSearch objSearchClientAddress, Int64 userID)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetClientAddressInfo",
                objSearchClientAddress.Action,
                objSearchClientAddress.ClientId,
                objSearchClientAddress.ClientAddressInfoId,
                objSearchClientAddress.TaskId);
        }


        public DataSet VectorGetClientInvoicePreferencesDL(ClientInvoicePreferenceSearch objClientInvoicePreferenceSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetClientInvoicePreferences.ToString(),
                objClientInvoicePreferenceSearch.Action,
                objClientInvoicePreferenceSearch.ClientId,
                objClientInvoicePreferenceSearch.ClientInvoicingPreferencesId,
                objClientInvoicePreferenceSearch.TaskId);
        }

        public DataSet VectorGetClientContactInfoDL(ClientContactInfoSearch objClientContactInfoSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetClientContactInfo.ToString(),
                objClientContactInfoSearch.Action,
                objClientContactInfoSearch.ClientId,
                objClientContactInfoSearch.ClientContactInfoId,
                objClientContactInfoSearch.TaskId);
        }

        public DataSet VectorManageClientContactInfoDL(ClientContactInfo ClientContactInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageClientContactInfo.ToString(),
                                                ClientContactInfo.Action,
                                                  ClientContactInfo.ClientId,
                                                  ClientContactInfo.TaskId,
                                                  ClientContactInfo.ContactsXml,
                                                  ClientContactInfo.Comments, ClientContactInfo.UserId,
                                                  ClientContactInfo.IsFromTask, ClientContactInfo.SaveOrComplete
                                                  );
            return res;
        }


        public DataSet VectorManageClientInvoicePreferencesDL(ClientInvoicePreferences objClientInvoicePreferences)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageClientInvoicePreferences.ToString(),
                                                objClientInvoicePreferences.Action,
                                                  objClientInvoicePreferences.ClientId,
                                                  objClientInvoicePreferences.ClientInvoicingPreferencesId,
                                                  objClientInvoicePreferences.TaskId,
                                                  objClientInvoicePreferences.SummeryRecipient,
                                                  objClientInvoicePreferences.SummaryInvoiceDeliveryMode,
                                                  objClientInvoicePreferences.SummaryInvoiceDeliveryFrequency,
                                                  objClientInvoicePreferences.SummaryInvoiceDayOfTheMonth,
                                                  objClientInvoicePreferences.SummaryInvoiceIsLastDay,
                                                  objClientInvoicePreferences.SummaryInvoiceDayOfTheWeek,
                                                  objClientInvoicePreferences.SummaryInvoiceModeOfReceipt,
                                                  objClientInvoicePreferences.SummaryInvoiceSendTo,
                                                  objClientInvoicePreferences.SummaryInvoiceDeliveryInstructions,
                                                   objClientInvoicePreferences.VendorRecipient,
                                                  objClientInvoicePreferences.VendorInvoiceDeliveryMode,
                                                  objClientInvoicePreferences.VendorInvoiceDeliveryFrequency,
                                                  objClientInvoicePreferences.VendorInvoiceDayOfTheMonth,
                                                  objClientInvoicePreferences.VendorInvoiceIsLastDay,
                                                  objClientInvoicePreferences.VendorInvoiceDayOfTheWeek,
                                                  objClientInvoicePreferences.VendorInvoiceModeOfReceipt,
                                                  objClientInvoicePreferences.VendorInvoiceSendTo,
                                                  objClientInvoicePreferences.VendorInvoiceDeliveryInstructions,
                                                    objClientInvoicePreferences.RSRecipient,
                                                  objClientInvoicePreferences.RsInvoiceDeliveryMode,
                                                  objClientInvoicePreferences.RsInvoiceDeliveryFrequency,
                                                  objClientInvoicePreferences.RsInvoiceDayOfTheMonth,
                                                  objClientInvoicePreferences.RsInvoiceIsLastDay,
                                                  objClientInvoicePreferences.RsInvoiceDayOfTheWeek,
                                                  objClientInvoicePreferences.RsInvoiceModeOfReceipt,
                                                  objClientInvoicePreferences.RsInvoiceSendTo,
                                                  objClientInvoicePreferences.RsInvoiceDeliveryInstructions,
                                                  objClientInvoicePreferences.Comments,
                                                  objClientInvoicePreferences.UserId,
                                                  objClientInvoicePreferences.IsFromTask,
                                                  objClientInvoicePreferences.SaveOrComplete);

            return res;
        }

        public DataSet ManageClientAddressInfo(ClientAddressInfo objClientAddressInfo, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageClientAddressInfo.ToString(),
                                                objClientAddressInfo.Action,
                                                  objClientAddressInfo.ClientId,
                                                  objClientAddressInfo.ClientAddressInfoId,
                                                  objClientAddressInfo.TaskId,
                                                  objClientAddressInfo.MailIngAddress1,
                                                  objClientAddressInfo.MailIngAddress2,
                                                  objClientAddressInfo.MailingCity,
                                                  objClientAddressInfo.MailingState,
                                                  objClientAddressInfo.MailingZip,
                                                  objClientAddressInfo.MailingCountry,
                                                  objClientAddressInfo.MailingPhone,
                                                  objClientAddressInfo.MailingPhoneExtension,
                                                  objClientAddressInfo.MailingMobile,
                                                  objClientAddressInfo.MailingEmail,
                                                  objClientAddressInfo.MialingFax,
                                                  objClientAddressInfo.BillingAddress1,
                                                  objClientAddressInfo.BillingAddress2,
                                                  objClientAddressInfo.BillingCity,
                                                  objClientAddressInfo.BillingState,
                                                  objClientAddressInfo.BillingZip,
                                                  objClientAddressInfo.BillingCountry,
                                                  objClientAddressInfo.BillingPhone,
                                                  objClientAddressInfo.BillingPhoneExtension,
                                                  objClientAddressInfo.BillingMobile,
                                                  objClientAddressInfo.BillingEmail,
                                                  objClientAddressInfo.BillingFax,
                                                  objClientAddressInfo.Comments,
                                                  objClientAddressInfo.UserId,
                                                  objClientAddressInfo.IsFromTask,
                                                  objClientAddressInfo.SaveOrComplete);
            return res;
        }

        public DataSet VectorManageClientContractInfoDL(ClientContractInfo objClientContractInfo, string contractDocs)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageClientContractInfo.ToString(),
                                                objClientContractInfo.Action,
                                                  objClientContractInfo.ClientId,
                                                  objClientContractInfo.ClientContractInfoId,
                                                  objClientContractInfo.TaskId,
                                                 objClientContractInfo.ClientContractBeginDate,
                                                 objClientContractInfo.ClientContractEndDate,
                                                 objClientContractInfo.DateOfSigning,
                                                 //objClientContractInfo.PaymentTerms,
                                                 //objClientContractInfo.RenewalTerms,
                                                 //objClientContractInfo.TermMonths,
                                                 objClientContractInfo.RsSavingsSharePercent,
                                                 objClientContractInfo.ApprovedAnnualIncrease,
                                                 objClientContractInfo.Status == "1" ? true : false,
                                                 objClientContractInfo.Comments,
                                                 objClientContractInfo.UserId,
                                                 objClientContractInfo.IsFromTask == objClientContractInfo.IsFromTask,
                                                 objClientContractInfo.SaveOrComplete,
                                                 contractDocs,
                                                 objClientContractInfo.SalesPersons,
                                                 objClientContractInfo.Negotiations,
                                                 objClientContractInfo.AccountExecutives
                                                 );

            return res;
        }

        internal DataSet ManageDealPackage(string dealSheetXML, string invoicePreferencesXML, string propertyInfoListXML, string propertyInvDistributionListXML,
                                        string propertyBLInvoiceListXML, DealPackage objDealPackage)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageClientDealPackage.ToString(),
                                                "AddDealPackage", objDealPackage.ClientId, dealSheetXML, invoicePreferencesXML, propertyInfoListXML, propertyInvDistributionListXML,
                                                propertyBLInvoiceListXML, objDealPackage.FileName, objDealPackage.Comments, objDealPackage.UserId,
                                                objDealPackage.TaskId, objDealPackage.IsFromTask
                                                 );
            return res;
        }

        public DataSet VectorManageClientRoleDL(ClientContactRole objClientContactRole)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageClientRole.ToString(),
                                                objClientContactRole.ContactType,
                                                  objClientContactRole.ContactRoleMasterName,
                                                  objClientContactRole.UserId);

            return res;
        }

        public DataSet GetClientContractInfo(Int64 clientID, Int64 taskId)
        {
            objVectorConnection = GetVectorDBInstance();
            // if taskId write another query or SP
            //DataSet res = objVectorConnection.ExecuteDataSet(CommandType.Text, 
            //    "SELECT * FROM Clients where ClientId = " + clientID + ";" +
            //    " SELECT * FROM ClientDocuments WHERE  ClientId = " + clientID + ";" +
            //    "SELECT * FROM ClientContractResourceInfo WHERE  ClientId = " + clientID + ";");
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetClientContractInfo.ToString(),
                                                "GetClientContractInfo",
                                                clientID,
                                                  0,
                                                  taskId);

            return res;
        }


        public DataSet GetVectorViewClientInformation(string action, Int64 clientId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorViewClient.ToString(), action, clientId);
            return res;
        }

        public DataSet VectorGetClientSearchDL(ClientInfo objClientInfo, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetSearchClients",
                                                objClientInfo.Currency,
                                                objClientInfo.Vertical,
                                                objClientInfo.ClientRegions,
                                                objClientInfo.InActivationReason,
                                                objClientInfo.ClientStatus,
                                                objClientInfo.BasicOfManagementFee,
                                                objClientInfo.DiscountPercentage,
                                                objClientInfo.ManagementFee,
                                                objClientInfo.VectorRevenueStream,
                                                objClientInfo.ContractVersion,
                                                objClientInfo.ClientLegalName,
                                                objClientInfo.ClientNo,
                                                objClientInfo.ClientId,
                                                userId
                );
        }

        public DataSet GetClientDealPackageInfo(DealPackage objClientInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetClientDealPackageInfo.ToString(),
                                                objClientInfo.Action,
                                                Convert.ToInt64(objClientInfo.ClientId),
                                                 objClientInfo.TaskId);
            
        }


        public DataSet CompleteDealPackage(Int64 taskId,Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorCompleteDealPackage.ToString(),
                taskId, userId);
            return res;
        }


        public DataSet GetClientRelatedProperties(string action, Int64 clientId,Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetPropertiesByClient.ToString(), action, clientId, userId);
            return res;
        }
    }
}
