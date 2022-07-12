using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Vector.API.Handlers;
using Vector.Common.DataLayer;
using Vector.Garage.BusinessLayer;
using Vector.Garage.Entities;

namespace Vector.API.Controllers
{
    [RoutePrefix("api/account")]
    [VectorActionAutorizationFilter]
    public class AccountController : ApiController
    {
        [HttpPost]
        [Route("ManageAccountInfo")]

        public IHttpActionResult ManageAccountInfo(AccountInfo objAccountInfo)
        {
            using (var objAccountBL = new AccountBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objAccountBL.ManageAccountInfo(objAccountInfo));
            }
        }

        [HttpPost]
        [Route("GetAccountInfo")]

        public IHttpActionResult GetAccountInfo(AccountInfoSearch objAccountInfoSearch)
        {
            using (var objAccountBL = new AccountBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objAccountBL.GetAccountInfo(objAccountInfoSearch));
            }
        }



        [HttpPost]
        [Route("ManageAccountRemitInfo")]

        public IHttpActionResult ManageAccountRemitInfo(AccountRemitInfo objAccountRemitInfo)
        {
            using (var objAccountBL = new AccountBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objAccountBL.ManageAccountRemitInfo(objAccountRemitInfo));
            }
        }

        [HttpPost]
        [Route("GetAccountRemitInfo")]

        public IHttpActionResult GetAccountRemitInfo(AccountRemitInfoSearch objAccountRemitInfoSearch)
        {
            using (var objAccountBL = new AccountBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objAccountBL.GetAccountRemitInfo(objAccountRemitInfoSearch));
            }
        }


        [HttpPost]
        [Route("VectorSearchAccounts")]
        public IHttpActionResult VectorSearchAccounts(SearchEntity objSearchEntity)
        {
            using (var objAccountBL = new AccountBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objAccountBL.VectorSearchAccounts(objSearchEntity, VectorAPIContext.Current.UserId));
            }
        }


        [HttpPost]
        [Route("GetAccountLineitemInfo")]
        public IHttpActionResult GetAccountLineitemInfo(AccountLineItemInfo objSearchEntity)
        {
            using (var objAccountBL = new AccountBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objAccountBL.GetAccountLineitemInfo(objSearchEntity, VectorAPIContext.Current.UserId));
            }
        }


        [HttpPost]
        [Route("ManageAccountLineItem")]
        public IHttpActionResult ManageAccountLineItem(UpdateAccountLineitemInfo objSearchEntity)
        {
            using (var objAccountBL = new AccountBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objAccountBL.ManageAccountLineItem(objSearchEntity, VectorAPIContext.Current.UserId));
            }
        }


        [HttpGet]   
        [Route("GetAccountLineitems")]
        public IHttpActionResult GetAccountLineitems(string action,Int64 accountid)
        {
            using (var objAccountBL = new AccountBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objAccountBL.GetAccountLineitems(action, accountid, VectorAPIContext.Current.UserId));
            }
        }



        [HttpPost]
        [Route("GetAccountComments")]
        public IHttpActionResult GetAccountComments(AccountComments accountComment )
        {
            using (var objAccountBL = new AccountBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objAccountBL.GetAccountComments(accountComment, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("ArchiveAccountVerificationRecord")]
        public IHttpActionResult ArchiveAccountVerificationRecord(ArchiveAccountVericationRecord objArchiveAccountVericationRecord)
        {
            using (var objAccountBL = new AccountBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objAccountBL.ArchiveAccountVerificationRecord(objArchiveAccountVericationRecord, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("GetAccountListReport")]
        public IHttpActionResult GetAccountListReport(SearchEntity objEntity)
        {
            using (var objAccountBL = new AccountBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objAccountBL.GetAccountListReport(objEntity, VectorAPIContext.Current.UserId));
            }
        }


        [HttpPost]
        [Route("GetMasterDataTrueUpPanelData")]
        public IHttpActionResult GetMasterDataTrueUpPanelData(SearchEntity objSearchEntity)
        {
            using (var objAccountBL = new AccountBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objAccountBL.GetMasterDataTrueUpPanelData(objSearchEntity, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("ManageMasterTrueUpDataLineitem")]
        public IHttpActionResult ManageMasterTrueUpDataLineitem(UpdateAccountLineitemInfo objSearchEntity)
        {
            using (var objAccountBL = new AccountBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objAccountBL.ManageMasterTrueUpDataLineitem(objSearchEntity, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("VectorGetMasterDataTrueupAccountLineitemInfo")]
        public IHttpActionResult VectorGetMasterDataTrueupAccountLineitemInfo(AccountLineItemInfo objSearchEntity)
        {
            using (var objAccountBL = new AccountBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objAccountBL.VectorGetMasterDataTrueupAccountLineitemInfo(objSearchEntity, VectorAPIContext.Current.UserId));
            }
        }
    }
}
