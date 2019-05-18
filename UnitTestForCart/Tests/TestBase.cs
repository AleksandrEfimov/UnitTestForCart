using NUnit.Framework;
using NUnit.Compatibility;

namespace UnitTestForCart.Tests
{
    class TestBase
    {
        public App _app;

        [SetUp]
        public void Start()
        {
            _app = new App();
        }

        [Test]
        public void GoToUrl()
        {
            _app._driver.Navigate().GoToUrl("http:ya.ru");
            _app._wait.Until(driver => _app._driver.Url.EndsWith("ya.ru/"));
            string Url = _app._driver.Url.ToString();
            Assert.AreEqual("https://ya.ru/", Url );
        }

        [TearDown]
        public void Stop()
        {
            _app.Quit();
            _app = null; 
        }
    }
}
