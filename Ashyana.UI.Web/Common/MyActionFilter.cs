using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ashyana.UI.Web.Common
{
    public class MyActionFilter : FilterAttribute, IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.HttpContext.Session["UserID"] != null)
            {
                filterContext.Result = new RedirectResult("/Home/Index");
            }
            else
            {
                filterContext.Result = new RedirectResult("/Login /Login");
            }
        }
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["UserID"] != null)
            {
                filterContext.Result = new RedirectResult("/Home/ Index");
            }
            else
            {
                filterContext.Result = new RedirectResult("/Login /Login");
            }
        }
    }
}