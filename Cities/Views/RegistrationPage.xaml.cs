using Cities.Models;
using Cities.Services;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Cities.Utils;

namespace Cities
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public bool Capital { get; set; }
        public string Year { get; set; }
        public string Population { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public RegistrationPage()
        {
            InitializeComponent();
            this.BindingContext = this;
        }

        private void onBackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void onRegistrateButtonClicked(object sender, EventArgs e)
        {
            string err = Validator.ValidYear(Year);

            if (err != null)
            {
                DisplayAlert("Error", err, "OK");
                return;
            }

            err = Validator.ValidPopulation(Population);

            if (err != null)
            {
                DisplayAlert("Error", err, "OK");
                return;
            }


            err = Validator.ValidLatitude(Latitude);

            if (err != null)
            {
                DisplayAlert("Error", err, "OK");
                return;
            }


            err = Validator.ValidLongitude(Longitude);

            if (err != null)
            {
                DisplayAlert("Error", err, "OK");
                return;
            }

            IFirebaseDB db = DependencyService.Get<IFirebaseDB>();
            var city = new City()
            {
                Name = this.Name,
                Country = this.Country,
                Mail = this.Mail,
                Capital = this.Capital,
                Population = int.Parse(this.Population),
                Year = int.Parse(this.Year),
                Latitude = double.Parse(this.Latitude),
                Longitude = double.Parse(this.Longitude)
            };

            db.AddCity(city);

            var auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyC6TCz9Bj6UrFiXUPAldrha__JBSUr7QTo"));
            var task = auth.CreateUserWithEmailAndPasswordAsync(this.Mail, this.Password);
            task.ContinueWith((t) => 
            {
                if (t.Exception != null)
                {
                    Console.WriteLine(t.Exception.Message);
                    DisplayAlert("Error", "Can not registrate user with this mail and password", "OK");
                    return;
                }

                Console.WriteLine(t.Result);
            });
        }
    }
}