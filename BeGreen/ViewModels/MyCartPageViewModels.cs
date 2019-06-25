using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BeGreen.Models.Cart;
using BeGreen.Utilities;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class MyCartPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public AsyncCommand CommandInitialize { get; internal set; }

        public ImageSource imgNavigation { get; set; }
        public ImageSource imgTrash { get; set; }

        public Command CommandNavigation { get; set; }

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

        #endregion

        public MyCartPageViewModels()
        {
            imgNavigation = ImageSource.FromResource("BeGreen.Images.nav_perfil_min.png");
            imgTrash = ImageSource.FromResource("BeGreen.Images.ic_trash.png");

            sourceCartProducts = new ObservableCollection<sourceCart>();

            CommandNavigation = new Command(ShowMenu);

            CommandInitialize = new AsyncCommand(InitializeAsync, CanExecuteSubmit);
        }

        void ShowMenu()
        {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            mdp.IsPresented = true;
        }

        private async Task InitializeAsync()
        {
            try
            {
                IsBusy = true;

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
