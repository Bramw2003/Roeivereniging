using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Linq;

namespace Model.DAO
{
    public class EventsDAO
    {
        public List<Event> GetAll()
        {
            Database.Init();
            String sql = "SELECT [maxMembers],[creator],[start],[end],[name],[type],[ID],[map], DATALENGTH(map) FROM [Events]";
            IMember MemberD = new MemberDAO();
            List<Event> list = new List<Event>();
            SqlCommand command = new SqlCommand(sql, Database.connection);
            if (Database.OpenConnection())
            {
                var a = command.ExecuteReader();
                while (a.Read())
                {
                    int i = 0;
                    long length;
                    try
                    {
                        length = a.GetInt64(7);
                    }
                    catch (Exception)
                    {
                        length = 0;
                    }
                    byte[] Data = null;
                    if (length > 0)
                    {
                        Data = new byte[length];
                        a.GetBytes(8, 0, Data, 0, (int)length);
                    }
                    Event n = new Event(a.GetInt32(0), MemberD.GetById(a.GetInt32(1)), a.GetDateTime(2), a.GetDateTime(3),a.GetString(4),a.GetString(5),a.GetInt32(6),Data);
                    n.availableBoats = GetAvailableBoatsByEventID(n.Id);
                    n.members = GetMembersByEventID(n.Id);
                    list.Add(n);
                }
                command.Dispose();
                Database.connection.Close();
            }
            return list;
        }

        public List<Event> GetByCreator(Member creator)
        {
            return GetAll().Where(x => x.creator == creator).ToList();
        }

        /// <summary>
        /// Reserve a boat for a certain event
        /// </summary>
        /// <param name="member">Member reserving the boat</param>
        /// <param name="boat">The boat that is beeing reserved</param>
        /// <param name="event">The event that the member tries to reserve in</param>
        /// <returns></returns>
        public bool ReserveBoat(Member member, Boat boat, Event @event)
        {
            Database.Init();
            string sql = "UPDATE [Events_boat] SET [member] = @memberID WHERE [EventsID] = @eventid AND [BoatID] = @boatID AND [member] IS NULL";
            SqlCommand command = new SqlCommand(sql,Database.connection);
            command.Parameters.AddWithValue("boatID",boat.id);
            command.Parameters.AddWithValue("memberID",member.id);
            command.Parameters.AddWithValue("eventid",@event.Id);
            
            if (Database.OpenConnection())
            {
                return command.ExecuteNonQuery() > 0;
            }
            return false;

        }

