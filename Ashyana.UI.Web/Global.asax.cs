using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Ashyana.UI.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Application["Now"] = 0;
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ///added registration for Dependency injection by add ref of Unity.MVC5

            //UnityConfig.RegisterComponents();
        }

        //code added to solve error of default constructor
        private void RegisterDependencyResolver()
        {

        }

       
        protected void Session_Start(object sender, EventArgs e)
        {//New user connected to website
            Application["Now"] = (int)Application["Now"] + 1;
        }
        protected void Session_End(object sender, EventArgs e)
        {//user disconnected from webste
            Application["Now"] = (int)Application["Now"] - 1;
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            var error = Server.GetLastError();
            var code = (error is HttpException) ? (error as HttpException).GetHttpCode() : 500;

            HttpException httpException = error as HttpException;
            if (httpException != null)
            {
                RouteData routeData = new RouteData();
                routeData.Values.Add("controller", "Error");
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // page not found
                        routeData.Values.Add("action", "HttpError404");
                        break;
                    case 500:
                        // server error
                        routeData.Values.Add("action", "HttpError500");
                        break;
                    default:
                        routeData.Values.Add("action", "General");
                        break;
                }
                routeData.Values.Add("error", error);
                // clear error on server
                Server.ClearError();

                // at this point how to properly pass route data to error controller?
            }

        }
    }

}
