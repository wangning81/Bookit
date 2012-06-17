using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Bookit.Domain;

namespace Bookit.Data
{
    public class MapDBRepository : IMapRepository, IDisposable
    {
        private BookitDB db = new BookitDB();
        private readonly Map map = new Map();
        private readonly IList<CubeIsland> allIslands;

        public MapDBRepository()
        {
            foreach (var node in this.db.MapNodes)
                map.AddMapNode(node);
            foreach (var path in this.db.MapPathes)
                map.AddPath(path);

            this.allIslands = this.db.MapNodes
                            .Where(n => n is CubeIsland)
                            .Select(i => i as CubeIsland)
                            .Include(p => p.Cubes).ToList();
        }
        #region IMapRepository Members

        public Map Map
        {
            get { return map; }
        }

        public CubeIsland GetIsland(string cubeNo)
        {
            foreach (var island in this.allIslands)
            {
                var cb = island.Cubes
                         .Where(c => c.Name == cubeNo).Count();
                if (cb > 0)
                    return island;
            }
            return null;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
        }

        #endregion
    }
}
