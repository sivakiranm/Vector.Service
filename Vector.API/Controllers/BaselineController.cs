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

namespace Vector.API.Controllers
{
    [RoutePrefix("api/baseline")]
    [VectorActionAutorizationFilter]

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BaselineController : ApiController
    {
        [HttpPost]
        [Route("VectorManageBaseLineSupportFiles")]
        
        public IHttpActionResult VectorManageBaseLineSupportFiles(BaseLineSupportFiles objBaselineSupportFilesSearch)
        {
            using (var objBaselineBL = new BaselineBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objBaselineBL.VectorManageBaseLineSupportFilesBL(objBaselineSupportFilesSearch));
            }
        }

        [HttpPost]
        [Route("GetBaseLineSupportFiles")]
        
        public IHttpActionResult GetBaseLineSupportFiles(BaselineSupportingFilesInfoSearch objBaselineSupportingFilesInfoSearch)
        {
            using (var objBaselineBL = new BaselineBL(new VectorDataConnection()))
            {
                // ToDO : Modify the controller to get from task i.e form intermediate table instead direct from client;
                return VectorResponseHandler.GetVectorResponse(objBaselineBL.GetBaseLineSupportFiles(objBaselineSupportingFilesInfoSearch));
            }
        }

        //[HttpPost]
        //[Route("UploadBaseLineSupportFiles")]
        //public IHttpActionResult UploadBaseLineSupportFiles([FromForm] BaseLineFileModel files)
        //{
        //    try
        //    {
        //        string path = Path.Combine(Directory.GetCurrentDirectory(), "Files/Baseline/" + files.BaseLineName + "/SupportingFiles/");
        //        bool isFolderExists = Directory.Exists(path);
        //        if (!isFolderExists)
        //            Directory.CreateDirectory(path);
        //        foreach (var file in files.FormFiles)
        //        {
        //            using (Stream stream = new FileStream((path + file.FileName), FileMode.Create))
        //            {
        //                file.CopyTo(stream);
        //            }
        //        }
        //        return VectorResponseHandler.GetVectorResponse(new VectorResponse<string>() { ResponseData = "File Uploaded Successfully" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return VectorResponseHandler.GetVectorResponse(new VectorResponse<string>() { Error = new Error() { ErrorDescription = ex.Message } });
        //    }
        //}

        [HttpPost]
        [Route("VectorManageBaseLineInfo")]
        
