using BeGreen.Utilities;
using BeGreen.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeGreen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderSalesPage : ContentPage
    {
        private OrderSalesPageViewModels viewModel;

        public OrderSalesPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new OrderSalesPageViewModels();
            viewModel.Navigation = this.Navigation;

            IErrorHandler errorHandler = null;
            viewModel.CommandInitialize.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            IErrorHandler errorHandler = null;
            viewModel.CommandPayment.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }
    }
}