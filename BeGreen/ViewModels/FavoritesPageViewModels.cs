using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BeGreen.Helpers;
using BeGreen.Models.Cart;
using BeGreen.Models.Orchard;
using BeGreen.Models.Product;
using BeGreen.Utilities;
using BeGreen.Views;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class FavoritesPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public AsyncCommand CommandInitialize { get; internal set; }
        public AsyncCommand CommandDeleteProducAsync { get; internal set; }
        public AsyncCommand CommandAddProductAsync { get; internal set; }
        public AsyncCommand CommandDeleteOrchardAsync { get; internal set; }
        public AsyncCommand CommandDetailAsync { get; internal set; }

        public ImageSource imgNavigation { get; set; }
        public ImageSource imgBackground { get; set; }
        public ImageSource imgAddList { get; set; }

        public Command CommandNavigation { get; set; }
        public Command CommandShowProducts { get; set; }
        public Command CommandShowOrchards { get; set; }

        public ICommand CommandDeleteProduc
        {
            get
            {
                return new Command((e) =>
                {
                    itemProductSelect = (e as Product);
                    IErrorHandler errorHandler = null;
                    CommandDeleteProducAsync.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
                });
            }
        }

        public ICommand CommandAddProduct
        {
            get
            {
                return new Command((e) =>
                {
                    itemProductSelect = (e as Product);
                    IErrorHandler errorHandler = null;
                    CommandAddProductAsync.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
                });
            }
        }

        public ICommand CommandDeleteOrchard
        {
            get
            {
                return new Command((e) =>
                {
                    itemOrchardSelect = (e as Orchard);
                    IErrorHandler errorHandler = null;
                    CommandDeleteOrchardAsync.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
                });
            }
        }

        public ICommand CommandDetail
        {
            get
            {
                return new Command((e) =>
                {
                    itemOrchardSelect = (e as Orchard);
                    IErrorHandler errorHandler = null;
                    CommandDetailAsync.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
                });
            }
        }

        public int RowDefinitionHeader { get; set; }


        #region "properties"

        private ObservableCollection<Product> _sourceProducts;
        public ObservableCollection<Product> sourceProducts
        {
            get { return _sourceProducts; }
            set
            {
                SetProperty(ref _sourceProducts, value);
            }
        }
        private ObservableCollection<Orchard> _sourceOrchards;
        public ObservableCollection<Orchard> sourceOrchards
        {
            get { return _sourceOrchards; }
            set
            {
                SetProperty(ref _sourceOrchards, value);
            }
        }

        private Product _itemProductSelect;
        public Product itemProductSelect
        {
            get { return _itemProductSelect; }
            set
            {
                SetProperty(ref _itemProductSelect, value);
            }
        }
        private Orchard _itemOrchardSelect;
        public Orchard itemOrchardSelect
        {
            get { return _itemOrchardSelect; }
            set
            {
                SetProperty(ref _itemOrchardSelect, value);
            }
        }

        private bool _isProductVisible;
        public bool isProductVisible
        {
            get { return _isProductVisible; }
            set
            {
                SetProperty(ref _isProductVisible, value);
            }
        }

        private bool _isOrchardsVisible;
        public bool isOrchardsVisible
        {
            get { return _isOrchardsVisible; }
            set
            {
                SetProperty(ref _isOrchardsVisible, value);
            }
        }

        private bool _isEmptyVisible;
        public bool isEmptyVisible
        {
            get { return _isEmptyVisible; }
            set
            {
                SetProperty(ref _isEmptyVisible, value);
            }
        }

        private Color _ColorButtonProducts;
        public Color ColorButtonProducts
        {
            get { return _ColorButtonProducts; }
            set
            {
                SetProperty(ref _ColorButtonProducts, value);
            }
        }

        private Color _ColorButtonOrchards;
        public Color ColorButtonOrchards
        {
            get { return _ColorButtonOrchards; }
            set
            {
                SetProperty(ref _ColorButtonOrchards, value);
            }
        }

        private ImageSource _imgProductos;
        public ImageSource imgProductos
        {
            get { return _imgProductos; }
            set
            {
                SetProperty(ref _imgProductos, value);
            }
        }

        private ImageSource _imtHuertas;
        public ImageSource imtHuertas
        {
            get { return _imtHuertas; }
            set
            {
                SetProperty(ref _imtHuertas, value);
            }
        }



        #endregion

        public FavoritesPageViewModels()
        {
            RowDefinitionHeader = Device.RuntimePlatform == Device.Android ? 50 : 80;
            imgNavigation = ImageSource.FromResource("BeGreen.Images.left-arrow.png");
            imgBackground = ImageSource.FromResource("BeGreen.Images.empty_set.png");
            imgAddList = ImageSource.FromResource("BeGreen.Images.add_from_wish_list.png");

            ColorButtonProducts = Color.White;
            ColorButtonOrchards = Color.Black;

            imgProductos = ImageSource.FromResource("BeGreen.Images.btn_green.png");
            imtHuertas = ImageSource.FromResource("BeGreen.Images.btn_gray.png");

            CommandInitialize = new AsyncCommand(InitializeAsync, CanExecuteSubmit);
            CommandDeleteProducAsync = new AsyncCommand(DeleteProducAsync, CanExecuteSubmit);
            CommandAddProductAsync = new AsyncCommand(AddProductAsync, CanExecuteSubmit);
            CommandDeleteOrchardAsync = new AsyncCommand(DeleteOrchardAsync, CanExecuteSubmit);
            CommandDetailAsync = new AsyncCommand(DetailAsync, CanExecuteSubmit);

            CommandNavigation = new Command(ShowMenu);
            CommandShowProducts = new Command(ShowProducts);
            CommandShowOrchards = new Command(ShowOrchards);

            

            isEmptyVisible = true;
        }

        void ShowProducts()
        {
            /*ColorButtonProducts = Color.FromHex("#8bc540");
            ColorButtonOrchards = Color.Gray;*/

            ColorButtonProducts = Color.White;
            ColorButtonOrchards = Color.Black;

            imgProductos = ImageSource.FromResource("BeGreen.Images.btn_green.png");
            imtHuertas = ImageSource.FromResource("BeGreen.Images.btn_gray.png");


            if (sourceProducts.Count > 0) {
                isProductVisible = true;
                isEmptyVisible = false;
            } else {
                isProductVisible = false;
                isEmptyVisible = true;
            }

            isOrchardsVisible = false;
        }

        void ShowOrchards()
        {
            /*ColorButtonProducts = Color.Gray;
            ColorButtonOrchards = Color.FromHex("#8bc540"); */

            ColorButtonOrchards = Color.White;
            ColorButtonProducts = Color.Black;

            imtHuertas = ImageSource.FromResource("BeGreen.Images.btn_green.png");
            imgProductos = ImageSource.FromResource("BeGreen.Images.btn_gray.png");

            if (sourceOrchards.Count > 0)
            {
                isOrchardsVisible = true;
                isEmptyVisible = false;
            }
            else
            {
                isOrchardsVisible = false;
                isEmptyVisible = true;
            }

            isProductVisible = false;
        }

        void ShowMenu()
        {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;
            navPage.PopAsync();

        }

        private async Task InitializeAsync() {
            try
            {
                IsBusy = true;

                sourceProducts = new ObservableCollection<Product>();
                sourceOrchards = new ObservableCollection<Orchard>();

                if (!Settings.isLogin)
                {
                    bool answer = await Application.Current.MainPage.DisplayAlert("Notificación", "No haz iniciado sesión, ¿deseas ingresar?", "Si", "No");

                    if (answer)
                    {
                        var mdp = (Application.Current.MainPage as MasterDetailPage);
                        var navPage = mdp.Detail as NavigationPage;
                        await navPage.PushAsync(new LoginPage(true));
                    }

                    isProductVisible = false;
                    isOrchardsVisible = false;
                }
                else {
                    var products = await App.DataBase.GetProductsAsync();
                    var orchards = await App.DataBase.GetOrchards();

                    var prodcts = from x in products
                                  .Where(x => x.isLiked.Equals("1"))
                                  select x;

                    foreach (var item in prodcts)
                    {
                        sourceProducts.Add(item);
                    }

                    foreach (var item in orchards)
                    {
                        sourceOrchards.Add(item);
                    }

                    if (sourceProducts.Count > 0)
                    {
                        isProductVisible = true;
                        isEmptyVisible = false;
                    }
                    else
                    {
                        isProductVisible = false;
                        isEmptyVisible = true;
                    }

                    if (sourceOrchards.Count > 0)
                        isOrchardsVisible = false;
                }

            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task DeleteProducAsync() {
            try
            {
                
                IsBusy = true;

                itemProductSelect.isLiked = "0";
                await App.oServiceManager.setUnLikeProducts(itemProductSelect.products_id, Settings.IdCustomer);
                await App.DataBase.DeleteProducts(itemProductSelect);
                await InitializeAsync();
                itemProductSelect = null;


                IsBusy = false;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AddProductAsync() {
            try
            {
                    
                IsBusy = true;

                CartProduct cartProduct = new CartProduct();

                double? productBasePrice, productFinalPrice, attributesPrice = 0;
                List<CartProductAttributes> selectedAttributesList = new List<CartProductAttributes>();
                string discount = Util.checkDiscount(itemProductSelect.products_price.ToString(), itemProductSelect.discount_price);

                // Get Product's Price based on Discount
                if (discount != null)
                {
                    itemProductSelect.isSale_product = "1";
                    productBasePrice = double.Parse(itemProductSelect.discount_price);
                }
                else
                {
                    itemProductSelect.isSale_product = "0";
                    productBasePrice = itemProductSelect.products_price;
                }

                // Add Attributes Price to Product's Final Price
                productFinalPrice = itemProductSelect.products_price;
                double? priceAux = itemProductSelect.products_price;

                itemProductSelect.customers_basket_quantity = 1;
                itemProductSelect.products_price = productBasePrice;
                itemProductSelect.attributes_price = attributesPrice.ToString();
                itemProductSelect.final_price = productFinalPrice.ToString();
                itemProductSelect.total_price = priceAux;
                itemProductSelect.comentProduct = string.Empty;

                cartProduct.customersBasketProduct = itemProductSelect;
                cartProduct.customersBasketProductAttributes = selectedAttributesList;

                App.TxtComment = string.Empty;
                App.ItemSelectedOrchard = new Models.Orchard.Orchard();

                await App.DataBase.SaveCartProductAsync(cartProduct);
                itemProductSelect = null;

                await Application.Current.MainPage.DisplayAlert("Notificación", "Se agrego su producto al carrito", "Aceptar");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task DeleteOrchardAsync() {
            try
            {
                IsBusy = true;

                await App.DataBase.DeleteOrchar(itemOrchardSelect);
                await InitializeAsync();
                itemOrchardSelect = null;

                IsBusy = false;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task DetailAsync() {
            try
            {
                IsBusy = true;

                var mdp = (Application.Current.MainPage as MasterDetailPage);
                var navPage = mdp.Detail as NavigationPage;
                await navPage.PushAsync(new OrchardDetailPage(itemOrchardSelect));
                itemOrchardSelect = null;
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
