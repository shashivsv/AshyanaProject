using Ashyana.UI.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ashyana.UI.Web.Models;
using Ashyana.Common;
using Ashyana.UI.Web.ViewModel;

namespace Ashyana.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        // [HandleError]
        //[CustomErrorFilter]
        //[MyCheckSessionTimeout]

   
        public ActionResult Index()
        {
            using (AshyanaDBEntities objEntity = new AshyanaDBEntities())
            {
                var listMenu = (from i in objEntity.U_sp_SelectHomelink(0)
                                select new
                                 {
                                     i.linkname
                                 }
                               ).ToList();
                List<MenuList> lst = new List<MenuList>();
                foreach (var item in listMenu)
                {

                    MenuList lnk = new MenuList();
                    lnk.linkName = item.linkname;
                    lst.Add(lnk);
              
                }
                Session["topMenu"] = lst;
                Session["topMenu"] = lst as List<MenuList>;
                ViewBag.homeMenu = lst;
                return View();

            }
          


        }
        [MyActionFilter]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetPerson()
        {
            // Person p = new Person();

            return View("Person");

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Invest()
        {
            ViewBag.Message = "Your application InvestInvestInvestInvestInvest page.";

            return View();
        }
        public ActionResult Mortgage()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Opportunity()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult PropertyLaws()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult PropertyNews()
        {
            ViewBag.Message = "Your PropertyNews page.";

            return View();
        }
        public ActionResult Section()
        {
            ViewBag.Message = "Your PropertyNews page.";

            return View();
        }
        public ActionResult Valuation()
        {
            ViewBag.Message = "Your PropertyNews page.";

            return View();
        }
        //handling error at controller level
        [trackexecutionTime]
        protected override void OnException(ExceptionContext filterContext)
        {

            Exception exception = filterContext.Exception;
            //Logging the Exception
            filterContext.ExceptionHandled = true;


            var Result = this.View("Error", new HandleErrorInfo(exception,
                filterContext.RouteData.Values["controller"].ToString(),
                filterContext.RouteData.Values["action"].ToString()));

            filterContext.Result = Result;

            //log error



        }

        public string GetDate()
        {
            ViewBag.date = "Current time is :";
            return System.DateTime.Now.ToString();
        }

    }
}