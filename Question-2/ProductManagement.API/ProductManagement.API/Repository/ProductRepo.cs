using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProductManagement.API.Data;
using ProductManagement.Library.Domain;
using ProductManagement.Library.Dtos;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.API.Repository
{
    public class ProductRepo : IProductRepo
    {
        private readonly ProductDbContext _context;

        public ProductRepo(ProductDbContext context)
        {
            _context = context;
        }
        public async Task<Product> CreateProductAsync(string name, decimal price, int categoryId, int supplierId)
        {
            var product = new Product
            {
                Name = name,
                Price = price,
                CategoryId = categoryId,
                SupplierId = supplierId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> UpdateProductAsync(int id, string name, decimal price, int categoryId, int supplierId)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return null; 
            }

            product.Name = name;
            product.Price = price;
            product.CategoryId = categoryId;
            product.SupplierId = supplierId;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var productDto = await _context.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    CategoryId = p.Category.Id,
                    SupplierId = p.SupplierId,
                    CategoryName = p.Category.Name,
                    SupplierName = p.Supplier.Name
                })
                .SingleOrDefaultAsync();

            return productDto;
        }



        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _context.Products
                .Include(p => p.Category)   
                .Include(p => p.Supplier)  
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    CategoryId = p.Category.Id,
                    SupplierId=p.SupplierId,
                    CategoryName = p.Category.Name,    
                    SupplierName = p.Supplier.Name  
                })
                .ToListAsync();

            return products;
        }


        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                throw new ArgumentException("Product not found");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

    }
}
