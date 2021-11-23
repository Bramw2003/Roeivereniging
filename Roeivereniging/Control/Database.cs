using System;
using System.Data.SqlClient;

namespace Control
{
    public enum BoatType
    {           // https://nl.wikipedia.org/wiki/Roeiboot#Naar_bouwtype
        A = 0,  // Gladde boten, A-boten (ongebruikelijk) of wedstrijdboten, Dit zijn de boten geschikt voor wedstrijden.
        B = 1,  // B-boten, Een vrij zeldzame tussenvorm die breder is dan een gladde boot maar smaller dan een C-boot.
        C = 2,  // C-boten, Een veelvoorkomend type geschikt voor recreatief roeien, training en de langere wedstrijden.
        W = 3,  // wherry, Dit zijn de breedste en zwaarste boten. Ze worden voornamelijk gebruikt als recreatiemateriaal.
    }
    public static class Database
    {
        public static SqlConnection connection;
        public static void Init()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "127.0.0.1";
            builder.UserID = "sa";
            builder.Password = "#7mBzd*EN";
            builder.InitialCatalog = "Roeivereniging";
            connection = new SqlConnection(builder.ConnectionString);
        }
        public static bool OpenConnection()
        {
            try
            {
                connection.Open();
            }
            catch (Exception)
            {
                Console.WriteLine("DB unavailable");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if the username and password match in the database
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>True if a match was found</returns>
        public static bool UserLogin(string username, string password)
        {
            Init();
            String sql = "SELECT TOP(1) PWDCOMPARE(@password, [password]) FROM [LID] WHERE [username] = @username";
            bool result = false;
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("username", username);
            command.Parameters.AddWithValue("password", password);
            if (OpenConnection())
            {
                var a = command.ExecuteScalar();
                if (a != null)
                {
                    result = (int)a == 1;
                }
                command.Dispose();
                connection.Close();
            }
            return result;

        }

        /// <summary>
        /// Checks if a user exists in the database
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool UserExist(string username)
        {
            Init();
            String sql = "SELECT TOP(1) * FROM [LID] WHERE [username] = @username";
            bool result = false;
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("username", username);
            if (OpenConnection())
            {
                var a = command.ExecuteNonQuery();
                if (a != null)
                {
                    result = (int)a == 1;
                }
                command.Dispose();
                connection.Close();
            }
            return result;
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
        public static bool AddUser(string username, string password, string email, string name, DateTime birthday)
        {
            if (username == null || username == "") return false;
            if (password == null || password == "") return false;
            if (email == null || email == "") return false;
            if (name == null || name == "") return false;
            if (birthday == null) return false;

            String sql = "SELECT PWDCOMPARE(@password, [password]) FROM [Roeivereniging].[dbo].[LID] WHERE [username] = @username";
            using (SqlCommand command = new SqlCommand(sql, Database.connection))
            {
                //command.Parameters.AddWithValue("username", username);
                //command.Parameters.AddWithValue("password", password);

                bool result = (int)command.ExecuteScalar() == 1;
                connection.Close();
                command.Dispose();
                return result;
            }
        }

        /// <summary>
        /// Tries to reserve a boat
        /// </summary>
        /// <returns></returns>
        public static bool ReserveBoat()
        {
            Init();
            String sql = "SELECT PWDCOMPARE(@password, [password]) FROM[Roeivereniging].[dbo].[LID] WHERE [username] = @username";

            using (SqlCommand command = new SqlCommand(sql, Database.connection))
            {
                //command.Parameters.AddWithValue("username", username);
                //command.Parameters.AddWithValue("password", password);

                bool result = (int)command.ExecuteScalar() == 1;
                connection.Close();
                command.Dispose();
                return result;
            }
        }
        /// <summary>
        /// Tries to create a new boat in the database
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool AddBoat(string name, BoatType type)
        {
            Init();
            string sql = "INSERT INTO boat(name, typesID) VALUES (@name, @type)";
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("name", name);
            command.Parameters.AddWithValue("type", type);
            if (OpenConnection())
            {
                return command.ExecuteNonQuery() == 1;
            }
            return false;
        }

        /// <summary>
        /// Adds a type of boat to the database
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="steer"></param>
        /// <returns></returns>
        public static bool AddType(int capacity, bool steer)
        {
            Init();
            string sql = "INSERT INTO types(capacity, steer) VALUES ( @capacity, @steer);";
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("capacity", capacity);
            command.Parameters.AddWithValue("steer", steer);
            if (OpenConnection())
            {
                return command.ExecuteNonQuery() == 1;
            }
            return false;
        }
        public static bool AddReservation()
        {
            Init();
            string sql = "INSERT INTO types(capacity, steer) VALUES ( @capacity, @steer);";
            SqlCommand command = new SqlCommand(sql, Database.connection);
            //command.Parameters.AddWithValue("capacity", capacity);
            //command.Parameters.AddWithValue("steer", steer);
            return command.ExecuteNonQuery() == 1;
        }
    }
}
