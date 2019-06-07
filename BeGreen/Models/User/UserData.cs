using System;
using System.Collections.Generic;

namespace BeGreen.Models.User
{
    public class UserData
    {
        public string success { get; set; }
        public string message { get; set; }
        public List<User> data { get; set; }
    }
}
