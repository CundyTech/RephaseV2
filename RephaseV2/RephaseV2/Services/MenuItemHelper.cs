using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RephaseV2.Models;
using RephaseV2.Services.Interfaces;

namespace RephaseV2.Services
{
    public class MenuItemHelper : IMenuItemHelper
    {
        ISerialisationHelper SerialisationHelper;
        ILocalStorageHelper LocalStorageHelper;
        public MenuItemHelper(ISerialisationHelper serialisationHelper, ILocalStorageHelper localStorageHelper)
        {
            SerialisationHelper = serialisationHelper;
            LocalStorageHelper = localStorageHelper;
        }

        public List<MenuItems> SearchForMenuItem(List<MenuItems> menuItems, string title)
        {
            List<MenuItems> foundMenuItems = new List<MenuItems>();
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (menuItems[i].Title == title)
                {
                    foundMenuItems.Add(menuItems[i]);
                }
                if (menuItems[i].Child.Count > 0)
                {
                    SearchForMenuItem(menuItems[i].Child, title);
                }
            }

            return foundMenuItems;
        }

        ///// <summary>
        ///// Converts content json to a list of menuitems.
        ///// </summary>
        ///// <param name="json"></param>
        ///// <returns></returns>
        //public ObservableCollection<MenuItems> ConvertToObservableList(string json)
        //{
        //    try
        //    {
        //        ObservableCollection<MenuItems> olistItems = new ObservableCollection<MenuItems>();
        //        var items = SerialisationHelper.DeserialiseJson<List<MenuItems>>(json);

        //        foreach (var item in items)
        //        {
        //            olistItems.Add(item);
        //        }

        //        return olistItems;
        //    }
        //    catch (Exception e)
        //    {
        //        System.Console.WriteLine(e);
        //        throw;
        //    }
        //}

        /// <summary>
        /// Retrieve the child of a menu item given a key.
        /// </summary>
        /// <param name="parentKey"></param>
        /// <returns></returns>
        public List<MenuItems> GetChild(string parentKey)
        {
            try
            {
                var lListItems = SerialisationHelper.DeserialiseJson<List<MenuItems>>(LocalStorageHelper.GetContentJsonFromLocalStorage());

                List<MenuItems> children = new List<MenuItems>();
                foreach (var item in lListItems)
                {
                    if (item.Title == parentKey)
                    {
                        foreach (var child in item.Child)
                        {
                            children.Add(child);
                        }
                    }
                    else
                    {
                        foreach (var child in item.Child)
                        {
                            if (child.Title == parentKey)
                            {
                                foreach (var subChild in child.Child)
                                {
                                    children.Add(subChild);
                                }

                                continue;
                            }
                        }
                    }
                }

                List<MenuItems> olistItems = new List<MenuItems>();
                foreach (var child in children)
                {
                    olistItems.Add(child);
                }

                return olistItems;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}