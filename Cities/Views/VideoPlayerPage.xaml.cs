using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cities.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoPlayerPage : ContentPage
    {
        public string VideoUrl { get; private set; }

        public VideoPlayerPage(string videoUrl)
        {
            InitializeComponent();
            this.VideoUrl = videoUrl;
            this.BindingContext = this;
        }
    }
}