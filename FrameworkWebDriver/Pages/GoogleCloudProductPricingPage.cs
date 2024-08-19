using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using FrameworkWebDriver.Models;

namespace FrameworkWebDriver
{
    public class GoogleCloudProductPricingPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private const string URL = "https://cloud.google.com/products/calculator";
        #region Locators
        [FindsBy(How = How.XPath, Using = "//span[text()='Add to estimate']//parent::button")]
        private IWebElement addToEstimateButton;

        [FindsBy(How = How.XPath, Using = "//h2[text()='Compute Engine']//parent::div[@role='button']")]
        private IWebElement computeEngineButton;

        [FindsBy(How = How.XPath, Using = "//input[@value='1']")]
        private IWebElement numOfInstances;

        [FindsBy(How = How.XPath, Using = "//div[@data-field-input-type='2']")]
        private IWebElement operatingSystemDropdown;

        [FindsBy(How = How.XPath, Using = "(//div[@class='VfPpkd-aPP78e'])[5]")]
        private IWebElement machineFamilyDropdown;

        [FindsBy(How = How.XPath, Using = "(//div[@class='VfPpkd-aPP78e'])[6]")]
        private IWebElement seriesDropdown;

        [FindsBy(How = How.XPath, Using = "(//div[@class='VfPpkd-aPP78e'])[7]")]
        private IWebElement machineTypeDropdown;

        [FindsBy(How = How.XPath, Using = "/html/body/c-wiz[1]/div/div/div/div[1]/div/div/div/div/div/div/div/div[1]/div/div[2]/div[3]/div[23]/div/div[1]/div/div/div/div[1]/div")]
        private IWebElement gpuModelDropdown;

        [FindsBy(How = How.XPath, Using = "(//div[@class='VfPpkd-aPP78e'])[9]")]
        private IWebElement numOfGPUsDropDown;

        [FindsBy(How = How.XPath, Using = "(//div[@class='VfPpkd-aPP78e'])[10]")]
        private IWebElement storageDropdown;

        [FindsBy(How = How.XPath, Using = "(//div[@class='VfPpkd-aPP78e'])[11]")]
        private IWebElement regionDropdown;

        [FindsBy(How = How.XPath, Using = "//button[@aria-label='Open Share Estimate dialog']")]
        private IWebElement shareButton;

        [FindsBy(How = How.XPath, Using = "//a[@track-name='open estimate summary']")]
        private IWebElement openEstimateSummary;
        #endregion
#pragma warning disable CS8618
        public GoogleCloudProductPricingPage(IWebDriver driver)
#pragma warning restore CS8618
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(_driver, this);
        }

        public void NavigateToPage()
        {
            _driver.Navigate().GoToUrl(URL);
            _driver.Manage().Window.Maximize();
            ClickElement(addToEstimateButton);
            ClickElement(computeEngineButton);
        }

        public void FillForm(InstanceConfiguration config)
        {
            ChangeNumberOfInstances(config.NumInstances);
            SelectOption(operatingSystemDropdown, _driver.FindElement(config.OperatingSystem));
            ClickElement(_driver.FindElement(config.ProvisioningButton));
            SelectOption(machineFamilyDropdown, _driver.FindElement(config.MachineFamily));
            SelectOption(seriesDropdown, _driver.FindElement(config.Series));
            SelectOption(machineTypeDropdown, _driver.FindElement(config.MachineType));
            ClickElement(_driver.FindElement(config.AddGPUsButton));
            SelectOption(gpuModelDropdown, _driver.FindElement(config.GpuModel));
            SelectOption(numOfGPUsDropDown, _driver.FindElement(config.NumOfGPUs));
            SelectOption(storageDropdown, _driver.FindElement(config.Storage));
            SelectOption(regionDropdown, _driver.FindElement(config.Region));
            OpenEstimateSummary();
        }

        private void OpenEstimateSummary()
        {
            ClickElement(shareButton);
            ClickElement(openEstimateSummary);
            CloseExtraTabs();
            ClickElement(shareButton);
            ClickElement(openEstimateSummary);
            SwitchToSummaryTab();
        }

        private void ChangeNumberOfInstances(int numInstances)
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            numOfInstances.Clear();
            numOfInstances.SendKeys(numInstances.ToString());
        }

        private void ClickElement(IWebElement element)
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            element.Click();
        }

        private void SelectOption(IWebElement dropdown, IWebElement option)
        {
            ClickElement(dropdown);
            ClickElement(option);
        }

        private void SwitchToSummaryTab()
        {
            _wait.Until(d => d.WindowHandles.Count > 1);
            var handles = _driver.WindowHandles;
            if (handles.Count > 1)
            {
                _driver.SwitchTo().Window(handles[1]);
            }
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//h4[@class='QvLFl Nh2Phe D0aEmf']")));
        }

        private void CloseExtraTabs()
        {
            _wait.Until(d => d.WindowHandles.Count > 1);
            var handles = _driver.WindowHandles;
            while (handles.Count > 1)
            {
                _driver.SwitchTo().Window(handles[handles.Count - 1]);
                _driver.Close();
                handles = _driver.WindowHandles;
            }
            _driver.SwitchTo().Window(handles[0]);
        }
    }
}