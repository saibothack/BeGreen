using System;
using System.Collections.Generic;
using BeGreen.Utilities;
using BeGreen.ViewModels.Popup;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeGreen.Views.popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductCommentPage 
    {
        private ProductCommentPageViewModels viewModel;

        public ProductCommentPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ProductCommentPageViewModels();
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
