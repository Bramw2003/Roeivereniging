using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Model.DAO
{
    public class Boat
    {
        public static bool AddBoat(string name, BoatType type)
        {
            Database.Init();
            string sql = "INSERT INTO boat(name, typesID) VALUES (@name, @type)";
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("name", name);
            command.Parameters.AddWithValue("type", type);
            if (Database.OpenConnection())
            {
                return command.ExecuteNonQuery() == 1;
            }
            return false;
        }
        public static Model.Boat GetBoatByID(int id)
        {
            Database.Init();
            String sql = "SELECT [boat].[ID], [boat].[name], [types].[capacity],[types].[category],[types].[steer],[types].[sculling] FROM [boat] JOIN[types] ON[types].[ID] =[boat].[typesID] WHERE[boat].[ID] = @id";
            Model.Boat n = null;
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("id", id);
            if (Database.OpenConnection())
            {
                var a = command.ExecuteReader(System.Data.CommandBehavior.SingleResult);
                while (a.Read())
                {
                    n = new Model.Boat(a.GetInt32(0), a.GetString(1), a.GetInt32(2), a.GetInt32(3), a.GetBoolean(4), a.GetBoolean(5));
                }
                command.Dispose();
                Database.connection.Close();
            }
            return n;

        }
        public static List<Model.Boat> GetBoatAll()
        {
            Database.Init();
            String sql = "SELECT [boat].[ID], [boat].[name], [types].[capacity],[types].[category],[types].[steer],[types].[sculling] FROM [boat] JOIN[types] ON[types].[ID] =[boat].[typesID]";
            List<Model.Boat> list = new List<Model.Boat>();
            SqlCommand command = new SqlCommand(sql, Database.connection);
            if (Database.OpenConnection())
            {
                var a = command.ExecuteReader();
                while (a.Read())
                {
                    Model.Boat n = new Model.Boat(a.GetInt32(0), a.GetString(1), a.GetInt32(2), a.GetInt32(3), a.GetBoolean(4), a.GetBoolean(5));
                    list.Add(n);
                }
                command.Dispose();
                Database.connection.Close();
            }
            return list;
        }
    }
}
