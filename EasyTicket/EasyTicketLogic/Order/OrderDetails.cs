using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTicketLogic
{
    public class OrderDetails
    {
        public int OrderDetailsId { get; set; }
        public DateTime OrderDate { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public bool IsAccepted { get; set; }
        public User User { get; set; }
        public Offer Offer { get; set; }
    }
}
