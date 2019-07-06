using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BeGreen.Helpers;
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

        public ImageSource imgNavigation { get; set; }
        public ImageSource imgBackground { get; set; }
        public ImageSource imgAddList { get; set; }

        public Command CommandNavigation { get; set; }
        public Command CommandShowProducts { get; set; }
        public Command CommandShowOrchards { get; set; }
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

        #endregion

        public FavoritesPageViewModels()
        {
            RowDefinitionHeader = Device.RuntimePlatform == Device.Android ? 50 : 80;
            imgNavigation = ImageSource.FromResource("BeGreen.Images.left-arrow.png");
            imgBackground = ImageSource.FromResource("BeGreen.Images.empty_set.png");
            imgAddList = ImageSource.FromResource("BeGreen.Images.add_from_wish_list.png");

            ColorButtonProducts = Color.FromHex("#8bc540");
            ColorButtonOrchards = Color.Gray;

            sourceProducts = new ObservableCollection<Product>();
            sourceOrchards = new ObservableCollection<Orchard>();

            CommandInitialize = new AsyncCommand(InitializeAsync, CanExecuteSubmit);

            CommandNavigation = new Command(ShowMenu);
            CommandShowProducts = new Command(ShowProducts);
            CommandShowOrchards = new Command(ShowOrchards);

            isEmptyVisible = true;
        }

        void ShowProducts()
        {
            ColorButtonProducts = Color.FromHex("#8bc540");
            ColorButtonOrchards = Color.Gray;

            if (sourceProducts.Count > 0) {
                isProductVisible = true;
            } else {
                isProductVisible = false;
                isEmptyVisible = true;
            }

            isOrchardsVisible = false;
        }

        void ShowOrchards()
        {
            ColorButtonProducts = Color.Gray;
            ColorButtonOrchards = Color.FromHex("#8bc540"); 

            if (sourceProducts.Count > 0)
            {
                isOrchardsVisible = true;
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

        private bool CanExecuteSubmit()
        {
            return !IsBusy;
        }
    }
}
