using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ashyana.UI.Web.ViewModel;
using Ashyana.UI.Web.Models;

namespace Ashyana.UI.Web.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
       
        public PartialViewResult Index()
        {
            //List<MenuList > menus = MenuGenerator.CreateMenu();
            return PartialView("Partials/_menu");
        }
    }
}