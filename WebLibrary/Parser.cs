using System.Windows;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using OpenQA.Selenium;

namespace WebLibrary
{
    public class Parser
    {
        IWebDriver driver;
        public Parser()
        {
            CreateDriver();
        }
        public async Task<int> GetPriceAsync(string link)
        {
            Match match = null;
            int iteration = 0;
            await Task.Run(() =>
            {
                driver.Navigate().GoToUrl(link);
                string pattern = "\"price\":(.*?),";
                string line = driver.PageSource;
                do
                {
                    match = Regex.Match(line, pattern);
                    iteration++;
                }
                while (match.Groups.Count == 0 && iteration != 100);
            });
            if (match.Groups.Count == 0)
                return -1;
            return int.Parse(match.Groups[1].Value);
        }

        public void CreateDriver()
        {
            driver = Driver.CreateDriver();
            if (!GetDriver())
                MessageBox.Show("Ошибка сборки драйвера!");
        }
        public void DisposeDriver()
        {
            driver.Quit();
            driver.Close();
            driver.Dispose();
        }
        public bool GetDriver()
        {
            return driver != null;
        }
    }
}
