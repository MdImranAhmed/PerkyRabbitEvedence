using Microsoft.EntityFrameworkCore;
using ProductManagement.Library.Domain;

namespace ProductManagement.API.Data
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Clothing" },
                new Category { Id = 3, Name = "Grocery" },
                new Category { Id = 4, Name = "Health & Beauty" },
                new Category { Id = 5, Name = "Sports & Outdoors" }                
            };
           
            var suppliers = new List<Supplier>
            {
                new Supplier { Id = 1, Name = "Supplier A" },
                new Supplier { Id = 2, Name = "Supplier B" },
                new Supplier { Id = 3, Name = "Supplier C" },
                new Supplier { Id = 4, Name = "Supplier D" },
                new Supplier { Id = 5, Name = "Supplier E" }                
            };
            
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 19.99m, CategoryId = 1, SupplierId = 1 },
                new Product { Id = 2, Name = "Product 2", Price = 29.99m, CategoryId = 2, SupplierId = 2 },
                new Product { Id = 3, Name = "Product 3", Price = 30.99m, CategoryId = 3, SupplierId = 3 },
                new Product { Id = 4, Name = "Product 4", Price = 40.00m, CategoryId = 2, SupplierId = 4 },
                new Product { Id = 5, Name = "Product 5", Price = 45.50m, CategoryId = 3, SupplierId = 5 }
                
            };

            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Supplier>().HasData(suppliers);
            modelBuilder.Entity<Product>().HasData(products);
        }
    }
}

