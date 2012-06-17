using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookit.UI.Mvc4.Models
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages
        {
            get
            {
                if (ItemsPerPage == 0) return 0;
                return (int)(Math.Ceiling((1.0 * TotalItems) / ItemsPerPage));
            }
        }
    }
}