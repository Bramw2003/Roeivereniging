using MicroMvvm;
using Model;
using Model.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Viewmodel {
    public class MemberViewModel : ObservableObject{
        private static MemberDAO MemberDB = new MemberDAO();
        public List<Member> _MemberList = GetAllMembers();

        public List<Member> MemberList{
            get { return _MemberList; }
            set { _MemberList = value; RaisePropertyChanged("MemberList"); }
        }

        private static List<Member> GetAllMembers()
        {
            return MemberDB.GetAll();
        }

        public void MakeUser(string name, string username, DateTime date, string email, string password) {
            Member member = new Member(name, username, date, email);
            MemberDB.insert(member, password);
            MemberList = GetAllMembers();
        }

        public static bool EditUserRoles(int ID, bool admin, bool examinator, bool canRepair)
        {
            return MemberDB.Alter(MemberDB.GetById(ID), null, admin, canRepair, examinator);
        }

        public static Member GetByUsername(string name) {
           return MemberDB.GetByUsername(name);
        }

        public static Member GetByID(int ID) {
            return MemberDB.GetById(ID);
        }
    }
}
