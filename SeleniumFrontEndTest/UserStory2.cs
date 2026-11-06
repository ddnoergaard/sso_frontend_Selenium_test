using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumFrontEndTest
{
    public class UserStory2 : IDisposable
    {
        private string DriverDirectory = "C:\\webdrivers";
        private static IWebDriver _driver;
        private const string URL = "https://frontend-ten-liart-ijufbzpsx4.vercel.app";
        private WebDriverWait wait;
        public UserStory2()
        {
            _driver = new ChromeDriver(DriverDirectory);
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(2));
        }

        //Type a known title (e.g. “Billie Jean”) and verify the matching record appears in the list.
        [Fact]
        public void VerifyTitleApearsInSearch()
        {
            string searchTitle = "Billie Jean";
            _driver.Navigate().GoToUrl(URL);

            wait.Until(d => _driver.FindElement(By.ClassName("tr-th")).Displayed);
            IWebElement searchField = _driver.FindElement(By.Id("title-search-bar"));
            searchField.SendKeys(searchTitle);
            IWebElement searchButton = _driver.FindElement(By.Id("search-button"));
            searchButton.Click();
            wait.Until(d => _driver.FindElement(By.Id("2-billie-jean")).FindElement(By.ClassName("title")).Displayed);
            string searchResult = _driver.FindElement(By.Id("2-billie-jean")).FindElement(By.ClassName("title")).Text;

            //string actualOutput = searchResult.Text;

            Assert.Equal(searchTitle, searchResult);
        }


        //Type a known genre and verify all of that artist’s records appear.
        [Fact]
        public void MultipleRecordsSearch()
        {
            string genreName = "pop";
            _driver.Navigate().GoToUrl(URL);
            wait.Until(d => _driver.FindElement(By.ClassName("tr-th")).Displayed);
            IWebElement searchField = _driver.FindElement(By.Id("genre-search-bar"));
            searchField.SendKeys(genreName);
            IWebElement searchButton = _driver.FindElement(By.Id("search-button"));
            searchButton.Click();
            wait.Until(d => d.FindElements(By.ClassName("table-row")).Count() == 3);
            IEnumerable<IWebElement> searchResult = _driver.FindElements(By.ClassName("table-row"));

            int count = searchResult.Count();

            Assert.Equal(3, count);
        }

        //Search by publication year and verify only records from that year appear.


        public void Dispose()
        {
            _driver.Dispose();
        }
    }
}
