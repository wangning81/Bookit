using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookit.UI.Mvc4.Models
{
    public class NavLink
    {
        public string Text { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public bool IsSelected { get; set; }
    }
}