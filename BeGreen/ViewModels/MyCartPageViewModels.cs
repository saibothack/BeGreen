using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BeGreen.Models.Cart;
using BeGreen.Utilities;
using BeGreen.Views;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class MyCartPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public AsyncCommand CommandInitialize { get; internal set; }
        public AsyncCommand CommandOrderSales { get; internal set; }
        public AsyncCommand CommandAddProduct { get; internal set; }
        public AsyncCommand CommandRemoveProduct { get; internal set; }
        public AsyncCommand CommandDeleteProduct { get; internal set; }

        public ImageSource imgNavigation { get; set; }
        public ImageSource imgTrash { get; set; }
        public ImageSource imgBackground { get; set; }

        public Command CommandNavigation { get; set; }

        public int RowDefinitionHeader { get; set; }

        public class sourceCart {
            public int products_id { get; set; }
            public string categories_name { get; set; }
            public string products_name { get; set; }
            public ImageSource products_image { get; set; }
            public double products_price { get; set; }
            public double total_price { get; set; }
            public int customers_basket_quantity { get; set; }

        }

        #region "Properties"

        private ObservableCollection<sourceCart> _sourceCartProducts;
        public ObservableCollection<sourceCart> sourceCartProducts
        {
            get { return _sourceCartProducts; }
            set
            {
                SetProperty(ref _sourceCartProducts, value);
            }
        }

        private double _subTotal;
        public double subTotal
        {
            get { return _subTotal; }
            set
            {
                SetProperty(ref _subTotal, value);
            }
        }

        private double _dEnvio;
        public double dEnvio
        {
            get { return _dEnvio; }
            set
            {
                SetProperty(ref _dEnvio, value);
            }
        }

        private string _sCoupon;
        public string sCoupon
        {
            get { return _sCoupon; }
            set
            {
                SetProperty(ref _sCoupon, value);
            }
        }

        private double _dTotal;
        public double dTotal
        {
            get { return _dTotal; }
            set
            {
                SetProperty(ref _dTotal, value);
            }
        }

        private sourceCart _selectedItem;
        public sourceCart selectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
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

        #endregion

        public MyCartPageViewModels()
        {
            imgNavigation = ImageSource.FromResource("BeGreen.Images.nav_perfil_min.png");
            imgTrash = ImageSource.FromResource("BeGreen.Images.ic_trash.png");
            imgBackground = ImageSource.FromResource("BeGreen.Images.empty_set.png");

            RowDefinitionHeader = Device.RuntimePlatform == Device.Android ? 50 : 80;

            sourceCartProducts = new ObservableCollection<sourceCart>();

            CommandNavigation = new Command(ShowMenu);

            CommandInitialize = new AsyncCommand(InitializeAsync, CanExecuteSubmit);
            CommandAddProduct = new AsyncCommand(AddProduct, CanExecuteSubmit);
            CommandRemoveProduct = new AsyncCommand(RemoveProduct, CanExecuteSubmit);
            CommandDeleteProduct = new AsyncCommand(DeleteProduct, CanExecuteSubmit);
            CommandOrderSales = new AsyncCommand(OrderSales, CanExecuteSubmit);

            isEmptyVisible = true;
        }

        void ShowMenu()
        {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            mdp.IsPresented = true;
        }

        private async Task OrderSales()
        {
            try
            {
                if (sourceCartProducts.Count > 0)
                {
                    var mdp = (Application.Current.MainPage as MasterDetailPage);
                    var navPage = mdp.Detail as NavigationPage;
                    await navPage.PushAsync(new OrderSalesPage() { Title = "" });
                } else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Por favor selecciona algunos productos para continuar con tu compra", "Aceptar");
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AddProduct() {
            //var producto = await App.DataBase.GetProductById(products_id);
        }

        private async Task RemoveProduct() { }
        private async Task DeleteProduct() { }

        private async Task InitializeAsync()
        {
            try
            {
                IsBusy = true;

                sourceCartProducts = new ObservableCollection<sourceCart>();
                isEmptyVisible = true;

                subTotal = 0;
                dTotal = 0;

                var cart = await App.DataBase.GetCartProductAsync();

                foreach (var item in cart)
                {
                    sourceCart sourceCart = new sourceCart
                    {
                        products_id = item.customersBasketProduct.products_id,
                        categories_name = item.customersBasketProduct.categories_name,
                        products_name = item.customersBasketProduct.products_name,
                        products_image = item.customersBasketProduct.products_image,
                        products_price = double.Parse(item.customersBasketProduct.products_price),
                        total_price = double.Parse(item.customersBasketProduct.total_price),
                        customers_basket_quantity = item.customersBasketProduct.customers_basket_quantity
                    };

                    sourceCartProducts.Add(sourceCart);
                    isEmptyVisible = false;

                    subTotal = subTotal + double.Parse(item.customersBasketProduct.total_price);
                    dTotal = dTotal + double.Parse(item.customersBasketProduct.total_price);
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