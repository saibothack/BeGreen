using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BeGreen.Models.Orchard;
using BeGreen.Utilities;
using BeGreen.Views;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class OrchardsPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public AsyncCommand CommandInitializeAsync { get; internal set; }
        public AsyncCommand CommandItemTapped { get; set; }
        public AsyncCommand CommandShowMenu { get; set; }

        public ImageSource imgBackground { get; set; }
        public ImageSource imgNavigation { get; set; }

        public Color colorBackgroundLoading { get; set; }


        #region "Properties"

        private ObservableCollection<Orchard> _sourceOrchards;
        public ObservableCollection<Orchard> sourceOrchards
        {
            get { return _sourceOrchards; }
            set
            {
                SetProperty(ref _sourceOrchards, value);
            }
        }

        private bool _isVisibleSourceOrchard;
        public bool isVisibleSourceOrchard
        {
            get { return _isVisibleSourceOrchard; }
            set
            {
                SetProperty(ref _isVisibleSourceOrchard, value);
            }
        }

        private Orchard _ItemSelectedOrchard;
        public Orchard ItemSelectedOrchard
        {
            get { return _ItemSelectedOrchard; }
            set
            {
                SetProperty(ref _ItemSelectedOrchard, value);
            }
        }

        #endregion

        public OrchardsPageViewModels()
        {
            imgBackground = ImageSource.FromResource("BeGreen.Images.catalog.png");
            imgNavigation = ImageSource.FromResource("BeGreen.Images.nav_perfil_min.png");

            colorBackgroundLoading = Color.FromHsla(0, 0, 0, 0.1);

            sourceOrchards = new ObservableCollection<Orchard>();

            CommandInitializeAsync = new AsyncCommand(InitializeAsync, CanExecuteSubmit);
            CommandItemTapped = new AsyncCommand(ItemTapped, CanExecuteSubmit);
            CommandShowMenu = new AsyncCommand(showMenu, CanExecuteSubmit);


        }

        private async Task ItemTapped() {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;
            await navPage.PushAsync(new OrchardDetailPage(ItemSelectedOrchard));
        }

        private async Task InitializeAsync() {
            try
            {
                IsBusy = true;

                var getDataOrchards = await App.oServiceManager.getAllOrchards(1, 0);

                foreach (var item in getDataOrchards.news_data)
                {
                    item.news_name = item.news_name.ToUpper();
                    item.news_image = (Constants.urlApi + item.news_image);
                    sourceOrchards.Add(item);
                }

                if (sourceOrchards.Count > 0)
                    isVisibleSourceOrchard = true;
            }
            finally
            {
                IsBusy = false;
            }
        }

        #pragma warning disable CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica
        async Task showMenu()
        #pragma warning restore CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica
        {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;
            mdp.IsPresented = true;
        }

        private bool CanExecuteSubmit()
        {
            return !IsBusy;
        }
    }
}
