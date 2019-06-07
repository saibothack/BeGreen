using System;
using System.Threading.Tasks;
using BeGreen.Utilities;
using BeGreen.Views;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class LoginPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public ImageSource imgBackground { get; set; }
        public IAsyncCommand CommandLogin { get; set; }
        public Command CommandRegister { get; set; }
        public Command ReturnCommandEntry { get; set; }
        public Command CommandCancel { get; set; }


        #region "Properties"

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

        #endregion

        public LoginPageViewModels()
        {
            imgBackground = ImageSource.FromResource("BeGreen.Images.login_background.png");

            CommandLogin = new AsyncCommand(Login, CanExecuteSubmit);
            CommandRegister = new Command(Register);
            CommandCancel = new Command(Cancel);

            ReturnCommandEntry = new Command<View>((view) =>
            {
                view?.Focus();
            });
        }

        void Cancel()
        {
            Application.Current.MainPage = new MasterDetailPage()
            {
                Master = new MasterPage() { Title = "Menú" },
                Detail = new NavigationPage(new HomePage())
            };
        }

        private async Task Login()
        {
            try
            {
                if (validate()) {
                    IsBusy = true;

                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        void Register()
        {
            Application.Current.MainPage = new NavigationPage(new RegisterPage());
        }

        private bool CanExecuteSubmit()
        {
            return !IsBusy;
        }

        private bool validate() {
            bool success = true;

            sEmailError = "Ingrese su usuario";
            sPasswordError = "Ingrese su contraseña";

            if (string.IsNullOrEmpty(sEmail)) {
                success = false;
                bEmailError = true;
            } else {
                bEmailError = false;
            }

            if (string.IsNullOrEmpty(sPassword)) {
                success = false;
                bPasswordError = true;
            } else {
                bPasswordError = false;
            }

            return success;
        }
    }
}
