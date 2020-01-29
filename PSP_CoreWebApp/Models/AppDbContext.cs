using Microsoft.EntityFrameworkCore; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP_CoreWebApp.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
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
