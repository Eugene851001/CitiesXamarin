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
    public partial class SizePickerPage : ContentPage
    {
        double[] sizes = new double[]
        {
            10.0,
            15.0,
            20.0,
            25.0,
        };

        public SizePickerPage()
        {
            InitializeComponent();
            BindingContext = this;

            picker.Title = Settings.TextSize.ToString();

            foreach (var size in sizes)
            {
                picker.Items.Add(size.ToString());
            }
        }

        public void OnPickerClick(object sender, EventArgs e)
        {
            double textSize;
            if (!double.TryParse(picker.Items[picker.SelectedIndex], out textSize))
            {
                Console.WriteLine("Error while parsing");
            }
            else
            {
                Settings.TextSize = textSize;
                Xamarin.Essentials.Preferences.Set("Size", Settings.TextSize);
                MessagingCenter.Send(this, "SizeChanged");
            }
        }

        public void OnApplyButtonClick(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}