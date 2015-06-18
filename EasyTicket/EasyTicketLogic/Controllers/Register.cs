using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyTicketLogic
{
    public class Register
    {
        private Journey currentJourney;
        private JourneyPreferences currentJourneyPreferences;        
        private UserManagement userManagement;

        public User CurrentUser { get; private set; }

        public Register()
        {            
            userManagement = new UserManagement();            
        }

        async public Task<bool> RegisterUserAsync(User user)
        {
            return await userManagement.RegisterUserAsync(user);
        }

        async public Task<bool> LoginUserAsync(string login, string password)
        {
            return await userManagement.LoginUserAsync(login, password);
        }

        public User GetCurrentUser()
        {
            return userManagement.CurrentUser;
        }

        public JourneyPreferences GetNewJourneyPreferences()
        {
            return new JourneyPreferences();
        }

        public async Task<Journey> SearchJourney(JourneyPreferences journeyPref)
        {
            currentJourney = new Journey(journeyPref);            
            await currentJourney.SearchJourney();
            return currentJourney;
        }

        public async Task AddOffer(Offer offer)
        {
            //offerManagement.AddOffer(offer, userManagement.CurrentUser);            
            //await offerManagement.AddOffer(offer, userManagement.CurrentUser);            
        }

        public async Task<Dictionary<string, string>> GetAllAvailableDestinations()
        {
            SkyScannerAdapter skyScannerAdapter = new SkyScannerAdapter();

            return await skyScannerAdapter.GetAllAvailableDestinations();            
        }
    }
}
