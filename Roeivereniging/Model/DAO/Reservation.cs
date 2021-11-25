using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Model.DAO
{
    internal class Reservation
    {
        public static bool ReserveBoat(Model.Boat boat, DateTime date, DateTime endTime, Model.Member member)
        {
            Database.Init();
            String sql = "INSERT INTO [reservations] (starttime, boatID, endtime, LIDID, [date]) VALUES (@starttime, @boatid, @endtime, @memberid, @date)";

            using (SqlCommand command = new SqlCommand(sql, Database.connection))
            {
                command.Parameters.AddWithValue("starttime",date.ToString("HH:mm:ss"));
                command.Parameters.AddWithValue("boatid", boat.id);
                command.Parameters.AddWithValue("endtime", endTime.ToString("HH:mm:ss"));
                command.Parameters.AddWithValue("memberid", member.GetId());
                command.Parameters.AddWithValue("date",date.Date);
                bool result = (int)command.ExecuteScalar() == 1;
                command.Dispose();
                Database.connection.Close();
                return result;
            }
        }
    }
}
