using System;
using System.Threading.Tasks;
using BeGreen.Helpers;
using BeGreen.Models.Orchard;
using BeGreen.Utilities;
using BeGreen.Views;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class OrchardDetailPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public AsyncCommand CommandInitialize { get; internal set; }
        public AsyncCommand CommandNavigation { get; internal set; }
        public AsyncCommand CommandLike { get; internal set; }

        public Command CommandGoWeb { get; internal set; }
        public Command CommandHome { get; internal set; }
        public Command CommandOrchardProducts { get; internal set; }
        public Command CommandOrchardLocation { get; internal set; }

        public ImageSource imgBackground { get; set; }
        public ImageSource imgNavigation { get; set; }
        public ImageSource imgBackgroundHome { get; set; }
        public ImageSource imgHome { get; set; }
        public ImageSource imgMapLocation { get; set; }

        public Color loadBackColor { get; set; }

        public bool isFavorite { get; set; }
        public int RowDefinitionHeader { get; set; }

        #region "Properties"

        private ImageSource _imgFavorite;
        public ImageSource imgFavorite
        {
            get { return _imgFavorite; }
            set
            {
                SetProperty(ref _imgFavorite, value);
            }
        }

        private Orchard _ItemSelectedOrchard;
        public Orchard ItemSelectedOrchard
        {
            get { return _ItemSelectedOrchard; }
            set
            {
                SetProperty(ref _ItemSelectedOrchard, value);
            }
        }

        private bool _isHome;
        public bool isHome
        {
            get { return _isHome; }
            set
            {
                SetProperty(ref _isHome, value);
            }
        }

        private bool _isProducts;
        public bool isProducts
        {
            get { return _isProducts; }
            set
            {
                SetProperty(ref _isProducts, value);
            }
        }

        private bool _isLocation;
        public bool isLocation
        {
            get { return _isLocation; }
            set
            {
                SetProperty(ref _isLocation, value);
            }
        }

        #endregion

        public OrchardDetailPageViewModels()
        {
            RowDefinitionHeader = Device.RuntimePlatform == Device.Android ? 50 : 90;
            imgBackground = ImageSource.FromResource("BeGreen.Images.producto_fondo.png");
            imgNavigation = ImageSource.FromResource("BeGreen.Images.left-arrow.png");
            imgFavorite = ImageSource.FromResource("BeGreen.Images.favorite.png");
            imgBackgroundHome = ImageSource.FromResource("BeGreen.Images.huertas_boton_prod.png");
            imgHome = ImageSource.FromResource("BeGreen.Images.huertas_home.png");
            imgMapLocation = ImageSource.FromResource("BeGreen.Images.map_default.png");
            loadBackColor = Color.FromHsla(0, 0, 0, 0.1);

            CommandNavigation = new AsyncCommand(EventBackAsync, CanExecuteSubmit);
            CommandLike = new AsyncCommand(EventLikeAsync, CanExecuteSubmit);
            CommandInitialize = new AsyncCommand(InitializeAsync, CanExecuteSubmit);

            CommandGoWeb = new Command(EventGoWeb);
            CommandHome = new Command(EventHome);
            CommandOrchardProducts = new Command(EventOrchardProducts);
            CommandOrchardLocation = new Command(EventOrchardLocation);

            isHome = true;
        }

        private async Task InitializeAsync() {
            var orchard = await App.DataBase.GetOrchardsById(ItemSelectedOrchard.news_id);

            if (orchard.categories_id.Equals(ItemSelectedOrchard.categories_id))
            {
                isFavorite = true;
                imgFavorite = ImageSource.FromResource("BeGreen.Images.like.png");
            }
            else
            {
                isFavorite = false;
                imgFavorite = ImageSource.FromResource("BeGreen.Images.favorite.png");
            }
        }

        private async Task EventLikeAsync() {
            try
            {
                if (Settings.isLogin)
                {
                    IsBusy = true;
                    if (isFavorite)
                    {

                        isFavorite = false;
                        await App.DataBase.DeleteOrchar(ItemSelectedOrchard);
                        imgFavorite = ImageSource.FromResource("BeGreen.Images.favorite.png");
                    }
                    else
                    {
                        isFavorite = true;
                        await App.DataBase.SaveOrchardAsync(ItemSelectedOrchard);
                        imgFavorite = ImageSource.FromResource("BeGreen.Images.like.png");
                    }
                }
                else
                {
                    bool answer = await Application.Current.MainPage.DisplayAlert("Notificación", "No haz iniciado sesión, ¿deseas ingresar?", "Si", "No");

                    if (answer)
                    {
                        var mdp = (Application.Current.MainPage as MasterDetailPage);
                        var navPage = mdp.Detail as NavigationPage;
                        await navPage.PushAsync(new LoginPage(true));
                    }
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        void EventGoWeb() {
            if (!string.IsNullOrEmpty(ItemSelectedOrchard.news_url)) 
                Device.OpenUri(new Uri(ItemSelectedOrchard.news_url));
        }

        void EventHome() {
            isHome = true;
            isProducts = false;
            isLocation = false;
        }

        void EventOrchardProducts()
        {
            isHome = false;
            isProducts = true;
            isLocation = false;
        }

        void EventOrchardLocation() {
            isHome = false;
            isProducts = false;
            isLocation = true;
        }

        async Task EventBackAsync()
        {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;
            await navPage.PopAsync();
        }

        private bool CanExecuteSubmit()
        {
            return !IsBusy;
        }

    }
}