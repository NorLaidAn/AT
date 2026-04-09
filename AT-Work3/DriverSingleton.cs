using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT_Work3
{
    internal class DriverSingleton
    {
        private static IWebDriver? driver;

        public static IWebDriver Get()
        {
            if (driver == null)
                driver = new ChromeDriver();

            return driver;
        }

        public static void QuitDriver()
        {
            driver?.Quit();
            driver = null;
        }
    }
}
