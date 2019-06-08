using System;
using System.Windows.Input;
using BeGreen.Helpers;
using BeGreen.Views;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class MasterPageViewModel
    {
        public string NombreUsuario { get; set; }
        public bool IsUsuarioVisible { get; set; }
        public ImageSource imageProfiler { get; set; }
        public ImageSource imageBackground { get; set; }
        public Command CommandRegister { get; set; }
        public Command CommandTerms { get; set; }

        public bool PanelRegister { get; set; }
        public bool PanelUser { get; set; }
        public string sName { get; set; }

        public MasterPageViewModel()
        {
            imageBackground = ImageSource.FromResource("BeGreen.Images.background_menu.png");
            imageProfiler = ImageSource.FromResource("BeGreen.Images.profile.png");
            CommandRegister = new Command(goToLogin);
            CommandTerms = new Command(Terms);

            if (Settings.isLogin) {
                PanelRegister = false;
                PanelUser = true;
                sName = Settings.UserName;
            } else {
                PanelRegister = true;
                PanelUser = false;
            }
        }

        private void Terms() {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;

            // Hide the Master page
            mdp.IsPresented = false;
            navPage.PushAsync(new TermsPage());
        }

        public void goToLogin() {
            if (Settings.isLogin)
            {
                Application.Current.MainPage = new NavigationPage(new RegisterPage());
            } else {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }

        public ICommand NavigationCommand
        {
            get
            {
                return new Command((value) =>
                {
                    // COMMENT: This is just quick demo code. Please don't put this in a production app.
                    var mdp = (Application.Current.MainPage as MasterDetailPage);
                    var navPage = mdp.Detail as NavigationPage;

                    // Hide the Master page
                    mdp.IsPresented = false;

                    switch (value)
                    {
                        case "1":
                            navPage.PushAsync(new HistoryPage());
                            break;

                        case "2":
                            navPage.PushAsync(new FavoritesPage());
                            break;

                        case "3":
                            navPage.PushAsync(new PaymentsPage());
                            break;

                        case "4":
                            Application.Current.MainPage = new NavigationPage(new TutorialPage());
                            break;

                        case "5":
                            navPage.PushAsync(new AboutUsPage());
                            break;

                        case "6":

                            Settings.isLogin = false;
                            Settings.UserName = string.Empty;
                            Settings.UserEmail = string.Empty;
                            Settings.UserAddress = string.Empty;

                            Application.Current.MainPage = new MasterDetailPage()
                            {
                                Master = new MasterPage() { Title = "Menú" },
                                Detail = new NavigationPage(new HomePage())
                            };

                            break;
                    }

                });
            }
        }
    }
}
