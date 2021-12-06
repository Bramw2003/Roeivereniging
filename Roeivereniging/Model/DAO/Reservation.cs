using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Model.DAO
{
    public static class Reservation
    {
        /// <summary>
        /// Get all reservations in the database
        /// </summary>
        /// <returns></returns>
        public static List<Model.Reservation> GetAll()
        {
            Database.Init();
            String sql = "SELECT starttime, boatID, endtime, memberID, [date] FROM reservations";
            List<Model.Reservation> list = new List<Model.Reservation>();
            SqlCommand command = new SqlCommand(sql, Database.connection);
            if (Database.OpenConnection())
            {
                var a = command.ExecuteReader();
                while (a.Read())
                {
                    DateTime start = a.GetDateTime(0);
                    DateTime end = a.GetDateTime(2);
                    DateTime date = a.GetDateTime(4).Date;
                    start = date + new TimeSpan(start.Hour, start.Minute, start.Second);
                    end = date + new TimeSpan(end.Hour, end.Minute, end.Second);
                    Model.Reservation n = new Model.Reservation(start, end, a.GetInt32(4), a.GetInt32(1));
                    list.Add(n);
                }
                command.Dispose();
                Database.connection.Close();
            }
            return list;
        }

        /// <summary>
        /// Get all reservations for a certain member
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static List<Model.Reservation> GetAllByMember(Model.Member member)
        {
            Database.Init();
            String sql = "SELECT starttime, boatID, endtime, memberID, [date] FROM reservations WHERE memberID=@id";
            List<Model.Reservation> list = new List<Model.Reservation>();
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("id",member.GetId());
            if (Database.OpenConnection())
            {
                var a = command.ExecuteReader();
                while (a.Read())
                {
                    DateTime start = a.GetDateTime(4).Date;
                    DateTime end = a.GetDateTime(4).Date;

                    start = start + a.GetTimeSpan(0);
                    end = end + a.GetTimeSpan(2);
                    Model.Reservation n = new Model.Reservation(start, end, a.GetInt32(1), a.GetInt32(3));
                    list.Add(n);
                }
                command.Dispose();
                Database.connection.Close();
            }
            return list;
        }

        /// <summary>
        /// Reserve the boat on the given date and between the start and end times
        /// </summary>
        /// <param name="boat"></param>
        /// <param name="date">Start date and time</param>
        /// <param name="endTime">End time</param>
        /// <param name="member">Member making the reservation</param>
        /// <returns>True if a reservation was added to the database</returns>
        public static bool ReserveBoat(Model.Boat boat, DateTime date, DateTime endTime, Model.Member member)
        {
            Database.Init();
            String sql = "INSERT INTO [reservations] (starttime, boatID, endtime, memberID, [date]) VALUES (@starttime, @boatid, @endtime, @memberid, @date)";

            using (SqlCommand command = new SqlCommand(sql, Database.connection))
            {
                command.Parameters.AddWithValue("starttime", date.ToString("HH:mm:ss"));
                command.Parameters.AddWithValue("boatid", boat.id);
                command.Parameters.AddWithValue("endtime", endTime.ToString("HH:mm:ss"));
                command.Parameters.AddWithValue("memberid", member.GetId());
                command.Parameters.AddWithValue("date", date.Date);
                bool result = false;
                if (Database.OpenConnection())
                {
                    result = (int)command.ExecuteNonQuery() == 1;
                }
                Database.connection.Close();
                return result;
            }
        }
    }
}
