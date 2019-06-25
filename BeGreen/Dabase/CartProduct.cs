using System;
using SQLite;

namespace BeGreen.Dabase
{
    public class CartProduct
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int customersId { get; set; }
        public int customersBasketId { get; set; }
        public string customersBasketDateAdded { get; set; }
    }
}
