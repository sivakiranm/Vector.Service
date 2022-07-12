using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vector.API.Handlers;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Garage.BusinessLayer;
using Vector.Garage.Entities;
using Vector.UserManagement.ClientInfo;

namespace Vector.API.Controllers
{
    [RoutePrefix("api/contracts")]
    [VectorActionAutorizationFilter]
    public class ContractsController : ApiController
    {
        [HttpPost]
        [Route("GetContracts")]

        public IHttpActionResult GetContracts(ContractSearch objContractSearch)
        {
            using (var objContractsBL = new ContractsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objContractsBL.GetContracts(objContractSearch));
            }
        }

        [HttpPost]
        [Route("GetContractData")]

        public IHttpActionResult GetContractData(ContractData objContractData)
        {
            using (var objContractsBL = new ContractsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objContractsBL.GetContractData(objContractData));
            }
        }

        [HttpPost]
        [Route("UpdateContract")]

        public IHttpActionResult UpdateContract(Contract objContract)
        {
            using (var objContractsBL = new ContractsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objContractsBL.UpdateContract(objContract,VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("ApproveOrDeclineContractAnnualIncrease")]

        public IHttpActionResult ApproveOrDeclineAnnualIncrease(ApproveOrDeclineAnnualIncrease objApproveOrDeclineAnnualIncrease)
        {
            using (var objContractsBL = new ContractsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objContractsBL.ApproveOrDeclineAnnualIncrease(objApproveOrDeclineAnnualIncrease));
            }
        }

        [HttpPost]
        [Route("ManageContractLineItems")]

        public IHttpActionResult ManageContractLineItems(ContractLineItems objContractLineItems)
        {
            using (var objContractsBL = new ContractsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objContractsBL.ManageContractLineItems(objContractLineItems));
            }
        }

        [HttpPost]
        [Route("RenewOrArchiveContract")]

        public IHttpActionResult RenewOrArchiveContract(Contract objContract)
        {
            using (var objContractsBL = new ContractsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objContractsBL.RenewOrArchiveContract(objContract));
            }
        }

        [HttpPost]
        [Route("ContractEmailDetailsForTransmit")]

        public IHttpActionResult GetContractEmailDetailsForTransmit(ContractApproval objContractSearch)
        {
            using (var objContractsBL = new ContractsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objContractsBL.GetContractEmailDetailsForTransmit(objContractSearch));
            }
        }

        [HttpPost]
        [Route("TransmitContract")]

        public IHttpActionResult TransmitContract(ContractApproval objContractSearch)
        {
            using (var objContractsBL = new ContractsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objContractsBL.TransmitContract(objContractSearch,VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("DeleteContractLineItem")] 
        public IHttpActionResult DeleteContractLineItem(ContractApproval objContractSearch)
        {
            using (var objContractsBL = new ContractsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objContractsBL.TransmitContract(objContractSearch, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("UpcomingContractExpiry")]
        public IHttpActionResult UpcomingContractExpiry(UpcomingContractExpiry objUpcomingContractExpiry)
        {
            using (var objContractsBL = new ContractsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objContractsBL.UpcomingContractExpiry(objUpcomingContractExpiry));
            }
        }


        [HttpPost]
        [Route("GetTrueupPanelContractApprovalData")]
        public IHttpActionResult GetTrueupPannelContractApprovalData(SearchEntity objSearchEntity)
        {
            using (var objContractsBL = new ContractsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objContractsBL.GetTrueupPannelContractApprovalData(objSearchEntity, VectorAPIContext.Current.UserId));
            }
        }


    }
}