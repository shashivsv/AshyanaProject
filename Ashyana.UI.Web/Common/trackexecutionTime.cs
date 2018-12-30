using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace  Ashyana.UI.Web.Common
{
    public class trackexecutionTime:ActionFilterAttribute,IExceptionFilter
    {
        private void LogExecutionTime(string data)
        {
            File.AppendAllText(HttpContext.Current.Server.MapPath("~/LogData/logFile.txt"),data);
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string details = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + " " + filterContext.ActionDescriptor.ActionName + "OnActionExecuting" + DateTime.Now.ToLongTimeString() + "\n";
            LogExecutionTime(details);
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string details = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + " " + filterContext.ActionDescriptor.ActionName + "-OnActionExecuted--" + DateTime.Now.ToLongTimeString() + "\n";
            LogExecutionTime(details);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            string details = filterContext.RouteData.Values["controller"] + "====" + filterContext.RouteData.Values["action"] + "-OnResultExecuting--" + DateTime.Now.ToLongTimeString() + "\n";
            LogExecutionTime(details);

        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            string details = filterContext.RouteData.Values["controller"] + "====" + filterContext.RouteData.Values["action"] + "-OnResultExecuted--" + DateTime.Now.ToLongTimeString() + "\n";
            LogExecutionTime(details);

        }
        public void OnException(ExceptionContext filterContext)
        {
            string details = filterContext.RouteData.Values["controller"] + "====" + filterContext.RouteData.Values["action"] + "-OnException--" + filterContext.Exception.Message + DateTime.Now.ToLongTimeString() + "\n";
            LogExecutionTime(details);
  
        }
    }
}