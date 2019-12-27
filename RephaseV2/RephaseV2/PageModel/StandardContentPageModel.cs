using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using RephaseV2.Models;
using RephaseV2.Services;
using RephaseV2.Services.Interfaces;
using Xamarin.Forms;

namespace RephaseV2.PageModel
{
    class StandardContentPageModel : PageModel, INotifyPropertyChanged
    {
        public ICommand TextToSpeechCommand { get; private set; }
        public ICommand OptionsCommand { get; private set; }

        ITextToSpeechHelper TextToSpeechHelper;

        public StandardContentPageModel()
        {
            TextToSpeechCommand = new Command<string>(ClickTextToSpeechButtonAsync);
            OptionsCommand = new Command<string>(ClickOptionsButtonAsync);
            TextToSpeechHelper = new TextToSpeechHelper();
        }

        private List<MenuItems> children;
        public List<MenuItems> Children
        {
            get
            {
                return children;
            }
            set
            {
                children = value;
                NotifyPropertyChanged();
            }
        }

        private string icon;
        public string Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                NotifyPropertyChanged();
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                NotifyPropertyChanged();
            }
        }

        public void ClickTextToSpeechButtonAsync(string value)
        {
            TextToSpeechHelper.ConvertTextToSpeechAsync(value);
        }

        public void ClickOptionsButtonAsync(string value)
        {
            TextToSpeechHelper.ConvertTextToSpeechAsync(value);
        }
    }
}
