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
using Vector.Workbench.BusinessLayer;
using Vector.Workbench.Entities;

namespace Vector.API.Controllers
{
    [RoutePrefix("api/workflow")]
    [VectorActionAutorizationFilter]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WorkFlowController : ApiController
    {
        #region "METHOD TO RETURN THE DATA IN THE FORM OF JSON OBJECT "

        [HttpGet]
        [Route("GetWorkFlows")]      
        public IHttpActionResult GetWorkFlows(string action, int flowId)
        {
            using (var workFlowBL = new WorkFlowBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(workFlowBL.GetWorkFLows(action, flowId));

            }
        }

        [HttpGet]
        [Route("GetManifests")]
        public IHttpActionResult GetManifests(string action, int manifestId)
        {
            using (var workFlowBL = new WorkFlowBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(workFlowBL.GetManifests(action, manifestId));

            }
        }

        #endregion

        #region GetTasks

        [HttpGet]
        [Route("GetTasks")]
        public IHttpActionResult GetTasks(int categoryId =0)
        {
            using (var workFlowBL = new WorkFlowBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(workFlowBL.GetTasks(categoryId));

            }
        }



        //[HttpGet]
        //[Route("GetTasks")]
        //public IHttpActionResult GetTasks()
        //{
        //    using (var workFlowBL = new WorkFlowBL(new VectorDataConnection()))
        //    {
        //        return VectorResponseHandler.GetVectorResponse(workFlowBL.GetTasks(categoryId));

        //    }
        //}

        #endregion

        #region GetEvents

        [HttpGet]
        [Route("GetEvents")]
        public IHttpActionResult GetEvents()
        {
            using (var workFlowBL = new WorkFlowBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(workFlowBL.GetEvents());

            }
        }

        #endregion

        #region Create Work FLow

        [HttpPost]
        [Route("AddWorkFlow")]
        public IHttpActionResult AddWorkFlow([FromBody]WorkFlowData workFlowData)
        {
            using (var workFlowBL = new WorkFlowBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(workFlowBL.AddWorkFlow(workFlowData));

            }
        }

        #endregion
        #region Modify Work FLow

        [HttpPost]
        [Route("UpdateWorkFlow")]
        public IHttpActionResult UpdateWorkFlow([FromBody] WorkFlowData workFlowData)
        {
            using (var workFlowBL = new WorkFlowBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(workFlowBL.UpdateWorkFlow(workFlowData));

            }
        }

        #endregion
        #region Create Work Manifest

        [HttpPost]
        [Route("AddWorkManifest")]
        public IHttpActionResult AddManifest([FromBody]WorkManifestData workManifestData)
        {
            using (var workFlowBL = new WorkFlowBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(workFlowBL.AddWorkManifest(workManifestData));
            }
        }

        #endregion
        #region Modify Work Manifest

        [HttpPost]
        [Route("UpdateWorkManifest")]
        public IHttpActionResult UpdateWorkManifest([FromBody]WorkManifestData workManifestData)
        {
            using (var workFlowBL = new WorkFlowBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(workFlowBL.UpdateWorkManifest(workManifestData));
            }
        }

        #endregion
    }
}
