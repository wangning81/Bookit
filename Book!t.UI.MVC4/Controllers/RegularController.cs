using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookit.Biz;
using Bookit.Domain;
using Bookit.UI.Mvc4.Models;
using log4net;
using Ninject;
using Constants = Bookit.UI.Mvc4.Infrastructure.Constants;

namespace Bookit.UI.Mvc4.Controllers
{
    public class RegularController : Controller
    {
        private readonly IRoomFinder _finder;
        private readonly ICalendarFileBuilder _calBuilder;
        private static readonly ILog Log = LogManager.GetLogger(typeof(RegularController));

        [Inject]
        public RegularController(IRoomFinder finder, ICalendarFileBuilder calBuilder)
        {
            this._finder = finder;
            this._calBuilder = calBuilder;
        }

        private void LogContext()
        {
            var req = HttpContext.Request;
            Log.InfoFormat(CultureInfo.InvariantCulture,
                            "[request started] from [HostAddr] = {0}, [HostName] = {1}, [URL] = {2}, [UserAgent] = {3}",
                                        req.UserHostAddress,
                                        req.UserHostName,
                                        req.Url,
                                        req.UserAgent
                                        );
        }


        public ActionResult Index(string startTime,
                                    string duration,
                                    string attendees,
                                    int page = 1)
        {
            LogContext();
            Log.InfoFormat(CultureInfo.InvariantCulture, "S = {0}, D = {1}, A = {2}, P = {3}", startTime, duration, attendees, page);


            SearchViewModel model = new SearchViewModel();
            IList<SearchResult> result = new List<SearchResult>();

            var searchDetail = RegularSearchDetail.Create(startTime, duration, attendees);
            if (searchDetail == null) //invalid datail.
            {
                Log.Info("invalid or empty search detail, redirect to default detail");
                return RedirectToGetRouteResult(RegularSearchDetail.CreateDefault());
            }

            var cubeCookie = this.HttpContext.Request.Cookies.Get(Constants.CubeNoCookieName);
            if (cubeCookie != null)
                searchDetail.CubeNo = cubeCookie.Value;

            result = _finder.Find(searchDetail);
            model.SearchDetail = searchDetail;
            model.SearchResults = result.Skip(Constants.ItemPerPage * (page - 1)).Take(Constants.ItemPerPage).ToList();
            model.PagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                ItemsPerPage = Constants.ItemPerPage,
                TotalItems = result.Count
            };

            Log.InfoFormat("found {0} results", result.Count);
            Log.Info("[request finished]");

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(RegularSearchDetail model)
        {
            Log.Info("posted to regular, redirect to GET...");
            return RedirectToGetRouteResult(model);
        }

        public FileContentResult Book(
                                        DateTime roomStartTime,
                                        int duration,
                                        string roomName,
                                        string roomEmail,
                                        string roomDescription
                                     )
        {
            Log.InfoFormat(
                                        CultureInfo.InvariantCulture,
                                        "booking room name = {0}, email = {1}, s = {2}, d = {3}",
                                        roomName,
                                        roomEmail,
                                        roomStartTime,
                                        duration
                          );

            string ext;
            var b = _calBuilder.Build(roomStartTime, duration, string.Format(CultureInfo.InvariantCulture, "{0} ({1})", roomName, roomDescription), roomEmail, out ext);
            return File(b, "text/calendar", string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            "Bookit_{0}_{1}_{2}.{3}",
                                                            roomName,
                                                            roomStartTime,
                                                            duration,
                                                            ext
                                                         ));
        }

        private RedirectToRouteResult RedirectToGetRouteResult(RegularSearchDetail model)
        {
            return
                        this.RedirectToRoute(
                                            new
                                            {
                                                startTime = Server.UrlEncode(model.StartDateTime.ToString(Bookit.Domain.Constants.MeetingStartDateTimeUrlSchema, CultureInfo.InvariantCulture)),
                                                duration = model.Duration,
                                                attendees = model.AttendeeNumber
                                            }
                                       );
        }

    }
}
