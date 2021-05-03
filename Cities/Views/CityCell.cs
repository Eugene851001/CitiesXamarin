using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Cities.Services;
using Cities.Models;

namespace Cities.Views
{
    class CityCell : ViewCell
    {
        Label lbName;
        Label lbCountry;
        Label lbCapital;

        Image imgAvatar;

        public CityCell()
        {
            lbName = new Label() { FontSize = Settings.TextSize, HorizontalOptions = LayoutOptions.EndAndExpand, Padding = 10};
            lbCountry = new Label() { HorizontalOptions = LayoutOptions.Start, Padding = 10};
            lbCapital = new Label() { HorizontalOptions=LayoutOptions.Start, Padding = 10};

            imgAvatar = new Image() { HeightRequest = 75, WidthRequest = 75 };
            imgAvatar.VerticalOptions = LayoutOptions.FillAndExpand;
            imgAvatar.Margin = 10;

            StackLayout cell = new StackLayout() { Orientation = StackOrientation.Horizontal};
            StackLayout titles = new StackLayout() { Orientation = StackOrientation.Vertical };
            StackLayout head = new StackLayout() 
            { 
                Orientation = StackOrientation.Horizontal, 
                HorizontalOptions= LayoutOptions.FillAndExpand 
            };

            head.Children.Add(lbName);
            head.Children.Add(lbCountry);
            titles.Children.Add(head);
            titles.Children.Add(lbCapital);

            cell.Children.Add(imgAvatar);
            cell.Children.Add(titles);
            
            this.View = cell;

            MessagingCenter.Subscribe<SizePickerPage>(this, "SizeChanged", (sender) => { this.lbName.FontSize = Settings.TextSize; });
            MessagingCenter.Subscribe<FontPickerPage>(this, "FontChanged", (sender) => { this.lbName.FontFamily = Settings.Font; });
        }

        public static readonly BindableProperty NameProperty = BindableProperty.Create($"{nameof(Name)}", typeof(string), typeof(CityCell), "");
        public static readonly BindableProperty CountryProperty = BindableProperty.Create($"{nameof(Country)}", typeof(string), typeof(CityCell), "");
        public static readonly BindableProperty CapitalProperty = BindableProperty.Create($"{nameof(Capital)}", typeof(bool), typeof(CityCell), false);
        public static readonly BindableProperty CityIdProperty = BindableProperty.Create($"{nameof(CityId)}", typeof(string), typeof(CityCell), "");
        public static readonly BindableProperty AvatarProperty = BindableProperty.Create($"{nameof(Avatar)}", typeof(ImageSource), typeof(CityCell), null);

        public string Name 
        { 
            get => (string)GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }

        public string Country
        {
            get => (string)GetValue(CountryProperty);
            set => SetValue(CountryProperty, value);
        }

        public bool Capital
        {
            get => (bool)GetValue(CapitalProperty);
            set => SetValue(CapitalProperty, value);
        }

        public string CityId
        {
            get => (string)GetValue(CityIdProperty);
            set => SetValue(CityIdProperty, value);
        }

        public ImageSource Avatar
        {
            get => (ImageSource)GetValue(AvatarProperty);
            set => SetValue(AvatarProperty, value);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                lbName.Text = Name;
                lbName.FontSize = Settings.TextSize;
                lbName.FontFamily = Settings.Font;
                lbCountry.Text = Country;
                lbCapital.Text = this.Capital ? "Capital" : "";
                imgAvatar.Source = Avatar;
            }
        }

    }
}
