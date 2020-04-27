using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace wisebits_test_task.tests
{
    [TestClass]
    public class Test1Select : PageHelper
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
        public void Test1()
        {          
            var contactName = TestContext.Properties["ContactNameDataForTest1"].ToString();
            var address = TestContext.Properties["AddressDataForTest1"].ToString();

            executeSQL("SELECT * FROM Customers");

            var resultRecords = getSqlResult();

            var recordWithSpecialContractAddress = new List<DbModel>();

            foreach (var record in resultRecords)
            {
                if (record.ContactName == contactName)
                {
                    recordWithSpecialContractAddress.Add(record);
                }
            }

            Assert.AreEqual(1, recordWithSpecialContractAddress.Count);
            Assert.AreEqual(address, recordWithSpecialContractAddress[0].Address);
        }

        [TestCleanup]
        public void TearDown()
        {
            stop();
        }
    }
}
