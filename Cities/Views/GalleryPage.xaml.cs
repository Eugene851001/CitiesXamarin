using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Cities.Models;

namespace Cities.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GalleryPage : ContentPage
    {
        public IList<ImageWrapper> Images { get; private set; }

        public GalleryPage(City city)
        {
            Images = new List<ImageWrapper>();
            foreach (string url in city.Images)
            {
                Images.Add(new ImageWrapper(url));
            }

            InitializeComponent();
            this.BindingContext = this;
        }

        public void OnImageClick(object sender, EventArgs e)
        {
            ImageSource  source = ((ImageButton)sender).Source; ;
            Navigation.PushAsync(new ImagePage(source));
        }

        public class ImageWrapper
        {
            public string ImageUrl { get; private set; }

            public ImageWrapper(string url)
            {
                this.ImageUrl = url;
            }
        }
    }
}