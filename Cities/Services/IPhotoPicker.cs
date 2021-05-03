using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text;

namespace Cities.Services
{
    public interface IPhotoPicker
    {
        /// <summary>
        /// Gets stream for image.
        /// </summary>
        /// <returns>Stream.</returns>
        Task<Stream> GetImageStreamAsync();


        /// <summary>
        /// Get url for image.
        /// </summary>
        /// <returns>Url.</returns>
        string GetImageUrl();
    }
}
