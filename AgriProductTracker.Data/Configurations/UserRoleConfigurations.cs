
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
    public  class UserRoleConfigurations : IEntityTypeConfiguration<UserRole>
    {
            public void Configure(EntityTypeBuilder<UserRole> builder)
            {
                builder.ToTable("UserRole");

                builder.HasKey(x => new { x.UserId, x.RoleId });

                builder.HasOne<User>(u => u.User)
                    .WithMany(x => x.UserRoles)
                    .HasForeignKey(f => f.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                builder.HasOne<Role>(r => r.Role)
                    .WithMany(x => x.UserRoles)
                    .HasForeignKey(f => f.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);

                builder.HasOne<User>(u => u.CreatedBy)
                   .WithMany(c => c.CreatedUserRoles)
                   .HasForeignKey(f => f.CreatedById)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

                builder.HasOne<User>(u => u.UpdatedBy)
                    .WithMany(c => c.UpdatedUserRoles)
                    .HasForeignKey(f => f.UpdatedById)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);

                var farmerRole = new UserRole()
                {
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow,
                    IsActive = true,
                    RoleId = 1,
                    UserId = 1,
                    UpdatedById = 1,
                    CreatedById = 1,
                };
                var buyerRole = new UserRole()
                {
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow,
                    IsActive = true,
                    RoleId = 2,
                    UserId = 2,
                    UpdatedById = 1,
                    CreatedById = 1,
                };

                builder.HasData(farmerRole, buyerRole);
            }

      
        
    }
}
