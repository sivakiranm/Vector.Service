using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Vector.API.Handlers;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Garage.BusinessLayer;
using Vector.Garage.Entities;

namespace Vector.API.Controllers
{
    [RoutePrefix("api/invoice")]
    [VectorActionAutorizationFilter]
    public class InvoiceController : ApiController
    {
        [HttpPost]
        [Route("GetBatchInfo")] 
        public IHttpActionResult GetBatchInfo(BatchInfoSearch objBatchInfo)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.GetBatchInfo(objBatchInfo));
            }
        }

        [HttpPost]
        [Route("ManageBatchInfo")]
        public IHttpActionResult ManageBatchInfo(BatchInfo objBatchInfo)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.ManageBatchInfo(objBatchInfo));
            }
        }

        [HttpPost]
        [Route("MangeFinalizeBatchInfo")]
        public IHttpActionResult MangeFinalizeBatchInfo(BatchInfo objBatchInfo)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.MangeFinalizeBatchInfo(objBatchInfo));
            }
        }

        [HttpPost]
        [Route("GetImagesForEntry")]
        public IHttpActionResult GetInvoiceHeaderInfoSearch(InvoiceHeaderInfoSearch objInvoiceHeaderInfoSearch)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.GetInvoiceHeaderInfoSearch(objInvoiceHeaderInfoSearch));
            }
        }
        [HttpPost]
        [Route("MangeBatchUploadDocumentsInfo")]
        public IHttpActionResult MangeBatchUploadDocumentsInfo(BatchUploadDocumentsInfo objBatchUploadDocumentsInfo)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.MangeBatchUploadDocumentsInfo(objBatchUploadDocumentsInfo));
            }
        }

        [HttpPost]
        [Route("ManageInvoiceHeaderInfo")]
        public IHttpActionResult ManageInvoiceHeaderInfo(InvoiceHeaderInfo objInvoiceHeaderInfo)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.ManageInvoiceHeaderInfo(objInvoiceHeaderInfo));
            }
        }

        [HttpPost]
        [Route("VectorGetInvoicesForProcessing")]
        public IHttpActionResult VectorGetInvoicesForProcessing(InvoiceLineItemInfoSearch objInvoiceLineItemInfoSearch)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.VectorGetInvoicesForProcessing(objInvoiceLineItemInfoSearch));
            }
        }

        [HttpPost]
        [Route("VectorGetInvoicesForAudit")]
        public IHttpActionResult VectorGetInvoicesForAudit(InvoiceLineItemInfoSearch objInvoiceLineItemInfoSearch)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.VectorGetInvoicesForAudit(objInvoiceLineItemInfoSearch));
            }
        }

        [HttpPost]
        [Route("GetInvoiceDetails")]
        public IHttpActionResult GetInvoiceDetails(InvoiceLineItemInfoSearch objInvoiceLineItemInfoSearch)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.GetInvoiceDetails(objInvoiceLineItemInfoSearch,VectorAPIContext.Current.UserId));
            }
        }


        [HttpPost]
        [Route("ManageInvoiceLineItemsInfo")]
        public IHttpActionResult ManageInvoiceLineItemsInfo(InvoiceHeaderInfo objInvoiceHeaderInfo)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.ManageInvoiceLineItemsInfo(objInvoiceHeaderInfo, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("GetInvoicesForVerify")]
        public IHttpActionResult GetInvoicesForVerify(InvoiceLineItemInfoSearch objInvoiceLineItemInfoSearch)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.GetInvoicesForVerify(objInvoiceLineItemInfoSearch));
            }
        }

        [HttpPost]
        [Route("GenerateInvoice")]
        public IHttpActionResult GenerateInvoice(InvoiceInfo objInvoiceInfo)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                string FileServerPath = SecurityManager.GetConfigValue("FileServerPath");
                string folderPath = FileServerPath+ "//InvoiceTemplates//";
                string batchFolderPath = FileServerPath+"//BatchInvoices//";
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.GenerateInvoice(objInvoiceInfo, folderPath, batchFolderPath));
            }
        }

        [HttpPost]
        [Route("GetExceptionInfo")]
        public IHttpActionResult GetExceptionInfo(SearchInfo objSearchInfo)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            { 
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.GetExceptionInfo(objSearchInfo));
            }
        }

        [HttpPost]
        [Route("ManageExceptionInfo")]
        public IHttpActionResult ManageExceptionInfo(InvoiceHeaderInfo objInvoiceHeaderInfo)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.ManageExceptionInfo(objInvoiceHeaderInfo));
            }
        }


        [HttpPost]
        [Route("VectorGetInvoiceLookup")]
        public IHttpActionResult VectorGetInvoiceLookup(SearchEntity objInvoiceEntity)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.VectorGetInvoiceLookup(objInvoiceEntity,VectorAPIContext.Current.UserId));
            }
        }


        [HttpPost]
        [Route("GetInvoiceForDispatch")]
        public IHttpActionResult GetInvoiceForDispatch(SearchEntity objSearchInvoice)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.GetInvoiceForDispatch(objSearchInvoice,VectorAPIContext.Current.UserId));
            }
        }


        [HttpPost]
        [Route("GetInvoiceVerification")]
        public IHttpActionResult GetInvoiceVerification(SearchEntity objSearchInvoice)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.GetInvoiceVerification(objSearchInvoice, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("GetInvoiceVerificationDetails")]
        public IHttpActionResult GetInvoiceVerificationDetails(SearchEntity objSearchInvoice)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.GetInvoiceVerificationDetails(objSearchInvoice, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("InvoiceVerification")]
        public IHttpActionResult ManageInvoiceVerificationDetails(InvoiceTransactions objInvoiceTransactions)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.ManageInvoiceVerificationDetails(objInvoiceTransactions, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("InvoiceInfoForDispatch")]
        public IHttpActionResult InvoiceInfoForDispatch(InvoiceInfo objInvoiceInfo)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.InvoiceInfoForDispatch(objInvoiceInfo));
            }
        }

        [HttpPost]
        [Route("ManageDispatchInvoice")]
        public IHttpActionResult ManageDispatchInvoice(DispatchInvoice objDispatchInvoice)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.ManageDispatchInvoice(objDispatchInvoice));
            }
        }

        [HttpGet]
        [Route("GetVendorPastDueInfo")]
        public IHttpActionResult GetVendorPastDueInfo(Int64 invoiceId, Int64 vendorPastDueInfoId, Int64 taskId)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.GetVendorPastDueInfo(invoiceId, vendorPastDueInfoId, taskId));
            }
        }

        [HttpPost]
        [Route("ManageVendorPastDue")]
        public IHttpActionResult ManageVendorPastDueInfo(VendorPastDue objVendorPastDue)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.ManageVendorPastDueInfo(objVendorPastDue));
            }
        }

        [HttpGet]
        [Route("GetVendorPendingCreditsInfo")]
        public IHttpActionResult GetVendorPendingCreditsInfo(Int64 invoiceId, Int64 vendorPendingCreditsInfoId, Int64 taskId)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.GetVendorPendingCreditsInfo(invoiceId, vendorPendingCreditsInfoId, taskId));
            }
        }

 
        [HttpPost]
        [Route("ManageVendorPendingCredits")]
        public IHttpActionResult ManageVendorPendingCreditsInfo(VendorPendingCredits objVendorPendingCredits)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.ManageVendorPendingCreditsInfo(objVendorPendingCredits));
            }
        }

        [HttpPost]
        [Route("VendorEmailData")]
        public IHttpActionResult ManageVendorEmailDataInfo(VendorEmailData objVendorEmailData)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.ManageVendorEmailDataInfo(objVendorEmailData));
            }
        }


        [HttpPost]
        [Route("AddLineitemToInvoice")]
        public IHttpActionResult AddLineitemToInvoice(LineItems objLineItems)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.AddLineitemToInvoice(objLineItems,VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("ManageBillGapComments")]
        public IHttpActionResult ManageBillGapComments(BillGapComments objBillGapComments)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.ManageBillGapComments(objBillGapComments, VectorAPIContext.Current.UserId));
            }
        }


        [HttpPost]
        [Route("UploadMissingInvoice")]
        public IHttpActionResult UploadMissingInvoice(MissingInvoice objMissingInvoice)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.UploadMissingInvoice(objMissingInvoice, VectorAPIContext.Current.UserId));
            }
        }


        [HttpPost]
        [Route("GeneratePlaceHolderInvoice")]
        public IHttpActionResult GeneratePlaceHolderInvoice(Placeholder objMissingInvoice)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                string FileServerPath = SecurityManager.GetConfigValue("FileServerPath");
                string folderPath = FileServerPath + "//InvoiceTemplates//";
                string batchFolderPath = FileServerPath + "//BatchInvoices//";
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.GeneratePlaceHolderInvoice(objMissingInvoice, VectorAPIContext.Current.UserId, folderPath, batchFolderPath));
            }
        }


        [HttpPost]
        [Route("DeleteInvoiceLineitem")]
        public IHttpActionResult DeleteInvoiceLineitem(InvoiceLineitemInfo objInvoiceLineitemInfo)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.DeleteInvoiceLineitem(objInvoiceLineitemInfo, VectorAPIContext.Current.UserId));
            }
        }



        [HttpPost]
        [Route("GetInvoiceReceiptData")]
        public IHttpActionResult GetInvoiceReceiptData(SearchEntity objSearchEntity)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.GetInvoiceReceiptData(objSearchEntity, VectorAPIContext.Current.UserId));
            }
        }


        [HttpPost]
        [Route("UploadIRPDocuments")]
        public IHttpActionResult UploadIRPDocuments(IRPDocuments objDocuments)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.UploadIRPDocuments(objDocuments, VectorAPIContext.Current.UserId));
            }
        }


        [HttpGet]
        [Route("GetIRPDocuments")]
        public IHttpActionResult GetIRPDocuments(string action, string IRPUniqueId)
        {
            using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objInvoiceBL.GetIRPDocuments(action, IRPUniqueId, VectorAPIContext.Current.UserId));
            }
        }


        //[HttpGet]
        //[Route("TriggerShortPayNotification")]
        //public IHttpActionResult TriggerShortPayNotification()
        //{
        //    using (var objInvoiceBL = new InvoiceBL(new VectorDataConnection()))
        //    {
        //        return VectorResponseHandler.GetVectorResponse(objInvoiceBL.TriggerShortPayNotification(VectorAPIContext.Current.UserId));
        //    }
        //}



    }


}
