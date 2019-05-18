using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Compatibility;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using UnitTestForCart.Pages;


namespace UnitTestForCart.Tests
{
    [TestFixture(Description = "Тесты на добавление товаров в корзину и удаление")]
    class AddDeleteProductInCartTests
    {
        private App _app;
        private IWebDriver _driver;
        private WebDriverWait _wait;
        private MainPage _mainPage;
        private ProductPage _prodPage;
        private CartPage _cartPage;
        private int _quantityInCartTable;

        [SetUp]
        public void Start()
        {
            _app = new App();
            _driver = _app._driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            _mainPage = new MainPage(_driver);
            // _prodPage = new ProductPage(_driver);
            _cartPage = new CartPage(_driver);
        }

        [Test(Description = "В данной части сначала последовательно добавляются 3 случайных товара с главной страницы," +
                            "а после перехода в корзину, последовательно удаляются." +
                            "Контролируется работа счётчика корзины и таблицы товаров в корзине.")]
        public void TestAddDelete()
        {
            _mainPage.Open();
            Assert.AreEqual(0, _mainPage.GetActualQuantity, "Текущее количество товара в корзине не равно ожидаемому");
            _prodPage = _mainPage.ClickOnProd();
            Assert.AreEqual(1, _prodPage.AddToCartWithWaitCounterRenew(), "Текущее количество товара в корзине не равно ожидаемому");

            _mainPage.Open();
            Assert.AreEqual(1, _mainPage.GetActualQuantity, "Текущее количество товара в корзине не равно ожидаемому");

            _prodPage = _mainPage.ClickOnProd();
            Assert.AreEqual(2, _prodPage.AddToCartWithWaitCounterRenew());

            _mainPage.Open();
            Assert.AreEqual(2, _mainPage.GetActualQuantity, "Текущее количество товара в корзине не равно ожидаемому");

            _prodPage = _mainPage.ClickOnProd();
            Assert.AreEqual(3, _prodPage.AddToCartWithWaitCounterRenew());

            _mainPage.Open();
            Assert.AreEqual(3, _mainPage.GetActualQuantity, "Текущее количество товара в корзине не равно ожидаемому");

            _cartPage =  _mainPage.GoToCart();

            Assert.AreEqual(0, _cartPage.QuantityAfterAllDeleting(),"Удаление товара из корзины окончилось неудачно.");
        }
        [TearDown]
        public void Stop()
        {
            _driver?.Quit();
            _driver = null;
        }
    }
}
