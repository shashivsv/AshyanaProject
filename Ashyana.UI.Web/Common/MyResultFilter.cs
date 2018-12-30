using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ashyana.UI.Web.Common
{
    public class MyResultFilter : FilterAttribute, IResultFilter
    {

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.HttpContext.Session["UserID"] != null)
            {
                filterContext.Result = new RedirectResult("/Home/Contact");
            }
            else
            {
                filterContext.Result = new RedirectResult("/Login/Login");
            }
        }


        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["UserID"] != null)
            {
                filterContext.Result = new RedirectResult("/Home/Contact");
            }
            else
            {
                filterContext.Result = new RedirectResult("/Login/Login");
            }
        }
    }
}