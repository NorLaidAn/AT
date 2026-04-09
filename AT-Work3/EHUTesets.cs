using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace AT_Work3
{
    [TestFixture]
    [Category("UI testing")]
    public class EHUTests
    {
        private IWebDriver driver;
        private Actions actions;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            actions = new Actions(driver);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://en.ehu.lt");
        }

        [TearDown]
        public void Cleanup()
        {
            driver.Quit();
        }

        [Test]
        [Category("WorkCheck")]
        [TestCase("https://en.ehuniversity.lt/about/")]
        public void EHUSiteWorkTest(string url)
        {
            IWebElement aboutTab = driver.FindElement(By.XPath("//a[text()='About']"));

            actions.MoveToElement(aboutTab).Perform();

            aboutTab.Click();

            string currentUrl = driver.Url;
            Assert.That(currentUrl, Is.EqualTo(url));

            driver.FindElement(By.XPath("//title[contains(text(), 'About')]"));
        }

        [Test]
        [TestCase("https://en.ehuniversity.lt/?s=study+programs")]
        public void EHUSearchTest(string url)
        {
            IWebElement search = driver.FindElement(By.XPath("//div[@class='header-search']"));

            actions.MoveToElement(search).Perform();

            IWebElement searchBox = driver.FindElement(By.XPath("//input[@class='form-control']"));

            searchBox.SendKeys("study programs");

            IWebElement submitButton = driver.FindElement(By.XPath("//button[@class='btn btn-info']"));

            submitButton.Click();

            string currentUrl = driver.Url;
            Assert.That(currentUrl, Is.EqualTo(url));

            driver.FindElement(By.XPath("//div[@class='content search-results']"));
        }

        [Test]
        [Category("WorkCheck")]
        public void EHULanguageTest()
        {
            IWebElement languageChanger = driver.FindElement(By.XPath("//li[a[text()='en']]"));

            actions.MoveToElement(languageChanger).Perform();

            IWebElement changeButton = driver.FindElement(By.XPath("//a[text()='lt']"));
            changeButton.Click();

            string currentUrl = driver.Url;
            Assert.That(currentUrl, Does.Contain("lt."));
            driver.FindElement(By.XPath("//a[text()='Apie mus']"));
        }

        [Test]
        [Category("WorkCheck")]
        public void EHUContactFormTest()
        {
            IWebElement contactsHref = driver.FindElement(By.XPath("//li[contains(@class,'menu-item-17512')]//a[contains(@href,'contacts')]"));
            contactsHref.Click();

            Assert.That(driver.FindElement(By.XPath("//em[text()='consult@ehu.lt']")).Displayed);

            Assert.That(driver.FindElement(By.XPath("//td[contains(., '+370')]")).Displayed);

            Assert.That(driver.FindElement(By.XPath("//a[contains(@href, 'facebook') and contains(text(), 'University')]")).Displayed);
        }
    }
}
