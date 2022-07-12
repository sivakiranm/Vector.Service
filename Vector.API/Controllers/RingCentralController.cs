using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vector.API.Handlers;
using Vector.Common.DataLayer;
using Vector.Workbench.BusinessLayer;

namespace Vector.API.Controllers
{
    [RoutePrefix("api/ringcentral")]
    [VectorActionAutorizationFilter]
    public class RingCentralController : ApiController
    {

        [HttpGet]
        [Route("GetRingCentralInfo")]
        public IHttpActionResult GetRingCentralInfo()
        {
            using (var objRingCentralBL = new RingCentralBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objRingCentralBL.GetRingCentralDetails(VectorAPIContext.Current.UserId));
            }
        }


    }
}
