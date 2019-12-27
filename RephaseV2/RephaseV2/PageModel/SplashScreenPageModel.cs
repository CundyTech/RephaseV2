using System.ComponentModel;

namespace RephaseV2.PageModel
{
    class SplashScreenPageModel : PageModel, INotifyPropertyChanged
    {
        //Welcome message displayed to user.
        public string WelcomeMessage { get; set; }
                
        //Text that shows what is going on during the splash screen.
        private string progressText;
        public string ProgressText
        {
            get { return progressText; }
            set
            {
                progressText = value;
                NotifyPropertyChanged();
            }
        }

        //Position of progress bar.
        private float progress;
        public float Progress
        {
            get { return progress; }
            set
            {
                progress = value;
                NotifyPropertyChanged();
            }
        }
    }
}
