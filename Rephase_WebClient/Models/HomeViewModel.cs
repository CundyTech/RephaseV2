using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RephaseV2.Models;

namespace Rephase_WebClient.Models
{
    public class HomeViewModel
    {
        public string ContentJson { get; set; }
        public List<MenuItems> MenuItems { get; set; }
    }
}
