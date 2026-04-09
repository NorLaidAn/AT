using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT_Work3
{
    internal class EHUHomePage
    {
        private IWebDriver driver;
        private Actions actions;

        public IWebElement aboutTab => driver.FindElement(By.XPath("//a[text()='About']"));
        public IWebElement search => driver.FindElement(By.XPath("//div[@class='header-search']"));
        public IWebElement searchBox => driver.FindElement(By.XPath("//input[@class='form-control']"));
        public IWebElement submitButton => driver.FindElement(By.XPath("//button[@class='btn btn-info']"));
        public IWebElement languageChanger => driver.FindElement(By.XPath("//li[a[text()='en']]"));
        public IWebElement changeButton => driver.FindElement(By.XPath("//a[text()='lt']"));
        public IWebElement contactsHref => driver.FindElement(By.XPath("//li[contains(@class,'menu-item-17512')]//a[contains(@href,'contacts')]"));

        public EHUHomePage(IWebDriver driver)
        {
            this.driver = driver;
            this.actions = new Actions(driver);
        }
        public void HoverAndCLick(IWebElement element)
        {
            actions.MoveToElement(element).Perform();
            element.Click();
        }
        public void Search(string str)
        {
            searchBox.Clear();
            searchBox.SendKeys(str);
            submitButton.Click();
        }
        public string GetCurrentUrl() => driver.Url;
        public string GetPageTitle() => driver.Title;
    }
}
