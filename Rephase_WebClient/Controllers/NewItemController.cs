using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RephaseV2.Models;
using RephaseV2.Services.Interfaces;

namespace Rephase_WebClient.Controllers
{
    public class NewItemController : Controller
    {
        private IAzureStorageHelper _azureStorageHelper;
        public NewItemController(IAzureStorageHelper storageHelper)
        {
            _azureStorageHelper = storageHelper;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IEnumerable<IFormFile> files)
        {
            Guid sessionId = Guid.NewGuid();
            foreach (var file in files)
            {
                if (file.FileName == null)
                    return Content("filename not present");

            
                await using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);

                    await _azureStorageHelper.UploadBlobFromWebAsync(stream, file.FileName, sessionId);
                }
            }

            return RedirectToAction("Index", "Home");

        }
    }
}
