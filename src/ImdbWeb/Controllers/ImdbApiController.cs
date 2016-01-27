using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Xml.Linq;
using ImdbDAL;
using ImdbWeb.ViewModels.ImdbApi;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ImdbWeb.Controllers
{
	public class ImdbApiController : Controller
	{
		[FromServices]
		public ImdbContext Db { get; set; }

		public IActionResult Movies()
		{
			var res = from movie in Db.Movies
					  select new MovieIndexModel{ Id = movie.MovieId, Title = movie.MovieId };

			return Ok(res);
		}

		[Route("Movie/Details/{id}.xml")]
		public IActionResult MovieDetails(string id)
		{
			var movie = Db.Movies.Find(id);
			if (movie == null) return HttpNotFound();

			var doc = new XElement("movie",
				new XAttribute("id", movie.MovieId),
				new XAttribute("title", movie.Title),
				new XAttribute("runLen", movie.RunningLength),
				new XAttribute("prodYear", movie.ProductionYear),
				from p in movie.Actors select new XElement("actor", new XAttribute("name", p.Name)),
				from p in movie.Producers select new XElement("producer", new XAttribute("name", p.Name)),
				from p in movie.Directors select new XElement("director", new XAttribute("name", p.Name)),
				new XCData(movie.Description)
				);
			return Content(doc.ToString(), "application/xml");
		}
	}
}
