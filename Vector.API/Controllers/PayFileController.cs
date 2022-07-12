using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vector.API.Handlers;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Garage.BusinessLayer;
using Vector.Garage.Entities;

namespace Vector.API.Controllers
{  
    [RoutePrefix("api/PayFile")]
    [VectorActionAutorizationFilter]
    public class PayFileController : ApiController
    {
        [HttpPost]
        [Route("GetClientPayConfigurationInfo")]
        public IHttpActionResult GetClientPayConfigurationInfo(ClientPayAccountInfo objClientPayAccountInfo)
        {
            using (var objPayFileBL = new PayFileBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPayFileBL.GetClientPayConfigurationInfo(objClientPayAccountInfo));
            }
        }

        [HttpPost]
        [Route("ManageClientPayAccount")]
        public IHttpActionResult ManageClientPayAccount(ClientPayAccountInfo objClientPayAccountInfo)
        {
            using (var objPayFileBL = new PayFileBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPayFileBL.ManageClientPayAccount(objClientPayAccountInfo));
            }
        }

        [HttpPost]
        [Route("GetConsolidatedInvoiceForPayFile")]
        public IHttpActionResult GetConsolidatedInvoiceForPayFile(PayFileInfo objPayFileInfo)
        {
            using (var objPayFileBL = new PayFileBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPayFileBL.GetConsolidatedInvoiceForPayFile(objPayFileInfo));
            }
        }

        [HttpPost]
        [Route("ManagePayFileGeneration")]
        public IHttpActionResult ManagePayFileGeneration(PayFileInfo objPayFileInfo)
        {
            using (var objPayFileBL = new PayFileBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPayFileBL.ManagePayFileGeneration(objPayFileInfo));
            }
        }

        [HttpPost]
        [Route("ManageElectronicPayFileGeneration")]
        public IHttpActionResult ManageElectronicPayFileGeneration(PayFileInfo objPayFileInfo)
        {
            using (var objPayFileBL = new PayFileBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPayFileBL.ManageElectronicPayFileGeneration(objPayFileInfo));
            }
        }

        [HttpPost]
        [Route("GetPayFiles")]
        public IHttpActionResult GetPayFiles(SearchPayFile objSearchPayFile)
        {
            using (var objPayFileBL = new PayFileBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPayFileBL.GetPayFiles(objSearchPayFile));
            }
        }


        [HttpPost]
        [Route("ApprovePayFile")]
        public IHttpActionResult ApprovePayFile(ManagePayFile objSearchPayFile)
        {
            using (var objPayFileBL = new PayFileBL(new VectorDataConnection()))
            {
                string FileServerPath = SecurityManager.GetConfigValue("FileServerPath");
                string folderPath = FileServerPath + "//Payments//PayFile//";

                return VectorResponseHandler.GetVectorResponse(objPayFileBL.ManagePayFile(objSearchPayFile, folderPath,VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("ManageEletronicTransaction")]
        public IHttpActionResult ManageEletronicTransaction(EletronicTransaction objEletronicTransaction)
        {
            using (var objPayFileBL = new PayFileBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPayFileBL.ManageEletronicTransaction(objEletronicTransaction));
            }
        }

        [HttpPost]
        [Route("RejectPayFileTransactions")]
        public IHttpActionResult RejectPayFileTransactions(ManagePayFile objManagePayFile)
        {
            using (var objPayFileBL = new PayFileBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objPayFileBL.RejectPayFileTransactions(objManagePayFile));
            }
        }
    }
}
