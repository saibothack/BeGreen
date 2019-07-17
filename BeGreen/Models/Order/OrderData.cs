using System;
using System.Collections.Generic;
using System.Text;

namespace BeGreen.Models.Order
{
    public class OrderData
    {
        public string success { get; set; }
        public string message { get; set; }
        public List<OrderDetails> data { get; set; }
    }
}
