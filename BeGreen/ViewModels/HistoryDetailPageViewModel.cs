using System;
using System.Threading.Tasks;
using BeGreen.Models.Order;
using BeGreen.Utilities;
using BeGreen.Views.popup;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class HistoryDetailPageViewModel : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public AsyncCommand CommandInitialize { get; internal set; }
        public AsyncCommand CommandBack { get; internal set; }
        public AsyncCommand CommandCancel { get; internal set; }

        public ImageSource imgBackButton { get; set; }

        public Color loadBackColor { get; set; }

        public int RowDefinitionHeader { get; set; }

        public bool isVisibleCancel { get; set; }

        private OrderDetails _orderDetail;
        public OrderDetails orderDetail
        {
            get { return _orderDetail; }
            set
            {
                SetProperty(ref _orderDetail, value);
            }
        }

        private Color _ColorStatus;
        public Color ColorStatus
        {
            get { return _ColorStatus; }
            set
            {
                SetProperty(ref _ColorStatus, value);
            }
        }

        private int _HeightProduct;
        public int HeightProduct
        {
            get { return _HeightProduct; }
            set
            {
                SetProperty(ref _HeightProduct, value);
            }
        }

        [Obsolete]
        public HistoryDetailPageViewModel(OrderDetails orderDetail)
        {
            imgBackButton = ImageSource.FromResource("BeGreen.Images.left-arrow.png");

            CommandBack = new AsyncCommand(BackAsync, CanExecuteSubmit);
            CommandCancel = new AsyncCommand(CancelAsync, CanExecuteSubmit);

            RowDefinitionHeader = Device.RuntimePlatform == Device.Android ? 50 : 90;

            loadBackColor = Color.FromHsla(0, 0, 0, 0.1);
            this.orderDetail = orderDetail;

            switch (this.orderDetail.status) {
                case "Pendiente":
                    ColorStatus = Color.Gray;
                    isVisibleCancel = true;
                    break;
                case "Realizado":
                    ColorStatus = Color.FromHex("#8bc540");
                    break;
                case "Cancelado":
                    ColorStatus = Color.Red;
                    break;
            }

            HeightProduct = (60 * orderDetail.products.Count);

        }

        private bool CanExecuteSubmit()
        {
            return !IsBusy;
        }

        [Obsolete]
        private async Task CancelAsync() {
            try
            {
                IsBusy = true;

                var mdp = (Application.Current.MainPage as MasterDetailPage);
                var navPage = mdp.Detail as NavigationPage;
                //await navPage.PushAsync(new CancelPage());
                //await PopupNavigation.PushAsync(new CancelPage());
                await Navigation.PushModalAsync(new CancelPage(orderDetail));


                IsBusy = false;

            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task BackAsync()
        {
            try
            {
                IsBusy = true;

                var mdp = (Application.Current.MainPage as MasterDetailPage);
                var navPage = mdp.Detail as NavigationPage;
                await navPage.PopAsync();

                IsBusy = false;

            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
