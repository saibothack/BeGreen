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
        public string customers_name { get; set; }
        public string customers_company { get; set; }
        public string customers_street_address { get; set; }
        public string customers_suburb { get; set; }
        public string customers_city { get; set; }
        public string customers_postcode { get; set; }
        public string customers_state { get; set; }
        public string customers_country { get; set; }
        public string customers_telephone { get; set; }
        public string customers_email_address { get; set; }
        public int customers_address_format_id { get; set; }
        public string delivery_name { get; set; }
        public string delivery_company { get; set; }
        public string delivery_street_address { get; set; }
        public string delivery_suburb { get; set; }
        public string delivery_city { get; set; }
        public string delivery_postcode { get; set; }
        public string delivery_state { get; set; }
        public string delivery_country { get; set; }
        public int delivery_address_format_id { get; set; }
        public string billing_name { get; set; }
        public string billing_company { get; set; }
        public string billing_street_address { get; set; }
        public string billing_suburb { get; set; }
        public string billing_city { get; set; }
        public string billing_postcode { get; set; }
        public string billing_state { get; set; }
        public string billing_country { get; set; }
        public int billing_address_format_id { get; set; }
        public string payment_method { get; set; }
        public string cc_type { get; set; }
        public string cc_owner { get; set; }
        public string cc_number { get; set; }
        public string cc_expires { get; set; }
        public string last_modified { get; set; }
        public string date_purchased { get; set; }
        public string orders_date_finished { get; set; }
        public string currency { get; set; }
        public string currency_value { get; set; }
        public string order_price { get; set; }
        public string shipping_cost { get; set; }
        public string shipping_method { get; set; }
        public string shipping_duration { get; set; }
        public string order_information { get; set; }
        public string is_seen { get; set; }
        public string coupon_code { get; set; }
        public string coupon_amount { get; set; }
        public string exclude_product_ids { get; set; }
        public string product_categories { get; set; }
        public string excluded_product_categories { get; set; }
        public string free_shipping { get; set; }
        public string product_ids { get; set; }
        public string orders_status_id { get; set; }
        public string orders_status { get; set; }
        public string customer_comments { get; set; }
        public string admin_comments { get; set; }
        public List<CouponsInfo> coupons { get; set; }
        public List<OrderProducts> data { get; set; }
    }
}
