using System;
using System.Collections.Generic;
using BeGreen.Models.Order;
using BeGreen.ViewModels.Popup;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeGreen.Views.popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CancelPage 
    {
        private CancelPageViewModels viewModel;

        [Obsolete]
        public CancelPage(OrderDetails orderDetail)
        {
            InitializeComponent();
            BindingContext = viewModel = new CancelPageViewModels(orderDetail);
        }
    }
}
