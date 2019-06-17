﻿using BeGreen.Models.Orchard;
using BeGreen.Utilities;
using BeGreen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeGreen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrchardDetailPage : ContentPage
    {
        private OrchardDetailPageViewModels viewModel;

        public OrchardDetailPage(Orchard orchard)
        {
            InitializeComponent();

            BindingContext = viewModel = new OrchardDetailPageViewModels();
            viewModel.Navigation = this.Navigation;
            viewModel.ItemSelectedOrchard = orchard;
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            IErrorHandler errorHandler = null;
            viewModel.CommandNavigation.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }

        void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            IErrorHandler errorHandler = null;
            viewModel.CommandLike.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }
    }
}
