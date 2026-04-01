using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using Xunit;

[assembly: CollectionBehavior(MaxParallelThreads = 4)]

namespace xUnit_AT_Parallel
{
    public class TestsXUnit
    {
        public ChromeDriver Setup()
        {
            var driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://en.ehu.lt");
            return driver;
        }

        public void TearDown(ChromeDriver driver)
        {
            driver.Quit();
        }
    }
    public class A()
    {
        [Theory]
        [InlineData("https://en.ehuniversity.lt/about/")]
        public void EHUSiteWorkTest(string url)
        {
            TestsXUnit test = new TestsXUnit();
            var driver = test.Setup();
            var actions = new Actions(driver);

            IWebElement aboutTab = driver.FindElement(By.XPath("//a[text()='About']"));
            actions.MoveToElement(aboutTab).Perform();
            aboutTab.Click();

            string currentUrl = driver.Url;
            Assert.Equal(url, currentUrl);

            driver.FindElement(By.XPath("//title[contains(text(), 'About')]"));

            test.TearDown(driver);
        }
    }

    public class B()
    {
        [Theory]
        [InlineData("https://en.ehuniversity.lt/?s=study+programs")]
        public void EHUSearchTest(string url)
        {
            TestsXUnit test = new TestsXUnit();
            var driver = test.Setup();
            var actions = new Actions(driver);

            IWebElement search = driver.FindElement(By.XPath("//div[@class='header-search']"));
            actions.MoveToElement(search).Perform();

            IWebElement searchBox = driver.FindElement(By.XPath("//input[@class='form-control']"));
            searchBox.SendKeys("study programs");

            IWebElement submitButton = driver.FindElement(By.XPath("//button[@class='btn btn-info']"));
            submitButton.Click();

            string currentUrl = driver.Url;
            Assert.Equal(url, currentUrl);

            driver.FindElement(By.XPath("//div[@class='content search-results']"));

            test.TearDown(driver);
        }
    }

    public class C()
    {
        [Fact]
        public void EHULanguageTest()
        {
            TestsXUnit test = new TestsXUnit();
            var driver = test.Setup();
            var actions = new Actions(driver);

            IWebElement languageChanger = driver.FindElement(By.XPath("//li[a[text()='en']]"));
            actions.MoveToElement(languageChanger).Perform();

            IWebElement changeButton = driver.FindElement(By.XPath("//a[text()='lt']"));
            changeButton.Click();

            string currentUrl = driver.Url;
            Assert.Contains("lt", currentUrl);

            driver.FindElement(By.XPath("//a[text()='Apie mus']"));

            test.TearDown(driver);
        }
    }

    public class D()
    {
        [Fact]
        public void EHUContactFormTest()
        {
            TestsXUnit test = new TestsXUnit();
            var driver = test.Setup();
            var actions = new Actions(driver);

            IWebElement contactsHref = driver.FindElement(By.XPath("//li[contains(@class,'menu-item-17512')]//a[contains(@href,'contacts')]"));
            contactsHref.Click();

            Assert.True(driver.FindElement(By.XPath("//em[text()='consult@ehu.lt']")).Displayed);
            Assert.True(driver.FindElement(By.XPath("//td[contains(., '+370')]")).Displayed);
            Assert.True(driver.FindElement(By.XPath("//a[contains(@href, 'facebook') and contains(text(), 'University')]")).Displayed);

            test.TearDown(driver);
        }
    }    
}