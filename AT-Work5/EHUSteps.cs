using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using Serilog;
using Shouldly;

namespace AT_Work5
{
    [Binding]
    public class EHUSteps
    {
        private IWebDriver driver;
        private EHUHomePage page;

        [BeforeScenario]
        public void Setup()
        {
            Log.Information("Начало сценария");
            driver = DriverSingleton.Get();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://en.ehu.lt");

            page = new EHUHomePage(driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Log.Information("Конец сценария");
        }

        [BeforeTestRun]
        public static void SetupLogging()
        {
            Logger.Configure();
        }

        [AfterTestRun]
        public static void Cleanup()
        {
            DriverSingleton.Get().Quit();
            Log.CloseAndFlush();
        }

        [Given("user is on EHU homepage")]
        public void GivenUserIsOnHomepage()
        {
            Log.Information("Главная страница открыта");
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
            page.GetCurrentUrl().ShouldContain("about");
        }

        [When("user searches for {string}")]
        public void WhenUserSearches(string query)
        {
            page.Search(query);
        }

        [Then("search results page should be opened")]
        public void ThenSearchResults()
        {
            page.GetCurrentUrl().ShouldContain("?s=");
        }

        [When("user changes language to {string}")]
        public void WhenChangeLanguage(string lang)
        {
            page.ChangeLanguage(lang);
        }

        [Then("Lithuanian version should be displayed")]
        public void ThenLanguageChanged()
        {
            page.GetCurrentUrl().ShouldContain("lt.");
        }

        [When("user opens contacts page")]
        public void WhenOpenContacts()
        {
            page.contactsHref.Click();
        }

        [Then("contact information should be visible")]
        public void ThenContactsVisible()
        {
            page.consult.Displayed.ShouldBeTrue();
        }
    }
}

