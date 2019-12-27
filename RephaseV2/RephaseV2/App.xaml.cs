using System;
using RephaseV2.Pages;
using RephaseV2.Services;
using RephaseV2.Services.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RephaseV2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ConnectivityManager connectivityManager = new ConnectivityManager();
            SerialisationHelper serialisationHelper = new SerialisationHelper();
            LocalStorageHelper localStorageHelper = new LocalStorageHelper();
            MenuItemHelper menuItemHelper = new MenuItemHelper(serialisationHelper, localStorageHelper);
            AzureStorageHelper azureStorageHelper = new AzureStorageHelper();
            ImageHelper imageHelper = new ImageHelper(azureStorageHelper);
            TextToSpeechHelper textToSpeechHelper = new TextToSpeechHelper();

            MainPage = new NavigationPage(
                new SplashScreenPage(
                    connectivityManager,
                    menuItemHelper,
                    azureStorageHelper,
                    imageHelper,
                    textToSpeechHelper,
                    localStorageHelper,
                    serialisationHelper));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
