using System.Collections.Generic;
using BeGreen.Models.Category;
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
    }
}
