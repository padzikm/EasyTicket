using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTicketLogic
{
    public interface IUserNotificationStrategy
    {
        void NotifyUser(User user, string body);
    }
}
