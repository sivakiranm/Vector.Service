using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using Vector.API.Handlers;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Garage.BusinessLayer;
using Vector.Garage.Entities;
using Vector.UserManagement.ClientInfo;

namespace Vector.API.Controllers
{
    [RoutePrefix("api/negotiations")]
    [VectorActionAutorizationFilter]

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class NegotiationsController : ApiController
    {

        [HttpPost]
        [Route("VectorManageNegotiationsInfo")]

        public IHttpActionResult VectorManageNegotiationsInfo(Negotiations objNegotiations)
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.VectorManageNegotiationsBL(objNegotiations));
            }
        }

        [HttpPost]
        [Route("GetNegotiationsInfo")]

        public IHttpActionResult GetNegotiationsInfo(NegotiationSearch objNegotiationSearch)
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                // ToDO : Modify the controller to get from task i.e form intermediate table instead direct from client;
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.GetNegotiationsInfoBL(objNegotiationSearch));
            }
        }

        [HttpPost]
        [Route("GetNegotiations")]

        public IHttpActionResult GetNegotiations(NegotiationSearch objNegotiationSearch)
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.GetNegotiations(objNegotiationSearch));
            }
        }


        [HttpPost]
        [Route("VectorGetNegotiationsBidSheetInfo")]

        public IHttpActionResult VectorGetNegotiationsBidSheetInfo(NegotiationsBidSheetInfo objNegotiationsBidSheetInfo)
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.VectorGetNegotiationsBidSheetInfoBL(objNegotiationsBidSheetInfo));
            }
        }

        [HttpPost]
        [Route("GetDraftNegotiations")]
        //[TokenAuthenticationFilter]
        public IHttpActionResult GetDraftNegotiations(NegotiationSearch objNegotiationSearch)
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.GetDraftNegotiationsBL(objNegotiationSearch));
            }
        }

        /// <summary>
        /// Get Negotiation Baseline Details
        /// @Action = GetBaseLineforNegotiation & GetBaseLineItemsforNegotiation
        /// </summary>
        /// <param name="objNegotiationSearch"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetNegotiationBaselineDetails")]
        public IHttpActionResult GetNegotiationBaselineDetails(NegotiationSearch objNegotiationSearch)
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.GetNegotiationBaselineDetails(objNegotiationSearch));
            }
        }

        /// <summary>
        /// Vector Add or Update Negotiation Bid Values
        /// </summary>
        /// <param name="objNegotiationsBidValues"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUpdateNegotiationBidValues")]
        public IHttpActionResult VectorAddUpdateNegotiationBidValues(NegotiationsBidValues objNegotiationsBidValues)
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.VectorAddUpdateNegotiationBidValues(objNegotiationsBidValues));
            }
        }

        [HttpPost]
        [Route("VectorManageDraftNegotiations")]
        public IHttpActionResult VectorManageDraftNegotiations(DraftNegotiations objDraftNegotiations)
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.VectorManageDraftNegotiationsBL(objDraftNegotiations));
            }
        }

        [HttpPost]
        [Route("VectorManageNegotiationBidSheetInfo")]
        public IHttpActionResult VectorManageNegotiationBidSheetInfo(NegotiationBidSheetInfo objNegotiationBidSheetInfo)
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.VectorManageNegotiationBidSheetInfoBL(objNegotiationBidSheetInfo));
            }
        }

        [HttpPost]
        [Route("UpdateNegotiationLineItem")]
        public IHttpActionResult UpdateNegotiationLineItem(NegotiationLineItemData objNegotiationLineItem)
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.UpdateNegotiationLineItem(objNegotiationLineItem));
            }
        }

        [HttpPost]
        [Route("ManageBaselineNegotiationLineItems")]
        public IHttpActionResult ManageBaselineNegotiationLineItems(BaselineNegotiationLineItems objBaselineNegotiationLineItems)
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.VectorAddUpdateBaselineNegotiationLineItemsBL(objBaselineNegotiationLineItems));
            }
        }

        [HttpGet]
        [Route("RequestBidEmail")]
        public IHttpActionResult SendRequestBidEmail(string userId, string negotiationId = "", string vendorId = "")
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.SendRequestBidEmail(userId, negotiationId, vendorId));
            }
        }

        [HttpPost]
        [Route("RequestBidEmail")]
        public IHttpActionResult SendRequestBidEmail(SendEmailNegotiation objSendEmail)
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.SendRequestBidEmail(objSendEmail, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("VectorManagePreBidRequest")]
        public IHttpActionResult VectorManagePreBidRequest(DraftNegotiations objDraftNegotiations)
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.VectorManagePreBidRequest(objDraftNegotiations));
            }
        }

        [HttpGet]
        [Route("NegotiatonRequestBidMails")]
        public IHttpActionResult GetNegotiatonRequestBidMails(string negotiationId = "")
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.GetNegotiatonRequestBidMails(negotiationId));
            }
        }

        [HttpPost]
        [Route("SendEmail")]
        public IHttpActionResult SendEmail(NegotiationSendEmail objSendEmail)
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.GetNegotiatonSendEmail(objSendEmail));
            }
        }

        [HttpGet]
        [Route("DownloadNegotiationEmailAttachments")]
        public IHttpActionResult DownloadNegotiationEmailAttachments(string uniqueId, string tempFilesFolderName)
        {
            tempFilesFolderName = SecurityManager.GetConfigValue("FileServerTempPath") + tempFilesFolderName + "\\";
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.DownloadNegotiationEmailAttachments(uniqueId, tempFilesFolderName));
            }
        }

        [HttpGet]
        [Route("DownloadAttachments")]
        public object DownloadAttachments(string fileName, string tempFilesFolderName)
        {
            tempFilesFolderName = SecurityManager.GetConfigValue("FileServerTempPath") + tempFilesFolderName + "\\";
            string filePath = tempFilesFolderName;
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            byte[] fileData = File.ReadAllBytes(Path.Combine(filePath, fileName));
            if (fileData == null)
                return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to download attachments.Please contact Administrator." } };

            response.Content = new ByteArrayContent(fileData);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
            response.Content.Headers.Add("fileName", fileName);
            var disposition = new ContentDispositionHeaderValue("attachment");
            disposition.FileName = fileName;
            response.Content.Headers.ContentDisposition = disposition;

            return response;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("NegotiationRevisitEmail")]
        public IHttpActionResult SendNegotiationRevisitEmail()
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.SendNegotiationRevisitEmail());
            }
        }

        [HttpGet]
        [Route("VectorServiceManager")]
        [AllowAnonymous]
        public IHttpActionResult VectorServiceManager()
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.VectorServiceManager());
            }
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("GetNegotiationBaselineLineitems")]
        public IHttpActionResult GetNegotiationBaselineLineitems(string type,Int64 negotiationId)
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.GetNegotiationBaselineLineitems(type, negotiationId,VectorAPIContext.Current.UserId));
            }
        }


        [HttpPost]
        [Route("GetNegotiationVendorEmailDetails")]

        public IHttpActionResult GetNegotiationVendorEmailDetails(SendEmailNegotiation objContractSearch)
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.GetNegotiationVendorEmailDetails(objContractSearch,VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("CalculateBeginAndEndDate")]

        public IHttpActionResult CalculateBeginAndEndDates(DateEntity objDateEntity)
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.CalculateBeginAndEndDates(objDateEntity));
            }
        }

        [HttpPost]
        [Route("ManageNegotiationDocuments")]
        public IHttpActionResult ManageNegotiationDocuments(NegotiationDocuments onjNegotiationDocuments)
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.ManageNegotiationDocuments(onjNegotiationDocuments,VectorAPIContext.Current.UserId));
            }
        }


        [HttpGet]
        [Route("GetNegotiationDocuments")]
        public IHttpActionResult GetNegotiationDocuments(Int64 negotiationId)
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.GetNegotiationDocuments(negotiationId, VectorAPIContext.Current.UserId));
            }
        }


        [HttpPost]
        [Route("UpdateNegotiationLineitemInfo")]
        public IHttpActionResult UpdateNegotiationLineitem(NegotiationLineitemUpdate ObjNegotiationLineitem)
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.UpdateNegotiationLineitem(ObjNegotiationLineitem, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("GetNegotiation360ReportStatusInfo")]
        public IHttpActionResult GetNegotiation360ReportStatusInfo(NegotiationSearch objNegotiationsSearch)
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.GetNegotiation360ReportStatusInfo(objNegotiationsSearch, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("ManageBidEmailService")]
        public IHttpActionResult ManageBidEmailService(BidServiceInfo objBidServiceInfo)
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.ManageBidEmailService(objBidServiceInfo, VectorAPIContext.Current.UserId));
            }
        }

        [HttpGet]
        [Route("GetNegotiaitons")]
        public IHttpActionResult GetNegotiaitons(string action, string propertyId)
        {
            using (var objBaselineBL = new BaselineBL(new VectorDataConnection()))
            {
                // ToDO : Modify the controller to get from task i.e form intermediate table instead direct from client;
                return VectorResponseHandler.GetVectorResponse(objBaselineBL.GetNegotiations(action, propertyId, VectorAPIContext.Current.UserId));
            }
        }



        [HttpGet]
        [Route("ManageNegotiaionLineitemState")]
        public IHttpActionResult ManageNegotiaionLineitemState(string action,Int64 baselineLineitemId,Int64 negotiaitonId,Int64 negotiationLineitemId,Int64 accountId,Int64 accountlineitemid)
        {
            using (var objBaselineBL = new NegotiationsBL(new VectorDataConnection()))
            {
                // ToDO : Modify the controller to get from task i.e form intermediate table instead direct from client;
                return VectorResponseHandler.GetVectorResponse(objBaselineBL.ManageNegotiaionLineitemState(action, baselineLineitemId, negotiaitonId, negotiationLineitemId, accountId,accountlineitemid, VectorAPIContext.Current.UserId));
            }
        }



        [HttpGet]
        [Route("GetNegotiationVendorLineitems")]
        public IHttpActionResult GetNegotiationVendorLineitems(string action, Int64 negotiaitonId, Int64 vendorId)
        {
            using (var objBaselineBL = new NegotiationsBL(new VectorDataConnection()))
            {
                // ToDO : Modify the controller to get from task i.e form intermediate table instead direct from client;
                return VectorResponseHandler.GetVectorResponse(objBaselineBL.GetNegotiationVendorLineitems(action, negotiaitonId, vendorId, VectorAPIContext.Current.UserId));
            }
        }


        [HttpGet]
        [Route("GetLowestBidInfo")]
        public IHttpActionResult GetLowestBidInfo(string action, Int64 negotiationLineitemId)
        {
            using (var objBaselineBL = new NegotiationsBL(new VectorDataConnection()))
            {
                // ToDO : Modify the controller to get from task i.e form intermediate table instead direct from client;
                return VectorResponseHandler.GetVectorResponse(objBaselineBL.GetLowestBidInfo(action, negotiationLineitemId, VectorAPIContext.Current.UserId));
            }
        }

        [HttpGet]
        [Route("GetAwardedVendors")]
        public IHttpActionResult GetAwardedVendors(string action, Int64 Id,Int16 fromRange,Int16 toRange)
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.GetAwardedVendors(action,Id, VectorAPIContext.Current.UserId, fromRange, toRange));
            }
        }


        [HttpPost]
        [Route("NegotiationCloneLineItems")]
        public IHttpActionResult NegotiationCloneLineItems(CloneLineitems objCloneLineitems)
        {
            using (var objNegotiationsBL = new NegotiationsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objNegotiationsBL.NegotiationCloneLineItems(objCloneLineitems, VectorAPIContext.Current.UserId));
            }
        }




    }
}
