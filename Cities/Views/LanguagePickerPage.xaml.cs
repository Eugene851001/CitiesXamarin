using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Cities.Utils;
using Cities.Models;
using Xamarin.Essentials;

namespace Cities.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LanguagePickerPage : ContentPage
    {
        Dictionary<string, string> languages = new Dictionary<string, string>()
        {
            ["ru"] = "ru-RU",
            ["en"] = "en-US",
        };

        public LanguagePickerPage()
        {
            InitializeComponent();
            BindingContext = this;

            foreach (var lang in languages.Keys)
            {
                langPicker.Items.Add(lang);
            }
        }

        public IEnumerable<string> Languages 
        {
            get => languages.Keys;
        }


        public string Language
        {
            get => Settings.Language;
        }

        public void OnPickerClick(object sender, EventArgs e)
        {
            Settings.Language = languages[langPicker.Items[langPicker.SelectedIndex]];
            Preferences.Set("Lang", Settings.Language);
            Console.WriteLine("Language changed");
            MessagingCenter.Send(this, "LanguageChanged");
        }

        public void OnApplyButtonClick(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}