using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Vector.API.Handlers
{
    /// <summary>
    /// Http Not Found Aware Default Http Controller Selector
    /// </summary>
    public class HttpNotFoundAwareDefaultHttpControllerSelector : DefaultHttpControllerSelector
    {
        /// <summary>
        /// Http Not Found Aware Default Http Controller Selector
        /// </summary>
        /// <param name="configuration"></param>
        public HttpNotFoundAwareDefaultHttpControllerSelector(HttpConfiguration configuration)
       : base(configuration)
        {
        }

        /// <summary>
        /// Controller Selector
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            HttpControllerDescriptor decriptor = null;
            try
            {
                decriptor = base.SelectController(request);
            }
            catch (HttpResponseException)
            {
                var routeValues = request.GetRouteData().Values;
                routeValues["controller"] = "Error";
                routeValues["action"] = "Handle404";
                decriptor = base.SelectController(request);
            }
            return decriptor;
        }
    }
}