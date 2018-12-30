using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ashyana.UI.Web.Common
{
    public class CustomErrorFilter : HandleErrorAttribute
    {
       public   override void OnException(ExceptionContext filterContext)
        {
            Log(filterContext.Exception);
        
        }

        private void Log(Exception exception)
        {
            File.AppendAllText(HttpContext.Current.Server.MapPath("~/LogData/logFile.txt"),exception.ToString());

        }
    }

}