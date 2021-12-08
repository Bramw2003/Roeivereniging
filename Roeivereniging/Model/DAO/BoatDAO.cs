using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Model.DAO {
    public class BoatDAO : IBoat {
        public bool AddBoat(string name, BoatType type) {
            Database.Init();
            string sql = "INSERT INTO boat(name, typesID) VALUES (@name, @type)";
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("name", name);
            command.Parameters.AddWithValue("type", type);
            if (Database.OpenConnection()) {
                return command.ExecuteNonQuery() == 1;
            }
            return false;
        }

        public Boat GetBoatByID(int id) {
            Database.Init();
            String sql = "SELECT [boat].[ID], [boat].[name], [types].[capacity],[types].[category],[types].[steer],[types].[sculling], (SELECT CASE WHEN EXISTS ( SELECT * FROM[brokenboat] WHERE boatID = boat.[ID]) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END) FROM [boat] JOIN[types] ON[types].[ID] =[boat].[typesID] WHERE[boat].[ID] = @id";
            Boat n = null;
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("id", id);
            if (Database.OpenConnection()) {
                var a = command.ExecuteReader(System.Data.CommandBehavior.SingleResult);
                while (a.Read()) {
                    n = new Boat(a.GetInt32(0), a.GetString(1), a.GetInt32(2), a.GetInt32(3), a.GetBoolean(4), a.GetBoolean(5), a.GetBoolean(6));
                }
                command.Dispose();
                Database.connection.Close();
            }
            return n;

        }

        public List<Boat> GetAll() {
            Database.Init();
            String sql = "SELECT [boat].[ID], [boat].[name], [types].[capacity],[types].[category],[types].[steer],[types].[sculling], (SELECT CASE WHEN EXISTS ( SELECT * FROM[brokenboat] WHERE boatID = boat.[ID]) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END) FROM [boat] JOIN[types] ON[types].[ID] =[boat].[typesID]";
            List<Boat> list = new List<Boat>();
            SqlCommand command = new SqlCommand(sql, Database.connection);
            if (Database.OpenConnection()) {
                var a = command.ExecuteReader();
                while (a.Read()) {
                    Boat n = new Boat(a.GetInt32(0), a.GetString(1), a.GetInt32(2), a.GetInt32(3), a.GetBoolean(4), a.GetBoolean(5), a.GetBoolean(6));
                    list.Add(n);
                }
                command.Dispose();
                Database.connection.Close();
            }
            return list;
        }

        public bool insert(Boat boat) {
            throw new NotImplementedException();
        }

        public bool Delete(Boat boat) {
            throw new NotImplementedException();
        }

        public int FindTypeIDByDetails(int capacity, int category, bool steer, bool sculling) {
            Database.Init();
            int id = 0;
            String sql = "SELECT ID FROM types WHERE capacity = @capacity AND category = @category AND steer = @steer AND sculling = @sculling";
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("capacity", capacity);
            command.Parameters.AddWithValue("category", category);
            command.Parameters.AddWithValue("steer", steer);
            command.Parameters.AddWithValue("sculling", sculling);
            if (Database.OpenConnection()) {
                var a = command.ExecuteReader();
                while (a.Read()) {
                    id = a.GetInt32(0);
                }
                command.Dispose();
                Database.connection.Close();
            }
            return id;
        }
    }
}
