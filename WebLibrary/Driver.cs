using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace WebLibrary
{
    class Driver
    {
        public static IWebDriver CreateDriver(string path)
        {
            try
            {
                IWebDriver driver;
                EdgeOptions edgeOptions = new EdgeOptions();
                EdgeDriverService driverService;
                edgeOptions.BinaryLocation = path;
                edgeOptions.AddArgument("headless");
                edgeOptions.AddArgument("disable-gpu");
                driverService = EdgeDriverService.CreateDefaultService();
                driverService.HideCommandPromptWindow = true;
                driver = new EdgeDriver(driverService, edgeOptions);
                return driver;
            }
            catch
            {

            }
            return null;
        }
    }
}
