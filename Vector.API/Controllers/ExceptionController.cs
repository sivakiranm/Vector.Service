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
    [RoutePrefix("api/exception")]
    [VectorActionAutorizationFilter]
    public class ExceptionController : ApiController
    {
        [HttpPost]
        [Route("Exceptions")]

        public IHttpActionResult GetExceptions(ExceptionsSearch objExceptionsSearch)
        {
            using (var objExceptionsBL = new ExceptionsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objExceptionsBL.GetExceptions(objExceptionsSearch));
            }
        }

        [HttpPost]
        [Route("Create")]

        public IHttpActionResult CreateException(CreateException objCreateException)
        {
            using (var objExceptionsBL = new ExceptionsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objExceptionsBL.RaiseException(objCreateException));
            }
        }


        [HttpPost]
        [Route("CreateExceptionTicket")]

        public IHttpActionResult CreateExceptionTicket(ExceptionTicketEntity objCreateException)
        {
            using (var objExceptionsBL = new ExceptionsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objExceptionsBL.CreateExceptionTicket(objCreateException,VectorAPIContext.Current.UserId));
            }
        }

        [HttpGet]
        [Route("GetExceptionAnalyticsData")]

        public IHttpActionResult GetExceptionAnalyticsData(string fromDate, string  toDate)
        {
            using (var objExceptionsBL = new ExceptionsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objExceptionsBL.GetExceptionAnalyticsData(fromDate, toDate,VectorAPIContext.Current.UserId));
            }
        }


        [HttpGet]
        [Route("GetExceptionHistory")]

        public IHttpActionResult GetExceptionHistory(Int64  exceptionId)
        {
            using (var objExceptionsBL = new ExceptionsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objExceptionsBL.GetExceptionHistory(exceptionId, VectorAPIContext.Current.UserId));
            }
        }
    }
}
