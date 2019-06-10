using System;
using System.Collections.Generic;

namespace BeGreen.Models.Product
{
    public class GetProducts
    {
        public int page_number { get; set; }
        public int language_id { get; set; }
        public string customers_id { get; set; }
        public string categories_id { get; set; }
        public string type { get; set; }
        public List<productsFilters> filters { get; set; }
        public productsPrice price { get; set; }
    }

    public class productsFilters {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class productsPrice
    {
        public int minPrice { get; set; }
        public int maxPrice { get; set; }
    }
}
