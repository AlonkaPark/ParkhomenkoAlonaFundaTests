using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FundaTestProject
{
    internal class SampleApplicationPage : BasePage
    {
        public SampleApplicationPage(IWebDriver driver) : base(driver){}

        public bool IsVisible {
            get
            {
                return Driver.FindElement(By.XPath("//*[@type='submit']")).Text.Contains("Zoek");
            }
            internal set { } }

        public IWebElement CityNameField => Driver.FindElement(By.Id("autocomplete-input"));

        public IWebElement SearchButton => Driver.FindElement(By.XPath("//*[@class='search-block__submit']/button"));

        public IWebElement PriceFrom => Driver.FindElement(By.Id("range-filter-selector-select-filter_koopprijsvan"));

        public IWebElement PriceTo => Driver.FindElement(By.Id("range-filter-selector-select-filter_koopprijstot"));

        public IWebElement Ray => Driver.FindElement(By.Id("Straal"));

        public IWebElement GoBack => Driver.FindElement(By.ClassName("logo"));

        internal void GoTo()
        {
            Driver.Navigate().GoToUrl("https://www.funda.nl");
            Assert.IsTrue(IsVisible, "Main Funda page was not visible.");
        }

        internal SearchResult FillOutFormAndSubmit(TestSearch search)
        {
            CityNameField.SendKeys(search.City);
            CityNameField.SendKeys(Keys.Space);
            var selectRay = new SelectElement(Ray);
            selectRay.SelectByValue(search.Ray.ToString());
            var selectMinPrice = new SelectElement(PriceFrom);
            selectMinPrice.SelectByValue(search.MinAmount);
            var selectMaxPrice = new SelectElement(PriceTo);
            selectMaxPrice.SelectByValue(search.MaxAmount);
            SearchButton.Click();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            return new SearchResult(Driver);
        }

        internal void GoToMainPage()
        {
            GoBack.Click();
            Assert.IsTrue(IsVisible, "Main Funda page was not visible.");
        }
    }
}
