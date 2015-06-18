using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using EasyTicketLogic;
using System.Threading.Tasks;

namespace WcfService
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        bool CheckNewOffer();
        [OperationContract]
        void SendMailToAll(string body);
        [OperationContract]
        int RegisteredUsers();
        [OperationContract]
        void RegisterPreferences(User user, UserPreferences userPrefs);
        [OperationContract]
        Task CheckOffers();
    }

}
