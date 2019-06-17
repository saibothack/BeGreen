using BeGreen.Utilities;
using BeGreen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeGreen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrchardsPage : ContentPage
    {
        private OrchardsPageViewModels viewModel;

        public OrchardsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new OrchardsPageViewModels();
            viewModel.Navigation = this.Navigation;

            IErrorHandler errorHandler = null;
            viewModel.CommandInitializeAsync.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            IErrorHandler errorHandler = null;
            viewModel.CommandItemTapped.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            IErrorHandler errorHandler = null;
            viewModel.CommandShowMenu.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }
    }
}
