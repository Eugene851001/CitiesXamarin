#nullable enable
using System;
using System.Collections.Generic;
using System.Text;

namespace Cities.Utils
{
    public static class Validator
    {
        public static string? ValidYear(string strYear)
        {
            int year;
            if (!int.TryParse(strYear, out year))
            {
                return "Year should be string";
            }

            int minValue = -9600;
            int maxValue = 2021;
            if (year < minValue || year > maxValue)
            {
                return $"Year should be from {minValue} to {maxValue}";
            }

            return null;
        }

        public static string? ValidPopulation(string strPopulation)
        {
            int population;
            if (!int.TryParse(strPopulation, out population))
            {
                return "Population should be integer";
            }

            if (population < 0)
            {
                return "Population should be from 0";
            }

            return null;
        }

        public static string? ValidLatitude(string strLatitude)
        {
            double latitude;
            if (!double.TryParse(strLatitude, out latitude))
            {
                return "Latitude should be float number";
            }

            int minValue = -90;
            int maxValue = 90;
            if (latitude < minValue || latitude > maxValue)
            {
                return $"Latitude should be from {minValue} to {maxValue}";
            }

            return null;
        }

        public static string? ValidLongitude(string strLongitude)
        {
            double longitude;
            if (!double.TryParse(strLongitude, out longitude))
            {
                return "Longitude should be float number";
            }

            int minValue = -180;
            int maxValue = 180;
            if (longitude < minValue || longitude > maxValue)
            {
                return $"Longitide sould be from {minValue} to {maxValue}";
            }

            return null;
        }
    }
}
