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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.UserName)
                .IsUnique();

            builder.HasIndex(x => x.Email)
                .IsUnique();


            builder.HasOne<User>(u => u.CreatedBy)
                .WithMany(c => c.CreatedUserss)
                .HasForeignKey(f => f.CreatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasOne<User>(u => u.UpdatedBy)
                .WithMany(c => c.UpdatedUserss)
                .HasForeignKey(f => f.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

           /* var farmer = new User()
            {
                Id = 1,
                FullName = "SuperAdmin",

                Email = "avdunusinghe@gmail.com",
                Username = "avdunusinghe@gmail.com",
                MobileNo = "0703375581",
                Password = CustomPasswordHasher.GenerateHash("pass@123!"),
                IsActive = true,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            var admin = new User()
            {
                Id = 2,
                FullName = "Admin",
                Email = "admin@gmail.com",
                Username = "admin@gmail.com",
                MobileNo = "0112487086",
                Password = CustomPasswordHasher.GenerateHash("pass@123!"),
                IsActive = true,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            builder.HasData(superAdmin, admin);
           */
        }

        
    }
}
