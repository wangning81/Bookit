using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bookit.UI.Mvc4.Infrastructure
{
    public static class SpanHelper
    {
        public static MvcHtmlString Span(this HtmlHelper helper,
                                              string innerText,
                                              string cssClass            
                                             )
        {
            TagBuilder tb = new TagBuilder("span");
            tb.SetInnerText(innerText);
            tb.AddCssClass(cssClass);
            return MvcHtmlString.Create(tb.ToString());
        }
    }
}