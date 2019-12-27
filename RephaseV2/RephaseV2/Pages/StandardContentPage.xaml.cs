using RephaseV2.Models;
using RephaseV2.PageModel;
using RephaseV2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RephaseV2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StandardContentPage : ContentPage
    {
        private ITextToSpeechHelper TextToSpeechHelper;
        private IMenuItemHelper MenuItemHelper;
        private StandardContentPageModel StandardContentPageModel;
        private const string MainMenuTitle = "Main Menu";

        public StandardContentPage(
            ITextToSpeechHelper textToSpeechHelper,
            IMenuItemHelper menuItemHelper,
            List<MenuItems> children,
            string icon = null,
            string title = null)
        {
            InitializeComponent();

            TextToSpeechHelper = textToSpeechHelper ?? throw new ArgumentNullException("TextToSpeechHelper");
            MenuItemHelper = menuItemHelper ?? throw new ArgumentNullException("MenuItemHelper");

            StandardContentPageModel = new StandardContentPageModel
            {
                Children = children,
                Icon = icon,
                Title = title ?? MainMenuTitle
            };

            BindingContext = StandardContentPageModel;

            if (StandardContentPageModel.Title == MainMenuTitle)
            {
                NavigationPage.SetHasBackButton(this, false);
            }

            var labelTapGestureRecognizer = new TapGestureRecognizer();
            labelTapGestureRecognizer.Tapped += async (s, e) =>
            {
                await Button_ClickedAsync(s, e);
            };
        }

        /// <summary>
        /// Overide back button. Disabled if on main menu.
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            if (Title == "Main Menu")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void TapGestureRecognizer_TappedAsync(object sender, EventArgs e)
        {
            try
            {
                if (sender is Image image)
                {
                    var title = ((MenuItems)image.BindingContext).Title;
                    TextToSpeechHelper.ConvertTextToSpeechAsync(title);
                }
                if (sender is Button button)
                {
                    var title = ((MenuItems)button.BindingContext).Title;
                    TextToSpeechHelper.ConvertTextToSpeechAsync(title);
                }
                else if (sender is ViewCell viewCell)
                {

                    var title = ((MenuItems)viewCell.BindingContext).Title;
                    var childList = MenuItemHelper.GetChild(title);
                    var icon = ((MenuItems)viewCell.BindingContext).LocalImagePath;
                    if (childList.Count != 0)
                    {
                        Navigation.PushAsync(new StandardContentPage(TextToSpeechHelper, MenuItemHelper, childList, icon, title));
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
        private async Task Button_ClickedAsync(object sender, EventArgs e)
        {
            try
            {
                IsEnabled = false;
                Button btn = (Button)sender;
                var title = ((ListItems)btn.BindingContext).Title;
                await TextToSpeechHelper.ConvertTextToSpeechAsync(title);
                IsEnabled = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
        private void Settings_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage());
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            TextToSpeechHelper.ConvertTextToSpeechAsync(btn.Text);
        }
    }
}

