using System;
using System.Threading;
using System.Threading.Tasks;
using BeGreen.Views;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class IntroPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public ImageSource imgLogo { get; set; }    

        public IntroPageViewModels()
        {
            imgLogo = ImageSource.FromResource("BeGreen.Images.ic_launcher_foreground_begreen.png");
            goToTutorial();
        }

        public async void goToTutorial() {
            await Task.Delay(5000);
            Application.Current.MainPage = new NavigationPage(new TutorialPage());
        }

    }
}
