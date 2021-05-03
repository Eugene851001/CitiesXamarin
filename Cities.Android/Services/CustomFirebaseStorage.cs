using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cities.Services;
using Firebase.Storage;
using Xamarin.Forms;

using Cities.Models;

[assembly: Dependency(typeof(Cities.Droid.Services.CustomFirebaseStorage))]
namespace Cities.Droid.Services
{
    class CustomFirebaseStorage : IFirebaseStorage
    {
        public string LoadImage(Stream source, City city)
        {
            var storage = new FirebaseStorage("cities-9ae6c.appspot.com");
            var task = storage.Child($"images/{city.Id}/{city.Images.Count}").PutAsync(source);
            task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");
            return task.GetAwaiter().GetResult();
        }

        public string LoadVideo(Stream source, City city)
        {
            var storage = new FirebaseStorage("cities-9ae6c.appspot.com");
            var task = storage.Child($"videos/{city.Id}/video.mp4").PutAsync(source);
            task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");
            return task.GetAwaiter().GetResult();
        }
    }
}