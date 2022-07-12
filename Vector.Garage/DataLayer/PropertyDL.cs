using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Garage.Entities;

namespace Vector.Garage.DataLayer
{
    public class PropertyDL : DisposeLogic
    {
        private Database objVectorConnection;
        private VectorDataConnection objVectorDB;

        DataSet dsData = new DataSet();

        public PropertyDL(VectorDataConnection objVectorDB)
        {
            this.objVectorDB = objVectorDB;
        }

        private Database GetVectorDBInstance()
        {
            return objVectorDB.GetVectorDBConnection();
        }

        public DataSet ManagePropertyInfo(PropertyInfo objPropertyInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManagePropertyInfo.ToString(),
                                                  objPropertyInfo.Action,
                                                  objPropertyInfo.PropertyId,
                                                  objPropertyInfo.PropertyInfoId,
                                                  objPropertyInfo.TaskId,
                                                  objPropertyInfo.PropertyName,
                                                  objPropertyInfo.PropertyLegalName,
                                                  objPropertyInfo.PropertyLedgerNo,
                                                  objPropertyInfo.ClientId,
                                                  objPropertyInfo.Region,
                                                  objPropertyInfo.PropertyStatus,
                                                  objPropertyInfo.OwnershipTransfer,
                                                  objPropertyInfo.TransferType,
                                                  objPropertyInfo.TransferFromTo,
                                                  objPropertyInfo.TransferFromToClientId,
                                                  objPropertyInfo.TransferDate,
                                                  objPropertyInfo.MarketType,
                                                  objPropertyInfo.PropertyType,
                                                  objPropertyInfo.PropertyDocumentXml,
                                                  "",
                                                  objPropertyInfo.Comments,
                                                  objPropertyInfo.UserId,
                                                  objPropertyInfo.IsFromTask,
                                                  objPropertyInfo.SaveOrComplete);
            return res;
        }

