using DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace TestProject1
{
    
    
    /// <summary>
    ///This is a test class for DB_SERVERTest and is intended
    ///to contain all DB_SERVERTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DB_SERVERTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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


        /// <summary>
        ///A test for DB_SERVER Constructor
        ///</summary>
        [TestMethod()]
        public void DB_SERVERConstructorTest()
        {
            DB_SERVER db = new DB_SERVER();
            var u =db.User.FirstOrDefault(e => e.UserId == "1");
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
