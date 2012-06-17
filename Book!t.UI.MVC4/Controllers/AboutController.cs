using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bookit.UI.Mvc4.Controllers
{
    public class AboutController : Controller
    {
        public ActionResult Index(string viewName = "help")
        {
            ViewBag.MenuList = new Dictionary<string, string>()
                               {
                                   {"Help & FAQ", "help"},
                                   {"innovation @ TECH", "technology"},
                                   {"innovation @ IDEA", "idea"},
                                   {"Feedback", "feedback"},
                               };
            ViewBag.ContentViewName = viewName;
            return View();
        }

    }
}
