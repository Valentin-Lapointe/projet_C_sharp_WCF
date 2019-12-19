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
    public class Game
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

        private string _name;

        [DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private DateTime _createdAt;

        [DataMember]
        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = value; }
        }

        private DateTime? _endedAt;

        [DataMember]
        public DateTime? EndedAt
        {
            get { return _endedAt; }
            set { _endedAt = value; }
        }

        #endregion

        #region Méthodes SQL
        public Game GetGameById(int id)
        {
            Game game = new Game();
            string query = "SELECT * FROM game WHERE id_game = ?id";
            using (MySqlConnection cn = new MySqlConnection(cs))
            {
                cn.Open();
                MySqlDataReader dr = null;
                using (MySqlCommand cmd = new MySqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("?id", id);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    game.Id = dr.GetInt32(dr.GetOrdinal("id_game"));
                    game.Name = dr.GetString(dr.GetOrdinal("name"));
                    game.CreatedAt = dr.GetDateTime(dr.GetOrdinal("created_at"));
                    game.EndedAt = dr.IsDBNull(dr.GetOrdinal("ended_at")) ? null : (DateTime?)dr.GetDateTime(dr.GetOrdinal("ended_at"));

                }
                cn.Close();
            }
            return game;
        }
        public Game GetLastGame()
        {
            Game game = new Game();
            string query = "SELECT * FROM game ORDER BY id_game DESC LIMIT 1";
            using (MySqlConnection cn = new MySqlConnection(cs))
            {
                cn.Open();
                MySqlDataReader dr = null;
                using (MySqlCommand cmd = new MySqlCommand(query, cn))
                {
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    game.Id = dr.GetInt32(dr.GetOrdinal("id_game"));
                    game.Name = dr.GetString(dr.GetOrdinal("name"));
                    game.CreatedAt = dr.GetDateTime(dr.GetOrdinal("created_at"));
                    game.EndedAt = dr.IsDBNull(dr.GetOrdinal("ended_at")) ? null : (DateTime?)dr.GetDateTime(dr.GetOrdinal("ended_at"));

                }
                cn.Close();
            }
            return game;
        }

        public bool AddGame(Game game)
        {
            bool ok = false;

            string query = "INSERT INTO game(name, created_at, ended_at)" +
                           "VALUES(?name, ?created_at, ?ended_at)";

            using (MySqlConnection cn = new MySqlConnection(cs))
            {
                cn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("?name", game.Name);
                    cmd.Parameters.AddWithValue("?created_at", game.CreatedAt);
                    cmd.Parameters.AddWithValue("?ended_at", game.EndedAt);
                    ok = Convert.ToBoolean(cmd.ExecuteNonQuery());
                }
                cn.Close();
            }
            return ok;
        }

        public bool UpdateGame(Game game)
        {
            bool ok = false;

            string query = "UPDATE game SET name = ?name, created_at = ?created_at, ended_at = ?ended_at WHERE id_game = ?id_game";
            using (MySqlConnection cn = new MySqlConnection(cs))
            {
                cn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("?id_game", game.Id);
                    cmd.Parameters.AddWithValue("?name", game.Name);
                    cmd.Parameters.AddWithValue("?created_at", game.CreatedAt);
                    cmd.Parameters.AddWithValue("?ended_at", game.EndedAt);

                    ok = Convert.ToBoolean(cmd.ExecuteNonQuery());
                }
                cn.Close();
            } 
            return ok;
        }

        public bool DeleteGame(int id)
        {
            bool ok = false;
            string query = "DELETE FROM game WHERE id_game = ?id";

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