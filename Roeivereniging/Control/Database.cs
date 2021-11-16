using System;
using System.Data.SqlClient;

namespace Control
{
    public static class Database
    {
        public static SqlConnection connection;
        public static void Init()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost";
            builder.UserID = "sa";
            builder.Password = "#7mBzd*EN";
            builder.InitialCatalog = "Roeivereniging";
            connection = new SqlConnection(builder.ConnectionString);
        }
        public static bool Login(string username, string password)
        {
            Init();
            String sql = "SELECT PWDCOMPARE(@password, [Password]) FROM[Roeivereniging].[dbo].[User] WHERE Username = @username";

            using (SqlCommand command = new SqlCommand(sql, Database.connection))
            {
                command.Parameters.AddWithValue("username", username);
                command.Parameters.AddWithValue("password", password);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return reader.GetInt32(0) == 1;
                    }
                }
            }
            return false;
        }
    }
}
