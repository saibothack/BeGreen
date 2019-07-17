using BeGreen.Models.Product;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeGreen.Dabase
{
    public class ProductFavorite
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int? CartProductID { get; set; }
        public int products_id { get; set; }
        public int products_quantity { get; set; }
        public string products_image { get; set; }
        public string products_model { get; set; }
        public string products_price { get; set; }
        public string discount_price { get; set; }
        public string products_date_added { get; set; }
        public string products_last_modified { get; set; }
        public string products_date_available { get; set; }
        public string products_weight { get; set; }
        public string products_weight_unit { get; set; }
        public int products_status { get; set; }
        public int products_ordered { get; set; }
        public int products_liked { get; set; }
        public int language_id { get; set; }
        public string products_name { get; set; }
        public string products_description { get; set; }
        public string products_url { get; set; }
        public int products_viewed { get; set; }
        public int products_tax_class_id { get; set; }
        public int tax_rates_id { get; set; }
        public int tax_zone_id { get; set; }
        public int tax_class_id { get; set; }
        public int tax_priority { get; set; }
        public string tax_rate { get; set; }
        public string tax_description { get; set; }
        public string tax_class_title { get; set; }
        public string tax_class_description { get; set; }
        public string categories_id { get; set; }
        public string categories_name { get; set; }
        public string categories_image { get; set; }
        public string categories_icon { get; set; }
        public int parent_id { get; set; }
        public int sort_order { get; set; }
        public string isLiked { get; set; }
        public int? manufacturers_id { get; set; }
        public string manufacturers_name { get; set; }
        public string manufacturers_image { get; set; }
        public string manufacturers_url { get; set; }
        public string date_added { get; set; }
        public string last_modified { get; set; }
        public string isSale_product { get; set; }
        public string attributes_price { get; set; }
        public string final_price { get; set; }
        public string total_price { get; set; }
        public int customers_basket_quantity { get; set; }
        public string comentProduct { get; set; }
    }
}
