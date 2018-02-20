using MVCAppTest.Controllers.UITest.Common;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace MVCAppTest.Controllers.UITest.PageObjects
{

    public class LoginPage
    { 

        public static string Name
        {
            get
            {
                var title = Driver.driver.FindElement(By.Id("registerLink"));
                Console.WriteLine("title" + title);
     
                if (title != null)
                    return title.Text;
                return String.Empty;
            }
        }

        public static bool register(string name)
        {
            

            var bodyTag = Driver.driver.FindElement(By.Id("registerLink"));


            Driver.driver.FindElement(By.Id("FirstName")).SendKeys("HoneyWell");
            Thread.Sleep(2000);
            Driver.driver.FindElement(By.Id("LastName")).SendKeys("Chennai");
            Thread.Sleep(2000);
            Driver.driver.FindElement(By.Id("LastName")).Clear();
            Thread.Sleep(3000);
            Driver.driver.FindElement(By.Id("LastName")).SendKeys("Bangalore");

            Thread.Sleep(3000);

            Driver.driver.FindElement(By.Id("FirstName")).SendKeys(Keys.Enter); 
            //Driver.driver.FindElement(By.XPath("//input[@value='Create']")).Click();

            return bodyTag.Text.Contains(name);
        }
 
    }
}
