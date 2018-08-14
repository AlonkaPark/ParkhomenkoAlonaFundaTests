using OpenQA.Selenium;

namespace FundaTestProject
{
    internal class SearchResult : BasePage
    {
    
        public SearchResult(IWebDriver driver) : base(driver){}

        public bool IsVisible => SearchOutputResult.Displayed;

        public IWebElement SearchOutputResult => Driver.FindElement(By.ClassName("search-output-result-count"));
    }
}