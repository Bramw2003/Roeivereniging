using MicroMvvm;
using Model;
using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Member MakeUser(string name, string username, DateTime date, string email, string password) {
            Member member = new Member(name, username, date, email);
            MemberDB.insert(member, password);
            MemberList = GetAllMembers();
            return member;
        }

        public static bool EditUserRoles(int ID, bool admin, bool examinator, bool canRepair)
        {
            return MemberDB.Alter(MemberDB.GetById(ID), null, admin, canRepair, examinator);
        }

        public static Member GetByUsername(string name) {
           return MemberDB.GetByUsername(name);
        }

        public Member GetByID(int ID) {
            return MemberList.Where(x => x.id == ID).First(); 
        }

        public void DeleteMember(int ID)
        {
            MemberDB.Delete(ID);
            MemberList.Remove(MemberList.Where(x => x.id == ID).First());
        }

        public void EditMember(Member member, string name, string username, string email, DateTime date, bool admin,bool examinator, bool repair)
        {
            MemberDB.Alter(member, date, admin, repair, examinator, username, name, email);
            MemberList = GetAllMembers();
        }
    }
}
