using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RephaseV2.Models
{
    public class MenuItems
    {
        /// <summary>
        /// Unique Id of menu item.
        /// </summary>
        public Guid? Id { get; set; }
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
        public List<MenuItems> Child { get; set; }
    }
}