using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BeGreen.Models.Orchard;
using BeGreen.Utilities;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class SelectOrchardPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public ImageSource imgProducBackground { get; set; }
        public ImageSource imgBackButton { get; set; }
        public AsyncCommand CommandInit { get; internal set; }
        public AsyncCommand CommandSelectedOrchard { get; internal set; }
        public AsyncCommand CommandBack { get; internal set; }
        public int RowDefinitionHeader { get; set; }
        public Color loadBackColor { get; set; }

        private ObservableCollection<Orchard> _dataOrchards;
        public ObservableCollection<Orchard> dataOrchards
        {
            get { return _dataOrchards; }
            set
            {
                SetProperty(ref _dataOrchards, value);
            }
        }

        private Orchard _orchardSelected;
        public Orchard orchardSelected
        {
            get { return _orchardSelected; }
            set
            {
                SetProperty(ref _orchardSelected, value);
            }
        }

        public SelectOrchardPageViewModels()
        {
            loadBackColor = Color.FromHsla(0, 0, 0, 0.1);
            RowDefinitionHeader = Device.RuntimePlatform == Device.Android ? 50 : 80;
            dataOrchards = new ObservableCollection<Orchard>();
            imgProducBackground = ImageSource.FromResource("BeGreen.Images.producto_fondo.png");
            imgBackButton = ImageSource.FromResource("BeGreen.Images.left-arrow.png");
            CommandInit = new AsyncCommand(InitPage, CanExecuteSubmit);
            CommandSelectedOrchard = new AsyncCommand(SelectItemOrchard, CanExecuteSubmit);
            CommandBack = new AsyncCommand(EventBack, CanExecuteSubmit);
        }

        private async Task EventBack()
        {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;
            await navPage.PopAsync();
        }

        private async Task SelectItemOrchard() {

            bool answer = await Application.Current.MainPage.DisplayAlert("Notificación", "¿Deseas que tu producto sea de: " + orchardSelected .news_name + "?", "Si", "No");

            if (answer) {
                App.ItemSelectedOrchard = orchardSelected;
                var mdp = (Application.Current.MainPage as MasterDetailPage);
                var navPage = mdp.Detail as NavigationPage;
                await navPage.PopAsync();
            }  else
            {
                orchardSelected = null;
            }
        }

        private async Task InitPage() {
            try
            {
                IsBusy = true;

                var getDataOrchards = await App.oServiceManager.getAllOrchards(1, 0);

                foreach (var item in getDataOrchards.news_data)
                {
                    item.news_image = (Constants.urlApi + item.news_image);
                    dataOrchards.Add(item);
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
