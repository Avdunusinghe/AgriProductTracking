using AgriProductTracker.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(x => x.Id);

            builder.Property(t => t.TotalPrice)
               .HasPrecision(14, 2);

            builder.HasOne<User>(u=>u.Customer)
                .WithMany(u=>u.PlaceOders)
                .HasForeignKey(fk=>fk.CustomerId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);
        }
    }
}
