using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTicketLogic
{
    public interface IJourneySystemAdapter
    {
        Task<IEnumerable<Offer>> GetOffers(JourneyPreferences pref);
        void MakeReservations();
    }
}
