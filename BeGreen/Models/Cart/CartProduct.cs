using System.Collections.Generic;

namespace BeGreen.Models.Cart
{
    public class CartProduct
    {
        public int ID { get; set; }
        public int customersId { get; set; }
        public int customersBasketId { get; set; }
        public string customersBasketDateAdded { get; set; }
        public Product.Product customersBasketProduct { get; set; }
        public List<CartProductAttributes> customersBasketProductAttributes { get; set; }
        public bool status { get; set; }
    }
}
