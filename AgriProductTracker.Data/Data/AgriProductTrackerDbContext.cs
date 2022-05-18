using AgriProductTracker.Data.Configurations;
using AgriProductTracker.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Data.Data
{
    public class AgriProductTrackerDbContext : DbContext
    {
        public AgriProductTrackerDbContext()
        {

        }

        public AgriProductTrackerDbContext(DbContextOptions<AgriProductTrackerDbContext> options) : base(options)
        {
           

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseSqlServer("Server=DESKTOP-PSI7D8C\\SQLEXPRESS;Database=APT;User Id=sa;Password=1qaz2wsx@;");
           // optionsBuilder.UseSqlServer(@"Server=DESKTOP-L5IG0M5\SQLEXPRESS;Database=APT;User Id=sa;Password=1qaz2wsx@;");
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-9KTAG16\SQLEXPRESS;Database=APT;User Id=sa;Password=1qaz2wsx@;");

            //optionsBuilder.UseSqlServer("Server=DESKTOP-PSI7D8C\\SQLEXPRESS;Database=APT;User Id=sa;Password=1qaz2wsx@;");
           // optionsBuilder.UseSqlServer(@"Server=DESKTOP-L5IG0M5\SQLEXPRESS;Database=APT;User Id=sa;Password=1qaz2wsx@;"); 326341c29a5a93e7ddb9dfa59505347efe47f0cd
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfigurations());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new DeliveryServiceConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new ProductImageConfiguration());


        }

        #region Database Entities
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<DeliveryService> DeliveryServices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        #endregion
    }
}
