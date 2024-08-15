using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace FrameworkWebDriver
{
    public class EstimateSummaryPage
    {
        private readonly IWebDriver _driver;
        #region Locators
        [FindsBy(How = How.XPath, Using = "(//span[@class='Kfvdz'])[10]")]
        private IWebElement numInstances;

        [FindsBy(How = How.XPath, Using = "(//span[@class='Kfvdz'])[11]")]
        private IWebElement operatingSystem;

        [FindsBy(How = How.XPath, Using = "(//span[@class='Kfvdz'])[12]")]
        private IWebElement provisioningModel;

        [FindsBy(How = How.XPath, Using = "(//span[@class='Kfvdz'])[3]")]
        private IWebElement machineType;

        [FindsBy(How = How.XPath, Using = "(//span[@class='Kfvdz'])[5]")]
        private IWebElement gpuModel;

        [FindsBy(How = How.XPath, Using = "(//span[@class='Kfvdz'])[6]")]
        private IWebElement numOfGPUs;

        [FindsBy(How = How.XPath, Using = "(//span[@class='Kfvdz'])[7]")]
        private IWebElement storage;

        [FindsBy(How = How.XPath, Using = "(//span[@class='Kfvdz'])[18]")]
        private IWebElement region;
        #endregion
#pragma warning disable CS8618
        public EstimateSummaryPage(IWebDriver driver)
#pragma warning restore CS8618
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        public Dictionary<string, string> ExtractSummaryDetails()
        {
            return new Dictionary<string, string>
            {
                { "numInstances", numInstances.Text },
                { "operatingSystem", operatingSystem.Text },
                { "provisioningModel", provisioningModel.Text },
                { "machineType", machineType.Text },
                { "gpuModel", gpuModel.Text },
                { "numOfGPUs", numOfGPUs.Text },
                { "storage", storage.Text },
                { "region", region.Text }
            };
        }
    }
}