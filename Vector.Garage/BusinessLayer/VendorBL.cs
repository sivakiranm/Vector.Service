using System;
using System.Linq;
using System.Xml.Linq;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Garage.DataLayer;
using Vector.Garage.Entities;

namespace Vector.Garage.BusinessLayer
{
    public class VendorBL : DisposeLogic
    {
        private VectorDataConnection objVectorDB;
        public VendorBL(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }

        public VectorResponse<object> GetVendorInfo(VendorInfoSearch objVendorInfoSearch, Int64 userId)
        {
            using (var objVendorDL = new VendorDL(objVectorDB))
            {
                var result = objVendorDL.GetVendorInfo(objVendorInfoSearch, userId);
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


        public VectorResponse<object> ManageVendorInfo(VendorInfo objVendorInfo)
        {
            using (var objVendorDL = new VendorDL(objVectorDB))
            {
                //XDocument contractDocs = new XDocument(
                //new XElement("ROOT",
                //from vendorDoc in objVendorInfo.VendorDocuments
                //select new XElement("Contacts",
                //            new XElement("ContractName", ""),
                //            new XElement("FileName", vendorDoc.FileName),
                //            new XElement("FilePath", vendorDoc.FilePath),
                //            new XElement("ApproveStatus", "Approved")
                //)));
                var result = objVendorDL.ManageVendorInfo(objVendorInfo, string.Empty);
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



        public VectorResponse<object> VectorGetVendorCoroporateContactInfoBL(VendorCorporateContactInfoSearch objVendorCorporateContactInfoSearch)
        {
            using (var objVendorDL = new VendorDL(objVectorDB))
            {
                var result = objVendorDL.VectorGetVendorCoroporateContactInfoDL(objVendorCorporateContactInfoSearch);
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

        public VectorResponse<object> VectorManageVendorCoroporateContactInfoBL(VendorCorporateContactInfo objVendorCorporateContactInfo)
        {
            using (var objVendorDL = new VendorDL(objVectorDB))
            {
                var result = objVendorDL.VectorManageVendorCoroporateContactInfoDL(objVendorCorporateContactInfo);
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

        public VectorResponse<object> VectorGetVendorContactInfoBL(VendorContactInfoSearch objVendorContactInfoSearch)
        {
            using (var objVendorDL = new VendorDL(objVectorDB))
            {
                var result = objVendorDL.VectorGetVendorContactInfoDL(objVendorContactInfoSearch);
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

        public VectorResponse<object> VectorManageVendorContactInfoBL(VendorContactInfo objVendorContactInfo)
        {
            using (var objVendorDL = new VendorDL(objVectorDB))
            {
                var result = objVendorDL.VectorManageVendorContactInfoDL(objVendorContactInfo);
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

        public VectorResponse<object> VectorGetVendorCorporateInfoBL(Int64 vendorCorporateId, Int64 taskId)
        {
            using (var objVendorDL = new VendorDL(objVectorDB))
            {
                var result = objVendorDL.VectorGetVendorCorporateInfoDL(vendorCorporateId,  taskId);
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

        public VectorResponse<object> VectorManageVendorCorporateInfoBL(VendorCorporateInfo objVendorCorporateInfo)
        {
            using (var objVendorDL = new VendorDL(objVectorDB))
            {
                var result = objVendorDL.VectorManageVendorCorporateInfoDL(objVendorCorporateInfo);
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

        public VectorResponse<object> GetVendorCorporateAddressInfo(VendorCorporateAddressInfoSearch objSearchVendorCorporateAddress)
        {
            using (var objVendorDL = new VendorDL(objVectorDB))
            {
                var result = objVendorDL.GetVendorCorporateAddressInfo(objSearchVendorCorporateAddress);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "VendorCorporateAddressInfo";
                    result.Tables[1].TableName = "ActivityLog";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> ManageVendorCorporateAddressInfo(VendorCorporateAddressInfo objVendorCorporateAddressInfo)
        {
            using (var objVendorDL = new VendorDL(objVectorDB))
            {
                var result = objVendorDL.ManageVendorCorporateAddressInfo(objVendorCorporateAddressInfo);
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
        public VectorResponse<object> ManageVendorAddressInfo(VendorAddressInfo objVendorAddressInfo)
        {
            using (var objVendorDL = new VendorDL(objVectorDB))
            {
                var result = objVendorDL.ManageVendorAddressInfo(objVendorAddressInfo);
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
        public VectorResponse<object> GetVendorAddressInfo(VendorAddressInfoSearch objSearchVendorAddress)
        {
            using (var objVendorDL = new VendorDL(objVectorDB))
            {
                var result = objVendorDL.GetVendorAddressInfo(objSearchVendorAddress);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "VendorAddressInfo";
                    result.Tables[1].TableName = "ActivityLog";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }
        public VectorResponse<object> ManageVendorPaymentInfo(VendorPaymentInfo objVendorPaymentInfo)
        {
            using (var objVendorDL = new VendorDL(objVectorDB))
            {
                var result = objVendorDL.ManageVendorPaymentInfo(objVendorPaymentInfo);
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
        public VectorResponse<object> GetVendorPaymentInfo(VendorPaymentInfoSearch objSearchVendorPayment)
        {
            using (var objVendorDL = new VendorDL(objVectorDB))
            {
                var result = objVendorDL.GetVendorPaymentInfo(objSearchVendorPayment);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "paymentTypeInfo";
                    result.Tables[1].TableName = "achPaymentInfo";
                    result.Tables[2].TableName = "pcardPaymentInfo";
                    result.Tables[3].TableName = "checkPaymentInfo";
                    result.Tables[4].TableName = "ActivityLog";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }
        public VectorResponse<object> ManageVendorCorporatePaymentInfo(VendorCorporatePaymentInfo objVendorCorporatePaymentInfo)
        {
            using (var objVendorDL = new VendorDL(objVectorDB))
            {
                var result = objVendorDL.ManageVendorCorporatePaymentInfo(objVendorCorporatePaymentInfo);
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
        public VectorResponse<object> GetVendorCorporatePaymentInfo(VendorCorporatePaymentInfoSearch objSearchVendorCorporatePayment)
        {
            using (var objVendorDL = new VendorDL(objVectorDB))
            {
                var result = objVendorDL.GetVendorCorporatePaymentInfo(objSearchVendorCorporatePayment);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "paymentTypeInfo";
                    result.Tables[1].TableName = "achPaymentInfo";
                    result.Tables[2].TableName = "pcardPaymentInfo";
                    result.Tables[3].TableName = "checkPaymentInfo";
                    result.Tables[4].TableName = "ActivityLog";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> VectorGetViewVendorBL(Int64 vendorId)
        {
            using (var objVendorDL = new VendorDL(objVectorDB))
            {
                var result = objVendorDL.VectorGetViewVendorDL(vendorId);
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
        public VectorResponse<object> GetVendorCorporateViewInfo(Int64 vendorId)
        {
            using (var objVendorDL = new VendorDL(objVectorDB))
            {
                var result = objVendorDL.GetVendorCorporateViewInfoDL(vendorId);
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


        public VectorResponse<object> GetVendorCorporates(SearchEntity objSearchEntity,Int64 userId)
        {
            using (var objVendorDL = new VendorDL(objVectorDB))
            {
                var result = objVendorDL.GetVendorCorporates(objSearchEntity, userId);
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

        public VectorResponse<object> GetSearchVendor(SearchEntity objSearchEntity, Int64 userId)
        {
            using (var objVendorDL = new VendorDL(objVectorDB))
            {
                var result = objVendorDL.GetSearchVendor(objSearchEntity, userId);
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

        public VectorResponse<object> GetVendorContactsForBaseline(Int64 vendorId,Int64? baselineVendorContactId)
        {
            using (var objVendorDL = new VendorDL(objVectorDB))
            {
                var result = objVendorDL.GetVendorContactsForBaseline(vendorId, baselineVendorContactId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[VectorConstants.Zero].TableName = "vendorContacts";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }
    }
}
