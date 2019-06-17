using System;
namespace BeGreen.Models
{
    public class Coupon
    {
        public int coupans_id { get; set; }
        public int amount { get; set; }
        public int usage_count { get; set; }
        public int individual_use { get; set; }
        public int usage_limit { get; set; }
        public int usage_limit_per_user { get; set; }
        public int limit_usage_to_x_items { get; set; }
        public int free_shipping { get; set; }
        public int exclude_sale_items { get; set; }

        public string code { get; set; }
        public string date_created { get; set; }
        public string date_modified { get; set; }
        public string description { get; set; }
        public string discount_type { get; set; }
        public string expiry_date { get; set; }
        public string minimum_amount { get; set; }
        public string maximum_amount { get; set; }
        public string product_ids { get; set; }
        public string exclude_product_ids { get; set; }
        public string product_categories { get; set; }
        public string email_restrictions { get; set; }
        public string excluded_product_categories { get; set; }
        public string used_by { get; set; }
        public string photo { get; set; }


    }
}
