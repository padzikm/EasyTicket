using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EasyTicketLogic
{
    public class Ticket
    {
        public int TicketId { get; private set; }
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        [Required]
        public decimal Price { get; internal set; }
        [Required]
        public DateTime? DepartureDate { get; internal set; }
        public DateTime? ArrivalDate { get; internal set; }
        [Required]
        public Address DepartureAddress { get; internal set; }
        [Required]
        public Address ArrivalAddress { get; internal set; }

        public Ticket() { }

        public Ticket(decimal price, Address depAddress, DateTime depDate, Address arrAddress) : this(price, depAddress, depDate, arrAddress, null) { }

        public Ticket(decimal price, Address depAddress, DateTime? depDate, Address arrAddress, DateTime? arrDate)
        {
            Price = price;
            DepartureAddress = depAddress;
            DepartureDate = depDate;
            ArrivalAddress = arrAddress;
            ArrivalDate = arrDate;
        }
    }
}
