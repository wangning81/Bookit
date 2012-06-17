using Bookit.Biz;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Bookit.Domain;
using Bookit.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Bookit.Test.Biz
{
    [TestClass()]
    public class DijikstraPathFinderTest
    {

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod()]
        public void GetDistanceTest()
        {
            Map map = CreateChaoYang();
            DijikstraPathFinder target = new DijikstraPathFinder(map);
            MapNode source = new CubeIsland() { Id = 0, Name = "ChaoYang-Island7" };

            var g1 = new Gateway() { Id = 0, Name = "ChaoYang-G-W" };
            var g2 = new Gateway() { Id = 0, Name = "ChaoYang-G-N" };
            var g3 = new MeetingRoom { Id = 0, Name = "Tian Tan" };
            var g4 = new MeetingRoom { Id = 0, Name = "Wang Fu Jing" };
            var g5 = new CubeIsland { Id = 0, Name = "ChaoYang-Island10" };
            var g6 = new CubeIsland { Id = 0, Name = "ChaoYang-Island12" };
            var g7 = new CubeIsland { Id = 0, Name = "ChaoYang-Island13" };

            IList<MapNode> dest = new List<MapNode>() { g1, g2, g3, g4, g4, g5, g6, g7 };
            IDictionary<MapNode, double> expected = new Dictionary<MapNode, double>();

            expected.Add(g1, 11);
            expected.Add(g2, 3);
            expected.Add(g3, 7);
            expected.Add(g4, 5);

            IDictionary<MapNode, double> actual = target.GetDistance(source, dest);
            Assert.AreEqual(expected, actual);

        }

        private Map CreateChaoYang()
        {
            Map map = new Map();
            for (int i = 1; i <= 14; i++)
            {
                map.AddMapNode(new CubeIsland() { Id = i, Name = "ChaoYang-Island" + i });
            }

            map.AddMapNode(new MeetingRoom() { Id = 15, Name = "Wang Fu Jing" });
            map.AddMapNode(new MeetingRoom() { Id = 16, Name = "Tian Tan" });

            map.AddMapNode(new Gateway() { Id = 17, Name = "ChaoYang-G-W" });
            map.AddMapNode(new Gateway() { Id = 18, Name = "ChaoYang-G-N" });

            //1
            map.AddPath(1, 4, 1);
            map.AddPath(1, 7, 1);
            map.AddPath(1, 18, 2);

            //2
            map.AddPath(2, 5, 1);
            map.AddPath(2, 6, 1);
            map.AddPath(2, 3, 1);

            //3
            map.AddPath(3, 4, 1);
            map.AddPath(3, 6, 1);

            //4
            map.AddPath(4, 7, 1);
            map.AddPath(4, 18, 5);

            //5
            map.AddPath(5, 6, 3);
            map.AddPath(5, 8, 1);
            map.AddPath(5, 9, 1);
            map.AddPath(5, 17, 6);

            //6
            map.AddPath(6, 7, 3);
            map.AddPath(6, 9, 1);
            map.AddPath(6, 10, 1);

            //7
            map.AddPath(7, 10, 1);
            map.AddPath(7, 11, 1);

            //8
            map.AddPath(8, 9, 1);
            map.AddPath(8, 17, 6);
            map.AddPath(8, 12, 1);

            //9
            map.AddPath(9, 10, 1);
            map.AddPath(9, 12, 1);

            //10
            map.AddPath(10, 11, 1);
            map.AddPath(10, 12, 1);

            //11
            map.AddPath(11, 12, 3);
            map.AddPath(11, 14, 1);

            //12
            map.AddPath(12, 13, 1);
            map.AddPath(12, 14, 2);

            //13
            map.AddPath(13, 14, 1);
            map.AddPath(13, 15, 2);

            //14

            //15 Wang Fu Jing
            map.AddPath(15, 16, 1);

            //16 Tian Tan
            map.AddPath(16, 17, 3);

            return map;

        }

        [TestMethod]
        public void TestAllCubeDistance()
        {
            BookitDB db = new BookitDB();
            var allCubes = db.Cubes;
            IMapRepository mapRep = new MapDBRepository();
            DijikstraPathFinder sfinder = new DijikstraPathFinder(mapRep.Map);
            var allRooms = db.MeetingRooms.Select(mr => mr as MapNode).ToList();

            using (var fs = File.Open(@"C:\result.txt", FileMode.OpenOrCreate))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.Write(string.Format("CUBE,"));
                    foreach (var mr in allRooms)
                        sw.Write(mr.Name + ",");
                    sw.WriteLine();

                    foreach (var cube in allCubes)
                    {
                        var island = mapRep.GetIsland(cube.Name);
                        var nodeWithDistance = sfinder.GetDistance(island, allRooms);
                        sw.Write(cube.Name + ",");

                        foreach (var mr in allRooms)
                        {
                            sw.Write(nodeWithDistance[mr] + ",");
                        }
                        sw.WriteLine();
                    }
                }
            }
        }
    }
}
