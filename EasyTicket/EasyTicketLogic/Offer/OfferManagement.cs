using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("EasyTicketLogicTest")]
namespace EasyTicketLogic
{
    
    internal class OfferManagement
    {        
        internal OfferManagement()
        {            
        }        

        internal async Task AddOffer(Offer offer, User user)
        {
            if (offer == null || user == null)
                throw new Exception("You have to log in");

            offer.Owner = user;

            await EasyTicketContext.Instance.AddOffer(offer);
        }
    }
}
