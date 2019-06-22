using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeGreen.Models.Settings;
using BeGreen.Models.Terms;
using BeGreen.Utilities;
using BeGreen.Views;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class AboutUsPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public AsyncCommand CommandInitializeAsync { get; internal set; }
        public ImageSource imgLogo { get; set; }

        public Command CommandGoWeb { get; set; }
        public Command CommandGoPrivacy { get; set; }
        public Command CommandGoRefound { get; set; }
        public Command CommandGoTerms { get; set; }


        #region "Properties"

        private Terms _sourceAbout;
        public Terms sourceAbout
        {
            get { return _sourceAbout; }
            set
            {
                SetProperty(ref _sourceAbout, value);
            }
        }

        private Terms _sourceTerms;
        public Terms sourceTerms
        {
            get { return _sourceTerms; }
            set
            {
                SetProperty(ref _sourceTerms, value);
            }
        }

        private Terms _sourcePrivacy;
        public Terms sourcePrivacy
        {
            get { return _sourcePrivacy; }
            set
            {
                SetProperty(ref _sourcePrivacy, value);
            }
        }

        private Terms _sourceRefund;
        public Terms sourceRefund
        {
            get { return _sourceRefund; }
            set
            {
                SetProperty(ref _sourceRefund, value);
            }
        }

        private bool _sourceAboutVisible;
        public bool sourceAboutVisible
        {
            get { return _sourceAboutVisible; }
            set
            {
                SetProperty(ref _sourceAboutVisible, value);
            }
        }

        private bool _sourceTermsVisible;
        public bool sourceTermsVisible
        {
            get { return _sourceTermsVisible; }
            set
            {
                SetProperty(ref _sourceTermsVisible, value);
            }
        }

        private bool _sourcePrivacyVisible;
        public bool sourcePrivacyVisible
        {
            get { return _sourcePrivacyVisible; }
            set
            {
                SetProperty(ref _sourcePrivacyVisible, value);
            }
        }

        private bool _sourceRefundVisible;
        public bool sourceRefundVisible
        {
            get { return _sourceRefundVisible; }
            set
            {
                SetProperty(ref _sourceRefundVisible, value);
            }
        }

        private string _urlSite;
        public string urlSite
        {
            get { return _urlSite; }
            set
            {
                SetProperty(ref _urlSite, value);
            }
        }

        #endregion

        public AboutUsPageViewModels()
        {
            imgLogo = ImageSource.FromResource("BeGreen.Images.ic_launcher_foreground_begreen.png");

            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;

            navPage.BarBackgroundColor = Color.FromHex("#00c853");
            navPage.BarTextColor = Color.White;

            _sourceTerms = new Terms();
            CommandInitializeAsync = new AsyncCommand(InitializeAsync, CanExecuteSubmit);

            CommandGoWeb = new Command(GoWeb);
            CommandGoPrivacy = new Command(GoPrivacyAsync);
            CommandGoRefound = new Command(GoRefoundAsync);
            CommandGoTerms = new Command(GoTerms);
        }

        public void GoWeb() {
            if (!string.IsNullOrEmpty(urlSite)) {
                Device.OpenUri(new Uri(urlSite));
            }
        }

        public async void GoPrivacyAsync() {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;
            await navPage.PushAsync(new TermsPage() { Title = "" });
        }

        public async void GoRefoundAsync() {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;
            await navPage.PushAsync(new TermsPage() { Title = "" });
        }

        async void GoTerms() {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;
            await navPage.PushAsync(new TermsPage() { Title = "" });
        }

        private async Task InitializeAsync()
        {
            try
            {
                IsBusy = true;

                var sourceTermsData = await App.oServiceManager.getAllTerms(1);

                foreach (var item in sourceTermsData.pages_data) {
                    switch (item.slug)
                    {
                        case "about-us":
                            sourceAbout = item;
                            break;
                        case "term-services":
                            sourceTerms = item;
                            sourceTermsVisible = true;
                            break;
                        case "privacy-policy":
                            sourcePrivacy = item;
                            break;
                        case "refund-policy":
                            sourceRefund = item;
                            break;
                    }
                }

                List<Settings> settings = await App.DataBase.GetSettings();

                urlSite = settings[0].site_url;

                IsBusy = false;

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
