using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ImdbDAL;
using ImdbWeb.Areas.Admin.ViewModels.MovieModels;
using Microsoft.AspNet.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ImdbWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class MovieController : Controller
	{
		[FromServices]
		public ImdbContext Db { get; set; }

		public async Task<IActionResult> Index()
		{
			ViewData.Model = await (from movie in Db.Movies
									select new IndexModel
									{
										Id = movie.MovieId,
										RunningLength = movie.RunningLength,
										Title = movie.Title
									}).ToListAsync();
			return View();
		}

		public async Task<IActionResult> Create()
		{
			ViewBag.Genres = new SelectList(await Db.Genres.ToListAsync(), "GenreId", "Name");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateModel model)
		{
			if (ModelState.IsValid)
			{
				var movie = new Movie
				{
					MovieId = model.MovieId,
					Title = model.Title,
					OriginalTitle = model.OriginalTitle,
					Description = model.Description,
					ProductionYear = model.ProductionYear,
					RunningLength = model.RunningLengthHours * 60 + model.RunningLengthMinutes,
					GenreId = model.GenreId
				};

				Db.Movies.Add(movie);
				await Db.SaveChangesAsync();

				return RedirectToAction("Index");
			}

			return await Create();
		}
	}
}
