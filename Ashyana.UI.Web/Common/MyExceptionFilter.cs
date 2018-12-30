using Ashyana.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Ashyana.UI.Web.Common
{
    public class MyExceptionFilter : HandleErrorAttribute
    {

        private Log _log;
        public override void OnException(ExceptionContext filterContext)
        {
            _log.LogException(filterContext.Exception.ToString());
            Logg(filterContext.Exception);

        }

        private void Logg(Exception exception)
        {
           File.AppendAllText(HttpContext.Current.Server.MapPath("~/LogData/logFile.txt"), exception.ToString());

        }
    }

}