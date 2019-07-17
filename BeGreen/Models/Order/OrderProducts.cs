using System;
using System.Collections.Generic;
using System.Text;

namespace BeGreen.Models.Order
{
    public class OrderProducts
    {
        public int orders_products_id { get; set; }
        public int orders_id { get; set; }
        public int products_id { get; set; }
        public string products_model { get; set; }
        public string products_name { get; set; }
        public string products_price { get; set; }
        public string final_price { get; set; }
        public string products_tax { get; set; }
        public int products_quantity { get; set; }
        public string image { get; set; }
        public string categories_name { get; set; }
        public string language_id { get; set; }
        public List<PostProductsAttributes> attributes { get; set; }
    }
}
