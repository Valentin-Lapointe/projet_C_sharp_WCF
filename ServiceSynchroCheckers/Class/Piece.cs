using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServiceSynchroCheckers.Class
{
    [DataContract]
    public class Piece
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

        private string _position;

        [DataMember]
        public string Position
        {
            get { return _position; }
            set { _position = value; }
        }


        private bool _isEliminate;

        [DataMember]
        public bool IsEliminate
        {
            get { return _isEliminate; }
            set { _isEliminate = value; }
        }

        private bool _isSuper;

        [DataMember]
        public bool IsSuper
        {
            get { return _isSuper; }
            set { _isSuper = value; }
        }

        private DateTime? _updateAt;

        [DataMember]
        public DateTime? UpdateAt
        {
            get { return _updateAt; }
            set { _updateAt = value; }
        }

        private int _idGameUser;

        [DataMember]
        public int IdGameUser
        {
            get { return _idGameUser; }
            set { _idGameUser = value; }
        }

        #endregion

        #region Méthodes SQL
        public Piece GetPieceById(int id)
        {
            Piece piece = new Piece();
            string query = "SELECT * FROM piece WHERE id_piece = ?id";
            using (MySqlConnection cn = new MySqlConnection(cs))
            {
                cn.Open();
                MySqlDataReader dr = null;
                using (MySqlCommand cmd = new MySqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("?id", id);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    piece.Id = dr.GetInt32(dr.GetOrdinal("id_piece"));
                    piece.IdGameUser = dr.GetInt32(dr.GetOrdinal("id_game_user"));
                    piece.IsEliminate = dr.GetBoolean(dr.GetOrdinal("is_eliminate"));
                    piece.IsSuper = dr.GetBoolean(dr.GetOrdinal("is_super"));
                    piece.UpdateAt = dr.IsDBNull(dr.GetOrdinal("updated_at")) ? null : (DateTime?)dr.GetDateTime(dr.GetOrdinal("updated_at"));
                    piece.Position = dr.GetString(dr.GetOrdinal("position"));
                }
                cn.Close();
            }
            return piece;
        }

        public bool AddPiece(Piece piece)
        {
            bool ok = false;

            string query = "INSERT INTO piece(id_game_user, is_eliminate, is_super, updated_at, position)" +
                           "VALUES(?id_game_user, ?is_eliminate, ?is_super, ?updated_at, ?position)";

            using (MySqlConnection cn = new MySqlConnection(cs))
            {
                cn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("?id_game_user", piece.IdGameUser);
                    cmd.Parameters.AddWithValue("?is_eliminate", piece.IsEliminate);
                    cmd.Parameters.AddWithValue("?is_super", piece.IsSuper);
                    cmd.Parameters.AddWithValue("?updated_at", piece.UpdateAt);
                    cmd.Parameters.AddWithValue("?position", piece.Position);
                    ok = Convert.ToBoolean(cmd.ExecuteNonQuery());
                }
                cn.Close();
            }
            return ok;
        }

        public bool UpdatePiece(Piece piece)
        {
            bool ok = false;

            string query = "UPDATE piece SET position = ?position, id_game_user = ?id_game_user, is_eliminate = ?is_eliminate, is_super = ?is_super, updated_at = ?updated_at WHERE id_piece = ?id_piece";
            using (MySqlConnection cn = new MySqlConnection(cs))
            {
                cn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("?id_piece", piece.Id);
                    cmd.Parameters.AddWithValue("?position", piece.Position);
                    cmd.Parameters.AddWithValue("?id_game_user", piece.IdGameUser);
                    cmd.Parameters.AddWithValue("?is_eliminate", piece.IsEliminate);
                    cmd.Parameters.AddWithValue("?is_super", piece.IsSuper);
                    cmd.Parameters.AddWithValue("?updated_at", piece.UpdateAt);

                    ok = Convert.ToBoolean(cmd.ExecuteNonQuery());
                }
                cn.Close();
            }
            return ok;
        }

        public bool DeletePiece(int id)
        {
            bool ok = false;
            string query = "DELETE FROM piece WHERE id_piece = ?id";

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