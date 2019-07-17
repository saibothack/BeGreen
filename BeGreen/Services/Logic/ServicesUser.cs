using System;
using System.Threading.Tasks;
using BeGreen.Models.User;
using BeGreen.Helpers;
using Xamarin.Forms;
using BeGreen.Views;
using BeGreen.Dabase;

namespace BeGreen.Services.Logic
{
    public class ServicesUser
    {
        public async Task LoginAsync(string sUser, string sPassword, bool isNeedPop)
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
                dbUser dbUser = new dbUser
                {
                    customers_id = currentUser.data[0].customers_id,
                    customers_firstname = currentUser.data[0].customers_firstname,
                    customers_lastname = currentUser.data[0].customers_lastname,
                    customers_dob = currentUser.data[0].customers_dob,
                    customers_gender = currentUser.data[0].customers_gender,
                    customers_picture = currentUser.data[0].customers_picture,
                    customers_email_address = currentUser.data[0].customers_email_address,
                    customers_password = currentUser.data[0].customers_password,
                    customers_telephone = currentUser.data[0].customers_telephone,
                    customers_fax = currentUser.data[0].customers_fax,
                    customers_newsletter = currentUser.data[0].customers_newsletter,
                    fb_id = currentUser.data[0].fb_id,
                    google_id = currentUser.data[0].google_id,
                    isActive = currentUser.data[0].isActive,
                    customers_default_address_id = currentUser.data[0].customers_default_address_id
                };

                await App.DataBase.SaveUser(dbUser);

                Settings.isLogin = true;
                Settings.IdCustomer = currentUser.data[0].customers_id;
                Settings.UserName = currentUser.data[0].customers_firstname;
                Settings.UserEmail = currentUser.data[0].customers_email_address;

                if (isNeedPop)
                {
                    var mdp = (Application.Current.MainPage as MasterDetailPage);
                    var navPage = mdp.Detail as NavigationPage;
                    await navPage.PopAsync();
                }
                else
                {
                    Application.Current.MainPage = new MasterDetailPage()
                    {
                        Master = new MasterPage() { Title = "Menú" },
                        Detail = new NavigationPage(new HomePage())
                    };
                }
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
