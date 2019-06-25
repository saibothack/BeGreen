using System;
using System.Collections.Generic;

namespace BeGreen.Models.Cart
{
    public class CartProductAttributes
    {
        public int customersBasketId { get; set; }
        public string productsId { get; set; }
        public Option option { get; set; }
        public List<Value> values { get; set; }
    }
}
