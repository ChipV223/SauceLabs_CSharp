using NUnit.Framework;
using PNUnit.Framework;
using System;
using System.Threading;
using System.Web;
using System.Text;
using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace SauceLabs.NUnitExample
{
    [TestFixture()]
    public class SaucePNUnit_Test
    {
        private IWebDriver driver;
        private string[] testParams;

        [SetUp]
        public void Init()
        {
            testParams = PNUnitServices.Get().GetTestParams();
            String params1 = String.Join(",", testParams);
            Console.WriteLine(params1);
            String browser = testParams[0];
            String version = testParams[1];
            String platform = testParams[2];
            DesiredCapabilities caps = new DesiredCapabilities();
            caps.SetCapability("browserName", browser);
            caps.SetCapability(CapabilityType.Version, version);
            caps.SetCapability(CapabilityType.Platform, platform);
            caps.SetCapability("username", Constants.SAUCE_LABS_ACCOUNT_NAME);
            caps.SetCapability("accessKey", Constants.SAUCE_LABS_ACCOUNT_KEY);
            caps.SetCapability("name", TestContext.CurrentContext.Test.Name);

            Console.WriteLine("Capabilities" + caps.ToString());

            driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps, TimeSpan.FromSeconds(600));
            //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(180));
        }


        [Test]
        public void googleTest()
        {
            driver.Navigate().GoToUrl("http://www.google.com");
            StringAssert.Contains("Google", driver.Title);
            IWebElement query = driver.FindElement(By.Name("q"));
            query.SendKeys("Sauce Labs");
            query.Submit();
        }

        [TearDown]
        public void Cleanup()
        {
            // Gets the status of the current test
            bool passed = TestContext.CurrentContext.Result.Status == TestStatus.Passed;
            try
            {
                // Logs the result to Sauce Labs
                ((IJavaScriptExecutor)driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
            }
            finally
            {
                // Terminates the remote webdriver session
                driver.Quit();
            }
            
        }
    }
}
