using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Rephase_WebClient.Models;
using RephaseV2.Models;
using RephaseV2.Services;
using RephaseV2.Services.Interfaces;

namespace Rephase_WebClient.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _configuration;
        private IMenuItemHelper _menuItemHelper;

        public HomeController(IConfiguration configuration, IMenuItemHelper menuItemHelper)
        {
            _configuration = configuration;
            _menuItemHelper = menuItemHelper;
        }

        public async Task<IActionResult> Index()
        {
            AzureStorageHelper azureStorageHelper = new AzureStorageHelper(_configuration["TableStorageConnString"]);

            string json = await azureStorageHelper.DownloadContentJsonAsync();
            List<MenuItems> menuItems = JsonConvert.DeserializeObject<List<MenuItems>>(json);
            menuItems = AddGuidId(menuItems);

            HomeViewModel model = new HomeViewModel
            {
                ContentJson = json,
                MenuItems = menuItems
            };

            //azureStorageHelper.DownloadImages(model.MenuItems);

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateContentTree(string json)
        {
            List<MenuItems> menuItems = JsonConvert.DeserializeObject<List<MenuItems>>(json);

            //List<MenuItems> foundMenuItem = _menuItemHelper.SearchForMenuItem();

            HomeViewModel model = new HomeViewModel
            {
                MenuItems = menuItems
            };

            return PartialView("ContentTree", model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<MenuItems> AddGuidId(List<MenuItems> menuItems)
        {
            foreach (MenuItems items in menuItems)
            {
                if (items.Id == null)
                {
                    items.Id = Guid.NewGuid();
                }

                if (items.Child.Count > 0)
                {
                    AddGuidId(items.Child);
                }
            }

            return menuItems;
        }
    }
}
