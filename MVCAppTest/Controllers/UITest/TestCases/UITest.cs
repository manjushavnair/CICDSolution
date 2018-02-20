
using MVCAppTest.Controllers.UITest.Common;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace MVCAppTest.Controllers.UITest.TestCases

{

 
   [TestFixture("chrome", "63", "Windows 10")]
    public class UITest
    {
      private IWebDriver driver;
        private string browser;
        private string version;
        private string os;


           public UITest(string browser, string version, string os)
           {
               this.browser = browser;
               this.version = version;
               this.os = os;


           }

           public UITest()
           { 


           } 


        [SetUp]
        public void Init()
        {
            
            Driver.Initialize();
        }

        [TearDown]

        
        public void Cleanup()
        {
            Driver.driver.Close();
            Driver.driver.Quit();
            Driver.driver.Dispose();

        }

    }
}
