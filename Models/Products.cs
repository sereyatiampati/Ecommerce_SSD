using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Products
    {
        public string Id { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string ProductDescription { get; set; } = string.Empty;

        public string ProductPrice { get; set; } = string.Empty;

        // public string ProductRating { get; set; } = string.Empty;
        public string ProductCategory { get; set; } = string.Empty;
    }
}