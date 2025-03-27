using Microsoft.VisualStudio.TestTools.UnitTesting;
using testing;
using System.Linq;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestа3
    {

        [TestMethod]
        public void AuthTestSuccess()
        {
            var page = new AuthPage();
            {
                Assert.IsTrue(page.Auth("admin1", "password123"));
                Assert.IsTrue(page.Auth("admin2", "securepass"));
                Assert.IsTrue(page.Auth("visitor1", "qwerty"));
                Assert.IsTrue(page.Auth("visitor2", "welcome123"));
                Assert.IsTrue(page.Auth("visitor3", "mypassword"));
                Assert.IsTrue(page.Auth("visitor4", "letmein"));
                Assert.IsTrue(page.Auth("artist1", "artistpass"));
                Assert.IsTrue(page.Auth("artist2", "creative123"));
                Assert.IsTrue(page.Auth("artist3", "artlover"));
                Assert.IsTrue(page.Auth("organizer1", "eventpro"));
                Assert.IsTrue(page.Auth("organizer2", "planit"));
                Assert.IsTrue(page.Auth("organizer3", "manageexpo"));
                Assert.IsTrue(page.Auth("tsyguleva", "123"));
                Assert.IsTrue(page.Auth("smirnov", "65"));
                Assert.IsTrue(page.Auth("petr", "765"));
                Assert.IsTrue(page.Auth("edor", "345"));
                Assert.IsTrue(page.Auth("pav", "789"));
                Assert.IsTrue(page.Auth("esenin", "05"));
                Assert.IsTrue(page.Auth("pavka3", "78"));

            }
        }
    }
}