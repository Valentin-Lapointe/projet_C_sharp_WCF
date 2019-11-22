using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServiceSynchroCheckers
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IServiceSynchroCheckers
    {
        [OperationContract]
        User GetUserByIdUser(int idUser);

        [OperationContract]
        bool AddUser(User user);

        [OperationContract]
        bool UpdateUser(User user);

        [OperationContract]
        bool DeleteUser(int id);

        // TODO: ajoutez vos opérations de service ici
        // Mettre tout les methode que l'on souhaite faire passer
    }

}
