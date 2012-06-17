using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bookit.Domain;

namespace Bookit.Biz
{
    public interface IRoomFinder
    {
        IList<SearchResult> Find(RegularSearchDetail detail);
        IList<SearchResult> Find(OneClickSearchDetail detail);
    }
}
