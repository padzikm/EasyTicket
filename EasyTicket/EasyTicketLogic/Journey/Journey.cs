using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTicketLogic
{
    public class Journey
    {
        public JourneyPreferences Preferences { get; private set; }
        public LinkedList<Offer> Offers { get; internal set; }

        public Journey(JourneyPreferences pref)
        {
            Preferences = pref;
            Offers = new LinkedList<Offer>();
        }

        public async Task SearchJourney()
        {
            SearchJourneyStrategyFactory strategyFactory = SearchJourneyStrategyFactory.GetInstatnce();
            ISearchJourneyStrategy searchJourneyStrategy = strategyFactory.GetSearchJourneyStrategy(Preferences);

            if(searchJourneyStrategy != null)
                await searchJourneyStrategy.SearchJourney(this);
        }        
    }
}
