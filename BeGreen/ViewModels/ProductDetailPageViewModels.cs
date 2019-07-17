using System;
using BeGreen.Models.Product;
using BeGreen.Helpers;
using Xamarin.Forms;
using BeGreen.Utilities;
using BeGreen.Views;
using System.Threading.Tasks;
using BeGreen.Models.Cart;
using System.Collections.Generic;
using System.Linq;

namespace BeGreen.ViewModels
{
    public class ProductDetailPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public ImageSource imgProducBackground { get; set; }
        public ImageSource imgBackground { get; set; }
        public ImageSource imgBackButton { get; set; }

        public AsyncCommand CommandInitialize { get; internal set; }
        public AsyncCommand CommandComments { get; internal set; }
        public AsyncCommand CommandSelectOrchard { get; internal set; }
        public AsyncCommand CommandFavorite { get; internal set; }
        public AsyncCommand CommandAddToCar { get; internal set; }

        public ImageSource imgProduct { get; set; }

        public Color loadBackColor { get; set; }

        public Command CommandBack { get; internal set; }
        public Command CommandAdd { get; internal set; }
        public Command CommandDelete { get; internal set; }

        public int RowDefinitionHeader { get; set; }

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
            loadBackColor = Color.FromHsla(0, 0, 0, 0.1);
            imgBackground = ImageSource.FromResource("BeGreen.Images.catalog.png");
            imgBackButton = ImageSource.FromResource("BeGreen.Images.left-arrow.png");
            imgProducBackground = ImageSource.FromResource("BeGreen.Images.producto_fondo.png");
            imgFavoriteButton = ImageSource.FromResource("BeGreen.Images.favorite.png");

            RowDefinitionHeader = Device.RuntimePlatform == Device.Android ? 50 : 80;

            App.ItemSelectedOrchard = new Models.Orchard.Orchard();
            App.TxtComment = string.Empty;

            CommandComments = new AsyncCommand(GoComments, CanExecuteSubmit);
            CommandSelectOrchard = new AsyncCommand(GoSelectOrchard, CanExecuteSubmit);
            CommandFavorite = new AsyncCommand(EventFavorite, CanExecuteSubmit);
            CommandAddToCar = new AsyncCommand(AddToCar, CanExecuteSubmit);
            CommandInitialize = new AsyncCommand(InitializeAsync, CanExecuteSubmit);

            CommandBack = new Command(EventBack);
            CommandAdd = new Command(EventAddProduct);
            CommandDelete = new Command(EventDeleteProduct);

            btnCommentBackground = Color.Gray;
            btnOrchardBackground = Color.Gray;
        }

        private async Task AddToCar()
        {
            try
            {
                if (Settings.isLogin)
                {
                    if (NumberProduct > 0)
                    {
                        IsBusy = true;

                        CartProduct cartProduct = new CartProduct();

                        double productBasePrice, productFinalPrice, attributesPrice = 0;
                        List<CartProductAttributes> selectedAttributesList = new List<CartProductAttributes>();
                        string discount = Util.checkDiscount(ProductSelected.products_price, ProductSelected.discount_price);

                        // Get Product's Price based on Discount
                        if (discount != null)
                        {
                            ProductSelected.isSale_product = "1";
                            productBasePrice = double.Parse(ProductSelected.discount_price);
                        }
                        else
                        {
                            ProductSelected.isSale_product = "0";
                            productBasePrice = double.Parse(ProductSelected.products_price);
                        }

                        foreach (var item in ProductSelected.attributes)
                        {
                            CartProductAttributes productAttribute = new CartProductAttributes();

                            Option option = new Option
                            {
                                id = item.option.id,
                                name = item.option.name
                            };

                            Value value = new Value
                            {
                                id = item.values[0].id,
                                value = item.values[0].value,
                                price = item.values[0].price,
                                price_prefix = item.values[0].price_prefix
                            };

                            string attrPrice = value.price_prefix + value.price;
                            attributesPrice += Double.Parse(attrPrice);


                            List<Value> valuesList = new List<Value>();
                            valuesList.Add(value);

                            productAttribute.option = option;
                            productAttribute.values = valuesList;

                            selectedAttributesList.Add(productAttribute);
                        }

                        // Add Attributes Price to Product's Final Price
                        productFinalPrice = productBasePrice + attributesPrice;
                        double priceAux = PriceTotalProduct;

                        ProductSelected.customers_basket_quantity = NumberProduct;
                        ProductSelected.products_price = productBasePrice.ToString();
                        ProductSelected.attributes_price = attributesPrice.ToString();
                        ProductSelected.final_price = productFinalPrice.ToString();
                        ProductSelected.total_price = priceAux.ToString();
                        ProductSelected.comentProduct = App.TxtComment;

                        cartProduct.customersBasketProduct = ProductSelected;
                        cartProduct.customersBasketProductAttributes = selectedAttributesList;

                        App.TxtComment = string.Empty;
                        App.ItemSelectedOrchard = new Models.Orchard.Orchard();

                        await App.DataBase.SaveCartProductAsync(cartProduct);

                        var mdp = (Application.Current.MainPage as MasterDetailPage);
                        var navPage = mdp.Detail as NavigationPage;
                        await navPage.PopAsync();

                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Notificación", "No hay material disponible", "Aceptar");
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

        private async Task GoComments()
        {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;
            await navPage.PushAsync(new CommentaryPage());
        }

        private async Task GoSelectOrchard()
        {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;
            await navPage.PushAsync(new SelectOrchardPage());
        }


        public async Task InitializeAsync()
        {
            EventAddProduct();

            if (ProductSelected.products_weight_unit.Equals("kg"))
            {
                selectedKilo = Color.FromHex("#8bc540");
                selectedPieza = Color.Gray;
            }
            else
            {
                selectedKilo = Color.Gray;
                selectedPieza = Color.FromHex("#8bc540");
            }

            var products = await App.DataBase.GetProductsAsync();

            var pdts = (from x in products
                       .Where (x => x.products_id.Equals(ProductSelected.products_id) && x.isLiked.Equals("1"))
                       select x).ToList();

            if (pdts.Count < 1)
            {
                imgFavoriteButton = ImageSource.FromResource("BeGreen.Images.favorite.png");
                ProductSelected.isLiked = "0";
            }
            else {
                imgFavoriteButton = ImageSource.FromResource("BeGreen.Images.like.png");
                ProductSelected.isLiked = "1";
            }
        }

        public void EventAddProduct()
        {
            NumberProduct++;
            PriceTotalProduct = (double.Parse(ProductSelected.products_price) * NumberProduct);
        }
        
        void EventDeleteProduct()
        {
            if (NumberProduct > 1)
            {
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
            try
            {
                if (Settings.isLogin)
                {
                    IsBusy = true;

                    if (ProductSelected.isLiked.Equals("0"))
                    {
                        ProductSelected.isLiked = "1";
                        await App.oServiceManager.setLikeProducts(ProductSelected.products_id, Settings.IdCustomer);
                        await App.DataBase.SaveProductsAsync(ProductSelected);
                        imgFavoriteButton = ImageSource.FromResource("BeGreen.Images.like.png");
                    }
                    else
                    {
                        ProductSelected.isLiked = "0";
                        await App.oServiceManager.setUnLikeProducts(ProductSelected.products_id, Settings.IdCustomer);
                        await App.DataBase.DeleteProducts(ProductSelected);
                        imgFavoriteButton = ImageSource.FromResource("BeGreen.Images.favorite.png");
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

        private bool CanExecuteSubmit()
        {
            return !IsBusy;
        }
    }
}
