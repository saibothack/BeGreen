using System;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class HomePageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }

        public ImageSource imgCatalog { get; set; }
        public ImageSource imgOrchard { get; set; }
        public ImageSource imgPurchase { get; set; }
        public ImageSource imgOffer { get; set; }

        public HomePageViewModels()
        {
            imgCatalog = ImageSource.FromResource("BeGreen.Images.menu_catalogo.png");
            imgOrchard = ImageSource.FromResource("BeGreen.Images.menu_huertas.png");
            imgPurchase = ImageSource.FromResource("BeGreen.Images.menu_compras.png");
            imgOffer = ImageSource.FromResource("BeGreen.Images.menu_ofertas.png");
        }
    }
}
