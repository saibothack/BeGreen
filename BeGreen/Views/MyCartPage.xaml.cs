using System;
using System.Collections.Generic;
using BeGreen.Utilities;
using BeGreen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeGreen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyCartPage : ContentPage
    {
        private MyCartPageViewModels viewModel;

        public MyCartPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new MyCartPageViewModels();
            viewModel.Navigation = this.Navigation;

            IErrorHandler errorHandler = null;
            viewModel.CommandInitialize.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            IErrorHandler errorHandler = null;
            viewModel.CommandInitialize.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }

        public void Refresh()
        {
            IErrorHandler errorHandler = null;
            viewModel.CommandInitialize.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            IErrorHandler errorHandler = null;
            viewModel.CommandOrderSales.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }
    }
}
