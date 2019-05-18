using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;

namespace UnitTestForCart
{
    public class App
    {
        public IWebDriver _driver;
        public WebDriverWait _wait;

        public App()
        {
            _driver = new ChromeDriver();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void Quit()
        {
            _driver?.Quit();
        }



    }
}
