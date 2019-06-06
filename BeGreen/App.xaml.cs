using System;
using BeGreen.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeGreen
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new IntroPage();
            MainPage = new TutorialPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static bool CurrentConetion()
        {
            bool success = true;
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.None || current == NetworkAccess.Unknown)
            {
                success = false;
                Current.MainPage.DisplayAlert("Notificación", "No se cuenta con conexion a internet.", "Aceptar");
            }

            return success;
        }
    }
}
