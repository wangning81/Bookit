using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bookit.Domain;

namespace Bookit.UI.Mvc4.Models
{
    public class SearchViewModel
    {
        public SearchDetail SearchDetail { get; set; }
        public IList<SearchResult> SearchResults { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}