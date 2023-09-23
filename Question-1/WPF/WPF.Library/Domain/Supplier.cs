using System.Collections.Generic;

namespace WPF.Library.Domain
{
    public class Supplier
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}