        public DataSet GetPropertyInfo(PropertyInfoSearch objPropertyInfoSearch, Int64 userID)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetPropertyInfo",
               objPropertyInfoSearch.Action,
               objPropertyInfoSearch.PropertyId,
               objPropertyInfoSearch.PropertyInfoId,
               objPropertyInfoSearch.TaskId);
        }
        public DataSet GetPropertyAddressInfo(PropertyAddressInfoSearch objSearchPropertyAddress, Int64 userID)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetPropertyAddressInfo",
                objSearchPropertyAddress.Action,
                objSearchPropertyAddress.PropertyId,
                objSearchPropertyAddress.PropertyAddressInfoId,
                objSearchPropertyAddress.TaskId);
        }


        public DataSet ManagePropertyAddressInfo(PropertyAddressInfo objPropertyAddressInfo, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManagePropertyAddressInfo.ToString(),
                                                objPropertyAddressInfo.Action,
                                                  objPropertyAddressInfo.PropertyId,
                                                  objPropertyAddressInfo.ProeprtyAddressInfoId,
                                                  objPropertyAddressInfo.TaskId,
                                                  objPropertyAddressInfo.MailIngAddress1,
                                                  objPropertyAddressInfo.MailIngAddress2,
                                                  objPropertyAddressInfo.MailingCity,
                                                  objPropertyAddressInfo.MailingState,
                                                  objPropertyAddressInfo.MailingZip,
                                                  objPropertyAddressInfo.MailingCountry,
                                                  objPropertyAddressInfo.MailingPhone,
                                                  objPropertyAddressInfo.MailingPhoneExtension,
                                                  objPropertyAddressInfo.MailingMobile,
                                                  objPropertyAddressInfo.MailingEmail,
                                                  objPropertyAddressInfo.MialingFax,
                                                  objPropertyAddressInfo.BillingAddress1,
                                                  objPropertyAddressInfo.BillingAddress2,
                                                  objPropertyAddressInfo.BillingCity,
                                                  objPropertyAddressInfo.BillingState,
                                                  objPropertyAddressInfo.BillingZip,
                                                  objPropertyAddressInfo.BillingCountry,
                                                  objPropertyAddressInfo.BillingPhone,
                                                  objPropertyAddressInfo.BillingPhoneExtension,
                                                  objPropertyAddressInfo.BillingMobile,
                                                  objPropertyAddressInfo.BillingEmail,
                                                  objPropertyAddressInfo.BillingFax,
                                                  objPropertyAddressInfo.Comments,
                                                  objPropertyAddressInfo.UserId,
                                                  objPropertyAddressInfo.IsFromTask,
                                                  objPropertyAddressInfo.SaveOrComplete);
            return res;
        }

        public DataSet VectorGetPropertyInvoicePreferencesDL(PropertyInvoicePreferenceSearch objPropertyInvoicePreferenceSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetPropertyInvoicePreferences.ToString(),
                objPropertyInvoicePreferenceSearch.Action,
                objPropertyInvoicePreferenceSearch.PropertyId,
                objPropertyInvoicePreferenceSearch.PropertyInvoicingPreferencesId,
                objPropertyInvoicePreferenceSearch.TaskId);
        }

        public DataSet VectorManagePropertyInvoicePreferencesDL(PropertyInvoicePreferences objProeprtyInvoicePreferences)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManagePropertyInvoicePreferences.ToString(),
                                                objProeprtyInvoicePreferences.Action,
                                                  objProeprtyInvoicePreferences.PropertyId,
                                                  objProeprtyInvoicePreferences.PropertyInvoicingPreferencesId,
                                                  objProeprtyInvoicePreferences.TaskId,
                                                   objProeprtyInvoicePreferences.SummeryRecipient,
                                                  objProeprtyInvoicePreferences.SummaryInvoiceDeliveryMode,
                                                  objProeprtyInvoicePreferences.SummaryInvoiceDeliveryFrequency,
                                                  objProeprtyInvoicePreferences.SummaryInvoiceDayOfTheMonth,
                                                  objProeprtyInvoicePreferences.SummaryInvoiceIsLastDay,
                                                  objProeprtyInvoicePreferences.SummaryInvoiceDayOfTheWeek,
                                                  objProeprtyInvoicePreferences.SummaryInvoiceModeOfReceipt,
                                                  objProeprtyInvoicePreferences.SummaryInvoiceSendTo,
                                                  objProeprtyInvoicePreferences.SummaryInvoiceDeliveryInstructions,
                                                  objProeprtyInvoicePreferences.VendorRecipient,
                                                  objProeprtyInvoicePreferences.VendorInvoiceDeliveryMode,
                                                  objProeprtyInvoicePreferences.VendorInvoiceDeliveryFrequency,
                                                  objProeprtyInvoicePreferences.VendorInvoiceDayOfTheMonth,
                                                  objProeprtyInvoicePreferences.VendorInvoiceIsLastDay,
                                                  objProeprtyInvoicePreferences.VendorInvoiceDayOfTheWeek,
                                                  objProeprtyInvoicePreferences.VendorInvoiceModeOfReceipt,
                                                  objProeprtyInvoicePreferences.VendorInvoiceSendTo,
                                                  objProeprtyInvoicePreferences.VendorInvoiceDeliveryInstructions,
                                                  objProeprtyInvoicePreferences.RSRecipient,
                                                  objProeprtyInvoicePreferences.RsInvoiceDeliveryMode,
                                                  objProeprtyInvoicePreferences.RsInvoiceDeliveryFrequency,
                                                  objProeprtyInvoicePreferences.RsInvoiceDayOfTheMonth,
                                                  objProeprtyInvoicePreferences.RsInvoiceIsLastDay,
                                                  objProeprtyInvoicePreferences.RsInvoiceDayOfTheWeek,
                                                  objProeprtyInvoicePreferences.RsInvoiceModeOfReceipt,
                                                  objProeprtyInvoicePreferences.RsInvoiceSendTo,
                                                  objProeprtyInvoicePreferences.RsInvoiceDeliveryInstructions,
                                                  objProeprtyInvoicePreferences.Comments,
                                                  objProeprtyInvoicePreferences.UserId,
                                                  objProeprtyInvoicePreferences.IsFromTask,
                                                  objProeprtyInvoicePreferences.SaveOrComplete);

            return res;
        }

        public DataSet VectorGetPropertyContactInfoDL(PropertyContactInfoSearch objPropertyContactInfoSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetPropertyContactInfo.ToString(),
                objPropertyContactInfoSearch.Action,
                objPropertyContactInfoSearch.PropertyId,
                objPropertyContactInfoSearch.PropertyContactInfoId,
                objPropertyContactInfoSearch.TaskId);
        }

        public DataSet VectorManagePropertyContactInfoDL(PropertyContactInfo objPropertyContactInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManagePropertyContactInfo.ToString(),
                                                objPropertyContactInfo.Action,
                                                  objPropertyContactInfo.PropertyId,
                                                  objPropertyContactInfo.TaskId,
                                                  objPropertyContactInfo.ContactsXml,
                                                  objPropertyContactInfo.Comments, objPropertyContactInfo.UserId,
                                                  objPropertyContactInfo.IsFromTask, objPropertyContactInfo.SaveOrComplete
                                                  );
            return res;
        }

        public DataSet VectorManagePropertyMiscInfoDL(PropertyMiscInfo objPropertyMiscInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManagePropertyMiscellaneousInfo.ToString(),
                                                objPropertyMiscInfo.Action,
                                                objPropertyMiscInfo.PropertyId,
                                                objPropertyMiscInfo.PropertyMiscellaneousInfoId,
                                                objPropertyMiscInfo.TaskId,
                                                objPropertyMiscInfo.RsSavingSharePercentage,
                                                objPropertyMiscInfo.BaselineVendorId,
                                                objPropertyMiscInfo.Attributes,
                                                objPropertyMiscInfo.AttributeTotal,
                                                objPropertyMiscInfo.Occupied,
                                                objPropertyMiscInfo.ExistingVendorContract,
                                                objPropertyMiscInfo.ExistingVendorContractExpDate,
                                                objPropertyMiscInfo.Addendum,
                                                objPropertyMiscInfo.Comments,
                                                objPropertyMiscInfo.UserId,
                                                objPropertyMiscInfo.IsFromTask,
                                                objPropertyMiscInfo.SaveOrComplete,
                                                objPropertyMiscInfo.SalesPersons,
                                                objPropertyMiscInfo.AccountExecutives
                                                  );
            return res;
        }

        public DataSet GetPropertyMiscInfo(PropertyMiscInfoSearch propertyInfoSearch)
        {
            objVectorConnection = GetVectorDBInstance(); 
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetPropertyMiscellaneousInfo.ToString(),
                                                "GetPropertyMiscellaneousInfo",
                                                propertyInfoSearch.PropertyId,
                                                  propertyInfoSearch.PropertyMiscellaneousInfoId,
                                                  propertyInfoSearch.TaskId);

            return res;
        }
        public DataSet GetPropertyContractInfo(PropertyContractInfoSearch propertyContractInfoSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetPropertyContractInfo.ToString(),
                                                propertyContractInfoSearch.Action,
                                                propertyContractInfoSearch.PropertyId,
                                                propertyContractInfoSearch.PropertyContractInfoId,
                                                propertyContractInfoSearch.TaskId);

            return res;
        }
        public DataSet VectorManagePropertyContractInfoDL(PropertyContractInfo propertyContractInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManagePropertyContractInfo.ToString(),
                                                propertyContractInfo.Action,
                                                  propertyContractInfo.PropertyId,
                                                   propertyContractInfo.PropertyContractInfoId,
                                                  propertyContractInfo.TaskId,
                                                  propertyContractInfo.ContractVersion,
                                                  propertyContractInfo.VectorRevenueStream,
                                                  propertyContractInfo.BasisOfMgmtFee,
                                                  propertyContractInfo.ManagementFee,
                                                  propertyContractInfo.DiscountType,
                                                  propertyContractInfo.DiscountPercentage,
                                                  propertyContractInfo.RsSharingPercentage,
                                                  propertyContractInfo.Comments,
                                                  propertyContractInfo.SaveOrComplete,
                                                  propertyContractInfo.UserId
                                                  );
            return res;
        }

        public DataSet GetVectorViewPropertyInformation(string action, Int64 propertyId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorViewProperty.ToString(), action, propertyId);
            return res;
        }

        public DataSet GetAllVectorViewPropertyInformation(Int64 UserId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetPropertyDetails.ToString(),UserId);
            return res;
        }

        public DataSet VectorGetPropertySearchDL(PropertySearch objPropertyInfo, Int64 UserId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetSearchProperties.ToString(),
                                                objPropertyInfo.ClientId,
                                                objPropertyInfo.AccountNo,
                                                objPropertyInfo.PropertyNo,
                                                objPropertyInfo.PropertyName,
                                                objPropertyInfo.PropertyType,
                                                objPropertyInfo.PropertyLegalName,
                                                objPropertyInfo.PropertyLedgerNo,
                                                objPropertyInfo.Region,
                                                objPropertyInfo.OwnershipTransfer,
                                                objPropertyInfo.TransferType,
                                                objPropertyInfo.TransferFromTo,
                                                objPropertyInfo.TransferDate,
                                                objPropertyInfo.MarketType,
                                                objPropertyInfo.city,
                                                objPropertyInfo.state,
                                                objPropertyInfo.zip,
                                                objPropertyInfo.Address,
                                                objPropertyInfo.VendorName,
                                                 UserId);
            return res;
        }

        public DataSet VectorGetPropertyBaselineInvoices(PropertyBaselineinvoices objPropertyInfo,Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetPropertyBaselineInfo.ToString(),
                                                objPropertyInfo.Action,
                                                objPropertyInfo.PropertyId,
                                                objPropertyInfo.TaskId,
                                                userId
                                                  );
            return res;
        } 

        public DataSet ManageBaselineInvoiceInfo(BaselineInvoiceInfo objPropertyInfo, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManagePropertyBaselineInfo.ToString(),
                                                objPropertyInfo.Action,
                                                objPropertyInfo.baseLineAmount,
                                                objPropertyInfo.baselineId, 
                                                objPropertyInfo.baselineVendorId,
                                                objPropertyInfo.baselineVendorName,
                                                objPropertyInfo.existingContractExpiryDate,
                                                objPropertyInfo.nonRenualWindowMin,
                                                objPropertyInfo.nonRenualWindowMix,
                                                objPropertyInfo.propertyBaseLineInvoicesId,
                                                objPropertyInfo.PropertyId,
                                                objPropertyInfo.renualType,
                                                objPropertyInfo.serviceType,
                                                userId
                                                  );
            return res;
        }

        public DataSet UploadPropertyBaselineInvoices(PropertyBaselineDocuments objPropertyInfo, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorUploadPropertyBaselineInfo.ToString(),
                                                objPropertyInfo.Action,
                                                objPropertyInfo.propertyId,
                                                objPropertyInfo.documents,
                                                 objPropertyInfo.taskId,
                                                  objPropertyInfo.IsFromTask,
                                                  objPropertyInfo.SaveOrComplete,
                                                userId
                                                  );
            return res;
        }


        public DataSet GetPropertyInfo(string type, string action, Int64 propertyId, Int64 UserId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = new DataSet();

            if (type.Equals("PropertyBaselines"))
            {
                res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetPropertyBaselines.ToString(),
                                                     action,
                                                     propertyId,
                                                     UserId
                                                    );

                if (!DataManager.IsNullOrEmptyDataSet(res))
                    res.Tables[0].TableName = "propertyBaselines";
            }
            else if (type.Equals("PropertyNegotiations"))
            {
                res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetPropertyNegotiations.ToString(),
                                                   action,
                                                     propertyId,
                                                     UserId
                                                    );
                if (!DataManager.IsNullOrEmptyDataSet(res))
                    res.Tables[0].TableName = "PropertyNegotiations";

            }
            else if (type.Equals("PropertyContracts"))
            {
                res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetPropertyContracts.ToString(),
                                                 action,
                                                     propertyId,
                                                     UserId
                                                    );

                if (!DataManager.IsNullOrEmptyDataSet(res))
                    res.Tables[0].TableName = "propertyContracts";
            }
            else if (type.Equals("PropertyAccounts"))
            {
                res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetPropertyAccounts.ToString(),
                                                 action,
                                                     propertyId,
                                                     UserId
                                                    );

                if (!DataManager.IsNullOrEmptyDataSet(res))
                    res.Tables[0].TableName = "propertyAccounts";
            }
            else if (type.Equals("PropertyDocuments"))
            {
                res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetPropertyDocuments.ToString(),
                                                 action,
                                                     propertyId,
                                                     UserId
                                                    );

                if (!DataManager.IsNullOrEmptyDataSet(res))
                {
                    res.Tables[0].TableName = "propertyDocuments";
                    res.Tables[1].TableName = "propertyActivity";
                }
            }
            else if (type.Equals("PropertyContacts"))
            {
                res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetPropertyContacts.ToString(),
                                                 action,
                                                     propertyId,
                                                     UserId
                                                    );

                if (!DataManager.IsNullOrEmptyDataSet(res))
                    res.Tables[0].TableName = "propertyContacts";
            }
            return res;
        }

    }
}
