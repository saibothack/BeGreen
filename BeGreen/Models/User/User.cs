using System;
using System.Collections.Generic;

namespace BeGreen.Models.User
{
    public class User
    {
        public string customers_id { get; set; }
        public string customers_firstname { get; set; }
        public string customers_lastname { get; set; }
        public string customers_dob { get; set; }
        public string customers_gender { get; set; }
        public string customers_picture { get; set; }
        public string customers_email_address { get; set; }
        public string customers_password { get; set; }
        public string customers_telephone { get; set; }
        public string customers_fax { get; set; }
        public string customers_newsletter { get; set; }
        public string fb_id { get; set; }
        public string google_id { get; set; }
        public int isActive { get; set; }
        public string customers_default_address_id { get; set; }
        public List<UserLikedProducts> liked_products { get; set; }
    }
}
