using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bookit.Domain;
using Bookit.Data;
using Ninject;

namespace Bookit.Biz
{
    public class ShortestPathFinder : IRoomFinder
    {
        private DijikstraPathFinder sfinder;
        private IMapRepository mapRep;
        private IRoomFinder innerFinder;

        [Inject]
        public ShortestPathFinder(IMapRepository mapRep, IRoomFinder innerFinder)
        {
            this.mapRep = mapRep;
            sfinder = new DijikstraPathFinder(mapRep.Map);
            this.innerFinder = innerFinder;
        }

        public IList<SearchResult> Find(OneClickSearchDetail detail)
        {
            List<SearchResult> ret = innerFinder.Find(detail).ToList();
            Evaluate(detail, ret);

            ret.Sort();

            return ret;
        }

        public IList<SearchResult> Find(RegularSearchDetail detail)
        {
            List<SearchResult> ret = innerFinder.Find(detail).ToList();
            
            Evaluate(detail, ret);
            foreach (var r in ret)
                r.Score += Math.Pow(r.TheRoom.Capacity - detail.AttendeeNumber, 2);

            ret.Sort();

            return ret;
        }

        private void Evaluate(SearchDetail detail, IList<SearchResult> ret)
        {
            var island = mapRep.GetIsland(detail.CubeNo);
            if (island != null && ret.Count > 0)
            {
                IList<MapNode> availRooms = ret.Select(r => r.TheRoom as MapNode)
                                               .ToList();

                var nodeWithDistance = sfinder.GetDistance(island, availRooms);

                foreach (var r in ret)
                {
                    r.Dist = nodeWithDistance[r.TheRoom];
                    r.Score = r.Dist;
                }
            }
        }
    }
}
