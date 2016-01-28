using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ImdbDAL;
using ImdbWeb.Areas.Admin.ViewModels.MovieModels;
using Microsoft.AspNet.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

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
					MovieId = model.Id,
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

		public static ValidationResult CheckIdLocal(string id, ValidationContext ctx)
		{
			var db = new ImdbContext(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Imdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
			//var db = (ImdbContext)ctx.ServiceContainer.GetService(typeof(ImdbContext));
			if (db.Movies.Any(m => m.MovieId == id))
			{
				return new ValidationResult("This movie is allready registered");
			}
			return ValidationResult.Success;
		}


		public async Task<IActionResult> Delete(string id)
		{
			var movie = await Db.Movies.FindAsync(id);
			if (movie == null) return HttpNotFound();

			ViewData.Model = new DeleteModel
			{
				Id = movie.MovieId,
				Title = movie.Title,
				ProductionYear = movie.ProductionYear
			};
			return View();
		}

		[HttpDelete]
		[HttpPost]
		[ValidateAntiForgeryToken]
		[ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			var movie = await Db.Movies.FindAsync(id);
			if (movie == null) return HttpNotFound();

			Db.Movies.Remove(movie);
			await Db.SaveChangesAsync();

			return RedirectToAction("Index");
		}
	}
}
