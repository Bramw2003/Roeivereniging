using System;
using System.Data.SqlClient;
using Model;
namespace Model
{
    public class Member
    {
        private int _id { get; }
        private DateTime _date { get; set; }
        private string _name { get; set; }
        private bool _admin { get; set; }
        private bool _repair { get; set; }
        private bool _examinator { get; set; }
        private string _username { get; set; }
        private string _email { get; set; }

        public Member(int id, string name, string username, DateTime date, bool admin, bool repair, bool examinator, string email)
        {
            _id = id;
            _name = name;
            _username = username;
            _date = date.Date;
            _admin = admin;
            _repair = repair;
            _examinator = examinator;
            _email = email;
        }

        public Member(string name, string username, DateTime date, string email) {
            _name = name;
            _username = username;
            _date = date;
            _email = email;
        }

        #region Getters
        public DateTime GetBirthday() { return _date; }
        public string GetName() { return _name; }
        public bool IsAdmin() { return _admin; }
        public bool IsRepair() { return _repair; }
        public bool IsExaminator() { return _examinator; }
        public string GetUsername() { return _username; }
        public int GetId() { return _id; }
        public string GetEmail() { return _email; }
        #endregion

        #region Setters
        public bool SetBirthday(DateTime date) { return Alter(date: date); }
        public bool SetName(string name) { return Alter(name: name); }
        public bool SetUsername(string username) { return Alter(username: username); }
        public bool SetAdmin(bool admin) { return Alter(admin: admin); }
        public bool SetRepair(bool repair) { return Alter(repair: repair); }
        public bool SetExaminator (bool examinator) { return Alter(examinator: examinator); }
        #endregion

        private bool Alter(DateTime? date = null, bool? admin = null, bool? repair = null, bool? examinator = null, string username = null, string name = null)
        {

            date = date ?? _date;
            admin = admin ?? _admin;
            repair = repair ?? _repair;
            examinator = examinator ?? _examinator;
            username = username ?? _username;
            name = name ?? _name;

            Database.Init();
            string sql = "UPDATE [member] SET [name] = @name, [birthday] = @bday, [admin] = @admin, [repair] = @repair, [examinator] = @examinator, [username] = @username WHERE [ID] = @id";
            bool result = false;
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("username", username);
            command.Parameters.AddWithValue("name", name);
            command.Parameters.AddWithValue("bday", date.Value.Date);
            command.Parameters.AddWithValue("admin", admin.Value);
            command.Parameters.AddWithValue("repair", repair.Value);
            command.Parameters.AddWithValue("examinator", examinator.Value);
            command.Parameters.AddWithValue("id", _id);
            if (Database.OpenConnection())
            {
                var a = command.ExecuteScalar();
                if (a != null)
                {
                    result = (int)a == 1;
                }
                command.Dispose();
                Database.connection.Close();
            }
            if (result)
            {
                _date = date.Value;
                _admin = admin.Value;
                _repair = repair.Value;
                _examinator = examinator.Value;
                _username = username;
                _name = name;
            }
            return result;
        }
    }
}
