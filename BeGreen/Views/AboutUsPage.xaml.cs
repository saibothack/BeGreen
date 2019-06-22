using BeGreen.Utilities;
using BeGreen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeGreen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutUsPage : ContentPage
    {
        private AboutUsPageViewModels viewModel;

        public AboutUsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new AboutUsPageViewModels();
            viewModel.Navigation = this.Navigation;

            IErrorHandler errorHandler = null;
            viewModel.CommandInitializeAsync.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }
    }
}
