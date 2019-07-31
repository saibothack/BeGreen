using BeGreen.Models.Order;
using BeGreen.Utilities;
using BeGreen.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class HistoryPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public AsyncCommand CommandInitialize { get; internal set; }
        public AsyncCommand CommandBack { get; internal set; }
        public AsyncCommand CommandDetail { get; internal set; }

        public Command CommandAddMonth { get; set; }
        public Command CommandDeleteMonth { get; set; }

        public Command CommandShowPendiente { get; set; }
        public Command CommandShowRealizadas { get; set; }
        public Command CommandShowCanceladas { get; set; }

        public ImageSource arrowLeft { get; set; }
        public ImageSource arrowRight { get; set; }
        public ImageSource imgBackButton { get; set; }

        public Color loadBackColor { get; set; }

        public int RowDefinitionHeader { get; set; }
        public int CurrentMonth { get; set; }
        public int CurrentStatus { get; set; }

        public string[] ArrayMonth { get; set; }
        public string[] ArrayStatus { get; set; }

        public List<OrderDetails> AllOrders { get; set; }

        #region "Properties"

        private OrderDetails _selectedItem;
        public OrderDetails selectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
            }
        }

        private string _txtMonth;
        public string txtMonth
        {
            get { return _txtMonth; }
            set
            {
                SetProperty(ref _txtMonth, value);
            }
        }

        private Color _BackColorPendiente;
        public Color BackColorPendiente
        {
            get { return _BackColorPendiente; }
            set
            {
                SetProperty(ref _BackColorPendiente, value);
            }
        }

        private Color _BackColorRealizada;
        public Color BackColorRealizada
        {
            get { return _BackColorRealizada; }
            set
            {
                SetProperty(ref _BackColorRealizada, value);
            }
        }

        private Color _BackColorCancelada;
        public Color BackColorCancelada
        {
            get { return _BackColorCancelada; }
            set
            {
                SetProperty(ref _BackColorCancelada, value);
            }
        }

        private ObservableCollection<OrderDetails> _dataOrder;
        public ObservableCollection<OrderDetails> dataOrder
        {
            get { return _dataOrder; }
            set
            {
                SetProperty(ref _dataOrder, value);
            }
        }

        private ImageSource _imgPendientes;
        public ImageSource imgPendientes
        {
            get { return _imgPendientes; }
            set
            {
                SetProperty(ref _imgPendientes, value);
            }
        }

        private ImageSource _imgRealizados;
        public ImageSource imgRealizados
        {
            get { return _imgRealizados; }
            set
            {
                SetProperty(ref _imgRealizados, value);
            }
        }

        private ImageSource _imgCancelados;
        public ImageSource imgCancelados
        {
            get { return _imgCancelados; }
            set
            {
                SetProperty(ref _imgCancelados, value);
            }
        }

        private Color _txtColorPendiente;
        public Color txtColorPendiente
        {
            get { return _txtColorPendiente; }
            set
            {
                SetProperty(ref _txtColorPendiente, value);
            }
        }

        private Color _txtColorRealizados;
        public Color txtColorRealizados
        {
            get { return _txtColorRealizados; }
            set
            {
                SetProperty(ref _txtColorRealizados, value);
            }
        }

        private Color _txtColorCancelados;
        public Color txtColorCancelados
        {
            get { return _txtColorCancelados; }
            set
            {
                SetProperty(ref _txtColorCancelados, value);
            }
        }

        #endregion


        public HistoryPageViewModels()
        {
            RowDefinitionHeader = Device.RuntimePlatform == Device.Android ? 50 : 90;
            CommandInitialize = new AsyncCommand(InitializeAsync, CanExecuteSubmit);
            CommandBack = new AsyncCommand(BackAsync, CanExecuteSubmit);
            CommandDetail = new AsyncCommand(GoDetail, CanExecuteSubmit);

            loadBackColor = Color.FromHsla(0, 0, 0, 0.1);

            /*arrowLeft = ImageSource.FromResource("BeGreen.Images.arrow-left.png");
            arrowRight = ImageSource.FromResource("BeGreen.Images.arrow-right.png");*/
            imgBackButton = ImageSource.FromResource("BeGreen.Images.left-arrow.png");

            imgPendientes = ImageSource.FromResource("BeGreen.Images.btn_green.png");
            imgRealizados = ImageSource.FromResource("BeGreen.Images.btn_gray.png");
            imgCancelados = ImageSource.FromResource("BeGreen.Images.btn_gray.png");

            txtColorPendiente = Color.White;
            txtColorRealizados = Color.Black;
            txtColorCancelados = Color.Black;

            CommandAddMonth = new Command(AddMonth);
            CommandDeleteMonth = new Command(DeleteMonth);

            CurrentStatus = 0;

            CommandShowPendiente = new Command(ShowPendiente);
            CommandShowRealizadas = new Command(ShowRealizadas);
            CommandShowCanceladas = new Command(ShowCanceladas);

            ArrayMonth = new string[13];

            ArrayMonth[1] = "Enero";
            ArrayMonth[2] = "Febrero";
            ArrayMonth[3] = "Marzo";
            ArrayMonth[4] = "Abril";
            ArrayMonth[5] = "Mayo";
            ArrayMonth[6] = "Junio";
            ArrayMonth[7] = "Julio";
            ArrayMonth[8] = "Agosto";
            ArrayMonth[9] = "Septiembre";
            ArrayMonth[10] = "Octubre";
            ArrayMonth[11] = "Noviembre";
            ArrayMonth[12] = "Diciembre";

            ArrayStatus = new string[3];
            ArrayStatus[0] = "Pendiente";
            ArrayStatus[1] = "Realizado";
            ArrayStatus[2] = "Cancelado";

            CurrentMonth = DateTime.Now.Month;
            txtMonth = ArrayMonth[CurrentMonth];

            BackColorPendiente = Color.FromHex("#8bc540");
            BackColorRealizada = Color.LightGray;
            BackColorCancelada = Color.LightGray;
        }

        void ShowPendiente()
        {
            CurrentStatus = 0;
            BindingDataOrder();

            imgPendientes = ImageSource.FromResource("BeGreen.Images.btn_green.png");
            imgRealizados = ImageSource.FromResource("BeGreen.Images.btn_gray.png");
            imgCancelados = ImageSource.FromResource("BeGreen.Images.btn_gray.png");

            txtColorPendiente = Color.White;
            txtColorRealizados = Color.Black;
            txtColorCancelados = Color.Black;

            /*BackColorPendiente = Color.FromHex("#8bc540");
            BackColorRealizada = Color.LightGray;
            BackColorCancelada = Color.LightGray;*/
        }

        void ShowRealizadas()
        {
            CurrentStatus = 1;
            BindingDataOrder();

            imgPendientes = ImageSource.FromResource("BeGreen.Images.btn_gray.png");
            imgRealizados = ImageSource.FromResource("BeGreen.Images.btn_green.png");
            imgCancelados = ImageSource.FromResource("BeGreen.Images.btn_gray.png");

            txtColorPendiente = Color.Black;
            txtColorRealizados = Color.White;
            txtColorCancelados = Color.Black;

            /*BackColorPendiente = Color.LightGray;
            BackColorRealizada = Color.FromHex("#8bc540");
            BackColorCancelada = Color.LightGray;*/
        }

        void ShowCanceladas()
        {
            CurrentStatus = 2;
            BindingDataOrder();

            imgPendientes = ImageSource.FromResource("BeGreen.Images.btn_gray.png");
            imgRealizados = ImageSource.FromResource("BeGreen.Images.btn_gray.png");
            imgCancelados = ImageSource.FromResource("BeGreen.Images.btn_green.png");

            txtColorPendiente = Color.Black;
            txtColorRealizados = Color.Black;
            txtColorCancelados = Color.White;

            /*BackColorPendiente = Color.LightGray;
            BackColorRealizada = Color.LightGray;
            BackColorCancelada = Color.FromHex("#8bc540");*/
        }

        void AddMonth() {
            if (CurrentMonth == 12) CurrentMonth = 1;
            else CurrentMonth++;

            txtMonth = ArrayMonth[CurrentMonth];
            BindingDataOrder();
        }

        void DeleteMonth() {
            if (CurrentMonth == 1) CurrentMonth = 12;
            else CurrentMonth--;

            txtMonth = ArrayMonth[CurrentMonth];
            BindingDataOrder();
        }



        private bool CanExecuteSubmit()
        {
            return !IsBusy;
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

        private async Task GoDetail()
        {
            try
            {
                if (selectedItem == null) {
                    await Application.Current.MainPage.DisplayAlert("Error", "Por favor seleccione un elemento para ver su detalle.", "Aceptar");
                } else {
                    IsBusy = true;

                    var mdp = (Application.Current.MainPage as MasterDetailPage);
                    var navPage = mdp.Detail as NavigationPage;
                    await navPage.PushAsync(new HistoryDetailPage(selectedItem));

                    IsBusy = false;
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

                AllOrders = await App.oServiceManager.getOrders();
                BindingDataOrder();

                IsBusy = false;

            }
            finally
            {
                IsBusy = false;
            }
        }

        void BindingDataOrder()
        {
            dataOrder = new ObservableCollection<OrderDetails>();

            var orders = (from x in AllOrders
                         where x.status.Equals(ArrayStatus[CurrentStatus])
                         orderby x.created_at descending
                         select x).ToList();

            foreach (var item in orders)
            {
                dataOrder.Add(item);
            }


        }
    }
}
