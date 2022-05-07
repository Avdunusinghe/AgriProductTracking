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
    public class DeliveryServiceConfiguration : IEntityTypeConfiguration<DeliveryService>
    {
        public void Configure(EntityTypeBuilder<DeliveryService> builder)
        {
            builder.ToTable("DeliveryService");

            builder.HasKey(x => x.Id);

            builder.HasOne<User>(cu=>cu.CreatedBy)
                .WithMany(c=>c.CreatedDeliveryServices)
                .HasForeignKey(fk=>fk.CreatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);

            builder.HasOne<User>(uc => uc.UpdatedBy)
                .WithMany(c => c.UpdatedDeliveryServices)
                .HasForeignKey(fk => fk.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);

        }
    }
}
