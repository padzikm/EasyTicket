using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace EasyTicketLogic
{
    public class TicketConfiguration : EntityTypeConfiguration<Ticket>
    {
        public TicketConfiguration()
        {
            HasRequired(p => p.ArrivalAddress).WithMany().WillCascadeOnDelete(false);
            Property(p => p.DepartureDate).HasColumnType("datetime2").HasPrecision(0);
            Property(p => p.ArrivalDate).HasColumnType("datetime2").HasPrecision(0);
        }
    }
}
