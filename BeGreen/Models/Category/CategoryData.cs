using System;
using System.Collections.Generic;

namespace BeGreen.Models.Category
{
    public class CategoryData
    {
        public string success { get; set; }
        public string message { get; set; }
        public int categories { get; set; }
        public List<Category> data { get; set; }
    }
}
