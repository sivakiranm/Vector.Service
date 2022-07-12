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
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Garage.BusinessLayer;
using Vector.Garage.Entities;
using Vector.UserManagement.ClientInfo;

namespace Vector.API.Controllers
{
    [RoutePrefix("api/client")]

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    //[VectorActionAutorizationFilter]
    public class ClientController : ApiController
    {
        [HttpPost]
        [Route("GetClientInfo")]
        public IHttpActionResult GetClientInfo(ClientInfoSearch clientInfoSearch)
        {
            using (var clientsBL = new ClientBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(clientsBL.GetClientsSearch(clientInfoSearch));

            }
        }

        [HttpPost]
        [Route("ManageClientInfo")]
        public IHttpActionResult ManageClientInfo(ClientInfo objClientInfo)
        {
            using (var clientsBL = new ClientBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(clientsBL.ManageClientInfo(objClientInfo));
            }
        }

        [HttpGet]
        [Route("GetClientByClientInfoId")]
        public IHttpActionResult GetClientByClientInfoId(long clientInfoId)
        {
            using (var clientsBL = new ClientBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(clientsBL.GetClientByClientInfoId(clientInfoId));

            }
        }

        [HttpGet]
        [Route("GetClientByClientId")]
        public IHttpActionResult GetClientByClientId(long clientId)
        {
            using (var clientsBL = new ClientBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(clientsBL.GetClientByClientId(clientId));

            }
        }

        [HttpPost]
        [Route("GetClientAgreementInfo")]

        public IHttpActionResult GetClientAgreementInfo(ClientAgreementInfoSearch objSearchClientAgreement)
        {
            // RequestFrom - Task/Garage
            using (var objClientBL = new ClientBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objClientBL.GetClientAgreementInfo(objSearchClientAgreement, 6));
            }
        }

