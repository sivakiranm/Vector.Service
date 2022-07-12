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

namespace Vector.API.Controllers
{
    [RoutePrefix("api/ownershiptransfer")]
    [VectorActionAutorizationFilter]
    public class OwnershipTransferController : ApiController
    {
        [HttpPost]
        [Route("VectorManageInitiateOwnerShipTransferInfo")]

        public IHttpActionResult VectorManageInitiateOwnerShipTransferInfo(OwnerShipTransferEntity objInitiateOwnershipTransfer)
        {
            using (var objOwnershipTransferBL = new OwnershipTransferBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objOwnershipTransferBL.VectorManageInitiateOwnerShipTransferInfo(objInitiateOwnershipTransfer, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("GetOwnershipTransfers")]

        public IHttpActionResult GetOwnershipTransfers(string Action)
        {
            using (var objOwnershipTransferBL = new OwnershipTransferBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objOwnershipTransferBL.GetOwnershipTransfers(Action));
            }
        }

        [HttpPost]
        [Route("VectorManageOwnershipTransferClientApproval")]

        public IHttpActionResult VectorManageOwnershipTransferClientApproval(OTClientApprovalRequest objOTClientApprovalRequest)
        {
            using (var objOwnershipTransferBL = new OwnershipTransferBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objOwnershipTransferBL.VectorManageOwnershipTransferClientApproval(objOTClientApprovalRequest));
            }
        }

        [HttpPost]
        [Route("VectorManageOTApproveClientApproval")]

        public IHttpActionResult VectorManageOTApproveClientApproval(OTClientApprovalRequest objOTClientApprovalRequest)
        {
            using (var objOwnershipTransferBL = new OwnershipTransferBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objOwnershipTransferBL.VectorManageOTApproveClientApproval(objOTClientApprovalRequest));
            }
        }

        [HttpPost]
        [Route("VectorManageOwnershipTransferLogEmail")]

        public IHttpActionResult VectorManageOwnershipTransferLogEmail(OwnershipTransferLogEmail objOwnershipTransferLogEmail)
        {
            using (var objOwnershipTransferBL = new OwnershipTransferBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objOwnershipTransferBL.VectorManageOwnershipTransferLogEmail(objOwnershipTransferLogEmail));
            }
        }
    }
}
