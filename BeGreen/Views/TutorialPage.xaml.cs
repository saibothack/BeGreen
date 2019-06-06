using System;
using System.Collections.Generic;
using BeGreen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            //Application.Current.MainPage = new NavigationPage(new HomePage());
            Application.Current.MainPage = new MasterDetailPage()
            {
                Master = new MasterPage() { Title = "Menú" },
                Detail = new NavigationPage(new HomePage())
            };
        }
    }
}
