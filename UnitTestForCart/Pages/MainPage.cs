using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Chrome;


namespace UnitTestForCart.Pages
{
    class MainPage
    {
        protected IWebDriver _driver;
        protected WebDriverWait _wait;
        public int GetActualQuantity => int.Parse(_driver.FindElement(By.CssSelector("#cart .quantity")).Text);
        IList<IWebElement> ProdList => _driver.FindElements(By.CssSelector("#box-most-popular li"));
        IWebElement CartBtn => _driver.FindElement(By.Id("cart"));

        public MainPage(IWebDriver driver)
        {
            this._driver = driver;
            this._wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        /// <summary>
        /// Открывает главную страницу сайта.
        /// </summary>
        internal void Open()
        {
            _driver.Url = "http://localhost:8080/litecart/en/";
        }

       /// <summary>
       /// Кликает по первому в блоке товару.
       /// </summary>
       /// <returns>Объект страницы товара</returns>       
        internal ProductPage ClickOnProd()
        {
           return ProductPage.Open(ProdList[0], _driver);
        }

        /// <summary>
        /// Клик по ссылке на Корзину.
        /// </summary>
        internal CartPage GoToCart()
        {
            return CartPage.Open(CartBtn, _driver);
        }

    }
}
