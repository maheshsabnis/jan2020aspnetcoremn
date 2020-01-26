using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebApp.Models
{
    /// <summary>
    /// Manage all Data Acccess
    /// </summary>
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// The DbContextOptions will be the injected DbContext class
        /// in DI Container of the application aka ConfigureServices()
        /// This class will read the connection-string from the DI Container
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Navigation with One to Many Relationship and Foreign Key
            modelBuilder.Entity<Product>()
             .HasOne(p => p.Category)
             .WithMany(b => b.Products)
             .HasForeignKey(p => p.CategoryRowId);
        }
    }
}
