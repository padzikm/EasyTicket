using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EasyTicketLogic
{
    public class UserNotificationStrategyFactory
    {
        private static UserNotificationStrategyFactory instace;

        public static UserNotificationStrategyFactory GetInstatnce()
        {
            if (instace == null)
                instace = new UserNotificationStrategyFactory();
            return instace;
        }

        public IUserNotificationStrategy GetUserNotificationStrategy(User user)
        {
            return new EmailNotification(new MailAddress(user.Email, user.Name + user.Lastname), user);
        }
    }
}
