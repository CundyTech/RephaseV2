using RephaseV2.Models;
using System.Collections.Generic;

namespace RephaseV2.Services.Interfaces
{
    public interface ISerialisationHelper
    {
        /// <summary>
        /// Serialise menu items to json
        /// </summary>
        /// <param name="menuItems"></param>
        /// <returns></returns>
        T DeserialiseJson<T>(string menuItems);

        /// <summary>
        /// Deserialise json to T object. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="menuItems"></param>
        /// <returns></returns>
        string SerialiseMenuItems(List<MenuItems> menuItems);
    }
}