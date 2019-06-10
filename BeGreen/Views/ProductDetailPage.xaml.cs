using System;
using System.Collections.Generic;
using BeGreen.Models.Product;
using BeGreen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeGreen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailPage : ContentPage
    {
        private ProductDetailPageViewModels viewModel;

        public ProductDetailPage(Product product)
        {
            InitializeComponent();

            BindingContext = viewModel = new ProductDetailPageViewModels();
            viewModel.Navigation = this.Navigation;
            viewModel.ProductSelected = product;
        }
    }
}
