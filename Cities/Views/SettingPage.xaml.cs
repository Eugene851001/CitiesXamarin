using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Amporis.Xamarin.Forms.ColorPicker;

namespace Cities.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : ContentPage
    {
        public SettingPage()
        {
            InitializeComponent();
        }

        public void onColorButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ColorPickerPage());
        }

        public void onLanguageButtonClicked(object sender, EventArgs e)
        {
            var langPicker = new LanguagePickerPage();
            Navigation.PushModalAsync(langPicker);
        }

        public void onSizeButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SizePickerPage());
        }

        public void onFontButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new FontPickerPage());
        }
    }
}