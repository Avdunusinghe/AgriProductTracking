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
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payment");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.MobileNumber)
                .IsUnique();

            builder.HasIndex(x => x.CardNumber)
                .IsUnique();

            builder.HasIndex(x => x.CVV)
                .IsUnique();

            builder.HasOne<User>(u => u.CreatedBy)
                .WithMany(u => u.CreatedPayments)
                .HasForeignKey(f => f.CreatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasOne<User>(u => u.UpdatedBy)
                .WithMany(u => u.UpdatedPayments)
                .HasForeignKey(f => f.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
}
