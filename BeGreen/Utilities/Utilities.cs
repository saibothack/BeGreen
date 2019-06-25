using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeGreen.Utilities
{
    public static class Util
    {
        public static async Task ShowMessage(string title,
            string message,
            string buttonText,
            Action afterHideCallback)
        {
            await Application.Current.MainPage.DisplayAlert(
                title,
                message,
                buttonText);

            afterHideCallback?.Invoke();
        }

        public static bool IsEmail(string email)
        {
            return Regex.IsMatch(email, @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$");

        }

        public static string checkDiscount(string actualPrice, string discountedPrice)
        {

            if (discountedPrice == null)
            {
                discountedPrice = actualPrice;
            }

            double oldPrice = Double.Parse(actualPrice);
            double newPrice = Double.Parse(discountedPrice);

            double discount = (oldPrice - newPrice) / oldPrice * 100;

            return (discount > 0) ? Math.Round(discount) + "% " + "OFF" : null;
        }
    }
}
