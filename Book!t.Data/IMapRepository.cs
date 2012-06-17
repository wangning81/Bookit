using Bookit.Domain;

namespace Bookit.Data
{
    public interface IMapRepository
    {
        Map Map
        {
            get;
        }

        CubeIsland GetIsland(string cubeNo);
    }
}
