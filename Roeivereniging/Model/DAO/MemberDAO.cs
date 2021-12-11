using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Model.DAO {
    public class MemberDAO : IMember {

        public List<Member> GetAll()
        {
            Database.Init();
            string sql = "SELECT [ID],[name],[birthday],[admin],[repair],[examinator],[email],[username]FROM[member]";
            List<Member> list = new List<Member>();
            SqlCommand command = new SqlCommand(sql, Database.connection);
            if (Database.OpenConnection())
            {
                var a = command.ExecuteReader();
                while (a.Read())
                {
                    Member member = new Member(a.GetInt32(0), a.GetString(1), a.GetString(7), a.GetDateTime(2), a.GetBoolean(3), a.GetBoolean(4), a.GetBoolean(5), a.GetString(6));
                    list.Add(member);
                }
                command.Dispose();
                Database.connection.Close();
            }
            return list;
        }

        /// <summary>
        /// Inserts a new member into the database
        /// </summary>
        public bool insert(Member member, string password) {
            Database.Init();
            String sql = "INSERT INTO member(username,password,name,birthday,admin,repair,examinator,email) VALUES( @username, PWDENCRYPT(@password), @name, @birthday, 0, 0, 0,@email)";
            bool result = false;
            SqlCommand command = new SqlCommand(sql, Database.connection);
            {
                command.Parameters.AddWithValue("username", member.GetUsername());
                command.Parameters.AddWithValue("password", password);
                command.Parameters.AddWithValue("name", member.GetName());
                command.Parameters.AddWithValue("birthday", member.GetBirthday());
                command.Parameters.AddWithValue("email", member.GetEmail());

                if (Database.OpenConnection()) {
                    var a = command.ExecuteNonQuery();
                    result = a == 1;
                    command.Dispose();
                    Database.connection.Close();
                }
                return result;
            }
        }

        public bool Delete(int memberID) {
            throw new NotImplementedException();
        }

        public Member GetByUsername(string username) {
            Database.Init();
            string sql = "SELECT [ID],[name],[birthday],[admin],[repair],[examinator],[email]FROM[member] WHERE username = @username";
            Member member = null;
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("username", username);
            if (Database.OpenConnection()) {
                var a = command.ExecuteReader(System.Data.CommandBehavior.SingleResult);
                while (a.Read()) {
                    member = new Member(a.GetInt32(0), a.GetString(1), username, a.GetDateTime(2), a.GetBoolean(3), a.GetBoolean(4), a.GetBoolean(5), a.GetString(6));

                }
                command.Dispose();
                Database.connection.Close();
            }
            return member;

        }

        public Member GetById(int id) {
            Database.Init();
            string sql = "SELECT TOP(1)[ID],[name],[birthday],[admin],[repair],[examinator],[username],[email]FROM[member] WHERE ID = @id";
            Member member = null;
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("id", id);
            if (Database.OpenConnection()) {
                var a = command.ExecuteReader(System.Data.CommandBehavior.SingleResult);
                while (a.Read()) {
                    member = new Member(a.GetInt32(0), a.GetString(1), a.GetString(6), a.GetDateTime(2), a.GetBoolean(3), a.GetBoolean(4), a.GetBoolean(5), a.GetString(7));

                }
                command.Dispose();
                Database.connection.Close();
            }
            return member;

        }

        /// <summary>
        /// Tries to create a user in the database
        /// </summary>
        /// <param name="username">Name used for login</param>
        /// <param name="password">Password used for login</param>
        /// <param name="email">Email for confirmation</param>
        /// <param name="name">Full name</param>
        /// <param name="birthday">Birth date</param>
        /// <returns>False on failure</returns>
        public static bool AddUser(string username, string password, string email, string name, DateTime birthday) {
            if (username == null || username == "") return false;
            if (password == null || password == "") return false;
            if (email == null || email == "") return false;
            if (name == null || name == "") return false;
            if (birthday == null) return false;

            Database.Init();
            String sql = "INSERT INTO member(username,password,name,birthday,admin,repair,examinator,email) VALUES( @username, PWDENCRYPT(@password), @name, @birthday, 0, 0, 0,@email)";
            bool result = false;
            SqlCommand command = new SqlCommand(sql, Database.connection);
            {
                command.Parameters.AddWithValue("username", username);
                command.Parameters.AddWithValue("password", password);
                command.Parameters.AddWithValue("name", name);
                command.Parameters.AddWithValue("birthday", birthday);
                command.Parameters.AddWithValue("email", email);

                if (Database.OpenConnection()) {
                    var a = command.ExecuteNonQuery();
                    result = a == 1;
                    command.Dispose();
                    Database.connection.Close();
                }
                return result;
            }
        }
        /// <summary>
        /// !!!Should redefine member after altering!!!
        /// </summary>
        /// <param name="member"></param>
        /// <param name="date"></param>
        /// <param name="admin"></param>
        /// <param name="repair"></param>
        /// <param name="examinator"></param>
        /// <param name="username"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool Alter(Member member, DateTime? date = null, bool? admin = null, bool? repair = null, bool? examinator = null, string username = null, string name = null)
        {

            date = date ?? member.date;
            admin = admin ?? member.admin;
            repair = repair ?? member.repair;
            examinator = examinator ?? member.examinator;
            username = username ?? member.username;
            name = name ?? member.name;

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
            command.Parameters.AddWithValue("id", member.GetId());
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
            return result;
        }

        public bool UpdatePassword(Member member, string password)
        {
            string sql = "UPDATE [member] SET [password]=PWDENCRYPT(@pass) WHERE [username] = @username and [email] = @email";

            bool result = false;
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("username", member.username);
            command.Parameters.AddWithValue("email", member.email);
            command.Parameters.AddWithValue("pass", password);
            if (Database.OpenConnection())
            {
                var a = command.ExecuteNonQuery();
                if (a != null)
                {
                    result = (int)a == 1;
                }
                command.Dispose();
                Database.connection.Close();
            }
            return result;
        }
    }
}
