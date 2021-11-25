using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Model.DAO
{
    public static class Reservation
    {
        public static List<Model.Reservation> GetAll()
        {
            Database.Init();
            String sql = "SELECT starttime, boatID, endtime, LIDID, [date] FROM reservations";
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
        public static List<Model.Reservation> GetAllByMember(Model.Member member)
        {
            Database.Init();
            String sql = "SELECT starttime, boatID, endtime, LIDID, [date] FROM reservations WHERE LIDID=@id";
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
        public static bool ReserveBoat(Model.Boat boat, DateTime date, DateTime endTime, Model.Member member)
        {
            Database.Init();
            String sql = "INSERT INTO [reservations] (starttime, boatID, endtime, LIDID, [date]) VALUES (@starttime, @boatid, @endtime, @memberid, @date)";

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
