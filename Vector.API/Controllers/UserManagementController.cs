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
using Vector.UserManagement.BusinessLayer;
using Vector.UserManagement.ClientInfo;
using Vector.UserManagement.Entities;

namespace Vector.API.Controllers
{
    [RoutePrefix("api/usermanagement")]
    [VectorActionAutorizationFilter]

    public class UserManagementController : ApiController
    {
        [HttpPost]
        [Route("VectorGetUserManagementInfo")]

        public IHttpActionResult VectorGetUserManagementInfo(VectorGetUserManagementInfo objVectorGetUserManagementInfo)
        {
            using (var objUserManageBL = new UserManagementBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objUserManageBL.VectorGetUserManagementInfoBl(objVectorGetUserManagementInfo));
            }
        }

        [HttpPost]
        [Route("GetVactorMasterData")]

        public IHttpActionResult GetVactorMasterData(VactorMasterData objVactorMasterData)
        {
            using (var objUserManageBL = new UserManagementBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objUserManageBL.GetVactorMasterDataBL(objVactorMasterData));
            }
        }

        [HttpPost]
        [Route("VectorManageUserInfo")]

        public IHttpActionResult VectorManageUserInfo(VectorMangerUserInfo objVectorMangerUserInfo)
        {
            using (var objUserManageBL = new UserManagementBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objUserManageBL.VectorManageUserInfoBL(objVectorMangerUserInfo));
            }
        }

        [HttpGet]
        [Route("VectorGetToolsFeaturesInfo")]

        public IHttpActionResult VectorGetToolsFeaturesInfo(int personaId)
        {
            using (var objUserManageBL = new UserManagementBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objUserManageBL.VectorGetToolsFeaturesInfoBL(personaId));
            }
        }

        [HttpPost]
        [Route("VectorManageWorkManifestInfo")]

        public IHttpActionResult VectorManageWorkManifestInfo(VectorManageWorkManifestInfo objVectorManageWorkManifestInfo)
        {
            using (var objUserManageBL = new UserManagementBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objUserManageBL.VectorManageWorkManifestInfoBL(objVectorManageWorkManifestInfo));
            }
        }

        [HttpPost]
        [Route("VectorManagePersonaFeatures")]

        public IHttpActionResult VectorManagePersonaFeatures(VectorManagePersonaFeatures objVectorManagePersonaFeatures)
        {
            using (var objUserManageBL = new UserManagementBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objUserManageBL.VectorManagePersonaFeaturesBL(objVectorManagePersonaFeatures));
            }
        }


        [HttpPost]
        [Route("VectorGetWorkManifestInfo")]

        public IHttpActionResult VectorGetWorkManifestInfo(VectorGetWorkManifestInfo objVectorGetWorkManifestInfo)
        {
            using (var objUserManageBL = new UserManagementBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objUserManageBL.VectorGetWorkManifestInfoBL(objVectorGetWorkManifestInfo));
            }
        }

        [HttpGet]
        [Route("GetUserAssigendFeatures")]

        public IHttpActionResult GetUserAssigendFeatures(Int64 userId, string menuType)
        {
            using (var objUserManageBL = new UserManagementBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objUserManageBL.GetUserAssigendFeatures(userId, menuType));
            }
        }

        [HttpGet]
        [Route("GetUserProfileInfo")]
        public IHttpActionResult GetUserProfileInfo(Int64 userId)
        {
            using (var objUserManageBL = new UserManagementBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objUserManageBL.GetUserProfileInfoBL(userId));
            }
        }

        [HttpPost]
        [Route("ManageUserProfileInfo")]
        public IHttpActionResult ManageUserProfileInfoBL(VectorMangerUserInfo objVectorMangerUserInfo)
        {
            using (var objUserManageBL = new UserManagementBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objUserManageBL.VectorManageUserProfileInfoBL(objVectorMangerUserInfo));
            }
        }


        [HttpGet]
        [Route("GetResources")]
        public IHttpActionResult GetResources(string action)
        {
            using (var objUserManageBL = new UserManagementBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objUserManageBL.GetResources(action, VectorAPIContext.Current.UserId));
            }
        }


        [HttpPost]
        [Route("ManageResources")]
        public IHttpActionResult ManageResources(ManageResources objManageResources)
        {
            using (var objUserManageBL = new UserManagementBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objUserManageBL.ManageResources(objManageResources, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("LogFeatureNavigations")]
        public IHttpActionResult LogFeatureNavigations(UserFeatureNavigationLogDetail objUserFeatureLogDetail, string action)
        {
            using (var objUserManageBL = new UserManagementBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objUserManageBL.LogFeatureNavigations(objUserFeatureLogDetail, VectorAPIContext.Current.LoginId, action));
            }
        }

        [HttpPost]
        [Route("LogUserDetails")]
        public IHttpActionResult LogUserDetails(UserLogDetail objUserLogDetail, string action)
        {
            using (var objUserManageBL = new UserManagementBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objUserManageBL.LogUserDetails(objUserLogDetail, action, VectorAPIContext.Current.LoginId));
            }
        }
    }
}
