using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vector.API.Handlers;
using Vector.Common.DataLayer;
using Vector.Garage.BusinessLayer;
using Vector.Garage.Entities;

namespace Vector.API.Controllers
{
    [RoutePrefix("api/ConsolidatedInvoice")]
    [VectorActionAutorizationFilter]
    public class ConsolidatedInvoiceController : ApiController
    {
        [HttpPost]
        [Route("Create")]
        public IHttpActionResult CreateConsolidatedInvoice(ConsolidatedInvoiceInfo objConsolidatedInvoice)
        {
            using (var objConsolidatedInvoiceBL = new ConsolidatedInvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objConsolidatedInvoiceBL.CreateConsolidatedInvoice(objConsolidatedInvoice));
            }
        }


        [HttpPost]
        [Route("GetConsolidatedInvoices")]
        public IHttpActionResult GetConsolidatedInvoices(ConsolidatedInvoiceSearch objConsolidatedInvoice)
        {
            using (var objConsolidatedInvoiceBL = new ConsolidatedInvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objConsolidatedInvoiceBL.ConsolidatedInvoiceSearch(objConsolidatedInvoice));
            }
        }

        [HttpPost]
        [Route("ApproveFundCI")]
        public IHttpActionResult ApproveFundCI(FundApproveCI objFundApproveCI)
        {
            using (var objConsolidatedInvoiceBL = new ConsolidatedInvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objConsolidatedInvoiceBL.ApproveFundCI(objFundApproveCI));
            }
        }


        [HttpPost]
        [Route("ReporcessInvoice")]
        public IHttpActionResult ReporcessInvoice(FundApproveCI objInvoiceInfo)
        {
            using (var objConsolidatedInvoiceBL = new ConsolidatedInvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objConsolidatedInvoiceBL.ReporcessInvoice(objInvoiceInfo));
            }
        }


        [HttpPost]
        [Route("RejectCITransactions")]
        public IHttpActionResult RejectCITransactions(RejectCITransactions objInvoiceInfo)
        {
            using (var objConsolidatedInvoiceBL = new ConsolidatedInvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objConsolidatedInvoiceBL.RejectCITransactions(objInvoiceInfo));
            }
        }

        [HttpPost]
        [Route("ManagePartialCITransactions")]
        public IHttpActionResult PartiallyFundCITransactions(PartiallyFundCITransactions objInvoiceInfo)
        {
            using (var objConsolidatedInvoiceBL = new ConsolidatedInvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objConsolidatedInvoiceBL.PartiallyFundCITransactions(objInvoiceInfo));
            }
        }



    }
}
