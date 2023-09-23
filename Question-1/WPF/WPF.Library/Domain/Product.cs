using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WPF.Library.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public Category Category { get; set; }
        public Supplier Supplier { get; set; }
        public string CategoryName { get; set; }
        public string SupplierName { get; set; }
    }
}
