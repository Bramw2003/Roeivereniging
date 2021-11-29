using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Model.DAO
{
    public static class Member
    {
        public static Model.Member GetByUsername(string username)
        {
            Database.Init();
            string sql = "SELECT TOP(1000)[ID],[name],[birthday],[admin],[repair],[examinator]FROM[LID] WHERE username = @username";
            Model.Member member = null;
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("username", username);
            if (Database.OpenConnection())
            {
                var a = command.ExecuteReader(System.Data.CommandBehavior.SingleResult);
                while (a.Read())
                {
                    member = new Model.Member(a.GetInt32(0),a.GetString(1),username,a.GetDateTime(2), a.GetBoolean(3), a.GetBoolean(4), a.GetBoolean(5));
                    
                }
                command.Dispose();
                Database.connection.Close();
            }
            return member;
            
        }
        public static Model.Member GetById(int id)
        {
            Database.Init();
            string sql = "SELECT TOP(1)[ID],[name],[birthday],[admin],[repair],[examinator],[username]FROM[LID] WHERE ID = @id";
            Model.Member member = null;
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("id", id);
            if (Database.OpenConnection())
            {
                var a = command.ExecuteReader(System.Data.CommandBehavior.SingleResult);
                while (a.Read())
                {
                    member = new Model.Member(a.GetInt32(0), a.GetString(1), a.GetString(6), a.GetDateTime(2), a.GetBoolean(3), a.GetBoolean(4), a.GetBoolean(5));

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
            String sql = "INSERT INTO LID(username,password,name,birthday,admin,repair,examinator) VALUES( @username, PWDENCRYPT(@password), @name, @birthday, 0, 0, 0)";
            bool result = false;
            SqlCommand command = new SqlCommand(sql, Database.connection);
            {
                command.Parameters.AddWithValue("username", username);
                command.Parameters.AddWithValue("password", password);
                command.Parameters.AddWithValue("name", name);
                command.Parameters.AddWithValue("birthday", birthday);
                if (Database.OpenConnection()) {
                    var a = command.ExecuteNonQuery();
                    result = a == 1;
                    command.Dispose();
                    Database.connection.Close();
                }
                return result;
            }
        }
    }
}
