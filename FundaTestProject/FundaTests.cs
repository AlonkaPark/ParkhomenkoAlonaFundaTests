using System.IO;
using System.Reflection;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FundaTestProject
{
    [TestClass]
    [TestCategory("FundaTest")]
    public class FundaTests
    {
        private IWebDriver Driver { get; set; }
        internal TestSearch TheTestSearch { get; private set; }
        public IWebElement LastSearch => Driver.FindElement(By.ClassName("link-alternative"));

        [TestInitialize]
        public void SetupForEveryTestMethod()
        {
            Driver = GetChromeDriver();
            TheTestSearch = new TestSearch();
            TheTestSearch.City = "";
            TheTestSearch.Ray = 0;
            TheTestSearch.MinAmount = "other";
            TheTestSearch.MaxAmount = "ignore_filter";
        }

        [TestMethod]
        public void SearchByCity()
        {
            TheTestSearch.City = "Hilversum";
            TheTestSearch.Ray = 0;
            TheTestSearch.MinAmount = "other";
            TheTestSearch.MaxAmount = "ignore_filter";
            var sampleApplicationPage = new SampleApplicationPage(Driver);
            sampleApplicationPage.GoTo();
            var searchResult = sampleApplicationPage.FillOutFormAndSubmit(TheTestSearch);
            Assert.IsTrue(searchResult.IsVisible, "Page with search results was not visible.");
        }

        [TestMethod]
        public void SearchByCityAndMinAmount()
        {
            TheTestSearch.City = "Amersfoort";
            TheTestSearch.MinAmount = "150000";
            var sampleApplicationPage = new SampleApplicationPage(Driver);
            sampleApplicationPage.GoTo();
            var searchResult = sampleApplicationPage.FillOutFormAndSubmit(TheTestSearch);
            Assert.IsTrue(searchResult.IsVisible, "Page with search results was not visible.");
        }

        [TestMethod]
        public void SearchByCityAndMaxAmount()
        {
            TheTestSearch.City = "Amersfoort";
            TheTestSearch.MaxAmount = "2000000";
            var sampleApplicationPage = new SampleApplicationPage(Driver);
            sampleApplicationPage.GoTo();
            var searchResult = sampleApplicationPage.FillOutFormAndSubmit(TheTestSearch);
            Assert.IsTrue(searchResult.IsVisible, "Page with search results was not visible.");
        }

        [TestMethod]
        public void SearchByCityAndMinMaxAmountAndRay()
        {
            TheTestSearch.City = "Hilversum";
            TheTestSearch.Ray = 5;
            TheTestSearch.MinAmount = "150000";
            TheTestSearch.MaxAmount = "2000000";
            var sampleApplicationPage = new SampleApplicationPage(Driver);
            sampleApplicationPage.GoTo();
            var searchResult = sampleApplicationPage.FillOutFormAndSubmit(TheTestSearch);
            Assert.IsTrue(searchResult.IsVisible, "Page with search results was not visible.");
        }

        [TestMethod]
        public void SearchByCityAndRay()
        {
            TheTestSearch.City = "Hilversum";
            TheTestSearch.Ray = 5;
            var sampleApplicationPage = new SampleApplicationPage(Driver);
            sampleApplicationPage.GoTo();
            var searchResult = sampleApplicationPage.FillOutFormAndSubmit(TheTestSearch);
            Assert.IsTrue(searchResult.IsVisible, "Page with search results was not visible.");
        }

        [TestMethod]
        public void SearchByMinMaxAmount()
        {
            TheTestSearch.MinAmount = "0";
            TheTestSearch.MaxAmount = "2000000";
            var sampleApplicationPage = new SampleApplicationPage(Driver);
            sampleApplicationPage.GoTo();
            var searchResult = sampleApplicationPage.FillOutFormAndSubmit(TheTestSearch);
            Assert.IsTrue(searchResult.IsVisible, "Page with search results was not visible.");
        }

        [TestMethod]
        public void SubmitSearchAndGoBack()
        {
            var sampleApplicationPage = new SampleApplicationPage(Driver);
            sampleApplicationPage.GoTo();
            var searchResult = sampleApplicationPage.FillOutFormAndSubmit(TheTestSearch);
            Assert.IsTrue(searchResult.IsVisible, "Page with search results was not visible.");
            sampleApplicationPage.GoToMainPage();
        }

        [TestMethod]
        public void SubmitSearchGoBackAndCheckPreviousSearch()
        {
            var sampleApplicationPage = new SampleApplicationPage(Driver);
            sampleApplicationPage.GoTo();
            var searchResult = sampleApplicationPage.FillOutFormAndSubmit(TheTestSearch);
            Assert.IsTrue(searchResult.IsVisible, "Page with search results was not visible.");
            sampleApplicationPage.GoToMainPage();
            Assert.IsTrue(LastSearch.Text.Contains("Nederland"), "Quick search is not available");
        }

        private IWebDriver GetChromeDriver()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return new ChromeDriver(outPutDirectory);
        }

        [TestCleanup]
        public void CleanUp()
        {
            Driver.Close();
            Driver.Quit();
        }
 
    }
}
