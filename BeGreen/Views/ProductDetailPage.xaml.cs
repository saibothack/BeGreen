using BeGreen.Models.Product;
using BeGreen.Utilities;
using BeGreen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeGreen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailPage : ContentPage
    {
        private ProductDetailPageViewModels viewModel;

        [System.Obsolete]
        public ProductDetailPage(Product product)
        {
            InitializeComponent();

            BindingContext = viewModel = new ProductDetailPageViewModels();
            viewModel.Navigation = this.Navigation;
            viewModel.ProductSelected = product;
            //viewModel.initPageAsync();

            IErrorHandler errorHandler = null;
            viewModel.CommandInitialize.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }

        void Handle_Clicked_Comments(object sender, System.EventArgs e)
        {
            IErrorHandler errorHandler = null;
            viewModel.CommandComments.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }

        void Handle_Clicked_Orchard(object sender, System.EventArgs e)
        {
            IErrorHandler errorHandler = null;
            viewModel.CommandSelectOrchard.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!string.IsNullOrEmpty(App.TxtComment)) {
                viewModel.btnCommentBackground = Color.FromHex("#8bc540");
            }

            if (App.ItemSelectedOrchard.news_id != null) {
                viewModel.btnOrchardBackground = Color.FromHex("#8bc540");
            }
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            IErrorHandler errorHandler = null;
            viewModel.CommandAddToCar.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }
    }
}
