using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.ExceptionHandling;
using Vector.Common.Entities;
using System.Web.Http.Results;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System.Globalization;
using System.Configuration;
using Vector.Common.BusinessLayer;

namespace Vector.API.Handlers
{
    public class VectorExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var ErrorResult = new VectorResponse<Error>() { Error = new Error() 
            { ErrorCode = "500", ErrorDescription = "An internal server error occured.. Contact administrator" } };

            HttpResponseMessage response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorResult);

            string uri = context.Request.RequestUri.AbsoluteUri + " Method:" + context.Request.Method.Method + " Version:" + VectorAPIContext.Current?.VectorVersion;

            var result = new ResponseMessageResult(response);
            context.Result = result;

            VectorTextLogger.LogErrortoFile(VectorTextLogger.GetExceptionDetails(1, context.Exception)
                , Convert.ToString("Vector"), Convert.ToString(VectorAPIContext.Current?.Name), uri);

            base.Handle(context);
        }

        public override Task HandleAsync(ExceptionHandlerContext context,
        CancellationToken cancellationToken)
        {
            // not needed, but I left it to debug and find out why it never reaches Handle() method
            return base.HandleAsync(context, cancellationToken);
        }

        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            // not needed, but I left it to debug and find out why it never reaches Handle() method
            return true; //context.CatchBlock.IsTopLevel;
        }
    }
}