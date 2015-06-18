using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace EasyTicketLogic
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(d => d.Name).IsRequired().HasMaxLength(30);
            Property(d => d.Lastname).IsRequired().HasMaxLength(40);
            Property(d => d.Login).IsRequired().HasMaxLength(15);
            Property(d => d.Password).IsRequired();
            Property(d => d.Email).IsRequired();
            Property(d => d.Avatar).HasColumnType("image");

            //HasRequired(ur => ur.Role).WithRequiredDependent(u => u.User);
            //HasRequired(up => up.Preferencs).WithRequiredDependent(u => u.User);



            
        }
    }
}
