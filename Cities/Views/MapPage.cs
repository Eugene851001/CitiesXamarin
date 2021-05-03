using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Cities.Models;
using Cities.Services;
using Xamarin.Forms.GoogleMaps;

namespace Cities.Views
{
    public class MapPage : ContentPage
    {
        public MapPage()
        {
            var cities = SharedCities.cities;

            var map = new Map();
            map.VerticalOptions = LayoutOptions.FillAndExpand;
            map.HorizontalOptions = LayoutOptions.FillAndExpand;

            map.InfoWindowClicked += InfoWindowClicked;
            Content = new StackLayout
            {
                Children = {
                    map,
                }
            };

            if (cities is null)
            {
                return;
            }

            if (cities.Count > 0)
            {
                map.MoveToRegion(MapSpan.FromCenterAndRadius
                    (new Position
                        (cities[0].Latitude, 
                         cities[0].Longitude), 
                         Distance.FromKilometers(100)));
            }

            foreach (var city in cities)
            {
                var pin = new Pin()
                {
                    Label = city.Name,
                    Address = city.Country,
                    Position = new Position(city.Latitude, city.Longitude)
                };

                map.Pins.Add(pin);
            }
        }

        void InfoWindowClicked(object sender, Xamarin.Forms.GoogleMaps.InfoWindowClickedEventArgs e)
        {
            foreach (var city in SharedCities.cities)
            {
                if (city.Latitude == e.Pin.Position.Latitude &&
                    city.Longitude == e.Pin.Position.Longitude)
                {
                    Navigation.PushAsync(new CityPage(city));
                    return;
                }
            }

        }
    }
}