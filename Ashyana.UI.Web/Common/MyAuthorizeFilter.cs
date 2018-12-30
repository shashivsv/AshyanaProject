using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ashyana.UI.Web.Common
{
    public class MyAuthorizeFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {

            //if (filterContext.HttpContext.Request.IsAuthenticated)
            //{
            //    if (!System.Web.Security.Roles.IsUserInRole("Admin"))
            //    {
            //        ViewResult result = new ViewResult();
            //        result.ViewName = "Error";
            //        result.ViewBag.ErrorMessage = "Invalid User";
            //        filterContext.Result = result;
            //    }
            //}
            //else
            //{
            //    ViewResult result = new ViewResult();
            //    result.ViewName = "login";
            //}
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            Controller controller = filterContext.Controller as Controller;

            if (controller != null)
            {

                if (session["userid"] == null)
                {
                    ViewResult result = new ViewResult();

                    result.ViewName = "Error.cshtml";
                    result.ViewBag.ErrorMessage = "Invalid User";
                   // filterContext.Result = result;
       
                   controller.HttpContext.Response.Redirect("~/Views/Shared/Error.cshtml");
                }
            }
        }
    }
}

















