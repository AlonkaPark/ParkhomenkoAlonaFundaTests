using OpenQA.Selenium;

namespace FundaTestProject
{
    internal class BasePage
    {
        protected IWebDriver Driver { get; set; }

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
        }
    }
}