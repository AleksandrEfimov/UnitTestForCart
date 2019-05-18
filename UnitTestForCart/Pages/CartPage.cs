using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.PageObjects;

namespace UnitTestForCart.Pages
{
    class CartPage
    {
        IWebDriver _driver;
        WebDriverWait _wait;
        IList<IWebElement> _itemsINTable => _driver.FindElements(By.CssSelector("table.dataTable tr td.item"));
        IList<IWebElement> _PreviewList => _driver.FindElements(By.CssSelector("li.shortcut"));
        IList<IWebElement> _RemoveBtnList => _driver.FindElements(By.Name("remove_cart_item"));


        public CartPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }


        public int GetCountItemInTable()
        {
            return _itemsINTable.Count();
        }

        void StopMovingPreview()
        {
            _PreviewList[0].Click();
        }

        public int QuantityAfterAllDeleting()
        {
            StopMovingPreview();
            do
            {
                int expectedCount = GetCountItemInTable() - 1;
                _RemoveBtnList[0].Click();
                _wait.Until(driver => GetCountItemInTable() == expectedCount);
            } while (GetCountItemInTable() != 0);
            return GetCountItemInTable();
        }

        internal static CartPage Open(IWebElement cartBtn, IWebDriver wd)
        {
            cartBtn.Click();
            return new CartPage(wd);
        }
    }
}
