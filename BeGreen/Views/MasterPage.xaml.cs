using System;
using System.Collections.Generic;
using BeGreen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeGreen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : ContentPage
    {
        private MasterPageViewModel viewModel;

        public MasterPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new MasterPageViewModel();
        }
    }
}
