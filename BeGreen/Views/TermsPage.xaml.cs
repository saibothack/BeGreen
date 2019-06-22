using BeGreen.Utilities;
using BeGreen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeGreen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermsPage : ContentPage
    {
        private TermsPageViewModels viewModel;

        public TermsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new TermsPageViewModels();
            viewModel.Navigation = this.Navigation;

            IErrorHandler errorHandler = null;
            viewModel.CommandInitializeAsync.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }
    }
}
