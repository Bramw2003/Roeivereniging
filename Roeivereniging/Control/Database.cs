using System;
using System.Data.SqlClient;


namespace Control
{
    public enum BoatType
    {
        Boot1,
        Boot2,
        Boot3,
        Boot4,
        Boot5,
    }
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
        public static bool UserLogin(string username, string password)
        {
            Init();
            String sql = "SELECT PWDCOMPARE(@password, [password]) FROM[Roeivereniging].[dbo].[LID] WHERE [username] = @username";

            using (SqlCommand command = new SqlCommand(sql, Database.connection))
            {
                command.Parameters.AddWithValue("username", username);
                command.Parameters.AddWithValue("password", password);
                connection.Open();
                return (int)command.ExecuteScalar() == 1;
            }
        }

        public static bool ReserveBoat()
        {
            Init();
            String sql = "SELECT PWDCOMPARE(@password, [password]) FROM[Roeivereniging].[dbo].[LID] WHERE [username] = @username";

            using (SqlCommand command = new SqlCommand(sql, Database.connection))
            {
                //command.Parameters.AddWithValue("username", username);
                //command.Parameters.AddWithValue("password", password);
                connection.Open();
                return (int)command.ExecuteScalar() == 1;
            }
            return false;
        }

        public static bool AddBoat(string name, BoatType type)
        {
            Init();
            string sql = "INSERT INTO boat(name, typesID) VALUES (@name, @type)";
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("name", name);
            command.Parameters.AddWithValue("type", type);
            return command.ExecuteNonQuery() == 1;
        }
        public static bool AddType(int capacity, bool steer)
        {
            Init();
            string sql = "INSERT INTO types(capacity, steer) VALUES ( @capacity, @steer);";
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("capacity", capacity);
            command.Parameters.AddWithValue("steer", steer);
            return command.ExecuteNonQuery() == 1;
        }
    }
}
