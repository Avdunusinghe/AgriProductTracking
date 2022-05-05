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
            optionsBuilder.UseSqlServer("Server=DESKTOP-PSI7D8C\\SQLEXPRESS;Database=APT;User Id=sa;Password=1qaz2wsx@;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        #region Database Entities
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; } 
        #endregion
    }
}
