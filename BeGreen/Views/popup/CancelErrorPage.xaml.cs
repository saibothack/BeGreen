using System;
using System.Collections.Generic;
using BeGreen.ViewModels.Popup;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeGreen.Views.popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CancelErrorPage
    {
        private CancelErrorPageViewModels viewModel;

        [Obsolete]
        public CancelErrorPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new CancelErrorPageViewModels();
        }
    }
}
