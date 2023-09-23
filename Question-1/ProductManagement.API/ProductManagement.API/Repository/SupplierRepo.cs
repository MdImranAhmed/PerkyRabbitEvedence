using Microsoft.EntityFrameworkCore;
using ProductManagement.API.Data;
using ProductManagement.Library.Domain;

namespace ProductManagement.API.Repository
{
    public class SupplierRepo:ISupplierRepo
    {
        private readonly ProductDbContext _context;

        public SupplierRepo(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Supplier>> GetSuppliersAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public async Task<Supplier> GetSupplierAsync(int id)
        {
            return await _context.Suppliers.FindAsync(id);
        }

        public async Task UpdateSupplierAsync(Supplier supplier)
        {
            _context.Entry(supplier).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Supplier> AddSupplierAsync(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }

        public async Task DeleteSupplierAsync(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
                await _context.SaveChangesAsync();
            }
        }
    }
}
