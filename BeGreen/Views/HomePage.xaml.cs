using System;
using System.Collections.Generic;
using BeGreen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeGreen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        private HomePageViewModels viewModel;

        public HomePage()
        {
            InitializeComponent();
            BindingContext = viewModel = new HomePageViewModels();
            viewModel.Navigation = this.Navigation;
        }
    }
}
