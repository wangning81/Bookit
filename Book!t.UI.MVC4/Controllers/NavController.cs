using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookit.UI.Mvc4.Models;

namespace Bookit.UI.Mvc4.Controllers
{
    public class NavController : Controller
    {
        public ActionResult Menu(Type controllerType)
        {
            IList<NavLink> links = new List<NavLink>();

            links.Add(
                    new NavLink()
                    {
                        ControllerName = "home",
                        ActionName = "Index",
                        Text = "Home",
                        IsSelected = controllerType == null || controllerType == typeof(HomeController)
                    }
                );

            links.Add(
                    new NavLink()
                    {
                        ControllerName = "regular",
                        ActionName = "Index",
                        Text = "Regular",
                        IsSelected = controllerType == typeof(RegularController)
                    }
                );

            links.Add(
                    new NavLink()
                    {
                        ControllerName = "oneclick",
                        ActionName = "Index",
                        Text = "OneClick",
                        IsSelected = controllerType == typeof(OneClickController)
                    }
            );
            links.Add(
                    new NavLink()
                    {
                        ControllerName = "about",
                        ActionName = "Index",
                        Text = "About",
                        IsSelected = controllerType == typeof(AboutController)
                    }
                );

            return PartialView(links);
        }

    }
}
