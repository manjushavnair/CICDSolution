using NUnit.Framework;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.PhantomJS;


 
 

namespace MVCAppTest.Controllers
{

    [TestFixture("chrome", "63", "Windows 7")]

    public class RegisterControllerTests
    {

        private IWebDriver driver;
        private string browser;
        private string version;
        private string os;

        public RegisterControllerTests()
        {

        }

        public RegisterControllerTests(string browser, string version, string os) 
        {

            this.browser = browser;
            this.version = version;
            this.os = os;

        }
     


        /**   
        <summary>RegisterControllerTests Test docuemntation</summary>   
        */
        /** 
         <param name=""></param>
         */
        [Test]
        public void UserAddTest()
        {
             NUnit.Framework.Assert.Pass();
        }
    }
}