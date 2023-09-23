using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Library.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public string CategoryName { get; set; }
        public string SupplierName { get; set; }
    }

    public class ViewModel
    {
        public ObservableCollection<ProductDto> Products { get; set; }
    }
}

