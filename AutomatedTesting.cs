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
            Actions actions = new Actions(driver);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://en.ehu.lt");

            IWebElement aboutTab = driver.FindElement(By.XPath("//a[text()='About']"));

            
            actions.MoveToElement(aboutTab).Perform();

            aboutTab.Click();

            string currentUrl = driver.Url;
            Assert.That(currentUrl, Is.EqualTo("https://en.ehuniversity.lt/about/"));

            driver.FindElement(By.XPath("//title[contains(text(), 'About')]"));

            driver.Quit();
        }

        [Test]
        public void EHUSearchTest()
        {
            IWebDriver driver = new ChromeDriver();
            Actions actions = new Actions(driver);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://en.ehu.lt");

            IWebElement search = driver.FindElement(By.XPath("//div[@class='header-search']"));
            
            actions.MoveToElement(search).Perform();

            IWebElement searchBox = driver.FindElement(By.XPath("//input[@class='form-control']"));

            searchBox.SendKeys("study programs");

            IWebElement submitButton = driver.FindElement(By.XPath("//button[@class='btn btn-info']"));

            submitButton.Click();

            string currentUrl = driver.Url;
            Assert.That(currentUrl, Is.EqualTo("https://en.ehuniversity.lt/?s=study+programs"));

            driver.FindElement(By.XPath("//div[@class='content search-results']"));

            driver.Quit();
        }

        [Test]
        public void EHULanguageTest()
        {
            IWebDriver driver = new ChromeDriver();
            Actions actions = new Actions(driver);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://en.ehu.lt");

            IWebElement languageChanger = driver.FindElement(By.XPath("//li[a[text()='en']]"));

            actions.MoveToElement(languageChanger).Perform();

            IWebElement changeButton = driver.FindElement(By.XPath("//a[text()='lt']"));
            changeButton.Click();

            string currentUrl = driver.Url;
            Assert.That(currentUrl, Does.Contain("lt."));
            driver.FindElement(By.XPath("//a[text()='Apie mus']"));

            driver.Quit();
        }
    }
}