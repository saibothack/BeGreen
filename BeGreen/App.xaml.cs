using System;
using BeGreen.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using BeGreen.Helpers;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

namespace BeGreen
{
    public partial class App : Application
    {
        public static Services.ServiceManager oServiceManager { get; private set; }

        public App()
        {
            InitializeComponent();
            oServiceManager = new Services.ServiceManager(new Services.RestService());

            MainPage = new RegisterPage();

            /*if (Settings.isShowIntro)
                Current.MainPage = new MasterDetailPage()
                {
                    Master = new MasterPage() { Title = "Menú" },
                    Detail = new NavigationPage(new HomePage())
                };
            else 
                MainPage = new IntroPage();*/

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

        public static async Task<Location> ObtieneUbicacionAsync()
        {
            Location location = new Location();

            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best);
                location = await Geolocation.GetLocationAsync(request);


                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
            }

            return location;
        }
    }
}
