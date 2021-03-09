using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
        private IAzureStorageHelper _azureStorageHelper;

        public HomeController(IConfiguration configuration, IMenuItemHelper menuItemHelper, IAzureStorageHelper azureStorageHelper)
        {
            _configuration = configuration;
            _menuItemHelper = menuItemHelper;
            _azureStorageHelper = azureStorageHelper;
        }

        public async Task<IActionResult> Index()
        {
            AzureStorageHelper azureStorageHelper = new AzureStorageHelper(_configuration);

            string json = await azureStorageHelper.DownloadContentJsonAsync();
            List<MenuItems> menuItems = JsonConvert.DeserializeObject<List<MenuItems>>(json);
            menuItems = AddGuidId(menuItems);
            json = JsonConvert.SerializeObject(menuItems);

            HomeViewModel model = new HomeViewModel
            {
                ContentJson = json,
                MenuItems = menuItems
            };

            //azureStorageHelper.DownloadImages(model.MenuItems);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadContentJson([FromBody] string data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data))
                {
                    return BadRequest();
                }

                List<MenuItems> menuItems;
                if (data.Length > 0)
                {
                    menuItems = JsonConvert.DeserializeObject<List<MenuItems>>(HttpUtility.HtmlDecode(data));
                    _azureStorageHelper.UploadContentJsonAsync(HttpUtility.HtmlDecode(data));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> ReloadContentTreeAsync()
        {
            AzureStorageHelper azureStorageHelper = new AzureStorageHelper(_configuration);

            string json = await azureStorageHelper.DownloadContentJsonAsync();
            List<MenuItems> menuItems = JsonConvert.DeserializeObject<List<MenuItems>>(json);
            menuItems = AddGuidId(menuItems);
            json = JsonConvert.SerializeObject(menuItems);

            HomeViewModel model = new HomeViewModel
            {
                ContentJson = json,
                MenuItems = menuItems
            };

            return PartialView("ContentTree", model);
        }

        [HttpGet]
        public async Task<ActionResult> DownloadContentJsonAsync()
        {
            AzureStorageHelper azureStorageHelper = new AzureStorageHelper(_configuration);

            string json = await azureStorageHelper.DownloadContentJsonAsync();
            List<MenuItems> menuItems = JsonConvert.DeserializeObject<List<MenuItems>>(json);
            menuItems = AddGuidId(menuItems);
            json = JsonConvert.SerializeObject(menuItems);

            HomeViewModel model = new HomeViewModel
            {
                ContentJson = json,
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
