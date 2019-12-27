using RephaseV2.PageModel;
using Xamarin.Forms;

namespace RephaseV2.Pages
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            var pageModel = new SplashScreenPageModel();
            pageModel.WelcomeMessage = "Welcome to Rephase!";

            BindingContext = pageModel;                      
        }
	}
}
