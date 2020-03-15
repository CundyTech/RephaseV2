using RephaseV2.Models;
using RephaseV2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace RephaseV2.Services
{
    internal class ImageHelper : IImageHelper
    {
        private IAzureStorageHelper AzureStorageHelper;
        public ImageHelper(IAzureStorageHelper azureStorageHelper)
        {
            AzureStorageHelper = azureStorageHelper ?? throw new NullReferenceException("AzureStorageHelper argument is null");
        }

        /// <summary>
        /// Saves given image object to local storage.
        /// </summary>
        /// <param name="download"></param>
        /// <returns></returns>
        public async Task<ImageDownload> SaveImageToLocalStorageAsync(ImageDownload download)
        {
            string filePath = string.Empty;
            try
            {
                if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    await AzureStorageHelper.DownloadBlobAsync(download.LocalImagePath, download.ImageUrl);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return download;
        }

        /// <summary>
        /// Collect a list of images to be downloaded from the collection of menuitems.
        /// </summary>
        /// <param name="oc"></param>
        /// <param name="urls"></param>
        /// <param name="localFilePath"></param>
        /// <returns></returns>
        public List<ImageDownload> CollectImagesToBeDownloaded(List<MenuItems> oc, List<ImageDownload> urls, string localFilePath)
        {
            for (int i = 0; i < oc.Count; i++)
            {
                var imageDownload = new ImageDownload
                {
                    ImageUrl = oc[i].ImageUrl,
                    Title = oc[i].Title,
                    LocalImagePath = Path.Combine(localFilePath, oc[i].ImageUrl)
                };

                urls.Add(imageDownload);

                if (oc[i].Child.Count > 0)
                {
                    CollectImagesToBeDownloaded(oc[i].Child, urls, localFilePath);
                }
            }

            return urls;
        }
        
        /// <summary>
        /// Download image to local file store.
        /// </summary>
        /// <param name="download"></param>
        /// <returns></returns>
        public async Task<ImageDownload> DownloadImagesAsync(ImageDownload download)
        {
            return await SaveImageToLocalStorageAsync(download);
        }
    }
}