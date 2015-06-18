using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTicketLogic
{
    public class UserRole
    {
        public int UserRoleId { get; private set; }
        public string Name { get; private set; }
        public bool CanSearchOffers { get; private set; }
        public bool CanAddOffers { get; private set; }
        public bool CanBuyTickets { get; private set; }
        public bool IsBanned { get; private set; }

        public UserRole()
        {
            CanSearchOffers = true;
            CanAddOffers = true;
            IsBanned = false;
        }
    }
}
