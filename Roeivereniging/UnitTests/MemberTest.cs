using NUnit.Framework;
using Model.DAO;
using System;
using Model;
using Viewmodel;

namespace UnitTests
{
    internal class MemberTest
    {
        [SetUp]
        public void Setup()
        {
            Database.catalog = "Roeivereniging_Test";
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void TestMemberID(int id)
        {
            Model.Member member = new Model.Member(id, "", "", System.DateTime.Now, true, true, true,"admin@pizza.com");
            Assert.AreEqual(id, member.GetId());
        }

        [Test]
        public void Member_AddUser_MakeUser() {
            DateTime date = new DateTime(2500, 12, 31);
            MemberViewModel.MakeUser("test", "tester", date, "test@mail.com", "wachtwoord");
            Member result = MemberViewModel.GetByUsername("tester");
            Assert.AreEqual(date, result.GetBirthday());
        }

    }
}
