using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;

namespace AT_Work3
{
    [TestFixture]
    [Category("UI testing")]
    public class EHUTests
    {
        private IWebDriver driver;
        private EHUHomePage page;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            page = new EHUHomePage(driver);
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
            page.HoverAndCLick(page.aboutTab);
            Assert.That(page.GetCurrentUrl(), Is.EqualTo(url));
            driver.FindElement(By.XPath("//title[contains(text(), 'About')]"));
        }

        [Test]
        [TestCase("https://en.ehuniversity.lt/?s=study+programs")]
        public void EHUSearchTest(string url)
        {
            page.Search("study programs");
            Assert.That(page.GetCurrentUrl(), Is.EqualTo(url));
            driver.FindElement(By.XPath("//div[@class='content search-results']"));
        }

        [Test]
        [Category("WorkCheck")]
        [TestCase("lt")]
        public void EHULanguageTest(string str)
        {
            page.ChangeLanguage(str);
            Assert.That(page.GetCurrentUrl(), Does.Contain($"{str}."));
            driver.FindElement(By.XPath("//a[text()='Apie mus']"));
        }

        [Test]
        [Category("WorkCheck")]
        public void EHUContactFormTest()
        {
            page.contactsHref.Click();

            Assert.That(driver.FindElement(By.XPath("//em[text()='consult@ehu.lt']")).Displayed);
            Assert.That(driver.FindElement(By.XPath("//td[contains(., '+370')]")).Displayed);
            Assert.That(driver.FindElement(By.XPath("//a[contains(@href, 'facebook') and contains(text(), 'University')]")).Displayed);
        }
    }
}
