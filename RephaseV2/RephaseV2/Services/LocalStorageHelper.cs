using Newtonsoft.Json;
using PCLStorage;
using RephaseV2.Models;
using RephaseV2.Services.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RephaseV2.Services
{
    public class LocalStorageHelper : ILocalStorageHelper
    {
        /// <summary>
        /// Retrieve local storage path, create directory if it doesn't exist.
        /// </summary>
        /// <returns></returns>
        public async Task<string> RetrieveLocalStoragePathAsync()
        {
            // Access the file system for the current platform.
            IFileSystem fileSystem = FileSystem.Current;

            // Get the root directory of the file system.
            IFolder rootFolder = fileSystem.LocalStorage;

            // Create folder, if it doesn't already exist.
            // IFolder localStoragePath = await rootFolder.CreateFolderAsync("Content", CreationCollisionOption.OpenIfExists);

            return rootFolder.Path;
        }

        /// <summary>
        /// Saves content json file to local storage folder json.
        /// </summary>
        /// <param name="json"></param>
        public async Task SaveJsonToLocalStorageAsync(string json)
        {
            // Access the file system for the current platform.
            IFileSystem fileSystem = FileSystem.Current;

            // Get the root directory of the file system.
            IFolder rootFolder = fileSystem.LocalStorage;

            // Create a file, if one doesn't already exist.
            IFile jsonFile = await rootFolder.CreateFileAsync("content.txt", CreationCollisionOption.ReplaceExisting);

            var items = JsonConvert.DeserializeObject<List<MenuItems>>(json);

            AdaptContentJsonToLocalUrl(items, rootFolder.Path);

            json = JsonConvert.SerializeObject(items);

            File.WriteAllText(jsonFile.Path, json);
        }

        public void AdaptContentJsonToLocalUrl(List<MenuItems> items, string jsonFilePath)
        {
            for (int i = 0; i < items.Count; i++)
            {
                var localImagePath = items[i].LocalImagePath;
                items[i].LocalImagePath = $"{jsonFilePath}/{localImagePath}";

                if (items[i].Child.Count > 0)
                {
                    AdaptContentJsonToLocalUrl(items[i].Child, jsonFilePath);
                }
            }

        }

        /// <summary>
        /// Retrieves content json file to local storage folder json.
        /// </summary>
        /// <param name="json"></param>
        public string GetContentJsonFromLocalStorage()
        {
            // Access the file system for the current platform.
            IFileSystem fileSystem = FileSystem.Current;

            // Get the root directory of the file system.
            IFolder rootFolder = fileSystem.LocalStorage;

            var jsonFilePath = Path.Combine(rootFolder.Path, "content.txt");

            if (!File.Exists(jsonFilePath))
            {
                return null;
            }

            return File.ReadAllText(jsonFilePath);
        }
    }
}
