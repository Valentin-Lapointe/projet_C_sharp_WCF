using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServiceSynchroCheckers
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class ServiceSynchroCheckers : IServiceSynchroCheckers
    {
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
            User user = new User();
            user = new User().GetUserById(id);
            return user;
        }

        public bool UpdateUser(User user)
        {
            bool ok = false;
            if (user != null)
            {
                ok = new User().AddUser(user);
            }
            return ok;
        }
    }
}
