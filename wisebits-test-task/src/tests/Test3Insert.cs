using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace wisebits_test_task.tests
{
    [TestClass]
    public class Test3Insert : PageHelper
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
        public void Test3()
        {
            var elementToInsert = new DbModel("NewCustomerName", "NewContactName", "NewAddress", "NewCity", "NewPostalCode", "NewCountry");

            // get number of records before INSERT
            executeSQL("SELECT * FROM Customers");
            var recordsNumberBefore = getSqlResultCount();

            insertRecordToDb(elementToInsert);

            // get number of records after INSERT
            executeSQL("SELECT * FROM Customers");
            var recordsNumberAfter = getSqlResultCount();

            Assert.AreEqual(recordsNumberBefore + 1, recordsNumberAfter);

            executeSQL("SELECT * FROM Customers WHERE CustomerID = " + recordsNumberAfter.ToString());

            var insertedRecord = getSqlResult()[0];

            elementToInsert.AddProperty("CustomerID", recordsNumberAfter.ToString());

            Assert.AreEqual(elementToInsert, insertedRecord);
        }

        [TestCleanup]
        public void TearDown()
        {
            stop();
        }
    }
}
