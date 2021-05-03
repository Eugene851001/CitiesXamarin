using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Firebase.Auth;
using System.ComponentModel;

using Cities.Services;

namespace Cities
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage, INotifyPropertyChanged
    {
        private string password;
        private string mail;

        public string Password 
        { 
            get 
            {
                return password;
            }
            set 
            {
                this.password = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Password)));
            } 
        }

        public string Mail 
        { 
            get 
            {
                return mail; ;
            }
            set 
            {
                this.mail = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Mail)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private FirebaseAuthProvider authProvider;

        public StartPage()
        {
            InitializeComponent();
            this.BindingContext = this;
            authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyC6TCz9Bj6UrFiXUPAldrha__JBSUr7QTo"));
           // authProvider.SignInWithEmailAndPasswordAsync("ex1@mail.ru", "pass")
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private async void onRegistrationButtonClicked(object sender, EventArgs e)
        {
            var page = new RegistrationPage();
            await Navigation.PushAsync(page);
        }

        private async void onLoginButtonClicked(object sender, EventArgs e)
        {
            try
            {
                await authProvider.SignInWithEmailAndPasswordAsync(Mail, Password);
            }
            catch (FirebaseAuthException ex)
            {
                Console.WriteLine(ex.Message);
                await DisplayAlert("Error", "Please, check email and password", "Ok");
                return;
            }

            SharedCities.CurrentMail = this.Mail;
            var page = new Views.CitiesMain();
            await Navigation.PushAsync(page);
        }
    }
}