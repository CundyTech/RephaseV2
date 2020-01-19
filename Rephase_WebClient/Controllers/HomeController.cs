using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rephase_WebClient.Models;
using RephaseV2.Models;
using RephaseV2.Services;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;

namespace Rephase_WebClient.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            AzureStorageHelper azureStorageHelper = new AzureStorageHelper();

            HomeViewModel model = new HomeViewModel
            {
                ContentJson = await azureStorageHelper.DownloadContentJsonAsync(),
                MenuItems = JsonConvert.DeserializeObject<MenuItems[]>( await azureStorageHelper.DownloadContentJsonAsync())
            };

            //azureStorageHelper.DownloadImages(model.MenuItems);

            return View(model);
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
    }
}
