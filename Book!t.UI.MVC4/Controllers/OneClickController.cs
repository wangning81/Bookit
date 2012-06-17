using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookit.Biz;
using Bookit.Domain;
using Bookit.UI.Mvc4.Models;
using log4net;
using Ninject;
using Constants = Bookit.UI.Mvc4.Infrastructure.Constants;
using System.Globalization;

namespace Bookit.UI.Mvc4.Controllers
{
    public class OneClickController : Controller
    {
        private readonly IRoomFinder _finder;
        private readonly ICalendarFileBuilder _calBuilder;
        private static readonly ILog _log = LogManager.GetLogger(typeof(OneClickController));

        [Inject]
        public OneClickController(IRoomFinder finder, ICalendarFileBuilder calBuilder)
        {
            this._finder = finder;
            this._calBuilder = calBuilder;
        }

        private void LogContext()
        {
            var req = HttpContext.Request;
            _log.InfoFormat(CultureInfo.InvariantCulture, "[request started] from [HostAddr] = {0}, [HostName] = {1}, [URL] = {2}, [UserAgent] = {3}",
                                        req.UserHostAddress,
                                        req.UserHostName,
                                        req.Url,
                                        req.UserAgent
                                        );
        }

        public ActionResult Index(int threshold = 30, int page = 1)
        {
            LogContext();
            _log.InfoFormat("threshold = {0}, page = {1}", threshold, page);

            SearchViewModel model = new SearchViewModel();

            model.SearchDetail = new OneClickSearchDetail()
                                            {
                                                StartDateTime = DateTime.Now,
                                                AvailableTolerenceThreshold = threshold
                                            };

            var cubeCookie = this.HttpContext.Request.Cookies.Get(Constants.CubeNoCookieName);
            if (cubeCookie != null)
                model.SearchDetail.CubeNo = cubeCookie.Value;

            var result = _finder.Find(model.SearchDetail as OneClickSearchDetail);

            model.SearchResults = result.Skip(Constants.ItemPerPage * (page - 1)).Take(Constants.ItemPerPage).ToList();
            model.PagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                ItemsPerPage = Constants.ItemPerPage,
                TotalItems = result.Count
            };

            ViewBag.Threshold = threshold;
            ViewBag.MenuList = new Dictionary<string, int>()
                               {
                                   {"30 minutes", 30},
                                   {"1 hour", 60},
                                   {"3 hours", 180},
                                   {"5 hours", 300}
                               };

            _log.InfoFormat("found {0} results", result.Count);
            _log.Info("[request finished]");

            return View(model);
        }

        public FileContentResult Book(
                                        DateTime availTime,
                                        int availDuration,
                                        string roomName,
                                        string roomEmail,
                                        string roomDescription
                                     )
        {
            _log.InfoFormat(CultureInfo.InvariantCulture,
                                "booking room name = {0}, email = {1}, avail_time = {2}, avail_duration = {3}",
                                      roomName,
                                       roomEmail,
                                       availTime,
                                       availDuration
                                       );

            string ext;
            return File(_calBuilder.Build(availTime, availDuration, string.Format(CultureInfo.InvariantCulture, "{0} ({1})", roomName, roomDescription), roomEmail, out ext),
                         "text/calendar",
                         string.Format(CultureInfo.InvariantCulture,
                                        "Bookit_{0}_{1}_{2}.{3}",
                                        roomName,
                                        availTime,
                                        availDuration,
                                        ext
                                        )
                       );
        }

    }
}
