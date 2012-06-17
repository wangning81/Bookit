using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookit.Domain
{
    public class OneClickSearchResult : SearchResult
    {
        public DateTime AvailStartTime { get; set; }
        public int AvailDuration { get; set; }
    }
}
