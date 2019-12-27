using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RephaseV2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : TabbedPage
    {
        public SettingsPage()
        {
            InitializeComponent();

           this.Title = "Settings";
           this.Children.Add(new TextToSpeechSettings
            {
                Title = "Text To Speech"
            });
            this.Children.Add(new ContentPage
            {
                Title = "Reload Content",
                Content = new StackLayout
                {
                    Children = {
                        new BoxView { Color = Color.Blue },
                        new BoxView { Color = Color.Red}
                        }
                }
            });
        }
    }
}