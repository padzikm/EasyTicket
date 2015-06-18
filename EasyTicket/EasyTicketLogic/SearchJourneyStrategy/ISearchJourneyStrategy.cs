using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTicketLogic
{
    public interface ISearchJourneyStrategy
    {
        Task SearchJourney(Journey journey);
    }
}
