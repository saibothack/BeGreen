using System;
using System.Threading.Tasks;
using BeGreen.Models.User;
using BeGreen.Helpers;
using Xamarin.Forms;
using BeGreen.Views;

namespace BeGreen.Services.Logic
{
    public class ServicesUser
    {
        public async Task LoginAsync(string sUser, string sPassword)
        {
            User user = new User
            {
                customers_email_address = sUser,
                customers_password = sPassword
            };

            var currentUser = await App.oServiceManager.LoginAsync(user);

            if (!currentUser.success.Equals("1"))
            {
                await Application.Current.MainPage.DisplayAlert("Notificación", currentUser.message, "Aceptar");
            }
            else
            {
                Settings.isLogin = true;
                Settings.UserName = currentUser.data[0].customers_firstname;
                Settings.UserEmail = currentUser.data[0].customers_email_address;

                Application.Current.MainPage = new MasterDetailPage()
                {
                    Master = new MasterPage() { Title = "Menú" },
                    Detail = new NavigationPage(new HomePage())
                };
            }
        }

        public async Task RegisterAsync(string sName, string sAddress, string sEmail, string sPassword)
        {
            User user = new User
            {
                customers_firstname = sName,
                customers_email_address = sEmail,
                customers_password = sPassword
            };

            var currentUser = await App.oServiceManager.RegisterAsync(user);

            if (!currentUser.success.Equals("1"))
            {
                await Application.Current.MainPage.DisplayAlert("Notificación", currentUser.message, "Aceptar");
            }
            else
            {
                Settings.isLogin = true;
                Settings.UserName = currentUser.data[0].customers_firstname;
                Settings.UserEmail = currentUser.data[0].customers_email_address;
                Settings.UserAddress = sAddress;

                Application.Current.MainPage = new MasterDetailPage()
                {
                    Master = new MasterPage() { Title = "Menú" },
                    Detail = new NavigationPage(new HomePage())
                };
            }
        }
    }
}
