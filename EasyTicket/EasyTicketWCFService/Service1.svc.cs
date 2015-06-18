using EasyTicketLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace WcfService
{
    public class Service : IService
    {
        private static DateTime lastDate;
        private static int lastQuantity;
        private void SendMail(User currentUser, string body)
        {
            UserNotificationStrategyFactory strategyFactory = UserNotificationStrategyFactory.GetInstatnce();
            IUserNotificationStrategy userNotificationStrategy = strategyFactory.GetUserNotificationStrategy(currentUser);

            if (userNotificationStrategy != null)
                userNotificationStrategy.NotifyUser(currentUser, body);
        }

        public bool CheckNewOffer()
        {
            List<Offer> listOffer = EasyTicketContext.Instance.GetLastOffers(lastDate);
            if (listOffer.Count > 0)
            {
                List<User> list = EasyTicketContext.Instance.GetSignedUsers();

                string body = new MessageCreator(listOffer).GetMessage();

                foreach (var user in list)
                {
                    SendMail(user, body);
                }

                lastDate = DateTime.Now;

                return true;
            }
            else
                return false;

        }

        public void SendMailToAll(string body)
        {
            List<User> list = EasyTicketContext.Instance.GetSignedUsers();

            foreach (var user in list)
            {
                SendMail(user, body);
            }
        }


        public int RegisteredUsers()
        {
            return EasyTicketContext.Instance.Users.Count();
        }

        public void RegisterPreferences(User user, UserPreferences userPrefs)
        {
            EasyTicketContext.Instance.AddNewUserPrefs(user, userPrefs);
            
        }

        public async Task CheckOffers()
        {
            List<User> list = EasyTicketContext.Instance.GetSignedUsers();

            foreach (var user in list)
            {
                var prefs = new JourneyPreferences(user.Preferences);

                var currentJourney = new Journey(prefs);
                await currentJourney.SearchJourney();

                var listOffer = new List<Offer>();

                foreach (var offer in currentJourney.Offers)
                    if (offer.Tickets.Count() == 1)
                        listOffer.Add(offer);

                if (listOffer != null && listOffer.Count() != 0 && listOffer.Count() != lastQuantity)
                {
                    string body = new MessageCreator(listOffer).GetMessage();
                    SendMail(user, body);
                    lastQuantity = listOffer.Count();
                }
            }
        }
    }
}
