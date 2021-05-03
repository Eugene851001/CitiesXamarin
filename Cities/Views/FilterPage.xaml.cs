#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Cities.Models;
using Cities.Utils;

namespace Cities.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterPage : ContentPage
    {
        public string Name { get; set; } = "";
        public string Country { get; set; } = "";
        public string MaxYear { get; set; } = "";
        public string MinYear { get; set; } = "";
        public string MaxPopulation { get; set; } = "";
        public string MinPopulation { get; set; } = "";
        public bool Capital { get; set; }

        public FilterPage()
        {
            this.Name = FilterSettings.Name ?? "";
            this.Country = FilterSettings.Country ?? "";
            this.MaxYear = FilterSettings.MaxYear?.ToString() ?? "";
            this.MinYear = FilterSettings.MinYear?.ToString() ?? "";
            this.MaxPopulation = FilterSettings.MaxPopulation.ToString() ?? "";
            this.MinPopulation = FilterSettings.MinPopulation.ToString() ?? "";
            this.Capital = FilterSettings.Capital;

            InitializeComponent();

            this.BindingContext = this;
        }

        public void onSetButtonClick(object sender, EventArgs e)
        {

            FilterSettings.Name = this.Name.Length > 0 ? this.Name : null;
            FilterSettings.Country = this.Country.Length > 0 ? this.Country : null;
            FilterSettings.Capital = this.Capital;

            string? error = null;
            if (MinYear.Length == 0)
            {
                FilterSettings.MinYear = null;
            }
            else
            {
                error = Validator.ValidYear(MinYear);
                if (error != null)
                {
                    DisplayAlert("Error", error, "OK");
                    return;
                }

                FilterSettings.MinYear = int.Parse(MinYear);
            }


            if (MaxYear.Length == 0)
            {
                FilterSettings.MaxYear = null;
            }
            else
            {
                error = Validator.ValidYear(MaxYear);
                if (error != null)
                {
                    DisplayAlert("Error", error, "OK");
                    return;
                }

                FilterSettings.MaxYear = int.Parse(MaxYear);
            }

            if (MinPopulation.Length == 0)
            {
                FilterSettings.MinPopulation = null;
            }
            else
            {
                error = Validator.ValidPopulation(MinPopulation);
                if (error != null)
                {
                    DisplayAlert("Error", error, "OK");
                    return;
                }

                FilterSettings.MinPopulation = int.Parse(MinPopulation);
            }

            if (MaxPopulation.Length == 0)
            {
                FilterSettings.MaxPopulation = null;
            }
            else
            {
                error = Validator.ValidPopulation(MaxPopulation);
                if (error != null)
                {
                    DisplayAlert("Error", error, "OK");
                    return;
                }

                FilterSettings.MaxPopulation = int.Parse(MaxPopulation);
            }

            MessagingCenter.Send<FilterPage>(this, "FilterChanged");
            Navigation.PopModalAsync();
        }

        public void onResetButtonClick(object sender, EventArgs e)
        {
            FilterSettings.Name = FilterSettings.Country = null;
            FilterSettings.MaxPopulation = FilterSettings.MinPopulation 
                = FilterSettings.MinYear = FilterSettings.MaxYear = null;
            FilterSettings.Capital = false;
            MessagingCenter.Send<FilterPage>(this, "FilterChanged");
            Navigation.PopModalAsync();
        }
    }
}