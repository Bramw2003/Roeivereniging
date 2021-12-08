using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Model.DAO
{
    public class CerticateDAO
    {
        public List<Certificate> GetAll()
        {
            Database.Init();
            String sql = "SELECT name FROM certificate";
            List<Certificate> list = new List<Certificate>();
            SqlCommand command = new SqlCommand(sql, Database.connection);
            if (Database.OpenConnection())
            {
                var a = command.ExecuteReader();
                while (a.Read())
                {
                    Certificate n = new Certificate(a.GetString(0));
                    list.Add(n);
                }
                command.Dispose();
                Database.connection.Close();
            }
            return list;
        }
        public List<Certificate> GetAvailableByMember(Member member)
        {
            Database.Init();
            String sql = @"SELECT name
                            FROM certificate
                            WHERE id NOT IN(
                            SELECT certificateid
                            FROM member_certificate
                            WHERE memberid = @memberid)";
            List<Certificate> list = new List<Certificate>();
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("memberid", member.GetId());
            if (Database.OpenConnection())
            {
                var a = command.ExecuteReader();
                while (a.Read())
                {
                    Certificate n = new Certificate(a.GetString(0));
                    list.Add(n);
                }
                command.Dispose();
                Database.connection.Close();
            }
            return list;
        }
        public List<Certificate> GetByMember(Member member)
        {
            Database.Init();
            String sql = @"select [name], [date]
                            FROM member_certificate
                            JOIN certificate ON certificateID = certificate.id
                            WHERE memberID = @memberid";
            List<Certificate> list = new List<Certificate>();
            SqlCommand command = new SqlCommand(sql, Database.connection);
            command.Parameters.AddWithValue("memberid", member.GetId());
            if (Database.OpenConnection())
            {
                var a = command.ExecuteReader();
                while (a.Read())
                {
                    Certificate n = new Certificate(a.GetString(0), a.GetDateTime(1));
                    list.Add(n);
                }
                command.Dispose();
                Database.connection.Close();
            }
            return list;
        }
        public bool AddToMember(Member member, Certificate certificate)
        {
            Database.Init();
            string sql = "INSERT INTO member_certificate (memberid, certificateid, [date]) VALUES (@memberid, (SELECT id FROM certificate WHERE name = @certname), GETDATE())";
            bool result = false;
            SqlCommand command = new SqlCommand(sql, Database.connection);
            {
                command.Parameters.AddWithValue("memberid", member.GetId());
                command.Parameters.AddWithValue("certname", certificate.name);
                if (Database.OpenConnection())
                {
                    var a = command.ExecuteNonQuery();
                    result = a == 1;
                    command.Dispose();
                    Database.connection.Close();
                }
            }
            return result;
        }
    }
}