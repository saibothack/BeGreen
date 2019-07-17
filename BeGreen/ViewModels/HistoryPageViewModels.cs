using BeGreen.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class HistoryPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public AsyncCommand CommandInitialize { get; internal set; }
        public Color loadBackColor { get; set; }
        public int RowDefinitionHeader { get; set; }


        public HistoryPageViewModels()
        {
            RowDefinitionHeader = Device.RuntimePlatform == Device.Android ? 50 : 80;
            CommandInitialize = new AsyncCommand(InitializeAsync, CanExecuteSubmit);
            loadBackColor = Color.FromHsla(0, 0, 0, 0.1);
        }

        private bool CanExecuteSubmit()
        {
            return !IsBusy;
        }

        private async Task InitializeAsync()
        {
            try
            {
                IsBusy = true;

                

                IsBusy = false;

            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
