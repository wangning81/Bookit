using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookit.Domain
{
    public class SearchResult : IComparable<SearchResult>
    {
        public double Score { get; set; }
        public double Dist { get; set; }
        public MeetingRoom TheRoom { get; set; }

        public int CompareTo(SearchResult other)
        {
            double cmp = this.Score - other.Score;
            if (cmp > 0) return 1;
            else if (cmp < 0) return -1;
            else return 0;
        }
    }
}
