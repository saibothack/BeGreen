using System;
using BeGreen.Models.Order;
using BeGreen.Utilities;
using BeGreen.Views.popup;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace BeGreen.ViewModels.Popup
{
    public class CancelCommentPageViewModels : ViewModelBase
    {
        public Command CommandConfirm { get; set; }
        public OrderDetails orderDetail { get; set; }

        private string _sComment;
        public string sComment
        {
            get { return _sComment; }
            set
            {
                SetProperty(ref _sComment, value);
            }
        }

        [Obsolete]
        public CancelCommentPageViewModels(OrderDetails orderDetail)
        {
            this.orderDetail = orderDetail;
            CommandConfirm = new Command(Confirm);
        }

        [Obsolete]
        async void Confirm()
        {
            if (string.IsNullOrEmpty(sComment)) {
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor ingrese una razón para su cancelación.", "Aceptar");
            } else
            {
                if (await App.oServiceManager.setCancel(orderDetail.orders_id)) {

                    await Util.ShowMessage("Notificación", "Su cancelación fue completada.", "Aceptar", () =>
                    {
                        continuar();
                    });

                }
                else
                {
                    await PopupNavigation.PushAsync(new CancelErrorPage());
                }
            }
            //await PopupNavigation.PushAsync(new CancelCommentPage());
        }

        async void continuar() {
        }
    }
}
