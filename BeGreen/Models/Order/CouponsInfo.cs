using System;
using System.Collections.Generic;
using System.Text;

namespace BeGreen.Models.Order
{
    public class CouponsInfo
    {
        private string coupans_id;
        private string code;
        private string amount;
        private string discount;
        private string discount_type;
        private string date_created;
        private string date_modified;
        private string description;
        private string expiry_date;
        private string usage_count;
        private string individual_use;
        private List<string> product_ids;
        private List<string> exclude_product_ids;
        private string usage_limit;
        private string usage_limit_per_user;
        private string limit_usage_to_x_items;
        private string free_shipping;
        private List<string> product_categories;
        private List<string> excluded_product_categories;
        private string exclude_sale_items;
        private string minimum_amount;
        private string maximum_amount;
        private List<string> email_restrictions;
        private List<string> used_by;
    }
}
