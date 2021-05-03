#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Cities.Services;
using Cities.ViewModels;
using Cities.Models;
using Cities.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Cities
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CitiesList : ContentPage
    {

        public CitiesList()
        {
            InitializeComponent();
            
            this.BindingContext = new CitiesViewModel(Navigation);
        }

        private void onBackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        public async void onCityItemTapped(object sender, ItemTappedEventArgs e)
        {
            Console.WriteLine(e.Item);

            await Navigation.PushAsync(new CityPage(((CityAvatarDecorator)e.Item).City));
        }
    }
}