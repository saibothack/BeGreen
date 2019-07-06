using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BeGreen.Helpers;
using BeGreen.Models;
using BeGreen.Utilities;
using BeGreen.Views;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class TutorialPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }

        public AsyncCommand CommandGoHome { get; internal set; }

        public ImageSource imgBackground { get; set; }    
        public ObservableCollection<Slide> Slides { get; }

        public Color loadBackColor { get; set; }

        public TutorialPageViewModels()
        {
            imgBackground = ImageSource.FromResource("BeGreen.Images.tuto_01_0.png");
            loadBackColor = Color.FromHsla(0, 0, 0, 0.1);

            Slides = new ObservableCollection<Slide>(new[]
            {
                new Slide(ImageSource.FromResource("BeGreen.Images.tuto_01_T.png"), "Some description for slide one."),
                new Slide(ImageSource.FromResource("BeGreen.Images.tuto_1.png"), "Some description for slide two."),
                new Slide(ImageSource.FromResource("BeGreen.Images.tuto_2.png"), "Some description for slide three."),
                new Slide(ImageSource.FromResource("BeGreen.Images.tuto_3.png"), "Some description for slide three."),
                new Slide(ImageSource.FromResource("BeGreen.Images.tuto_4.png"), "Some description for slide three.")
            });

            CommandGoHome = new AsyncCommand(GoHome, CanExecuteSubmit);
        }

        private async Task GoHome() {

            await Task.Delay(100);

            Settings.isShowIntro = true;

            Application.Current.MainPage = new MasterDetailPage()
            {
                Master = new MasterPage() { Title = "Menú" },
                Detail = new NavigationPage(new HomePage())
            };

        }

        private bool CanExecuteSubmit()
        {
            return !IsBusy;
        }
    }
}
