using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BeGreen.Models.Category;
using BeGreen.Models.Product;
using BeGreen.Services.Logic;
using BeGreen.Utilities;
using BeGreen.Views;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class ProductsPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public List<Category> SubCategories { get; internal set; }
        public AsyncCommand CommandInit { get; internal set; }
        public AsyncCommand CommandSearch { get; internal set; }
        public AsyncCommand CommandSelected { get; internal set; }
        public ImageSource imgProducBackground { get; set; }
        public ImageSource imgBackground { get; set; }
        public ImageSource imgBackButton { get; set; }
        public Command CommandBack { get; internal set; }
        public Color loadingBackground { get; set; }
        public List<Product> AllProducts { get; set; }
        public bool isSearch { get; set; }

        #region "Properties"

        private string _txtSearch;
        public string txtSearch
        {
            get { return _txtSearch; }
            set
            {
                SetProperty(ref _txtSearch, value);
            }
        }

        private ObservableCollection<Product> _dataProducts;
        public ObservableCollection<Product> dataProducts
        {
            get { return _dataProducts; }
            set
            {
                SetProperty(ref _dataProducts, value);
            }
        }

        private bool _isBackVisible;
        public bool isBackVisible
        {
            get { return _isBackVisible; }
            set
            {
                SetProperty(ref _isBackVisible, value);
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

        #endregion

        public ProductsPageViewModels()
        {
            loadingBackground = Color.FromHsla(0, 0, 0, 0.1);
            CommandInit = new AsyncCommand(InitializeAsync, CanExecuteSubmit);
            CommandSearch = new AsyncCommand(SeachText, CanExecuteSubmit);
            CommandSelected = new AsyncCommand(ItemSelected, CanExecuteSubmit);

            imgBackground = ImageSource.FromResource("BeGreen.Images.catalog.png");
            imgBackButton = ImageSource.FromResource("BeGreen.Images.left-arrow.png");
            imgProducBackground = ImageSource.FromResource("BeGreen.Images.producto_fondo.png");

            CommandBack = new Command(back);

            isBackVisible = true;
        }

        private async Task ItemSelected() {

            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;
            await navPage.PushAsync(new ProductDetailPage(ProductSelected));
        }

        async void back()
        {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;
            await navPage.PopAsync();
        }

        #pragma warning disable CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica
        private async Task SeachText()
        {
        #pragma warning restore CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica
            try
            {
                if (txtSearch.Length > 2)
                {
                    IsBusy = true;
                    isSearch = true;

                    var productsSeach = from x in AllProducts
                           .Where(x => x.products_name.ToUpper().Contains(txtSearch.ToUpper()))
                                        select x;

                    dataProducts = new ObservableCollection<Product>();

                    foreach (var item in productsSeach)
                    {
                        dataProducts.Add(item);
                    }

                    IsBusy = false;
                } else {
                    if (isSearch) {
                        dataProducts = new ObservableCollection<Product>();

                        foreach (var item in AllProducts)
                        {
                            dataProducts.Add(item);
                        }

                        isSearch = false;
                    }
                }

            }
            finally
            {
                if (dataProducts.Count > 0) {
                    isBackVisible = false;
                }
                else {
                    isBackVisible = true;
                }

                IsBusy = false;
            }


        }

        private bool CanExecuteSubmit()
        {
            return !IsBusy;
        }

        private async Task InitializeAsync()
        {
            try
            {
                IsBusy = true;

                ServicesProduct servicesProduct = new ServicesProduct();
                AllProducts = await servicesProduct.ProductsAsync(SubCategories);

                dataProducts = new ObservableCollection<Product>();

                foreach (var item in AllProducts)
                {
                    dataProducts.Add(item);
                }

                IsBusy = false;

            }
            finally
            {
                if (dataProducts.Count > 0)
                {
                    isBackVisible = false;
                }
                else
                {
                    isBackVisible = true;
                }

                IsBusy = false;
            }
        }
    }
}
