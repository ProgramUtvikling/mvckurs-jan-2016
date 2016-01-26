using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ImdbDAL;
using Microsoft.Extensions.Caching.Memory;
using ImdbWeb.Filters;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ImdbWeb.Controllers
{
	[TimingFilter]
	public class MovieController : Controller
    {
		[FromServices]
		public ImdbContext Db { get; set; }

		public IActionResult Index()
        {
			ViewData.Model = Db.Movies;
            return View();
        }

        public IActionResult Details(string id)
        {
			var movie = Db.Movies.Find(id);
			if(movie == null)
			{
				return HttpNotFound();
			}


			ViewData.Model = movie;
			return View();
		}

		public IActionResult Genres()
        {
			ViewData.Model = Db.Genres;
			return View();
		}

		[Route("Movie/Genre/{genrename}")]
        public IActionResult MoviesByGenre(string genrename)
        {
			ViewData.Model = Db.Movies.Where(m => m.Genre.Name == genrename);
			ViewBag.Genrename = genrename;
			return View("Index");
		}
	}
}
