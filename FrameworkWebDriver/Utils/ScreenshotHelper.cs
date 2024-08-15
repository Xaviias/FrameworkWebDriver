using OpenQA.Selenium;
using System;
using System.IO;

namespace FrameworkWebDriver.Utils
{
    public static class ScreenshotHelper
    {
        public static void TakeScreenshot(IWebDriver driver, string testName)
        {
            try
            {
                string screenshotsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
                if (!Directory.Exists(screenshotsDirectory))
                {
                    Directory.CreateDirectory(screenshotsDirectory);
                }
                string timestamp = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string screenshotFileName = $"{testName}_{timestamp}.png";

                string screenshotFilePath = Path.Combine(screenshotsDirectory, screenshotFileName);

                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(screenshotFilePath);

                if (File.Exists(screenshotFilePath))
                {
                    Console.WriteLine($"Screenshot saved: {screenshotFilePath}");
                }
                else
                {
                    Console.WriteLine($"Screenshot file not found after saving attempt: {screenshotFilePath}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to take screenshot: {e.Message}");
            }
        }
    }
}
