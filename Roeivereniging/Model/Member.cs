using System;
using System.Data.SqlClient;
using Model;
namespace Model
{
    public class Member
    {
        public int id { get; set; }
        public DateTime date { get; }
        public string name { get; }
        public bool admin { get; }
        public bool repair { get; }
        public bool examinator { get; }
        public string username { get; }
        public string email { get; }

        public Member(int id, string name, string username, DateTime date, bool admin, bool repair, bool examinator, string email)
        {
            this.id = id;
            this.name = name;
            this.username = username;
            this.date = date.Date;
            this.admin = admin;
            this.repair = repair;
            this.examinator = examinator;
            this.email = email;
        }

        public Member(string name, string username, DateTime date, string email)
        {
            this.name = name;
            this.username = username;
            this.date = date;
            this.email = email;
        }

        #region Getters
        public DateTime GetBirthday() { return date; }
        public string GetName() { return name; }
        public bool IsAdmin() { return admin; }
        public bool IsRepair() { return repair; }
        public bool IsExaminator() { return examinator; }
        public string GetUsername() { return username; }
        public int GetId() { return id; }
        public string GetEmail() { return email; }
        #endregion



    }
}
