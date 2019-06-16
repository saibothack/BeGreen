using System.Collections.Generic;

namespace BeGreen.Models.Orchard
{
    public class OrchardData
    {
        public string success { get; set; }
        public string message { get; set; }
        public int total_record { get; set; }
        public List<Orchard> news_data { get; set; }
    }
}
