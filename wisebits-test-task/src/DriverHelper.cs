using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;


namespace wisebits_test_task
{    public class DriverHelper
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
       
        protected void init()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");

            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        protected void goToURL(string URL)
        {
            driver.Navigate().GoToUrl(URL);
        }

        protected IAlert getAlert()
        {
            IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert();

            return alert;
        }

        protected void stop()
        {
            driver.Quit();
        }
    }
}
