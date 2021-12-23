using NUnit.Framework;
using Model.DAO;
using System;
using Model;
using Viewmodel;

namespace UnitTests
{
    internal class MemberTest
    {
        private MemberViewModel viewModel;
        [SetUp]
        public void Setup()
        {
            Database.catalog = "Roeivereniging_Test";
            viewModel = new MemberViewModel();
        }

        [Test]
        public void A_TestMemberList_Empty(){
            Assert.AreEqual(0, viewModel.MemberList.Count);
        }

        [Test]
        public void B_MakeUser_GiveMemberDetails_ReturnMember()
        {
            string name = "test";
            string username = "test";
            DateTime date = new DateTime(2500, 12, 31);
            string email = "test@mail.com";
            string password = "password";
            Member result = viewModel.MakeUser(name, username, date, email, password);
            Assert.AreEqual(name,result.name);
            Assert.AreEqual(username,result.username);
            Assert.AreEqual(date, result.date);
            Assert.AreEqual(email,result.email);
        }



        [Test]
        public void C_TestMemberID()
        {
            Member test = MemberViewModel.GetByUsername("test");
            Member result = viewModel.GetByID(test.id);
            Assert.AreEqual(test.id, result.id);
        }

        [Test]
        public void D_DeleteMember_ID_RemoveMember()
        {
            Member test = MemberViewModel.GetByUsername("test");
            viewModel.DeleteMember(test.id);
            Assert.AreEqual(0, viewModel.MemberList.Count);
        }


    }
}
