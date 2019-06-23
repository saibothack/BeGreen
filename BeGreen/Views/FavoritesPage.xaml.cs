using System;
using System.Collections.Generic;
using BeGreen.Utilities;
using BeGreen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeGreen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoritesPage : ContentPage
    {
        private FavoritesPageViewModels viewModel;

        public FavoritesPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new FavoritesPageViewModels();
            viewModel.Navigation = this.Navigation;

            IErrorHandler errorHandler = null;
            viewModel.CommandInitialize.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }
    }
}
