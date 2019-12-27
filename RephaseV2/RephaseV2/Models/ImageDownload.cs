namespace RephaseV2.Models
{
    public class ImageDownload
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
    }
}