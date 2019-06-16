using System;
using System.Collections.Generic;
using BeGreen.Utilities;
using BeGreen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeGreen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private LoginPageViewModels viewModel;

        public LoginPage(bool ndPop = false)
        {
            InitializeComponent();

            BindingContext = viewModel = new LoginPageViewModels();
            viewModel.Navigation = this.Navigation;
            viewModel.isNeedPop = ndPop;
        }

        void Handle_Clicked_Entrar(object sender, System.EventArgs e)
        {
            IErrorHandler errorHandler = null;
            viewModel.CommandLogin.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }
    }
}
