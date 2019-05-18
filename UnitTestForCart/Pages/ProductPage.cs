using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

namespace UnitTestForCart.Pages
{
    class ProductPage : MainPage
    {
        IList<IWebElement> IsSizeList => _driver.FindElements(By.Name("options[Size]"));
        IWebElement AddToCartBtn => _driver.FindElement(By.Name("add_cart_product"));
        WebDriverWait _wait;
        IJavaScriptExecutor js;
        static int _expectedQantity = 0;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="wd"></param>
        public ProductPage(IWebDriver wd) : base(wd)
        {
            PageFactory.InitElements(wd, this);
            _wait = new WebDriverWait(wd, TimeSpan.FromSeconds(30));
            js = _driver as IJavaScriptExecutor;
        }

        /// <summary>
        /// Конструктор вызываемый в процессе перехода с главной страницы на страницу товара.
        /// </summary>
        /// <param name="el">IWebElement el</param>
        /// <param name="wd">IWebDriver wd</param>
        /// <returns>Страница с товаром по которому кликнули на главной странице.</returns>
        public static ProductPage Open(IWebElement el, IWebDriver wd)
        {
            el.Click();
            return new ProductPage(wd);
        }

        /// <summary>
        /// Клик по кнопке добавления товара в корзину.
        /// Ожидание изменения счётчика товарров в корзине.
        /// </summary>
        /// <returns>Количество товаров в корзине.</returns>
        internal int AddToCartWithWaitCounterRenew()
        {
            _expectedQantity++;
            if (IsSizeList.Count > 0)
            {
                SelectElement select = new SelectElement(IsSizeList[0]);
                select.SelectByText("Small");
            }
            AddToCartBtn.Submit();

            _wait.Until(wd => js.ExecuteScript(
                        "return document.querySelector('span.quantity').innerText").ToString()
                        .Equals(_expectedQantity.ToString()));
            return GetActualQuantity;
        }
    }
}
