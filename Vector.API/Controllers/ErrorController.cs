using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vector.Common.Entities;

namespace Vector.API.Controllers
{
    [RoutePrefix("api/error")]
    public class ErrorController : ApiController
    {   

        /// <summary>
        /// Handle 404 Error
        /// </summary>
        /// <returns></returns>
        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions, AcceptVerbs("PATCH")]
        public HttpResponseMessage Handle404()
        {
            return this.Request.CreateResponse<VectorResponse<Error>>(HttpStatusCode.BadRequest, new VectorResponse<Error> { Error = new ErrorManager().SetErrorResponseWithNoResourceFound() });
        }
    }   
}
