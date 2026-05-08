using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;

namespace AT_Work4
{
    [Binding]
    public class EHUSteps
    {
        private IWebDriver driver;
        private EHUHomePage page;

        [BeforeScenario]
        public void Setup()
        {
            driver = DriverSingleton.Get();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://en.ehu.lt");

            page = new EHUHomePage(driver);
        }

        [AfterScenario]
        public void Cleanup()
        {
            driver.Quit();
        }

        [Given("user is on EHU homepage")]
        public void GivenUserIsOnHomepage()
        {
            // alredy opend
        }

        [When("user clicks About tab")]
        public void WhenUserClicksAbout()
        {
            page.HoverAndCLick(page.aboutTab);
        }

        [Then("About page should be opened")]
        public void ThenAboutPageOpened()
        {
            Assert.That(page.GetCurrentUrl(), Does.Contain("about"));
        }

        [When("user searches for {string}")]
        public void WhenUserSearches(string query)
        {
            page.Search(query);
        }

        [Then("search results page should be opened")]
        public void ThenSearchResults()
        {
            Assert.That(page.GetCurrentUrl(), Does.Contain("?s="));
        }

        [When("user changes language to {string}")]
        public void WhenChangeLanguage(string lang)
        {
            page.ChangeLanguage(lang);
        }

        [Then("Lithuanian version should be displayed")]
        public void ThenLanguageChanged()
        {
            Assert.That(page.GetCurrentUrl(), Does.Contain("lt."));
        }

        [When("user opens contacts page")]
        public void WhenOpenContacts()
        {
            page.contactsHref.Click();
        }

        [Then("contact information should be visible")]
        public void ThenContactsVisible()
        {
            Assert.That(driver.FindElement(By.XPath("//em[text()='consult@ehu.lt']")).Displayed);
        }
    }
}
