﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;
using ServiceSynchroCheckers;
using System.Configuration;

namespace ServiceSynchroCheckers
{
    [DataContract]
    public class User
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

        private string _pseudo;

        [DataMember]
        public string Pseudo
        {
            get { return _pseudo; }
            set { _pseudo = value; }
        }

        private string _mail;

        [DataMember]
        public string Mail
        {
            get { return _mail; }
            set { _mail = value; }
        }

        private string _password;

        [DataMember]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private int? _score;

        [DataMember]
        public int? Score
        {
            get { return _score; }
            set { _score = value; }
        }

        private DateTime _createdAt;

        [DataMember]
        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = value; }
        }

        private int _idRole;

        [DataMember]
        public int IdRole
        {
            get { return _idRole; }
            set { _idRole = value; }
        }

        private bool _isAvailable;

        [DataMember]
        public bool IsAvailable
        {
            get { return _isAvailable; }
            set { _isAvailable = value; }
        }

        #endregion

    
        #region Méthodes SQL
        public User GetUserById(int id)
        {
            User user = new User();
            string query = "SELECT * FROM user WHERE id_user = ?id";
            using (MySqlConnection cn = new MySqlConnection(cs))
            {
                cn.Open();
                MySqlDataReader dr = null;
                using (MySqlCommand cmd = new MySqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("?id", id);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    user.Id = dr.GetInt32(dr.GetOrdinal("id_user"));
                    user.Pseudo = dr.GetString(dr.GetOrdinal("pseudo"));
                    user.Mail = dr.GetString(dr.GetOrdinal("mail"));
                    user.Password = dr.GetString(dr.GetOrdinal("password"));
                    if (!dr.IsDBNull(dr.GetOrdinal("score")))
                    {
                        user.Score = dr.GetInt32(dr.GetOrdinal("score"));
                    }
                    user.CreatedAt = dr.GetDateTime(dr.GetOrdinal("created_at"));
                    user.IdRole = dr.GetInt32(dr.GetOrdinal("id_role"));
                    user.IsAvailable = dr.GetBoolean(dr.GetOrdinal("is_available"));
                }
                cn.Close();
            }
            return user;
        }


        public bool AddUser(User user)
        {
            bool ok = false;

            string query = "INSERT INTO user(pseudo, mail, password, score, id_role, created_at, is_available)" +
                           "VALUES(?pseudo, ?mail, ?password, ?score, ?id_role, ?created_at, ?is_available)";


            using (MySqlConnection cn = new MySqlConnection(cs))
            {
                cn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("?pseudo", user.Pseudo);
                    cmd.Parameters.AddWithValue("?mail", user.Mail);
                    cmd.Parameters.AddWithValue("?password", user.Password);
                    cmd.Parameters.AddWithValue("?score", user.Score);
                    cmd.Parameters.AddWithValue("?id_role", user.IdRole);
                    cmd.Parameters.AddWithValue("?created_at", user.CreatedAt);
                    cmd.Parameters.AddWithValue("?is_available", user.IsAvailable);

                    ok = Convert.ToBoolean(cmd.ExecuteNonQuery());
                }
                cn.Close();
            }
            return ok;
        }

        public bool UpdateUser(User user)
        {
            bool ok = false;

            string query = "UPDATE user SET pseudo = ?pseudo, mail = ?mail, password = ?password, score = ?score, id_role = ?id_role, created_at = ?created_at, is_available = ?is_available WHERE id_user = ?id_user";
            using (MySqlConnection cn = new MySqlConnection(cs))
            {
                cn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("?id_user", user.Id);
                    cmd.Parameters.AddWithValue("?pseudo", user.Pseudo);
                    cmd.Parameters.AddWithValue("?mail", user.Mail);
                    cmd.Parameters.AddWithValue("?password", user.Password);
                    cmd.Parameters.AddWithValue("?score", user.Score);
                    cmd.Parameters.AddWithValue("?id_role", user.IdRole);
                    cmd.Parameters.AddWithValue("?created_at", user.CreatedAt);
                    cmd.Parameters.AddWithValue("?is_available", user.IsAvailable);

                    ok = Convert.ToBoolean(cmd.ExecuteNonQuery());
                }
                cn.Close();
            }

            return ok;
        }

        public bool DeleteUser(int id)
        {
            bool ok = false;
            string query = "DELETE FROM user WHERE id_user = ?id";

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

        public List<User> GetUsersAvailable()
        {
            List<User> users = new List<User>();

            string query = "SELECT * FROM user WHERE is_available IS TRUE";

            using (MySqlConnection cn = new MySqlConnection(cs))
            {
                cn.Open();
                MySqlDataReader dr = null;
                using (MySqlCommand cmd = new MySqlCommand(query, cn))
                {
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {

                            User user = new User();
                            user.Id = dr.GetInt32(dr.GetOrdinal("id_user"));
                            user.Pseudo = dr.GetString(dr.GetOrdinal("pseudo"));
                            user.Mail = dr.GetString(dr.GetOrdinal("mail"));
                            user.Password = dr.GetString(dr.GetOrdinal("password"));
                            if (!dr.IsDBNull(dr.GetOrdinal("score")))
                            {
                                user.Score = dr.GetInt32(dr.GetOrdinal("score"));
                            }
                            user.CreatedAt = dr.GetDateTime(dr.GetOrdinal("created_at"));
                            user.IdRole = dr.GetInt32(dr.GetOrdinal("id_role"));
                            user.IsAvailable = dr.GetBoolean(dr.GetOrdinal("is_available"));

                            users.Add(user);
                        }

                    }

                    dr.Close();

                    cn.Close();
                }
                return users;
            }

        } 
        
        public bool UpdateAvailabilityUser(int id, bool is_available)
        {
            bool ok = false;

            string query = "UPDATE user SET is_available = ?is_available WHERE id_user = ?id_user";

            using(MySqlConnection cn = new MySqlConnection(cs))
            {
                cn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("?id_user", id);
                    cmd.Parameters.AddWithValue("?is_available", is_available);

                    ok = Convert.ToBoolean(cmd.ExecuteNonQuery());
                }
                cn.Close();
            }
            return ok;
        }

        #endregion

    }
}