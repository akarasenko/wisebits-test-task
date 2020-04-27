using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace wisebits_test_task
{
    public class PageHelper : DriverHelper
    {
        private string runButtonCss = "button.w3-green";
        private string restoreDbButtonCss = "button#restoreDBBtn";
        private string dbRestoredAckXpath = "//div[contains(., 'The database is fully restored.')]";
        private string resultTableCss = "div#divResultSQL>div>table";
        private string dbUpdatedXpath = "//div[contains(., 'You have made changes to the database. Rows affected: 1')]";

        protected void restoreBd()
        {
            driver.FindElement(By.CssSelector(restoreDbButtonCss)).Click();
            IAlert alert = getAlert();
            alert.Accept();
            
            wait.Until(driver => driver.FindElement(By.XPath(dbRestoredAckXpath)));
        }

        protected bool dbIsEmpty()
        {
            executeSQL("SELECT * FROM Customers");

            return (getSqlResultCount() == 0);
        }

        protected void insertRecordToDb(DbModel elementToInsert)
        {
           executeSQL("INSERT INTO Customers ( CustomerName, ContactName, Address, City, PostalCode, Country)"
                + "VALUES ('" + elementToInsert.CustomerName + "', '"
                + elementToInsert.ContactName + "', '"
                + elementToInsert.Address + "', '"
                + elementToInsert.City + "', '"
                + elementToInsert.PostalCode + "', '"
                + elementToInsert.Country + "')");

            waitDbUpdateFinished();
        }

        protected void executeSQL(string SQLScript)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            var script = @"window.editor.setValue("" " + SQLScript + @" "")";
            js.ExecuteScript(script);

            driver.FindElement(By.CssSelector(runButtonCss)).Click();
        }

        protected void waitDbUpdateFinished()
        {
            wait.Until(driver => driver.FindElement(By.XPath(dbUpdatedXpath)));
        }

        protected int getSqlResultCount()
        {
            var table = driver.FindElement(By.CssSelector(resultTableCss));
            var rows = table.FindElements(By.TagName("tr"));

            return rows.Count - 1;
        }

        protected List<DbModel> getSqlResult()
        {
            var result = new List<DbModel>();

            var table = driver.FindElement(By.CssSelector(resultTableCss));

            var rows = table.FindElements(By.TagName("tr"));
            var titleValues = getValuesFromRow(rows[0], "th");

            for (var i = 1; i < rows.Count; i++)
            {
                var element = new DbModel();

                var values = getValuesFromRow(rows[i], "td");

                for (var j = 0; j < values.Count; j++)
                {
                    element.AddProperty(titleValues[j], values[j]);
                }

                result.Add(element);
            }

            return result;
        }
       
        private List<string> getValuesFromRow(IWebElement row, string tagName)
        {
            var result = new List<string>();
            var fields = row.FindElements(By.TagName(tagName));
            foreach (var field in fields)
            {
                result.Add(field.Text);
            }

            return result;
        }
    }
}
