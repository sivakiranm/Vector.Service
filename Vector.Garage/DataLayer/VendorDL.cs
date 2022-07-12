using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Garage.Entities;

namespace Vector.Garage.DataLayer
{
    public class VendorDL : DisposeLogic
    {
        private Database objVectorConnection;
        private VectorDataConnection objVectorDB;

        DataSet dsData = new DataSet();

        public VendorDL(VectorDataConnection objVectorDB)
        {
            this.objVectorDB = objVectorDB;
        }

        private Database GetVectorDBInstance()
        {
            return objVectorDB.GetVectorDBConnection();
        }

        public DataSet GetVendorInfo(VendorInfoSearch objVendorInfoSearch, Int64 userID)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetVendorInfo.ToString(),
               objVendorInfoSearch.Action,
               objVendorInfoSearch.VendorId,
               objVendorInfoSearch.VendorInfoId,
               objVendorInfoSearch.TaskId,
               objVendorInfoSearch.VendorName);
        }




        public DataSet ManageVendorInfo(VendorInfo objVendorInfo, string vendorDocuments)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageVendorInfo.ToString(),
                                                  objVendorInfo.Action,
                                                  objVendorInfo.VendorId,
                                                  objVendorInfo.VendorInfoId,
                                                  objVendorInfo.VendorCorporateId,
                                                  objVendorInfo.TaskId,
                                                  objVendorInfo.VendorName,
                                                  objVendorInfo.VendorStatus,
                                                  objVendorInfo.BillingAnalyst,
                                                  objVendorInfo.Negotiator,
                                                  objVendorInfo.invoiceRecipients,
                                                  objVendorInfo.ModeOfInvoiceReciept,
                                                  vendorDocuments,
                                                  objVendorInfo.ContractName,
                                                  objVendorInfo.Comments,
                                                  objVendorInfo.UserId,
                                                  objVendorInfo.IsFromTask,
                                                  objVendorInfo.SaveOrComplete);
            return res;
        }

        public DataSet VectorGetVendorCoroporateContactInfoDL(VendorCorporateContactInfoSearch objVendorCorporateContactInfoSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetVendorCorporateContactInfo.ToString(),
                objVendorCorporateContactInfoSearch.Action,
                objVendorCorporateContactInfoSearch.VendorCorporateId,
                objVendorCorporateContactInfoSearch.VendorCorporateContactInfoId,
                objVendorCorporateContactInfoSearch.TaskId);
        }

        public DataSet VectorManageVendorCoroporateContactInfoDL(VendorCorporateContactInfo objVendorCorporateContactInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageVendorCorporateContactInfo.ToString(),
                                                objVendorCorporateContactInfo.Action,
                                                  objVendorCorporateContactInfo.VendorCorporateId,
                                                  objVendorCorporateContactInfo.TaskId,
                                                  objVendorCorporateContactInfo.ContactsXml,
                                                  objVendorCorporateContactInfo.Comments, objVendorCorporateContactInfo.UserId,
                                                  objVendorCorporateContactInfo.IsFromTask, objVendorCorporateContactInfo.SaveOrComplete
                                                  );
            return res;
        }

        public DataSet VectorGetVendorCorporateInfoDL(Int64 vendorCorporateId, Int64 taskId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetVendorCorporateInfo.ToString(),
                "GetVendorCorporateInfo",
                vendorCorporateId,
                0,
                taskId);
        }

        public DataSet VectorManageVendorCorporateInfoDL(VendorCorporateInfo objVendorCorporateInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageVendorCorporateInfo.ToString(),
                                                 objVendorCorporateInfo.Action,
                                 objVendorCorporateInfo.VendorCorporateId,
                                 objVendorCorporateInfo.VendorCorporateInfoId,
                                 objVendorCorporateInfo.TaskId,
                                 objVendorCorporateInfo.VendorCorporateName,
                                 objVendorCorporateInfo.ModeOfInvoiceReceipt,
                                 objVendorCorporateInfo.PaymentType,
                                 objVendorCorporateInfo.Comments,
                                 objVendorCorporateInfo.UserId,
                                 objVendorCorporateInfo.IsFromTask,
                                 objVendorCorporateInfo.SaveOrComplete
                );
            return res;
        }
        public DataSet GetVendorCorporateAddressInfo(VendorCorporateAddressInfoSearch objSearchVendorCorporateAddress)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetVendorCorporateAddressInfo.ToString(),
                objSearchVendorCorporateAddress.Action,
                objSearchVendorCorporateAddress.VendorCorporateId,
                objSearchVendorCorporateAddress.VendorCorporateAddressInfoId,
                objSearchVendorCorporateAddress.TaskId);
        }


        public DataSet ManageVendorCorporateAddressInfo(VendorCorporateAddressInfo objVendorCorporateAddressInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageVendorCorporateAddressInfo.ToString(),
                                                  objVendorCorporateAddressInfo.Action,
                                                  objVendorCorporateAddressInfo.VendorCorporateId,
                                                  objVendorCorporateAddressInfo.VendorCorporateAddressInfoId,
                                                  objVendorCorporateAddressInfo.TaskId,
                                                  objVendorCorporateAddressInfo.PhysicalAddress1,
                                                  objVendorCorporateAddressInfo.PhysicalAddress2,
                                                  objVendorCorporateAddressInfo.PhysicalCity,
                                                  objVendorCorporateAddressInfo.PhysicalState,
                                                  objVendorCorporateAddressInfo.PhysicalZip,
                                                  objVendorCorporateAddressInfo.PhysicalCountry,
                                                  objVendorCorporateAddressInfo.PhysicalPhone,
                                                  objVendorCorporateAddressInfo.PhysicalPhoneExtension,
                                                  objVendorCorporateAddressInfo.PhysicalFax,
                                                  objVendorCorporateAddressInfo.PhysicalEmail,
                                                  objVendorCorporateAddressInfo.BillingAddress1,
                                                  objVendorCorporateAddressInfo.BillingAddress2,
                                                  objVendorCorporateAddressInfo.BillingCity,
                                                  objVendorCorporateAddressInfo.BillingState,
                                                  objVendorCorporateAddressInfo.BillingZip,
                                                  objVendorCorporateAddressInfo.BillingCountry,
                                                  objVendorCorporateAddressInfo.BillingPhone,
                                                  objVendorCorporateAddressInfo.BillingPhoneExtension,
                                                  objVendorCorporateAddressInfo.BillingEmail,
                                                  objVendorCorporateAddressInfo.BillingFax,
                                                  objVendorCorporateAddressInfo.Comments,
                                                  objVendorCorporateAddressInfo.UserId,
                                                  objVendorCorporateAddressInfo.IsFromTask,
                                                  objVendorCorporateAddressInfo.SaveOrComplete);
            return res;
        }

        public DataSet VectorGetVendorContactInfoDL(VendorContactInfoSearch objVendorContactInfoSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetVendorContactInfo.ToString(),
                objVendorContactInfoSearch.Action,
                objVendorContactInfoSearch.VendorId,
                objVendorContactInfoSearch.vendorContactInfoId,
                objVendorContactInfoSearch.TaskId);
        }

        public DataSet VectorManageVendorContactInfoDL(VendorContactInfo objVendorContactInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageVendorContactInfo.ToString(),
                                                objVendorContactInfo.Action,
                                                  objVendorContactInfo.VendorId,
                                                  objVendorContactInfo.TaskId,
                                                  objVendorContactInfo.ContactsXml,
                                                  objVendorContactInfo.Comments, objVendorContactInfo.UserId,
                                                  objVendorContactInfo.IsFromTask, objVendorContactInfo.SaveOrComplete
                                                  );
            return res;
        }
        public DataSet GetVendorAddressInfo(VendorAddressInfoSearch objSearchVendorAddress)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetVendorAddressInfo.ToString(),
                objSearchVendorAddress.Action,
                objSearchVendorAddress.VendorId,
                objSearchVendorAddress.VendorAddressInfoId,
                objSearchVendorAddress.TaskId);
        }


        public DataSet ManageVendorAddressInfo(VendorAddressInfo objVendorAddressInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageVendorAddressInfo.ToString(),
                                                  objVendorAddressInfo.Action,
                                                  objVendorAddressInfo.VendorId,
                                                  objVendorAddressInfo.VendorAddressInfoId,
                                                  objVendorAddressInfo.TaskId,
                                                  objVendorAddressInfo.PhysicalAddress1,
                                                  objVendorAddressInfo.PhysicalAddress2,
                                                  objVendorAddressInfo.PhysicalCity,
                                                  objVendorAddressInfo.PhysicalState,
                                                  objVendorAddressInfo.PhysicalZip,
                                                  objVendorAddressInfo.PhysicalCountry,
                                                  objVendorAddressInfo.PhysicalPhone,
                                                  objVendorAddressInfo.PhysicalPhoneExtension,
                                                  objVendorAddressInfo.PhysicalFax,
                                                  objVendorAddressInfo.PhysicalEmail,
                                                  objVendorAddressInfo.BillingAddress1,
                                                  objVendorAddressInfo.BillingAddress2,
                                                  objVendorAddressInfo.BillingCity,
                                                  objVendorAddressInfo.BillingState,
                                                  objVendorAddressInfo.BillingZip,
                                                  objVendorAddressInfo.BillingCountry,
                                                  objVendorAddressInfo.BillingPhone,
                                                  objVendorAddressInfo.BillingPhoneExtension,
                                                  objVendorAddressInfo.BillingEmail,
                                                  objVendorAddressInfo.BillingFax,
                                                  objVendorAddressInfo.Comments,
                                                  objVendorAddressInfo.UserId,
                                                  objVendorAddressInfo.IsFromTask,
                                                  objVendorAddressInfo.SaveOrComplete);
            return res;
        }
        public DataSet GetVendorPaymentInfo(VendorPaymentInfoSearch objSearchVendorPayment)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetVendorPaymentInfo.ToString(),
                objSearchVendorPayment.Action,
                objSearchVendorPayment.VendorId,
                objSearchVendorPayment.VendorPaymentInfoId,
                objSearchVendorPayment.TaskId);
        }


        public DataSet ManageVendorPaymentInfo(VendorPaymentInfo objVendorPaymentInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageVendorPaymentInfo.ToString(),
                                                  objVendorPaymentInfo.Action,
                                                  objVendorPaymentInfo.VendorId,
                                                  objVendorPaymentInfo.VendorPaymentInfoId,
                                                  objVendorPaymentInfo.PaymentType,
                                                  objVendorPaymentInfo.TaskId,
                                                  objVendorPaymentInfo.AchXml,
                                                  objVendorPaymentInfo.PCardXml,
                                                  objVendorPaymentInfo.CheckXml,
                                                  objVendorPaymentInfo.Comments,
                                                  objVendorPaymentInfo.UserId,
                                                  objVendorPaymentInfo.IsFromTask,
                                                  objVendorPaymentInfo.SaveOrComplete);
            return res;
        }

        public DataSet GetVendorCorporatePaymentInfo(VendorCorporatePaymentInfoSearch objSearchVendorCorporatePayment)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetVendorCorporatePaymentInfo.ToString(),
                objSearchVendorCorporatePayment.Action,
                objSearchVendorCorporatePayment.VendorCorporateId,
                objSearchVendorCorporatePayment.VendorCorporatePaymentInfoId,
                objSearchVendorCorporatePayment.TaskId);
        }


        public DataSet ManageVendorCorporatePaymentInfo(VendorCorporatePaymentInfo objVendorCorporatePaymentInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageVendorCorporatePaymentInfo.ToString(),
                                                  objVendorCorporatePaymentInfo.Action,
                                                  objVendorCorporatePaymentInfo.VendorCorporateId,
                                                  objVendorCorporatePaymentInfo.VendorCorporatePaymentInfoId,
                                                  objVendorCorporatePaymentInfo.PaymentType,
                                                  objVendorCorporatePaymentInfo.TaskId,
                                                  objVendorCorporatePaymentInfo.AchXml,
                                                  objVendorCorporatePaymentInfo.PCardXml,
                                                  objVendorCorporatePaymentInfo.CheckXml,
                                                  objVendorCorporatePaymentInfo.Comments,
                                                  objVendorCorporatePaymentInfo.UserId,
                                                  objVendorCorporatePaymentInfo.IsFromTask,
                                                  objVendorCorporatePaymentInfo.SaveOrComplete);
            return res;
        }

        public DataSet VectorGetViewVendorDL(Int64 vendorId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorViewVendor.ToString(),"Vendor", vendorId);
        }

        public DataSet GetVendorCorporateViewInfoDL(Int64 vendorId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorViewVendor.ToString(),"VendorCorporate", vendorId);
        }

        public DataSet GetVendorCorporates(SearchEntity objSearchEntity, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorSearchVendorCorporate.ToString(),
                        "VendorCorporate",
                       objSearchEntity.vendorCoporateName,
                       objSearchEntity.vendorCorporateId,
                       userId);
        }
        public DataSet GetSearchVendor(SearchEntity objSearchEntity, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorSearchVendors.ToString(),
                       objSearchEntity.vendorId,
                       objSearchEntity.vendorName,
                       objSearchEntity.vendorMailingCity,
                       objSearchEntity.vendorMailingState,
                       objSearchEntity.vendorMailingZip,
                       userId);
        }

        public DataSet GetVendorContactsForBaseline(Int64 vendorId,Int64? baselineVendorContactId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetVendorContactsForBaseline.ToString(), "VendorContactsForBaseline", vendorId, baselineVendorContactId);
        }

    }
}
