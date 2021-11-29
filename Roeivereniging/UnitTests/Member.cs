using NUnit.Framework;
using Model;
namespace UnitTests
{
    internal class Member
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void TestMemberID(int id)
        {
            Model.Member member = new Model.Member(id, "", "", System.DateTime.Now, true, true, true);
            Assert.AreEqual(id, member.GetId());
        }

    }
}
