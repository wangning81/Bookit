using System;
using System.Web.Mvc;
using System.Text;
using Bookit.UI.Mvc4.Models;
using System.Globalization;

namespace Bookit.HtmlHelpers
{
    public static class PagingHelper
    {
        public static MvcHtmlString PageLinks(
                                              this HtmlHelper html,
                                              PagingInfo info,
                                              Func<int, string> url
                                              )
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= info.TotalPages; i++)
            {
                TagBuilder tag = null;
                if (i == info.CurrentPage)
                {
                    tag = new TagBuilder("span");
                    tag.AddCssClass("selected");
                }
                else
                {
                    tag = new TagBuilder("a");
                    tag.MergeAttribute("href", url(i));
                }
                tag.InnerHtml = i.ToString(CultureInfo.InvariantCulture);
                sb.Append(tag.ToString());
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}