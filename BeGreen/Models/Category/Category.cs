using System;
namespace BeGreen.Models.Category
{
    public class Category
    {
        public string id { get; set; }
        public string image { get; set; }
        public string icon { get; set; }
        public string order { get; set; }
        public string parent_id { get; set; }
        public string name { get; set; }
        public string total_products { get; set; }
    }
}
