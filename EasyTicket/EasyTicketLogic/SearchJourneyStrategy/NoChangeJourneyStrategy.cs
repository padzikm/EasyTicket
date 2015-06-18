using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTicketLogic
{
    internal class NoChangeJourneyStrategy : ISearchJourneyStrategy
    {
        public async Task SearchJourney(Journey journey)
        {
            //JourneyPreferences pref = journey.Preferences;            
            //IEnumerable<Offer> offers = await EasyTicketContext.Instance.GetOffers(pref.DepartureAddress, pref.ArrivalAddress);                  

            //foreach (Offer offer in offers)
            //{                
            //    journey.Offers.AddLast(offer);                                
            //}            
            var result = await JourneySystemAdapterFactory.adapter.GetOffers(journey.Preferences);

            foreach(var tmp in result)
                if(tmp.TotalPrice <= journey.Preferences.MaxTotalPrice && tmp.Tickets.Count() == 1)
                    journey.Offers.AddLast(tmp);
        }
    }
}
