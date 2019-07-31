using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using BeGreen.Models.Coupon;
using BeGreen.Utilities;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class OffersPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public AsyncCommand CommandInitializeAsync { get; internal set; }
        public ImageSource imgNavigation { get; set; }
        public Color loadBackColor { get; set; }

        public Command CommandShowMenu { get; set; }
        public int RowDefinitionHeader { get; set; }

        #region "Properties"

        private ObservableCollection<CouponSource> _sourceCoupons;
        public ObservableCollection<CouponSource> sourceCoupons
        {
            get { return _sourceCoupons; }
            set
            {
                SetProperty(ref _sourceCoupons, value);
            }
        }

        #endregion

        public OffersPageViewModels()
        {
            RowDefinitionHeader = Device.RuntimePlatform == Device.Android ? 50 : 90;
            loadBackColor = Color.FromHsla(0, 0, 0, 0.1);
            imgNavigation = ImageSource.FromResource("BeGreen.Images.nav_perfil_min.png");
            CommandInitializeAsync = new AsyncCommand(InitializeAsync, CanExecuteSubmit);
            CommandShowMenu = new Command(showMenu);

            sourceCoupons = new ObservableCollection<CouponSource>();

        }

        void showMenu()
        {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;
            mdp.IsPresented = true;
        }

        private async Task InitializeAsync()
        {
            try
            {
                IsBusy = true;

                List<Coupon> data = await App.oServiceManager.getAllCoupons();

                foreach (var item in data)
                {
                    var sourceItem = new CouponSource();
                    var compar = compareToDate(item.date_modified);
                        
                    sourceItem.code = item.code;
                    sourceItem.description = item.description;
                    sourceItem.colorCode = Color.FromHex("#8bc540");
                            
                    switch (compar) {
                        case 1:
                            sourceItem.icon = ImageSource.FromResource("BeGreen.Images.clock.png");
                            sourceItem.colorCode = Color.Orange;
                            break;
                        case 2:
                            sourceItem.icon = ImageSource.FromResource("BeGreen.Images.star.png");
                            break;
                        case 3:
                            sourceItem.icon = ImageSource.FromResource("BeGreen.Images.clock.png");
                            sourceItem.colorCode = Color.Orange;
                            break;
                        default:
                            sourceItem.icon = ImageSource.FromResource("BeGreen.Images.star.png");
                            break;
                    }


                    switch (item.discount_type) {
                        case "percent_product":
                            sourceItem.offer = item.amount.ToString() + " % DE DESCUENTO EN PRODUCTO";
                            break;
                        case "percent":
                            sourceItem.offer = item.amount.ToString() + " % DE DESCUENTO EN TODA TU COMPRA";
                            break;
                        case "fixed_cart":
                            sourceItem.offer = "$" + item.amount.ToString() + " PESOS DE DESCUENTO EN PRODUCTO";
                            break;
                        case "fixed_product":
                            sourceItem.offer = "$" + item.amount.ToString() + " PESOS DE DESCUENTO EN TODA TU COMPRA";
                            break;
                    }

                    sourceCoupons.Add(sourceItem);
                }

                IsBusy = false;

            }
            finally
            {
                IsBusy = false;
            }
        }

        int compareToDate(string dateModified) {
            int compare = 0;

            try {
                var today = DateTime.Now;
                var dateCoupon = DateTime.Parse(dateModified);

                compare = today.CompareTo(dateCoupon);
            }
            catch (Exception ex) {
                Debug.Write("@Error" + ex.Message);
            }

            return compare;
        }

        private bool CanExecuteSubmit()
        {
            return !IsBusy;
        }
    }
}
