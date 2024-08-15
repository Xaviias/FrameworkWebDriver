using NUnit.Framework;
using OpenQA.Selenium;
using FrameworkWebDriver.Models;
using FrameworkWebDriver.Utils;
using NUnit.Framework.Interfaces;

namespace FrameworkWebDriver.Tests
{
    [TestFixture]
    public class GoogleCloudPricingTests
    {
        private IWebDriver _driver;
        private GoogleCloudProductPricingPage _pricingPage;
        private EstimateSummaryPage _summaryPage;
        private Dictionary<string, string> expectedValues;
        private InstanceConfiguration config;

        [SetUp]
        public void SetUp()
        {
            string environment = Environment.GetEnvironmentVariable("ENVIRONMENT") ?? "qa";
            string browser = Environment.GetEnvironmentVariable("BROWSER") ?? "Chrome";

            var propertyReader = new PropertyReader(environment);

            BrowserType browserType = Enum.TryParse(browser, true, out BrowserType parsedBrowserType)
                                      ? parsedBrowserType
                                      : BrowserType.Chrome;

            var webDriverManager = new WebDriverManager();
            _driver = webDriverManager.GetWebDriver(browserType);
            _pricingPage = new GoogleCloudProductPricingPage(_driver);
            _summaryPage = new EstimateSummaryPage(_driver);
            _pricingPage.NavigateToPage();
            expectedValues = new Dictionary<string, string>
            {
                { "numInstances", "4" },
                { "operatingSystem", "Free: Debian, CentOS, CoreOS, Ubuntu or BYOL (Bring Your Own License)" },
                { "provisioningModel", "Regular" },
                { "machineType", "n1-standard-8, vCPUs: 8, RAM: 30 GB" },
                { "gpuModel", "NVIDIA V100" },
                { "numOfGPUs", "1" },
                { "storage", "2x375 GB" },
                { "region", "Netherlands (europe-west4)" }
            };
            config = new InstanceConfiguration(
               numInstances: propertyReader.GetIntProperty("numInstances"),
               operatingSystem: By.XPath(propertyReader.GetProperty("operatingSystem")),
               provisioningButton: By.XPath(propertyReader.GetProperty("provisioningModelButton")),
               machineFamily: By.XPath(propertyReader.GetProperty("machineFamily")),
               series: By.XPath(propertyReader.GetProperty("series")),
               machineType: By.XPath(propertyReader.GetProperty("machineType")),
               addGPUsButton: By.XPath(propertyReader.GetProperty("addGPUsButton")),
               gpuModel: By.XPath(propertyReader.GetProperty("gpuModel")),
               numOfGPUs: By.XPath(propertyReader.GetProperty("numOfGPUs")),
               storage: By.XPath(propertyReader.GetProperty("storage")),
               region: By.XPath(propertyReader.GetProperty("region"))
           );
        }

        [Test]
        public void VerifyCostEstimateSummary()
        {
            _pricingPage.FillForm(config);

            var summaryValues = _summaryPage.ExtractSummaryDetails();

            foreach (var key in expectedValues.Keys)
            {
                Assert.That(summaryValues[key], Is.EqualTo(expectedValues[key]), $"Mismatch in {key}");
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                ScreenshotHelper.TakeScreenshot(_driver, TestContext.CurrentContext.Test.Name);
            }

            _driver.Quit();
            _driver.Dispose();
        }
    }
}