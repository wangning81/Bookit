using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookit.Domain
{
    public class MapPath
    {
        public int Id { get; set; }
        
        public MapNode U { get; set; }
        public MapNode V { get; set; }

        public MapNode Either
        {
            get { return U; }
        }

        public MapNode Other(MapNode nodeW)
        {
            if (nodeW.Equals(U)) return V;
            else return U;
        }

        public double Distance { get; set; }
    }
}
