using System;
using System.Collections.Generic;
using System.Text;
using Cities.Models;

namespace Cities.Services
{
    public static class SharedCities
    {
        public static IList<City> cities;
        public static string CurrentMail;
    }
}
