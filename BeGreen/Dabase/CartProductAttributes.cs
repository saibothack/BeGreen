using System;
using SQLite;

namespace BeGreen.Dabase
{
    public class CartProductAttributes
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int? CartProductID { get; set; }
        public int customersBasketId { get; set; }
        public string productsId { get; set; }
    }
}
