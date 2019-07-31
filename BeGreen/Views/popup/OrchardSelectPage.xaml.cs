﻿using System;
using System.Collections.Generic;
using BeGreen.Utilities;
using BeGreen.ViewModels.Popup;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeGreen.Views.popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrchardSelectPage
    {
        private OrchardSelectPageViewModels viewModel;

        [Obsolete]
        public OrchardSelectPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new OrchardSelectPageViewModels();
            viewModel.Navigation = this.Navigation;

            IErrorHandler errorHandler = null;
            viewModel.CommandInit.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            IErrorHandler errorHandler = null;
            viewModel.CommandSelectedOrchard.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            IErrorHandler errorHandler = null;
            viewModel.CommandBack.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }
    }
}
