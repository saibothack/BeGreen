using System;
using System.Threading.Tasks;
using BeGreen.Models.Terms;
using BeGreen.Utilities;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class TermsPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public AsyncCommand CommandInitializeAsync { get; internal set; }

        private Terms _sourceTerms;
        public Terms sourceTerms
        {
            get { return _sourceTerms; }
            set
            {
                SetProperty(ref _sourceTerms, value);
            }
        }

        public TermsPageViewModels()
        {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;

            navPage.BarBackgroundColor = Color.FromHex("#00c853");
            navPage.BarTextColor = Color.White;

            _sourceTerms = new Terms();
            CommandInitializeAsync = new AsyncCommand(InitializeAsync, CanExecuteSubmit);
        }

        private async Task InitializeAsync()
        {
            try
            {
                IsBusy = true;

                var sourceTermsData = await App.oServiceManager.getAllTerms(1);

                sourceTerms = sourceTermsData.pages_data[0];

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
