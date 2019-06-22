using System.Collections.Generic;

namespace BeGreen.Models.Terms
{
    public class TermsData
    {
        public string success { get; set; }
        public string message { get; set; }
        public List<Terms> pages_data { get; set; }
    }
}
