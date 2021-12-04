using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DAO
{
    internal class Repairs
    {
        public List<Repairs> GetAll() { return new List<Repairs>(); }
        public bool AddRepair()
        {
            string sqlInsert = "INSERT INTO repairs(title, [description], reporterID, boatID, [date], repairnote, repairerID) SELECT title, [description], LIDID, boatID, GETDATE(), @repairnote, @member FROM brokenboat WHERE boatID = @boatid";
            string sqlDelete = "DELETE FROM brokenboat WHERE boatID=@boatid";
            return false;
        }
    }
}
