using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace AT_Work5.Tests
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

        public IWebElement contactsHref => driver.FindElement(By.XPath("//li[contains(@class,'menu-item-17512')]//a[contains(@href,'contacts')]"));

        public IWebElement consult => driver.FindElement(By.XPath("//em[text()='consult@ehu.lt']"));

        
        public EHUHomePage(IWebDriver driver)
        {
            this.driver = driver;
            actions = new Actions(driver);
        }

        public void HoverAndCLick(IWebElement element)
        {
            Log.Information("Передвигаюсь к элементу");
            actions.MoveToElement(element).Perform();

            Log.Information("Нажимаю на элемент");
            element.Click();

            Log.Information("Клик выполнен");
        }

        public void Search(string str)
        {
            Log.Information("Переход к поиску");
            actions.MoveToElement(search).Perform();

            Log.Information("Очистка поля поиска");
            searchBox.Clear();

            Log.Information("Ввод текста поиска: {Query}", str);
            searchBox.SendKeys(str);

            Log.Information("Нажатие кнопки поиска");
            submitButton.Click();

            Log.Information("Поиск выполнен");
        }

        public void ChangeLanguage(string str)
        {
            Log.Information("Открываю меню смены языка");
            actions.MoveToElement(languageChanger).Perform();

            Log.Information("Выбираю язык: {Language}", str);
            IWebElement changeButton = driver.FindElement(By.XPath($"//a[text()='{str}']"));
            changeButton.Click();

            Log.Information("Язык изменён на: {Language}", str);
        }

        public string GetCurrentUrl()
        {
            var url = driver.Url;

            Log.Information("Текущий URL: {Url}", url);

            return url;
        }

        public string GetPageTitle()
        {
            var title = driver.Title;

            Log.Information("Заголовок страницы: {Title}", title);

            return title;
        }
    }
}
