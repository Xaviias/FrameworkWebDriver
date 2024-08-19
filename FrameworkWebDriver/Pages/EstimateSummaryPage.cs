using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace FrameworkWebDriver
{
    public class EstimateSummaryPage
    {
        private readonly IWebDriver _driver;
        #region Locators
        [FindsBy(How = How.XPath, Using = "//span[text()='Number of Instances']//following-sibling::span")]
        private IWebElement numInstances;

        [FindsBy(How = How.XPath, Using = "//span[text()='Operating System / Software']//following-sibling::span")]
        private IWebElement operatingSystem;

        [FindsBy(How = How.XPath, Using = "//span[text()='Provisioning Model']//following-sibling::span")]
        private IWebElement provisioningModel;

        [FindsBy(How = How.XPath, Using = "//span[text()='Machine type']//following-sibling::span")]
        private IWebElement machineType;

        [FindsBy(How = How.XPath, Using = "//span[text()='GPU Model']//following-sibling::span")]
        private IWebElement gpuModel;

        [FindsBy(How = How.XPath, Using = "//span[text()='Number of GPUs']//following-sibling::span")]
        private IWebElement numOfGPUs;

        [FindsBy(How = How.XPath, Using = "//span[text()='Local SSD']//following-sibling::span")]
        private IWebElement storage;

        [FindsBy(How = How.XPath, Using = "//span[text()='Region']//following-sibling::span")]
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