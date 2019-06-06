using Xamarin.Forms;

namespace BeGreen.Models
{
    public class Slide
    {
        public Slide(ImageSource image, string description)
        {
            Image = image;
            Description = description;
        }

        public ImageSource Image { get; set; }

        public string Description { get; set; }
    }
}
