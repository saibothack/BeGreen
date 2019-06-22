using BeGreen.Utilities;
using BeGreen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeGreen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OffersPage : ContentPage
    {
        private OffersPageViewModels viewModel;

        public OffersPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new OffersPageViewModels();
            viewModel.Navigation = this.Navigation;

            IErrorHandler errorHandler = null;
            viewModel.CommandInitializeAsync.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }
    }
}
