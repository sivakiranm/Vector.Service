using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vector.API.Handlers;
using Vector.Common.DataLayer;
using Vector.IntacctManager.BusinessLayer;
using Vector.IntacctManager.Entities;

namespace Vector.API.Controllers
{
    [RoutePrefix("api/intacct")]
    [VectorActionAutorizationFilter]
    public class IntacctController : ApiController
    {
        [HttpGet]
        [Route("Customer")]
        public IHttpActionResult GetCustomer(string fromDate, string toDate)
        {
            using (var objInvoiceBL = new IntacctManagerBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.GetReportDataByType("Customer", fromDate, toDate));
            }
        }

        [HttpGet]
        [Route("Property")]
        public IHttpActionResult GetProperty(string fromDate, string toDate, string syncType)
        {
            using (var objInvoiceBL = new IntacctManagerBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.GetReportDataByType("Property", fromDate, toDate, syncType));
            }
        }

        [HttpGet]
        [Route("Invoice")]
        public IHttpActionResult GetInvoice(string fromDate, string toDate,string type)
        {
            using (var objInvoiceBL = new IntacctManagerBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.GetReportDataByType("Invoice", fromDate, toDate, type));
            }
        }

        [HttpGet]
        [Route("UpdateReportDataByEntity")]
        public IHttpActionResult UpdateReportDataByEntity(string entityType, string customerKey, string qBID, string KeyNo, string intacctId)
        {
            using (var objInvoiceBL = new IntacctManagerBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.UpdateReportDataByType(entityType, customerKey, qBID, intacctId, KeyNo));
            }
        }

        [HttpGet]
        [Route("UpdateIntacctId")]
        public IHttpActionResult UpdateIntacctId(string intacctType, string entityId, string intacctId)
        {
            using (var objInvoiceBL = new IntacctManagerBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.UpdateIntacctSyncId(intacctType, entityId, intacctId));
            }
        }

        [HttpGet]
        [Route("UpdateInvoice")]
        public IHttpActionResult UpdateInvoice(string invoiceNumber, string qBID, string type)
        {
            using (var objInvoiceBL = new IntacctManagerBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.UpdateInvoice(invoiceNumber, qBID, type));
            }
        }

        [HttpGet]
        [Route("UpdateBillData")]
        public IHttpActionResult UpdateBillData(string invoiceNumber, string qBID, string type)
        {
            using (var objInvoiceBL = new IntacctManagerBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.UpdateBillData(invoiceNumber, qBID, type));
            }
        }

        [HttpPost]
        [Route("UpdateReceiptPayment")]
        public IHttpActionResult UpdateReceiptPayment(PaymentReceipt objPaymentReceipt)
        {
            using (var objInvoiceBL = new IntacctManagerBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.UpdateReceiptPayment(objPaymentReceipt.PaymentQBID, objPaymentReceipt.Comments, 
                    objPaymentReceipt.PaymentType, objPaymentReceipt.DateUpdated, objPaymentReceipt.TotalAmount,
                                             objPaymentReceipt.InvoiceDetailsXML, objPaymentReceipt.Id));
            }
        }

        [HttpPost]
        [Route("LogException")]
        public IHttpActionResult LogException(IntacctException objIntacctException)
        {
            using (var objInvoiceBL = new IntacctManagerBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.LogException(objIntacctException.CustomerKey, objIntacctException.Intacct, objIntacctException.EntityType, objIntacctException.ErrorDescription, objIntacctException.IntacctSession, objIntacctException.SystemIp, objIntacctException.ErrorType, objIntacctException.Xml, objIntacctException.UserId));
            }
        }
    }
}
