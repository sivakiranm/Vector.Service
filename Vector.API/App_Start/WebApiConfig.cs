using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Cors;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using Vector.API.Handlers;

namespace Vector.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //EnableCorsAttribute cors = new EnableCorsAttribute("http://localhost:4200/", "*", "GET,POST,PUT,DELETE");
           // config.EnableCors(); 
            
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //config.MessageHandlers.Add(new MessageHandler());

            config.MessageHandlers.Add(new PreflightRequestsHandler());
            config.MessageHandlers.Add(new VectorTransactionLoggingHandler());
            config.Services.Replace(typeof(IExceptionHandler), new VectorExceptionHandler());

            config.Services.Replace(typeof(IHttpControllerSelector), new HttpNotFoundAwareDefaultHttpControllerSelector(config));
            config.Services.Replace(typeof(IHttpActionSelector), new HttpNotFoundAwareControllerActionSelector());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }

    /// <summary>
    /// To allow Preflight request for uploading files in cross domain
    /// </summary>
    public class PreflightRequestsHandler : DelegatingHandler
    {
        /// <summary>
        /// Overrising SendAsync Method to add headers for response i.e Access Control Allow Origin
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Contains("Origin") && request.Method.Method.Equals("OPTIONS"))
            {
                var response = new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
                // Define and add values to variables: origins, headers, methods (can be global)               
                response.Headers.Add("Access-Control-Allow-Origin", "*");
                //response.Headers.Add("Access-Control-Allow-Headers", "emstoken,enctype,encoding");
                response.Headers.Add("Access-Control-Allow-Headers", "*");
                //response.Headers.Add("Access-Control-Allow-Methods", "*");
                response.Headers.Add("Access-Control-Allow-Methods", "POST, GET, PUT, DELETE, OPTIONS,HEAD");
                //response.Headers.Add("Access-Control-Allow-Headers", "emstoken,enctype,encoding,Access-Control-Allow-Headers, Origin,Accept, X-Requested-With, Content-Type, Access-Control-Request-Method, Access-Control-Request-Headers");
                response.Headers.Add("Access-Control-Allow-Headers", "rstoken,appversion,enctype,encoding,loginid,emailid,Uri,DataModel,DataModelAccess-Control-Allow-Headers, Origin,Accept, X-Requested-With, Content-Type, Access-Control-Request-Method, Access-Control-Request-Headers, responsetype, Access-Control-Allow-Origin, userIPDetails");

                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }
            return base.SendAsync(request, cancellationToken);
        }

    }

}
