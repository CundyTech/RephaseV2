using RephaseV2.Models;
using RephaseV2.PageModel;
using RephaseV2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RephaseV2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashScreenPage : ContentPage
    {
        private string LocalFilePath;
        private IConnectivityManager ConnectivityManager;
        private IMenuItemHelper MenuItemHelper;
        private IAzureStorageHelper AzureStorageHelper;
        private IImageHelper ImageHelper;
        private ITextToSpeechHelper TextToSpeechHelper;
        private ILocalStorageHelper LocalStorageHelper;
        private ISerialisationHelper SerialisationHelper;
        private SplashScreenPageModel pageModel;

        public SplashScreenPage(
            IConnectivityManager connectivityManager,
            IMenuItemHelper menuItemHelper,
            IAzureStorageHelper azureStorageHelper,
            IImageHelper imageHelper,
            ITextToSpeechHelper textToSpeechHelper,
            ILocalStorageHelper localStorageHelper,
            ISerialisationHelper serialisationHelper)
        {
            ConnectivityManager = connectivityManager ?? throw new NullReferenceException("ConnectivityManager");
            MenuItemHelper = menuItemHelper ?? throw new NullReferenceException("MenuItemHelper");
            AzureStorageHelper = azureStorageHelper ?? throw new NullReferenceException("AzureStorageHelper");
            ImageHelper = imageHelper ?? throw new NullReferenceException("ImageHelper");
            TextToSpeechHelper = textToSpeechHelper ?? throw new NullReferenceException("TextToSpeechHelper");
            LocalStorageHelper = localStorageHelper ?? throw new NullReferenceException("LocalStorageHelper");
            SerialisationHelper = serialisationHelper ?? throw new NullReferenceException("SerialisationHelper");

            InitializeComponent();

            pageModel = new SplashScreenPageModel();
            pageModel.WelcomeMessage = "Welcome to Rephase!";
            BindingContext = pageModel;

            ApplicationStartAsync();
        }

        /// <summary>
        /// Collect content json, any assets not already 
        /// downloaded and then navigate to main content page.
        /// </summary>
        /// <returns></returns>
        private async Task ApplicationStartAsync()
        {
            //Hide Navigation bar.
            NavigationPage.SetHasNavigationBar(this, false);

            if (ConnectivityManager.IsInternetAccessable())
            {
                await CollectAssetsAsync();
            }

            var menuItems = LocalStorageHelper.GetContentJsonFromLocalStorage();
            if (menuItems == null)
            {
                pageModel.ProgressText = "An internet connection is required for first run. Please close Rephase and try again.";
            }
            else
            {
                await Navigation.PushAsync(new StandardContentPage(
                    TextToSpeechHelper,
                    MenuItemHelper,
                    SerialisationHelper.DeserialiseJson<List<MenuItems>>(menuItems)));
            }
        }

        /// <summary>
        /// Collect and download all assets needed.
        /// </summary>
        /// <returns></returns>
        private async Task CollectAssetsAsync()
        {
            try
            {
                List<ImageDownload> imageDownloads = new List<ImageDownload>();

                pageModel.ProgressText = "Collecting information...";

                await LocalStorageHelper.SaveJsonToLocalStorageAsync(await AzureStorageHelper.DownloadContentJsonAsync());

                imageDownloads = CollectImagesToBeDownloaded(SerialisationHelper.DeserialiseJson<List<MenuItems>>(LocalStorageHelper.GetContentJsonFromLocalStorage()), imageDownloads);

                int downloadCount = imageDownloads.Count;

                // Get all images in local file store
                string[] fileEntries = Directory.GetFiles(await LocalStorageHelper.RetrieveLocalStoragePathAsync());

                List<ImageDownload> toDownload = new List<ImageDownload>();
                // Work out if we have already downloaded each file.
                for (int i = 0; i < imageDownloads.Count; i++)
                {
                    if (fileEntries.ToList().All(x => !string.Equals(x, imageDownloads[i].LocalImagePath, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        if (!string.IsNullOrWhiteSpace(imageDownloads[i].LocalImagePath))
                        {
                            toDownload.Add(imageDownloads[i]);
                        }
                    }
                }

                for (int i = 0; i < toDownload.Count; i++)
                {
                    pageModel.ProgressText = string.Format("Downloading {0} of {1}", i + 1, toDownload.Count);
                    UpdateProgressBar(i, downloadCount);
                    await DownloadImagesAsync(toDownload[i]);
                }

                pageModel.ProgressText = "Finished, loading main page...";
            }
            catch (Exception ex)
            {
                pageModel.ProgressText = ex.Message;
            }
        }

        /// <summary>
        /// Work out progress and update
        /// </summary>
        /// <param name="value"></param>
        /// <param name="total"></param>
        public void UpdateProgressBar(dynamic value, int total)
        {
            float progress = (float)value / total;
            pageModel.Progress = progress;
        }

        private Task<ImageDownload> DownloadImagesAsync(ImageDownload download)
        {
            return Task.Run(() => ImageHelper.SaveImageToLocalStorageAsync(download));
        }

        private List<ImageDownload> CollectImagesToBeDownloaded(List<MenuItems> oc, List<ImageDownload> urls)
        {

            for (int i = 0; i < oc.Count; i++)
            {
                var imageDownload = new ImageDownload
                {
                    ImageUrl = oc[i].ImageUrl,
                    Title = oc[i].Title,
                    LocalImagePath = oc[i].LocalImagePath
                };

                urls.Add(imageDownload);

                if (oc[i].Child.Count > 0)
                {
                    CollectImagesToBeDownloaded(oc[i].Child, urls);
                }
            }

            return urls;
        }

    }
}