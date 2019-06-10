using System;
using BeGreen.Utilities;
using BeGreen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeGreen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryPage : ContentPage
    {
        private CategoryPageViewModels viewModel;

        public CategoryPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new CategoryPageViewModels();
            viewModel.Navigation = this.Navigation;

            IErrorHandler errorHandler = null;
            viewModel.CommandInit.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            IErrorHandler errorHandler = null;
            viewModel.ItemTapped.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }
    }
}
