using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bookit.Domain;

namespace Bookit.Biz
{
    public class DijikstraPathFinder
    {
        private IndexMinPQ<MapNode, double> pq = new IndexMinPQ<MapNode, double>();
        private IDictionary<MapNode, double> distanceTo = new Dictionary<MapNode, double>();
        private IDictionary<MapNode, MapPath> pathTo = new Dictionary<MapNode, MapPath>();
        private Map map;

        public DijikstraPathFinder(Map map)
        {
            this.map = map;        
        }

        public IDictionary<MapNode, double> GetDistance(MapNode source, IList<MapNode> dest)
        {
            distanceTo.Clear();
            foreach (var n in map)
            {
                distanceTo[n] = double.MaxValue;
            }
            distanceTo[source] = 0.0;
            pq.Clear();

            var ret = new Dictionary<MapNode, double>();
            pq.Enqueue(source, 0.0d);
            while (pq.Count != 0)
            {
                var nodeToExam = pq.Dequeue();
                Relax(nodeToExam);
                if (dest.Contains(nodeToExam))
                {
                    ret[nodeToExam] = distanceTo[nodeToExam];
                    if (ret.Count == dest.Count)
                        return ret;
                }
            }
           
            return ret;
        }

        private void Relax(MapNode n)
        {
            var paths = map.AdjacentPath(n);
            foreach (var p in paths)
            {
                double newDist = distanceTo[n] + p.Distance;
                MapNode other = p.Other(n);
                if (newDist < distanceTo[other])
                {
                    distanceTo[other] = newDist;
                    pq[other] = newDist;
                    pathTo[other] = p;
                }
            }
        }
    }
}
