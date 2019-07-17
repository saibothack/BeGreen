using System;
using BeGreen.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using BeGreen.Helpers;
using System.Threading.Tasks;
using System.Diagnostics;
using BeGreen.Models.Orchard;
using BeGreen.Dabase;
using System.IO;

namespace BeGreen
{
    public partial class App : Application
    {
        private static string tax = "";
        public static Services.ServiceManager oServiceManager { get; private set; }
        static dbLogic database;
        public static Orchard ItemSelectedOrchard { get; set; }
        public static string TxtComment { get; set; }

        public App()
        {
            InitializeComponent();
            oServiceManager = new Services.ServiceManager(new Services.RestService());

            //MainPage = new LoginPage();
            //Settings.isShowIntro = false;

            if (Settings.isShowIntro)
                Current.MainPage = new MasterDetailPage()
                {
                    Master = new MasterPage() { Title = "Menú" },
                    Detail = new NavigationPage(new HomePage())
                };
            else 
                MainPage = new IntroPage();

        }

        public static dbLogic DataBase
        {
            get
            {
                if (database == null)
                {
                    try
                    {
                        database = new dbLogic(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BeGreen.db3"));
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(@"ERROR {0}", ex.Message);
                    }
                }
                return database;
            }
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

        public static string getTax()
        {
            return tax;
        }

        public static bool CurrentConetion()
        {
            bool success = true;

            try {
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.None || current == NetworkAccess.Unknown)
                {
                    success = false;
                    Current.MainPage.DisplayAlert("Notificación", "No se cuenta con conexion a internet.", "Aceptar");
                }
            } catch(Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
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
                await Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
            }

            return location;
        }
    }
}
