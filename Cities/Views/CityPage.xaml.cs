#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Cities.Models;
using Cities.Services;
using Cities.Utils;
using System.IO;
using Cities.ViewModels;

namespace Cities.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CityPage : ContentPage
    {
        private City city;

        public CityPage(City city)
        {
            InitializeComponent();

            this.city = city;
            this.BindingContext = new CityViewModel(city, this);
        }

        public void OnImageClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GalleryPage(city));
        }

        public void OnMapClick(object sender, EventArgs e)
        {
            SharedCities.cities = new List<City>() { this.city };
            Navigation.PushAsync(new MapPage());
        }
    }
}