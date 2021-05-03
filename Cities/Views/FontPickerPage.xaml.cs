using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Cities.Models;

namespace Cities.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FontPickerPage : ContentPage
    {
        string[] fonts = new string[]
        {
            "sans-serif-thin",
            "sans-serif-medium",
            "cursive",
            "casual",
            "serif",
        };

        public FontPickerPage()
        {
            InitializeComponent();
            BindingContext = this;

            foreach (var font in fonts)
            {
                picker.Items.Add(font);
            }
        }

        public string Font
        {
            get => Settings.Font;
        }

        public void OnPickerClick(object sender, EventArgs e)
        {
            Settings.Font = picker.Items[picker.SelectedIndex];
            Xamarin.Essentials.Preferences.Set("Font", Settings.Font);
            MessagingCenter.Send(this, "FontChanged");
        }

        public void OnApplyButtonClick(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}