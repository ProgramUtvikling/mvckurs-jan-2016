using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ImdbDAL;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ImdbWeb.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
			var db = new ImdbContext();
			ViewData.Model = db.Movies;
            return View();
        }

        public IActionResult Details(string id)
        {
			var db = new ImdbContext();
			ViewData.Model = db.Movies.Find(id);
			return View();
		}

		public IActionResult Genres()
        {
			var db = new ImdbContext();
			ViewData.Model = db.Genres;
			return View();
		}

		[Route("Movie/Genre/{genrename}")]
        public IActionResult MoviesByGenre(string genrename)
        {
			var db = new ImdbContext();
			ViewData.Model = db.Movies.Where(m => m.Genre.Name == genrename);
			ViewBag.Genrename = genrename;
			return View("Index");
		}
	}
}
