using BeGreen.Utilities;
using System.Threading.Tasks;
using Xamarin.Essentials;
using BeGreen.Views;
using Xamarin.Forms;
using System.Linq;
using BeGreen.Services.Logic;
using BeGreen.Helpers;

namespace BeGreen.ViewModels
{
    public class RegisterPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public ImageSource imgBackground { get; set; }
        public ImageSource imgMarker { get; set; }
        public IAsyncCommand CommandRegister { get; set; }
        public IAsyncCommand CommandAddress { get; set; }
        public Command CommandCancel { get; set; }
        public Command ReturnCommandEntry { get; set; }
        public string sTitle { get; set; }
        public bool isPassword { get; set; }

        public Color loadBackColor { get; set; }

        #region "Properties"

        private string _sName;
        public string sName
        {
            get { return _sName; }
            set
            {
                SetProperty(ref _sName, value);
            }
        }

        private string _sNameError;
        public string sNameError
        {
            get { return _sNameError; }
            set
            {
                SetProperty(ref _sNameError, value);
            }
        }

        private bool _bNameError;
        public bool bNameError
        {
            get { return _bNameError; }
            set
            {
                SetProperty(ref _bNameError, value);
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

        private string _sAddressError;
        public string sAddressError
        {
            get { return _sAddressError; }
            set
            {
                SetProperty(ref _sAddressError, value);
            }
        }

        private bool _bAddressError;
        public bool bAddressError
        {
            get { return _bAddressError; }
            set
            {
                SetProperty(ref _bAddressError, value);
            }
        }

        private string _sEmail;
        public string sEmail
        {
            get { return _sEmail; }
            set
            {
                SetProperty(ref _sEmail, value);
            }
        }

        private string _sEmailError;
        public string sEmailError
        {
            get { return _sEmailError; }
            set
            {
                SetProperty(ref _sEmailError, value);
            }
        }

        private bool _bEmailError;
        public bool bEmailError
        {
            get { return _bEmailError; }
            set
            {
                SetProperty(ref _bEmailError, value);
            }
        }

        private string _sPassword;
        public string sPassword
        {
            get { return _sPassword; }
            set
            {
                SetProperty(ref _sPassword, value);
            }
        }

        private string _sPasswordError;
        public string sPasswordError
        {
            get { return _sPasswordError; }
            set
            {
                SetProperty(ref _sPasswordError, value);
            }
        }

        private bool _bPasswordError;
        public bool bPasswordError
        {
            get { return _bPasswordError; }
            set
            {
                SetProperty(ref _bPasswordError, value);
            }
        }

        private string _sPasswordConfirm;
        public string sPasswordConfirm
        {
            get { return _sPasswordConfirm; }
            set
            {
                SetProperty(ref _sPasswordConfirm, value);
            }
        }

        private string _sPasswordConfirmError;
        public string sPasswordConfirmError
        {
            get { return _sPasswordConfirmError; }
            set
            {
                SetProperty(ref _sPasswordConfirmError, value);
            }
        }

        private bool _bPasswordConfirmError;
        public bool bPasswordConfirmError
        {
            get { return _bPasswordConfirmError; }
            set
            {
                SetProperty(ref _bPasswordConfirmError, value);
            }
        }

        #endregion

        public RegisterPageViewModels()
        {
            loadBackColor = Color.FromHsla(0, 0, 0, 0.1);
            imgBackground = ImageSource.FromResource("BeGreen.Images.login_background.png");
            imgMarker = ImageSource.FromResource("BeGreen.Images.ic_marker.png");

            CommandAddress = new AsyncCommand(Address, CanExecuteSubmit);
            CommandRegister = new AsyncCommand(Register, CanExecuteSubmit);
            CommandCancel = new Command(Cancel);

            sTitle = "Crear perfil";
            isPassword = true;

            if (Settings.isLogin) {
                sTitle = "Mi perfil";
                isPassword = false;
                sName = Settings.UserName;
                sEmail = Settings.UserEmail;
                sAddress = Settings.UserAddress;
            }

            ReturnCommandEntry = new Command<View>((view) =>
            {
                view?.Focus();
            });
        }

        private async Task Address()
        {
            try
            {
                IsBusy = true;
                Location location = await App.ObtieneUbicacionAsync();

                var placemarks = await Geocoding.GetPlacemarksAsync(location);

                var placemark = placemarks?.FirstOrDefault();

                if (placemark != null)
                {
                    var geocodeAddress =
                        placemark.Thoroughfare + " " +
                        placemark.PostalCode + ", " +
                        placemark.Locality + ", " +
                        placemark.CountryName;
                        
                    sAddress = geocodeAddress;
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

        private async Task Register()
        {
            try
            {
                if (validate())
                {
                    IsBusy = true;

                    if (Settings.isLogin) {
                        Settings.UserName = sName;
                        Settings.UserEmail = sEmail;
                        Settings.UserAddress = sAddress;

                        Application.Current.MainPage = new MasterDetailPage()
                        {
                            Master = new MasterPage() { Title = "Menú" },
                            Detail = new NavigationPage(new HomePage())
                        };

                    } else {
                        ServicesUser servicesUser = new ServicesUser();
                        await servicesUser.RegisterAsync(sName, sAddress, sEmail, sPassword);
                    }
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        bool validate() {
            bool success = true;

            sNameError = "Ingrese su nombre";
            sAddressError = "Ingrese su dirección";
            sEmailError = "Ingrese su correo";
            sPasswordError = "Ingrese su contraseña";

            if (string.IsNullOrEmpty(sName))
            {
                success = false;
                bNameError = true;

            } else {
                bNameError = false;
            }

            if (string.IsNullOrEmpty(sAddress))
            {
                success = false;
                bAddressError = true;

            }
            else
            {
                bAddressError = false;
            }

            if (string.IsNullOrEmpty(sEmail))
            {
                sEmailError = "Ingrese su correo";
                success = false;
                bEmailError = true;

            }
            else
            {
                if (!Util.IsEmail(sEmail))
                {
                    sEmailError = "El formato de correo no es correcto";
                    success = false;
                    bEmailError = true;
                }
                else
                {
                    bEmailError = false;
                }
            }

            if (!Settings.isLogin) {
                if (string.IsNullOrEmpty(sPassword))
                {
                    success = false;
                    bPasswordError = true;

                }
                else
                {
                    bPasswordError = false;
                }

                if (string.IsNullOrEmpty(sPasswordConfirm))
                {
                    success = false;
                    bPasswordConfirmError = true;

                }
                else
                {
                    if (!sPassword.Equals(sPasswordConfirm))
                    {
                        success = false;
                        bPasswordConfirmError = true;
                        sPasswordConfirmError = "Sus contraseñas no son iguales";
                    }
                    else
                    {
                        bPasswordConfirmError = false;
                    }
                }
            }

            return success;
        }

        void Cancel() {
            IsBusy = true;

            if (Settings.isLogin) {
                Application.Current.MainPage = new MasterDetailPage()
                {
                    Master = new MasterPage() { Title = "Menú" },
                    Detail = new NavigationPage(new HomePage())
                };
            } else {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }
    }
}
