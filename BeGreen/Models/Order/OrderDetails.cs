using System;
using System.Collections.Generic;
using System.Text;

namespace BeGreen.Models.Order
{
    public class OrderDetails
    {
        public int orders_id { get; set; }
        public string total_tax { get; set; }
        public int customers_id { get; set; }
        public string delivery_street_address { get; set; }
        public string shipping_method { get; set; }
        public string is_coupon_applied { get; set; }
        public string is_seen { get; set; }
        public string coupon_code { get; set; }
        public string coupon_amount { get; set; }
        public string tax_zone_id { get; set; }
        public double? shipping_cost { get; set; }
        public string comments { get; set; }
        public string nonce { get; set; }
        public string payment_method { get; set; }
        public double? productsTotal { get; set; }
        public double? totalPrice { get; set; }
        public string status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public List<OrderProducts> products { get; set; }
    }
}
