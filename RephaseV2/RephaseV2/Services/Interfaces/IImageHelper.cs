using System.Threading.Tasks;
using RephaseV2.Models;

namespace RephaseV2.Services.Interfaces
{
    public interface IImageHelper
    {
        /// <summary>
        /// Saves given image object to local storage.
        /// </summary>
        /// <param name="download"></param>
        /// <returns></returns>
        Task<ImageDownload> SaveImageToLocalStorageAsync(ImageDownload download);
    }
}