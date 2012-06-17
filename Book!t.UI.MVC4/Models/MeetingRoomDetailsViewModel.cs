using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bookit.Domain;

namespace Bookit.UI.Mvc4.Models
{
    public class MeetingRoomDetailsViewModel
    {
        public SearchDetail SearchDetail { get; set; }
        public IList<Bookit.Domain.SearchResult> SearchResult { get; set; }
    }
}