using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BeGreen.Models.Category;
using BeGreen.Services.Logic;
using BeGreen.Utilities;
using BeGreen.Views;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class CategoryPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public AsyncCommand CommandInit { get; internal set; }
        public AsyncCommand ItemTapped { get; set; }
        public ImageSource imgBackground { get; set; }
        public ImageSource faIcon { get; set; }
        public Color loadingBackground { get; set; }
        public Command CommandShowMenu { get; set; }
        public List<Category> subCategories { get; set; }

        #region "Properties"

        private ObservableCollection<Category> _dataCategories;
        public ObservableCollection<Category> dataCategories
        {
            get { return _dataCategories; }
            set
            {
                SetProperty(ref _dataCategories, value);
            }
        }

        private bool _isCategoryVisible;
        public bool isCategoryVisible
        {
            get { return _isCategoryVisible; }
            set
            {
                SetProperty(ref _isCategoryVisible, value);
            }
        }

        private Category _CategorySelected;
        public Category CategorySelected
        {
            get { return _CategorySelected; }
            set
            {
                SetProperty(ref _CategorySelected, value);
            }
        }

        #endregion

        public CategoryPageViewModels()
        {
            CommandInit = new AsyncCommand(InitializeAsync, CanExecuteSubmit);
            ItemTapped = new AsyncCommand(Selected, CanExecuteSubmit);
            loadingBackground = Color.FromHsla(0, 0, 0, 0.1);
            imgBackground = ImageSource.FromResource("BeGreen.Images.catalog.png");
            faIcon = ImageSource.FromResource("BeGreen.Images.nav_perfil_min.png");
            CommandShowMenu = new Command(showMenu);
        }

        void showMenu() {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;
            mdp.IsPresented = true;
        }

        private async Task Selected() {

            List<Category> subCategory = (from x in subCategories
                                          where x.parent_id.Equals(CategorySelected.id)
                              select x).ToList();

            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;
            await navPage.PushAsync(new ProductsPage(subCategory));
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
                List<Category> data = await servicesProduct.CategoriesAsync(1);

                dataCategories = new ObservableCollection<Category>();

                var categories = from x in data
                                 .Where(x => x.parent_id.Equals("0"))
                                 select x;

                subCategories = (from x in data
                                 .Where(x => x.parent_id != "0")
                                 select x).ToList();

                foreach (var item in categories) {
                    item.image = Constants.urlApi + item.image;
                    dataCategories.Add(item);
                }

                if (dataCategories.Count > 0)
                    isCategoryVisible = true;

                IsBusy = false;

            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
