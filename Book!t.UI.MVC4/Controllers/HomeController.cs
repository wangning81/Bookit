using System.Web;
using System.Web.Mvc;
using Bookit.Domain;
using log4net;
using System.Globalization;

namespace Bookit.UI.Mvc4.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(HomeController));
        public ActionResult Index()
        {
            LogContext();
            return View(RegularSearchDetail.CreateDefault());
        }

        private void LogContext()
        {
            var req = HttpContext.Request;
            Log.InfoFormat(CultureInfo.InvariantCulture,
                                        "request from [HostAddr] = {0}, [HostName] = {1}, [URL] = {2}, [UserAgent] = {3}",
                                        req.UserHostAddress,
                                        req.UserHostName,
                                        req.Url,
                                        req.UserAgent
                                        );
        }

        [HttpPost]
        public ActionResult Index(RegularSearchDetail model)
        {
            Log.Info("posting to regular controller...");
            return this.RedirectToRoute(
                                            new { 
                                                controller="regular",
                                                startTime = Server.UrlEncode(model.StartDateTime.ToString(Constants.MeetingStartDateTimeUrlSchema, CultureInfo.InvariantCulture)), 
                                                duration = model.Duration , 
                                                attendees = model.AttendeeNumber
                                           }
                                       );

        }

    }
}
