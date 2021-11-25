using NUnit.Framework;
using Model;
namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        [TestCase("admin","admin",true)]
        [TestCase("bestaatniet","nee",false)]
        public void Database_UserLoginTest(string username, string password, bool exists)
        {
            Assert.AreEqual(exists, Database.UserLogin(username,password));
        }

        
    }
}