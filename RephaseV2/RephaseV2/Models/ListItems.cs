using System.Collections.ObjectModel;

namespace RephaseV2.Models
{
    public class ListItems
    {
        /// <summary>
        /// Title of List Item Object.
        /// e.g food, breakfast, drinks.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Url of image, used to download a local copy of image.
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// Local path of image.
        /// </summary>
        public string LocalImagePath { get; set; }
        public ObservableCollection<ListItems> Child { get; set; }
    }
}