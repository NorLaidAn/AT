using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace AT_Work1
{
    [TestFixture]
    public class AutomatedTesting
    {
        [Test]
        public void EHUSiteWorkTest()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("https://en.ehu.lt");
            IWebElement aboutTab = driver.FindElement(By.XPath("//a[text()='About']"));

            Actions actions = new Actions(driver);
            actions.MoveToElement(aboutTab).Perform();
            
            aboutTab.Click();

            string currentUrl = driver.Url;
            Assert.That(currentUrl, Is.EqualTo("https://en.ehuniversity.lt/about/"));

            driver.FindElement(By.XPath("//title[contains(text(), 'About')]"));
            
            driver.Quit();
        }
    }
}
