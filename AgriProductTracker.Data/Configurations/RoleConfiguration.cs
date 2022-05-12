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
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");

            builder.HasKey(x => x.Id);

            var admin = new Role()
            {
                Id = 1,
                IsActive = true,
                Name = "Admin"
            };

            var farmer = new Role()
            {
                Id = 2,
                IsActive = true,
                Name = "Farmer"
            };

            var buyer = new Role()
            {
                Id = 3,
                IsActive = true,
                Name = "Buyer"
            };

            

            builder.HasData(admin, farmer, buyer);
        }

        
    }
}
