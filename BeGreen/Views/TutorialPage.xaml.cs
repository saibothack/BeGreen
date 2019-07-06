using System;
using System.Collections.Generic;
using BeGreen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BeGreen.Helpers;
using System.Threading.Tasks;
using BeGreen.Utilities;

namespace BeGreen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutorialPage : ContentPage
    {
        private TutorialPageViewModels viewModel;

        public TutorialPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new TutorialPageViewModels();
            viewModel.Navigation = this.Navigation;
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            layIndicator.IsVisible = true;
            indLoading.IsVisible = true;
            indLoading.IsRunning = true;

            IErrorHandler errorHandler = null;
            viewModel.CommandGoHome.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }
    }
}
