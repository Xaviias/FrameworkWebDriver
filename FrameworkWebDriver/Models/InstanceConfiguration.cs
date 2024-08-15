using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
namespace FrameworkWebDriver.Models
{
    public class InstanceConfiguration
    {
        public int NumInstances { get; set; }
        public By OperatingSystem { get; set; }
        public By ProvisioningButton { get; set; }
        public By MachineFamily { get; set; }
        
        public By Series { get; set; }
        public By MachineType { get; set; }
        public By AddGPUsButton { get; set; }
        public By GpuModel { get; set; }
        public By NumOfGPUs { get; set; }
        public By Storage { get; set; }
        public By Region { get; set; }

        public InstanceConfiguration(int numInstances, By operatingSystem, By provisioningButton, By machineFamily, By series,
                                     By machineType, By addGPUsButton, By gpuModel, By numOfGPUs, By storage, By region)
        {
            NumInstances = numInstances;
            OperatingSystem = operatingSystem;
            ProvisioningButton = provisioningButton;
            MachineFamily = machineFamily;
            Series = series;
            MachineType = machineType;
            AddGPUsButton = addGPUsButton;
            GpuModel = gpuModel;
            NumOfGPUs = numOfGPUs;
            Storage = storage;
            Region = region;
        }
    }
}
