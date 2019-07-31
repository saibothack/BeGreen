using System;
using System.Collections.Generic;
using BeGreen.Models.Order;
using BeGreen.ViewModels.Popup;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeGreen.Views.popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CancelCommentPage
    {
        private CancelCommentPageViewModels viewModel;

        [Obsolete]
        public CancelCommentPage(OrderDetails orderDetail)
        {
            InitializeComponent();

            BindingContext = viewModel = new CancelCommentPageViewModels(orderDetail);
        }
    }
}
