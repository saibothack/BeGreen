using System;
using System.Collections.Generic;

namespace BeGreen.Models.Product
{
    public class ProductData
    {
        public string success { get; set; }
        public string message { get; set; }
        public int total_record { get; set; }
        public List<Product> product_data { get; set; }
    }
}
