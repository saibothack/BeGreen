using System;
using BeGreen.Models.Product;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class ProductDetailPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public Product ProductSelected { get; set; }
        public ImageSource imgProducBackground { get; set; }
        public ImageSource imgBackground { get; set; }
        public ImageSource imgBackButton { get; set; }
        public Color loadingBackground { get; set; }
        public Command CommandBack { get; internal set; }

        public ProductDetailPageViewModels()
        {
            loadingBackground = Color.FromHsla(0, 0, 0, 0.1);
            imgBackground = ImageSource.FromResource("BeGreen.Images.catalog.png");
            imgBackButton = ImageSource.FromResource("BeGreen.Images.left-arrow.png");
            imgProducBackground = ImageSource.FromResource("BeGreen.Images.producto_fondo.png");
            CommandBack = new Command(EventBack);
        }

        async void EventBack()
        {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;
            await navPage.PopAsync();
        }

    }
}
