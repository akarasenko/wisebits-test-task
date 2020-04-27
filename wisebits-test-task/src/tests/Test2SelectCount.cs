using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace wisebits_test_task.tests
{
    [TestClass]
    public class Test2SelectCount : PageHelper
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
        public void Test2()
        {
            var cityName = TestContext.Properties["CityDataForTest2"].ToString();
            executeSQL("SELECT * FROM Customers WHERE city='" + cityName + "'");

            // as an assert
            driver.FindElement(By.XPath("//div[contains(., 'Number of Records: 6')]"));

            var recordCount = getSqlResultCount();

            Assert.AreEqual(6, recordCount);
        }

        [TestCleanup]
        public void TearDown()
        {
            stop();
        }
    }
}
