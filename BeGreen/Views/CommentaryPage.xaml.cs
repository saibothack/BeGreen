using System;
using System.Collections.Generic;
using BeGreen.Utilities;
using BeGreen.ViewModels;
using Xamarin.Forms;

namespace BeGreen.Views
{
    public partial class CommentaryPage : ContentPage
    {
        private CommentaryPageViewModels viewModel;

        public CommentaryPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new CommentaryPageViewModels();
            viewModel.Navigation = this.Navigation;
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            IErrorHandler errorHandler = null;
            viewModel.CommandBack.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }

        void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            IErrorHandler errorHandler = null;
            viewModel.CommandSave.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }
    }
}
