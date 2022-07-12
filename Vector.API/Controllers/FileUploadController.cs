using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Vector.API.Handlers;
using Vector.Common.BusinessLayer;
using Vector.Common.Entities;

namespace Vector.API.Controllers
{
    [RoutePrefix("api/fileupload")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FileUploadController : ApiController
    {
        /// <summary>
        /// To upload file 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="folderPath"></param>
        /// /// <param name="fileNameWithExt"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("FileUpload")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<object> UploadFiles(HttpRequestMessage request, string folderName, bool isTempFolder = false)
        {
            if (isTempFolder)
                folderName = SecurityManager.GetConfigValue("FileServerTempPath") + folderName + "\\";
            else
                folderName = SecurityManager.GetConfigValue("FileServerPath") + folderName + "\\";

            string response = await FileManager.UploadFiles(request, string.Format("{0}", folderName), isFullPath: true);

            if (!string.IsNullOrEmpty(response))
                return VectorResponseHandler.GetVectorResponse(new VectorResponse<string>() { ResponseData = "File Uploaded Successfully" });
            else
                return VectorResponseHandler.GetVectorResponse(new VectorResponse<string>() { Error = new Error() { ErrorDescription = "File upload failed" } });
        }

        [HttpGet]
        [Route("DeleteFile")]
        public IHttpActionResult DeleteFile(string fileName, string folderPath, bool isTempFolder = false)
        {
            if (isTempFolder)
                folderPath = SecurityManager.GetConfigValue("FileServerTempPath") + folderPath + "\\";
            else
                folderPath = SecurityManager.GetConfigValue("FileServerPath") + folderPath + "\\";

            if (FileManager.DeleteFile(fileName, string.Format("{0}", folderPath), isDeleteFodler: false))
                return VectorResponseHandler.GetVectorResponse(new VectorResponse<string>() { ResponseData = "File deleted successfully" });
            else
                return VectorResponseHandler.GetVectorResponse(new VectorResponse<string>() { Error = new Error() { ErrorDescription = "File deletion failed" } });
        }

        [HttpGet]
        [Route("DeleteFolder")]
        public IHttpActionResult DeleteFolder(string folderPath, bool isTempFolder = false)
        {
            if (isTempFolder)
                folderPath = SecurityManager.GetConfigValue("FileServerTempPath") + folderPath + "\\";
            else
                folderPath = SecurityManager.GetConfigValue("FileServerPath") + folderPath + "\\";

            if (Directory.Exists(HttpContext.Current.Server.MapPath(folderPath)))
            {
              //  Directory.Delete(HttpContext.Current.Server.MapPath(folderPath), true);
                return VectorResponseHandler.GetVectorResponse(new VectorResponse<string>() { ResponseData = "Folder deleted successfully" });

            }
            else
                return VectorResponseHandler.GetVectorResponse(new VectorResponse<string>() { Error = new Error() { ErrorDescription = "Folder deletion failed" } });
        }

        [HttpGet]
        [Route("MoveFiles")]
        public IHttpActionResult MoveFiles(string tempFolderName, string parentFolderName)
        {
            tempFolderName = SecurityManager.GetConfigValue("FileServerTempPath") + tempFolderName + "\\";
            parentFolderName = SecurityManager.GetConfigValue("FileServerPath") + parentFolderName + "\\";

            if (FileManager.MoveFiles(tempFolderName, parentFolderName,isDeleteFodler:false))
            {
                return VectorResponseHandler.GetVectorResponse(new VectorResponse<string>() { ResponseData = "Files moved successfully" });

            }
            else
                return VectorResponseHandler.GetVectorResponse(new VectorResponse<string>() { Error = new Error() { ErrorDescription = "Files moving failed" } });
        }
    }
}

