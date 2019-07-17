using System;
using System.Collections.Generic;
using System.Text;

namespace BeGreen.Models
{
    public class AddressDetails
    {
        public int address_id { get; set; }
        public string gender { get; set; }
        public string company { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string street { get; set; }
        public string suburb { get; set; }
        public string postcode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int countries_id { get; set; }
        public string country_name { get; set; }
        public int zone_id { get; set; }
        public string zone_code { get; set; }
        public string zone_name { get; set; }
        public int default_address { get; set; }
    }
}
