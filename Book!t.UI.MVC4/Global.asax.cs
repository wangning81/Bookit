using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using Bookit.Data;
using System.ComponentModel.DataAnnotations;
using Ninject;
using Bookit.UI.Mvc4.Infrastructure;
using Ninject.Web.Mvc;
using log4net;

namespace Bookit.UI.Mvc4
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : NinjectHttpApplication//System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
               "RegularWithT_D_A",
               "regular/{startTime}/{duration}/{attendees}",
               new { controller = "Regular", action = "Index" }
               );

            routes.MapRoute(
               "OneClickBook",
               "oneclick/book",
               new { controller = "OneClick", action = "Book" }
            );

            routes.MapRoute(
               "OneClickWithThreshold",
               "oneclick/{threshold}",
               new { controller = "OneClick", action = "Index" }
            );

            routes.MapRoute(
               "About",
               "about/{viewName}",
               new { controller = "About", action = "Index" }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            //Database.SetInitializer<BookitDb>(new DropCreateDatabaseIfModelChanges<BookitDb>());

            log4net.Config.XmlConfigurator.Configure();
            
        }

        //protected void Application_Start()
        //{
        //    AreaRegistration.RegisterAllAreas();

        //    RegisterGlobalFilters(GlobalFilters.Filters);
        //    RegisterRoutes(RouteTable.Routes);

        //    Database.SetInitializer<BookitDb>(new DropCreateDatabaseIfModelChanges<BookitDb>());
        //}

        protected override IKernel CreateKernel()
        {
            return new StandardKernel(new BookitModule());
        }
    }
}