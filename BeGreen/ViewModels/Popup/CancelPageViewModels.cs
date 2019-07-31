using System;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using BeGreen.Views.popup;
using BeGreen.Models.Order;

namespace BeGreen.ViewModels.Popup
{
    public class CancelPageViewModels : ViewModelBase
    {
        public Command CommandConfirm { get; set; }
        public Command CommandReturn { get; set; }

        OrderDetails orderDetail { get; set; }

        [Obsolete]
        public CancelPageViewModels(OrderDetails orderDetail)
        {
            this.orderDetail = orderDetail;
            CommandConfirm = new Command(Confirm);
            CommandReturn = new Command(Return);
        }

        [Obsolete]
        async void Confirm()
        {
            
            await PopupNavigation.PushAsync(new CancelCommentPage(orderDetail));
        }

        [Obsolete]
        async void Return() {
            await PopupNavigation.PopAsync(true);
        }
    }
}
