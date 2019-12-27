using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RephaseV2.PageModel
{
    //PageModel Base class
    class PageModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
