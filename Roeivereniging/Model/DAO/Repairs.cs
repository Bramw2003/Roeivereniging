using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Model.DAO
{
    public static class Repairs
    {
        /// <summary>
        /// Add a new repair the database
        /// </summary>
        /// <param name="defect"></param>
        /// <param name="repairNote"></param>
        /// <param name="repairer"></param>
        /// <returns></returns>
        public static bool AddRepair(Model.Defect defect, string repairNote, Model.Member repairer)
        {
            string sqlInsert = "INSERT INTO repairs(title, [description], reportdate, reporterID, boatID, [repairdate], repairnote, repairerID) SELECT title, [description], [date], memberID, boatID, GETDATE(), @repairnote, @member FROM brokenboat WHERE boatID = @boatid";
            string sqlDelete = "DELETE FROM brokenboat WHERE boatID=@boatid";

            Database.Init();
            bool result = false;
            SqlCommand insertCommand = new SqlCommand(sqlInsert, Database.connection);
            {
                insertCommand.Parameters.AddWithValue("repairnote", repairNote);
                insertCommand.Parameters.AddWithValue("member", repairer.GetId());
                insertCommand.Parameters.AddWithValue("boatid", defect.boat.id);

                if (Database.OpenConnection())
                {
                    var a = insertCommand.ExecuteNonQuery();
                    result = a == 1;
                    insertCommand.Dispose();
                    Database.connection.Close();
                }
            }
            // If the insert fails the brokenboat row shouldn't be deleted
            if (!result)
            {
                return false;
            }
            SqlCommand deleteCommand = new SqlCommand(sqlDelete, Database.connection);
            {
                deleteCommand.Parameters.AddWithValue("boatid", defect.boat.id);

                if (Database.OpenConnection())
                {
                    var a = deleteCommand.ExecuteNonQuery();
                    result = a == 1;
                    deleteCommand.Dispose();
                    Database.connection.Close();
                }

            }
            return result;
        }
    }
}
