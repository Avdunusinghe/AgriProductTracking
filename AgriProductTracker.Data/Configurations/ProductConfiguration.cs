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

            builder.Property(p => p.Price)
                .HasPrecision(14, 2);

            builder.HasOne<ProductCategory>(p => p.ProductCategory)
                .WithMany(p => p.Products)
                .HasForeignKey(fk => fk.CategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);

            builder.HasOne<User>(u => u.CreatedBy)
                .WithMany(c => c.CreatedProducts)
                .HasForeignKey(fk => fk.CreatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);

            builder.HasOne<User>(u => u.UpdatedBy)
               .WithMany(c => c.UpdatedProducts)
               .HasForeignKey(fk => fk.UpdatedById)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired(true);

            builder.HasOne<OrderItem>(o => o.OrderItem)
               .WithOne(p=>p.Product)
               .HasForeignKey<OrderItem>(f => f.ProductId);

          

            /*modelBuilder.Entity<Part>()
        .Property(p => p.Size)
        .HasColumnType("decimal(18,4)");*/
        }
    }
}
