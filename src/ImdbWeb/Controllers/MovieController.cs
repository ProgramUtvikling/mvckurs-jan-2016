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

        public string Details(string id)
        {
            return $"MovieController.Details({id})";
        }

        public string Genres()
        {
            return "MovieController.Genres()";
        }

		[Route("Movie/Genre/{genrename}")]
        public string MoviesByGenre(string genrename)
        {
            return $"MovieController.MoviesByGenre({genrename})";
        }
    }
}
