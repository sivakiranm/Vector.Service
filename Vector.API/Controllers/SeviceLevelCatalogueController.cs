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
    [RoutePrefix("api/sevicelevelcatalogue")]
    [VectorActionAutorizationFilter]

    public class SeviceLevelCatalogueController : ApiController
    {
        [HttpPost]
        [Route("GetServiceLevelCatalogue")]

        public IHttpActionResult GetServiceLevelCatalogue(ServiceLevelCatalogueSearch objServiceLevelCatalogueSearch)
        {
            using (var objServiceLevelCatalogueBL = new ServiceLevelCatalogueBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objServiceLevelCatalogueBL.GetServiceLevelCatalogue(objServiceLevelCatalogueSearch));
            }
        }

        [HttpPost]
        [Route("ManageServiceLevelCatalogue")]

        public IHttpActionResult ManageServiceLevelCatalogue([FromBody] ServiceLevelCatalogue objServiceLevelCatalogue)
        {
            using (var objServiceLevelCatalogueBL = new ServiceLevelCatalogueBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objServiceLevelCatalogueBL.ManageServiceLevelCatalogue(objServiceLevelCatalogue));

            }
        }

        [HttpPost]
        [Route("ManageServiceRequestInfo")]
        public IHttpActionResult ManageServiceRequestInfo([FromBody] ServiceRequest objServiceRequest)
        {
            using (var objServiceLevelCatalogueBL = new ServiceLevelCatalogueBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objServiceLevelCatalogueBL.ManageServiceRequestInfo(objServiceRequest));

            }
        }

        [HttpPost]
        [Route("VectorGetServiceRequestInfo")]

        public IHttpActionResult VectorGetServiceRequestInfo(VectorServiceRequestSearch objVectorServiceRequestSearch)
        {
            using (var objServiceLevelCatalogueBL = new ServiceLevelCatalogueBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objServiceLevelCatalogueBL.VectorGetServiceRequestInfoBL(objVectorServiceRequestSearch));
            }
        }


    }
}
