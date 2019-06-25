using System;
using SQLite;

namespace BeGreen.Dabase
{
    public class Value
    {
        [PrimaryKey, AutoIncrement]
        public int IDValue { get; set; }
        public int? CartProductAttributesID { get; set; }
        public int id { get; set; }
        public string value { get; set; }
        public string price { get; set; }
        public string price_prefix { get; set; }
    }
}
