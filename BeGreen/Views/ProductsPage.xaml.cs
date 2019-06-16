using System.Collections.Generic;
using BeGreen.Models.Category;
using BeGreen.Models.Product;
using BeGreen.Utilities;
using BeGreen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeGreen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsPage : ContentPage
    {
        private ProductsPageViewModels viewModel;

        public ProductsPage(List<Category> SubCategories)
        {
            InitializeComponent();

            App.ItemSelectedOrchard = null;
            App.TxtComment = string.Empty;

            BindingContext = viewModel = new ProductsPageViewModels();
            viewModel.Navigation = this.Navigation;
            viewModel.SubCategories = SubCategories;

            IErrorHandler errorHandler = null;
            viewModel.CommandInit.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            IErrorHandler errorHandler = null;
            viewModel.CommandSearch.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }

        private void SelectableItemsView_OnSelectionChanged(CollectionView sender, SelectionChangedEventArgs e)
        {
            if (sender.SelectedItem != null)
            {
                viewModel.ProductSelected = (Product)sender.SelectedItem;
                IErrorHandler errorHandler = null;
                viewModel.CommandSelected.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.ProductSelected = null;
            CVProducts.SelectedItem = null;
            CVProducts.SelectionMode = SelectionMode.None;
            CVProducts.SelectionMode = SelectionMode.Single;

        }
    }
}
