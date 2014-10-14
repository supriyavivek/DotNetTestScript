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

        //[SetUp]
        //public void Setup()
        //{
           
        //    driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 30));
        //}

        //[TearDown]
        //public void Teardown()
        //{
        //    driver.Quit();
        //}

        [Test]
        public void test1Login()
        {
            driver = new FirefoxDriver();
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 30));
            driver.Navigate().GoToUrl("http://ipsupriyav:7070/login.do");
            IWebElement userName=driver.FindElement(By.Id("username"));
            userName.SendKeys("admin");
            IWebElement password = driver.FindElement(By.Name("pwd"));
            password.SendKeys("manager");
            IWebElement login = driver.FindElement(By.Id("loginButton"));
            login.Click();
        }

        [Test]
        public void test2AddTasks()
        {
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 30));
            IWebElement tasksTab=driver.FindElement(By.LinkText("Tasks"));
            tasksTab.Click();

            IWebElement createTasks=driver.FindElement(By.Id("ext-gen33"));
            createTasks.Click();

            IWebElement customerName = driver.FindElement(By.Name("customerName"));
            customerName.SendKeys("Indpro");

            IWebElement projectName = driver.FindElement(By.Name("projectName"));
            projectName.SendKeys("Assistansboken");

            IWebElement taskName = driver.FindElement(By.Id("task[0].name"));
            taskName.SendKeys("Testing");

            IWebElement deadLine = driver.FindElement(By.Id("ext-gen7"));
            deadLine.Click();

            IWebElement todaysDate = driver.FindElement(By.XPath("//button[text()='today']"));
            todaysDate.Click();

            IWebElement createTaskButton = driver.FindElement(By.ClassName("hierarchy_element_wide_button"));
            createTaskButton.Click();

            IWebElement custSuccessMsg = driver.FindElement(By.XPath(".//*[@id='SuccessMessages']/tbody/tr[1]/td/span"));
            String cMsg=custSuccessMsg.Text;
            Assert.AreEqual("Customer \"Indpro\" was created.", cMsg);

            IWebElement projSuccessMsg = driver.FindElement(By.XPath(".//*[@id='SuccessMessages']/tbody/tr[2]/td/span"));
            String pMsg = projSuccessMsg.Text;
            Assert.AreEqual("Project \"Assistansboken\" was created.", pMsg);

            IWebElement taskSuccessMsg = driver.FindElement(By.XPath(".//*[@id='SuccessMessages']/tbody/tr[3]/td/span"));
            String tMsg = taskSuccessMsg.Text;
            Assert.AreEqual("1 new task was added to the customer \"Indpro\", project \"Assistansboken\".", tMsg);

            IWebElement customerAddedSuccessfully = driver.FindElement(By.XPath(".//*[@id='tasksListForm']/table/tbody/tr[2]/td/table/tbody/tr[3]/td[1]"));
            String cName = customerAddedSuccessfully.Text;
            Assert.AreEqual("Indpro", cName);
        }

        [Test]
        public void test3EditTask()
        {
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 30));
            IWebElement edit = driver.FindElement(By.XPath("//a[contains(@href, '/tasks/task_details.do?')]"));
            edit.Click();

            IWebElement editParameter = driver.FindElement(By.XPath("//a[contains(@href, '/tasks/otaskedit.do?')]"));
            editParameter.Click();

            SelectElement select = new SelectElement(driver.FindElement(By.Name("billingTypeId")));
            select.SelectByText("Billable");

            IWebElement description = driver.FindElement(By.Name("description"));
            description.SendKeys("task description");

            IWebElement saveChanges = driver.FindElement(By.XPath(".//input[@value='Save Changes']"));
            saveChanges.Click();

            IWebElement changesSaved = driver.FindElement(By.XPath(".//*[@id='SuccessMessages']/tbody/tr/td/span"));
            String successMessage=changesSaved.Text;
            Assert.AreEqual("Your changes have been saved.", successMessage);
          
            IWebElement clickOnEnterComment = driver.FindElement(By.XPath(".//*[@id='content']/table/tbody/tr[4]/td/table[1]/tbody/tr/td[2]/a"));
            clickOnEnterComment.Click();

            IWebElement enterComment = driver.FindElement(By.Id("editDescriptionPopupText"));
            enterComment.SendKeys("task comments");

            IWebElement saveComment = driver.FindElement(By.Id("scbutton"));
            saveComment.Click();

            IWebElement comment = driver.FindElement(By.Id("todayComment"));
            String commentMsg = comment.Text;
            Assert.AreEqual("task comments", commentMsg);

            IWebElement dateField = driver.FindElement(By.XPath(".//*[@id='content']/table/tbody/tr[4]/td/table[2]/tbody/tr[2]/td[1]"));
            String dateText = dateField.Text;

            IWebElement taskCreationDate = driver.FindElement(By.XPath(".//*[@id='content']/table/tbody/tr[3]/td/table/tbody/tr[8]/td[2]/table/tbody/tr/td[1]"));
            String date = taskCreationDate.Text;

            Assert.AreEqual(date, dateText);          
        }

        [Test]
        public void test4DeleteTaskAndCustomer()
        {
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 30));
            IWebElement deleteTaskButton=driver.FindElement(By.ClassName("hierarchy_element_wide_button"));
            deleteTaskButton.Click();

            IWebElement deleteTask = driver.FindElement(By.Id("deleteButton"));
            deleteTask.Click();

            IWebElement projectAndCustomers = driver.FindElement(By.LinkText("Projects & Customers"));
            projectAndCustomers.Click();

            IWebElement checkCustomer = driver.FindElement(By.XPath("//input[contains(@name, 'customers')]"));
            checkCustomer.Click();

            IWebElement deleteSelected = driver.FindElement(By.XPath("//input[@value= 'Delete Selected']"));
            deleteSelected.Click();

            IWebElement delete = driver.FindElement(By.Id("deleteButton"));
            delete.Click();

            IWebElement successMsg = driver.FindElement(By.XPath(".//*[@id='SuccessMessages']/tbody/tr/td/span"));
            String msg=successMsg.Text;
            Assert.AreEqual("Selected customers and projects have been successfully deleted.", msg);

            IWebElement logout = driver.FindElement(By.Id("logoutLink"));
            logout.Click();

            driver.Quit();
        }
    }
}