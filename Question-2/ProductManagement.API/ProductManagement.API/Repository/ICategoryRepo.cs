using ProductManagement.Library.Domain;

namespace ProductManagement.API.Repository
{
    public interface ICategoryRepo
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryAsync(int id);
        Task UpdateCategoryAsync(Category category);
        Task<Category> AddCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
    }
}
