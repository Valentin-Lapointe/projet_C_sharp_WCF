using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServiceSynchroCheckers.Classes
{
    [DataContract]
    public class Role
    {
        public static string cs = ConfigurationManager.ConnectionStrings["csCheckers"].ConnectionString;

        #region Propriétés
        private int _id;

        [DataMember]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _value;

        [DataMember]
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        #endregion

        #region Méthodes SQL
        public Role GetRoleById(int id)
        {
            Role role = new Role();
            string query = "SELECT * FROM role WHERE id_role = ?id";
            using (MySqlConnection cn = new MySqlConnection(cs))
            {
                cn.Open();
                MySqlDataReader dr = null;
                using (MySqlCommand cmd = new MySqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("?id", id);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    role.Id = dr.GetInt32(dr.GetOrdinal("id_role"));
                    role.Value = dr.GetString(dr.GetOrdinal("value"));
                }
                cn.Close();
            }
            return role;
        }

        public bool AddRole(Role role)
        {
            bool ok = false;

            string query = "INSERT INTO role(value)" +
                           "VALUES(?value)";
            
            using (MySqlConnection cn = new MySqlConnection(cs))
            {
                cn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("?value", role.Value);
                    ok = Convert.ToBoolean(cmd.ExecuteNonQuery());
                }
                cn.Close();
            }
            return ok;
        }

        public bool UpdateRole(Role role)
        {
            bool ok = false;

            string query = "UPDATE role SET value = ?value WHERE id_role = ?id_role";
            using (MySqlConnection cn = new MySqlConnection(cs))
            {
                cn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("?id_role", role.Id);
                    cmd.Parameters.AddWithValue("?value", role.Value);

                    ok = Convert.ToBoolean(cmd.ExecuteNonQuery());
                }
                cn.Close();
            }

            return ok;
        }

        public bool DeleteRole(int id)
        {
            bool ok = false;
            string query = "DELETE FROM role WHERE id_role = ?id";

            using (MySqlConnection cn = new MySqlConnection(cs))
            {
                cn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("?id", id);
                    ok = Convert.ToBoolean(cmd.ExecuteNonQuery());
                }
                cn.Close();
            }
            return ok;
        }

        #endregion
    }
}