using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EasyTicketLogic
{
    public class Offer
    {
        public int OfferId { get; private set; }
        [Required]
        public User Owner { get; set; }
        [Required]
        public IEnumerable<Ticket> Tickets { get; private set; }
        [Range(1, int.MaxValue)]
        [Required]
        public int Quantity { get; internal set; }
        public decimal TotalPrice { get; internal set; }
        public DateTime? DepartureDate { get; private set; }
        public DateTime? ArrivalDate { get; private set; }
        public Address DepartureAddress { get; private set; }
        public Address ArrivalAddress { get; set; }
        
        public string Description { get; internal set; }
        public LinkedList<OrderDetails> Orders { get; internal set; }
        public DateTime LastModificationDate { get; internal set; }

        internal Offer()
        {
            Tickets = new LinkedList<Ticket>();
            Orders = new LinkedList<OrderDetails>();
        }

        public Offer(int quantity, string desc = "") : this(null, quantity, desc) { }

        public Offer(IEnumerable<Ticket> tickets, int quantity, string desc = "") : this(null, tickets, quantity, desc) { }        
        
        public Offer(User owner, IEnumerable<Ticket> tickets, int quantity, string desc = "")
        {
            Owner = owner;
            Tickets = tickets != null ? new LinkedList<Ticket>(tickets) : new LinkedList<Ticket>();
            Quantity = quantity;
            Description = desc;
            Orders = new LinkedList<OrderDetails>();
            LastModificationDate = DateTime.Now;
            SetDateAddressEndPoints();
            //CalculateTotalPrice();
        }

        private void SetDateAddressEndPoints()
        {
            DepartureDate = Tickets.First().DepartureDate;
            DepartureAddress = Tickets.First().DepartureAddress;
            ArrivalDate = Tickets.First().ArrivalDate;
            ArrivalAddress = Tickets.First().ArrivalAddress;
        }

        private void CalculateTotalPrice()
        {
            foreach (Ticket ticket in Tickets)
                TotalPrice += ticket.Price;
        }

        public void AddTicket(Ticket ticket)
        {
            ((LinkedList<Ticket>)Tickets).AddLast(ticket);
            SetDateAddressEndPoints();
            //CalculateTotalPrice();
        }

        public override string ToString()
        {
            return string.Format("<p>Quantity {0}</p><p>Total Price {1}</p>", Quantity, TotalPrice)
                + string.Format("<p>Departure Date {0:MM/dd/yyyy}</p><p>Arrival Date {1:MM/dd/yyyy}</p>", DepartureDate, ArrivalDate)
                +string.Format("<p>Departure Address {0}</p><p>Arrival address {1}", DepartureAddress, ArrivalAddress);
        }
    }
}
