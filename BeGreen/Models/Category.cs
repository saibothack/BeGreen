using System;
using System.Collections.Generic;

namespace BeGreen.Models
{
    public class AllCategory
    {
        public string success { get; set; }
        public string message { get; set; }
        public int categories { get; set; }
        public List<Category> data { get; set; }
    }

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
