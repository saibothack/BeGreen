using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
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

        public ICommand CommandDelete
        {
            get
            {
                return new Command((e) =>
                {
                    selectedItem = (e as CartProduct);
                    IErrorHandler errorHandler = null;
                    CommandDeleteProduct.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
                });
            }
        }

        public ICommand CommandAdd
        {
            get
            {
                return new Command((e) =>
                {
                    selectedItem = (e as CartProduct);
                    IErrorHandler errorHandler = null;
                    CommandAddProduct.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
                });
            }
        }

        public ICommand CommandRemove
        {
            get
            {
                return new Command((e) =>
                {
                    selectedItem = (e as CartProduct);
                    IErrorHandler errorHandler = null;
                    CommandRemoveProduct.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
                });
            }
        }

        public int RowDefinitionHeader { get; set; }

        /*public class sourceCart {
            public int products_id { get; set; }
            public string categories_name { get; set; }
            public string products_name { get; set; }
            public ImageSource products_image { get; set; }
            public double products_price { get; set; }
            public double total_price { get; set; }
            public int customers_basket_quantity { get; set; }

        }*/

        #region "Properties"

        private ObservableCollection<CartProduct> _sourceCartProducts;
        public ObservableCollection<CartProduct> sourceCartProducts
        {
            get { return _sourceCartProducts; }
            set
            {
                SetProperty(ref _sourceCartProducts, value);
            }
        }

        private double? _subTotal;
        public double? subTotal
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

        private double? _dTotal;
        public double? dTotal
        {
            get { return _dTotal; }
            set
            {
                SetProperty(ref _dTotal, value);
            }
        }

        private CartProduct _selectedItem;
        public CartProduct selectedItem
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

            sourceCartProducts = new ObservableCollection<CartProduct>();

            CommandNavigation = new Command(ShowMenu);

            CommandInitialize = new AsyncCommand(InitializeAsync, CanExecuteSubmit);
            CommandOrderSales = new AsyncCommand(OrderSales, CanExecuteSubmit);
            CommandDeleteProduct = new AsyncCommand(DeleteProductAsync, CanExecuteSubmit);
            CommandAddProduct = new AsyncCommand(AddProduct, CanExecuteSubmit);
            CommandRemoveProduct = new AsyncCommand(RemoveProductAsync, CanExecuteSubmit);

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
            try
            {
                IsBusy = true;

                var item = sourceCartProducts.FirstOrDefault(i => i.ID.Equals(selectedItem.ID));

                if (item != null)
                {
                    var price = item.customersBasketProduct.products_price;

                    item.customersBasketProduct.customers_basket_quantity++;
                    item.customersBasketProduct.total_price = (item.customersBasketProduct.total_price + price);

                    await App.DataBase.UpdateProducts(item.customersBasketProduct);

                    sourceCartProducts = new ObservableCollection<Models.Cart.CartProduct>();
                    //isEmptyVisible = true;

                    subTotal = 0;
                    dTotal = 0;

                    var cart = await App.DataBase.GetCartProductAsync();

                    foreach (var itemCard in cart)
                    {
                        sourceCartProducts.Add(itemCard);
                        isEmptyVisible = false;

                        subTotal = subTotal + itemCard.customersBasketProduct.total_price;
                        dTotal = dTotal + itemCard.customersBasketProduct.total_price;
                    }
                }

                IsBusy = false;

            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task RemoveProductAsync() {

            try
            {
                IsBusy = true;

                var item = sourceCartProducts.FirstOrDefault(i => i.ID.Equals(selectedItem.ID));

                if (item != null)
                {
                    var price = item.customersBasketProduct.products_price;

                    if (item.customersBasketProduct.customers_basket_quantity > 1)
                    {
                        item.customersBasketProduct.customers_basket_quantity--;
                        item.customersBasketProduct.total_price = (item.customersBasketProduct.total_price - price);

                        await App.DataBase.UpdateProducts(item.customersBasketProduct);

                        sourceCartProducts = new ObservableCollection<Models.Cart.CartProduct>();
                        //isEmptyVisible = true;

                        subTotal = 0;
                        dTotal = 0;

                        var cart = await App.DataBase.GetCartProductAsync();

                        foreach (var itemCard in cart)
                        {
                            sourceCartProducts.Add(itemCard);
                            isEmptyVisible = false;

                            subTotal = subTotal + itemCard.customersBasketProduct.total_price;
                            dTotal = dTotal + itemCard.customersBasketProduct.total_price;
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Notificación", "¿No es posible quitar cantidad por favor elimine el producto de la cesta?", "Aceptar");
                    }
                }

                IsBusy = false;

            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task DeleteProductAsync() {

            try
            {
                IsBusy = true;

                bool answer = await Application.Current.MainPage.DisplayAlert("Notificación", "¿Seguro que desea eliminar el producto?", "Si", "No");

                if (answer)
                {
                    sourceCartProducts.Remove(selectedItem);
                    await App.DataBase.DeleteCartProduct(selectedItem.ID);

                    subTotal = 0;
                    dTotal = 0;

                    foreach (var item in sourceCartProducts)
                    {
                        subTotal = subTotal + item.customersBasketProduct.total_price;
                        dTotal = dTotal + item.customersBasketProduct.total_price;
                    }

                    if (sourceCartProducts.Count == 0)
                        isEmptyVisible = true;
                }

                IsBusy = false;

            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task InitializeAsync()
        {
            try
            {
                IsBusy = true;

                sourceCartProducts = new ObservableCollection<Models.Cart.CartProduct>();
                isEmptyVisible = true;

                subTotal = 0;
                dTotal = 0;

                var cart = await App.DataBase.GetCartProductAsync();

                foreach (var item in cart)
                {
                    sourceCartProducts.Add(item);
                    isEmptyVisible = false;

                    subTotal = subTotal + item.customersBasketProduct.total_price;
                    dTotal = dTotal + item.customersBasketProduct.total_price;
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
