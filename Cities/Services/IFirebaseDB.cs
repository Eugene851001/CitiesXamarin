using Cities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cities.Services
{
    public interface IFirebaseDB
    {
        /// <summary>
        /// Gets cities from firestore.
        /// </summary>
        /// <returns>Cities.</returns>
        IEnumerable<City> GetCities();

        /// <summary>
        /// Addes city to firestore.
        /// </summary>
        /// <param name="city">City to add.</param>
        /// <returns>True if success.</returns>
        bool AddCity(City city);

        /// <summary>
        /// Overwrites existing city using Id property. 
        /// </summary>
        /// <param name="city">New city.</param>
        /// <returns>True if success.</returns>
        bool UpdateCity(City city);
    }
}
