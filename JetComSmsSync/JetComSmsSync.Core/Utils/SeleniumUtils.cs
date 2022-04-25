using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Serilog;
using System;
using System.Threading;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace JetComSmsSync.Core.Utils
{
    public static class SeleniumUtils
    {
        public static WebDriver GetChromeWebDriver()
        {
            Log.Debug("Getting chrome web driver");
            var version = new ChromeConfig().GetMatchingBrowserVersion();
            new DriverManager().SetUpDriver(new ChromeConfig(), version);
            var options = new ChromeOptions();
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();

            service.HideCommandPromptWindow = true;
            service.SuppressInitialDiagnosticInformation = true;
            options.AddExcludedArgument("enable-automation");
            //options.AddArgument("--headless");  

            Log.Debug("Creating new chrome driver");
            var driver = new ChromeDriver(service, options);
            driver.Manage().Window.Maximize();
            return driver;
        }

        public static void LoginToRSP(RemoteWebDriver driver, string username, string password)
        {
            Log.Debug("Logging to the RSP");
            driver.Navigate().GoToUrl("https://rsp.national.aaa.com/app/login");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(300));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"username-input\"]"))).SendKeys(username);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"content\"]/div/div/div[2]/div[3]/div/div[2]/div/div/div[2]/div/span/input"))).SendKeys(password);
            Log.Debug("Open login page");
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"content\"]/div/div/div[2]/div[4]/div/div/button[2]"))).Click();
            Thread.Sleep(3000);
        }
    }
}
