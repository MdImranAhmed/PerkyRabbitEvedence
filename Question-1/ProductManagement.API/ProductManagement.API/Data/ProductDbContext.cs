using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using ProductManagement.Library.Domain;
using System.Reflection;

namespace ProductManagement.API.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);
            modelBuilder.Entity<Product>().HasOne(p => p.Supplier).WithMany(s => s.Products).HasForeignKey(p => p.SupplierId);
            SeedData.Seed(modelBuilder);
        }
      
    }
    
}
