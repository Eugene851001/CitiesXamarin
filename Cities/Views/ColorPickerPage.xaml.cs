using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Cities.Models;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cities.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColorPickerPage : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Color color;

        public Color EditedColor
        {
            get => this.color;
            set
            {
                this.color = value;
                NotifyPropertyChanged();
                BindingContext = this;
            }
        }

        public ColorPickerPage()
        {
            InitializeComponent();
            EditedColor = Color.FromHex(Settings.HexColor);
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void onButtonApplyClicked(object sender, EventArgs e)
        {
            Settings.HexColor = this.color.ToHex();
            Preferences.Set("Color", Settings.HexColor);
            MessagingCenter.Send(this, "ColorChanged");
            Navigation.PopModalAsync();
        }
    }
}