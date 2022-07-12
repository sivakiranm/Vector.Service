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
using Vector.Master.BusinessLayer;
using Vector.UserManagement.ClientInfo;
using static Vector.Master.Entities.MasterData;

namespace Vector.API.Controllers
{
    [RoutePrefix("api/masterdata")]
    [VectorActionAutorizationFilter]
    public class MasterDataController : ApiController
    {

        [HttpPost]
        [Route("AutoCompleteExtender")]
        //[TokenAuthenticationFilter]
        public IHttpActionResult GetAutoCompleteExtenderInfo([FromBody] VactorMasterDataSearch objVactorMasterDataSearch)
        {
            using (var objMasterDataBL = new MasterDataBL(new VectorDataConnection()))
            {
                objVactorMasterDataSearch.UserId = VectorAPIContext.Current.UserId;
                return VectorResponseHandler.GetVectorResponse(objMasterDataBL.GetAutoCompleteExtenderInfo(objVactorMasterDataSearch,VectorAPIContext.Current.UserId));

            }
        }

        [HttpPost]
        [Route("GetMasterData")]
        //[TokenAuthenticationFilter]
        public IHttpActionResult GetMasterData([FromBody] VectorMasterData objVectorMasterData)
        {
            using (var objMasterDataBL = new MasterDataBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objMasterDataBL.GetVectorMasterData(objVectorMasterData, VectorAPIContext.Current.UserId));

            }
        }

        [HttpPost]
        [Route("GetInventoryData")]
        //[TokenAuthenticationFilter]
        public IHttpActionResult GetInventoryData([FromBody] VectorInventoryDataSearch objVectorInventoryDataSearch)
        {
            using (var objMasterDataBL = new MasterDataBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objMasterDataBL.GetInventoryData(objVectorInventoryDataSearch, VectorAPIContext.Current.UserId));

            }
        }

        [HttpPost]
        [Route("GetDocuments")]
        //[TokenAuthenticationFilter]
        public IHttpActionResult GetDocuments([FromBody] DocumentInfo objDocumentInfo)
        {
            using (var objMasterDataBL = new MasterDataBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objMasterDataBL.GetDocuments(objDocumentInfo, VectorAPIContext.Current.UserId));

            }
        }


        [HttpPost]
        [Route("ManageProcessUpdates")] 
        public IHttpActionResult ManageProcessUpdates([FromBody] ProcessUpdateEntity objProcessUpdateEntity)
        {
            using (var objMasterDataBL = new MasterDataBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objMasterDataBL.ManageProcessUpdates(objProcessUpdateEntity, VectorAPIContext.Current.UserId));

            }
        }

    }
}
