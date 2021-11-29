﻿using NUnit.Framework;
using Model.DAO;
using System;

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

        [Test]
        public void Member_AddUser_MakeUser() {
            Model.DAO.Member.AddUser("username", "password", "email@mail.nl", "name", DateTime.Now);
            Assert.AreEqual("name", Model.DAO.Member.GetByUsername("username").GetName());
        }

    }
}