        public bool AddMember(Event @event, Member member)
        {
            Database.Init();
            string sql = "INSERT Events_member (EventsID,memberID) VALUES (@eventid,@memberID)";
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("memberID", member.id);
            command.Parameters.AddWithValue("eventid", @event.Id);
            if (Database.OpenConnection())
            {
                try
                {
                    return command.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {

                }
            }
            return false;
        }

        /// <summary>
        /// Get all boats unreserved boats for a certain event
        /// </summary>
        /// <param name="id">event ID</param>
        /// <returns>All unreserved boats</returns>
        private List<Boat> GetAvailableBoatsByEventID(int id)
        {
            BoatDAO dao = new BoatDAO();
            Database.Init();
            String sql = "SELECT [boatID] FROM [Events_boat] WHERE [EventsID] = @id AND [member] IS NULL";
            List<Boat> list = new List<Boat>();
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("id", id);

            if (Database.OpenConnection())
            {
                var a = command.ExecuteReader();
                while (a.Read())
                {
                    Boat n = dao.GetBoatByID(a.GetInt32(0));
                    list.Add(n);
                }
                command.Dispose();
                Database.connection.Close();
            }
            return list;
        }

        /// <summary>
        /// Get all boats unreserved boats for a certain event
        /// </summary>
        /// <param name="event">Event</param>
        /// <returns>All unreserve boats</returns>
        private List<Boat> GetAvailableBoatsByEventID(Event @event)
        {
            return GetAvailableBoatsByEventID(@event.Id);
        }

        /// <summary>
        /// Get all joined members for an event
        /// </summary>
        /// <param name="id">Id of the event</param>
        /// <returns>List of members attending the event</returns>
        public List<Member> GetMembersByEventID(int id)
        {
            MemberDAO dao = new MemberDAO();
            Database.Init();
            String sql = "SELECT [memberID] FROM [Events_member] WHERE [EventsID] = @id";
            List<Member> list = new List<Member>();
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("id", id);

            if (Database.OpenConnection())
            {
                var a = command.ExecuteReader();
                while (a.Read())
                {
                    Member n = dao.GetById(a.GetInt32(0));
                    if (n != null)
                        list.Add(n);
                }
                command.Dispose();
                Database.connection.Close();
            }
            return list;
        }

        public List<Member> GetMembersByEventID(Event @event)
        {
            return GetMembersByEventID(@event.Id);
        }

        public bool AddBoatsToEvent(List<Boat> boats, Event @event)
        {
            foreach(Boat boat in boats)
            {
                AddBoatToEvent(boat, @event);
            }
            return true;
        }

        public bool AddBoatToEvent(Boat boat, Event @event)
        {
            Database.Init();
            string sql = "INSERT INTO Events_boat (EventsID,boatID) VALUES (@eventid, @boatid)";
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("boatid", boat.id);
            command.Parameters.AddWithValue("eventid", @event.Id);
            if (Database.OpenConnection())
            {
                return command.ExecuteNonQuery() > 0;
            }
            return false;
        }

        public bool RemoveBoatsFromEvent(List<Boat> boats, Event @event)
        {
            foreach (Boat boat in boats)
            {
                RemoveBoatFromEvent(boat, @event);
            }
            return true;
        }

        public bool RemoveBoatFromEvent(Boat boat, Event @event)
        {
            Database.Init();
            string sql = "DELETE FROM Events_boat WHERE EventsID = @eventid AND boatID = @boatid";
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("boatid", boat.id);
            command.Parameters.AddWithValue("eventid", @event.Id);
            if (Database.OpenConnection())
            {
                return command.ExecuteNonQuery() > 0;
            }
            return false;
        }
        
        /// <summary>
        /// Checks if a user has a reservation for the event
        /// </summary>
        /// <param name="member"></param>
        /// <param name="event"></param>
        /// <returns></returns>
        public bool HasReservationForEvent(Member member, Event @event)
        {
            string sql = "SELECT HasReservation=(SELECT CASE WHEN EXISTS ( SELECT * FROM Events_boat WHERE [Events_boat].[member] = @memberID AND [Events_boat].EventsID = @eventID) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END)";
            Database.Init();
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("memberID", member.id);
            command.Parameters.AddWithValue("eventID", @event.Id);
            if (Database.OpenConnection())
            {
                return (bool)command.ExecuteScalar();
            }
            return false;
        }

        public List<Boat> GetAllBoatsByEventID(int id)
        {
            BoatDAO dao = new BoatDAO();
            Database.Init();
            String sql = "SELECT [boatID] FROM [Events_boat] WHERE [EventsID] = @id";
            List<Boat> list = new List<Boat>();
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("id", id);

            if (Database.OpenConnection())
            {
                var a = command.ExecuteReader();
                while (a.Read())
                {
                    Boat n = dao.GetBoatByID(a.GetInt32(0));
                    list.Add(n);
                }
                command.Dispose();
                Database.connection.Close();
            }
            return list;
        }

        public bool UpdateMap(byte[] map, Event @event)
        {
            Database.Init();
            string sql = "UPDATE Events SET map=@map WHERE ID=@eventid";
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("eventid", @event.Id);
            command.Parameters.AddWithValue("map", map);

            if (Database.OpenConnection())
            {
                return command.ExecuteNonQuery() > 0;
            }
            return false;
        }

        public bool UpdateEvent(Event @event) {
            Database.Init();
            string sql = "UPDATE Events SET description=@kaas, name=@name, type=@type, maxMembers=@maxMembers WHERE ID=@eventid";
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("kaas", @event.description);
            command.Parameters.AddWithValue("name", @event.name);
            command.Parameters.AddWithValue("type", @event.type);
            command.Parameters.AddWithValue("maxMembers", @event.maxMembers);
            command.Parameters.AddWithValue("eventid", @event.Id);

            if (Database.OpenConnection())
            {
                return command.ExecuteNonQuery() > 0;
            }
            return false;
        }
        
    }
}
