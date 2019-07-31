using System;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace BeGreen.ViewModels.Popup
{
    public class CancelErrorPageViewModels : ViewModelBase
    {
        public Command CommandConfirm { get; set; }

        [Obsolete]
        public CancelErrorPageViewModels()
        {
            CommandConfirm = new Command(Confirm);
        }

        [Obsolete]
        async void Confirm() {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;
            await navPage.PopAsync();
            
            await PopupNavigation.PopAllAsync();
        }
    }
}
