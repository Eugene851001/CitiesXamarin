#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Cities.Models;
using Cities.Views;
using Xamarin.Forms;

using Cities.Utils;
using Cities.Services;
using Xamarin.Essentials;

namespace Cities.ViewModels
{
    public class CityViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SaveCommand { get; set; }
        public ICommand AddImageCommand { get; set; }
        public ICommand AddVideoCommand { get; set; }
        public ICommand ShowVideoCommand { get; set; }
        public INavigation Navigation;

        private Page page;
        private City city;
        private ImageSource avatar;
        private bool hasVideo;
        private string year;
        private string population;
        private string latitude;
        private string longitude;


        public CityViewModel(City city, Page page)
        {
            this.city = city;
            this.HasVideo = city.Video != null && city.Video.Length > 0;
            this.SaveCommand = new Command(SaveCity);
            this.AddImageCommand = new Command(AddImage);
            this.AddVideoCommand = new Command(AddVideo);
            this.ShowVideoCommand = new Command(ShowVideo);

            Year = city.Year.ToString();
            Population = city.Population.ToString();
            Latitude = city.Latitude.ToString();
            Longitude = city.Longitude.ToString();

            if (!(page is null))
            {
                this.page = page;
                this.Navigation = page.Navigation;
            }

            Uri? uri = city.Images.Count > 0 ? new Uri(city.Images[0]) : null;
            string noImg = "https://www.reallycheapfloors.com/wp-content/uploads/2018/11/x2017-05-15_18.png.pagespeed.ic_.tLD9q0ZZph.png";
            this.avatar = uri is null ? ImageSource.FromUri(new Uri(noImg)) : ImageSource.FromUri(uri);
        }

        public string Mail 
        {
            get => this.city.Mail;
        }


        public bool HasVideo
        {
            get => this.hasVideo;
            set
            {
                this.hasVideo = value;
                NotifyPropertyChanged();
            }
        }


        public ImageSource Avatar
        {
            get
            {
                return avatar;
            }

            set
            {
                this.avatar = value;
                NotifyPropertyChanged();
            }
        }


        public string Name
        {
            get => this.city.Name;
            set
            {
                this.city.Name = value;
                NotifyPropertyChanged();
            }
        }

        public string Country
        {
            get => this.city.Country;
            set
            {
                this.city.Country = value;
                NotifyPropertyChanged();
            }
        }

        public string Year
        {
            get => this.year;
            set
            {
                this.year = value;
                NotifyPropertyChanged();
            }
        }

        public string Population
        {
            get => this.population;
            set
            {
                this.population = value;
                NotifyPropertyChanged();
            }
        }

        public bool Capital
        {
            get => this.city.Capital;
            set
            {
                this.city.Capital = value;
                NotifyPropertyChanged();
            }
        }

        public string Latitude
        {
            get => this.latitude;
            set
            {
                this.latitude = value;
                NotifyPropertyChanged();
            }
        }

        public string Longitude
        {
            get => this.longitude;
            set
            {
                this.longitude = value;
                NotifyPropertyChanged();
            }
        }

        private void SaveCity()
        {
            string? err = Validator.ValidLatitude(this.latitude);
            if (err != null)
            {
                this.page.DisplayAlert("Error", err, "OK");
                return;
            }

            this.city.Latitude = double.Parse(Latitude);

            err = Validator.ValidLongitude(this.longitude);
            if (err != null)
            {
                this.page.DisplayAlert("Error", err, "OK");
                return;
            }

            city.Longitude = double.Parse(Longitude);

            err = Validator.ValidYear(this.year);
            if (err != null)
            {
                this.page.DisplayAlert("Error", err, "OK");
                return;
            }

            city.Year = int.Parse(Year);

            err = Validator.ValidPopulation(this.population);
            if (err != null)
            {
                this.page.DisplayAlert("Error", err, "OK");
                return;
            }

            city.Population = int.Parse(Population);

            bool result = DependencyService.Get<IFirebaseDB>().UpdateCity(this.city);

            if (!result)
            {
                this.page.DisplayAlert("Error", "Can not update", "OK");
            }
            else
            {
                MessagingCenter.Send(this, "CityUpdate");
                this.Navigation.PopAsync();
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void onAddButtonClicked(object sender, EventArgs e)
        {
            //    var s = DependencyService.Get<IPhotoPicker>().GetImageStreamAsync();
            //   s.Wait();
            //  Console.WriteLine(s.Result);
            string url = "https://astrakhanfm.ru/uploads/posts/2020-04/1587140178_kotenok.jpg"; // DependencyService.Get<IPhotoPicker>().GetImageUrl();
            this.city.Video = url;
        }

        private async void AddImage()
        {
            //    var s = DependencyService.Get<IPhotoPicker>().GetImageStreamAsync();
            //   s.Wait();
            //  Console.WriteLine(s.Result);
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions() { Title = "Please, choose image" });
            if (result is null)
            {
                return;
            }

            Avatar = ImageSource.FromFile(result.FullPath);
            var stream = await result.OpenReadAsync();
            string uri = DependencyService.Get<IFirebaseStorage>().LoadImage(stream, this.city);
            this.city.Images.Add(uri);
        }

        private async void AddVideo()
        {
            var result = await MediaPicker.PickVideoAsync(new MediaPickerOptions() { Title = "Please, choose video" });
            if (result is null)
            {
                return;
            }

            var stream = await result.OpenReadAsync();
            string uri = DependencyService.Get<IFirebaseStorage>().LoadVideo(stream, this.city);
            this.city.Video = uri;
            this.HasVideo = true;
        }

        private async void ShowVideo()
        {
            if (this.HasVideo)
            {
                await Navigation.PushAsync(new VideoPlayerPage(this.city.Video));
            }
        }
    }
}
