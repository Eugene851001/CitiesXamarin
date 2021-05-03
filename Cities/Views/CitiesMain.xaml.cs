using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cities.Models;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace Cities.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CitiesMain : Xamarin.Forms.TabbedPage
    {
        public Color Color
        {
            get => Color.FromHex(Settings.HexColor);
        }

        public CitiesMain()
        {
            InitializeComponent();
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            this.BindingContext = this;
            MessagingCenter.Subscribe<ColorPickerPage>(this, "ColorChanged", 
                (sender) => { this.BarBackgroundColor = Color.FromHex(Settings.HexColor);});
            MessagingCenter.Subscribe<LanguagePickerPage>(this, "LanguageChanged",
                (sender) => { Navigation.PopAsync(); Navigation.PushAsync(new CitiesMain()); });
        }
    }
}