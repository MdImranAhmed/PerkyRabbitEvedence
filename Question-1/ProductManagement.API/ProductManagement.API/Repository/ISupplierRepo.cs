using ProductManagement.Library.Domain;

namespace ProductManagement.API.Repository
{
    public interface ISupplierRepo
    {
        Task<IEnumerable<Supplier>> GetSuppliersAsync();
        Task<Supplier> GetSupplierAsync(int id);
        Task UpdateSupplierAsync(Supplier supplier);
        Task<Supplier> AddSupplierAsync(Supplier supplier);
        Task DeleteSupplierAsync(int id);
    }
}
