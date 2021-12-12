using Model;
using Model.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Viewmodel {
    public static class MemberViewModel {
        private static MemberDAO MemberDB = new MemberDAO();

        public static bool MakeUser(string name, string username, DateTime date, string email, string password) {
            Member member = new Member(name, username, date, email);
            MemberDB.insert(member, password);
            return true;
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
