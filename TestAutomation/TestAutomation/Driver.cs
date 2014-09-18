using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomation
{

    [TestFixture]
    public class Driver
    {
        IWebDriver driver;


        [SetUp]
        public void Setup()
        {
            driver = new FirefoxDriver();
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 30));
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }


        [Test]
        public void TestMethod1()
        {
            driver.Navigate().GoToUrl("http://www.google.com");
            IWebElement ele=driver.FindElement(By.Name("q"));
            ele.SendKeys("selenium");
            ele.SendKeys(Keys.Enter);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until((d) => { return d.Title.StartsWith("selenium"); });
            Assert.AreEqual("selenium - Google Search", driver.Title);
            //driver.FindElement(By.LinkText(" - Web Browser Automation")).Click();
        }
    }
}
