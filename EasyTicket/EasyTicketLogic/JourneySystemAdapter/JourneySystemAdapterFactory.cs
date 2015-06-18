using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTicketLogic
{
    public static class JourneySystemAdapterFactory
    {
        public static IJourneySystemAdapter adapter = new SkyScannerAdapter();
    }
}
