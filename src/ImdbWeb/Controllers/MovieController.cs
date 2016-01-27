using System;
using System.Data.Entity;
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

		public async Task<IActionResult> Index()
        {
			ViewData.Model = await Db.Movies.ToListAsync();
            return View();
        }

        public async Task<IActionResult> Details(string id)
        {
			var movie = await Db.Movies.FindAsync(id);
			if(movie == null) return HttpNotFound();

			ViewData.Model = movie;
			return View();
		}

		public async Task<IActionResult> Genres()
        {
			ViewData.Model = await Db.Genres.ToListAsync();
			return View();
		}

		[Route("Movie/Genre/{genrename}")]
        public async Task<IActionResult> MoviesByGenre(string genrename)
        {
			ViewData.Model = await Db.Movies.Where(m => m.Genre.Name == genrename).ToListAsync();
			ViewBag.Genrename = genrename;
			return View("Index");
		}
	}
}
