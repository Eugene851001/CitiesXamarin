using System;
using System.Collections.Generic;
using System.Text;

namespace Cities.Models
{
    public static class Settings
    {
        public static string Language { get; set; } = "";
        public static string Font { get; set; } = "serif";
        public static double TextSize { get; set; } = 15.0;
        public static string HexColor { get; set; } = "FF00FF00";
    }
}
