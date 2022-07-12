using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Vector.API.Controllers;

namespace Vector.API.Handlers
{
    /// <summary>
    /// Http Not Found Aware Controller Action Selector
    /// </summary>
    public class HttpNotFoundAwareControllerActionSelector : ApiControllerActionSelector
    {
        /// <summary>
        /// Http Not Foun
        /// .d Aware Controller Action Selector
        /// </summary>
        public HttpNotFoundAwareControllerActionSelector()
        {
        }

        /// <summary>
        /// Action Selector
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <returns></returns>
        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            HttpActionDescriptor decriptor = null;
            try
            {
                decriptor = base.SelectAction(controllerContext);
            }
            catch (HttpResponseException)
            {
                var routeData = controllerContext.RouteData;
                routeData.Values["action"] = "Handle404"; //controllerContext.Request.Method.Method
                controllerContext.RouteData = routeData;
                IHttpController httpController = new ErrorController();
                controllerContext.Controller = httpController;
                controllerContext.ControllerDescriptor = new HttpControllerDescriptor(controllerContext.Configuration, "Error", httpController.GetType());
                controllerContext.RouteData.Values["action"] = "Handle404";
                decriptor = base.SelectAction(controllerContext);

                //var routeData = controllerContext.RouteData;
                //routeData.Values["action"] = "Handle404"; 
                //IHttpController httpController = new ErrorController();
                //controllerContext.Controller = httpController;
                //controllerContext.ControllerDescriptor = new HttpControllerDescriptor(controllerContext.Configuration, "Error", httpController.GetType());
                //decriptor = base.SelectAction(controllerContext);
            }
            return decriptor;
        }
    }
}