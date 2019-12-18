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
    public class GameUser
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

        private int _IdGame;

        [DataMember]
        public int IdGame
        {
            get { return _IdGame; }
            set { _IdGame = value; }
        }

        private int _idUser;

        [DataMember]
        public int IdUser
        {
            get { return _idUser; }
            set { _idUser = value; }
        }

        private int _side;

        [DataMember]
        public int Side
        {
            get { return _side; }
            set { _side = value; }
        }

        private int? _result;

        [DataMember]
        public int? Result
        {
            get { return _result; }
            set { _result = value; }
        }

        #endregion

        #region Méthodes SQL
        public GameUser GetGameUserById(int id)
        {
            GameUser gameUser = new GameUser();
            string query = "SELECT * FROM game_user WHERE id_game_user = ?id";
            using (MySqlConnection cn = new MySqlConnection(cs))
            {
                cn.Open();
                MySqlDataReader dr = null;
                using (MySqlCommand cmd = new MySqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("?id", id);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    gameUser.Id = dr.GetInt32(dr.GetOrdinal("id_game_user"));
                    gameUser.IdGame = dr.GetInt32(dr.GetOrdinal("id_game"));
                    gameUser.IdUser = dr.GetInt32(dr.GetOrdinal("id_user"));
                    gameUser.Result = dr.IsDBNull(dr.GetOrdinal("result")) ? null : (int?)dr.GetInt32(dr.GetOrdinal("result"));
                    gameUser.Side = dr.GetInt32(dr.GetOrdinal("side"));
                }
                cn.Close();
            }
            return gameUser;
        }

        public bool AddGameUser(GameUser gameUser)
        {
            bool ok = false;

            string query = "INSERT INTO game_user(id_game, id_user, side, result)" +
                           "VALUES(?id_game, ?id_user, ?side, ?result)";

            using (MySqlConnection cn = new MySqlConnection(cs))
            {
                cn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("?id_game", gameUser.IdGame);
                    cmd.Parameters.AddWithValue("?id_user", gameUser.IdUser);
                    cmd.Parameters.AddWithValue("?side", gameUser.Side);
                    cmd.Parameters.AddWithValue("?result", gameUser.Result);
                    ok = Convert.ToBoolean(cmd.ExecuteNonQuery());
                }
                cn.Close();
            }
            return ok;
        }

        public bool UpdateGameUser(GameUser gameUser)
        {
            bool ok = false;

            string query = "UPDATE game_user SET id_game = ?id_game, id_user = ?id_user, side = ?side, result = ?result WHERE id_game_user = ?id_game_user";
            using (MySqlConnection cn = new MySqlConnection(cs))
            {
                cn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("?id_game_user", gameUser.Id);
                    cmd.Parameters.AddWithValue("?id_game", gameUser.IdGame);
                    cmd.Parameters.AddWithValue("?id_user", gameUser.Id);
                    cmd.Parameters.AddWithValue("?side", gameUser.Side);
                    cmd.Parameters.AddWithValue("?result", gameUser.Result);

                    ok = Convert.ToBoolean(cmd.ExecuteNonQuery());
                }
                cn.Close();
            }

            return ok;
        }

        public bool DeleteGameUser(int id)
        {
            bool ok = false;
            string query = "DELETE FROM game_user WHERE id_game_user = ?id";

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