using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EasyTicketLogic
{
    public class EmailNotification : IUserNotificationStrategy
    {
        public EmailNotification(MailAddress ToAddress, User user)
        {
            FromAddress = new MailAddress("easyticket.company@gmail.com", "Easy Ticket");
            ToAddress = new MailAddress(user.Email, user.Name + user.Lastname);
            User = user;
        }
        public MailAddress FromAddress { get; set; }
        public MailAddress ToAddress { get; set; }
        public string Password { get; set; }
        public User User { get; set; }

        public void NotifyUser(User user, string body)
        {
            const string fromPassword = "easyticket1314";
            const string subject = "Easy Ticket";

            ToAddress = new MailAddress(user.Email, user.Name + user.Lastname);
            var smtp = new SmtpClient
                       {
                           Host = "smtp.gmail.com",
                           Port = 587,
                           EnableSsl = true,
                           DeliveryMethod = SmtpDeliveryMethod.Network,
                           UseDefaultCredentials = false,
                           Credentials = new NetworkCredential(FromAddress.Address, fromPassword)
                       };
            using (var message = new MailMessage(FromAddress, ToAddress)
                                 {
                                     Subject = subject,
                                     Body = body,
                                     IsBodyHtml = true
                                     
                                 })
            {
                smtp.Send(message);
            }
        }
    }
}
