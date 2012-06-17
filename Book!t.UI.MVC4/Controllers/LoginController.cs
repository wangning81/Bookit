using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookit.Data;
using Ninject;
using Bookit.UI.Mvc4.Infrastructure;

namespace Bookit.UI.Mvc4.Controllers
{
    public class LoginController : Controller
    {
        //private readonly IMapRepository rep;

        //[Inject]
        //public LoginController(IMapRepository rep)
        //{
        //    this.rep = rep;
        //}
        private const string cookieName = Constants.CubeNoCookieName;

        private void RemoveCubeCookie()
        {
            HttpContext.Response.SetCookie(new HttpCookie(cookieName) { Expires = DateTime.Now.AddDays(-1) });
        }

        private void SetCubeCookie(string cubeNo)
        {
            this.HttpContext.Response.SetCookie(new HttpCookie(cookieName,
                                                          cubeNo)
            {
                Expires = DateTime.Now.AddYears(1)
            });
        }

        public RedirectToRouteResult Change()
        {
            RemoveCubeCookie();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public PartialViewResult Login()
        {
            var cubeNo = this.HttpContext.Request.Cookies[cookieName];
            if (cubeNo == null)
                return PartialView();
            else
                return PartialView("ShowCube", cubeNo.Value);
        }

        [HttpPost]
        public PartialViewResult Login(string cubeNo)
        {
            SetCubeCookie(cubeNo);
            return PartialView("ShowCube", cubeNo);
        }

    }
}
