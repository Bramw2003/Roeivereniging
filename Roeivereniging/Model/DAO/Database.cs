﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Model
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
                result = a == 1;
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
        public static bool ReserveBoat(int id, DateTime date, DateTime endTime)
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
        
        public static List<Boat> GetAvailableBoats(DateTime date, DateTime start, DateTime end)
        {
            List<Boat> boats = new List<Boat>();
            string sql = "SELECT DISTINCT [boat].[ID], [boat].[name], [types].[capacity],[types].[category],[types].[steer],[types].[sculling] FROM [boat] LEFT JOIN reservations AS r ON r.boatID = boat.ID JOIN types ON types.ID = boat.typesID WHERE(r.[date] != @date OR r.[date] IS NULL) OR(r.starttime NOT BETWEEN @starttime and @endtime) AND(r.endtime NOT BETWEEN @starttime and @endtime) OR(starttime is NULL AND endtime is null)";
            List<Boat> list = new List<Boat>();
            SqlCommand command = new SqlCommand(sql, Database.connection);
            var st = start.AddSeconds(1).ToString("HH:mm:ss");
            var et = end.AddSeconds(-1).ToString("HH:mm:ss");
            command.Parameters.AddWithValue("starttime", st);
            command.Parameters.AddWithValue("endtime", et);
            command.Parameters.AddWithValue("date", date);
            if (OpenConnection())
            {
                var a = command.ExecuteReader();
                while (a.Read())
                {
                    Boat n = new Boat(a.GetInt32(0), a.GetString(1), a.GetInt32(2), a.GetInt32(3), a.GetBoolean(4), a.GetBoolean(5));
                    list.Add(n);
                }
                command.Dispose();
                connection.Close();
            }
             return list;
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


    }
}