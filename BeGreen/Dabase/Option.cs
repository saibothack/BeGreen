using SQLite;

namespace BeGreen.Dabase
{
    public class Option
    {
        [PrimaryKey, AutoIncrement]
        public int IDOption { get; set; }
        public int? CartProductAttributesID { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }
}
