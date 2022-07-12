using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vector.API.Handlers;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Garage.BusinessLayer;
using Vector.Garage.Entities;
using Vector.UserManagement.ClientInfo;

namespace Vector.API.Controllers
{
    [RoutePrefix("api/property")]
    [VectorActionAutorizationFilter]

    public class PropertyController : ApiController
    {
        [HttpPost]
        [Route("ManagePropertyInfo")]
        
        public IHttpActionResult ManagePropertyInfo([FromBody] PropertyInfo objPropertyInfo)
        {
            using (var objPropertyBL = new PropertyBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPropertyBL.ManagePropertyInfo(objPropertyInfo, 1));

            }
        }

        //[HttpPost]
        //[Route("UploadPropertyDocuments")]
        //public IHttpActionResult UploadPropertyDocuments([FromForm] FileModel files)
        //{
        //    try
        //    {
        //        string path = Path.Combine(Directory.GetCurrentDirectory(), "Files/Property/" + files.TypeName + "/");
        //        bool isFolderExists = Directory.Exists(path);
        //        if (!isFolderExists)
        //            Directory.CreateDirectory(path);
        //        foreach (var file in files.FormFiles)
        //        {
        //            using (Stream stream = new FileStream((path + file.FileName), FileMode.Create))
        //            {
        //                file.CopyTo(stream);
        //            }
        //        }
        //        return VectorResponseHandler.GetVectorResponse(new VectorResponse<string>() { ResponseData = "File Uploaded Successfully" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return VectorResponseHandler.GetVectorResponse(new VectorResponse<string>() { Error = new Error() { ErrorDescription = ex.Message } });
        //    }
        //}

        [HttpPost]
        [Route("GetPropertyInfo")]
        
