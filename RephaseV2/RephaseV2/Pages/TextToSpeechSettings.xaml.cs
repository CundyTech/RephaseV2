using System.Collections.Generic;
using System.Linq;
using Plugin.TextToSpeech;
using Plugin.TextToSpeech.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RephaseV2.Pages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextToSpeechSettings : ContentPage
    {
        static CrossLocale crossLocale;
        public TextToSpeechSettings()
        {
            #region AddViews

            var speechText = new Editor
            {
                Text = "The quick brown fox jumped over the lazy dog.",
                HeightRequest = 100,
                VerticalOptions = LayoutOptions.Start
            };

            var speakButton = new Button
            {
                Text = "Test"
            };

            var languageButton = new Button
            {
                Text = "Default Language"
            };

            var saveButton = new Button
            {
                Text = "Save"
            };

            var sliderPitch = new Slider(0, 2.0, 1.0);
            var sliderRate = new Slider(0, 2.0, Device.OnPlatform(.25, 1.0, 1.0));
            var sliderVolume = new Slider(0, 1.0, 1.0);

            var useDefaults = new Switch
            {
                IsToggled = false,
                HorizontalOptions = LayoutOptions.Center
            };

            var scrollView = new ScrollView();
            Content = scrollView;
            scrollView.Content = new StackLayout
            {

                Padding = 10,
                Spacing = 10,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center,
                Children =
                {
                    new  BoxView {
                        Color = Color.Transparent,
                        HeightRequest = 10,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.CenterAndExpand},
                    new Label {Text = "Example Text"}, speechText,
                    new Label {Text = "Pitch"}, sliderPitch,
                    new Label {Text = "Speak Rate"}, sliderRate,
                    new Label {Text = "Volume"}, sliderVolume,
                    new Label {Text = "Use Defaults"}, useDefaults,
                    new Label {Text = "Language"}, languageButton,
                    new BoxView() { Color = Color.Gray, WidthRequest = 100, HeightRequest = 1},
                    new  BoxView {
                        Color = Color.Transparent,
                        WidthRequest = 150,
                        HeightRequest = 25,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.CenterAndExpand},
                    speakButton,
                    saveButton
                }
            };

            #endregion

            #region GetViewDefaults

            //sliderPitch.Value = SharedPreferencesHelper.GetPreference<float>("pitch") ?? 1.0;
            //sliderRate.Value = SharedPreferencesHelper.GetPreference<float>("speakRate") ?? 1.0;
            //sliderVolume.Value = SharedPreferencesHelper.GetPreference<float>("volume") ?? 1.0;
            //crossLocale = SharedPreferences.GetPreference<CrossLocale?>("crossLocale") ??  locales.FirstOrDefault(x => x.ToString() == crossLocale.Language); 
            #endregion

            #region AddViewEvents

            languageButton.Clicked += async (sender, args) =>
                {
                    var locales = await CrossTextToSpeech.Current.GetInstalledLanguages();
                    var menuItems = locales.Select(a => a.DisplayName.ToString()).ToArray();
                    var selected = await this.DisplayActionSheet("Language", "OK", null, menuItems);

                    if (string.IsNullOrWhiteSpace(selected) || selected == "OK")
                    {
                        return;
                    }

                    using (IEnumerator<CrossLocale> localesEnum = locales.GetEnumerator())
                    {
                        while (localesEnum.MoveNext())
                        {
                            crossLocale = localesEnum.Current;
                            string displayName = crossLocale.DisplayName;
                            string language = crossLocale.Language;
                            string country = crossLocale.Country;

                            if (selected == displayName)
                            {
                                languageButton.Text = displayName;
                                return;
                            }
                        }
                    }

                    if (Device.RuntimePlatform == Device.Android)
                    {
                        crossLocale = locales.FirstOrDefault(x => x.ToString() == crossLocale.Language);
                    }
                    else
                    {
                        crossLocale = new CrossLocale
                        {
                            Language = crossLocale.Language
                        };
                    }
                };

            speakButton.Clicked += async (sender, args) =>
             {
                 speakButton.IsEnabled = false;

                 if (useDefaults.IsToggled)
                 {
                     await CrossTextToSpeech.Current.Speak(speechText.Text);
                     speakButton.IsEnabled = true;
                     return;
                 }

                 await CrossTextToSpeech.Current.Speak(speechText.Text,
                     pitch: (float)sliderPitch.Value,
                     speakRate: (float)sliderRate.Value,
                     volume: (float)sliderVolume.Value,
                     crossLocale: crossLocale);

                 speakButton.IsEnabled = true;
             };

            saveButton.Clicked += async (sender, args) =>
            {
                //SharedPreferencesHelper.PutPreference("pitch", (float)sliderPitch.Value);
                //SharedPreferencesHelper.PutPreference("speakRate", (float)sliderRate.Value);
                //SharedPreferencesHelper.PutPreference("volume", (float)sliderVolume.Value);
                //SharedPreferencesHelper.PutPreference("crossLocale", crossLocale);
            };

            #endregion
        }
    };


}
