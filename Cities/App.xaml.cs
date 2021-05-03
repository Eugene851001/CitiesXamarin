using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using Cities.Models;

namespace Cities
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Settings.HexColor = Preferences.Get("Color", "FF00FF00");
            Settings.Language = Preferences.Get("Lang", "en-US");
            Settings.Font = Preferences.Get("Font", "serif");
       //     Settings.TextSize = Preferences.Get("Size", 15);
            
            this.MainPage = new NavigationPage(new StartPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
