using Model;
using Model.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Viewmodel {
    public static class MemberViewModel {
        private static MemberDAO MemberDB = new MemberDAO();

        public static void MakeUser(string name, string username, DateTime date, string email, string password) {
            Member member = new Member(name, username, date, email, password);
            MemberDB.insert(member);
        }

        public static Member GetByUsername(string name) {
           return MemberDB.GetByUsername(name);
        }

        public static Member GetByID(int ID) {
            return MemberDB.GetById(ID);
        }
    }
}
