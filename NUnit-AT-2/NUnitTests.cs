using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

[assembly: Parallelizable(ParallelScope.All)]

namespace NUnit_AT_2
{
    [TestFixture]
    [Category("UI testing")]
    public class NUnitTests
    {
        private ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        private ThreadLocal<Actions> actions = new ThreadLocal<Actions>();

        [SetUp]
        public void Setup()
        {
            driver.Value = new ChromeDriver();
            actions.Value = new Actions(driver.Value);
            driver.Value.Manage().Window.Maximize();
            driver.Value.Navigate().GoToUrl("https://en.ehu.lt");
        }

        [TearDown]
        public void Cleanup()
        {
            driver.Value.Quit();
        }

        [Test]
        [Category("WorkCheck")]
        [TestCase("https://en.ehuniversity.lt/about/")]
        public void EHUSiteWorkTest(string url)
        {
            IWebElement aboutTab = driver.Value.FindElement(By.XPath("//a[text()='About']"));

            actions.Value.MoveToElement(aboutTab).Perform();

            aboutTab.Click();

            string currentUrl = driver.Value.Url;
            Assert.That(currentUrl, Is.EqualTo(url));

            driver.Value.FindElement(By.XPath("//title[contains(text(), 'About')]"));
        }

        [Test]
        [TestCase("https://en.ehuniversity.lt/?s=study+programs")]
        public void EHUSearchTest(string url)
        {
            IWebElement search = driver.Value.FindElement(By.XPath("//div[@class='header-search']"));

            actions.Value.MoveToElement(search).Perform();

            IWebElement searchBox = driver.Value.FindElement(By.XPath("//input[@class='form-control']"));

            searchBox.SendKeys("study programs");

            IWebElement submitButton = driver.Value.FindElement(By.XPath("//button[@class='btn btn-info']"));

            submitButton.Click();

            string currentUrl = driver.Value.Url;
            Assert.That(currentUrl, Is.EqualTo(url));

            driver.Value.FindElement(By.XPath("//div[@class='content search-results']"));
        }

        [Test]
        [Category("WorkCheck")]
        public void EHULanguageTest()
        {
            IWebElement languageChanger = driver.Value.FindElement(By.XPath("//li[a[text()='en']]"));

            actions.Value.MoveToElement(languageChanger).Perform();

            IWebElement changeButton = driver.Value.FindElement(By.XPath("//a[text()='lt']"));
            changeButton.Click();

            string currentUrl = driver.Value.Url;
            Assert.That(currentUrl, Does.Contain("lt."));
            driver.Value.FindElement(By.XPath("//a[text()='Apie mus']"));
        }

        [Test]
        [Category("WorkCheck")]
        public void EHUContactFormTest()
        {
            IWebElement contactsHref = driver.Value.FindElement(By.XPath("//li[contains(@class,'menu-item-17512')]//a[contains(@href,'contacts')]"));
            contactsHref.Click();

            Assert.That(driver.Value.FindElement(By.XPath("//em[text()='consult@ehu.lt']")).Displayed);

            Assert.That(driver.Value.FindElement(By.XPath("//td[contains(., '+370')]")).Displayed);

            Assert.That(driver.Value.FindElement(By.XPath("//a[contains(@href, 'facebook') and contains(text(), 'University')]")).Displayed);
        }
    }
}