        public IHttpActionResult GetPropertyInfo(PropertyInfoSearch objPropertyInfoSearch)
        {
            // RequestFrom - Task/Garage
            using (var objPropertyBL = new PropertyBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPropertyBL.GetPropertyInfo(objPropertyInfoSearch, 6));
            }
        }

        [HttpPost]
        [Route("ManagePropertyAddressInfo")]
        
        public IHttpActionResult ManagePropertyAddressInfo([FromBody] PropertyAddressInfo objPropertyAddressInfo)
        {
            using (var objPropertyBL = new PropertyBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPropertyBL.ManagePropertyAddressInfo(objPropertyAddressInfo, 1));

            }
        }

        [HttpPost]
        [Route("GetPropertyAddressInfo")]
        
        public IHttpActionResult GetPropertyAddressInfo(PropertyAddressInfoSearch objPropertyAddressInfoSearch)
        {
            // RequestFrom - Task/Garage
            using (var objPropertyBL = new PropertyBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPropertyBL.GetPropertyAddressInfo(objPropertyAddressInfoSearch, 6));
            }
        }

        [HttpPost]
        [Route("VectorGetPropertyInvoicePreferences")]
        
        public IHttpActionResult VectorGetPropertyInvoicePreferences(PropertyInvoicePreferenceSearch objPropertyInvoicePreferenceSearch)
        {
            using (var objPropertyBL = new PropertyBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPropertyBL.VectorGetPropertyInvoicePreferencesBL(objPropertyInvoicePreferenceSearch));
            }
        }

        [HttpPost]
        [Route("VectorManagePropertyInvoicePreferences")]
        
        public IHttpActionResult VectorManagePropertyInvoicePreferensces(PropertyInvoicePreferences objPropertyInvoicePreferences)
        {
            using (var objPropertyBL = new PropertyBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPropertyBL.VectorManagePropertyInvoicePreferencesBL(objPropertyInvoicePreferences));
            }
        }

        [HttpPost]
        [Route("VectorGetPropertyContactInfo")]
        
        public IHttpActionResult VectorGetPropertyContactInfo(PropertyContactInfoSearch objPropertyContactInfoSearch)
        {
            using (var objPropertyBL = new PropertyBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPropertyBL.VectorGetPropertyContactInfoBL(objPropertyContactInfoSearch));
            }
        }

        [HttpPost]
        [Route("VectorManagePropertyContactInfo")]
        
        public IHttpActionResult VectorManagePropertyContactInfo(PropertyContactInfo objPropertyContactInfo)
        {
            using (var objPropertyBL = new PropertyBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPropertyBL.VectorManagePropertyContactInfoBL(objPropertyContactInfo));
            }
        }


        [HttpPost]
        [Route("VectorManagePropertyMiscInfo")]
        
        public IHttpActionResult VectorManagePropertyMiscInfo(PropertyMiscInfo objPropertyMiscInfo)
        {
            using (var objPropertyBL = new PropertyBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPropertyBL.VectorManagePropertyMiscInfoBL(objPropertyMiscInfo));
            }
        }

        [HttpPost]
        [Route("GetPropertyMiscInfo")]
        
        public IHttpActionResult GetPropertyMiscInfo(PropertyMiscInfoSearch propertyInfoSearch)
        {
            using (var objPropertyBL = new PropertyBL(new VectorDataConnection()))
            {
                // ToDO : Modify the controller to get from task i.e form intermediate table instead direct from client;
                return VectorResponseHandler.GetVectorResponse(objPropertyBL.GetPropertyMiscInfo(propertyInfoSearch));
            }
        }
        [HttpPost]
        [Route("GetPropertyContractInfo")]
        
        public IHttpActionResult GetPropertyContractInfo(PropertyContractInfoSearch propertyContractInfoSearch)
        {
            using (var objPropertyBL = new PropertyBL(new VectorDataConnection()))

                return VectorResponseHandler.GetVectorResponse(objPropertyBL.GetPropertyContractInfo(propertyContractInfoSearch));
        }
        [HttpPost]
        [Route("VectorManagePropertyContractInfo")]
        
        public IHttpActionResult VectorManagePropertyContractInfo(PropertyContractInfo propertyContractInfo)
        {
            using (var objPropertyBL = new PropertyBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPropertyBL.VectorManagePropertyContractInfoBL(propertyContractInfo));
            }
        }
        [HttpGet]
        [Route("GetVectorViewPropertyInformation")]
        public IHttpActionResult GetVectorViewPropertyInformation(string action, Int64 propertyId)
        {
            using (var objPropertyBL = new PropertyBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPropertyBL.GetVectorViewPropertyInformation(action, propertyId));
            }
        }

        [HttpGet]
        [Route("GetAllVectorViewPropertyInformation")]
        public IHttpActionResult GetAllVectorViewPropertyInformation(Int64 UserId)
        {
            using (var objPropertyBL = new PropertyBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPropertyBL.GetAllVectorViewPropertyInformation(UserId));
            }
        }

        [HttpPost]
        [Route("VectorGetPropertySearch")] 
        public IHttpActionResult VectorGetPropertySearch(PropertySearch objPropertyInfo)
        {
            using (var objPropertyBL = new PropertyBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPropertyBL.VectorGetPropertySearchBL(objPropertyInfo,VectorAPIContext.Current.UserId));
            }
        }


        [HttpPost]
        [Route("VectorGetPropertyBaselineInvoices")]
        public IHttpActionResult VectorGetPropertyBaselineInvoices(PropertyBaselineinvoices objPropertyInfo)
        {
            using (var objPropertyBL = new PropertyBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPropertyBL.VectorGetPropertyBaselineInvoices(objPropertyInfo,VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("ManageBaselineInvoiceInfo")]
        public IHttpActionResult ManageBaselineInvoiceInfo(BaselineInvoiceInfo objPropertyInfo)
        {
            using (var objPropertyBL = new PropertyBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPropertyBL.ManageBaselineInvoiceInfo(objPropertyInfo, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("UploadPropertyBaselineInvoices")]
        public IHttpActionResult UploadPropertyBaselineInvoices(PropertyBaselineDocuments objPropertyInfo)
        {
            using (var objPropertyBL = new PropertyBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPropertyBL.UploadPropertyBaselineInvoices(objPropertyInfo, VectorAPIContext.Current.UserId));
            }
        }





        [HttpGet]
        [Route("GetPropertyBaselines")]
        public IHttpActionResult GetPropertyBaselines(string action,Int64 propertyId)
        {
            using (var objPropertyBL = new PropertyBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPropertyBL.GetPropertyInfo("PropertyBaselines", action,propertyId,VectorAPIContext.Current.UserId));
            }
        }


        [HttpGet]
        [Route("GetPropertyNegotiations")]
        public IHttpActionResult GetPropertyNegotiations(string action, Int64 propertyId)
        {
            using (var objPropertyBL = new PropertyBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPropertyBL.GetPropertyInfo("PropertyNegotiations", action, propertyId, VectorAPIContext.Current.UserId));
            }
        }


        [HttpGet]
        [Route("GetPropertyContracts")]
        public IHttpActionResult GetPropertyContracts(string action, Int64 propertyId)
        {
            using (var objPropertyBL = new PropertyBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPropertyBL.GetPropertyInfo("PropertyContracts", action, propertyId, VectorAPIContext.Current.UserId));
            }
        }


        [HttpGet]
        [Route("GetPropertyAccounts")]
        public IHttpActionResult GetPropertyAccounts(string action, Int64 propertyId)
        {
            using (var objPropertyBL = new PropertyBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPropertyBL.GetPropertyInfo("PropertyAccounts", action, propertyId, VectorAPIContext.Current.UserId));
            }
        }



        [HttpGet]
        [Route("GetPropertyDocuments")]
        public IHttpActionResult GetPropertyDocuments(string action, Int64 propertyId)
        {
            using (var objPropertyBL = new PropertyBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPropertyBL.GetPropertyInfo("PropertyDocuments", action, propertyId, VectorAPIContext.Current.UserId));
            }
        }

        [HttpGet]
        [Route("GetPropertyContacts")]
        public IHttpActionResult GetPropertyContacts(string action, Int64 propertyId)
        {
            using (var objPropertyBL = new PropertyBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPropertyBL.GetPropertyInfo("PropertyContacts", action, propertyId, VectorAPIContext.Current.UserId));
            }
        }
    }
}
