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

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }

        void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }

        void Handle_Clicked_2(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
