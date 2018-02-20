
using MVCAppTest.Controllers.UITest.Common;
using MVCAppTest.Controllers.UITest.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Threading;
using NUnit.Framework;
namespace MVCAppTest.Controllers.UITest.TestCases
{


   [TestFixture("chrome", "63", "Windows 7")]
      
    public class WUITest : UITest
    {
         private IWebDriver driver;
          private string browser;
          private string version;
          private string os;

         public WUITest()
            {

            }
        public WUITest(string browser, string version, string os) : 
            base(browser, version, os)
        {
                         

        }


        /**
        * a normal member  
        * @param s a constant character pointer.
        * @see Can_Navigate_To_GoToHome()
        * @see ~Can_Navigate_To_GoToHome()
        * @see Can_Navigate_To_GoToHome()
        * @see publicVar()
        * @return The test results
        */
        [Test]

        public void Can_Navigate_To_GoToHome()
        {
            HomePage.GoTo();

            System.Diagnostics.Debug.WriteLine(HomePage.Name);
            Console.WriteLine(HomePage.Name);
            Console.Write("MyPage.Name" + HomePage.Name);
            // Driver.driver.Manage().Window.Maximize();

            HomePage.home("Log in");
            Driver.driver.FindElement(By.Id("loginLink")).Click();

            Thread.Sleep(2000);
            NUnit.Framework.Assert.AreEqual(HomePage.Name, "Log in");
         

        }


        [Test]
        public void Can_Navigate_To_Login()


        {

            HomePage.home("Log in");
            //Driver.driver.Manage().Window.Maximize();

            Thread.Sleep(3000);

            Driver.driver.FindElement(By.Id("loginLink")).Click();


            Thread.Sleep(4000);

            Driver.driver.FindElement(By.Id("registerLink")).Click();

            LoginPage.register("Register");

            Thread.Sleep(4000);

            NUnit.Framework.Assert.AreEqual(LoginPage.Name, "Register");

         




        }


        [TearDown]
        public void close()
        {

            Driver.driver.Close();
            Driver.driver.Quit();
            Driver.driver.Dispose();


        } 
    }
}