        public IHttpActionResult VectorManageBaseLineInfo(BaseLineInfo objBaseLineInfo)
        {
            using (var objBaselineBL = new BaselineBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objBaselineBL.VectorManageBaseLineInfoBL(objBaseLineInfo));
            }
        }
        [HttpPost]
        [Route("GetBaseLineInfo")]
        
        public IHttpActionResult GetBaseLineInfo(BaselineInfoSearch objBaselineInfoSearch)
        {
            using (var objBaselineBL = new BaselineBL(new VectorDataConnection()))
            {
                // ToDO : Modify the controller to get from task i.e form intermediate table instead direct from client;
                return VectorResponseHandler.GetVectorResponse(objBaselineBL.GetBaseLineInfoBL(objBaselineInfoSearch));
            }
        }

        [HttpPost]
        [Route("VectorGetMapCatalogLineItemInfo")]
        
        public IHttpActionResult VectorGetMapCatalogLineItemInfo(MapCatalogSearch objMapCatalogSearch)
        {
            using (var objBaselineBL = new BaselineBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objBaselineBL.VectorGetMapCatalogLineItemInfoBL(objMapCatalogSearch));
            }
        }
        [HttpPost]
        [Route("VectorManageMapCatalogLineItemInfo")]
        
        public IHttpActionResult VectorManageMapCatalogLineItemInfo(MapCatalog objMapCatalog)
        {
            using (var objBaselineBL = new BaselineBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objBaselineBL.VectorManageMapCatalogLineItemInfoBL(objMapCatalog));
            }
        }

        //[HttpPost]
        //[Route("UploadApproveBaseLineDocuments")]
        //public IHttpActionResult UploadApproveBaseLineDocuments([FromForm] BaseLineFileModel files)
        //{
        //    try
        //    {
        //        string path = Path.Combine(Directory.GetCurrentDirectory(), "Files/Baseline/" + files.BaseLineName + "/BaselineInvoice/");
        //        bool isFolderExists = Directory.Exists(path);
        //        if (!isFolderExists)
        //            Directory.CreateDirectory(path);
        //        foreach (var file in files.FormFiles)
        //        {
        //            using (Stream stream = new FileStream((path + file.FileName), FileMode.Create))
        //            {
        //                file.CopyTo(stream);
        //            }
        //        }
        //        return VectorResponseHandler.GetVectorResponse(new VectorResponse<string>() { ResponseData = "File Uploaded Successfully" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return VectorResponseHandler.GetVectorResponse(new VectorResponse<string>() { Error = new Error() { ErrorDescription = ex.Message } });
        //    }
        //}

        [HttpPost]
        [Route("VectorGetApproveBaseLineInfo")]
        public IHttpActionResult VectorGetApproveBaseLineInfo(ApproveBaseLineInfoSearch objApproveBaseLineInfoSearch)
        {
            using (var objBaselineBL = new BaselineBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objBaselineBL.VectorGetApproveBaseLineInfoBL(objApproveBaseLineInfoSearch));
            }
        }
        [HttpPost]
        [Route("VectorManageApproveBaseLineInfo")]
        public IHttpActionResult VectorManageApproveBaseLineInfo(ApproveBaseLineInfo objApproveBaseLineInfo)
        {
            using (var objBaselineBL = new BaselineBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objBaselineBL.VectorManageApproveBaseLineInfoBL(objApproveBaseLineInfo));
            }
        }

        [HttpPost]
        [Route("VectorManageBaseLineMainInfo")]
        public IHttpActionResult VectorManageBaseLineMainInfo(BaseLineMainInfo objBaseLineMainInfo)
        {
            using (var objBaselineBL = new BaselineBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objBaselineBL.VectorManageBaseLineMainInfoBL(objBaseLineMainInfo));
            }
        }
        [HttpPost]
        [Route("GetBaseLineMainInfo")]
        public IHttpActionResult GetBaseLineMainInfo(BaselineInfoSearch  objBaselineInfoSearch)
        {
            using (var objBaselineBL = new BaselineBL(new VectorDataConnection()))
            {
                // ToDO : Modify the controller to get from task i.e form intermediate table instead direct from client;
                return VectorResponseHandler.GetVectorResponse(objBaselineBL.GetBaseLineMainInfoBL(objBaselineInfoSearch));
            }
        }


        [HttpPost]
        [Route("VectorSearchBaselines")]
        public IHttpActionResult VectorSearchBaselines(SearchBaseline objSearchBaseline)
        {
            using (var objBaselineBL = new BaselineBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objBaselineBL.VectorSearchBaselines(objSearchBaseline, VectorAPIContext.Current.UserId));
            }
        }

        [HttpGet]
        [Route("BaseLineInformation")]
        public IHttpActionResult VectorGetBaseLineInformation(string baseLineId)
        {
            using (var objBaselineBL = new BaselineBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objBaselineBL.VectorGetBaseLineInformation(baseLineId, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("ManageBaseline")]
        public IHttpActionResult VectorAddBaseline(Baseline objBaseline)
        {
            using (var objBaselineBL = new BaselineBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objBaselineBL.VectorAddBaseline(objBaseline, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("GetBaseLineDetails")]

        public IHttpActionResult GetBaseLineDetails(BaselineInfoSearch objBaselineInfoSearch)
        {
            using (var objBaselineBL = new BaselineBL(new VectorDataConnection()))
            {
                // ToDO : Modify the controller to get from task i.e form intermediate table instead direct from client;
                return VectorResponseHandler.GetVectorResponse(objBaselineBL.GetBaseLineDetails(objBaselineInfoSearch));
            }
        }



        [HttpPost]
        [Route("ManageLineItemState")]
        public IHttpActionResult MangeLineItemState(LineitemInfo ObjbaselineLineitemInfo)
        {
            using (var objBaselineBL = new BaselineBL(new VectorDataConnection()))
            {
                // ToDO : Modify the controller to get from task i.e form intermediate table instead direct from client;
                return VectorResponseHandler.GetVectorResponse(objBaselineBL.MangeBaselineLineItemState(ObjbaselineLineitemInfo,VectorAPIContext.Current.UserId));
            }
        }


       

    }
}
