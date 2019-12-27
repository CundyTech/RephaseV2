using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RephaseV2.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainContentPage : ContentPage
	{
		public MainContentPage ()
		{
			InitializeComponent ();
		}

	    private void Settings_OnClicked(object sender, EventArgs e)
	    {
	        throw new NotImplementedException();
	    }

	    private void Cell_OnTapped(object sender, EventArgs e)
	    {
	        throw new NotImplementedException();
	    }

	    private void Button_OnClicked(object sender, EventArgs e)
	    {
	        throw new NotImplementedException();
	    }
	}
}