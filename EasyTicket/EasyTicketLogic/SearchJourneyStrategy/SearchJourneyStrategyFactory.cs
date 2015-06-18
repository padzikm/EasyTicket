using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTicketLogic
{
    internal class SearchJourneyStrategyFactory
    {
        private static SearchJourneyStrategyFactory instace;

        public static SearchJourneyStrategyFactory GetInstatnce()
        {
            if(instace == null)            
                instace = new SearchJourneyStrategyFactory();
            return instace;
        }

        public ISearchJourneyStrategy GetSearchJourneyStrategy(JourneyPreferences journeyPreferences)
        {
            return new NoChangeJourneyStrategy();
        }
    }
}
