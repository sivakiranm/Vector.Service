using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Vector.API.Handlers;
using Vector.Common.DataLayer;
using Vector.Garage.Entities;
using Vector.Workbench.BusinessLayer;
using Vector.Workbench.Entities;

namespace Vector.API.Controllers
{
    [RoutePrefix("api/Widgets")]
    [VectorActionAutorizationFilter]

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WidgetController : ApiController
    {

        [HttpPost]
        [Route("ClientSummaryInfo")]
        public IHttpActionResult ClientSummaryInfo(ClientSummaryInfo objClientSummaryInfo)
        {

            using (var objWidgetsBL = new WidgetsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objWidgetsBL.ClientSummaryInfo(objClientSummaryInfo, VectorAPIContext.Current.UserId));

            }


        }

        [HttpGet]
        [Route("ClientMetrics")]
        public IHttpActionResult ClientMetrics(Int64? clientId)
        {

            using (var objWidgetsBL = new WidgetsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objWidgetsBL.ClientMetrics(clientId, VectorAPIContext.Current.UserId));

            }


        }

        [HttpGet]
        [Route("ServiceSummary")]
        public IHttpActionResult ServiceSummary(Int64? propertyId,string propertyName)
        {

            using (var objWidgetsBL = new WidgetsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objWidgetsBL.ServiceSummary(propertyId, propertyName, VectorAPIContext.Current.UserId));

            }


        }


        [HttpPost]
        [Route("InvoiceExplorer")]
        public IHttpActionResult GetInvoiceExplorer(SearchInvoice objSearch)
        {
            using (var objWidgetsBL = new WidgetsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objWidgetsBL.GetInvoiceExplorerBL(objSearch,VectorAPIContext.Current.UserId));
            }
        }

        [HttpGet]
        [Route("TicketByServiceType")]
        public IHttpActionResult TicketByServiceType(Int64? propertyId)
        {

            using (var objWidgetsBL = new WidgetsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objWidgetsBL.TicketByServiceType(propertyId,VectorAPIContext.Current.UserId));

            }


        }

        [HttpGet]
        [Route("VectorOpenInvoiceByClient")]
        public IHttpActionResult VectorOpenInvoiceByClient(Int64? clientId)
        {

            using (var objWidgetsBL = new WidgetsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objWidgetsBL.VectorOpenInvoiceByClient(clientId, VectorAPIContext.Current.UserId));

            }


        }


        [HttpPost]
        [Route("ManageActions")]
        public IHttpActionResult ManageActions(Actions objActions)
        {
            using (var objWidgetsBL = new WidgetsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objWidgetsBL.ManageActions(objActions, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("GetActions")]
        public IHttpActionResult GetActions(Actions objActions)
        {
            using (var objWidgetsBL = new WidgetsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objWidgetsBL.GetActions(objActions, VectorAPIContext.Current.UserId));
            }
        }



        [HttpPost]
        [Route("GetNegotiationthreesixtyReport")]
        public IHttpActionResult GetNegotiationthreesixtyReport(SearchEntity objSearch)
        {

            using (var objWidgetsBL = new WidgetsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objWidgetsBL.GetNegotiationthreesixtyReport(objSearch, VectorAPIContext.Current.UserId));

            }


        }

        [HttpPost]
        [Route("GetNegotaitionStatusWidgetDetails")]
        public IHttpActionResult GetNegotaitionStatusWidgetDetails(SearchEntity objSearch)
        {

            using (var objWidgetsBL = new WidgetsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objWidgetsBL.GetNegotaitionStatusWidgetDetails(objSearch, VectorAPIContext.Current.UserId));

            }


        }


        [HttpPost]
        [Route("UpdatePersonalizedSettings")]
        public IHttpActionResult UpdatePersonalizedSettings(PersonalizeSettings objSearch)
        {

            using (var objWidgetsBL = new WidgetsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objWidgetsBL.UpdatePersonalizedSettings(objSearch, VectorAPIContext.Current.UserId));

            }


        }



        [HttpPost]
        [Route("GetWorkTrackerInfo")]
        public IHttpActionResult GetWorkTrackerInfo(WorkEntity objSearch)
        {
            using (var objWidgetsBL = new WidgetsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objWidgetsBL.GetWorkTrackerInfo(objSearch, VectorAPIContext.Current.UserId));

            }
        }


        [HttpPost]
        [Route("ManageWorkTracker")]
        public IHttpActionResult ManageWorkTracker(WorkEntity objSearch)
        {
            using (var objWidgetsBL = new WidgetsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objWidgetsBL.ManageWorkTracker(objSearch, VectorAPIContext.Current.UserId));

            }
        }

        [HttpGet]
        [Route("GetProductivityMetrics")]
        public IHttpActionResult GetProductivityMetrics(string action,string reportBy,Int64? reporterId,Int64? clientId)
        {

            using (var objWidgetsBL = new WidgetsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objWidgetsBL.GetProductivityMetrics(action, reportBy, reporterId,clientId, VectorAPIContext.Current.UserId));

            }


        }


    }


}
