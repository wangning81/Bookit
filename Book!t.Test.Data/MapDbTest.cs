using Bookit.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Bookit.Domain;
using System.Data.Entity;
using System.Linq;

namespace Bookit.Test.Data
{
    [TestClass()]
    public class BookitDbTest
    {
        private TestContext testContextInstance;
        public BookitDbTest()
        {
            Database.SetInitializer<BookitDB>(new DropCreateDatabaseIfModelChanges<BookitDB>());
        }

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
        public void BookitDbConstructorTest()
        {
        }

        /// <summary>
        ///A test for Cubes
        ///</summary>
        [TestMethod()]
        public void CubesTest()
        {
            BookitDB db = new BookitDB();
            var island = db.MapNodes
                     .Where(n => n is CubeIsland)
                     .Select(n => n as CubeIsland).Include(p => p.Cubes);
            
            foreach (var i in island)
            {
                Assert.IsNotNull(i.Cubes);
            }
        }

        
    }
}
