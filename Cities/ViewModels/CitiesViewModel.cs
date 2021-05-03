using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Cities.Models;
using Cities.Services;
using Xamarin.Forms;
using Cities.Views;

namespace Cities.ViewModels
{
    public class CitiesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand FilterCommand { get; protected set; }
        public INavigation Navigation;

        private IList<CityAvatarDecorator> cities;

        private City currentCity;

        public CitiesViewModel(INavigation navigation)
        {
            this.cities = new ObservableCollection<CityAvatarDecorator>();
            this.Navigation = navigation;
            this.FilterCommand = new Command(OpenAndUseFilter);

            MessagingCenter.Subscribe<FilterPage>(this, "FilterChanged", 
                (sender) => 
                { 
                    FetchCities();  
                    UseFilter();
                });

            MessagingCenter.Subscribe<CityViewModel>(this, "CityUpdate", (sender) => { FetchCities(); UseFilter(); });

            Console.WriteLine("In Cities");

            FetchCities();
            UseFilter();

            foreach (var city in cities)
            {
                if (city.Mail == SharedCities.CurrentMail)
                {
                    currentCity = city.City;
                }
            }
        }

        public string CurrentName
        {
            get => currentCity is null ? "" : currentCity.Name;
        }



        public IList<CityAvatarDecorator> Cities
        {
            get => this.cities;
            set
            {
                this.cities = value;
                NotifyPropertyChanged();
            }
        }

        public double TextSize
        {
            get => Settings.TextSize;
        }

        private void FetchCities()
        {
            IFirebaseDB db = DependencyService.Get<IFirebaseDB>();
            IEnumerable<City> cities = db.GetCities();
            this.cities = new ObservableCollection<CityAvatarDecorator>(cities.Select((city) => new CityAvatarDecorator(city)));
            SharedCities.cities = new List<City>(cities);
        }

        private async void OpenAndUseFilter()
        {
            var page = new FilterPage();
            await this.Navigation.PushModalAsync(new FilterPage());
        }

        private void OnFilterPageClose(object sender, EventArgs e) => UseFilter();

        private void UseFilter() =>
            this.Cities = new ObservableCollection<CityAvatarDecorator>(this.cities.Where((item) => Filter(item)));

        private static bool Filter(CityAvatarDecorator item)
        {
            if (FilterSettings.Name != null && item.Name != FilterSettings.Name)
            {
                return false;
            }

            if (FilterSettings.Country != null && item.Country != FilterSettings.Country)
            {
                return false;
            }

            if (FilterSettings.MinYear != null && item.Year < FilterSettings.MinYear)
            {
                return false;
            }

            if (FilterSettings.MaxYear != null && item.Year > FilterSettings.MaxYear)
            {
                return false;
            }

            if (FilterSettings.MinPopulation != null && item.Population < FilterSettings.MinPopulation)
            {
                return false;
            }

            if (FilterSettings.MaxPopulation != null && item.Population > FilterSettings.MaxPopulation)
            {
                return false;
            }

            return FilterSettings.Capital ? item.Capital : true;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
