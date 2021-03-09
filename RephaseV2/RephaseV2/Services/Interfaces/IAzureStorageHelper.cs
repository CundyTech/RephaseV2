using System;
using System.IO;
using System.Threading.Tasks;

namespace RephaseV2.Services.Interfaces
{
    public interface IAzureStorageHelper
    {
        /// <summary>
        /// Download images from Azure blob storage.
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        Task DownloadBlobAsync(string filepath, string image);

        /// <summary>
        /// Uploads file from local storage to Azure Blob Storage.
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="fileName"></param>
        Task UploadBlobFromLocalAsync(string filepath, string fileName);

        /// <summary>
        /// Upload content json from azure storage.
        /// </summary>
        /// <returns></returns>
        void UploadContentJsonAsync(string json);

        /// <summary>
        /// Download content json from azure storage.
        /// </summary>
        /// <returns></returns>
        Task<string> DownloadContentJsonAsync();

        Task UploadBlobFromWebAsync(Stream fileStream, string fileName, Guid sessionId);
    }
}
