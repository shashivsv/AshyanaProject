using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Filters;
using Ashyana.UI.Web.Common;

namespace Ashyana.UI.Web.Common
{
    public class UsingAuthorize:  IAuthenticationFilter
    {

        private bool _auth;
      public void OnAuthentication(AuthenticationContext filterContext)  
        {  
            //Logic for authenticating a user  
            _auth = (filterContext.ActionDescriptor.GetCustomAttributes(typeof(AuthenticationContext), true).Length == 0);

        }

      //Runs after the OnAuthentication method  HttpStatusCodeResult
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)  
        {  
            //TODO: Additional tasks on the request  

            var user = filterContext.HttpContext.User;
            if(user==null ||!user.Identity.IsAuthenticated)
            {
               // filterContext.Result = new HttpUnauthorizedResult();
            }
        }  
    
    }
}