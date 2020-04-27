using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace wisebits_test_task.tests
{
    [TestClass]
    public class Test5InvalidSqlQuery : PageHelper
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            string URL = TestContext.Properties["URL"].ToString();

            init();
            goToURL(URL);

            // when you open new browser window DB is already restored
            // restoreBd();
        }

        [TestMethod]
        public void InvalidSqlQuery()
        {
            executeSQL("invalid SQL query");

            IAlert alert = getAlert();

            Assert.AreEqual(@"Error 1: could not prepare statement (1 near ""invalid"": syntax error)", alert.Text);
        }

        [TestCleanup]
        public void TearDown()
        {
            stop();
        }
    }
}
