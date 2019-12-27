using System.Collections.Generic;
using System.Collections.ObjectModel;
using RephaseV2.Models;

namespace RephaseV2.Services.Interfaces
{
    public interface IMenuItemHelper
    {
        /// <summary>
        /// Converts content json to a list of menuitems.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        //List<MenuItems> ConvertToObservableList(string json);

        /// <summary>
        /// Retrieve the child of a menu item given a key.
        /// </summary>
        /// <param name="parentKey"></param>
        /// <returns></returns>
        List<MenuItems> GetChild(string parentKey);
    }
}