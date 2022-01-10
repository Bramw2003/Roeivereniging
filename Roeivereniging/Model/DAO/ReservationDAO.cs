using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Linq;

namespace Model.DAO {
    public class ReservationDAO {

        /// <summary>
        /// Get all reservations in the database
        /// </summary>
        /// <returns></returns>
        public List<Reservation> GetAll() {
            Database.Init();
            MemberDAO tempMemberDB = new MemberDAO();
            BoatDAO tempBoatDB = new BoatDAO();
            String sql = "SELECT starttime, boatID, endtime, memberID, [date] FROM reservations";
            List<Reservation> list = new List<Reservation>();
            SqlCommand command = new SqlCommand(sql, Database.connection);
            if (Database.OpenConnection()) {
                var a = command.ExecuteReader();
                while (a.Read()) {
                    DateTime start = a.GetDateTime(4).Date;
                    DateTime end = a.GetDateTime(4).Date;

                    start = start + a.GetTimeSpan(0);
                    end = end + a.GetTimeSpan(2);
                    Reservation n = new Reservation(start, end, tempBoatDB.GetBoatByID(a.GetInt32(1)), tempMemberDB.GetById(a.GetInt32(3)));
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
        public List<Reservation> GetAllByMember(Member member) {
            return GetAll().Where(x=> x.member.GetId()==member.GetId()).ToList();
        }

        /// <summary>
        /// Reserve the boat on the given date and between the start and end times
        /// </summary>
        /// <param name="boat"></param>
        /// <param name="date">Start date and time</param>
        /// <param name="endTime">End time</param>
        /// <param name="member">Member making the reservation</param>
        /// <returns>True if a reservation was added to the database</returns>
        public int ReserveBoat(Boat boat, DateTime date, DateTime endTime, Member member, bool cancelable=true) {
            Database.Init();
            String sql = "INSERT INTO [reservations] (starttime, boatID, endtime, memberID, [date], [cancelable]) output inserted.ID VALUES (@starttime, @boatid, @endtime, @memberid, @date, @cancelable)";

            using (SqlCommand command = new SqlCommand(sql, Database.connection)) {
                command.Parameters.AddWithValue("starttime", date.ToString("HH:mm:ss"));
                command.Parameters.AddWithValue("boatid", boat.id);
                command.Parameters.AddWithValue("endtime", endTime.ToString("HH:mm:ss"));
                command.Parameters.AddWithValue("memberid", member.GetId());
                command.Parameters.AddWithValue("date", date.Date);
                command.Parameters.AddWithValue("cancelable", cancelable);
                int result = -1;
                if (Database.OpenConnection()) {
                    try
                    {
                        result = (int)command.ExecuteScalar();
                        
                    }
                    catch (Exception ex)
                    {
                        return -1;
                    }
                }
                Database.connection.Close();
                return result;
            }
        }

        /// <summary>
        /// Delete the matching reservation
        /// </summary>
        /// <param name="boat">boat</param>
        /// <param name="date">date</param>
        /// <param name="startTime">Start time</param>
        /// <param name="endTime">End time</param>
        /// <param name="member">Member from who the reservation is</param>
        /// <returns></returns>
        public void DeleteReservation(Boat boat, DateTime date, DateTime startTime, DateTime endTime, Member member) {
            Database.Init();
            String sql = "DELETE FROM [reservations] WHERE starttime = @starttime AND boatID = @boatid AND endtime = @endtime AND memberID = @memberid AND date = @date";

            using (SqlCommand command = new SqlCommand(sql, Database.connection)) {
                command.Parameters.AddWithValue("starttime", startTime.ToString("HH:mm:ss"));
                command.Parameters.AddWithValue("boatid", boat.id);
                command.Parameters.AddWithValue("endtime", endTime.ToString("HH:mm:ss"));
                command.Parameters.AddWithValue("memberid", member.GetId());
                command.Parameters.AddWithValue("date", date.Date);
                bool result = false;
                if (Database.OpenConnection()) {
                    result = (int)command.ExecuteNonQuery() == 1;
                }
                Database.connection.Close();
            }
        }

        public void DeleteReservation(Reservation reservation)
        {
            DeleteReservation(reservation.boat, reservation.date, reservation.startTime, reservation.endTime, reservation.member);
        }

        public void DeleteAllReservations(int memberID)
        {
            Database.Init();
            string sql = "DELETE FROM [reservations] WHERE memberID = @memberid";

            using (SqlCommand command = new SqlCommand(sql, Database.connection))
            {
                command.Parameters.AddWithValue("memberid", memberID);
                if (Database.OpenConnection())
                {
                    command.ExecuteNonQuery();
                }
                Database.connection.Close();
            }
        }

        public void DeleteAllFutureReservationsByBoat(Boat boat) {
            Database.Init();
            DateTime current = new DateTime();
            current = DateTime.Now;
            String sql = "DELETE FROM [reservations] WHERE starttime > @currentTime AND boatID = @boatid AND date = @currentDate OR date > @currentDate AND boatID = @boatid";

            using (SqlCommand command = new SqlCommand(sql, Database.connection)) {
                command.Parameters.AddWithValue("currentTime",current.ToString("HH:mm:ss"));
                command.Parameters.AddWithValue("boatid", boat.id);
                command.Parameters.AddWithValue("currentDate", current.Date);
                bool result = false;
                if (Database.OpenConnection()) {
                    result = (int)command.ExecuteNonQuery() == 1;
                }
                Database.connection.Close();
            }
        }
    }
}
