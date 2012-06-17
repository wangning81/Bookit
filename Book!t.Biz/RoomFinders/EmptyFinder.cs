using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bookit.Domain;

namespace Bookit.Biz
{
    public class EmptyFinder : IRoomFinder
    {
        #region IRoomFinder Members

        public IList<SearchResult> Find(RegularSearchDetail detail)
        {
            return new List<SearchResult>();
        }

        public IList<SearchResult> Find(OneClickSearchDetail detail)
        {
            return new List<SearchResult>();
        }

        #endregion
    }
}
