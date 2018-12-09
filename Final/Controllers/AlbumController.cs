using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Models;
using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{
    public class AlbumController : Controller
    {
        public IActionResult Index()
        {
            MusicStoreContext context = HttpContext.RequestServices.GetService(typeof(Final.Models.MusicStoreContext)) as MusicStoreContext;

            return View(context.GetAllAlbums());
        }
    }
}