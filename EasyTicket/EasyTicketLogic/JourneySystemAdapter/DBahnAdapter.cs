using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTicketLogic
{
    public class DBahnAdapter : IJourneySystemAdapter
    {
        public async Task<IEnumerable<Offer>> GetOffers(JourneyPreferences pref)
        {
            throw new NotImplementedException();
        }

        public void MakeReservations()
        {
            throw new NotImplementedException();
        }        
    }
}
