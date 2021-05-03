using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cities.Models;
using Cities.Services;
using Google.Cloud.Firestore;
using Xamarin.Forms;
using Firebase.Database;
using Firebase.Database.Query;
using System.Text.Json;

[assembly: Dependency(typeof(Cities.Droid.Services.FirebaseDB))]
namespace Cities.Droid.Services
{
    public class FirebaseDB : IFirebaseDB
    { 
        public FirebaseDB()
        {
            Console.WriteLine("In FirebaseDB ctor");
        }

        public bool AddCity(City city)
        {
            var firebase = new FirebaseClient("https://cities-9ae6c-default-rtdb.firebaseio.com/");
            firebase.Child("Cities").PostAsync(city);
            return true;
        }

        public IEnumerable<City> GetCities()
        {
            var firebase = new FirebaseClient("https://cities-9ae6c-default-rtdb.firebaseio.com/");

            var task = firebase.Child("Cities").OnceAsync<City>();
            task.Wait();

            if (task.Exception != null)
            {
                Console.WriteLine(task.Exception.Message);
                return null;
            }

            IEnumerable<FirebaseObject<City>> cities = task.Result;

            return cities.Select((item) => { item.Object.Id = item.Key;  return item.Object; });
        }

        public bool UpdateCity(City city)
        {
            var firebase = new FirebaseClient("https://cities-9ae6c-default-rtdb.firebaseio.com/");
            var task = firebase.Child("Cities").Child(city.Id).PutAsync(city);
            task.Wait();

            if (task.Exception != null)
            {
                Console.WriteLine(task.Exception);
                return false;
            }

            return true;
        }
    }
}