        [HttpPost]
        [Route("ManageClientAgreementInfo")]
        public IHttpActionResult ManageClientAgreementInfo([FromBody] ClientAgreementInfo objClientAgreementInfo)
        {
            using (var objClientBL = new ClientBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objClientBL.ManageClientAgreementInfo(objClientAgreementInfo, 1));

            }
        }

        [HttpPost]
        [Route("VectorGetClientInvoicePreferences")]
        public IHttpActionResult VectorGetClientInvoicePreferences(ClientInvoicePreferenceSearch objClientInvoicePreferenceSearch)
        {
            using (var objClientBL = new ClientBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objClientBL.VectorGetClientInvoicePreferencesBL(objClientInvoicePreferenceSearch));
            }
        }

        [HttpPost]
        [Route("VectorManageClientInvoicePreferences")]
        public IHttpActionResult VectorManageClientInvoicePreferences(ClientInvoicePreferences objClientInvoicePreferences)
        {
            using (var objClientBL = new ClientBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objClientBL.VectorManageClientInvoicePreferencesBL(objClientInvoicePreferences));
            }
        }


        [HttpPost]
        [Route("VectorGetClientContactInfo")]
        public IHttpActionResult VectorGetClientContactInfo(ClientContactInfoSearch objClientContactInfoSearch)
        {
            using (var objClientBL = new ClientBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objClientBL.VectorGetClientContactInfoBL(objClientContactInfoSearch));
            }
        }

        [HttpPost]
        [Route("VectorManageClientContactInfo")]
        public IHttpActionResult VectorManageClientContactInfo(ClientContactInfo objClientContactInfo)
        {
            using (var objClientBL = new ClientBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objClientBL.VectorManageClientContactInfoBL(objClientContactInfo));
            }
        }

        [HttpPost]
        [Route("VectorManageClientRole")]
        public IHttpActionResult VectorManageClientRole(ClientContactRole objClientContactRole)
        {
            using (var objClientBL = new ClientBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objClientBL.VectorManageClientRoleBL(objClientContactRole));
            }

        }
        [HttpPost]
        [Route("VectorManageClientContractInfo")]
        public IHttpActionResult VectorManageClientContractInfo(ClientContractInfo objClientContractInfo)
        {
            using (var objClientBL = new ClientBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objClientBL.VectorManageClientContractInfoBL(objClientContractInfo));
            }
        }

        [HttpGet]
        [Route("GetClientContractInfo")]
        public IHttpActionResult GetClientContractInfo(Int64 clientID, Int64 taskId)
        {
            using (var objClientBL = new ClientBL(new VectorDataConnection()))
            {
                // ToDO : Modify the controller to get from task i.e form intermediate table instead direct from client;
                return VectorResponseHandler.GetVectorResponse(objClientBL.GetClientContractInfo(clientID, taskId));
            }
        }

        #region"                METHOD TO RETURN THE DATA IN THE FROM OF STRING             "

        [HttpGet]
        [Route("GetClientInfo")]
        public string GetClientInfo([FromBody] ClientInformation clientinformation)
        {

            //if (clientinformation == null)
            //{
            //    return "Invalid Data.";
            //}

            return $"ClientId: {clientinformation.ClientID}, ClientName: {clientinformation.ClientName}, ClientPhoneNumber: {clientinformation.ClientPhoneNumber}, ClientDOB: {clientinformation.ClientDOB}, ClientGender: {clientinformation.ClientGender}, ClientAddress: {clientinformation.ClientAddress}";

        }

        #endregion

        #region" METHOD TO RETURN THE DATA IN THE FORM OF JSON OBJECT "

        [HttpPost]
        [Route("GetClientInfo1")]
        public IHttpActionResult GetClientInfo1([FromBody] ClientInformation clientinformation)
        {
            ClientInformation ClientInformation1 = null;
            ClientInformation1 = new ClientInformation();
            ClientInformation1.ClientID = clientinformation.ClientID;
            ClientInformation1.ClientName = clientinformation.ClientName;
            ClientInformation1.ClientPhoneNumber = clientinformation.ClientPhoneNumber;
            ClientInformation1.ClientGender = clientinformation.ClientGender;
            ClientInformation1.ClientDOB = clientinformation.ClientDOB;
            ClientInformation1.ClientAddress = clientinformation.ClientAddress;
            return Ok(ClientInformation1);

        }

        #endregion

        #region METHOD TO RETURN THE DATA IN THE FORM OF JSON OBJECT 

        [HttpPost]
        [Route("GetStakeholderInfo")]
        public IHttpActionResult GetStakeholderInfo([FromBody] ClientInformation clientinformation)
        {
            UsersInformation usersinformation = new UsersInformation();
            userInfo userinfo = new userInfo();
            usersinformation.userInfo = new System.Collections.Generic.List<userInfo>();

            userinfo.userId = 1;
            userinfo.userName = "Stakeholder";
            userinfo.userType = "Internal";
            userinfo.userRole = "Stakeholder";
            userinfo.userRoleId = 1;
            userinfo.VectorToken = "RSSDSCJHIUGIUGIUHIH";
            userinfo.tokenValidityFrom = "12/31/2020";
            userinfo.tokenValidityto = "01/31/2021";
            usersinformation.userInfo.Add(userinfo);


            var model = new
            {
                response = "Success",
                responseMessage = "Login Success, Kindly Secure Token",
                responseCode = "" + "200" + "",
                responseData = usersinformation,
            };
            return Ok(model);

        }

        #endregion

        [HttpPost]
        [Route("ManageClientAddressInfo")]
        public IHttpActionResult ManageClientAddressInfo([FromBody] ClientAddressInfo objClientAddressInfo)
        {
            using (var objClientBL = new ClientBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objClientBL.ManageClientAddressInfo(objClientAddressInfo, 1));

            }
        }

        [HttpPost]
        [Route("GetClientAddressInfo")]
        public IHttpActionResult GetClientAddressInfo(ClientAddressInfoSearch objSearchClientAddress)
        {
            using (var objClientBL = new ClientBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objClientBL.GetClientAddressInfo(objSearchClientAddress, 6));
            }
        }

        [HttpPost]
        [Route("VectorManageClientRegion")]
        public IHttpActionResult VectorManageClientRegion(Region objRegion)
        {
            using (var objClientBL = new ClientBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objClientBL.VectorManageClientRegion(objRegion));
            }

        }

        [HttpPost]
        [Route("VectorManageClientVertical")]
        public IHttpActionResult VectorManageClientVertical(Vertical objVertical)
        {
            using (var objClientBL = new ClientBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objClientBL.VectorManageClientVertical(objVertical));
            }

        }


        //[HttpPost]
        //[Route("UploadFiles")]
        //public IHttpActionResult UploadFiles([FromForm] FileModel files)
        //{
        //    try
        //    {
        //        string path = Path.Combine(Directory.GetCurrentDirectory(), $"Files/{files.Type}/{files.TypeName }/");
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

        [HttpGet]
        [Route("GetVectorViewClientInformation")]
        public IHttpActionResult GetVectorViewClientInformation(string action, Int64 clientId)
        {
            using (var objClientBL = new ClientBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objClientBL.GetVectorViewClientInformation(action, clientId));
            }
        }

        [HttpPost]
        [Route("UploadDealPackage")]
        public IHttpActionResult VectotManageUDealPackage(DealPackage objDealPackage)
        {
            using (var objClientBL = new ClientBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objClientBL.VectotManageUDealPackage(objDealPackage));
            }
        }

        //[HttpPost]
        //[Route("ManageDealPackage")]
        //public IHttpActionResult VectotManageDealPackage(string folderPath, Int64 clientId, Int64 userId)
        //{
        //    //folderPath = SecurityManager.GetConfigValue("FileServerPath") + "Client\\" + "C0001" + "//DealPackage//";
        //    //string response = await FileManager.UploadFiles(request, string.Format("{0}", folderPath), isFullPath: true);
        //    using (var objClientBL = new ClientBL(new VectorDataConnection()))
        //    {
        //        return VectorResponseHandler.GetVectorResponse(objClientBL.VectotManageUDealPackage(folderPath, clientId, userId));
        //    }
        //}

        [HttpPost]
        [Route("VectorGetClientSearch")]
        public IHttpActionResult VectorGetClientSearch(ClientInfo objClientInfo)
        {
            using (var clientsBL = new ClientBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(clientsBL.VectorGetClientSearchBL(objClientInfo,VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("GetClientDealPackageInfo")]
        public IHttpActionResult GetClientDealPackageInfo(DealPackage objClientInfo)
        {
            using (var clientsBL = new ClientBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(clientsBL.GetClientDealPackageInfo(objClientInfo));
            }
        }


        [HttpPost]
        [Route("CompleteDealPackage")]
        public IHttpActionResult CompleteDealPackage(DealPackage objDealPackage)
        {
            using (var objClientBL = new ClientBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objClientBL.CompleteDealPackage(objDealPackage,VectorAPIContext.Current.UserId));
            }
        }

        [HttpGet]
        [Route("GetClientRelatedProperties")]
        public IHttpActionResult GetClientRelatedProperties(string action, Int64 clientId)
        {
            using (var objClientBL = new ClientBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objClientBL.GetClientRelatedProperties(action, clientId,VectorAPIContext.Current.UserId));
            }
        }
    }
}
