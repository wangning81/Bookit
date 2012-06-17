using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookit.Domain
{
    public class Map : IEnumerable<MapNode>
    {
        private IDictionary<MapNode, HashSet<MapPath>> adjList = new Dictionary<MapNode, HashSet<MapPath>>();
        
        public void AddMapNode(MapNode nodeU)
        {
            if (!adjList.ContainsKey(nodeU))
                adjList.Add(nodeU, new HashSet<MapPath>());
        }

        public void AddPath(MapPath path)
        {
            adjList[path.U].Add(path);
            adjList[path.V].Add(path);
        }

        public void AddPath(int nodeU, int nodeV, double distance)
        {
            MapNode u = GetNodeById(nodeU);
            MapNode v = GetNodeById(nodeV);

            AddPath(new MapPath() { U = u, V = v, Distance = distance }); 
        }

        private MapNode GetNodeById(int id)
        {
            return (from node in adjList.Keys
                    where node.Id == id
                    select node).Single();        
        }

        public HashSet<MapPath> AdjacentPath(MapNode nodeU)
        {
            return adjList[nodeU];
        }

        #region IEnumerable<MapNode> Members

        public IEnumerator<MapNode> GetEnumerator()
        {
            return adjList.Keys.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return adjList.Keys.GetEnumerator();
        }

        #endregion
    }
}
