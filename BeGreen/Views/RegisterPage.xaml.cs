using System;
using BeGreen.Utilities;
using BeGreen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeGreen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private RegisterPageViewModels viewModel;

        public RegisterPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new RegisterPageViewModels();
            viewModel.Navigation = this.Navigation;
        }

        void Handle_Clicked_Address(object sender, System.EventArgs e)
        {
            IErrorHandler errorHandler = null;
            viewModel.CommandAddress.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }

        void Handle_Clicked_Register(object sender, System.EventArgs e)
        {
            IErrorHandler errorHandler = null;
            viewModel.CommandRegister.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }
    }
}
