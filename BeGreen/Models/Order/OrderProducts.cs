using System;
using System.Collections.Generic;
using System.Text;

namespace BeGreen.Models.Order
{
    public class OrderProducts
    {
        public int products_id { get; set; }
        public string products_quantity { get; set; }
        public string products_model { get; set; }
        public string products_name { get; set; }
        public string products_image { get; set; }
        public string products_price { get; set; }
        public string products_date_added { get; set; }
        public string products_last_modified { get; set; }
        public string products_date_available { get; set; }
        public string products_weight { get; set; }
        public string products_weight_unit { get; set; }
        public string products_status { get; set; }
        public string products_tax_class_id { get; set; }
        public string manufacturers_id { get; set; }
        public string products_ordered { get; set; }
        public string products_liked { get; set; }
        public string low_limit { get; set; }
        public OrderProducPivot pivot { get; set; }
        public ProducDetail details { get; set; }
    }
}
