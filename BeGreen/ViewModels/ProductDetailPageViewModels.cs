using System;
using BeGreen.Models.Product;
using BeGreen.Helpers;
using Xamarin.Forms;
using BeGreen.Utilities;
using BeGreen.Views;
using System.Threading.Tasks;

namespace BeGreen.ViewModels
{
    public class ProductDetailPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public ImageSource imgProducBackground { get; set;  }
        public ImageSource imgBackground { get; set; }
        public ImageSource imgBackButton { get; set; }

        public AsyncCommand CommandComments { get; internal set; }
        public AsyncCommand CommandSelectOrchard { get; internal set; }
        public AsyncCommand CommandFavorite { get; internal set; }

        public ImageSource imgProduct { get; set; }

        public Color loadingBackground { get; set; }

        public Command CommandBack { get; internal set; }
        public Command CommandAdd { get; internal set; }
        public Command CommandDelete { get; internal set; }

        #region "propiedades"

        private Color _btnCommentBackground;
        public Color btnCommentBackground
        {
            get { return _btnCommentBackground; }
            set
            {
                SetProperty(ref _btnCommentBackground, value);
            }
        }

        private Color _btnOrchardBackground;
        public Color btnOrchardBackground
        {
            get { return _btnOrchardBackground; }
            set
            {
                SetProperty(ref _btnOrchardBackground, value);
            }
        }

        private Product _ProductSelected;
        public Product ProductSelected
        {
            get { return _ProductSelected; }
            set
            {
                SetProperty(ref _ProductSelected, value);
            }
        }

        private int _NumberProduct;
        public int NumberProduct
        {
            get { return _NumberProduct; }
            set
            {
                SetProperty(ref _NumberProduct, value);
            }
        }

        private double _PriceTotalProduct;
        public double PriceTotalProduct
        {
            get { return _PriceTotalProduct; }
            set
            {
                SetProperty(ref _PriceTotalProduct, value);
            }
        }

        private Color _selectedPieza;
        public Color selectedPieza
        {
            get { return _selectedPieza; }
            set
            {
                SetProperty(ref _selectedPieza, value);
            }
        }

        private Color _selectedKilo;
        public Color selectedKilo
        {
            get { return _selectedKilo; }
            set
            {
                SetProperty(ref _selectedKilo, value);
            }
        }

        private ImageSource _imgFavoriteButton;
        public ImageSource imgFavoriteButton
        {
            get { return _imgFavoriteButton; }
            set
            {
                SetProperty(ref _imgFavoriteButton, value);
            }
        }

        #endregion

        public ProductDetailPageViewModels()
        {
            loadingBackground = Color.FromHsla(0, 0, 0, 0.1);
            imgBackground = ImageSource.FromResource("BeGreen.Images.catalog.png");
            imgBackButton = ImageSource.FromResource("BeGreen.Images.left-arrow.png");
            imgProducBackground = ImageSource.FromResource("BeGreen.Images.producto_fondo.png");
            imgFavoriteButton = ImageSource.FromResource("BeGreen.Images.favorite.png");

            CommandComments = new AsyncCommand(GoComments, CanExecuteSubmit);
            CommandSelectOrchard = new AsyncCommand(GoSelectOrchard, CanExecuteSubmit);
            CommandFavorite = new AsyncCommand(EventFavorite, CanExecuteSubmit);

            CommandBack = new Command(EventBack);
            CommandAdd = new Command(EventAddProduct);
            CommandDelete = new Command(EventDeleteProduct);

            btnCommentBackground = Color.Gray;
            btnOrchardBackground = Color.Gray;

        }

        private async Task GoComments() {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;
            await navPage.PushAsync(new CommentaryPage());
        }

        private async Task GoSelectOrchard() {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;
            await navPage.PushAsync(new SelectOrchardPage());
        }


        public void initPage() {
            EventAddProduct();

            if (ProductSelected.products_weight_unit.Equals("kg")) {
                selectedKilo = Color.FromHex("#8bc540");
                selectedPieza = Color.Gray;
            }
            else {
                selectedKilo = Color.Gray;
                selectedPieza = Color.FromHex("#8bc540");
            }
        }

        public void EventAddProduct() {
            NumberProduct++;
            PriceTotalProduct = (double.Parse(ProductSelected.products_price) * NumberProduct); 
        }

        void EventDeleteProduct() {
            if (NumberProduct > 1) {
                NumberProduct--;
                PriceTotalProduct = (double.Parse(ProductSelected.products_price) * NumberProduct);
            }
        }

        async void EventBack()
        {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;
            await navPage.PopAsync();
        }

        private async Task EventFavorite()
        {
            if (Settings.isLogin) {
                if (ProductSelected.isLiked.Equals("0"))
                {
                    ProductSelected.isLiked = "1";
                    await App.oServiceManager.setLikeProducts(ProductSelected.products_id, Settings.IdCustomer);
                    await App.DataBase.SaveProducts(ProductSelected);
                    imgFavoriteButton = ImageSource.FromResource("BeGreen.Images.like.png");
                }
                else
                {
                    ProductSelected.isLiked = "0";
                    await App.oServiceManager.setUnLikeProducts(ProductSelected.products_id, Settings.IdCustomer);
                    await App.DataBase.DeleteProducts(ProductSelected);
                    imgFavoriteButton = ImageSource.FromResource("BeGreen.Images.favorite.png");
                }
            } else {
                bool answer = await Application.Current.MainPage.DisplayAlert("Notificación", "No haz iniciado sesión, ¿deseas ingresar?", "Si", "No");

                if (answer) {
                    var mdp = (Application.Current.MainPage as MasterDetailPage);
                    var navPage = mdp.Detail as NavigationPage;
                    await navPage.PushAsync(new LoginPage(true));
                } 
            }
        }

        private bool CanExecuteSubmit()
        {
            return !IsBusy;
        }
    }
}
