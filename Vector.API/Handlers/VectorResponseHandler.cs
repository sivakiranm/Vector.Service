using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Vector.Common.Entities;

namespace Vector.API.Handlers
{
  public static class VectorResponseHandler
  {
    private const string PartialContentCode = "204";
    private const string BadRequestCode = "400";
    private const string ResourceNotFound = "404";
    private const string InternalServerErrorCode = "500";

    public static IHttpActionResult GetVectorResponse<T>(VectorResponse<T> resultData, Error error = null)
    {
      return new VectorHttpActionResult(HttpContext.Current.Items["RequestMessage"] as HttpRequestMessage, resultData, HttpStatusCode.OK);
    }

    public static IHttpActionResult GetVectorResponse(object vectorResponse)
    {
      IHttpActionResult iar = null;
      var reqMessage = HttpContext.Current.Items["RequestMessage"] as HttpRequestMessage;
      if (vectorResponse is VectorResponse<Error>)
      {
        if (((VectorResponse<Error>)vectorResponse).Error.ErrorCode.Contains(PartialContentCode))
          iar = new VectorHttpActionResult(reqMessage, vectorResponse, HttpStatusCode.PartialContent);
        if (((VectorResponse<Error>)vectorResponse).Error.ErrorCode.Contains(BadRequestCode))
          iar = new VectorHttpActionResult(reqMessage, vectorResponse, HttpStatusCode.BadRequest);
        if (((VectorResponse<Error>)vectorResponse).Error.ErrorCode.Contains(InternalServerErrorCode))
          iar = new VectorHttpActionResult(reqMessage, vectorResponse, HttpStatusCode.InternalServerError);
        if (((VectorResponse<Error>)vectorResponse).Error.ErrorCode.Contains(ResourceNotFound))
          iar = new VectorHttpActionResult(reqMessage, vectorResponse, HttpStatusCode.NotFound);
      }
      else
      {
        iar = new VectorHttpActionResult(reqMessage, vectorResponse, HttpStatusCode.OK);
      }
      return iar;
    }

  }

  public class VectorHttpActionResult : IHttpActionResult
  {
    private readonly HttpRequestMessage RequestMsg;
    private readonly object Msg;
    private readonly HttpStatusCode StatusCode;

    public VectorHttpActionResult(HttpRequestMessage requestMsg, object msg, HttpStatusCode statusCode)
    {
      RequestMsg = requestMsg;
      Msg = msg;
      StatusCode = statusCode;
    }

    public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
    {
      var response = RequestMsg.CreateResponse(StatusCode, Msg);
      return Task.FromResult(response);
    }
  }
}
