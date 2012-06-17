using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookit.Domain
{
    public class OneClickSearchDetail : SearchDetail
    {
        /// <summary>
        /// i.e. how many minutes after "now" we still think
        /// it as available.
        /// </summary>
        public int AvailableTolerenceThreshold
        {
            get;
            set;
        }

        public static OneClickSearchDetail CreateDefault()
        {
            DateTime now = DateTime.Now;
            OneClickSearchDetail model = new OneClickSearchDetail()
            {
                StartDateTime = now,
                AvailableTolerenceThreshold = 30
            };
            return model;
        }
    }
}
