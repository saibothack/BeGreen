﻿using System;
using System.Threading.Tasks;
using BeGreen.Models.Orchard;
using BeGreen.Utilities;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class OrchardDetailPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public AsyncCommand CommandNavigation { get; internal set; }
        public AsyncCommand CommandLike { get; internal set; }

        public Command CommandGoWeb { get; internal set; }
        public Command CommandHome { get; internal set; }
        public Command CommandOrchardProducts { get; internal set; }
        public Command CommandOrchardLocation { get; internal set; }

        public ImageSource imgBackground { get; set; }
        public ImageSource imgNavigation { get; set; }
        public ImageSource imgFavorite { get; set; }
        public ImageSource imgBackgroundHome { get; set; }
        public ImageSource imgHome { get; set; }
        public ImageSource imgMapLocation { get; set; }

        #region "Properties"

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
            imgBackground = ImageSource.FromResource("BeGreen.Images.producto_fondo.png");
            imgNavigation = ImageSource.FromResource("BeGreen.Images.left-arrow.png");
            imgFavorite = ImageSource.FromResource("BeGreen.Images.favorite.png");
            imgBackgroundHome = ImageSource.FromResource("BeGreen.Images.huertas_boton_prod.png");
            imgHome = ImageSource.FromResource("BeGreen.Images.huertas_home.png");
            imgMapLocation = ImageSource.FromResource("BeGreen.Images.map_default.png");

            CommandNavigation = new AsyncCommand(EventBackAsync, CanExecuteSubmit);
            CommandLike = new AsyncCommand(EventLikeAsync, CanExecuteSubmit);

            CommandGoWeb = new Command(EventGoWeb);
            CommandHome = new Command(EventHome);
            CommandOrchardProducts = new Command(EventOrchardProducts);
            CommandOrchardLocation = new Command(EventOrchardLocation);

            isHome = true;
        }

        private async Task EventLikeAsync() {
            
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