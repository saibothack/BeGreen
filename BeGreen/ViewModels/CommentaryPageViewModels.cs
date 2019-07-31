using System;
using System.Threading.Tasks;
using BeGreen.Utilities;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class CommentaryPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public ImageSource imgProducBackground { get; set; }
        public ImageSource imgBackButton { get; set; }
        public AsyncCommand CommandBack { get; internal set; }
        public AsyncCommand CommandSave { get; internal set; }
        public int RowDefinitionHeader { get; set; }
        public Color loadBackColor { get; set; }

        #region "Properties"

        private string _txtCommentary;
        public string txtCommentary
        {
            get { return _txtCommentary; }
            set
            {
                SetProperty(ref _txtCommentary, value);
            }
        }

        private string _txtCommentaryError;
        public string txtCommentaryError
        {
            get { return _txtCommentaryError; }
            set
            {
                SetProperty(ref _txtCommentaryError, value);
            }
        }

        #endregion

        public CommentaryPageViewModels()
        {
            loadBackColor = Color.FromHsla(0, 0, 0, 0.1);
            RowDefinitionHeader = Device.RuntimePlatform == Device.Android ? 50 : 90;
            imgProducBackground = ImageSource.FromResource("BeGreen.Images.producto_fondo.png");
            imgBackButton = ImageSource.FromResource("BeGreen.Images.left-arrow.png");
            CommandBack = new AsyncCommand(EventBack, CanExecuteSubmit);
            CommandSave = new AsyncCommand(EventSave, CanExecuteSubmit);
        }

        private async Task EventSave() {
            if (string.IsNullOrEmpty(txtCommentary)) {
                txtCommentaryError = "Ingrese sus comentarios";
            } else {
                App.TxtComment = txtCommentary;
                var mdp = (Application.Current.MainPage as MasterDetailPage);
                var navPage = mdp.Detail as NavigationPage;
                await navPage.PopAsync();
            }
        }

        private async Task EventBack() {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;
            await navPage.PopAsync();
        }

        private bool CanExecuteSubmit()
        {
            return !IsBusy;
        }
    }
}
