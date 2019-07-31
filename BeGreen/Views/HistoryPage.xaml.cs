using BeGreen.Utilities;
using BeGreen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeGreen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        private HistoryPageViewModels viewModel;

        public HistoryPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new HistoryPageViewModels();
            viewModel.Navigation = this.Navigation;

            IErrorHandler errorHandler = null;
            viewModel.CommandInitialize.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            IErrorHandler errorHandler = null;
            viewModel.CommandDetail.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            IErrorHandler errorHandler = null;
            viewModel.CommandBack.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.selectedItem = null;
        }
    }
}