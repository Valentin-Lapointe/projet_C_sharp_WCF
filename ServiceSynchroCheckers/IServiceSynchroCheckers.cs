using ServiceSynchroCheckers.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServiceSynchroCheckers
{
   [ServiceContract]
    public interface IServiceSynchroCheckers
    {
        #region User

        [OperationContract]
        List<User> GetUsers();

        [OperationContract]
        User GetUserByIdUser(int id);

        [OperationContract]
        bool AddUser(User user);

        [OperationContract]
        bool UpdateUser(User user);

        [OperationContract]
        bool DeleteUser(int id);

        [OperationContract]
        List<User> GetUsersAvailable();

        [OperationContract]
        bool UpdateAvailabilityUser(int id, bool is_available);
        
        #endregion

        #region Game

        [OperationContract]
        Game GetGameByIdGame(int id);

        [OperationContract]
        bool AddGame(Game game);

        [OperationContract]
        bool UpdateGame(Game game);

        [OperationContract]
        bool DeleteGame(int id);

        #endregion

        #region Role

        [OperationContract]
        Role GetRoleByIdRole(int id);

        [OperationContract]
        bool AddRole(Role role);

        [OperationContract]
        bool UpdateRole(Role role);

        [OperationContract]
        bool DeleteRole(int id);

        #endregion

        #region GameUser

        [OperationContract]
        GameUser GetGameUserByIdGameUser(int id);

        [OperationContract]
        bool AddGameUser(GameUser gameUser);

        [OperationContract]
        bool UpdateGameUser(GameUser gameUser);

        [OperationContract]
        bool DeleteGameUser(int id);

        #endregion

        #region Piece

        [OperationContract]
        Piece GetPieceByIdPiece(int id);

        [OperationContract]
        bool AddPiece(Piece piece);

        [OperationContract]
        bool UpdatePiece(Piece piece);

        [OperationContract]
        bool DeletePiece(int id);

        #endregion

    }

}
