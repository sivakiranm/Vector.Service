using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Vector.API.Handlers;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Garage.BusinessLayer;
using Vector.Garage.Entities;
using Vector.UserManagement.ClientInfo;

namespace Vector.API.Controllers
{
    [RoutePrefix("api/vendor")]
    [VectorActionAutorizationFilter]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class VendorController : ApiController
    {
        [HttpPost]
        [Route("GetVendorInfo")]
        
        public IHttpActionResult GetVendorInfo(VendorInfoSearch objVendorInfoSearch)
        {
            // RequestFrom - Task/Garage
            using (var objVendorBL = new VendorBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objVendorBL.GetVendorInfo(objVendorInfoSearch, 6));
            }
        }

        [HttpPost]
        [Route("ManageVendorInfo")]
        
        public IHttpActionResult ManageVendorInfo([FromBody] VendorInfo objVendorInfo)
        {
            using (var objVendorBL = new VendorBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objVendorBL.ManageVendorInfo(objVendorInfo));

            }
        }

        [HttpPost]
        [Route("VectorGetVendorCoroporateContactInfo")]
        
        public IHttpActionResult VectorGetVendorCoroporateContactInfo(VendorCorporateContactInfoSearch objVendorCorporateContactInfoSearch)
        {
            using (var objVendorBL = new VendorBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objVendorBL.VectorGetVendorCoroporateContactInfoBL(objVendorCorporateContactInfoSearch));
            }
        }

        [HttpPost]
        [Route("VectorGetVendorContactInfo")]
        
        public IHttpActionResult VectorGetVendorContactInfo(VendorContactInfoSearch objVendorContactInfoSearch)
        {
            using (var objVendorBL = new VendorBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objVendorBL.VectorGetVendorContactInfoBL(objVendorContactInfoSearch));
            }
        }

        [HttpPost]
        [Route("VectorManageVendorContactInfo")]
        
        public IHttpActionResult VectorManageVendorContactInfo(VendorContactInfo objVendorContactInfo)
        {
            using (var objVendorBL = new VendorBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objVendorBL.VectorManageVendorContactInfoBL(objVendorContactInfo));
            }
        }

        [HttpPost]
        [Route("VectorManageVendorCoroporateContactInfo")]
        
        public IHttpActionResult VectorManageVendorCoroporateContactInfo(VendorCorporateContactInfo objVendorCorporateContactInfo)
        {
            using (var objVendorBL = new VendorBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objVendorBL.VectorManageVendorCoroporateContactInfoBL(objVendorCorporateContactInfo));
            }
        }

        [HttpGet]
        [Route("VectorGetVendorCorporateInfo")]
        
        public IHttpActionResult VectorGetVendorCorporateInfo(Int64 vendorCorporateId, Int64 taskId)
        {
            using (var objVendorBL = new VendorBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objVendorBL.VectorGetVendorCorporateInfoBL(vendorCorporateId, taskId));
            }
        }

        [HttpPost]
        [Route("VectorManageVendorCorporateInfo")]
        
        public IHttpActionResult VectorManageVendorCorporateInfo(VendorCorporateInfo objVendorCorporateInfo)
        {
            using (var objVendorBL = new VendorBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objVendorBL.VectorManageVendorCorporateInfoBL(objVendorCorporateInfo));
            }
        }
        [HttpPost]
        [Route("ManageVendorCorporateAddressInfo")]
        
        public IHttpActionResult ManageVendorCorporateAddressInfo([FromBody] VendorCorporateAddressInfo objVendorCorporateAddressInfo)
        {
            using (var objVendorBL = new VendorBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objVendorBL.ManageVendorCorporateAddressInfo(objVendorCorporateAddressInfo));

            }
        }

        [HttpPost]
        [Route("GetVendorCorporateAddressInfo")]
        
        public IHttpActionResult GetVendorCorporateAddressInfo(VendorCorporateAddressInfoSearch objSearchVendorCorporateAddress)
        {
            // RequestFrom - Task/Garage
            using (var objVendorBL = new VendorBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objVendorBL.GetVendorCorporateAddressInfo(objSearchVendorCorporateAddress));
            }
        }

        [HttpPost]
        [Route("ManageVendorAddressInfo")]
        
        public IHttpActionResult ManageVendorAddressInfo([FromBody] VendorAddressInfo objVendorAddressInfo)
        {
            using (var objVendorBL = new VendorBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objVendorBL.ManageVendorAddressInfo(objVendorAddressInfo));

            }
        }

        [HttpPost]
        [Route("GetVendorAddressInfo")]
        
        public IHttpActionResult GetVendorAddressInfo(VendorAddressInfoSearch objSearchVendorAddress)
        {
            // RequestFrom - Task/Garage
            using (var objVendorBL = new VendorBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objVendorBL.GetVendorAddressInfo(objSearchVendorAddress));
            }
        }

        [HttpPost]
        [Route("ManageVendorPaymentInfo")]
        
        public IHttpActionResult ManageVendorPaymentInfo([FromBody] VendorPaymentInfo objVendorPaymentInfo)
        {
            using (var objVendorBL = new VendorBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objVendorBL.ManageVendorPaymentInfo(objVendorPaymentInfo));

            }
        }

        [HttpPost]
        [Route("GetVendorPaymentInfo")]
        
        public IHttpActionResult GetVendorPaymentInfo(VendorPaymentInfoSearch objSearchVendorPayment)
        {
            // RequestFrom - Task/Garage
            using (var objVendorBL = new VendorBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objVendorBL.GetVendorPaymentInfo(objSearchVendorPayment));
            }
        }

        [HttpPost]
        [Route("ManageVendorCorporatePaymentInfo")]
        
        public IHttpActionResult ManageVendorCorporatePaymentInfo([FromBody] VendorCorporatePaymentInfo objVendorCorporatePaymentInfo)
        {
            using (var objVendorBL = new VendorBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objVendorBL.ManageVendorCorporatePaymentInfo(objVendorCorporatePaymentInfo));

            }
        }

        [HttpPost]
        [Route("GetVendorCorporatePaymentInfo")]
        
        public IHttpActionResult GetVendorCorporatePaymentInfo(VendorCorporatePaymentInfoSearch objSearchVendorCorporatePayment)
        {
            // RequestFrom - Task/Garage
            using (var objVendorBL = new VendorBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objVendorBL.GetVendorCorporatePaymentInfo(objSearchVendorCorporatePayment));
            }
        }

        [HttpGet]
        [Route("VectorGetViewVendor")]
        
        public IHttpActionResult VectorGetViewVendor(Int64 vendorId)
        {
            using (var objVendorBL = new VendorBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objVendorBL.VectorGetViewVendorBL(vendorId));
            }
        }

        [HttpGet]
        [Route("GetVendorCorporateInfo")]

        public IHttpActionResult GetVendorCorporateViewInfo(Int64 vendorId)
        {
            using (var objVendorBL = new VendorBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objVendorBL.GetVendorCorporateViewInfo(vendorId));
            }
        }

        [HttpPost]
        [Route("SearchVendorCorporate")]

        public IHttpActionResult GetVendorCorporates(SearchEntity objSearchEntity)
        {
            using (var objVendorBL = new VendorBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objVendorBL.GetVendorCorporates(objSearchEntity,VectorAPIContext.Current.UserId));
            }
        }



        [HttpPost]
        [Route("SearchVendor")]

        public IHttpActionResult GetSearchVendor(SearchEntity objSearchEntity)
        {
            using (var objVendorBL = new VendorBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objVendorBL.GetSearchVendor(objSearchEntity, VectorAPIContext.Current.UserId));
            }
        }

        [HttpGet]
        [Route("GetVendorContactsForBaseline")]

        public IHttpActionResult GetVendorContactsForBaseline(Int64 vendorId,Int64? baselineVendorContactId)
        {
            using (var objVendorBL = new VendorBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objVendorBL.GetVendorContactsForBaseline(vendorId, baselineVendorContactId));
            }
        }

    }
}
