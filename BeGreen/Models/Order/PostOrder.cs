using System.Collections.Generic;

namespace BeGreen.Models.Order
{
    public class PostOrder
    {
        public int customers_id;
        public string delivery_street_address;
        public string comments;
        public int is_coupon_applied;
        public double coupon_amount;
        public List<CouponsInfo> coupons;
        public string payment_method;
        public double productsTotal;
        public double totalPrice;
        public List<PostProducts> products;
    }
}
