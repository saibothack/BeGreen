using System;
using System.Threading.Tasks;
using BeGreen.Utilities;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class CategoryPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public AsyncCommand CommandInit { get; internal set; }

        public CategoryPageViewModels()
        {
            CommandInit = new AsyncCommand(InitializeAsync, CanExecuteSubmit);

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
