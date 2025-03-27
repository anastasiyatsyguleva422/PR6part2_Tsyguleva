using Microsoft.VisualStudio.TestTools.UnitTesting;
using testing;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest4
    {
        [TestMethod]
        public void AuthTestTrue()
        {
            var page = new AuthPage();
            Assert.IsTrue(page.Auth("tsyguleva", "123"));//вход админ
            Assert.IsFalse(page.Auth("6789", "3330"));//неверный пароль
            Assert.IsFalse(page.Auth("smit", "qwerty"));//неверный логин
            Assert.IsFalse(page.Auth("org_art@mail.com", "OrgPass123"));//проверка доступа организатора
            Assert.IsFalse(page.Auth("", ""));//незаполненный логин и пароль

        }
    }
}
