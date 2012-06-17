using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookit.Domain
{
    public abstract class SearchDetail
    {
        public DateTime StartDateTime { get; set; }
        public int Duration { get; set; }
        public string CubeNo { get; set; }
    }
}
