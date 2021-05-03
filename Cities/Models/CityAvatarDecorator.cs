using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Cities.Models
{
    public class CityAvatarDecorator
    {
        private readonly City city;

        public CityAvatarDecorator(City city)
        {
            this.city = city;

            Uri uri = city.Images.Count > 0 ? new Uri(city.Images[0]) : null;
            string noImg = "https://www.reallycheapfloors.com/wp-content/uploads/2018/11/x2017-05-15_18.png.pagespeed.ic_.tLD9q0ZZph.png";
            this.Avatar = uri is null ? ImageSource.FromUri(new Uri(noImg)) : ImageSource.FromUri(uri);
        }

        public string Mail
        {
            get => city.Mail;
        }


        public string Name
        {
            get => city.Name;
            set => city.Name = value;
        }

        public string Country
        {
            get => city.Country;
            set => city.Country = value;
        }

        public bool Capital
        {
            get => city.Capital;
            set => city.Capital = value;
        }

        public int Year
        {
            get => city.Year;
            set => city.Year = value;
        }

        public int Population
        {
            get => city.Population;
            set => city.Population = value;
        }


        public City City
        {
            get => city;
        }

        public ImageSource Avatar
        {
            get; private set;
        }
    }
}
