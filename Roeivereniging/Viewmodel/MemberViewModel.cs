using MicroMvvm;
using Model;
using Model.DAO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Viewmodel {
    public class MemberViewModel : ObservableObject {
        private static MemberDAO MemberDB = new MemberDAO();
        List<Member> _MemberList;

        public MemberViewModel() {
            _MemberList = MemberDB.GetAll();
        }

        public List<Member> MembersList {
            get { return _MemberList; }
            set { _MemberList = value; RaisePropertyChanged("Members"); }
        }

        public static void MakeUser(string name, string username, DateTime date, string email, string password) {
            Member member = new Member(name, username, date, email);
            MemberDB.insert(member, password);
        }

        public static Member GetByUsername(string name) {
           return MemberDB.GetByUsername(name);
        }

        public static Member GetByID(int ID) {
            return MemberDB.GetById(ID);
        }

/*        public void UpdateMembers() {
            MembersList = MemberDB.GetAll();
        }*/

/*        public ICommand UpdateMemberList { get { return new RelayCommand(UpdateMembers); } }*/
    }
}
