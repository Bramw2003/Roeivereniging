using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Linq;

namespace Model.DAO {
    public class DefectDAO {

        private MemberDAO tempMemberDB = new MemberDAO();
        private BoatDAO tempBoatDB = new BoatDAO();
        /// <summary>
        /// Get all defects
        /// </summary>
        /// <returns>List of Defects</returns>
        public List<Defect> GetAll() {
            Database.Init();
            string sql = "SELECT [title], [description], [memberID], [boatID], [seaworthy] FROM [brokenboat]";
            List<Defect> list = new List<Defect>();
            SqlCommand command = new SqlCommand(sql, Database.connection);
            if (Database.OpenConnection()) {
                var a = command.ExecuteReader();
                while (a.Read()) {
                    Defect defect = new Defect(a.GetString(0), a.GetString(1), tempMemberDB.GetById(a.GetInt32(2)), tempBoatDB.GetBoatByID(a.GetInt32(3)),a.GetBoolean(4));
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
        public List<Defect> GetByBoatID(int boatID) {
            List<Defect>list = GetAll().Where(x=> x.boat.id == boatID).ToList();
            return list;
        }

        /// <summary>
        /// Get a list of defects for a particular boat
        /// </summary>
        /// <param name="boat"></param>
        /// <returns></returns>
        public List<Defect> GetByBoat(Boat boat) {
            return  GetByBoatID(boat.id);
        }

        /// <summary>
        /// Add a defect to the database
        /// </summary>
        /// <param name="defect"></param>
        /// <returns></returns>
        public bool Add(Defect defect) {
            Database.Init();
            string sql = "INSERT INTO brokenboat(title, description, memberID, boatID, [date], seaworthy) VALUES (@title, @desc, @memberID, @boatID, GETDATE(), @seaworthy)";
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("title", defect.title);
            command.Parameters.AddWithValue("desc", defect.description);
            command.Parameters.AddWithValue("memberID", defect.reporter.GetId());
            command.Parameters.AddWithValue("boatID", defect.boat.id);
            command.Parameters.AddWithValue("seaworthy", defect.seaworthy);
            if (Database.OpenConnection()) {
                return command.ExecuteNonQuery() == 1;
            }
            command.Dispose();
            Database.connection.Dispose();
            return false;
        }
    }
}
