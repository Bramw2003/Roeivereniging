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
    }
}
