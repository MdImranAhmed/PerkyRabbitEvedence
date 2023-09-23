using ProductManagement.Library.Domain;
using ProductManagement.Library.Dtos;

namespace ProductManagement.API.Repository
{
    public interface IProductRepo
    {
        Task<Product> CreateProductAsync(string name, decimal price, int categoryId, int supplierId);
        Task<Product> UpdateProductAsync(int id, string name, decimal price, int categoryId, int supplierId);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<List<ProductDto>> GetAllProductsAsync();
        Task DeleteProductAsync(int id);

    }
}
