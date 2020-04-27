using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace wisebits_test_task.tests
{
    [TestClass]
    public class Test4Update : PageHelper
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            string URL = TestContext.Properties["URL"].ToString();

            init();
            goToURL(URL);

            // when you open new browser window DB is already restored
            // restoreBd()
            // if (dbIsEmpty())
            // {
            //    var elementToInsert = new DbModel("NewCustomerName", "NewContactName", "NewAddress", "NewCity", "NewPostalCode", "NewCountry");
            //    insertRecordToDb(elementToInsert);
            // }
        }

        [TestMethod]
        public void Test4()
        {
            var indexToUpdate = "1";

            var elementToUpdate = new DbModel("NewCustomerName", "NewContactName", "NewAddress", "NewCity", "NewPostalCode", "NewCountry");
            elementToUpdate.AddProperty("CustomerID", indexToUpdate);

            executeSQL("UPDATE Customers "
                + "SET CustomerName = '" + elementToUpdate.CustomerName
                + "',  ContactName = '" + elementToUpdate.ContactName
                + "',  Address = '" + elementToUpdate.Address
                + "', City = '" + elementToUpdate.City
                + "', PostalCode = '" + elementToUpdate.PostalCode
                + "', Country = '" + elementToUpdate.Country
                + "' WHERE CustomerId = " + indexToUpdate);

            waitDbUpdateFinished();

            executeSQL("SELECT * FROM Customers WHERE CustomerID = " + indexToUpdate);

            var updatedRecord = getSqlResult()[0];

            Assert.AreEqual(elementToUpdate, updatedRecord);
        }

        [TestCleanup]
        public void TearDown()
        {
            stop();
        }
    }
}
