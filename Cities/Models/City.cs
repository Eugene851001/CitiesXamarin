using System;
using System.Collections.Generic;
using System.Text;

namespace Cities.Models
{
    public class City
    {
        public string Mail { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int Population { get; set; }
        public int Year { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool Capital { get; set; }
        public string Video { get; set; } = "";
        public string Id { get; set; } = "";
        public List<string> Images { get; set; } = new List<string>();

        public override string ToString()
        {
            return $"{this.Name}: {this.Country}";
        }
    }
}
