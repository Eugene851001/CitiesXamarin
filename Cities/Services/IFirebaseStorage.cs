using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Cities.Models;

namespace Cities.Services
{
    public interface IFirebaseStorage
    {
        string LoadImage(Stream source, City city);

        string LoadVideo(Stream source, City city); 
    }
}
