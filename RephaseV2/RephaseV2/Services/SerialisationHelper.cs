using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RephaseV2.Models;
using RephaseV2.Services.Interfaces;

namespace RephaseV2.Services
{
    public class SerialisationHelper : ISerialisationHelper
    {
        /// <summary>
        /// Serialise menu items to json
        /// </summary>
        /// <param name="menuItems"></param>
        /// <returns></returns>
        public string SerialiseMenuItems(List<MenuItems> menuItems)
        {
            return JsonConvert.SerializeObject(menuItems);
        }

        /// <summary>
        /// Deserialise json to T object. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="menuItems"></param>
        /// <returns></returns>
        public T DeserialiseJson<T>(string menuItems)
        {
            return JsonConvert.DeserializeObject<T>(menuItems);
        }
    }
}