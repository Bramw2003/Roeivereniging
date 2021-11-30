using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Model.DAO
{
    public static class Defect
    {
        /// <summary>
        /// Get all defects
        /// </summary>
        /// <returns>List of Defects</returns>
        public static List<Model.Defect> GetAll()
        {
            Database.Init();

            string sql = "SELECT [title], [description], [LIDID], [boatID] FROM [brokenboat]";
            List<Model.Defect> list = new List<Model.Defect>();
            SqlCommand command = new SqlCommand(sql, Database.connection);
            if (Database.OpenConnection())
            {
                var a = command.ExecuteReader();
                while (a.Read())
                {
                    Model.Defect defect = new Model.Defect(a.GetString(0),a.GetString(1),Member.GetById(a.GetInt32(2)),Boat.GetBoatByID(a.GetInt32(3)));
                    list.Add(defect);
                }
                command.Dispose();
                Database.connection.Close();
            }
            return list;
        }

        /// <summary>
        /// Get a list of defects for a particular boat/boatID
        /// </summary>
        /// <param name="boatID"></param>
        /// <returns></returns>
        public static List<Model.Defect> GetByBoatID(int boatID)
        {
            Database.Init();

            string sql = "SELECT [title], [description], [LIDID], [boatID] FROM [brokenboat] WHERE [boatID]=@id";
            List<Model.Defect> list = new List<Model.Defect>();
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("id", boatID);
            if (Database.OpenConnection())
            {
                var a = command.ExecuteReader();
                while (a.Read())
                {
                    Model.Defect defect = new Model.Defect(a.GetString(0), a.GetString(1), Member.GetById(a.GetInt32(2)), Boat.GetBoatByID(a.GetInt32(3)));
                    list.Add(defect);
                }
                command.Dispose();
                Database.connection.Close();
            }
            return list;
        }
        
        /// <summary>
        /// Get a list of defects for a particular boat
        /// </summary>
        /// <param name="boat"></param>
        /// <returns></returns>
        public static List<Model.Defect> GetByBoat(Model.Boat boat)
        {
            return GetByBoatID(boat.id);
        }

        /// <summary>
        /// Add a defect to the database
        /// </summary>
        /// <param name="defect"></param>
        /// <returns></returns>
        public static bool Add(Model.Defect defect)
        {
            Database.Init();
            string sql = "INSERT INTO brokenboat(title, description, LIDID, boatID) VALUES (@title, @desc, @memberID, @boatID)";
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("title", defect.title);
            command.Parameters.AddWithValue("desc", defect.description);
            command.Parameters.AddWithValue("memberID", defect.reporter.GetId());
            command.Parameters.AddWithValue("boatID", defect.boat.id);
            if (Database.OpenConnection())
            {
                return command.ExecuteNonQuery() == 1;
            }
            command.Dispose();
            Database.connection.Dispose();
            return false;
        }
    }
}
