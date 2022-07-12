using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Vector.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Application_BeginRequest(object sender, EventArgs e)
        {
            var context = HttpContext.Current;
            var response = context.Response;


            //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            //HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            //{
            //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "*");
            //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "*");
            //    HttpContext.Current.Response.End();
            //}

            if (context.Request.HttpMethod == "OPTIONS")
            {
                response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
                response.AddHeader("Access-Control-Allow-Headers", "rstoken,appversion,enctype,emailid,encoding,X-Requested-With, Content-Type");
                //response.AddHeader("Access-Control-Allow-Headers", "rstoken,appversion,enctype,emailid,encoding,Access-Control-Allow-Headers, Origin,Accept, X-Requested-With, Content-Type, Access-Control-Request-Method, Access-Control-Request-Headers, responsetype, Access-Control-Allow-Origin,Access-Control-Max-Age: 6000, userIPDetails");
                response.End();
            }
        }
    }
}
