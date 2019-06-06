using System;
using System.Collections.ObjectModel;
using BeGreen.Models;
using Xamarin.Forms;

namespace BeGreen.ViewModels
{
    public class TutorialPageViewModels : ViewModelBase
    {
        public INavigation Navigation { get; internal set; }
        public ImageSource imgBackground { get; set; }    
        public ObservableCollection<Slide> Slides { get; }

        public TutorialPageViewModels()
        {

            imgBackground = ImageSource.FromResource("BeGreen.Images.tuto_01_0.png");

            Slides = new ObservableCollection<Slide>(new[]
            {
                new Slide(ImageSource.FromResource("BeGreen.Images.tuto_01_T.png"), "Some description for slide one."),
                new Slide(ImageSource.FromResource("BeGreen.Images.tuto_1.png"), "Some description for slide two."),
                new Slide(ImageSource.FromResource("BeGreen.Images.tuto_2.png"), "Some description for slide three."),
                new Slide(ImageSource.FromResource("BeGreen.Images.tuto_3.png"), "Some description for slide three."),
                new Slide(ImageSource.FromResource("BeGreen.Images.tuto_4.png"), "Some description for slide three.")
            });
        }
    }
}
