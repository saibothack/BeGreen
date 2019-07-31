using System.Threading.Tasks;
using BeGreen.Models.Order;
using BeGreen.Utilities;
using BeGreen.ViewModels;
using BeGreen.Views.popup;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeGreen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryDetailPage : ContentPage
    {
        private HistoryDetailPageViewModel viewModel;

        [System.Obsolete]
        public HistoryDetailPage(OrderDetails orderDetail)
        {
            InitializeComponent();

            BindingContext = viewModel = new HistoryDetailPageViewModel(orderDetail);
            viewModel.Navigation = this.Navigation;
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            IErrorHandler errorHandler = null;
            viewModel.CommandBack.ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }
            
        [System.Obsolete]
        async void Handle_Clicked1(object sender, System.EventArgs e)
        {
            await PopupNavigation.PushAsync(new CancelPage(viewModel.orderDetail));
        }

    }
}
