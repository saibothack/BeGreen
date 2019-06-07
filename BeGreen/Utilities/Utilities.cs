using System;
using System.Text.RegularExpressions;

namespace BeGreen.Utilities
{
    public static class Util
    {
        public static bool IsEmail(string email)
        {
            return Regex.IsMatch(email, @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$");

        }
    }
}
