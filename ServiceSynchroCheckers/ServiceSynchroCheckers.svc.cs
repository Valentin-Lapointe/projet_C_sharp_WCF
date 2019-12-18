using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ServiceSynchroCheckers.Classes;

namespace ServiceSynchroCheckers
{
     public class ServiceSynchroCheckers : IServiceSynchroCheckers
    {
        #region User
        public bool AddUser(User user)
        {
            bool ok = false;
            if(user != null)
            {
                ok = new User().AddUser(user);
            }
            return ok;
        }

        public bool DeleteUser(int id)
        {
            bool ok = false;
            if (id != 0)
            {
                ok = new User().DeleteUser(id);
            }
            return ok;
        }

        public User GetUserByIdUser(int id)
        {
            User user = new User().GetUserById(id);
            return user;
            //return new User().GetUserById(id);
        }

        public bool UpdateUser(User user)
        {
            bool ok = false;
            if (user != null)
            {
                ok = new User().UpdateUser(user);
            }
            return ok;
        }

        public List<User> GetUsersAvailable()
        {
            List<User> users = new User().GetUsersAvailable();
            return users;
        }

        // Vérifie si user existe ?
        public bool UpdateAvailabilityUser(int id, bool is_available)
        {
            bool ok = new User().UpdateAvailabilityUser(id, is_available);
            return ok;
        }

        #endregion

        #region Game

        public bool AddGame(Game game)
        {
            bool ok = false;
            if(game != null)
            {
                ok = new Game().AddGame(game);
            }
            return ok;
        }
        
        public bool DeleteGame(int id)
        {
            bool ok = false;
            if (id != 0)
            {
                ok = new Game().DeleteGame(id);
            }
            return ok;
        }

        public Game GetGameByIdGame(int id)
        {
            Game game = new Game().GetGameById(id);
            return game;
        }

        public bool UpdateGame(Game game)
        {
            bool ok = false;
            if (game != null)
            {
                ok = new Game().UpdateGame(game);
            }
            return ok;
        }

        #endregion

        #region Role

        public bool AddRole(Role role)
        {
            bool ok = false;
            if(role != null)
            {
                ok = new Role().AddRole(role);
            }
            return ok;
        }

        public bool DeleteRole(int id)
        {
            bool ok = false;
            if(id != 0)
            {
                ok = new Role().DeleteRole(id);
            }
            return ok;
        }

        public Role GetRoleByIdRole(int id)
        {
            Role role = new Role().GetRoleById(id);
            return role;
        }

        public bool UpdateRole(Role role)
        {
            bool ok = false;
            if(role != null)
            {
                ok = new Role().UpdateRole(role);
            }
            return ok;
        }

        #endregion

        #region GameUser

        public bool AddGameUser(GameUser gameUser)
        {
            bool ok = false;
            if(gameUser != null)
            {
                ok = new GameUser().AddGameUser(gameUser);
            }
            return ok;
        }

        public bool DeleteGameUser(int id)
        {
            bool ok = false;
            if (id != 0)
            {
                ok = new GameUser().DeleteGameUser(id);
            }
            return ok;
        }

        public GameUser GetGameUserByIdGameUser(int id)
        {
            GameUser gameUser = new GameUser().GetGameUserById(id);
            return gameUser;
        }

        public bool UpdateGameUser(GameUser gameUser)
        {
            bool ok = false;
            if (gameUser != null)
            {
                ok = new GameUser().UpdateGameUser(gameUser);
            }
            return ok;
        }

        #endregion

        #region Piece

        public bool AddPiece(Piece piece)
        {
            bool ok = false;
            if (piece != null)
            {
                ok = new Piece().AddPiece(piece);
            }
            return ok;
        }

        public bool DeletePiece(int id)
        {
            bool ok = false;
            if (id != 0)
            {
                ok = new Piece().DeletePiece(id);
            }
            return ok;
        }

        public Piece GetPieceByIdPiece(int id)
        {
            Piece piece = new Piece().GetPieceById(id);
            return piece;
        }

        public bool UpdatePiece(Piece piece)
        {
            bool ok = false;
            if (piece != null)
            {
                ok = new Piece().UpdatePiece(piece);
            }
            return ok;
        }

        #endregion

    }
}
