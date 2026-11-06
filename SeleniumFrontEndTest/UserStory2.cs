using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumFrontEndTest
{
    public class UserStory2 : IDisposable
    {
        // Local way (visible browser, driver from C:\webdrivers):
    // private string DriverDirectory = "C:\\webdrivers";

    private IWebDriver _driver;
    private readonly WebDriverWait wait;
    private const string URL = "https://frontend-ten-liart-ijufbzpsx4.vercel.app";

    public UserStory2()
    {
        // Local way:
        // _driver = new ChromeDriver(DriverDirectory);

        // GitHub Actions way (headless, driver found by Selenium Manager):
        var options = new ChromeOptions();
        options.AddArgument("--headless=new");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");
        options.AddArgument("--window-size=1920,1080");
        _driver = new ChromeDriver(options);

        wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
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
            _driver.FindElement(By.Id("search-button")).Click();
        
            try
            {
                wait.Until(d => d.FindElements(By.ClassName("table-row")).Count == 4);
            }
            catch (WebDriverTimeoutException)
            {
                int actual = _driver.FindElements(By.ClassName("table-row")).Count;
                ((ITakesScreenshot)_driver).GetScreenshot().SaveAsFile("genre-fail.png");
                Assert.Fail($"Expected 3 rows, found {actual}");
            }
        }

        //Search by publication year and verify only records from that year appear.


        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
