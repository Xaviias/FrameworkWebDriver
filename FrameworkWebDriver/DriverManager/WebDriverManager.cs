using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace FrameworkWebDriver
{
    public enum BrowserType
    {
        Chrome,
        Edge
    }

    public class WebDriverManager
    {
        public IWebDriver GetWebDriver(BrowserType browserType)
        {
            IWebDriver driver = browserType switch
            {
                BrowserType.Chrome => new ChromeDriver(),
                BrowserType.Edge => new EdgeDriver(),
                _ => throw new ArgumentOutOfRangeException(nameof(browserType), browserType, null)
            };
            return driver;
        }
    }
}