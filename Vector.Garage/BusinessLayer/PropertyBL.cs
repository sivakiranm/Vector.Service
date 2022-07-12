using System;
using System.Data;
using System.Linq;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Garage.DataLayer;
using Vector.Garage.Entities;

namespace Vector.Garage.BusinessLayer
{
    public class PropertyBL : DisposeLogic
    {
        private VectorDataConnection objVectorDB;
        public PropertyBL(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }

        public VectorResponse<object> ManagePropertyInfo(PropertyInfo objPropertyInfo, Int64 userId)
        {
            using (var objPropertyDL = new PropertyDL(objVectorDB))
            {
                //XDocument contractDocs = new XDocument(
                //     new XElement("ROOT",
                //     from clientDoc in objPropertyInfo.PropertyDocuments
                //     select new XElement("ClientContractDocs",
                //                 //new XElement("ClientDocumentId", clientDoc.ClientDocumentId),
                //                 //new XElement("ClientContractInfoDocumentsID", clientDoc.ClientContractInfoDocumentsID),
                //                 new XElement("ClientId", clientDoc.ClientId),
                //                 new XElement("DocumentName", clientDoc.DocumentName),
                //                 new XElement("DocumentPath", clientDoc.DocumentPath),
                //                 new XElement("Version", clientDoc.Version),
                //                 new XElement("ChooseDocumentType", clientDoc.ChooseDocumentType),
                //                 new XElement("Status", clientDoc.Status)
                //     )));
                //var splitTransferDate = objPropertyInfo.TransferDate.Split('/');
                //string finalTransferDate = splitTransferDate[2] + "-" + splitTransferDate[0] + "-" + splitTransferDate[1];
                //objPropertyInfo.TransferDate = finalTransferDate;
                var result = objPropertyDL.ManagePropertyInfo(objPropertyInfo);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    if(objPropertyInfo.PropertyDocuments != null && objPropertyInfo.PropertyDocuments.Count > 0)
                    {
                        string PropertyNo = objPropertyInfo.PropertyNo;

                        if(string.IsNullOrEmpty(PropertyNo))
                        {
                            if(result.Tables.Count > 0 && result.Tables[0].Columns.Contains("PropertyNo"))
                            PropertyNo = (from c in result.Tables[0].AsEnumerable()
                                          select c.Field<string>("PropertyNo")).ToList().FirstOrDefault();
                        }

                        if(!string.IsNullOrEmpty(PropertyNo))
                        {
                           string tempFolderName = SecurityManager.GetConfigValue("FileServerTempPath") + objPropertyInfo.tempFolderName + "\\";
                           string parentFolderName = SecurityManager.GetConfigValue("FileServerPath") + "\\" + "Property" + "\\" + PropertyNo + "\\";


                            bool resultFiles = FileManager.MoveFiles(tempFolderName, parentFolderName, isDeleteFodler: false);
                        }
                    }
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> GetPropertyInfo(PropertyInfoSearch objPropertyInfoSearch, Int64 userId)
        {
            using (var objPropertyDL = new PropertyDL(objVectorDB))
            {
                var result = objPropertyDL.GetPropertyInfo(objPropertyInfoSearch, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "propertyInfo";
                    result.Tables[1].TableName = "propertyDocuments";
                    result.Tables[2].TableName = "ActivityLog";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> GetPropertyAddressInfo(PropertyAddressInfoSearch objPropertyAddressInfoSearch, Int64 userId)
        {
            using (var objPropertyDL = new PropertyDL(objVectorDB))
            {
                var result = objPropertyDL.GetPropertyAddressInfo(objPropertyAddressInfoSearch, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "PropertyAddressInfo";
                    result.Tables[1].TableName = "ActivityLog";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> ManagePropertyAddressInfo(PropertyAddressInfo objPropertyAddressInfo, Int64 userId)
        {
            using (var objPropertyDL = new PropertyDL(objVectorDB))
            {
                var result = objPropertyDL.ManagePropertyAddressInfo(objPropertyAddressInfo, userId);
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

        public VectorResponse<object> VectorGetPropertyInvoicePreferencesBL(PropertyInvoicePreferenceSearch objPropertyInvoicePreferenceSearch)
        {
            using (var objPropertyBL = new PropertyDL(objVectorDB))
            {
                var result = objPropertyBL.VectorGetPropertyInvoicePreferencesDL(objPropertyInvoicePreferenceSearch);
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

        public VectorResponse<object> VectorManagePropertyInvoicePreferencesBL(PropertyInvoicePreferences objPropertyInvoicePreferences)
        {
            using (var objPropertyBL = new PropertyDL(objVectorDB))
            {
                var result = objPropertyBL.VectorManagePropertyInvoicePreferencesDL(objPropertyInvoicePreferences);
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

        public VectorResponse<object> VectorGetPropertyContactInfoBL(PropertyContactInfoSearch objPropertyContactInfoSearch)
        {
            using (var objPropertyBL = new PropertyDL(objVectorDB))
            {
                var result = objPropertyBL.VectorGetPropertyContactInfoDL(objPropertyContactInfoSearch);
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

        public VectorResponse<object> VectorManagePropertyContactInfoBL(PropertyContactInfo objPropertyContactInfo)
        {
            using (var objPropertyBL = new PropertyDL(objVectorDB))
            {
                var result = objPropertyBL.VectorManagePropertyContactInfoDL(objPropertyContactInfo);
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

        public VectorResponse<object> VectorManagePropertyMiscInfoBL(PropertyMiscInfo objPropertyMiscInfo)
        {
            using (var objPropertyBL = new PropertyDL(objVectorDB))
            {
                var result = objPropertyBL.VectorManagePropertyMiscInfoDL(objPropertyMiscInfo);
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

        public VectorResponse<object> GetPropertyMiscInfo(PropertyMiscInfoSearch propertyInfoSearch)
        {
            using (var objPropertyDL = new PropertyDL(objVectorDB))
            {
                var result = objPropertyDL.GetPropertyMiscInfo(propertyInfoSearch);
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
        public VectorResponse<object> GetPropertyContractInfo(PropertyContractInfoSearch propertyContractInfoSearch)
        {
            using (var objPropertyDL = new PropertyDL(objVectorDB))
            {
                var result = objPropertyDL.GetPropertyContractInfo(propertyContractInfoSearch);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "PropertyContractInfo";
                    result.Tables[1].TableName = "ActivityLog";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }
        public VectorResponse<object> VectorManagePropertyContractInfoBL(PropertyContractInfo propertyContractInfo)
        {
            using (var objPropertyDL = new PropertyDL(objVectorDB))
            {
                var result = objPropertyDL.VectorManagePropertyContractInfoDL(propertyContractInfo);
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
        public VectorResponse<object> GetVectorViewPropertyInformation(string action, Int64 propertyId)
        {
            using (var objPropertyDL = new PropertyDL(objVectorDB)) 
            {
                var result = objPropertyDL.GetVectorViewPropertyInformation(action, propertyId);
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

        public VectorResponse<object> GetAllVectorViewPropertyInformation(Int64 UserId)
        {
            using (var objPropertyDL = new PropertyDL(objVectorDB))
            {
                var result = objPropertyDL.GetAllVectorViewPropertyInformation(UserId);
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

        public VectorResponse<object> VectorGetPropertySearchBL(PropertySearch objPropertyInfo,Int64 UserId)
        {
            using (var objPropertyDL = new PropertyDL(objVectorDB))
            {
                var result = objPropertyDL.VectorGetPropertySearchDL(objPropertyInfo, UserId);
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

        public VectorResponse<object> VectorGetPropertyBaselineInvoices(PropertyBaselineinvoices objPropertyInfo,Int64 userId)
        {
            using (var objPropertyDL = new PropertyDL(objVectorDB))
            {
                var result = objPropertyDL.VectorGetPropertyBaselineInvoices(objPropertyInfo, userId);
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

        public VectorResponse<object> ManageBaselineInvoiceInfo(BaselineInvoiceInfo objPropertyInfo, Int64 userId)
        {
            using (var objPropertyDL = new PropertyDL(objVectorDB))
            {
                var result = objPropertyDL.ManageBaselineInvoiceInfo(objPropertyInfo, userId);
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

        public VectorResponse<object> UploadPropertyBaselineInvoices(PropertyBaselineDocuments objPropertyInfo, Int64 userId)
        {
            using (var objPropertyDL = new PropertyDL(objVectorDB))
            {
                var result = objPropertyDL.UploadPropertyBaselineInvoices(objPropertyInfo, userId);
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



        public VectorResponse<object> GetPropertyInfo(string type,string action,Int64 propertyId, Int64 UserId)
        {
            using (var objPropertyDL = new PropertyDL(objVectorDB))
            { 
                var result = objPropertyDL.GetPropertyInfo(type,action,propertyId, UserId);
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
    }
}
