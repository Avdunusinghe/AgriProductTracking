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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(x => x.Id);

            builder.HasOne<ProductCategory>(p => p.ProductCategory)
                .WithMany(p => p.Products)
                .HasForeignKey(fk => fk.CategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);

            builder.HasOne<User>(u => u.CreatedBy)
                .WithMany(c => c.CreatedUsers)
                .HasForeignKey(fk => fk.CreatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);

            builder.HasOne<User>(u => u.UpdatedBy)
               .WithMany(c => c.UpdatedUsers)
               .HasForeignKey(fk => fk.UpdatedBy)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired(true);
        }
    }
}
