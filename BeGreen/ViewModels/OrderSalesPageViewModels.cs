using BeGreen.Services;
using BeGreen.Utilities;
using BeGreen.Views;
using PayPal.Forms;
using PayPal.Forms.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class OrderSalesPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public AsyncCommand CommandInitialize { get; internal set; }
        public AsyncCommand CommandPayment { get; internal set; }

        public ImageSource imgNavigation { get; set; }

        public Command CommandNavigation { get; set; }

        public int RowDefinitionHeader { get; set; }
        public Color loadBackColor { get; set; }

        private double? _subTotal;
        public double? subTotal
        {
            get { return _subTotal; }
            set
            {
                SetProperty(ref _subTotal, value);
            }
        }

        private bool _AddressErrorVisible;
        public bool AddressErrorVisible
        {
            get { return _AddressErrorVisible; }
            set
            {
                SetProperty(ref _AddressErrorVisible, value);
            }
        }

        private string _sComment;
        public string sComment
        {
            get { return _sComment; }
            set
            {
                SetProperty(ref _sComment, value);
            }
        }

        private string _sAddress;
        public string sAddress
        {
            get { return _sAddress; }
            set
            {
                SetProperty(ref _sAddress, value);
            }
        }

        private double? _dTotal;
        public double? dTotal
        {
            get { return _dTotal; }
            set
            {
                SetProperty(ref _dTotal, value);
            }
        }

        public OrderSalesPageViewModels() {
            imgNavigation = ImageSource.FromResource("BeGreen.Images.nav_perfil_min.png");
            RowDefinitionHeader = Device.RuntimePlatform == Device.Android ? 50 : 90;
            loadBackColor = Color.FromHsla(0, 0, 0, 0.1);

            CommandNavigation = new Command(ShowMenu);

            CommandInitialize = new AsyncCommand(InitializeAsync, CanExecuteSubmit);
            CommandPayment = new AsyncCommand(Payment, CanExecuteSubmit);
        }

        void ShowMenu()
        {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            mdp.IsPresented = true;
        }

        private async Task Payment() {
            try
            {
                IsBusy = true;

                if (!string.IsNullOrEmpty(sAddress)) {

                    /*
                    var result = await CrossPayPalManager.Current.Buy(new PayPalItem("Compra", new Decimal(dTotal), "MXN"),
                        new Decimal(0));
                    if (result.Status == PayPalStatus.Cancelled)
                    {
                        Debug.WriteLine("Cancelled");
                    }
                    else if (result.Status == PayPalStatus.Error)
                    {
                        Debug.WriteLine(result.ErrorMessage);
                    }
                    else if (result.Status == PayPalStatus.Successful)
                    {
                        PostOrder postOrder = new PostOrder();
                    await postOrder.PostOrderAsync(sComment, sAddress);
                    }
                    */

                    PostOrder postOrder = new PostOrder();
                    bool success = await postOrder.PostOrderAsync(sComment, sAddress);

                    if (success) {
                        await Application.Current.MainPage.DisplayAlert("Notificación", "Tu compra se ha realizado con exito", "Aceptar");

                        Application.Current.MainPage = new MasterDetailPage()
                        {
                            Master = new MasterPage() { Title = "Menú" },
                            Detail = new NavigationPage(new HomePage())
                        };
                    }
                } else
                {
                    AddressErrorVisible = true;
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task InitializeAsync()
        {
            try
            {
                IsBusy = true;

                subTotal = 0;
                dTotal = 0;

                var cart = await App.DataBase.GetCartProductAsync();

                foreach (var item in cart)
                {
                    subTotal = subTotal + item.customersBasketProduct.total_price;
                    dTotal = dTotal + item.customersBasketProduct.total_price;
                }

            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool CanExecuteSubmit()
        {
            return !IsBusy;
        }

    }
}
